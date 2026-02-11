using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using taxi4;

namespace TaxiAdminApp
{
    public partial class AdminClientForm : Form
    {
        private AdminClient adminClient;
        private DataTable clientsData;
        private bool isNewAddressMode = false;

        public AdminClientForm()
        {
            InitializeComponent();
            adminClient = new AdminClient();
            LoadClients();
            LoadComboBoxData();
            ClearForm();
            SetupPlaceholderTexts();
        }

        private void SetupPlaceholderTexts()
        {
            SetPlaceholder(textBoxNewCity, "Город");
            SetPlaceholder(textBoxNewStreet, "Улица");
            SetPlaceholder(textBoxNewHouse, "Дом");
            SetPlaceholder(textBoxNewEntrance, "Подъезд (опционально)");
        }

        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Tag = placeholder;
            textBox.ForeColor = Color.Gray;
            textBox.Text = placeholder;
            textBox.Font = new Font(textBox.Font, FontStyle.Italic);
        }

        private void RemovePlaceholder(TextBox textBox)
        {
            if (textBox.ForeColor == Color.Gray)
            {
                textBox.Text = "";
                textBox.ForeColor = SystemColors.WindowText;
                textBox.Font = new Font(textBox.Font, FontStyle.Regular);
            }
        }

        private bool IsPlaceholderActive(TextBox textBox)
        {
            return textBox.ForeColor == Color.Gray && textBox.Text == textBox.Tag?.ToString();
        }

        private string GetRealText(TextBox textBox)
        {
            return IsPlaceholderActive(textBox) ? "" : textBox.Text;
        }

