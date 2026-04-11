using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace taxi4
{
    public partial class AdminTariffForm : Form
    {
        private AdminTariff adminTariff;
        private DataTable tariffsData;
        private bool back = false;


        public AdminTariffForm()
        {
            InitializeComponent();
            adminTariff = new AdminTariff();
            LoadTariffs();
            LoadComboBoxData();
            ClearForm();
            groupBoxTariffData.Visible = false;

        }

        public void OnClosed()
        {
            if (back)
            { back = false; }
            else { Application.Exit(); }
        }

        private void LoadTariffs()
        {
            try
            {
                tariffsData = adminTariff.GetTariffs();
                dataGridViewTariffs.DataSource = tariffsData;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки тарифов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxData()
        {
            try
            {
                DataTable statuses = adminTariff.GetTariffStatuses();
                comboBoxStatus.DisplayMember = "status_name";
                comboBoxStatus.ValueMember = "status_id";
                comboBoxStatus.DataSource = statuses;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки статусов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            if (dataGridViewTariffs.Columns.Count == 0) return;

            if (dataGridViewTariffs.Columns["tariff_id"] != null)
                dataGridViewTariffs.Columns["tariff_id"].Visible = false;

            dataGridViewTariffs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            SetColumn("tariff_id", "ID", 50, true);
            SetColumn("name", "Название", 200, false);
            SetColumn("base_cost", "Базовая стоимость", 150, false);
            SetColumn("status_name", "Статус", 120, false);
            if (dataGridViewTariffs.Columns["status_id"] != null)
                dataGridViewTariffs.Columns["status_id"].Visible = false;

            // Порядок колонок
            int index = 0;
            string[] order = { "tariff_id", "name", "base_cost", "status_name" };
            foreach (string colName in order)
            {
                if (dataGridViewTariffs.Columns[colName] != null)
                    dataGridViewTariffs.Columns[colName].DisplayIndex = index++;
            }

            // Стиль заголовков
            dataGridViewTariffs.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dataGridViewTariffs.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewTariffs.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewTariffs.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewTariffs.ColumnHeadersHeight = 40;

            // Стиль строк
            dataGridViewTariffs.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
            dataGridViewTariffs.DefaultCellStyle.ForeColor = Color.Black;
            dataGridViewTariffs.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dataGridViewTariffs.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridViewTariffs.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 249);
            dataGridViewTariffs.RowHeadersVisible = false;
            dataGridViewTariffs.AllowUserToResizeRows = false;
            dataGridViewTariffs.ReadOnly = true;
            dataGridViewTariffs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void SetColumn(string columnName, string headerText, int width, bool frozen)
        {
            if (dataGridViewTariffs.Columns[columnName] != null)
            {
                dataGridViewTariffs.Columns[columnName].HeaderText = headerText;
                dataGridViewTariffs.Columns[columnName].Width = width;
                dataGridViewTariffs.Columns[columnName].Frozen = frozen;
                dataGridViewTariffs.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            groupBoxTariffData.Visible = true;
            groupBoxTariffData.Text = "Добавление нового тарифа";
            textBoxTariffId.Visible = false;
            labelTariffId.Visible = false;
            textBoxBaseCost.Text = "0"; // Устанавливаем значение по умолчанию
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewTariffs.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите тариф для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            groupBoxTariffData.Visible = true;
            DataGridViewRow row = dataGridViewTariffs.SelectedRows[0];
            LoadTariffToForm(row);
            groupBoxTariffData.Text = "Редактирование тарифа";
            textBoxTariffId.Visible = false;
            labelTariffId.Visible = false;
        }

        private void LoadTariffToForm(DataGridViewRow row)
        {
            try
            {
                textBoxTariffId.Text = row.Cells["tariff_id"].Value.ToString();
                textBoxName.Text = row.Cells["name"].Value.ToString();
                textBoxBaseCost.Text = row.Cells["base_cost"].Value.ToString();

                if (row.Cells["status_id"].Value != DBNull.Value)
                    comboBoxStatus.SelectedValue = Convert.ToInt32(row.Cells["status_id"].Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных тарифа: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                bool success;
                string name = textBoxName.Text.Trim();
                decimal baseCost = decimal.Parse(textBoxBaseCost.Text);
                int statusId = (int)comboBoxStatus.SelectedValue;

                if (string.IsNullOrEmpty(textBoxTariffId.Text))
                {
                    success = adminTariff.AddTariff(name, baseCost, statusId);
                    if (success)
                        MessageBox.Show("Тариф успешно добавлен", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    int tariffId = int.Parse(textBoxTariffId.Text);
                    success = adminTariff.UpdateTariff(tariffId, name, baseCost, statusId);
                    if (success)
                        MessageBox.Show("Тариф успешно обновлен", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (success)
                {
                    ClearForm();
                    groupBoxTariffData.Visible = false;
                    LoadTariffs();
                    LoadComboBoxData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadTariffs();
            LoadComboBoxData();
            ClearForm();
            groupBoxTariffData.Visible = false;

            MessageBox.Show("Данные обновлены", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string search = textBoxSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(search))
            {
                dataGridViewTariffs.DataSource = tariffsData;
                return;
            }

            DataView dv = new DataView(tariffsData);
            dv.RowFilter = $"name LIKE '%{search}%' OR status_name LIKE '%{search}%'";
            dataGridViewTariffs.DataSource = dv;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
            back = true;
            this.Close();
        }

        private void ClearForm()
        {
            textBoxTariffId.Clear();
            textBoxName.Clear();
            textBoxBaseCost.Clear();
            textBoxSearch.Clear();

            if (comboBoxStatus.Items.Count > 0)
                comboBoxStatus.SelectedIndex = 0;

            groupBoxTariffData.Text = "Данные тарифа";
            dataGridViewTariffs.DataSource = tariffsData;
        }

        private bool ValidateForm()
        {
            // Проверка названия
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Введите название тарифа", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxName.Focus();
                return false;
            }

            // Проверка названия на длину
            if (textBoxName.Text.Trim().Length > 50)
            {
                MessageBox.Show("Название тарифа не должно превышать 50 символов", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxName.Focus();
                return false;
            }

            // Проверка стоимости - пустое поле
            if (string.IsNullOrWhiteSpace(textBoxBaseCost.Text))
            {
                MessageBox.Show("Введите базовую стоимость", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBaseCost.Focus();
                return false;
            }

            // Проверка стоимости - корректное число
            if (!decimal.TryParse(textBoxBaseCost.Text, out decimal cost))
            {
                MessageBox.Show("Введите корректное числовое значение стоимости", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBaseCost.Focus();
                return false;
            }

            // Проверка стоимости - не отрицательная
            if (cost < 0)
            {
                MessageBox.Show("Стоимость не может быть отрицательной", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBaseCost.Focus();
                return false;
            }

            // Проверка стоимости - максимальное значение
            if (cost > 999999)
            {
                MessageBox.Show("Стоимость не может превышать 999 999", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxBaseCost.Focus();
                return false;
            }

            // Проверка статуса
            if (comboBoxStatus.SelectedValue == null)
            {
                MessageBox.Show("Выберите статус", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxStatus.Focus();
                return false;
            }

            return true;
        }

        private void textBoxBaseCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем цифры, запятую/точку и управляющие клавиши
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            // Заменяем точку на запятую для корректного парсинга decimal
            if (e.KeyChar == '.')
                e.KeyChar = ',';

            // Запрещаем ввод второй запятой
            if (e.KeyChar == ',' && ((TextBox)sender).Text.Contains(","))
            {
                e.Handled = true;
                return;
            }

            // Запрещаем ввод ведущих нулей
            if (((TextBox)sender).Text == "0" && char.IsDigit(e.KeyChar) && e.KeyChar != '0')
            {
                ((TextBox)sender).Text = "";
            }
        }

        private void textBoxBaseCost_Leave(object sender, EventArgs e)
        {
            // Автоматическое форматирование при потере фокуса
            if (!string.IsNullOrWhiteSpace(textBoxBaseCost.Text))
            {
                if (decimal.TryParse(textBoxBaseCost.Text, out decimal value))
                {
                    if (value < 0)
                        textBoxBaseCost.Text = "0";
                    else if (value > 999999)
                        textBoxBaseCost.Text = "999999";
                    else
                        textBoxBaseCost.Text = value.ToString("N0");
                }
            }
        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonSearch_Click(sender, e);
        }

        private void dataGridViewTariffs_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewTariffs.Rows[e.RowIndex];
                LoadTariffToForm(row);
                groupBoxTariffData.Text = "Редактирование тарифа";
                groupBoxTariffData.Visible = true;
            }
        }
    }
}