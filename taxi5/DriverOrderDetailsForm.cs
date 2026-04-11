using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace taxi4
{
    public partial class DriverOrderDetailsForm : Form
    {
        private int orderId;
        private string fromAddress;
        private string toAddress;
        private string price;
        private string orderStatus;
        private int driverId;
        private int accountId;
        private string connectionString;
        private DateTime orderDateTime;
        private DateTime? acceptTime;
        private Timer cancelTimer;
        private bool back = false;


        public DriverOrderDetailsForm(int orderId, string fromAddress, string toAddress, string price, string orderStatus, int driverId, string connectionString, int accountId)
        {
            InitializeComponent();
            this.orderId = orderId;
            this.fromAddress = fromAddress;
            this.toAddress = toAddress;
            this.price = price;
            this.orderStatus = orderStatus;
            this.driverId = driverId;
            this.accountId = accountId;
            this.connectionString = connectionString;

            // Настройка полноэкранного режима
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(1200, 672);

            LoadOrderDetails();
            LoadClientInfo();      
            SetupUI();
        }

        public void OnClosed()
        {
            if (back)
            { back = false; }
            else { Application.Exit(); }
        }

        private void SetupUI()
        {
            if (orderStatus == "1") // Создан
            {
                btnAction.Visible = true;
                btnAction.Text = "ПРИНЯТЬ ЗАКАЗ";
                btnAction.BackColor = Color.FromArgb(46, 204, 113);
                btnCancel.Visible = false;
                lblAcceptTime.Visible = false;
            }
            else if (orderStatus == "2") // В процессе
            {
                // Получаем время принятия заказа из БД
                LoadAcceptTime();

                // Проверяем, прошло ли 2 минуты после принятия
                bool isCancelable = false;

                if (acceptTime.HasValue)
                {
                    TimeSpan elapsed = DateTime.Now - acceptTime.Value;
                    isCancelable = elapsed.TotalMinutes < 2;
                }

                if (isCancelable)
                {
                    // Кнопка отмены видна только в первые 2 минуты
                    btnAction.Visible = true;
                    btnAction.Text = "Завершить";
                    btnAction.BackColor = Color.FromArgb(52, 152, 219);

                    btnCancel.Visible = true;
                    btnCancel.Text = "Отменить";
                    btnCancel.BackColor = Color.FromArgb(231, 76, 60);
                    btnCancel.Enabled = true;

                    // Показываем время принятия
                    lblAcceptTime.Text = $"Принят: {acceptTime.Value:dd.MM.yyyy HH:mm:ss}";
                    lblAcceptTime.Visible = true;

                    // Запускаем таймер для обновления оставшегося времени
                    cancelTimer = new Timer();
                    cancelTimer.Interval = 1000;
                    cancelTimer.Tick += (s, e) =>
                    {
                        if (acceptTime.HasValue)
                        {
                            TimeSpan elapsed = DateTime.Now - acceptTime.Value;
                            int remainingSeconds = 120 - (int)elapsed.TotalSeconds;

                            if (remainingSeconds <= 0)
                            {
                                // Время истекло - скрываем кнопку отмены
                                btnCancel.Visible = false;
                                btnCancel.Enabled = false;
                                lblAcceptTime.Visible = false;
                                cancelTimer.Stop();
                            }
                            else
                            {
                                btnCancel.Text = $"Отменить";
                            }
                        }
                    };
                    cancelTimer.Start();
                }
                else
                {
                    // Время отмены истекло - показываем только кнопку завершения
                    btnAction.Visible = true;
                    btnAction.Text = "Завершить";
                    btnAction.BackColor = Color.FromArgb(52, 152, 219);
                    btnCancel.Visible = false;

                    if (acceptTime.HasValue)
                    {
                        lblAcceptTime.Text = $"Принят: {acceptTime.Value:dd.MM.yyyy HH:mm:ss}";
                        lblAcceptTime.Visible = true;
                        lblAcceptTime.ForeColor = Color.Orange;
                    }
                    else
                    {
                        lblAcceptTime.Visible = false;
                    }
                }
            }
            else if (orderStatus == "3") // Завершен
            {
                btnAction.Visible = false;
                btnCancel.Visible = false;

                if (acceptTime.HasValue)
                {
                    lblAcceptTime.Text = $"Принят: {acceptTime.Value:dd.MM.yyyy HH:mm:ss}";
                    lblAcceptTime.Visible = true;
                }
                else
                {
                    lblAcceptTime.Visible = false;
                }
            }
            else if (orderStatus == "4") // Отменен
            {
                btnAction.Visible = false;
                btnCancel.Visible = false;
                lblStatus.Text = "Статус: Отменен";
                lblStatus.ForeColor = Color.Red;
                lblAcceptTime.Visible = false;
            }
        }

        private void LoadAcceptTime()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT start_trip_time
                        FROM ""Order""
                        WHERE order_id = @orderId";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        var result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            acceptTime = Convert.ToDateTime(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки времени принятия: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadOrderDetails()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT o.order_datetime, os.name as status_name, o.start_trip_time
                FROM ""Order"" o
                JOIN order_status os ON o.order_status = os.order_status_id
                WHERE o.order_id = @orderId";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                orderDateTime = reader.GetDateTime(0);
                                string statusName = reader.GetString(1);
                                DateTime? startTripTime = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2);

                                lblOrderId.Text = $"Заказ #{orderId}";
                                lblFrom.Text = $"📍 Откуда: {fromAddress}";
                                lblTo.Text = $"📍 Куда: {toAddress}";
                                lblPrice.Text = $"💰 {price} ₽";
                                lblTime.Text = $"🕐 Создан: {orderDateTime:dd.MM.yyyy HH:mm}";
                                lblStatus.Text = $"Статус: {statusName}";

                                // Отображение времени принятия
                                if (startTripTime.HasValue)
                                {
                                    lblAcceptTime.Text = $"⏱ Принят: {startTripTime.Value:dd.MM.yyyy HH:mm}";
                                    lblAcceptTime.Visible = true;
                                }
                                else
                                {
                                    lblAcceptTime.Visible = false;
                                }

                                // Цвет статуса
                                if (statusName == "Создан")
                                    lblStatus.ForeColor = Color.Orange;
                                else if (statusName == "В процессе")
                                    lblStatus.ForeColor = Color.Blue;
                                else if (statusName == "Завершен")
                                    lblStatus.ForeColor = Color.Green;
                                else if (statusName == "Отменен")
                                    lblStatus.ForeColor = Color.Red;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки деталей заказа: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadClientInfo()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                SELECT 
                    c.last_name || ' ' || c.first_name AS client_name,
                    c.phone_number
                FROM ""Order"" o
                JOIN client c ON o.client_id = c.client_id
                WHERE o.order_id = @orderId";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string clientName = reader.GetString(0);
                                string clientPhone = reader.GetString(1);

                                lblClientName.Text = $"👤 Клиент: {clientName}";
                                lblClientPhone.Text = $"📞 Телефон: {clientPhone}";
                            }
                            else
                            {
                                lblClientName.Text = "👤 Клиент: Не указан";
                                lblClientPhone.Text = "📞 Телефон: Не указан";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных клиента: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblClientName.Text = "👤 Клиент: Ошибка загрузки";
                lblClientPhone.Text = "📞 Телефон: Ошибка загрузки";
            }
        }

        

        // ---------- ЕДИНАЯ КНОПКА ДЕЙСТВИЯ ----------
        private void btnAction_Click(object sender, EventArgs e)
        {
            if (orderStatus == "1") // Создан -> Принять
            {
                DialogResult result = MessageBox.Show(
                    $"Вы уверены, что хотите принять заказ #{orderId}?",
                    "Подтверждение заказа",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    AcceptOrder();
                }
            }
            else if (orderStatus == "2") // В процессе -> Завершить
            {
                DialogResult result = MessageBox.Show(
                    $"Завершить заказ #{orderId}?",
                    "Завершение заказа",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    CompleteOrder();
                }
            }
        }

        private void AcceptOrder()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    // Проверяем, что заказ еще не занят
                    string checkQuery = @"
                        SELECT COUNT(*) 
                        FROM ""Order"" 
                        WHERE order_id = @orderId 
                        AND driver_id IS NULL 
                        AND order_status = 1";

                    using (var checkCmd = new NpgsqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@orderId", orderId);
                        long count = (long)checkCmd.ExecuteScalar();

                        if (count == 0)
                        {
                            MessageBox.Show("Заказ уже занят другим водителем!", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.DialogResult = DialogResult.Cancel;
                            DriverOrdersForm driverOrdersForm = new DriverOrdersForm(this.driverId, this.connectionString, this.accountId);
                            back = true;

                            driverOrdersForm.Show();
                            this.Close();
                            return;
                        }
                    }

                    // Обновляем заказ: назначаем водителя, меняем статус на 2 (В процессе)
                    string updateQuery = @"
                        UPDATE ""Order"" 
                        SET driver_id = @driverId, 
                            order_status = 2, 
                            start_trip_time = @startTime 
                        WHERE order_id = @orderId 
                        AND driver_id IS NULL 
                        AND order_status = 1
                        RETURNING order_id";

                    using (var cmd = new NpgsqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        cmd.Parameters.AddWithValue("@driverId", driverId);
                        cmd.Parameters.AddWithValue("@startTime", DateTime.Now);

                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            acceptTime = DateTime.Now;
                            MessageBox.Show("Заказ принят! Статус изменен на 'В процессе'\n\n" +
                                           "Внимание: у вас есть 2 минуты, чтобы отменить заказ!",
                                "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            DriverOrdersForm driverOrdersForm = new DriverOrdersForm(this.driverId, this.connectionString, this.accountId);
                            back = true;

                            driverOrdersForm.Show();
                            this.Close();

                        }
                        else
                        {
                            MessageBox.Show("Не удалось принять заказ. Возможно, его уже кто-то взял.", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при принятии заказа: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- ОТМЕНА ЗАКАЗА ----------
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (orderStatus != "2")
            {
                MessageBox.Show("Отменить можно только заказ в статусе 'В процессе'", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверяем, не прошло ли 2 минуты после принятия
            if (acceptTime.HasValue)
            {
                TimeSpan elapsed = DateTime.Now - acceptTime.Value;
                if (elapsed.TotalMinutes >= 2)
                {
                    MessageBox.Show("Время отмены заказа истекло (2 минуты после принятия)", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                // Если acceptTime не загружен, загружаем из БД
                LoadAcceptTime();
                if (acceptTime.HasValue)
                {
                    TimeSpan elapsed = DateTime.Now - acceptTime.Value;
                    if (elapsed.TotalMinutes >= 2)
                    {
                        MessageBox.Show("Время отмены заказа истекло (2 минуты после принятия)", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            DialogResult result = MessageBox.Show(
                $"Отменить заказ #{orderId}?",
                "Отмена заказа",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                CancelOrder();
            }
        }

        private void CancelOrder()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        UPDATE ""Order"" 
                        SET order_status = 4,
                            driver_id = NULL
                        WHERE order_id = @orderId 
                        AND driver_id = @driverId
                        AND order_status = 2";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        cmd.Parameters.AddWithValue("@driverId", driverId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Заказ отменен", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            DriverOrdersForm driverOrdersForm = new DriverOrdersForm(this.driverId, this.connectionString, this.accountId);
                            back = true;

                            driverOrdersForm.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось отменить заказ.", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отмене заказа: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CompleteOrder()
        {
            try
            {
                // Используем существующую стоимость из заказа
                decimal finalCost = decimal.Parse(price);

                DialogResult result = MessageBox.Show(
                    $"Завершить заказ #{orderId}?\n\n" +
                    $"Стоимость: {finalCost:N2} ₽",
                    "Завершение заказа",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;

                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                UPDATE ""Order"" 
                SET order_status = 3, 
                    end_trip_time = @endTime, 
                    final_cost = @cost 
                WHERE order_id = @orderId 
                AND order_status = 2 
                AND driver_id = @driverId";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        cmd.Parameters.AddWithValue("@driverId", driverId);
                        cmd.Parameters.AddWithValue("@endTime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@cost", finalCost);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            // Обновляем статистику водителя
                            DriverMenu.AddToShiftTotal(finalCost);

                            MessageBox.Show($"Заказ завершен!\nСтоимость: {finalCost:N2} ₽", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            DriverOrdersForm driverOrdersForm = new DriverOrdersForm(this.driverId, this.connectionString, this.accountId);
                            back = true;

                            driverOrdersForm.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось завершить заказ.", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при завершении заказа: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- ОТКРЫТИЕ КАРТЫ В БРАУЗЕРЕ ----------
        private void btnOpenInYandex_Click(object sender, EventArgs e)
        {
            try
            {
                string encodedStart = Uri.EscapeDataString(fromAddress);
                string encodedEnd = Uri.EscapeDataString(toAddress);
                string url = $"https://yandex.ru/maps/?rtext={encodedStart}~{encodedEnd}&rtt=auto";
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть Яндекс.Карты: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            cancelTimer?.Stop();
            DriverOrdersForm driverOrdersForm = new DriverOrdersForm(this.driverId, this.connectionString, this.accountId);
            back = true;

            driverOrdersForm.Show();
            this.Close();
        }
    }
}