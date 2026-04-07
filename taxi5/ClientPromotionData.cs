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
        /// Получить список доступных акций для клиента (только те, которые можно использовать)
        /// </summary>
        public DataTable GetAvailablePromotions(int clientId)
        {
            var dt = new DataTable();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    // Получаем все активные акции
                    string query = @"
                        SELECT 
                            p.promotion_id,
                            p.name AS promotion_name,
                            p.discont_percent AS discount,
                            COALESCE(p.description, '') AS description,
                            p.start_date,
                            p.end_date,
                            COALESCE(p.conditions, '') AS conditions
                        FROM promotion p
                        WHERE p.start_date <= CURRENT_DATE AND p.end_date >= CURRENT_DATE
                        ORDER BY p.promotion_id";

                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dt);
                    }

                    // Фильтруем акции, которые можно использовать
                    DataTable filteredDt = dt.Clone(); // Копируем структуру

                    foreach (DataRow row in dt.Rows)
                    {
                        int promoId = Convert.ToInt32(row["promotion_id"]);
                        bool canUse = CanUsePromotion(clientId, promoId);

                        if (canUse)
                        {
                            filteredDt.ImportRow(row);
                        }
                    }

                    return filteredDt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки акций:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return new DataTable();
                }
            }
        }

        /// <summary>
        /// Проверить, может ли клиент использовать акцию
        /// </summary>
        public bool CanUsePromotion(int clientId, int promotionId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Получаем информацию об акции
                    string promoQuery = @"
                        SELECT name, description, discont_percent 
                        FROM promotion 
                        WHERE promotion_id = @promotionId";

                    string promoName = "";
                    string promoDesc = "";
                    using (var cmd = new NpgsqlCommand(promoQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@promotionId", promotionId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                promoName = reader["name"].ToString();
                                promoDesc = reader["description"].ToString();
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }

                    // Проверяем, является ли акция "для первых клиентов"
                    bool isFirstTimePromo = promoName.Contains("первый") || promoName.Contains("новый") ||
                                           promoName.Contains("первых") || promoDesc.Contains("первый") ||
                                           promoDesc.Contains("новый");

                    if (isFirstTimePromo)
                    {
                        // Проверяем, использовал ли клиент уже эту акцию
                        string checkQuery = @"
                            SELECT COUNT(*) 
                            FROM clent_promotion 
                            WHERE clent_id = @clientId AND promotion_id = @promotionId";

                        using (var checkCmd = new NpgsqlCommand(checkQuery, conn))
                        {
                            checkCmd.Parameters.AddWithValue("@clientId", clientId);
                            checkCmd.Parameters.AddWithValue("@promotionId", promotionId);
                            int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                            return count == 0; // Можно использовать только 1 раз
                        }
                    }

                    // Для остальных акций - без ограничений
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка проверки акции:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Применить акцию для клиента (записать использование)
        /// </summary>
        public bool ApplyPromotion(int clientId, int promotionId, decimal assignedPercent)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO clent_promotion (clent_id, promotion_id, assigned_percent)
                        VALUES (@clientId, @promotionId, @assignedPercent)";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@clientId", clientId);
                        cmd.Parameters.AddWithValue("@promotionId", promotionId);
                        cmd.Parameters.AddWithValue("@assignedPercent", assignedPercent);
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка применения акции:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        /// <summary>
        /// Получить детальную информацию об акции
        /// </summary>
        public DataRow GetPromotionDetails(int promotionId)
        {
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
                            COALESCE(description, '') AS description,
                            start_date,
                            end_date,
                            COALESCE(conditions, '') AS conditions
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
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки деталей акции:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }
    }
}