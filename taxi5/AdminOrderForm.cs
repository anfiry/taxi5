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
        private string currentStatusFilter = null; // для отслеживания применённого статуса

        public AdminOrderForm()
        {
            InitializeComponent();
            adminOrder = new AdminOrder();
            LoadOrderStatuses();
            LoadOrders();
            ConfigureDataGridView();
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
                comboBoxStatusFilter.ValueMember = "name";
                comboBoxStatusFilter.DataSource = statuses;
                comboBoxStatusFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки статусов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- ЗАГРУЗКА ЗАКАЗОВ С ПРИМЕНЕНИЕМ ФИЛЬТРА ПО СТАТУСУ ----------
        private void LoadOrders()
        {
            try
            {
                // Получаем выбранный статус из комбобокса (если не "Все статусы")
                string selectedStatus = comboBoxStatusFilter.SelectedIndex > 0
                    ? comboBoxStatusFilter.SelectedValue.ToString()
                    : null;

                ordersData = adminOrder.GetOrders(selectedStatus);
                dataGridViewOrders.DataSource = ordersData;
                ConfigureDataGridView();
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

            dataGridViewOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            SetColumn("order_id", "ID", 60, true);
            SetColumn("client_name", "Клиент", 180, true);
            SetColumn("driver_name", "Водитель", 180, false);
            SetColumn("tariff_name", "Тариф", 120, false);
            SetColumn("order_status_name", "Статус", 120, false);
            SetColumn("payment_method_name", "Оплата", 120, false);
            SetColumn("address_from_text", "Откуда", 200, false);
            SetColumn("address_to_text", "Куда", 200, false);
            SetColumn("order_datetime", "Дата и время", 150, false);
            SetColumn("final_cost", "Стоимость", 100, false);

            if (dataGridViewOrders.Columns["status_id"] != null)
                dataGridViewOrders.Columns["status_id"].Visible = false;

            if (dataGridViewOrders.Columns["order_datetime"] != null)
            {
                dataGridViewOrders.Columns["order_datetime"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                dataGridViewOrders.Columns["order_datetime"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            if (dataGridViewOrders.Columns["final_cost"] != null)
            {
                dataGridViewOrders.Columns["final_cost"].DefaultCellStyle.Format = "N2";
                dataGridViewOrders.Columns["final_cost"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            int index = 0;
            string[] order = {
                "order_id", "client_name", "driver_name", "tariff_name",
                "order_status_name", "payment_method_name", "address_from_text",
                "address_to_text", "order_datetime", "final_cost"
            };
            foreach (string colName in order)
            {
                if (dataGridViewOrders.Columns[colName] != null)
                    dataGridViewOrders.Columns[colName].DisplayIndex = index++;
            }

            dataGridViewOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewOrders.ColumnHeadersHeight = 40;

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
                dataGridViewOrders.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        // ---------- ОБРАБОТЧИКИ ----------
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadOrders(); // перезагружает с текущим выбранным статусом
            MessageBox.Show("Данные обновлены", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonApplyFilter_Click(object sender, EventArgs e)
        {
            // Применяем фильтр по статусу – просто перезагружаем данные с выбранным статусом
            LoadOrders();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string search = textBoxSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(search))
            {
                // Если поиск пуст, возвращаем исходные данные (текущий статус)
                LoadOrders();
                return;
            }

            // Фильтруем уже загруженные данные по тексту
            DataView dv = new DataView(ordersData);
            dv.RowFilter = $"client_name LIKE '%{search}%' OR driver_name LIKE '%{search}%' OR " +
                           $"address_from_text LIKE '%{search}%' OR address_to_text LIKE '%{search}%'";
            dataGridViewOrders.DataSource = dv;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
            this.Hide();
        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonSearch_Click(sender, e);
        }
    }
}