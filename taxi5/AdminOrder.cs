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
        public DataTable GetOrders(int? statusIdFilter = null, DateTime? dateFrom = null, DateTime? dateTo = null)
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
                    COALESCE(d.last_name || ' ' || d.first_name, 'Не назначен') AS driver_name,
                    t.name AS tariff_name,
                    os.name AS order_status_name,
                    pm.method_name AS payment_method_name,
                    (SELECT city || ', ' || street || ', д.' || house FROM address WHERE address_id = o.address_from) AS address_from_text,
                    (SELECT city || ', ' || street || ', д.' || house FROM address WHERE address_id = o.address_to) AS address_to_text,
                    o.order_datetime,
                    o.start_trip_time,
                    o.end_trip_time,
                    o.final_cost,
                    o.order_status,
                    o.driver_id,
                    -- Объединенная информация об автомобиле
                    CASE 
                        WHEN c2.car_id IS NOT NULL THEN 
                            COALESCE(b.name || ' ' || m.name, 'Неизвестно') || 
                            ' (' || COALESCE(col.name, 'не указан') || ')' ||
                            ' | ' || COALESCE(c2.license_number, '----') || 
                            ' (' || COALESCE(c2.region_code, '00') || ')'
                        ELSE 'Не назначен'
                    END AS car_info
                FROM ""Order"" o
                LEFT JOIN client c ON o.client_id = c.client_id
                LEFT JOIN driver d ON o.driver_id = d.driver_id
                LEFT JOIN car c2 ON d.driver_id = c2.driver_id
                LEFT JOIN brand b ON c2.brand_id = b.brand_id
                LEFT JOIN model m ON c2.model_id = m.model_id
                LEFT JOIN color col ON c2.color_id = col.color_id
                LEFT JOIN tariff t ON o.tariff_id = t.tariff_id
                LEFT JOIN order_status os ON o.order_status = os.order_status_id
                LEFT JOIN payment_method pm ON o.payment_method = pm.method_id
                WHERE 1=1";

                    var cmd = new NpgsqlCommand();
                    cmd.Connection = conn;

                    if (statusIdFilter.HasValue && statusIdFilter.Value > 0)
                    {
                        query += " AND o.order_status = @statusId";
                        cmd.Parameters.AddWithValue("@statusId", statusIdFilter.Value);
                    }

                    if (dateFrom.HasValue)
                    {
                        query += " AND o.order_datetime >= @dateFrom";
                        cmd.Parameters.AddWithValue("@dateFrom", dateFrom.Value);
                    }

                    if (dateTo.HasValue)
                    {
                        query += " AND o.order_datetime <= @dateTo";
                        cmd.Parameters.AddWithValue("@dateTo", dateTo.Value.AddDays(1).AddSeconds(-1));
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

        // ---------- ПОЛУЧЕНИЕ ОТЗЫВА ПО ЗАКАЗУ ----------
        public DataRow GetReview(int orderId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT rating, comment FROM review WHERE orber_id = @orderId";
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
                    MessageBox.Show($"Ошибка загрузки отзыва:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }
    }
}