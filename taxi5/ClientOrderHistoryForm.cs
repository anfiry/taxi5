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

            // Настройка полноэкранного режима
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(1100, 640);

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

            // Растягиваем таблицу
            dataGridViewOrders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Скрываем служебные колонки
            if (dataGridViewOrders.Columns.Contains("order_id"))
                dataGridViewOrders.Columns["order_id"].Visible = false;
            if (dataGridViewOrders.Columns.Contains("has_review"))
                dataGridViewOrders.Columns["has_review"].Visible = false;

            // Настройка колонок с весами
            SetColumnFill("order_datetime", "Дата и время", 12);
            SetColumnFill("address_from", "Откуда", 15);
            SetColumnFill("address_to", "Куда", 15);
            SetColumnFill("tariff_name", "Тариф", 8);
            SetColumnFill("order_status", "Статус", 8);
            SetColumnFill("payment_method", "Оплата", 8);
            SetColumnFill("final_cost", "Стоимость", 8);
            SetColumnFill("driver_name", "Водитель", 10);
            SetColumnFill("promotion_name", "Акция", 8);
            SetColumnFill("promotion_percent", "Скидка, %", 6);
            SetColumnFill("promotion_amount", "Сумма скидки", 8);

            // Форматирование
            if (dataGridViewOrders.Columns["order_datetime"] != null)
                dataGridViewOrders.Columns["order_datetime"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
            if (dataGridViewOrders.Columns["final_cost"] != null)
                dataGridViewOrders.Columns["final_cost"].DefaultCellStyle.Format = "F2";
            if (dataGridViewOrders.Columns["promotion_amount"] != null)
                dataGridViewOrders.Columns["promotion_amount"].DefaultCellStyle.Format = "F2";
            if (dataGridViewOrders.Columns["promotion_percent"] != null)
                dataGridViewOrders.Columns["promotion_percent"].DefaultCellStyle.Format = "F0";

            // Добавляем кнопку "Отзыв"
            if (!dataGridViewOrders.Columns.Contains("ReviewButton"))
            {
                DataGridViewButtonColumn reviewButton = new DataGridViewButtonColumn();
                reviewButton.Name = "ReviewButton";
                reviewButton.HeaderText = "Отзыв";
                reviewButton.UseColumnTextForButtonValue = false;
                reviewButton.Width = 80;
                reviewButton.FillWeight = 8;
                dataGridViewOrders.Columns.Add(reviewButton);
            }

            // Добавляем кнопку "Маршрут"
            if (!dataGridViewOrders.Columns.Contains("RouteButton"))
            {
                DataGridViewButtonColumn routeButton = new DataGridViewButtonColumn();
                routeButton.Name = "RouteButton";
                routeButton.HeaderText = "Маршрут";
                routeButton.Text = "На карте";
                routeButton.UseColumnTextForButtonValue = true;
                routeButton.Width = 80;
                routeButton.FillWeight = 8;
                dataGridViewOrders.Columns.Add(routeButton);
            }

            // Стиль заголовков
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewOrders.ColumnHeadersHeight = 40;

            // Стиль строк
            dataGridViewOrders.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
            dataGridViewOrders.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 249);
            dataGridViewOrders.RowHeadersVisible = false;
            dataGridViewOrders.ReadOnly = true;
            dataGridViewOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewOrders.AllowUserToAddRows = false;
            dataGridViewOrders.AllowUserToDeleteRows = false;
        }

        private void SetColumnFill(string columnName, string headerText, int fillWeight)
        {
            if (dataGridViewOrders.Columns[columnName] != null)
            {
                dataGridViewOrders.Columns[columnName].HeaderText = headerText;
                dataGridViewOrders.Columns[columnName].FillWeight = fillWeight;
                dataGridViewOrders.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void DataGridViewOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewOrders.Columns[e.ColumnIndex].Name == "ReviewButton")
            {
                DataGridViewRow row = dataGridViewOrders.Rows[e.RowIndex];
                bool hasReview = Convert.ToBoolean(row.Cells["has_review"].Value);
                e.Value = hasReview ? "Просмотр" : "Написать";
                e.FormattingApplied = true;
            }

            // Форматирование пустых значений
            if (e.Value == null || e.Value == DBNull.Value)
            {
                e.Value = "—";
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
                string status = dataGridViewOrders.Rows[e.RowIndex].Cells["order_status"].Value.ToString();
                bool hasReview = Convert.ToBoolean(dataGridViewOrders.Rows[e.RowIndex].Cells["has_review"].Value);

                // Проверка: можно оставить отзыв только для завершенных заказов
                if (!hasReview && status != "Завершен")
                {
                    MessageBox.Show("Отзыв можно оставить только после завершения заказа.\nПожалуйста, дождитесь окончания поездки.",
                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

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
                                 (details["promotion_name"] != DBNull.Value && details["promotion_name"].ToString() != "—"
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