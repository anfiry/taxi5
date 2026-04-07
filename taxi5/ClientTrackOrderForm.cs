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

        public ClientTrackOrderForm()
        {
            InitializeComponent();
        }

        public ClientTrackOrderForm(int clientId)
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
        }

        private void LoadActiveOrders()
        {
            try
            {
                if (trackData == null) return;

                DataTable dt = trackData.GetActiveOrders(clientId);
                dataGridViewOrders.DataSource = dt;
                labelCount.Text = $"Активных заказов: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}");
            }
        }

        private void ConfigureDataGridView()
        {
            if (dataGridViewOrders.Columns.Count == 0) return;

            // Скрываем служебные колонки
            if (dataGridViewOrders.Columns.Contains("address_from_full"))
                dataGridViewOrders.Columns["address_from_full"].Visible = false;
            if (dataGridViewOrders.Columns.Contains("address_to_full"))
                dataGridViewOrders.Columns["address_to_full"].Visible = false;
            if (dataGridViewOrders.Columns.Contains("order_id"))
                dataGridViewOrders.Columns["order_id"].Visible = false;

            // Настройка заголовков
            if (dataGridViewOrders.Columns.Contains("order_id"))
                dataGridViewOrders.Columns["order_id"].HeaderText = "№";
            if (dataGridViewOrders.Columns.Contains("order_datetime"))
                dataGridViewOrders.Columns["order_datetime"].HeaderText = "Дата и время";
            if (dataGridViewOrders.Columns.Contains("address_from"))
                dataGridViewOrders.Columns["address_from"].HeaderText = "Откуда";
            if (dataGridViewOrders.Columns.Contains("address_to"))
                dataGridViewOrders.Columns["address_to"].HeaderText = "Куда";
            if (dataGridViewOrders.Columns.Contains("tariff_name"))
                dataGridViewOrders.Columns["tariff_name"].HeaderText = "Тариф";
            if (dataGridViewOrders.Columns.Contains("order_status"))
                dataGridViewOrders.Columns["order_status"].HeaderText = "Статус";
            if (dataGridViewOrders.Columns.Contains("driver_name"))
                dataGridViewOrders.Columns["driver_name"].HeaderText = "Водитель";
            if (dataGridViewOrders.Columns.Contains("driver_phone"))
                dataGridViewOrders.Columns["driver_phone"].HeaderText = "Телефон";

            // Форматирование даты
            if (dataGridViewOrders.Columns.Contains("order_datetime"))
                dataGridViewOrders.Columns["order_datetime"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";

            // Добавляем кнопку маршрута
            if (!dataGridViewOrders.Columns.Contains("RouteButton"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.Name = "RouteButton";
                btn.HeaderText = "";
                btn.Text = "🗺️ Маршрут";
                btn.UseColumnTextForButtonValue = true;
                dataGridViewOrders.Columns.Add(btn);
            }

            // Настройка внешнего вида
            dataGridViewOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewOrders.ReadOnly = true;
            dataGridViewOrders.RowHeadersVisible = false;
            dataGridViewOrders.AllowUserToAddRows = false;
            dataGridViewOrders.AllowUserToDeleteRows = false;

            dataGridViewOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewOrders.ColumnHeadersHeight = 40;

            dataGridViewOrders.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
            dataGridViewOrders.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 249);
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