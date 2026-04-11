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
                if (!dt.Columns.Contains("start_trip_display"))
                    dt.Columns.Add("start_trip_display", typeof(string));
                if (!dt.Columns.Contains("end_trip_display"))
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

                // ПРИВЯЗЫВАЕМ ДАННЫЕ
                dataGridViewOrders.DataSource = dt;

                // ОТЛАДКА: проверяем, сколько строк загружено
                System.Diagnostics.Debug.WriteLine($"Загружено строк: {dt.Rows.Count}");
                System.Diagnostics.Debug.WriteLine($"Колонок в DataTable: {dt.Columns.Count}");

                // Проверяем наличие колонки order_status
                if (dt.Columns.Contains("order_status"))
                {
                    System.Diagnostics.Debug.WriteLine("Колонка order_status существует");
                    if (dt.Rows.Count > 0)
                        System.Diagnostics.Debug.WriteLine($"Первый статус: {dt.Rows[0]["order_status"]}");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Колонка order_status НЕ существует!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заказов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            // ОЧИЩАЕМ ВСЕ СУЩЕСТВУЮЩИЕ КОЛОНКИ
            dataGridViewOrders.Columns.Clear();
            dataGridViewOrders.AutoGenerateColumns = false;

            // ========== СКРЫТАЯ КОЛОНКА ДЛЯ has_review (нужна для отображения кнопок) ==========
            DataGridViewTextBoxColumn colHasReview = new DataGridViewTextBoxColumn();
            colHasReview.Name = "has_review";
            colHasReview.DataPropertyName = "has_review";
            colHasReview.Visible = false;  // Скрываем, она нужна только для логики
            dataGridViewOrders.Columns.Add(colHasReview);

            // ========== СКРЫТАЯ КОЛОНКА ДЛЯ order_id ==========
            DataGridViewTextBoxColumn colOrderId = new DataGridViewTextBoxColumn();
            colOrderId.Name = "order_id";
            colOrderId.DataPropertyName = "order_id";
            colOrderId.Visible = false;  // Скрываем
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

            // 3. Завершение поездки
            DataGridViewTextBoxColumn colEndTrip = new DataGridViewTextBoxColumn();
            colEndTrip.Name = "end_trip_display";
            colEndTrip.HeaderText = "Завершение поездки";
            colEndTrip.DataPropertyName = "end_trip_display";
            colEndTrip.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colEndTrip.DefaultCellStyle.NullValue = "—";
            colEndTrip.Width = 140;
            dataGridViewOrders.Columns.Add(colEndTrip);

            // 4. Водитель
            DataGridViewTextBoxColumn colDriver = new DataGridViewTextBoxColumn();
            colDriver.Name = "driver_name";
            colDriver.HeaderText = "Водитель";
            colDriver.DataPropertyName = "driver_name";
            colDriver.Width = 180;
            dataGridViewOrders.Columns.Add(colDriver);

            // 5. Автомобиль
            DataGridViewTextBoxColumn colCar = new DataGridViewTextBoxColumn();
            colCar.Name = "car_info";
            colCar.HeaderText = "Автомобиль";
            colCar.DataPropertyName = "car_info";
            colCar.Width = 280;
            dataGridViewOrders.Columns.Add(colCar);

            // 6. Откуда
            DataGridViewTextBoxColumn colFrom = new DataGridViewTextBoxColumn();
            colFrom.Name = "address_from";
            colFrom.HeaderText = "Откуда";
            colFrom.DataPropertyName = "address_from";
            colFrom.Width = 320;
            dataGridViewOrders.Columns.Add(colFrom);

            // 7. Куда
            DataGridViewTextBoxColumn colTo = new DataGridViewTextBoxColumn();
            colTo.Name = "address_to";
            colTo.HeaderText = "Куда";
            colTo.DataPropertyName = "address_to";
            colTo.Width = 320;
            dataGridViewOrders.Columns.Add(colTo);

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

            // 10. Оплата
            DataGridViewTextBoxColumn colPayment = new DataGridViewTextBoxColumn();
            colPayment.Name = "payment_method";
            colPayment.HeaderText = "Оплата";
            colPayment.DataPropertyName = "payment_method";
            colPayment.Width = 120;
            dataGridViewOrders.Columns.Add(colPayment);

            // 11. Стоимость
            DataGridViewTextBoxColumn colCost = new DataGridViewTextBoxColumn();
            colCost.Name = "final_cost";
            colCost.HeaderText = "Стоимость";
            colCost.DataPropertyName = "final_cost";
            colCost.DefaultCellStyle.Format = "N2";
            colCost.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colCost.Width = 120;
            dataGridViewOrders.Columns.Add(colCost);

            // 12. Акция
            DataGridViewTextBoxColumn colPromo = new DataGridViewTextBoxColumn();
            colPromo.Name = "promotion_name";
            colPromo.HeaderText = "Акция";
            colPromo.DataPropertyName = "promotion_name";
            colPromo.Width = 180;
            dataGridViewOrders.Columns.Add(colPromo);

            // 13. Скидка, %
            DataGridViewTextBoxColumn colPercent = new DataGridViewTextBoxColumn();
            colPercent.Name = "promotion_percent";
            colPercent.HeaderText = "Скидка, %";
            colPercent.DataPropertyName = "promotion_percent";
            colPercent.DefaultCellStyle.Format = "F0";
            colPercent.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colPercent.Width = 100;
            dataGridViewOrders.Columns.Add(colPercent);

            // 14. Сумма скидки
            DataGridViewTextBoxColumn colAmount = new DataGridViewTextBoxColumn();
            colAmount.Name = "promotion_amount";
            colAmount.HeaderText = "Сумма скидки";
            colAmount.DataPropertyName = "promotion_amount";
            colAmount.DefaultCellStyle.Format = "F2";
            colAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colAmount.Width = 130;
            dataGridViewOrders.Columns.Add(colAmount);

            // 15. Кнопка "Отзыв" (В КОНЦЕ)
            DataGridViewButtonColumn reviewButton = new DataGridViewButtonColumn();
            reviewButton.Name = "ReviewButton";
            reviewButton.HeaderText = "Отзыв";
            reviewButton.UseColumnTextForButtonValue = false;
            reviewButton.Width = 80;
            dataGridViewOrders.Columns.Add(reviewButton);

            // 16. Кнопка "Маршрут" (В КОНЦЕ)
            DataGridViewButtonColumn routeButton = new DataGridViewButtonColumn();
            routeButton.Name = "RouteButton";
            routeButton.HeaderText = "Маршрут";
            routeButton.Text = "На карте";
            routeButton.UseColumnTextForButtonValue = true;
            routeButton.Width = 80;
            dataGridViewOrders.Columns.Add(routeButton);

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
            dataGridViewOrders.ScrollBars = ScrollBars.Both;
            dataGridViewOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            // ПРИНУДИТЕЛЬНО ОБНОВЛЯЕМ
            dataGridViewOrders.Refresh();
        }

        private void SetColumnWidth(string columnName, string headerText, int width)
        {
            if (dataGridViewOrders.Columns[columnName] != null)
            {
                dataGridViewOrders.Columns[columnName].HeaderText = headerText;
                dataGridViewOrders.Columns[columnName].Width = width;
                dataGridViewOrders.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
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