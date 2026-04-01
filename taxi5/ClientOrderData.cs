using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration; // для чтения ключа из App.config

namespace taxi4
{
    public class ClientOrderData
    {
        private string connectionString = "Host=localhost;Port=5432;Database=taxi4;Username=postgres;Password=123";

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
                string query = @"
            SELECT 
                p.point_id,
                CONCAT(a.city, ', ', a.street, ', д.', a.house,
                       CASE WHEN a.entrance IS NOT NULL AND a.entrance != '' 
                            THEN ', подъезд ' || a.entrance ELSE '' END) AS full_address,
                pt.name AS type_name
            FROM point p
            JOIN clent_point cp ON p.point_id = cp.point_id
            JOIN address a ON p.address_id = a.address_id
            JOIN point_tupe pt ON p.type_id = pt.point_tupe
            WHERE cp.clent_id = @clientId
            ORDER BY pt.name, p.name";
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
                string query = @"
                    SELECT p.promotion_id, p.name, p.discont_percent 
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

        public DataTable GetActiveTariffs()
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT tariff_id, name, base_cost, price_per_km
                    FROM tariff
                    WHERE tariff_status = (SELECT status_id FROM tariff_status WHERE status_name = 'Активен')";
                using (var adapter = new NpgsqlDataAdapter(query, conn))
                {
                    adapter.Fill(dt);
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

        /// <summary>
        /// Создание заказа с произвольными адресами (без использования point)
        /// </summary>
        public int CreateOrderWithAddresses(int clientId, string fromAddress, string toAddress, decimal price, int? promotionId, int tariffId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Получаем или создаём address_id для каждого адреса
                        int fromAddressId = GetOrCreateAddress(conn, transaction, fromAddress);
                        int toAddressId = GetOrCreateAddress(conn, transaction, toAddress);

                        string orderQuery = @"
                            INSERT INTO ""Order"" 
                                (client_id, driver_id, tariff_id, order_status, payment_method, 
                                 address_from, address_to, order_datetime, final_cost)
                            VALUES 
                                (@clientId, NULL, @tariffId, @statusId, @paymentId,
                                 @fromAddressId, @toAddressId,
                                 @orderDate, @price)
                            RETURNING order_id";

                        using (var cmd = new NpgsqlCommand(orderQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@clientId", clientId);
                            cmd.Parameters.AddWithValue("@tariffId", tariffId);
                            cmd.Parameters.AddWithValue("@statusId", 1); // "Создан"
                            cmd.Parameters.AddWithValue("@paymentId", 1); // "Наличные"
                            cmd.Parameters.AddWithValue("@fromAddressId", fromAddressId);
                            cmd.Parameters.AddWithValue("@toAddressId", toAddressId);
                            cmd.Parameters.AddWithValue("@orderDate", DateTime.Now);
                            cmd.Parameters.AddWithValue("@price", price);

                            int orderId = Convert.ToInt32(cmd.ExecuteScalar());

                            if (promotionId.HasValue)
                            {
                                int discountPercent = GetPromotionDiscount(promotionId.Value);
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

        private int GetOrCreateAddress(NpgsqlConnection conn, NpgsqlTransaction transaction, string addressText)
        {
            // Проверяем, существует ли уже такой адрес в таблице address
            string checkQuery = "SELECT address_id FROM address WHERE city || ', ' || street || ', ' || house = @addressText";
            using (var checkCmd = new NpgsqlCommand(checkQuery, conn, transaction))
            {
                checkCmd.Parameters.AddWithValue("@addressText", addressText);
                var existingId = checkCmd.ExecuteScalar();
                if (existingId != null)
                    return Convert.ToInt32(existingId);
            }

            // Разбираем адрес на части (простой парсинг)
            // Предполагаемый формат: "Город, Улица, Номер"
            string city = "Воронеж"; // по умолчанию
            string street = "";
            string house = "";

            string[] parts = addressText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 1)
                city = parts[0].Trim();
            if (parts.Length >= 2)
                street = parts[1].Trim();
            if (parts.Length >= 3)
            {
                string housePart = parts[2].Trim();
            
            }

            // Если не удалось разобрать, сохраняем как есть (хотя бы для уникальности)
            if (string.IsNullOrEmpty(street))
                street = "Неизвестная улица";
            if (string.IsNullOrEmpty(house))
                house = "0";

            string insertQuery = @"
                INSERT INTO address (city, street, house, entrance)
                VALUES (@city, @street, @house, @entrance)
                RETURNING address_id";
            using (var insertCmd = new NpgsqlCommand(insertQuery, conn, transaction))
            {
                insertCmd.Parameters.AddWithValue("@city", city);
                insertCmd.Parameters.AddWithValue("@street", street);
                insertCmd.Parameters.AddWithValue("@house", house);
                insertCmd.Parameters.AddWithValue("@entrance", DBNull.Value);
                return Convert.ToInt32(insertCmd.ExecuteScalar());
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

        /// <summary>
        /// Получить координаты адреса через Яндекс.Геокодер
        /// </summary>
        public async Task<(double? lat, double? lon)> GeocodeAddressYandex(string address, string apiKey)
        {
            try
            {
                // Кодируем адрес, чтобы он безопасно вставился в URL
                string encodedAddress = Uri.EscapeDataString(address);
                // Формируем адрес запроса
                string url = $"https://geocode-maps.yandex.ru/1.x/?apikey={apiKey}&geocode={encodedAddress}&format=json";

                using (var client = new HttpClient())
                {
                    // Отправляем запрос и получаем ответ
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode) return (null, null); // ошибка

                    // Читаем ответ как строку
                    string json = await response.Content.ReadAsStringAsync();
                    // Преобразуем JSON в удобный для чтения объект
                    var obj = Newtonsoft.Json.Linq.JObject.Parse(json);
                    // Достаём координаты из ответа
                    var point = obj["response"]["GeoObjectCollection"]["featureMember"][0]["GeoObject"]["Point"]["pos"]?.ToString();
                    if (!string.IsNullOrEmpty(point))
                    {
                        // Координаты приходят в формате "долгота широта" (через пробел)
                        var parts = point.Split(' ');
                        double lon = double.Parse(parts[0], System.Globalization.CultureInfo.InvariantCulture);
                        double lat = double.Parse(parts[1], System.Globalization.CultureInfo.InvariantCulture);
                        return (lat, lon);
                    }
                }
            }
            catch
            {
                // Если что-то пошло не так, возвращаем null
            }
            return (null, null);
        }


/// Получить расстояние (в км) между двумя точками через Яндекс.Маршрутизатор

public async Task<double?> GetDistanceByCoords(double lat1, double lon1, double lat2, double lon2, string apiKey)
        {
            try
            {
                // Формируем строку с координатами в формате "долгота,широта"
                string waypoints = $"{lon1},{lat1}|{lon2},{lat2}";
                string url = $"https://api.routing.yandex.net/v2/route?apikey={apiKey}&waypoints={waypoints}&mode=driving";

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    if (!response.IsSuccessStatusCode) return null;

                    string json = await response.Content.ReadAsStringAsync();
                    var obj = Newtonsoft.Json.Linq.JObject.Parse(json);
                    // Ищем расстояние в метрах
                    var distanceValue = obj["routes"]?[0]?["legs"]?[0]?["distance"]?["value"]?.Value<double>();
                    if (distanceValue.HasValue)
                        return distanceValue.Value / 1000.0; // переводим в километры
                }
            }
            catch { }
            return null;
        }

        /// <summary>
        /// Расчёт расстояния между двумя точками по формуле гаверсинуса (в километрах)
        /// </summary>
        public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // радиус Земли в км
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

 