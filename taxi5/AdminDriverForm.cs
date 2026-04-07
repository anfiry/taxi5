using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using taxi4;
using System.Linq;

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
            groupBoxDriverData.Visible = false;
            buttonViewCars.Visible = true;
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
            string[] hiddenColumns = {
                "driver_id", "driver_status_id", "account_id"
            };
            foreach (string col in hiddenColumns)
                if (dataGridViewDrivers.Columns.Contains(col))
                    dataGridViewDrivers.Columns[col].Visible = false;

            // Настройка отображения колонок с указанием порядка
            var columnsConfig = new List<(string Name, string Header, int FillWeight, int DisplayIndex)>
            {
                ("last_name", "Фамилия", 2, 0),
                ("first_name", "Имя", 2, 1),
                ("patronymic", "Отчество", 2, 2),
                ("phone_number", "Телефон", 2, 3),
                ("status_name", "Статус водителя", 1, 4),
                ("shift_status_name", "Статус смены", 1, 5),
                ("work_experience", "Стаж (лет)", 1, 6),
                ("license_series", "Серия прав", 1, 7),
                ("license_number", "Номер прав", 1, 8),
                ("login", "Логин", 2, 9),
                ("password", "Пароль", 2, 10)
            };

            // Настраиваем каждую колонку
            foreach (var col in columnsConfig)
            {
                if (dataGridViewDrivers.Columns[col.Name] != null)
                {
                    dataGridViewDrivers.Columns[col.Name].HeaderText = col.Header;
                    dataGridViewDrivers.Columns[col.Name].FillWeight = col.FillWeight;
                    dataGridViewDrivers.Columns[col.Name].Visible = true;
                    dataGridViewDrivers.Columns[col.Name].DisplayIndex = col.DisplayIndex;
                    dataGridViewDrivers.Columns[col.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
            }

            // Настройка внешнего вида
            dataGridViewDrivers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewDrivers.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dataGridViewDrivers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewDrivers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewDrivers.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewDrivers.ColumnHeadersHeight = 40;

            dataGridViewDrivers.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
            dataGridViewDrivers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 249);
            dataGridViewDrivers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewDrivers.ReadOnly = true;
            dataGridViewDrivers.RowHeadersVisible = false;
            dataGridViewDrivers.AllowUserToAddRows = false;
            dataGridViewDrivers.AllowUserToDeleteRows = false;
        }

        // ---------- ДЕЙСТВИЯ С ВОДИТЕЛЯМИ ----------
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            groupBoxDriverData.Visible = true;
            groupBoxDriverData.Text = "Добавление нового водителя";

            label7.Visible = false;
            textBoxAccountId.Visible = false;

            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string generatedLogin = $"driver_{timestamp}";
            string generatedPassword = GenerateRandomPassword(8);

            int newAccountId = adminDriver.CreateAccountForDriver(generatedLogin, generatedPassword);
            if (newAccountId != -1)
            {
                textBoxAccountId.Text = newAccountId.ToString();
                textBoxAccountId.ReadOnly = true;
                textBoxAccountId.BackColor = Color.LightGreen;

                MessageBox.Show($"Создан аккаунт:\nЛогин: {generatedLogin}\nПароль: {generatedPassword}\n\n" +
                                "Сохраните эти данные для передачи водителю.",
                                "Данные для входа",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Не удалось создать аккаунт", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            groupBoxDriverData.Visible = true;
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
                int selectedStatusId = (int)comboBoxStatus.SelectedValue;

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
                        selectedStatusId,
                        accountId,
                        decimal.Parse(textBoxWorkExperience.Text),
                        textBoxLicenseSeries.Text.Trim(),
                        textBoxLicenseNumber.Text.Trim()
                    );
                }
                else // обновление
                {
                    int driverId = Convert.ToInt32(textBoxID.Text);
                    int oldStatusId = adminDriver.GetDriverStatusId(driverId);

                    success = adminDriver.UpdateDriver(
                        driverId,
                        textBoxFirstName.Text.Trim(),
                        textBoxLastName.Text.Trim(),
                        textBoxPatronymic.Text.Trim(),
                        textBoxPhone.Text.Trim(),
                        selectedStatusId,
                        accountId,
                        decimal.Parse(textBoxWorkExperience.Text),
                        textBoxLicenseSeries.Text.Trim(),
                        textBoxLicenseNumber.Text.Trim()
                    );

                    // Если статус изменился на "Заблокирован" (2), добавляем в blacklist
                    if (success && oldStatusId != 2 && selectedStatusId == 2)
                    {
                        DataTable reasons = adminDriver.GetBlockReasons();
                        if (reasons.Rows.Count > 0)
                        {
                            using (var blockDialog = new BlockUserDialog(reasons))
                            {
                                if (blockDialog.ShowDialog() == DialogResult.OK)
                                {
                                    adminDriver.BlockDriver(driverId, blockDialog.SelectedReasonId,
                                        blockDialog.StartDate, blockDialog.EndDate);
                                }
                            }
                        }
                    }
                    // Если статус изменился с "Заблокирован" на другой, убираем из blacklist
                    else if (success && oldStatusId == 2 && selectedStatusId != 2)
                    {
                        adminDriver.UnblockDriver(driverId);
                    }
                }

                if (success)
                {
                    MessageBox.Show(string.IsNullOrEmpty(textBoxID.Text) ?
                        "Водитель успешно добавлен" : "Данные водителя обновлены",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    groupBoxDriverData.Visible = false;
                    buttonViewCars.Visible = true;
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

        private string GenerateRandomPassword(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // ---------- БЛОКИРОВКА/РАЗБЛОКИРОВКА ----------
        /*
        private void buttonBlock_Click(object sender, EventArgs e)
        {
            if (dataGridViewDrivers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите водителя для блокировки", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dataGridViewDrivers.SelectedRows[0];
            int driverId = Convert.ToInt32(row.Cells["driver_id"].Value);
            string driverName = $"{row.Cells["last_name"].Value} {row.Cells["first_name"].Value}";

            if (adminDriver.IsDriverBlocked(driverId))
            {
                MessageBox.Show("Водитель уже заблокирован", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable reasons = adminDriver.GetBlockReasons();
            if (reasons.Rows.Count == 0)
            {
                MessageBox.Show("Нет доступных причин для блокировки", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var blockDialog = new BlockUserDialog(reasons))
            {
                if (blockDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        bool success = adminDriver.BlockDriver(
                            driverId,
                            blockDialog.SelectedReasonId,
                            blockDialog.StartDate,
                            blockDialog.EndDate
                        );
                        if (success)
                        {
                            MessageBox.Show($"Водитель {driverName} заблокирован", "Успех",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDrivers();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка блокировки: {ex.Message}", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonUnblock_Click(object sender, EventArgs e)
        {
            if (dataGridViewDrivers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите водителя для разблокировки", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dataGridViewDrivers.SelectedRows[0];
            int driverId = Convert.ToInt32(row.Cells["driver_id"].Value);
            string driverName = $"{row.Cells["last_name"].Value} {row.Cells["first_name"].Value}";

            if (!adminDriver.IsDriverBlocked(driverId))
            {
                MessageBox.Show("Водитель не заблокирован", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show($"Разблокировать водителя {driverName}?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    bool success = adminDriver.UnblockDriver(driverId);
                    if (success)
                    {
                        MessageBox.Show("Водитель разблокирован", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDrivers();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка разблокировки: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
        */

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadDrivers();
            LoadComboBoxData();
            groupBoxDriverData.Visible = false;
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
                groupBoxDriverData.Visible = true;
            }
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


        private bool IsOnlyLetters(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return true;
            foreach (char c in text)
            {
                if (!char.IsLetter(c) && c != '-' && c != ' ')
                    return false;
            }
            return true;
        }


        private bool IsValidPhone(string phone)
        {
            string digits = new string(phone.Where(char.IsDigit).ToArray());
            if (digits.Length != 11) return false;
            if (!digits.StartsWith("79")) return false;
            return true;
        }

        private bool IsOnlyDigits(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return false;
            foreach (char c in text)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }
        // ---------- ВАЛИДАЦИЯ ----------
        private bool ValidateForm()
        {
            // Проверка имени (только буквы)
            if (string.IsNullOrWhiteSpace(textBoxFirstName.Text))
            {
                MessageBox.Show("Введите имя водителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxFirstName.Focus();
                return false;
            }
            if (!IsOnlyLetters(textBoxFirstName.Text))
            {
                MessageBox.Show("Имя должно содержать только буквы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxFirstName.Focus();
                textBoxFirstName.SelectAll();
                return false;
            }

            // Проверка фамилии (только буквы)
            if (string.IsNullOrWhiteSpace(textBoxLastName.Text))
            {
                MessageBox.Show("Введите фамилию водителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLastName.Focus();
                return false;
            }
            if (!IsOnlyLetters(textBoxLastName.Text))
            {
                MessageBox.Show("Фамилия должна содержать только буквы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLastName.Focus();
                textBoxLastName.SelectAll();
                return false;
            }

            // Проверка отчества (если заполнено, то только буквы)
            if (!string.IsNullOrWhiteSpace(textBoxPatronymic.Text) && !IsOnlyLetters(textBoxPatronymic.Text))
            {
                MessageBox.Show("Отчество должно содержать только буквы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPatronymic.Focus();
                textBoxPatronymic.SelectAll();
                return false;
            }

            // Проверка телефона
            if (string.IsNullOrWhiteSpace(textBoxPhone.Text))
            {
                MessageBox.Show("Введите телефон водителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPhone.Focus();
                return false;
            }
            if (!IsValidPhone(textBoxPhone.Text))
            {
                MessageBox.Show("Введите корректный номер телефона в формате: +79001234567", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPhone.Focus();
                textBoxPhone.SelectAll();
                return false;
            }

            // Проверка стажа
            if (string.IsNullOrWhiteSpace(textBoxWorkExperience.Text))
            {
                MessageBox.Show("Введите стаж водителя", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxWorkExperience.Focus();
                return false;
            }
            if (!decimal.TryParse(textBoxWorkExperience.Text, out decimal experience) || experience < 0 || experience > 50)
            {
                MessageBox.Show("Введите корректный стаж (0-50 лет)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxWorkExperience.Focus();
                textBoxWorkExperience.SelectAll();
                return false;
            }

            // Проверка серии прав (только ЦИФРЫ, 4 цифры)
            if (string.IsNullOrWhiteSpace(textBoxLicenseSeries.Text))
            {
                MessageBox.Show("Введите серию прав", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLicenseSeries.Focus();
                return false;
            }
            if (!IsOnlyDigits(textBoxLicenseSeries.Text))
            {
                MessageBox.Show("Серия прав должна содержать только цифры", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLicenseSeries.Focus();
                textBoxLicenseSeries.SelectAll();
                return false;
            }
            if (textBoxLicenseSeries.Text.Length != 4)
            {
                MessageBox.Show("Серия прав должна содержать 4 цифры", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLicenseSeries.Focus();
                textBoxLicenseSeries.SelectAll();
                return false;
            }

            // Проверка номера прав (только ЦИФРЫ, 6 цифр)
            if (string.IsNullOrWhiteSpace(textBoxLicenseNumber.Text))
            {
                MessageBox.Show("Введите номер прав", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLicenseNumber.Focus();
                return false;
            }
            if (!IsOnlyDigits(textBoxLicenseNumber.Text))
            {
                MessageBox.Show("Номер прав должен содержать только цифры", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLicenseNumber.Focus();
                textBoxLicenseNumber.SelectAll();
                return false;
            }
            if (textBoxLicenseNumber.Text.Length != 6)
            {
                MessageBox.Show("Номер прав должен содержать 6 цифр", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLicenseNumber.Focus();
                textBoxLicenseNumber.SelectAll();
                return false;
            }

            // Проверка статуса
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