        private void LoadClients()
        {
            try
            {
                clientsData = adminClient.GetClients();
                dataGridViewClients.DataSource = clientsData;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки клиентов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxData()
        {
            try
            {
                LoadAddresses();
                DataTable statuses = adminClient.GetClientStatuses();
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

        private void LoadAddresses()
        {
            DataTable addresses = adminClient.GetAddresses();
            comboBoxAddress.DisplayMember = "full_address";
            comboBoxAddress.ValueMember = "id";
            comboBoxAddress.DataSource = addresses;
        }

        private void ConfigureDataGridView()
        {
            if (dataGridViewClients.Columns.Count > 0)
            {
                dataGridViewClients.Columns["client_id"].HeaderText = "ID";
                dataGridViewClients.Columns["client_id"].Width = 50;

                dataGridViewClients.Columns["first_name"].HeaderText = "Имя";
                dataGridViewClients.Columns["first_name"].Width = 100;

                dataGridViewClients.Columns["last_name"].HeaderText = "Фамилия";
                dataGridViewClients.Columns["last_name"].Width = 120;

                dataGridViewClients.Columns["patronymic"].HeaderText = "Отчество";
                dataGridViewClients.Columns["patronymic"].Width = 100;

                dataGridViewClients.Columns["phone_number"].HeaderText = "Телефон";
                dataGridViewClients.Columns["phone_number"].Width = 120;

                dataGridViewClients.Columns["status_name"].HeaderText = "Статус";
                dataGridViewClients.Columns["status_name"].Width = 100;

                dataGridViewClients.Columns["address_info"].HeaderText = "Адрес";
                dataGridViewClients.Columns["address_info"].Width = 200;

                string[] hiddenColumns = { "clent_status_id", "address_id", "account_id", "city", "street", "house", "entrance" };
                foreach (string col in hiddenColumns)
                {
                    if (dataGridViewClients.Columns.Contains(col))
                        dataGridViewClients.Columns[col].Visible = false;
                }

                dataGridViewClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewClients.ReadOnly = true;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            groupBoxClientData.Text = "Добавление нового клиента";

            int newAccountId = adminClient.CreateAccountForClient();
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
            if (dataGridViewClients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите клиента для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow selectedRow = dataGridViewClients.SelectedRows[0];
            LoadClientToForm(selectedRow);
            groupBoxClientData.Text = "Редактирование клиента";
        }

        private void LoadClientToForm(DataGridViewRow row)
        {
            try
            {
                textBoxID.Text = row.Cells["client_id"].Value.ToString();
                textBoxFirstName.Text = row.Cells["first_name"].Value.ToString();
                textBoxLastName.Text = row.Cells["last_name"].Value.ToString();
                textBoxPatronymic.Text = row.Cells["patronymic"].Value?.ToString() ?? "";
                textBoxPhone.Text = row.Cells["phone_number"].Value.ToString();
                textBoxAccountId.Text = row.Cells["account_id"].Value?.ToString() ?? "";
                textBoxAccountId.ReadOnly = true;
                textBoxAccountId.BackColor = Color.LightGray;

                if (row.Cells["clent_status_id"].Value != DBNull.Value)
                {
                    comboBoxStatus.SelectedValue = Convert.ToInt32(row.Cells["clent_status_id"].Value);
                }

                if (row.Cells["address_id"].Value != DBNull.Value)
                {
                    comboBoxAddress.SelectedValue = Convert.ToInt32(row.Cells["address_id"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных клиента: {ex.Message}", "Ошибка",
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

                    if (!adminClient.AccountExists(accountId))
                    {
                        MessageBox.Show("Аккаунт не найден. Создайте нового клиента через кнопку 'Добавить'",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    int addressId = isNewAddressMode ?
                        adminClient.AddAddress(
                            GetRealText(textBoxNewCity),
                            GetRealText(textBoxNewStreet),
                            GetRealText(textBoxNewHouse),
                            GetRealText(textBoxNewEntrance)
                        ) :
                        (int)comboBoxAddress.SelectedValue;

                    if (addressId == -1 && isNewAddressMode)
                    {
                        MessageBox.Show("Не удалось добавить адрес", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    success = adminClient.AddClient(
                        textBoxFirstName.Text.Trim(),
                        textBoxLastName.Text.Trim(),
                        textBoxPatronymic.Text.Trim(),
                        textBoxPhone.Text.Trim(),
                        (int)comboBoxStatus.SelectedValue,
                        addressId,
                        accountId
                    );
                }
                else
                {
                    success = adminClient.UpdateClient(
                        Convert.ToInt32(textBoxID.Text),
                        textBoxFirstName.Text.Trim(),
                        textBoxLastName.Text.Trim(),
                        textBoxPatronymic.Text.Trim(),
                        textBoxPhone.Text.Trim(),
                        (int)comboBoxStatus.SelectedValue,
                        (int)comboBoxAddress.SelectedValue,
                        Convert.ToInt32(textBoxAccountId.Text.Trim())
                    );
                }

                if (success)
                {
                    MessageBox.Show(isNewAddressMode ? "Клиент и адрес успешно добавлены" :
                        (string.IsNullOrEmpty(textBoxID.Text) ? "Клиент успешно добавлен" : "Данные клиента успешно обновлены"),
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadClients();
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
            if (dataGridViewClients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите клиента для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow selectedRow = dataGridViewClients.SelectedRows[0];
            int clientId = Convert.ToInt32(selectedRow.Cells["client_id"].Value);
            string clientName = $"{selectedRow.Cells["last_name"].Value} {selectedRow.Cells["first_name"].Value}";

            DialogResult result = MessageBox.Show(
                $"Вы уверены, что хотите удалить клиента {clientName}?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                bool success = adminClient.DeleteClient(clientId);
                if (success)
                {
                    MessageBox.Show("Клиент успешно удален", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadClients();
                }
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadClients();
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
                dataGridViewClients.DataSource = clientsData;
                return;
            }

            DataView dv = new DataView(clientsData);
            dv.RowFilter = $"first_name LIKE '%{searchText}%' OR " +
                          $"last_name LIKE '%{searchText}%' OR " +
                          $"patronymic LIKE '%{searchText}%' OR " +
                          $"phone_number LIKE '%{searchText}%'";

            dataGridViewClients.DataSource = dv;
        }

        private void dataGridViewClients_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewClients.Rows[e.RowIndex];
                LoadClientToForm(row);
                groupBoxClientData.Text = "Редактирование клиента";
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(textBoxFirstName.Text))
            {
                MessageBox.Show("Введите имя клиента", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxLastName.Text))
            {
                MessageBox.Show("Введите фамилию клиента", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLastName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxPhone.Text))
            {
                MessageBox.Show("Введите телефон клиента", "Ошибка",
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

            if (comboBoxAddress.SelectedValue == null && !isNewAddressMode)
            {
                MessageBox.Show("Выберите адрес", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxAddress.Focus();
                return false;
            }

            if (comboBoxStatus.SelectedValue == null)
            {
                MessageBox.Show("Выберите статус", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxStatus.Focus();
                return false;
            }

            if (isNewAddressMode)
            {
                if (string.IsNullOrWhiteSpace(GetRealText(textBoxNewCity)) ||
                    string.IsNullOrWhiteSpace(GetRealText(textBoxNewStreet)) ||
                    string.IsNullOrWhiteSpace(GetRealText(textBoxNewHouse)))
                {
                    MessageBox.Show("Заполните город, улицу и дом", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
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
            textBoxAccountId.ReadOnly = false;
            textBoxAccountId.BackColor = Color.White;

            textBoxNewCity.Clear();
            textBoxNewStreet.Clear();
            textBoxNewHouse.Clear();
            textBoxNewEntrance.Clear();
            SetupPlaceholderTexts();

            if (comboBoxStatus.Items.Count > 0)
                comboBoxStatus.SelectedIndex = 0;

            if (comboBoxAddress.Items.Count > 0)
                comboBoxAddress.SelectedIndex = 0;

            groupBoxClientData.Text = "Данные клиента";
            dataGridViewClients.DataSource = clientsData;

            if (isNewAddressMode)
            {
                SwitchToNormalAddressMode();
            }
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

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonSearch_Click(sender, e);
            }
        }

        private void buttonNewAddress_Click(object sender, EventArgs e)
        {
            isNewAddressMode = true;
            comboBoxAddress.Visible = false;
            textBoxNewCity.Visible = true;
            textBoxNewStreet.Visible = true;
            textBoxNewHouse.Visible = true;
            textBoxNewEntrance.Visible = true;
            buttonSaveAddress.Visible = true;
            buttonCancelAddress.Visible = true;
            buttonNewAddress.Visible = false;
            label6.Text = "Новый адрес:";
        }

        private void buttonSaveAddress_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(GetRealText(textBoxNewCity)) ||
                string.IsNullOrWhiteSpace(GetRealText(textBoxNewStreet)) ||
                string.IsNullOrWhiteSpace(GetRealText(textBoxNewHouse)))
            {
                MessageBox.Show("Заполните город, улицу и дом", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int newAddressId = adminClient.AddAddress(
                GetRealText(textBoxNewCity),
                GetRealText(textBoxNewStreet),
                GetRealText(textBoxNewHouse),
                GetRealText(textBoxNewEntrance)
            );

            if (newAddressId != -1)
            {
                SwitchToNormalAddressMode();
                LoadAddresses();
                comboBoxAddress.SelectedValue = newAddressId;
                MessageBox.Show("Новый адрес успешно добавлен", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonCancelAddress_Click(object sender, EventArgs e)
        {
            SwitchToNormalAddressMode();
        }

        private void SwitchToNormalAddressMode()
        {
            isNewAddressMode = false;
            comboBoxAddress.Visible = true;
            textBoxNewCity.Visible = false;
            textBoxNewStreet.Visible = false;
            textBoxNewHouse.Visible = false;
            textBoxNewEntrance.Visible = false;
            buttonSaveAddress.Visible = false;
            buttonCancelAddress.Visible = false;
            buttonNewAddress.Visible = true;
            label6.Text = "Адрес:";
        }

        // Обработчики событий для placeholder
        private void textBoxNewCity_Enter(object sender, EventArgs e)
        {
            RemovePlaceholder(textBoxNewCity);
        }

        private void textBoxNewCity_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNewCity.Text))
            {
                SetPlaceholder(textBoxNewCity, "Город");
            }
        }

        private void textBoxNewStreet_Enter(object sender, EventArgs e)
        {
            RemovePlaceholder(textBoxNewStreet);
        }

        private void textBoxNewStreet_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNewStreet.Text))
            {
                SetPlaceholder(textBoxNewStreet, "Улица");
            }
        }

        private void textBoxNewHouse_Enter(object sender, EventArgs e)
        {
            RemovePlaceholder(textBoxNewHouse);
        }

        private void textBoxNewHouse_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNewHouse.Text))
            {
                SetPlaceholder(textBoxNewHouse, "Дом");
            }
        }

        private void textBoxNewEntrance_Enter(object sender, EventArgs e)
        {
            RemovePlaceholder(textBoxNewEntrance);
        }

        private void textBoxNewEntrance_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxNewEntrance.Text))
            {
                SetPlaceholder(textBoxNewEntrance, "Подъезд (опционально)");
            }
        }
    }
}