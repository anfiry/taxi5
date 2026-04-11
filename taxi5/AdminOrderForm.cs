using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    public partial class AdminOrderForm : Form
    {
        private AdminOrder adminOrder;
        private DataTable ordersData;
        private bool back = false;

        public AdminOrderForm()
        {
            InitializeComponent();
            adminOrder = new AdminOrder();
            LoadOrderStatuses();
            LoadOrders();
            ConfigureDataGridView();

            this.dataGridViewOrders.Refresh();

            dataGridViewOrders.CellClick += DataGridViewOrders_CellClick;
        }

        public void OnClosed()
        {
            if (back)
            { back = false; }
            else { Application.Exit(); }
        }

        // ---------- ЗАГРУЗКА СТАТУСОВ ДЛЯ ФИЛЬТРА ----------
        private void LoadOrderStatuses()
        {
            try
            {
                DataTable statuses = adminOrder.GetOrderStatuses();

                DataRow allRow = statuses.NewRow();
                allRow["order_status_id"] = 0;
                allRow["name"] = "Все статусы";
                statuses.Rows.InsertAt(allRow, 0);

                comboBoxStatusFilter.DisplayMember = "name";
                comboBoxStatusFilter.ValueMember = "order_status_id";
                comboBoxStatusFilter.DataSource = statuses;
                comboBoxStatusFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки статусов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- ЗАГРУЗКА ЗАКАЗОВ ----------
        private void LoadOrders()
        {
            try
            {
                int? statusIdFilter = null;
                if (comboBoxStatusFilter.SelectedIndex > 0)
                {
                    statusIdFilter = Convert.ToInt32(comboBoxStatusFilter.SelectedValue);
                }

                ordersData = adminOrder.GetOrders(statusIdFilter);
                dataGridViewOrders.DataSource = ordersData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заказов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- НАСТРОЙКА ТАБЛИЦЫ ----------
        private void ConfigureDataGridView()
        {
            if (dataGridViewOrders.Columns.Count == 0) return;

            // Удаляем старые колонки кнопок, если они есть
            if (dataGridViewOrders.Columns.Contains("ReviewButton"))
                dataGridViewOrders.Columns.Remove("ReviewButton");
            if (dataGridViewOrders.Columns.Contains("RouteButton"))
                dataGridViewOrders.Columns.Remove("RouteButton");

            // Скрываем служебные колонки
            if (dataGridViewOrders.Columns.Contains("order_id"))
                dataGridViewOrders.Columns["order_id"].Visible = false;
            if (dataGridViewOrders.Columns.Contains("order_status"))
                dataGridViewOrders.Columns["order_status"].Visible = false;
            if (dataGridViewOrders.Columns.Contains("driver_id"))
                dataGridViewOrders.Columns["driver_id"].Visible = false;

            // НАСТРОЙКА КОЛОНОК С ДАТАМИ
            if (dataGridViewOrders.Columns.Contains("order_datetime"))
            {
                dataGridViewOrders.Columns["order_datetime"].HeaderText = "Дата создания";
                dataGridViewOrders.Columns["order_datetime"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                dataGridViewOrders.Columns["order_datetime"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewOrders.Columns["order_datetime"].Width = 130;
            }

            if (dataGridViewOrders.Columns.Contains("start_trip_time"))
            {
                dataGridViewOrders.Columns["start_trip_time"].HeaderText = "Начало поездки";
                dataGridViewOrders.Columns["start_trip_time"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                dataGridViewOrders.Columns["start_trip_time"].DefaultCellStyle.NullValue = "—";
                dataGridViewOrders.Columns["start_trip_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewOrders.Columns["start_trip_time"].Width = 130;
            }

            if (dataGridViewOrders.Columns.Contains("end_trip_time"))
            {
                dataGridViewOrders.Columns["end_trip_time"].HeaderText = "Завершение поездки";
                dataGridViewOrders.Columns["end_trip_time"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                dataGridViewOrders.Columns["end_trip_time"].DefaultCellStyle.NullValue = "—";
                dataGridViewOrders.Columns["end_trip_time"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridViewOrders.Columns["end_trip_time"].Width = 130;
            }

            // НАСТРОЙКА ОСТАЛЬНЫХ КОЛОНОК
            if (dataGridViewOrders.Columns.Contains("client_name"))
            {
                dataGridViewOrders.Columns["client_name"].HeaderText = "Клиент";
                dataGridViewOrders.Columns["client_name"].Width = 180;
            }

            if (dataGridViewOrders.Columns.Contains("driver_name"))
            {
                dataGridViewOrders.Columns["driver_name"].HeaderText = "Водитель";
                dataGridViewOrders.Columns["driver_name"].Width = 150;
            }

            // НАСТРОЙКА КОЛОНКИ АВТОМОБИЛЯ
            if (dataGridViewOrders.Columns.Contains("car_info"))
            {
                dataGridViewOrders.Columns["car_info"].HeaderText = "Автомобиль";
                dataGridViewOrders.Columns["car_info"].Width = 220;
                dataGridViewOrders.Columns["car_info"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (dataGridViewOrders.Columns.Contains("tariff_name"))
            {
                dataGridViewOrders.Columns["tariff_name"].HeaderText = "Тариф";
                dataGridViewOrders.Columns["tariff_name"].Width = 100;
            }

            if (dataGridViewOrders.Columns.Contains("order_status_name"))
            {
                dataGridViewOrders.Columns["order_status_name"].HeaderText = "Статус";
                dataGridViewOrders.Columns["order_status_name"].Width = 100;
            }

            if (dataGridViewOrders.Columns.Contains("payment_method_name"))
            {
                dataGridViewOrders.Columns["payment_method_name"].HeaderText = "Оплата";
                dataGridViewOrders.Columns["payment_method_name"].Width = 100;
            }

            if (dataGridViewOrders.Columns.Contains("address_from_text"))
            {
                dataGridViewOrders.Columns["address_from_text"].HeaderText = "Откуда";
                dataGridViewOrders.Columns["address_from_text"].Width = 250;
            }

            if (dataGridViewOrders.Columns.Contains("address_to_text"))
            {
                dataGridViewOrders.Columns["address_to_text"].HeaderText = "Куда";
                dataGridViewOrders.Columns["address_to_text"].Width = 250;
            }

            if (dataGridViewOrders.Columns.Contains("final_cost"))
            {
                dataGridViewOrders.Columns["final_cost"].HeaderText = "Стоимость";
                dataGridViewOrders.Columns["final_cost"].DefaultCellStyle.Format = "N2";
                dataGridViewOrders.Columns["final_cost"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridViewOrders.Columns["final_cost"].Width = 100;
            }

            // ДОБАВЛЯЕМ КНОПКИ
            DataGridViewButtonColumn reviewButton = new DataGridViewButtonColumn();
            reviewButton.Name = "ReviewButton";
            reviewButton.HeaderText = "Отзыв";
            reviewButton.Text = "Просмотр";
            reviewButton.UseColumnTextForButtonValue = true;
            reviewButton.Width = 70;
            dataGridViewOrders.Columns.Add(reviewButton);

            DataGridViewButtonColumn routeButton = new DataGridViewButtonColumn();
            routeButton.Name = "RouteButton";
            routeButton.HeaderText = "Маршрут";
            routeButton.Text = "На карте";
            routeButton.UseColumnTextForButtonValue = true;
            routeButton.Width = 80;
            dataGridViewOrders.Columns.Add(routeButton);

            // ПРИНУДИТЕЛЬНАЯ УСТАНОВКА ПОРЯДКА СТОЛБЦОВ
            // Сначала отключаем автоматическое создание порядка
            dataGridViewOrders.AutoGenerateColumns = false;

            // Устанавливаем DisplayIndex для каждой колонки
            int idx = 0;

            if (dataGridViewOrders.Columns["order_datetime"] != null)
                dataGridViewOrders.Columns["order_datetime"].DisplayIndex = idx++;

            if (dataGridViewOrders.Columns["start_trip_time"] != null)
                dataGridViewOrders.Columns["start_trip_time"].DisplayIndex = idx++;

            if (dataGridViewOrders.Columns["end_trip_time"] != null)
                dataGridViewOrders.Columns["end_trip_time"].DisplayIndex = idx++;

            if (dataGridViewOrders.Columns["client_name"] != null)
                dataGridViewOrders.Columns["client_name"].DisplayIndex = idx++;

            if (dataGridViewOrders.Columns["driver_name"] != null)
                dataGridViewOrders.Columns["driver_name"].DisplayIndex = idx++;

            if (dataGridViewOrders.Columns["car_info"] != null)
                dataGridViewOrders.Columns["car_info"].DisplayIndex = idx++;

            if (dataGridViewOrders.Columns["tariff_name"] != null)
                dataGridViewOrders.Columns["tariff_name"].DisplayIndex = idx++;

            if (dataGridViewOrders.Columns["order_status_name"] != null)
                dataGridViewOrders.Columns["order_status_name"].DisplayIndex = idx++;

            if (dataGridViewOrders.Columns["payment_method_name"] != null)
                dataGridViewOrders.Columns["payment_method_name"].DisplayIndex = idx++;

            if (dataGridViewOrders.Columns["address_from_text"] != null)
                dataGridViewOrders.Columns["address_from_text"].DisplayIndex = idx++;

            if (dataGridViewOrders.Columns["address_to_text"] != null)
                dataGridViewOrders.Columns["address_to_text"].DisplayIndex = idx++;

            if (dataGridViewOrders.Columns["final_cost"] != null)
                dataGridViewOrders.Columns["final_cost"].DisplayIndex = idx++;

            if (dataGridViewOrders.Columns["ReviewButton"] != null)
                dataGridViewOrders.Columns["ReviewButton"].DisplayIndex = idx++;

            if (dataGridViewOrders.Columns["RouteButton"] != null)
                dataGridViewOrders.Columns["RouteButton"].DisplayIndex = idx++;

            // Стиль заголовков
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewOrders.ColumnHeadersHeight = 45;

            // Стиль строк
            dataGridViewOrders.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dataGridViewOrders.DefaultCellStyle.ForeColor = Color.Black;
            dataGridViewOrders.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dataGridViewOrders.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridViewOrders.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 249);
            dataGridViewOrders.RowHeadersVisible = false;
            dataGridViewOrders.AllowUserToResizeRows = false;
            dataGridViewOrders.ReadOnly = true;
            dataGridViewOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewOrders.AllowUserToAddRows = false;
            dataGridViewOrders.AllowUserToDeleteRows = false;

            // Отключаем автоматическую ширину, используем ручную
            dataGridViewOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        }

        // ---------- ОБРАБОТЧИК КЛИКА ПО КНОПКАМ В ТАБЛИЦЕ ----------
        private void DataGridViewOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string columnName = dataGridViewOrders.Columns[e.ColumnIndex].Name;
            int orderId = Convert.ToInt32(dataGridViewOrders.Rows[e.RowIndex].Cells["order_id"].Value);

            if (columnName == "RouteButton")
            {
                string from = dataGridViewOrders.Rows[e.RowIndex].Cells["address_from_text"].Value.ToString();
                string to = dataGridViewOrders.Rows[e.RowIndex].Cells["address_to_text"].Value.ToString();
                RouteViewForm routeForm = new RouteViewForm(from, to);
                routeForm.ShowDialog();
            }
            else if (columnName == "ReviewButton")
            {
                DataRow review = adminOrder.GetReview(orderId);
                if (review != null)
                {
                    MessageBox.Show($"Оценка: {review["rating"]}\nКомментарий: {review["comment"]}", "Отзыв",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Отзыв по данному заказу отсутствует.", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // ---------- ОБРАБОТЧИКИ КНОПОК ----------
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadOrders();
            MessageBox.Show("Данные обновлены", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonApplyFilter_Click(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string search = textBoxSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(search))
            {
                LoadOrders();
                return;
            }

            if (ordersData == null)
            {
                LoadOrders();
                return;
            }

            DataView dv = new DataView(ordersData);
            dv.RowFilter = $"client_name LIKE '%{search}%' OR driver_name LIKE '%{search}%' OR " +
                           $"address_from_text LIKE '%{search}%' OR address_to_text LIKE '%{search}%'";
            dataGridViewOrders.DataSource = dv;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
            back = true;
            this.Close();
        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonSearch_Click(sender, e);
        }
    }
}