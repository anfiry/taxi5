using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace taxi4
{
    public partial class ShiftsForm : Form
    {
        private int driverId;
        private string connectionString;
        private DataGridView dgvHistory;

        // Конструктор с двумя параметрами
        public ShiftsForm(int driverId, string connString)
        {
            this.driverId = driverId;
            this.connectionString = connString;

            InitializeComponent();
            this.Text = "История смен";
            this.Size = new Size(800, 500);
            this.StartPosition = FormStartPosition.CenterParent;

            SetupDataGridView();
            LoadHistory();
        }

        private void SetupDataGridView()
        {
            dgvHistory = new DataGridView();
            dgvHistory.Dock = DockStyle.Fill;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.ReadOnly = true;
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.BackgroundColor = Color.White;
            dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // Колонки
            dgvHistory.Columns.Add("Date", "Дата");
            dgvHistory.Columns.Add("Start", "Начало");
            dgvHistory.Columns.Add("End", "Конец");
            dgvHistory.Columns.Add("Duration", "Длительность");

            dgvHistory.Columns["Date"].Width = 100;
            dgvHistory.Columns["Start"].Width = 80;
            dgvHistory.Columns["End"].Width = 80;
            dgvHistory.Columns["Duration"].Width = 100;

            this.Controls.Add(dgvHistory);
        }

        private void LoadHistory()
        {
            dgvHistory.Rows.Clear();

            string query = @"
                SELECT shift_date, start_time, end_time
                FROM work_schedule
                WHERE driver_id = @driver_id
                  AND end_time IS NOT NULL
                ORDER BY shift_date DESC, start_time DESC;";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@driver_id", driverId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime date = reader.GetDateTime(0);
                            TimeSpan start = reader.GetTimeSpan(1);
                            TimeSpan end = reader.GetTimeSpan(2);

                            DateTime startDateTime = date.Add(start);
                            DateTime endDateTime = date.Add(end);
                            TimeSpan duration = endDateTime - startDateTime;

                            dgvHistory.Rows.Add(
                                date.ToString("dd.MM.yyyy"),
                                start.ToString(@"hh\:mm"),
                                end.ToString(@"hh\:mm"),
                                duration.ToString(@"hh\:mm")
                            );
                        }
                    }
                }
            }

            if (dgvHistory.Rows.Count == 0)
            {
                dgvHistory.Rows.Add("Нет завершённых смен", "", "", "");
            }
        }
    }
}