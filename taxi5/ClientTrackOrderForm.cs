using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    public partial class ClientTrackOrderForm : Form
    {
        private ClientTrackData trackData;
        private int clientId;
        private Timer refreshTimer;
        private bool back = false;
        private int accountId;


        public ClientTrackOrderForm()
        {
            InitializeComponent();
        }

        public void OnClosed()
        {
            if (back)
            { back = false; }
            else { Application.Exit(); }
        }

        public ClientTrackOrderForm(int clientId, int accountId)
        {
            InitializeComponent();

            this.clientId = clientId;
            trackData = new ClientTrackData();

            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            LoadActiveOrders();
            ConfigureDataGridView();

            refreshTimer = new Timer();
            refreshTimer.Interval = 30000;
            refreshTimer.Tick += (s, e) => LoadActiveOrders();
            refreshTimer.Start();

            dataGridViewOrders.CellClick += DataGridViewOrders_CellClick;
            this.accountId = accountId;
        }

        private void LoadActiveOrders()
        {
            try
            {
                if (trackData == null) return;

                DataTable dt = trackData.GetActiveOrders(clientId);

                // Добавляем вычисляемую колонку для отображения времени начала
                if (!dt.Columns.Contains("start_trip_display"))
                    dt.Columns.Add("start_trip_display", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    if (row.Table.Columns.Contains("start_trip_time") && row["start_trip_time"] != DBNull.Value)
                    {
                        DateTime startTime = Convert.ToDateTime(row["start_trip_time"]);
                        row["start_trip_display"] = startTime.ToString("dd.MM.yyyy HH:mm");
                    }
                    else
                    {
                        row["start_trip_display"] = "—";
                    }
                }

                dataGridViewOrders.DataSource = dt;
                labelCount.Text = $"Активных заказов: {dt.Rows.Count}";

                // Обновляем отображение
                dataGridViewOrders.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}");
            }
        }

        private void ConfigureDataGridView()
        {
            if (dataGridViewOrders.Columns.Count == 0) return;

            // Отключаем автоматическое создание колонок
            dataGridViewOrders.AutoGenerateColumns = false;

            // Очищаем существующие колонки
            dataGridViewOrders.Columns.Clear();

            // Скрытая колонка для order_id
            DataGridViewTextBoxColumn colOrderId = new DataGridViewTextBoxColumn();
            colOrderId.Name = "order_id";
            colOrderId.DataPropertyName = "order_id";
            colOrderId.Visible = false;
            dataGridViewOrders.Columns.Add(colOrderId);

            // 1. Дата создания
            DataGridViewTextBoxColumn colOrderDateTime = new DataGridViewTextBoxColumn();
            colOrderDateTime.Name = "order_datetime";
            colOrderDateTime.HeaderText = "Дата создания";
            colOrderDateTime.DataPropertyName = "order_datetime";
            colOrderDateTime.DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
            colOrderDateTime.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colOrderDateTime.Width = 140;
            dataGridViewOrders.Columns.Add(colOrderDateTime);

            // 2. Начало поездки
            DataGridViewTextBoxColumn colStartTrip = new DataGridViewTextBoxColumn();
            colStartTrip.Name = "start_trip_display";
            colStartTrip.HeaderText = "Начало поездки";
            colStartTrip.DataPropertyName = "start_trip_display";
            colStartTrip.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colStartTrip.DefaultCellStyle.NullValue = "—";
            colStartTrip.Width = 140;
            dataGridViewOrders.Columns.Add(colStartTrip);

            // 3. Откуда
            DataGridViewTextBoxColumn colFrom = new DataGridViewTextBoxColumn();
            colFrom.Name = "address_from";
            colFrom.HeaderText = "Откуда";
            colFrom.DataPropertyName = "address_from";
            colFrom.Width = 350;
            dataGridViewOrders.Columns.Add(colFrom);

            // 4. Куда
            DataGridViewTextBoxColumn colTo = new DataGridViewTextBoxColumn();
            colTo.Name = "address_to";
            colTo.HeaderText = "Куда";
            colTo.DataPropertyName = "address_to";
            colTo.Width = 350;
            dataGridViewOrders.Columns.Add(colTo);

            // 5. Автомобиль (после Куда)
            DataGridViewTextBoxColumn colCar = new DataGridViewTextBoxColumn();
            colCar.Name = "car_info";
            colCar.HeaderText = "Автомобиль";
            colCar.DataPropertyName = "car_info";
            colCar.Width = 280;
            dataGridViewOrders.Columns.Add(colCar);

            // 6. Водитель
            DataGridViewTextBoxColumn colDriver = new DataGridViewTextBoxColumn();
            colDriver.Name = "driver_name";
            colDriver.HeaderText = "Водитель";
            colDriver.DataPropertyName = "driver_name";
            colDriver.Width = 180;
            dataGridViewOrders.Columns.Add(colDriver);

            // 7. Телефон
            DataGridViewTextBoxColumn colPhone = new DataGridViewTextBoxColumn();
            colPhone.Name = "driver_phone";
            colPhone.HeaderText = "Телефон";
            colPhone.DataPropertyName = "driver_phone";
            colPhone.Width = 130;
            dataGridViewOrders.Columns.Add(colPhone);

            // 8. Тариф
            DataGridViewTextBoxColumn colTariff = new DataGridViewTextBoxColumn();
            colTariff.Name = "tariff_name";
            colTariff.HeaderText = "Тариф";
            colTariff.DataPropertyName = "tariff_name";
            colTariff.Width = 120;
            dataGridViewOrders.Columns.Add(colTariff);

            // 9. Статус
            DataGridViewTextBoxColumn colStatus = new DataGridViewTextBoxColumn();
            colStatus.Name = "order_status";
            colStatus.HeaderText = "Статус";
            colStatus.DataPropertyName = "order_status";
            colStatus.Width = 120;
            dataGridViewOrders.Columns.Add(colStatus);

            // 10. Кнопка маршрута
            DataGridViewButtonColumn routeButton = new DataGridViewButtonColumn();
            routeButton.Name = "RouteButton";
            routeButton.HeaderText = "Маршрут";
            routeButton.Text = "На карте";
            routeButton.UseColumnTextForButtonValue = true;
            routeButton.Width = 80;
            dataGridViewOrders.Columns.Add(routeButton);

            // Стиль заголовков (ВАШИ ЦВЕТА)
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewOrders.ColumnHeadersHeight = 40;

            // Стиль строк (ВАШИ ЦВЕТА)
            dataGridViewOrders.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
            dataGridViewOrders.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 249);
            dataGridViewOrders.RowHeadersVisible = false;
            dataGridViewOrders.ReadOnly = true;
            dataGridViewOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewOrders.AllowUserToAddRows = false;
            dataGridViewOrders.AllowUserToDeleteRows = false;
            dataGridViewOrders.ScrollBars = ScrollBars.Both;
            dataGridViewOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }

        private void DataGridViewOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex < 0) return;

            if (dataGridViewOrders.Columns[e.ColumnIndex].Name == "RouteButton")
            {
                string from = dataGridViewOrders.Rows[e.RowIndex].Cells["address_from"].Value?.ToString() ?? "";
                string to = dataGridViewOrders.Rows[e.RowIndex].Cells["address_to"].Value?.ToString() ?? "";

                if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to))
                {
                    RouteViewForm routeForm = new RouteViewForm(from, to);
                    routeForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Не удалось определить адреса для построения маршрута.", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadActiveOrders();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            refreshTimer?.Stop();
            ClientMenu ClientMenu = new ClientMenu(accountId);
            ClientMenu.Show();
            back = true;
            this.Close();
        }

        private void dataGridViewOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int orderId = Convert.ToInt32(dataGridViewOrders.Rows[e.RowIndex].Cells["order_id"].Value);
                ShowOrderDetails(orderId);
            }
        }

        private void ShowOrderDetails(int orderId)
        {
            if (trackData == null) return;

            DataRow details = trackData.GetOrderDetails(orderId);
            if (details != null)
            {
                string message = $"Заказ №{details["order_id"]}\n" +
                                 $"Дата: {Convert.ToDateTime(details["order_datetime"]):dd.MM.yyyy HH:mm}\n" +
                                 $"Откуда: {details["address_from_full"]}\n" +
                                 $"Куда: {details["address_to_full"]}\n" +
                                 $"Тариф: {details["tariff_name"]}\n" +
                                 $"Статус: {details["status_name"]}\n";

                if (details["driver_full_name"] != DBNull.Value && !string.IsNullOrEmpty(details["driver_full_name"].ToString()))
                    message += $"Водитель: {details["driver_full_name"]}\nТелефон: {details["driver_phone"]}";
                else
                    message += "Водитель ещё не назначен";

                MessageBox.Show(message, "Детали заказа", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            refreshTimer?.Stop();
            base.OnFormClosing(e);
        }
    }
}