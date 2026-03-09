using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

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

        /// <summary>
        /// Получение сохранённых точек клиента с полным адресом (как в ClientPointForm)
        /// </summary>
        public DataTable GetClientPoints(int clientId)
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        p.point_id,
                        CONCAT(a.city, ', ', a.street, ', д. ', a.house,
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

        /// <summary>
        /// Получение доступных промоакций (ещё не использованных клиентом)
        /// </summary>
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

        /// <summary>
        /// Получение активных тарифов
        /// </summary>
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

        /// <summary>
        /// Получение всех типов точек (для комбобокса в панели добавления)
        /// </summary>
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

        /// <summary>
        /// Добавление новой точки (аналог ClientPointForm.AddPoint)
        /// </summary>
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
                        // 1. Добавляем адрес
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

                        // 2. Добавляем точку
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

                        // 3. Связываем с клиентом
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
        /// Создание заказа
        /// </summary>
        public int CreateOrder(int clientId, int startPointId, int endPointId, decimal price, int? promotionId, int tariffId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        string orderQuery = @"
                            INSERT INTO ""Order"" 
                                (client_id, driver_id, tariff_id, order_status, payment_method, 
                                 address_from, address_to, order_datetime, final_cost)
                            VALUES 
                                (@clientId, NULL, @tariffId, @statusId, @paymentId,
                                 (SELECT address_id FROM point WHERE point_id = @startPointId),
                                 (SELECT address_id FROM point WHERE point_id = @endPointId),
                                 @orderDate, @price)
                            RETURNING order_id";

                        using (var cmd = new NpgsqlCommand(orderQuery, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@clientId", clientId);
                            cmd.Parameters.AddWithValue("@tariffId", tariffId);
                            cmd.Parameters.AddWithValue("@statusId", 1); // "Создан" (должен быть в order_status)
                            cmd.Parameters.AddWithValue("@paymentId", 1); // "Наличные" (должен быть в payment_method)
                            cmd.Parameters.AddWithValue("@startPointId", startPointId);
                            cmd.Parameters.AddWithValue("@endPointId", endPointId);
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