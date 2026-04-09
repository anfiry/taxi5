using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace taxi4
{
    public class ClientOrderData
    {
        private string connectionString = "Host=localhost;Port=5432;Database=taxi4;Username=postgres;Password=123";

        public DataRow GetClientInfo(int accountId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT client_id, last_name, first_name, patronymic, phone_number
                        FROM client 
                        WHERE account_id = @accountId";

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
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки данных клиента: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }

        // НОВЫЙ МЕТОД: получение точек с названиями
        public DataTable GetClientPointsWithNames(int clientId)
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            cp.clent_point_id,
                            p.name AS point_name,
                            CONCAT(a.city, ', ', a.street, ', д.', a.house,
                                   CASE WHEN a.entrance IS NOT NULL AND a.entrance != '' 
                                        THEN ', подъезд ' || a.entrance ELSE '' END) AS full_address
                        FROM clent_point cp
                        JOIN point p ON cp.point_id = p.point_id
                        JOIN address a ON p.address_id = a.address_id
                        WHERE cp.clent_id = @clientId
                        ORDER BY cp.added_date DESC";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@clientId", clientId);
                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки точек: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        public DataTable GetClientPoints(int clientId)
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            cp.clent_point_id,
                            CONCAT(a.city, ', ', a.street, ', д.', a.house,
                                   CASE WHEN a.entrance IS NOT NULL AND a.entrance != '' 
                                        THEN ', подъезд ' || a.entrance ELSE '' END) AS full_address
                        FROM clent_point cp
                        JOIN point p ON cp.point_id = p.point_id
                        JOIN address a ON p.address_id = a.address_id
                        WHERE cp.clent_id = @clientId
                        ORDER BY cp.added_date DESC";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@clientId", clientId);
                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки точек: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new DataTable();
                }
            }
            return dt;
        }

        public DataTable GetAvailablePromotions(int clientId)
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT p.promotion_id, p.name, p.discont_percent
                        FROM promotion p
                        WHERE p.start_date <= CURRENT_DATE 
                          AND p.end_date >= CURRENT_DATE
                        ORDER BY p.name";

                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки акций: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new DataTable();
                }
            }
            return dt;
        }

        public DataTable GetActiveTariffs()
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT t.tariff_id, t.name, t.base_cost, t.price_per_km
                        FROM tariff t
                        JOIN tariff_status ts ON t.tariff_status = ts.status_id
                        WHERE ts.status_name = 'Активен'
                        ORDER BY t.name";

                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки тарифов: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new DataTable();
                }
            }
            return dt;
        }

        public DataTable GetPointTypes()
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT point_tupe, name FROM point_tupe ORDER BY name";
                using (var adapter = new NpgsqlDataAdapter(query, conn))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public int AddPoint(int clientId, string pointName, int typeId,
                            string city, string street, string house, string entrance)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string addressQuery = @"
                            INSERT INTO address (city, street, house, entrance)
                            VALUES (@city, @street, @house, @entrance)
                            RETURNING address_id";
                        int addressId;
                        using (var addrCmd = new NpgsqlCommand(addressQuery, conn, transaction))
                        {
                            addrCmd.Parameters.AddWithValue("@city", city);
                            addrCmd.Parameters.AddWithValue("@street", street);
                            addrCmd.Parameters.AddWithValue("@house", house);
                            addrCmd.Parameters.AddWithValue("@entrance", entrance ?? (object)DBNull.Value);
                            addressId = Convert.ToInt32(addrCmd.ExecuteScalar());
                        }

                        string pointQuery = @"
                            INSERT INTO point (name, type_id, address_id)
                            VALUES (@name, @typeId, @addressId)
                            RETURNING point_id";
                        int pointId;
                        using (var pointCmd = new NpgsqlCommand(pointQuery, conn, transaction))
                        {
                            pointCmd.Parameters.AddWithValue("@name", pointName);
                            pointCmd.Parameters.AddWithValue("@typeId", typeId);
                            pointCmd.Parameters.AddWithValue("@addressId", addressId);
                            pointId = Convert.ToInt32(pointCmd.ExecuteScalar());
                        }

                        string linkQuery = @"
                            INSERT INTO clent_point (clent_id, point_id, added_date)
                            VALUES (@clientId, @pointId, @addedDate)";
                        using (var linkCmd = new NpgsqlCommand(linkQuery, conn, transaction))
                        {
                            linkCmd.Parameters.AddWithValue("@clientId", clientId);
                            linkCmd.Parameters.AddWithValue("@pointId", pointId);
                            linkCmd.Parameters.AddWithValue("@addedDate", DateTime.Now);
                            linkCmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        return pointId;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка добавления точки:\n{ex.Message}", "Ошибка",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }
            }
        }

        public int CreateOrderWithAddresses(int clientId, string fromAddress, string toAddress,
                                     decimal price, int? promotionId, int tariffId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        int fromAddressId = GetOrCreateAddressId(fromAddress);
                        int toAddressId = GetOrCreateAddressId(toAddress);

                        if (fromAddressId == -1 || toAddressId == -1)
                        {
                            MessageBox.Show("Не удалось определить адреса. Проверьте правильность ввода.",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return -1;
                        }

                        string orderQuery = @"
                            INSERT INTO ""Order"" 
                                (client_id, tariff_id, order_status, payment_method, 
                                 address_from, address_to, order_datetime, final_cost, driver_id)
                            VALUES 
                                (@clientId, @tariffId, 1, 1, @fromAddressId, @toAddressId, @orderDatetime, @price, NULL)
                            RETURNING order_id";

                        using (var cmd = new NpgsqlCommand(orderQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@clientId", clientId);
                            cmd.Parameters.AddWithValue("@tariffId", tariffId);
                            cmd.Parameters.AddWithValue("@fromAddressId", fromAddressId);
                            cmd.Parameters.AddWithValue("@toAddressId", toAddressId);
                            cmd.Parameters.AddWithValue("@orderDatetime", DateTime.Now);
                            cmd.Parameters.AddWithValue("@price", price);

                            int orderId = (int)cmd.ExecuteScalar();

                            if (promotionId.HasValue && promotionId.Value > 0)
                            {
                                string promoQuery = "SELECT discont_percent FROM promotion WHERE promotion_id = @promotionId";
                                decimal discountPercent = 0;
                                using (var promoCmd = new NpgsqlCommand(promoQuery, conn, transaction))
                                {
                                    promoCmd.Parameters.AddWithValue("@promotionId", promotionId.Value);
                                    discountPercent = Convert.ToDecimal(promoCmd.ExecuteScalar());
                                }

                                string orderPromoQuery = @"
                                    INSERT INTO order_promotion 
                                        (order_id, promotion_percent, promotion_name, promotion_amount)
                                    VALUES 
                                        (@orderId, @discountPercent, @promotionName, @discountAmount)";

                                using (var promoOrderCmd = new NpgsqlCommand(orderPromoQuery, conn, transaction))
                                {
                                    promoOrderCmd.Parameters.AddWithValue("@orderId", orderId);
                                    promoOrderCmd.Parameters.AddWithValue("@discountPercent", discountPercent);
                                    promoOrderCmd.Parameters.AddWithValue("@promotionName", "Акция");
                                    promoOrderCmd.Parameters.AddWithValue("@discountAmount", price * (discountPercent / 100));
                                    promoOrderCmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            return orderId;
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show($"Ошибка при создании заказа: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }
            }
        }

        private int GetOrCreateAddressId(string addressText)
        {
            if (string.IsNullOrWhiteSpace(addressText))
                return -1;

            string[] parts = addressText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 3)
            {
                MessageBox.Show($"Адрес должен содержать город, улицу и номер дома.\nПример: Воронеж, Ленина, д.10",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            string city = parts[0].Trim();
            string street = parts[1].Trim();
            string house = "";
            string entrance = "";

            for (int i = 2; i < parts.Length; i++)
            {
                string part = parts[i].Trim().ToLower();

                if (part.Contains("подъезд") || part.Contains("под.") || part.Contains("под"))
                {
                    entrance = part.Replace("подъезд", "").Replace("под.", "").Replace("под", "").Trim();
                }
                else if (string.IsNullOrEmpty(house))
                {
                    string tempHouse = part.Replace("д.", "").Replace("д", "").Replace("дом", "").Trim();
                    if (!string.IsNullOrEmpty(tempHouse))
                    {
                        house = tempHouse;
                    }
                }
            }

            if (string.IsNullOrEmpty(house))
            {
                MessageBox.Show($"Не удалось определить номер дома.\n\nВведенный адрес: {addressText}\n\n" +
                                "Пожалуйста, используйте формат:\n" +
                                "Воронеж, Ленина, д.10\n" +
                                "или\n" +
                                "Воронеж, Ленина, д.10, подъезд 3",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            if (!city.Equals("Воронеж", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Доставка осуществляется только по городу Воронеж",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                string checkQuery = @"
                    SELECT address_id FROM address 
                    WHERE city = @city AND street = @street AND house = @house 
                    AND (entrance = @entrance OR (entrance IS NULL AND @entrance IS NULL))";

                using (var cmd = new NpgsqlCommand(checkQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@street", street);
                    cmd.Parameters.AddWithValue("@house", house);
                    cmd.Parameters.AddWithValue("@entrance", string.IsNullOrEmpty(entrance) ? (object)DBNull.Value : entrance);

                    var existingId = cmd.ExecuteScalar();
                    if (existingId != null && existingId != DBNull.Value)
                        return Convert.ToInt32(existingId);
                }

                string insertQuery = @"
                    INSERT INTO address (city, street, house, entrance) 
                    VALUES (@city, @street, @house, @entrance) 
                    RETURNING address_id";

                using (var cmd = new NpgsqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@street", street);
                    cmd.Parameters.AddWithValue("@house", house);
                    cmd.Parameters.AddWithValue("@entrance", string.IsNullOrEmpty(entrance) ? (object)DBNull.Value : entrance);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public async Task<(double? lat, double? lon)> GeocodeAddressYandex(string address, string apiKey)
        {
            try
            {
                string encodedAddress = Uri.EscapeDataString(address);
                string url = $"https://geocode-maps.yandex.ru/1.x/?apikey={apiKey}&geocode={encodedAddress}&format=json";

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode) return (null, null);

                    string json = await response.Content.ReadAsStringAsync();
                    var obj = JObject.Parse(json);
                    var point = obj["response"]["GeoObjectCollection"]["featureMember"][0]["GeoObject"]["Point"]["pos"]?.ToString();
                    if (!string.IsNullOrEmpty(point))
                    {
                        var parts = point.Split(' ');
                        double lon = double.Parse(parts[0], System.Globalization.CultureInfo.InvariantCulture);
                        double lat = double.Parse(parts[1], System.Globalization.CultureInfo.InvariantCulture);
                        return (lat, lon);
                    }
                }
            }
            catch
            {
            }
            return (null, null);
        }

        public async Task<double?> GetDistanceByCoords(double lat1, double lon1, double lat2, double lon2, string apiKey)
        {
            try
            {
                string waypoints = $"{lon1},{lat1}|{lon2},{lat2}";
                string url = $"https://api.routing.yandex.net/v2/route?apikey={apiKey}&waypoints={waypoints}&mode=driving";

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode) return null;

                    string json = await response.Content.ReadAsStringAsync();
                    var obj = JObject.Parse(json);
                    var distanceValue = obj["routes"]?[0]?["legs"]?[0]?["distance"]?["value"]?.Value<double>();
                    if (distanceValue.HasValue)
                        return distanceValue.Value / 1000.0;
                }
            }
            catch { }
            return null;
        }

        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371;
            var dLat = (lat2 - lat1) * Math.PI / 180;
            var dLon = (lon2 - lon1) * Math.PI / 180;
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }
    }
}