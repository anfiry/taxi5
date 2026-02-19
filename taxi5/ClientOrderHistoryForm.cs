using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    public partial class ClientOrderHistoryForm : Form
    {
        private ClientOrderHistoryData historyData;
        private int clientId;

        public ClientOrderHistoryForm(int clientId)
        {
            InitializeComponent();
            this.clientId = clientId;
            historyData = new ClientOrderHistoryData();
            LoadOrders();
            ConfigureDataGridView();
        }

        private void LoadOrders()
        {
            try
            {
                DataTable dt = historyData.GetClientOrders(clientId);
                dataGridViewOrders.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заказов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            if (dataGridViewOrders.Columns.Count == 0) return;

            dataGridViewOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            // Настройка видимости и заголовков
            SetColumn("order_id", "№", 50, true);
            SetColumn("order_datetime", "Дата и время", 130, false);
            SetColumn("address_from", "Откуда", 200, false);
            SetColumn("address_to", "Куда", 200, false);
            SetColumn("tariff_name", "Тариф", 100, false);
            SetColumn("order_status", "Статус", 100, false);
            SetColumn("payment_method", "Оплата", 100, false);
            SetColumn("final_cost", "Стоимость", 80, false);
            SetColumn("driver_name", "Водитель", 150, false);

            // Форматирование столбцов
            if (dataGridViewOrders.Columns["order_datetime"] != null)
                dataGridViewOrders.Columns["order_datetime"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
            if (dataGridViewOrders.Columns["final_cost"] != null)
                dataGridViewOrders.Columns["final_cost"].DefaultCellStyle.Format = "F2";

            // Порядок колонок
            int index = 0;
            string[] order = { "order_id", "order_datetime", "address_from", "address_to", "tariff_name",
                               "order_status", "payment_method", "final_cost", "driver_name" };
            foreach (string colName in order)
                if (dataGridViewOrders.Columns[colName] != null)
                    dataGridViewOrders.Columns[colName].DisplayIndex = index++;

            // Стиль заголовков
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

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close(); // Просто закрываем форму – ClientMenu покажется через событие Closed
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadOrders();
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
            DataRow details = historyData.GetOrderDetails(orderId);
            if (details != null)
            {
                string message = $"Заказ №{details["order_id"]}\n" +
                                 $"Дата: {details["order_datetime"]}\n" +
                                 $"Откуда: {details["address_from_full"]}\n" +
                                 $"Куда: {details["address_to_full"]}\n" +
                                 $"Тариф: {details["tariff_name"]}\n" +
                                 $"Статус: {details["status_name"]}\n" +
                                 $"Оплата: {details["payment_name"]}\n" +
                                 $"Стоимость: {Convert.ToDecimal(details["final_cost"]):F2}\n" +
                                 $"Водитель: {details["driver_full_name"]}";
                MessageBox.Show(message, "Детали заказа", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Не удалось загрузить детали заказа.", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}