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
        private int accountId;
        private bool back = false;


        public ClientOrderHistoryForm()
        {
            InitializeComponent();
        }

        public void OnClosed()
        {
            if (back)
            { back = false; }
            else { Application.Exit(); }
        }

        public ClientOrderHistoryForm(int clientId, int accountId) : this()
        {
            this.clientId = clientId;
            this.accountId = accountId;

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

                // Добавляем вычисляемые колонки для отображения дат начала и завершения
                dt.Columns.Add("start_trip_display", typeof(string));
                dt.Columns.Add("end_trip_display", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    // Обработка даты начала поездки
                    if (row.Table.Columns.Contains("start_trip_time") && row["start_trip_time"] != DBNull.Value)
                    {
                        DateTime startTime = Convert.ToDateTime(row["start_trip_time"]);
                        row["start_trip_display"] = startTime.ToString("dd.MM.yyyy HH:mm");
                    }
                    else
                    {
                        row["start_trip_display"] = "—";
                    }

                    // Обработка даты завершения поездки
                    if (row.Table.Columns.Contains("end_trip_time") && row["end_trip_time"] != DBNull.Value)
                    {
                        DateTime endTime = Convert.ToDateTime(row["end_trip_time"]);
                        row["end_trip_display"] = endTime.ToString("dd.MM.yyyy HH:mm");
                    }
                    else
                    {
                        row["end_trip_display"] = "—";
                    }
                }

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
            if (dataGridViewOrders.Columns.Contains("start_trip_time"))
                dataGridViewOrders.Columns["start_trip_time"].Visible = false;
            if (dataGridViewOrders.Columns.Contains("end_trip_time"))
                dataGridViewOrders.Columns["end_trip_time"].Visible = false;

            // Настройка колонок с весами (ТРИ колонки для дат)
            SetColumnFill("order_datetime", "Дата создания", 10);
            SetColumnFill("start_trip_display", "Начало поездки", 10);
            SetColumnFill("end_trip_display", "Завершение поездки", 10);
            SetColumnFill("address_from", "Откуда", 12);
            SetColumnFill("address_to", "Куда", 12);
            SetColumnFill("tariff_name", "Тариф", 6);
            SetColumnFill("order_status", "Статус", 6);
            SetColumnFill("payment_method", "Оплата", 6);
            SetColumnFill("final_cost", "Стоимость", 8);
            SetColumnFill("driver_name", "Водитель", 10);
            SetColumnFill("promotion_name", "Акция", 8);
            SetColumnFill("promotion_percent", "Скидка, %", 5);
            SetColumnFill("promotion_amount", "Сумма скидки", 7);

            // Форматирование дат и чисел
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
                reviewButton.HeaderText = "⭐ Отзыв";
                reviewButton.UseColumnTextForButtonValue = false;
                reviewButton.Width = 70;
                reviewButton.FillWeight = 6;
                dataGridViewOrders.Columns.Add(reviewButton);
            }

            // Добавляем кнопку "Маршрут"
            if (!dataGridViewOrders.Columns.Contains("RouteButton"))
            {
                DataGridViewButtonColumn routeButton = new DataGridViewButtonColumn();
                routeButton.Name = "RouteButton";
                routeButton.HeaderText = "🗺️ Маршрут";
                routeButton.Text = "На карте";
                routeButton.UseColumnTextForButtonValue = true;
                routeButton.Width = 70;
                routeButton.FillWeight = 6;
                dataGridViewOrders.Columns.Add(routeButton);
            }

            // Стиль заголовков
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewOrders.ColumnHeadersHeight = 45;

            // Стиль строк
            dataGridViewOrders.DefaultCellStyle.Font = new Font("Segoe UI", 9);
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
                string statusText = dataGridViewOrders.Rows[e.RowIndex].Cells["order_status"].Value.ToString();
                bool hasReview = Convert.ToBoolean(dataGridViewOrders.Rows[e.RowIndex].Cells["has_review"].Value);

                // Пытаемся определить статус как число или как текст
                bool isCompleted = false;

                // Если статус - число
                if (int.TryParse(statusText, out int statusId))
                {
                    isCompleted = (statusId == 3); // 3 = Завершен
                }
                else
                {
                    // Если статус - текст
                    isCompleted = statusText.Equals("Завершен", StringComparison.OrdinalIgnoreCase) ||
                                  statusText.Equals("Завершён", StringComparison.OrdinalIgnoreCase);
                }

                // Проверка: можно оставить отзыв только для завершенных заказов
                if (!hasReview && !isCompleted)
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
            ClientMenu ClientMenu = new ClientMenu(accountId);
            back = true;

            ClientMenu.Show();
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