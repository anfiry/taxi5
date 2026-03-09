using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace taxi4
{
    public class ClientPromotionData
    {
        private string connectionString = "Host=localhost;Port=5432;Database=taxi4;Username=postgres;Password=123";

        /// <summary>
        /// Получить список доступных акций для клиента (не использованных ранее)
        /// </summary>
        public DataTable GetAvailablePromotions(int clientId)
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            promotion_id,
                            name AS promotion_name,
                            discont_percent AS discount,
                            description,
                            start_date,
                            end_date,
                            conditions
                        FROM promotion
                        WHERE end_date >= CURRENT_DATE
                          AND promotion_id NOT IN (
                              SELECT promotion_id FROM clent_promotion WHERE clent_id = @clientId
                          )
                        ORDER BY start_date DESC";

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
                    MessageBox.Show($"Ошибка загрузки акций:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return dt;
        }

        /// <summary>
        /// Получить детальную информацию об акции (для двойного клика)
        /// </summary>
        public DataRow GetPromotionDetails(int promotionId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        promotion_id,
                        name AS promotion_name,
                        discont_percent AS discount,
                        description,
                        start_date,
                        end_date,
                        conditions
                    FROM promotion
                    WHERE promotion_id = @promotionId";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@promotionId", promotionId);
                    using (var adapter = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                    }
                }
            }
        }
    }
}