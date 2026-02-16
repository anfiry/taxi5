using System;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;
using Microsoft.VisualBasic;

namespace taxi4
{
    public partial class меню_водителя : Form
    {
        // Строка подключения к БД
        private string connectionString = "Server=localhost;Port=5432;Database=taxi;User Id=postgres;Password=123";

        // ID текущего водителя
        private int currentDriverId = 1;

        // Данные активной смены
        private int? currentWorkScheduleId = null;
        private DateTime shiftStartTime;

        // Поля для перетаскивания формы
        Point lastPoint;
        Point lastPoint1;

        public меню_водителя()
        {
            InitializeComponent();

            // При загрузке проверяем активную смену и устанавливаем состояние кнопок
            this.Load += меню_водителя_Load;
        }

        private void меню_водителя_Load(object sender, EventArgs e)
        {
            CheckActiveShiftOnStart();
            // [!] Устанавливаем начальное состояние кнопки "Заказы"
            UpdateOrdersButtonState();
        }

        // ---------- ВАШИ МЕТОДЫ ----------
        private void button1_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }

        // Перемещение формы
        private void меню_водителя_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        private void меню_водителя_MouseDown(object sender, MouseEventArgs e)
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

        // Выход / смена пользователя
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm авторизация = new LoginForm();
            авторизация.Show();
        }

        // Переход к заказам
        private void Orders_Click(object sender, EventArgs e)
        {
            // [!] Проверка: если смена не активна – не открывать (на всякий случай, но кнопка должна быть неактивна)
            if (!IsShiftActive())
            {
                MessageBox.Show("Сначала начните смену!", "Доступ запрещён",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Orders ordersForm = new Orders(currentDriverId, connectionString);
            ordersForm.ShowDialog();
        }

        // Статистика и рейтинг
        private void Statistics_Click(object sender, EventArgs e)
        {
            StatisticsRatingForm statsForm = new StatisticsRatingForm(currentDriverId, connectionString);
            statsForm.ShowDialog();
        }

        // История смен
        private void Shifts_Click(object sender, EventArgs e)
        {
            ShiftsForm shiftForm = new ShiftsForm(currentDriverId, connectionString);
            shiftForm.ShowDialog();
        }

        // ---------- РАБОТА СО СМЕНАМИ ----------

        private bool IsShiftActive()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT COUNT(*) FROM work_schedule
                    WHERE driver_id = @driver_id
                      AND end_time IS NULL;";
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
                        SELECT work_schedule, start_time, shift_date
                        FROM work_schedule
                        WHERE driver_id = @driver_id
                          AND end_time IS NULL
                        LIMIT 1;";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@driver_id", currentDriverId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentWorkScheduleId = reader.GetInt32(0);
                                TimeSpan startTime = reader.GetTimeSpan(1);
                                DateTime shiftDate = reader.GetDateTime(2);
                                shiftStartTime = shiftDate.Add(startTime);

                                // Обновляем интерфейс
                                Startingshifts.Enabled = false;
                                Startingshifts.Text = "Смена активна";
                                Startingshifts.BackColor = Color.LightGreen;
                                Startingshifts.ForeColor = Color.DarkGreen;

                                EndShif.Enabled = true;
                                EndShif.BackColor = Color.LightCoral;
                                EndShif.ForeColor = Color.DarkRed;

                                // [!] Активируем кнопку "Заказы"
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

        // [!] Метод для обновления состояния кнопки "Заказы" по состоянию смены
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
                            (driver_id, shift_status_id, shift_date, start_time)
                        VALUES 
                            (@driver_id, @status_id, @date, @start_time)
                        RETURNING work_schedule;";

                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@driver_id", currentDriverId);
                            cmd.Parameters.AddWithValue("@status_id", 1);
                            cmd.Parameters.AddWithValue("@date", DateTime.Today);
                            cmd.Parameters.AddWithValue("@start_time", DateTime.Now.TimeOfDay);

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

                    // [!] Активируем кнопку "Заказы"
                    Orders.Enabled = true;

                    MessageBox.Show($"Смена начата в {shiftStartTime:HH:mm}",
                        "Смена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (PostgresException pgEx) when (pgEx.SqlState == "23502")
                {
                    MessageBox.Show("Ошибка базы данных: столбец 'end_time' не должен быть пустым.\n\n" +
                                    "Выполните в pgAdmin команду:\n" +
                                    "ALTER TABLE work_schedule ALTER COLUMN end_time DROP NOT NULL;",
                                    "Ошибка БД", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        SET end_time = @end_time
                        WHERE work_schedule = @id;";

                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();
                        using (var cmd = new NpgsqlCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@end_time", DateTime.Now.TimeOfDay);
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

                    // [!] Деактивируем кнопку "Заказы"
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
    }
}