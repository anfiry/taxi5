using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace taxi4
{
    public class ClientTrackData
    {
        private string connectionString = "Host=localhost;Port=5432;Database=taxi4;Username=postgres;Password=123";

        public DataTable GetActiveOrders(int clientId)
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            o.order_id,
                            o.order_datetime,
                            CONCAT(a_from.city, ', ', a_from.street, ', д.', a_from.house,
                                   CASE WHEN a_from.entrance IS NOT NULL AND a_from.entrance != '' 
                                        THEN ', подъезд ' || a_from.entrance ELSE '' END) AS address_from,
                            CONCAT(a_to.city, ', ', a_to.street, ', д.', a_to.house,
                                   CASE WHEN a_to.entrance IS NOT NULL AND a_to.entrance != '' 
                                        THEN ', подъезд ' || a_to.entrance ELSE '' END) AS address_to,
                            COALESCE(t.name, 'Не указан') AS tariff_name,
                            COALESCE(os.name, 'Неизвестно') AS order_status,
                            COALESCE(d.last_name || ' ' || d.first_name, 'Не назначен') AS driver_name,
                            COALESCE(d.phone_number, 'Не указан') AS driver_phone
                        FROM ""Order"" o
                        LEFT JOIN address a_from ON o.address_from = a_from.address_id
                        LEFT JOIN address a_to ON o.address_to = a_to.address_id
                        LEFT JOIN tariff t ON o.tariff_id = t.tariff_id
                        LEFT JOIN order_status os ON o.order_status = os.order_status_id
                        LEFT JOIN driver d ON o.driver_id = d.driver_id
                        WHERE o.client_id = @clientId
                          AND o.order_status IN (1, 2)
                        ORDER BY o.order_datetime DESC";

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
                    MessageBox.Show($"Ошибка загрузки активных заказов:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        public DataRow GetOrderDetails(int orderId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            o.order_id,
                            o.order_datetime,
                            CONCAT(a_from.city, ', ', a_from.street, ', д.', a_from.house,
                                   CASE WHEN a_from.entrance IS NOT NULL AND a_from.entrance != '' 
                                        THEN ', подъезд ' || a_from.entrance ELSE '' END) AS address_from_full,
                            CONCAT(a_to.city, ', ', a_to.street, ', д.', a_to.house,
                                   CASE WHEN a_to.entrance IS NOT NULL AND a_to.entrance != '' 
                                        THEN ', подъезд ' || a_to.entrance ELSE '' END) AS address_to_full,
                            COALESCE(t.name, 'Не указан') AS tariff_name,
                            COALESCE(os.name, 'Неизвестно') AS status_name,
                            COALESCE(d.last_name || ' ' || d.first_name, 'Не назначен') AS driver_full_name,
                            COALESCE(d.phone_number, 'Не указан') AS driver_phone
                        FROM ""Order"" o
                        LEFT JOIN address a_from ON o.address_from = a_from.address_id
                        LEFT JOIN address a_to ON o.address_to = a_to.address_id
                        LEFT JOIN tariff t ON o.tariff_id = t.tariff_id
                        LEFT JOIN order_status os ON o.order_status = os.order_status_id
                        LEFT JOIN driver d ON o.driver_id = d.driver_id
                        WHERE o.order_id = @orderId";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
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
                    MessageBox.Show($"Ошибка загрузки деталей заказа:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }
    }
}