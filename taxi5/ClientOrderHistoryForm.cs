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

        public ClientOrderHistoryForm()
        {
            InitializeComponent();
        }

        public ClientOrderHistoryForm(int clientId) : this()
        {
            this.clientId = clientId;

            if (!DesignMode)
            {
                historyData = new ClientOrderHistoryData();
                LoadOrders();
                ConfigureDataGridView();

                // Подписка на события
                dataGridViewOrders.CellClick += DataGridViewOrders_CellClick;
                dataGridViewOrders.CellFormatting += DataGridViewOrders_CellFormatting;
            }
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

            // Скрываем служебную колонку has_review
            if (dataGridViewOrders.Columns["has_review"] != null)
                dataGridViewOrders.Columns["has_review"].Visible = false;

            // Настройка основных колонок
            SetColumn("order_id", "№", 50, true);
            SetColumn("order_datetime", "Дата и время", 130, false);
            SetColumn("address_from", "Откуда", 200, false);
            SetColumn("address_to", "Куда", 200, false);
            SetColumn("tariff_name", "Тариф", 100, false);
            SetColumn("order_status", "Статус", 100, false);
            SetColumn("payment_method", "Оплата", 100, false);
            SetColumn("final_cost", "Стоимость", 80, false);
            SetColumn("driver_name", "Водитель", 150, false);
            SetColumn("promotion_name", "Акция", 150, false);
            SetColumn("promotion_percent", "Скидка, %", 70, false);
            SetColumn("promotion_amount", "Сумма скидки", 80, false);

            // Форматирование столбцов
            if (dataGridViewOrders.Columns["order_datetime"] != null)
                dataGridViewOrders.Columns["order_datetime"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
            if (dataGridViewOrders.Columns["final_cost"] != null)
                dataGridViewOrders.Columns["final_cost"].DefaultCellStyle.Format = "F2";
            if (dataGridViewOrders.Columns["promotion_amount"] != null)
                dataGridViewOrders.Columns["promotion_amount"].DefaultCellStyle.Format = "F2";

            // Порядок колонок
            int index = 0;
            string[] order = { "order_id", "order_datetime", "address_from", "address_to", "tariff_name",
                               "order_status", "payment_method", "final_cost", "driver_name",
                               "promotion_name", "promotion_percent", "promotion_amount" };
            foreach (string colName in order)
                if (dataGridViewOrders.Columns[colName] != null)
                    dataGridViewOrders.Columns[colName].DisplayIndex = index++;

            // Добавляем кнопку "Отзыв"
            DataGridViewButtonColumn reviewButton = new DataGridViewButtonColumn();
            reviewButton.Name = "ReviewButton";
            reviewButton.HeaderText = "Отзыв";
            reviewButton.UseColumnTextForButtonValue = false;
            reviewButton.Width = 100;
            dataGridViewOrders.Columns.Add(reviewButton);

            // Добавляем кнопку "Маршрут"
            DataGridViewButtonColumn routeButton = new DataGridViewButtonColumn();
            routeButton.Name = "RouteButton";
            routeButton.HeaderText = "Маршрут";
            routeButton.Text = "Показать на карте";
            routeButton.UseColumnTextForButtonValue = true; // всегда один текст
            routeButton.Width = 100;
            dataGridViewOrders.Columns.Add(routeButton);

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

        private void DataGridViewOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewOrders.Columns[e.ColumnIndex].Name == "ReviewButton")
            {
                DataGridViewRow row = dataGridViewOrders.Rows[e.RowIndex];
                bool hasReview = Convert.ToBoolean(row.Cells["has_review"].Value);
                e.Value = hasReview ? "Посмотреть отзыв" : "Оставить отзыв";
                e.FormattingApplied = true;
            }
        }

        private void DataGridViewOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string columnName = dataGridViewOrders.Columns[e.ColumnIndex].Name;

            if (columnName == "ReviewButton")
            {
                int orderId = Convert.ToInt32(dataGridViewOrders.Rows[e.RowIndex].Cells["order_id"].Value);
                bool hasReview = Convert.ToBoolean(dataGridViewOrders.Rows[e.RowIndex].Cells["has_review"].Value);

                if (hasReview)
                {
                    DataRow review = historyData.GetReview(orderId);
                    if (review != null)
                    {
                        MessageBox.Show($"Оценка: {review["rating"]}\nКомментарий: {review["comment"]}", "Отзыв",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить отзыв", "Ошибка");
                    }
                }
                else
                {
                    DataRow details = historyData.GetOrderDetails(orderId);
                    if (details != null)
                    {
                        int driverId = Convert.ToInt32(details["driver_id"]);
                        ClientReviewForm reviewForm = new ClientReviewForm(orderId, clientId, driverId);
                        if (reviewForm.ShowDialog() == DialogResult.OK)
                        {
                            LoadOrders(); // обновляем список
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не удалось загрузить данные заказа", "Ошибка");
                    }
                }
            }
            else if (columnName == "RouteButton")
            {
                string from = dataGridViewOrders.Rows[e.RowIndex].Cells["address_from"].Value.ToString();
                string to = dataGridViewOrders.Rows[e.RowIndex].Cells["address_to"].Value.ToString();
                RouteViewForm routeForm = new RouteViewForm(from, to);
                routeForm.ShowDialog();
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void dataGridViewOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Оставляем как есть для детального просмотра
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
                                 $"Водитель: {details["driver_full_name"]}\n" +
                                 (details["promotion_name"] != DBNull.Value
                                    ? $"Акция: {details["promotion_name"]} ({details["promotion_percent"]}%) – скидка {Convert.ToDecimal(details["promotion_amount"]):F2}"
                                    : "Акция: не применялась");
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