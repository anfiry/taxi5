using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using taxi4;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            if (dataGridViewDrivers.Columns.Count > 0)
            {
                dataGridViewDrivers.Columns["driver_id"].HeaderText = "ID";
                dataGridViewDrivers.Columns["driver_id"].Width = 50;

                dataGridViewDrivers.Columns["first_name"].HeaderText = "Имя";
                dataGridViewDrivers.Columns["first_name"].Width = 100;

                dataGridViewDrivers.Columns["last_name"].HeaderText = "Фамилия";
                dataGridViewDrivers.Columns["last_name"].Width = 120;

                dataGridViewDrivers.Columns["patronymic"].HeaderText = "Отчество";
                dataGridViewDrivers.Columns["patronymic"].Width = 100;

                dataGridViewDrivers.Columns["phone_number"].HeaderText = "Телефон";
                dataGridViewDrivers.Columns["phone_number"].Width = 120;

                dataGridViewDrivers.Columns["status_name"].HeaderText = "Статус";
                dataGridViewDrivers.Columns["status_name"].Width = 100;

                dataGridViewDrivers.Columns["work_experience"].HeaderText = "Стаж";
                dataGridViewDrivers.Columns["work_experience"].Width = 80;

                dataGridViewDrivers.Columns["license_series"].HeaderText = "Серия прав";
                dataGridViewDrivers.Columns["license_series"].Width = 80;

                dataGridViewDrivers.Columns["license_number"].HeaderText = "Номер прав";
                dataGridViewDrivers.Columns["license_number"].Width = 100;

                dataGridViewDrivers.Columns["login"].HeaderText = "Логин";
                dataGridViewDrivers.Columns["login"].Width = 120;

                string[] hiddenColumns = { "driver_status_id", "account_id" };
                foreach (string col in hiddenColumns)
                {
                    if (dataGridViewDrivers.Columns.Contains(col))
                        dataGridViewDrivers.Columns[col].Visible = false;
                }

                dataGridViewDrivers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewDrivers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewDrivers.ReadOnly = true;
            }
        }

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
                MessageBox.Show($"Создан аккаунт с ID: {newAccountId}", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            DataGridViewRow selectedRow = dataGridViewDrivers.SelectedRows[0];
            LoadDriverToForm(selectedRow);
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
                {
                    comboBoxStatus.SelectedValue = Convert.ToInt32(row.Cells["driver_status_id"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных водителя: {ex.Message}", "Ошибка",
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

                if (string.IsNullOrEmpty(textBoxID.Text))
                {
                    int accountId = Convert.ToInt32(textBoxAccountId.Text.Trim());

                    if (!adminDriver.AccountExists(accountId))
                    {
                        MessageBox.Show("Аккаунт не найден. Создайте нового водителя через кнопку 'Добавить'",
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
                else
                {
                    success = adminDriver.UpdateDriver(
                        Convert.ToInt32(textBoxID.Text),
                        textBoxFirstName.Text.Trim(),
                        textBoxLastName.Text.Trim(),
                        textBoxPatronymic.Text.Trim(),
                        textBoxPhone.Text.Trim(),
                        (int)comboBoxStatus.SelectedValue,
                        Convert.ToInt32(textBoxAccountId.Text.Trim()),
                        decimal.Parse(textBoxWorkExperience.Text),
                        textBoxLicenseSeries.Text.Trim(),
                        textBoxLicenseNumber.Text.Trim()
                    );
                }

                if (success)
                {
                    MessageBox.Show(string.IsNullOrEmpty(textBoxID.Text) ?
                        "Водитель успешно добавлен" : "Данные водителя успешно обновлены",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDrivers();
                    LoadComboBoxData();
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

            DataGridViewRow selectedRow = dataGridViewDrivers.SelectedRows[0];
            int driverId = Convert.ToInt32(selectedRow.Cells["driver_id"].Value);
            string driverName = $"{selectedRow.Cells["last_name"].Value} {selectedRow.Cells["first_name"].Value}";

            DialogResult result = MessageBox.Show(
                $"Вы уверены, что хотите удалить водителя {driverName}?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                bool success = adminDriver.DeleteDriver(driverId);
                if (success)
                {
                    MessageBox.Show("Водитель успешно удален", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDrivers();
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
            string searchText = textBoxSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                dataGridViewDrivers.DataSource = driversData;
                return;
            }

            DataView dv = new DataView(driversData);
            dv.RowFilter = $"first_name LIKE '%{searchText}%' OR " +
                          $"last_name LIKE '%{searchText}%' OR " +
                          $"patronymic LIKE '%{searchText}%' OR " +
                          $"phone_number LIKE '%{searchText}%' OR " +
                          $"license_number LIKE '%{searchText}%'";

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

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(textBoxFirstName.Text))
            {
                MessageBox.Show("Введите имя водителя", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxLastName.Text))
            {
                MessageBox.Show("Введите фамилию водителя", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLastName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxPhone.Text))
            {
                MessageBox.Show("Введите телефон водителя", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPhone.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxAccountId.Text))
            {
                MessageBox.Show("ID аккаунта отсутствует", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!int.TryParse(textBoxAccountId.Text, out _))
            {
                MessageBox.Show("ID аккаунта должен быть числом", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxAccountId.Focus();
                return false;
            }

            if (!decimal.TryParse(textBoxWorkExperience.Text, out _))
            {
                MessageBox.Show("Стаж должен быть числом", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxWorkExperience.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxLicenseSeries.Text))
            {
                MessageBox.Show("Введите серию водительских прав", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLicenseSeries.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxLicenseNumber.Text))
            {
                MessageBox.Show("Введите номер водительских прав", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLicenseNumber.Focus();
                return false;
            }

            if (comboBoxStatus.SelectedValue == null)
            {
                MessageBox.Show("Выберите статус", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            textBoxSearch.Clear();
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

        private void buttonBack_Click(object sender, EventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
            this.Hide();
        }

        private void textBoxPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '+' && e.KeyChar != '-' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxAccountId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxWorkExperience_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSearch_Click(sender, e);
            }
        }

        private void buttonViewCars_Click(object sender, EventArgs e)
        {
            if (dataGridViewDrivers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите водителя для просмотра автомобилей", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow selectedRow = dataGridViewDrivers.SelectedRows[0];
            int driverId = Convert.ToInt32(selectedRow.Cells["driver_id"].Value);
            string driverName = $"{selectedRow.Cells["last_name"].Value} {selectedRow.Cells["first_name"].Value}";

            DataTable cars = adminDriver.GetCarsForDriver(driverId);

            if (cars.Rows.Count == 0)
            {
                MessageBox.Show($"У водителя {driverName} нет привязанных автомобилей", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string carInfo = $"Автомобили водителя {driverName}:\n\n";
                foreach (DataRow row in cars.Rows)
                {
                    carInfo += $"{row["brand"]} {row["model"]} ({row["color"]})\n";
                    carInfo += $"Гос.номер: {row["license_number"]} {row["region_code"]}\n";
                    carInfo += $"Год выпуска: {row["year_of_manufacture"]}\n\n";
                }

                MessageBox.Show(carInfo, "Автомобили водителя",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}