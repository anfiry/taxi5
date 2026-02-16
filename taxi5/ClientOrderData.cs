using Npgsql;
using System;
using System.Data;

namespace taxi4
{
    public class ClientOrderData
    {
        private string connectionString = "Host=localhost;Port=5432;Database=taxi4;Username=postgres;Password=123";

        /// <summary>Получить данные клиента по account_id</summary>
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

        /// <summary>Получить сохранённые точки клиента</summary>
        public DataTable GetClientPoints(int clientId)
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT point_id, address, type, notes 
                               FROM point 
                               WHERE client_id = @clientId 
                               ORDER BY type, address";
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

        /// <summary>Получить доступные промоакции (ещё не использованные клиентом)</summary>
        public DataTable GetAvailablePromotions(int clientId)
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"SELECT p.promotion_id, p.name, p.discont_percent 
                               FROM promotion p
                               WHERE p.end_date >= CURRENT_DATE 
                               AND p.promotion_id NOT IN (
                                   SELECT promotion_id FROM clent_promotion 
                                   WHERE clent_id = @clientId AND used = true
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

        /// <summary>Добавить новую точку (адрес) клиента</summary>
        public int AddPoint(int clientId, string city, string street, string house, string entrance, string type)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string address = $"{city}, {street}, д.{house}";
                if (!string.IsNullOrEmpty(entrance))
                    address += $", под.{entrance}";

                string query = @"INSERT INTO point (address, type, notes, client_id)
                               VALUES (@address, @type, @notes, @clientId)
                               RETURNING point_id";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@type", string.IsNullOrEmpty(type) ? (object)DBNull.Value : type);
                    cmd.Parameters.AddWithValue("@notes", DBNull.Value);
                    cmd.Parameters.AddWithValue("@clientId", clientId);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        /// <summary>Создать новый заказ и при необходимости применить промоакцию</summary>
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
                                string promoQuery = @"INSERT INTO clent_promotion 
                                    (clent_id, promotion_id, assigned_percent, used)
                                    VALUES (@clientId, @promoId, @percent, true)";
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