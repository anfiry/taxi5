using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace taxi4
{
    public class AdminPromotion
    {
        private string connectionString = "Host=localhost;Port=5432;Database=taxi4;Username=postgres;Password=123";

        // ---------- ПОЛУЧЕНИЕ СПИСКА АКЦИЙ ----------
        public DataTable GetPromotions()
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
                            name,
                            discont_percent,
                            description,
                            start_date,
                            end_date,
                            conditions
                        FROM promotion
                        ORDER BY promotion_id";

                    using (var adapter = new NpgsqlDataAdapter(query, conn))
                    {
                        adapter.Fill(dt);
                    }

                    // Форматирование дат для отображения
                    if (dt.Columns.Contains("start_date") && dt.Columns.Contains("end_date"))
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["start_date"] != DBNull.Value)
                                row["start_date"] = Convert.ToDateTime(row["start_date"]).ToString("dd.MM.yyyy");
                            if (row["end_date"] != DBNull.Value)
                                row["end_date"] = Convert.ToDateTime(row["end_date"]).ToString("dd.MM.yyyy");
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

        // ---------- ДОБАВЛЕНИЕ АКЦИИ ----------
        public bool AddPromotion(string name, decimal discountPercent, string description,
                                 DateTime startDate, DateTime endDate, string conditions)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO promotion 
                            (name, discont_percent, description, start_date, end_date, conditions)
                        VALUES 
                            (@name, @discount, @description, @startDate, @endDate, @conditions)";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", name ?? "");
                        cmd.Parameters.AddWithValue("@discount", discountPercent);
                        cmd.Parameters.AddWithValue("@description", description ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@startDate", startDate);
                        cmd.Parameters.AddWithValue("@endDate", endDate);
                        cmd.Parameters.AddWithValue("@conditions", conditions ?? (object)DBNull.Value);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка добавления акции:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // ---------- ОБНОВЛЕНИЕ АКЦИИ ----------
        public bool UpdatePromotion(int promotionId, string name, decimal discountPercent, string description,
                                     DateTime startDate, DateTime endDate, string conditions)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        UPDATE promotion SET
                            name = @name,
                            discont_percent = @discount,
                            description = @description,
                            start_date = @startDate,
                            end_date = @endDate,
                            conditions = @conditions
                        WHERE promotion_id = @promotionId";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@promotionId", promotionId);
                        cmd.Parameters.AddWithValue("@name", name ?? "");
                        cmd.Parameters.AddWithValue("@discount", discountPercent);
                        cmd.Parameters.AddWithValue("@description", description ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@startDate", startDate);
                        cmd.Parameters.AddWithValue("@endDate", endDate);
                        cmd.Parameters.AddWithValue("@conditions", conditions ?? (object)DBNull.Value);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка обновления акции:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        // ---------- УДАЛЕНИЕ АКЦИИ ----------
        public bool DeletePromotion(int promotionId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM promotion WHERE promotion_id = @promotionId";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@promotionId", promotionId);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (NpgsqlException ex) when (ex.Message.Contains("23503") || ex.Message.Contains("foreign key"))
                {
                    MessageBox.Show("Невозможно удалить акцию: она используется в назначениях клиентам.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления акции:\n{ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
    }
}