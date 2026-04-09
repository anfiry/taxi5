using System;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace taxi4
{
    public partial class DriverShiftsForm : Form
    {
        private int driverId;
        private string connectionString;
        private bool back = false;
        private int accountId;

        public void OnClosed()
        {
            if (back)
            { back = false; }
            else { Application.Exit(); }
        }

        public DriverShiftsForm(int driverId, string connectionString, int accountId)
        {
            this.driverId = driverId;
            this.connectionString = connectionString;
            this.accountId = accountId;

            InitializeComponent();

            // Настройка стилей колонок ПОСЛЕ InitializeComponent
            ConfigureDataGridViewColumns();

            // Подписываемся на события
            this.btnBack.Click += BtnBack_Click;
            this.Resize += DriverShiftsForm_Resize;
            this.Load += DriverShiftsForm_Load;

            // Устанавливаем позицию кнопки
            UpdateButtonPosition();
        }

        private void ConfigureDataGridViewColumns()
        {
            // Настройка выравнивания колонок
            if (dgvHistory.Columns["Duration"] != null)
                dgvHistory.Columns["Duration"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (dgvHistory.Columns["Status"] != null)
                dgvHistory.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void DriverShiftsForm_Load(object sender, EventArgs e)
        {
            LoadHistory();
        }

        private void DriverShiftsForm_Resize(object sender, EventArgs e)
        {
            UpdateButtonPosition();
        }

        private void UpdateButtonPosition()
        {
            if (btnBack != null)
            {
                btnBack.Location = new Point(this.ClientSize.Width - 120, 12);
            }
        }

        /// <summary>
        /// Форматирует TimeSpan в читаемый вид
        /// </summary>
        private string FormatDuration(TimeSpan duration)
        {
            if (duration.TotalSeconds < 0)
                return "0 мин";

            int days = duration.Days;
            int hours = duration.Hours;
            int minutes = duration.Minutes;

            if (days > 0)
            {
                if (hours > 0 && minutes > 0)
                    return $"{days} д {hours} ч {minutes} мин";
                else if (hours > 0)
                    return $"{days} д {hours} ч";
                else if (minutes > 0)
                    return $"{days} д {minutes} мин";
                else
                    return $"{days} д";
            }
            else if (hours > 0)
            {
                if (minutes > 0)
                    return $"{hours} ч {minutes} мин";
                else
                    return $"{hours} ч";
            }
            else if (minutes > 0)
            {
                return $"{minutes} мин";
            }
            else
            {
                return "0 мин";
            }
        }

        private void LoadHistory()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    // Запрос с датой и временем начала и конца смены
                    string query = @"
                        SELECT 
                            ws.start_datetime,
                            ws.end_datetime,
                            ss.status_name
                        FROM work_schedule ws
                        LEFT JOIN shift_status ss ON ws.shift_status_id = ss.shiaft_status_id
                        WHERE ws.driver_id = @driver_id
                        ORDER BY ws.start_datetime DESC";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@driver_id", driverId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            dgvHistory.Rows.Clear();

                            while (reader.Read())
                            {
                                DateTime startDateTime = reader.GetDateTime(0);
                                DateTime? endDateTime = reader.IsDBNull(1) ? (DateTime?)null : reader.GetDateTime(1);
                                string status = reader.GetString(2);

                                string startStr = startDateTime.ToString("dd.MM.yyyy HH:mm");
                                string endStr = endDateTime.HasValue ? endDateTime.Value.ToString("dd.MM.yyyy HH:mm") : "—";
                                string durationStr = "";
                                string statusDisplay = "";
                                string statusKey = status.ToLower();

                                // Определяем статус на русском и рассчитываем длительность
                                if (statusKey == "active" || statusKey == "активна")
                                {
                                    statusDisplay = "🟢 Активная смена";
                                    // Для активной смены считаем длительность от начала до текущего момента
                                    if (endDateTime == null)
                                    {
                                        TimeSpan currentDuration = DateTime.Now - startDateTime;
                                        durationStr = FormatDuration(currentDuration) + " (идет)";
                                    }
                                    else
                                    {
                                        durationStr = "—";
                                    }
                                }
                                else if (statusKey == "completed" || statusKey == "завершена")
                                {
                                    statusDisplay = "✅ Завершена";
                                    if (endDateTime.HasValue)
                                    {
                                        // Правильный расчет длительности для завершенной смены
                                        TimeSpan duration = endDateTime.Value - startDateTime;
                                        durationStr = FormatDuration(duration);
                                    }
                                    else
                                    {
                                        durationStr = "—";
                                    }
                                }
                                else if (statusKey == "cancelled" || statusKey == "отменена")
                                {
                                    statusDisplay = "❌ Отменена";
                                    durationStr = "—";
                                }
                                else
                                {
                                    statusDisplay = status;
                                    durationStr = "—";
                                }

                                dgvHistory.Rows.Add(startStr, endStr, durationStr, statusDisplay);

                                // Закрашиваем строку в зависимости от статуса
                                int rowIndex = dgvHistory.Rows.Count - 1;
                                if (statusKey == "active" || statusKey == "активна")
                                {
                                    dgvHistory.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(220, 255, 220);
                                    dgvHistory.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.DarkGreen;
                                }
                                else if (statusKey == "completed" || statusKey == "завершена")
                                {
                                    dgvHistory.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 220);
                                }
                                else if (statusKey == "cancelled" || statusKey == "отменена")
                                {
                                    dgvHistory.Rows[rowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                                }
                            }
                        }
                    }
                }

                if (dgvHistory.Rows.Count == 0)
                {
                    dgvHistory.Rows.Add("Нет данных о сменах", "", "", "");
                    dgvHistory.Rows[0].DefaultCellStyle.ForeColor = Color.Gray;
                    dgvHistory.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
                }

                // Настройка внешнего вида
                dgvHistory.ClearSelection();
                dgvHistory.RowsDefaultCellStyle.Font = new Font("Segoe UI", 10F);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки истории смен: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            DriverMenu driverMenu = new DriverMenu(accountId);
            back = true;

            driverMenu.Show();
            this.Close();
        }
    }
}