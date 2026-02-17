using Npgsql;
using System;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace taxi4
{
    public class ClientOrderData
    {
        private string connectionString = "Host=localhost;Port=5432;Database=taxi4;Username=postgres;Password=123";
        private static readonly HttpClient httpClient = new HttpClient();

        public DataRow GetClientInfo(int accountId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT client_id, first_name, last_name FROM client WHERE account_id = @accountId";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@accountId", accountId);
                    using (var adapter = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                    }
                }
            }
        }

        public DataTable GetClientPoints(int clientId)
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT point_id, name AS address, type 
                               FROM point 
                               WHERE client_id = @clientId 
                               ORDER BY type, name";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@clientId", clientId);
                    using (var adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable GetAvailablePromotions(int clientId)
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                // Исключаем промоакции, которые уже были использованы (есть запись в clent_promotion)
                string query = @"SELECT p.promotion_id, p.name, p.discont_percent 
                               FROM promotion p
                               WHERE p.end_date >= CURRENT_DATE 
                               AND p.promotion_id NOT IN (
                                   SELECT promotion_id FROM clent_promotion 
                                   WHERE clent_id = @clientId
                               )";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@clientId", clientId);
                    using (var adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public async Task<int> AddPointWithGeocodingAsync(int clientId, string city, string street, string house, string entrance, string type)
        {
            string fullAddress = $"{city}, {street}, {house}";
            if (!string.IsNullOrEmpty(entrance))
                fullAddress += $", подъезд {entrance}";

            var (latitude, longitude) = await GeocodeAddressAsync(fullAddress);
            if (latitude == null || longitude == null)
            {
                MessageBox.Show("Не удалось определить координаты адреса. Проверьте правильность ввода или попробуйте позже.",
                                "Ошибка геокодирования", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            using (var conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();
                string query = @"INSERT INTO point (name, type, client_id, latitude, longitude)
                               VALUES (@name, @type, @clientId, @latitude, @longitude)
                               RETURNING point_id";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", fullAddress);
                    cmd.Parameters.AddWithValue("@type", string.IsNullOrEmpty(type) ? (object)DBNull.Value : type);
                    cmd.Parameters.AddWithValue("@clientId", clientId);
                    cmd.Parameters.AddWithValue("@latitude", latitude.Value);
                    cmd.Parameters.AddWithValue("@longitude", longitude.Value);

                    object result = await cmd.ExecuteScalarAsync();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

        private async Task<(double? lat, double? lon)> GeocodeAddressAsync(string address)
        {
            try
            {
                string encodedAddress = Uri.EscapeDataString(address);
                string url = $"https://nominatim.openstreetmap.org/search?q={encodedAddress}&format=json&limit=1";

                using (var request = new HttpRequestMessage(HttpMethod.Get, url))
                {
                    request.Headers.Add("User-Agent", "Taxi4Client/1.0");
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();
                    JArray array = JArray.Parse(json);
                    if (array.Count > 0)
                    {
                        double lat = array[0]["lat"].Value<double>();
                        double lon = array[0]["lon"].Value<double>();
                        return (lat, lon);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка геокодирования: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return (null, null);
        }

        public int CreateOrder(int clientId, int startPointId, int endPointId, decimal price, int? promotionId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string orderQuery = @"INSERT INTO orders 
                            (client_id, start_point_id, end_point_id, order_date, 
                             order_status_id, payment_method_id, price)
                            VALUES (@clientId, @startPoint, @endPoint, @orderDate,
                                    @statusId, @paymentId, @price)
                            RETURNING order_id";
                        using (var cmd = new NpgsqlCommand(orderQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@clientId", clientId);
                            cmd.Parameters.AddWithValue("@startPoint", startPointId);
                            cmd.Parameters.AddWithValue("@endPoint", endPointId);
                            cmd.Parameters.AddWithValue("@orderDate", DateTime.Now);
                            cmd.Parameters.AddWithValue("@statusId", 1); // Новый
                            cmd.Parameters.AddWithValue("@paymentId", 1); // Наличные
                            cmd.Parameters.AddWithValue("@price", price);

                            int orderId = Convert.ToInt32(cmd.ExecuteScalar());

                            if (promotionId.HasValue)
                            {
                                int discountPercent = GetPromotionDiscount(promotionId.Value);
                                // Вставляем запись об использовании промоакции (без поля used)
                                string promoQuery = @"INSERT INTO clent_promotion 
                                    (clent_id, promotion_id, assigned_percent)
                                    VALUES (@clientId, @promoId, @percent)";
                                using (var promoCmd = new NpgsqlCommand(promoQuery, conn, transaction))
                                {
                                    promoCmd.Parameters.AddWithValue("@clientId", clientId);
                                    promoCmd.Parameters.AddWithValue("@promoId", promotionId.Value);
                                    promoCmd.Parameters.AddWithValue("@percent", discountPercent);
                                    promoCmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            return orderId;
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private int GetPromotionDiscount(int promotionId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT discont_percent FROM promotion WHERE promotion_id = @promoId";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@promoId", promotionId);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
    }
}