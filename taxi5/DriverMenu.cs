using Npgsql;
using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace taxi4
{
    public partial class DriverMenu : Form
    {
        public string Role { get; set; }
        public int AccountId { get; private set; }
        public string UserLogin { get; set; }

        private string connectionString = "Server=localhost;Port=5432;Database=taxi4;User Id=postgres;Password=123";
        private int currentDriverId = 0;
        private string driverName = "";
        private int? currentWorkScheduleId = null;
        private DateTime shiftStartTime;

        // Поля для перетаскивания формы (если FormBorderStyle = None)
        Point lastPoint;
        Point lastPoint1;

        public DriverMenu(int accountId)
        {
            InitializeComponent();
            AccountId = accountId;
            LoadDriverData();
            this.Load += DriverMenu_Load;
        }

        public DriverMenu()
        {
            InitializeComponent();
        }

        private void LoadDriverData()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT driver_id, first_name, last_name FROM driver WHERE account_id = @accountId";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@accountId", AccountId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentDriverId = reader.GetInt32(0);
                                string firstName = reader.GetString(1);
                                string lastName = reader.GetString(2);
                                driverName = $"{firstName} {lastName}".Trim();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных водителя: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DriverMenu_Load(object sender, EventArgs e)
        {
            if (currentDriverId == 0)
            {
                MessageBox.Show("Не удалось определить водителя для данного аккаунта.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            CheckActiveShiftOnStart();
            UpdateOrdersButtonState();

            labelWelcome.Text = $"Добро пожаловать, {driverName}!";
        }

        // ---------- Перемещение формы ----------
        private void DriverMenu_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        private void DriverMenu_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint1.X;
                this.Top += e.Y - lastPoint1.Y;
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint1 = new Point(e.X, e.Y);
        }

        // ---------- Кнопки ----------
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm авторизация = new LoginForm();
            авторизация.Show();
        }

        private void Orders_Click(object sender, EventArgs e)
        {
            if (!IsShiftActive())
            {
                MessageBox.Show("Сначала начните смену!", "Доступ запрещён",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentDriverId == 0)
            {
                MessageBox.Show("Не удалось определить ID водителя", "Ошибка");
                return;
            }

            DriverOrdersForm ordersForm = new DriverOrdersForm(currentDriverId, connectionString);
            ordersForm.Closed += (s, args) => this.Show();
            ordersForm.Show();
            this.Hide();
        }

        private void Statistics_Click(object sender, EventArgs e)
        {
            if (currentDriverId == 0)
            {
                MessageBox.Show("Не удалось определить ID водителя", "Ошибка");
                return;
            }

            DriverStatisticsRatingForm statsForm = new DriverStatisticsRatingForm(currentDriverId, connectionString);
            statsForm.Closed += (s, args) => this.Show();
            statsForm.Show();
            this.Hide();
        }

        private void Shifts_Click(object sender, EventArgs e)
        {
            if (currentDriverId == 0)
            {
                MessageBox.Show("Не удалось определить ID водителя", "Ошибка");
                return;
            }

            DriverShiftsForm shiftsForm = new DriverShiftsForm(currentDriverId, connectionString);
            shiftsForm.Closed += (s, args) => this.Show();
            shiftsForm.Show();
            this.Hide();
        }

        // ---------- Работа со сменами ----------
        private bool IsShiftActive()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT COUNT(*) FROM work_schedule
                    WHERE driver_id = @driver_id
                      AND end_datetime IS NULL;";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@driver_id", currentDriverId);
                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        private void CheckActiveShiftOnStart()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT work_schedule, start_datetime
                        FROM work_schedule
                        WHERE driver_id = @driver_id
                          AND end_datetime IS NULL
                        LIMIT 1;";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@driver_id", currentDriverId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentWorkScheduleId = reader.GetInt32(0);
                                shiftStartTime = reader.GetDateTime(1);

                                // Обновляем интерфейс
                                Startingshifts.Enabled = false;
                                Startingshifts.Text = "Смена активна";
                                Startingshifts.BackColor = Color.LightGreen;
                                Startingshifts.ForeColor = Color.DarkGreen;

                                EndShif.Enabled = true;
                                EndShif.BackColor = Color.LightCoral;
                                EndShif.ForeColor = Color.DarkRed;

                                Orders.Enabled = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке активной смены: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateOrdersButtonState()
        {
            Orders.Enabled = IsShiftActive();
        }

        // ========== НАЧАЛО СМЕНЫ ==========
        private void Startingshifts_Click(object sender, EventArgs e)
        {
            if (IsShiftActive())
            {
                MessageBox.Show("У вас уже есть активная смена! Завершите её, чтобы начать новую.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Начать смену?", "Начало смены",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string insertQuery = @"
                        INSERT INTO work_schedule 
                            (driver_id, shift_status_id, start_datetime)
                        VALUES 
                            (@driver_id, @status_id, @start_datetime)
                        RETURNING work_schedule;";

                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@driver_id", currentDriverId);
                            cmd.Parameters.AddWithValue("@status_id", 1);
                            cmd.Parameters.AddWithValue("@start_datetime", DateTime.Now);

                            currentWorkScheduleId = (int)cmd.ExecuteScalar();
                            shiftStartTime = DateTime.Now;
                        }
                    }

                    // Обновляем интерфейс
                    Startingshifts.Enabled = false;
                    Startingshifts.Text = "Смена активна";
                    Startingshifts.BackColor = Color.LightGreen;
                    Startingshifts.ForeColor = Color.DarkGreen;

                    EndShif.Enabled = true;
                    EndShif.BackColor = Color.LightCoral;
                    EndShif.ForeColor = Color.DarkRed;

                    Orders.Enabled = true;

                    MessageBox.Show($"Смена начата в {shiftStartTime:HH:mm}",
                        "Смена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при начале смены: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ========== ЗАВЕРШЕНИЕ СМЕНЫ ==========
        private void EndShif_Click(object sender, EventArgs e)
        {
            if (currentWorkScheduleId == null)
            {
                MessageBox.Show("Нет активной смены!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Завершить смену?", "Завершение смены",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string earningsInput = Interaction.InputBox(
                        "Сколько заработали за смену? (₽)", "Доход", "0");
                    decimal.TryParse(earningsInput, out decimal earnings);

                    string ordersInput = Interaction.InputBox(
                        "Сколько выполнили заказов?", "Заказы", "0");
                    int.TryParse(ordersInput, out int orders);

                    string updateQuery = @"
                        UPDATE work_schedule 
                        SET end_datetime = @end_datetime
                        WHERE work_schedule = @id;";

                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@end_datetime", DateTime.Now);
                            cmd.Parameters.AddWithValue("@id", currentWorkScheduleId.Value);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    TimeSpan duration = DateTime.Now - shiftStartTime;

                    // Возвращаем кнопки в исходное состояние
                    Startingshifts.Enabled = true;
                    Startingshifts.Text = "Начать смену";
                    Startingshifts.BackColor = SystemColors.Control;
                    Startingshifts.ForeColor = SystemColors.ControlText;

                    EndShif.Enabled = false;
                    EndShif.BackColor = SystemColors.Control;
                    EndShif.ForeColor = SystemColors.ControlText;

                    Orders.Enabled = false;

                    MessageBox.Show($"Смена завершена!\n\n" +
                                    $"Время работы: {duration:hh\\:mm}\n" +
                                    $"Заработано: {earnings} ₽\n" +
                                    $"Заказов: {orders}",
                        "Итоги смены", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    currentWorkScheduleId = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при завершении смены: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Пустые обработчики для дизайнера (если нужны)
        private void button1_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
    }
}