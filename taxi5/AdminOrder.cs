using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace taxi4
{
    public class AdminOrder
    {
        private string connectionString = "Host=localhost;Port=5432;Database=taxi4;Username=postgres;Password=123";

        // ---------- ПОЛУЧЕНИЕ СПИСКА ЗАКАЗОВ ----------
        public DataTable GetOrders(string statusFilter = null, DateTime? dateFrom = null, DateTime? dateTo = null)
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
                            c.last_name || ' ' || c.first_name AS client_name,
                            d.last_name || ' ' || d.first_name AS driver_name,
                            t.name AS tariff_name,
                            os.name AS order_status_name,
                            pm.method_name AS payment_method_name,
                            (SELECT city || ', ' || street || ', д.' || house FROM address WHERE address_id = o.address_from) AS address_from_text,
                            (SELECT city || ', ' || street || ', д.' || house FROM address WHERE address_id = o.address_to) AS address_to_text,
                            o.order_datetime,
                            o.final_cost,
                            o.order_status AS status_id
                        FROM ""Order"" o
                        LEFT JOIN client c ON o.client_id = c.client_id
                        LEFT JOIN driver d ON o.driver_id = d.driver_id
                        LEFT JOIN tariff t ON o.tariff_id = t.tariff_id
                        LEFT JOIN order_status os ON o.order_status = os.order_status_id
                        LEFT JOIN payment_method pm ON o.payment_method = pm.method_id
                        WHERE 1=1";

                    var cmd = new NpgsqlCommand();
                    cmd.Connection = conn;

                    if (!string.IsNullOrEmpty(statusFilter))
                    {
                        query += " AND os.name = @status";
                        cmd.Parameters.AddWithValue("@status", statusFilter);
                    }

                    if (dateFrom.HasValue)
                    {
                        query += " AND o.order_datetime >= @dateFrom";
                        cmd.Parameters.AddWithValue("@dateFrom", dateFrom.Value);
                    }

                    if (dateTo.HasValue)
                    {
                        query += " AND o.order_datetime <= @dateTo";
                        cmd.Parameters.AddWithValue("@dateTo", dateTo.Value.AddDays(1).AddSeconds(-1)); // конец дня
                    }

                    query += " ORDER BY o.order_datetime DESC";

                    cmd.CommandText = query;

                    using (var adapter = new NpgsqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки заказов:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        // ---------- ПОЛУЧЕНИЕ СПИСКА СТАТУСОВ ЗАКАЗОВ ----------
        public DataTable GetOrderStatuses()
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT order_status_id, name FROM order_status ORDER BY order_status_id";
                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                        adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки статусов заказов:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }
    }
}