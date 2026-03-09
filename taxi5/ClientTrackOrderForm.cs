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

        // Конструктор для дизайнера
        public ClientTrackOrderForm()
        {
            InitializeComponent();
        }

        // Основной конструктор
        public ClientTrackOrderForm(int clientId) : this()
        {
            this.clientId = clientId;

            if (!DesignMode)
            {
                trackData = new ClientTrackData();
                LoadActiveOrders();
                ConfigureDataGridView();

                // Настраиваем таймер для автоматического обновления каждые 30 секунд
                refreshTimer = new Timer();
                refreshTimer.Interval = 30000; // 30 секунд
                refreshTimer.Tick += (s, e) => LoadActiveOrders();
                refreshTimer.Start();
            }
        }

        private void LoadActiveOrders()
        {
            try
            {
                DataTable dt = trackData.GetActiveOrders(clientId);
                dataGridViewOrders.DataSource = dt;
                labelCount.Text = $"Активных заказов: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки активных заказов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            if (dataGridViewOrders.Columns.Count == 0) return;

            dataGridViewOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            SetColumn("order_id", "№", 50, true);
            SetColumn("order_datetime", "Дата и время", 130, false);
            SetColumn("address_from", "Откуда", 200, false);
            SetColumn("address_to", "Куда", 200, false);
            SetColumn("tariff_name", "Тариф", 100, false);
            SetColumn("order_status", "Статус", 100, false);
            SetColumn("driver_name", "Водитель", 150, false);
            SetColumn("driver_phone", "Телефон водителя", 120, false);

            // Форматирование даты
            if (dataGridViewOrders.Columns["order_datetime"] != null)
                dataGridViewOrders.Columns["order_datetime"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";

            // Порядок колонок
            int index = 0;
            string[] order = { "order_id", "order_datetime", "address_from", "address_to", "tariff_name",
                               "order_status", "driver_name", "driver_phone" };
            foreach (string colName in order)
                if (dataGridViewOrders.Columns[colName] != null)
                    dataGridViewOrders.Columns[colName].DisplayIndex = index++;

            // Стиль заголовков (как в других формах)
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewOrders.ColumnHeadersHeight = 40;

            // Стиль строк
            dataGridViewOrders.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
            dataGridViewOrders.DefaultCellStyle.ForeColor = Color.Black;
            dataGridViewOrders.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dataGridViewOrders.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridViewOrders.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 249);
            dataGridViewOrders.RowHeadersVisible = false;
            dataGridViewOrders.AllowUserToResizeRows = false;
            dataGridViewOrders.ReadOnly = true;
            dataGridViewOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void SetColumn(string columnName, string headerText, int width, bool frozen)
        {
            if (dataGridViewOrders.Columns[columnName] != null)
            {
                dataGridViewOrders.Columns[columnName].HeaderText = headerText;
                dataGridViewOrders.Columns[columnName].Width = width;
                dataGridViewOrders.Columns[columnName].Frozen = frozen;
                dataGridViewOrders.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
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
            DataRow details = trackData.GetOrderDetails(orderId);
            if (details != null)
            {
                string message = $"Заказ №{details["order_id"]}\n" +
                                 $"Дата: {details["order_datetime"]}\n" +
                                 $"Откуда: {details["address_from_full"]}\n" +
                                 $"Куда: {details["address_to_full"]}\n" +
                                 $"Тариф: {details["tariff_name"]}\n" +
                                 $"Статус: {details["status_name"]}\n" +
                                 (details["driver_full_name"] != DBNull.Value
                                    ? $"Водитель: {details["driver_full_name"]}\nТелефон: {details["driver_phone"]}"
                                    : "Водитель ещё не назначен");
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