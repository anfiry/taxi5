using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    public partial class AdminDriverForm : Form
    {
        private AdminDriver adminDriver;
        private DataTable driversData;

        public AdminDriverForm()
        {
            InitializeComponent();
            adminDriver = new AdminDriver();
            LoadDrivers();
            LoadComboBoxData();
            ClearForm();
        }

        // ---------- ЗАГРУЗКА ДАННЫХ ----------
        private void LoadDrivers()
        {
            try
            {
                driversData = adminDriver.GetDrivers();
                dataGridViewDrivers.DataSource = driversData;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки водителей: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxData()
        {
            try
            {
                DataTable statuses = adminDriver.GetDriverStatuses();
                comboBoxStatus.DisplayMember = "status_name";
                comboBoxStatus.ValueMember = "id";
                comboBoxStatus.DataSource = statuses;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки статусов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- НАСТРОЙКА ТАБЛИЦЫ ----------
        private void ConfigureDataGridView()
        {
            if (dataGridViewDrivers.Columns.Count == 0) return;

            // Скрываем технические колонки
            if (dataGridViewDrivers.Columns["driver_status_id"] != null)
                dataGridViewDrivers.Columns["driver_status_id"].Visible = false;
            if (dataGridViewDrivers.Columns["account_id"] != null)
                dataGridViewDrivers.Columns["account_id"].Visible = false;

            // Настройка отображаемых колонок
            SetColumn("driver_id", "ID", 50, true);
            SetColumn("last_name", "Фамилия", 120, true);
            SetColumn("first_name", "Имя", 100, true);
            SetColumn("patronymic", "Отчество", 120, false);
            SetColumn("phone_number", "Телефон", 130, false);
            SetColumn("status_name", "Статус", 100, false);
            SetColumn("work_experience", "Стаж", 70, false);
            SetColumn("license_series", "Серия", 80, false);
            SetColumn("license_number", "Номер прав", 110, false);
            SetColumn("login", "Логин", 130, false);

            // Порядок колонок
            int index = 0;
            string[] order = {
                "driver_id", "last_name", "first_name", "patronymic", "phone_number",
                "status_name", "work_experience", "license_series", "license_number", "login"
            };
            foreach (string colName in order)
            {
                if (dataGridViewDrivers.Columns[colName] != null)
                    dataGridViewDrivers.Columns[colName].DisplayIndex = index++;
            }

            // Стиль заголовков
            dataGridViewDrivers.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dataGridViewDrivers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewDrivers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewDrivers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewDrivers.ColumnHeadersHeight = 40;

            // Стиль строк
            dataGridViewDrivers.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
            dataGridViewDrivers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 249);
            dataGridViewDrivers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewDrivers.ReadOnly = true;
            dataGridViewDrivers.RowHeadersVisible = false;
        }

        private void SetColumn(string columnName, string headerText, int width, bool frozen)
        {
            if (dataGridViewDrivers.Columns[columnName] != null)
            {
                dataGridViewDrivers.Columns[columnName].HeaderText = headerText;
                dataGridViewDrivers.Columns[columnName].Width = width;
                dataGridViewDrivers.Columns[columnName].Frozen = frozen;
                dataGridViewDrivers.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        // ---------- ДЕЙСТВИЯ С ВОДИТЕЛЯМИ ----------
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            groupBoxDriverData.Text = "Добавление нового водителя";
            int newAccountId = adminDriver.CreateAccountForDriver();
            if (newAccountId != -1)
            {
                textBoxAccountId.Text = newAccountId.ToString();
                textBoxAccountId.ReadOnly = true;
                textBoxAccountId.BackColor = Color.LightGreen;
      
            }
            else
            {
                MessageBox.Show("Не удалось создать аккаунт", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewDrivers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите водителя для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dataGridViewDrivers.SelectedRows[0];
            LoadDriverToForm(row);
            groupBoxDriverData.Text = "Редактирование водителя";
        }

        private void LoadDriverToForm(DataGridViewRow row)
        {
            try
            {
                textBoxID.Text = row.Cells["driver_id"].Value.ToString();
                textBoxFirstName.Text = row.Cells["first_name"].Value.ToString();
                textBoxLastName.Text = row.Cells["last_name"].Value.ToString();
                textBoxPatronymic.Text = row.Cells["patronymic"].Value?.ToString() ?? "";
                textBoxPhone.Text = row.Cells["phone_number"].Value.ToString();
                textBoxAccountId.Text = row.Cells["account_id"].Value?.ToString() ?? "";
                textBoxAccountId.ReadOnly = true;
                textBoxAccountId.BackColor = Color.LightGray;

                textBoxWorkExperience.Text = row.Cells["work_experience"].Value?.ToString() ?? "0";
                textBoxLicenseSeries.Text = row.Cells["license_series"].Value?.ToString() ?? "";
                textBoxLicenseNumber.Text = row.Cells["license_number"].Value?.ToString() ?? "";

                if (row.Cells["driver_status_id"].Value != DBNull.Value)
                    comboBoxStatus.SelectedValue = Convert.ToInt32(row.Cells["driver_status_id"].Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
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
                int accountId = Convert.ToInt32(textBoxAccountId.Text.Trim());

                if (string.IsNullOrEmpty(textBoxID.Text)) // добавление
                {
                    if (!adminDriver.AccountExists(accountId))
                    {
                        MessageBox.Show("Аккаунт не найден. Используйте кнопку 'Добавить'",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    success = adminDriver.AddDriver(
                        textBoxFirstName.Text.Trim(),
                        textBoxLastName.Text.Trim(),
                        textBoxPatronymic.Text.Trim(),
                        textBoxPhone.Text.Trim(),
                        (int)comboBoxStatus.SelectedValue,
                        accountId,
                        decimal.Parse(textBoxWorkExperience.Text),
                        textBoxLicenseSeries.Text.Trim(),
                        textBoxLicenseNumber.Text.Trim()
                    );
                }
                else // обновление
                {
                    success = adminDriver.UpdateDriver(
                        Convert.ToInt32(textBoxID.Text),
                        textBoxFirstName.Text.Trim(),
                        textBoxLastName.Text.Trim(),
                        textBoxPatronymic.Text.Trim(),
                        textBoxPhone.Text.Trim(),
                        (int)comboBoxStatus.SelectedValue,
                        accountId,
                        decimal.Parse(textBoxWorkExperience.Text),
                        textBoxLicenseSeries.Text.Trim(),
                        textBoxLicenseNumber.Text.Trim()
                    );
                }

                if (success)
                {
                    MessageBox.Show(string.IsNullOrEmpty(textBoxID.Text) ?
                        "Водитель успешно добавлен" : "Данные водителя обновлены",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDrivers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewDrivers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите водителя для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dataGridViewDrivers.SelectedRows[0];
            int driverId = Convert.ToInt32(row.Cells["driver_id"].Value);
            string driverName = $"{row.Cells["last_name"].Value} {row.Cells["first_name"].Value}";

            if (MessageBox.Show($"Удалить водителя {driverName}?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (adminDriver.DeleteDriver(driverId))
                {
                    MessageBox.Show("Водитель удалён", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDrivers();
                    ClearForm();
                }
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadDrivers();
            LoadComboBoxData();
            ClearForm();
            MessageBox.Show("Данные обновлены", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string search = textBoxSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(search))
            {
                dataGridViewDrivers.DataSource = driversData;
                return;
            }

            DataView dv = new DataView(driversData);
            dv.RowFilter = $"last_name LIKE '%{search}%' OR first_name LIKE '%{search}%' OR " +
                           $"patronymic LIKE '%{search}%' OR phone_number LIKE '%{search}%'";
            dataGridViewDrivers.DataSource = dv;
        }

        private void dataGridViewDrivers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewDrivers.Rows[e.RowIndex];
                LoadDriverToForm(row);
                groupBoxDriverData.Text = "Редактирование водителя";
            }
        }

        // ---------- ВАЛИДАЦИЯ ----------
        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(textBoxFirstName.Text))
            {
                MessageBox.Show("Введите имя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxFirstName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxLastName.Text))
            {
                MessageBox.Show("Введите фамилию", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLastName.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxPhone.Text))
            {
                MessageBox.Show("Введите телефон", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPhone.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxAccountId.Text) || !int.TryParse(textBoxAccountId.Text, out _))
            {
                MessageBox.Show("ID аккаунта должен быть числом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxAccountId.Focus();
                return false;
            }
            if (!decimal.TryParse(textBoxWorkExperience.Text, out _))
            {
                MessageBox.Show("Стаж должен быть числом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxWorkExperience.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxLicenseSeries.Text))
            {
                MessageBox.Show("Введите серию прав", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLicenseSeries.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxLicenseNumber.Text))
            {
                MessageBox.Show("Введите номер прав", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLicenseNumber.Focus();
                return false;
            }
            if (comboBoxStatus.SelectedValue == null)
            {
                MessageBox.Show("Выберите статус", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxStatus.Focus();
                return false;
            }
            return true;
        }

        private void ClearForm()
        {
            textBoxID.Clear();
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxPatronymic.Clear();
            textBoxPhone.Clear();
            textBoxAccountId.Clear();
            textBoxWorkExperience.Clear();
            textBoxLicenseSeries.Clear();
            textBoxLicenseNumber.Clear();
            textBoxAccountId.ReadOnly = false;
            textBoxAccountId.BackColor = Color.White;

            if (comboBoxStatus.Items.Count > 0)
                comboBoxStatus.SelectedIndex = 0;

            groupBoxDriverData.Text = "Данные водителя";
            dataGridViewDrivers.DataSource = driversData;
        }

        // ---------- ПРОСМОТР АВТОМОБИЛЕЙ ----------
        private void buttonViewCars_Click(object sender, EventArgs e)
        {
            if (dataGridViewDrivers.SelectedRows.Count == 0 && string.IsNullOrEmpty(textBoxID.Text))
            {
                MessageBox.Show("Выберите водителя для просмотра автомобилей", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int driverId;
            if (!string.IsNullOrEmpty(textBoxID.Text))
                driverId = Convert.ToInt32(textBoxID.Text);
            else
                driverId = Convert.ToInt32(dataGridViewDrivers.SelectedRows[0].Cells["driver_id"].Value);

            DataTable cars = adminDriver.GetCarsForDriver(driverId);
            if (cars.Rows.Count == 0)
            {
                MessageBox.Show("У водителя нет привязанных автомобилей", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string msg = "Автомобили водителя:\n\n";
                foreach (DataRow row in cars.Rows)
                {
                    msg += $"{row["brand_name"]} {row["model_name"]}, {row["color_name"]}\n";
                    msg += $"Госномер: {row["license_number"]} {row["region_code"]}, {row["year_of_manufacture"]} г.\n\n";
                }
                MessageBox.Show(msg, "Автомобили", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ---------- НАЗАД ----------
        private void buttonBack_Click(object sender, EventArgs e)
        {
            AdminMenu menu = new AdminMenu();
            menu.Show();
            this.Hide();
        }

        // ---------- ОГРАНИЧЕНИЯ ВВОДА ----------
        private void textBoxPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '+' && e.KeyChar != '-' && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBoxAccountId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBoxWorkExperience_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonSearch_Click(sender, e);
        }
    }
}