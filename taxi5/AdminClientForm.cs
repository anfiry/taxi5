using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using taxi4;

namespace taxi4
{
    public partial class AdminClientForm : Form
    {

        private AdminClient adminClient;
        private DataTable clientsData;
        private DataTable allAddresses;
        private DataTable originalClientsData;
        private bool columnsConfigured = false; // Флаг для отслеживания настройки колонок

        public AdminClientForm()
        {
            InitializeComponent();
            adminClient = new AdminClient();

            // Включаем двойную буферизацию для DataGridView
            typeof(DataGridView).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.SetProperty,
                null, dataGridViewClients, new object[] { true });

            // Подписываемся на события
            this.dataGridViewClients.CellDoubleClick += new DataGridViewCellEventHandler(this.dataGridViewClients_CellDoubleClick);

            LoadClients();
            LoadComboBoxData();
            LoadAllAddresses();
            ClearForm();
            SetupAutoComplete();

            // ToolTip
            this.toolTip = new System.Windows.Forms.ToolTip();
            this.toolTip.AutoPopDelay = 5000;
            this.toolTip.InitialDelay = 500;
            this.toolTip.ReshowDelay = 100;
            this.toolTip.ShowAlways = true;

            // Скрываем правую панель при загрузке
            groupBoxClientData.Visible = false;

            // Скрываем ID
            textBoxID.Visible = false;
            ID1.Visible = false;

            // Скрываем поле аккаунта (оно будет видно только при добавлении)
            label7.Visible = false;
            textBoxAccountId.Visible = false;
        }

        private void SetupAutoComplete()
        {
            var autoCompleteList = new AutoCompleteStringCollection();
            foreach (DataRow row in allAddresses.Rows)
            {
                autoCompleteList.Add(row["full_address"].ToString());
            }

            cmbAddress.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbAddress.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbAddress.AutoCompleteCustomSource = autoCompleteList;
            cmbAddress.DropDownStyle = ComboBoxStyle.DropDown;
        }

        private void LoadAllAddresses()
        {
            allAddresses = adminClient.GetAddresses();
        }

        private void LoadClients()
        {
            try
            {
                // Сохраняем позицию прокрутки и выделение
                int firstDisplayedScrollRow = 0;
                int selectedRowIndex = -1;

                if (dataGridViewClients.Rows.Count > 0)
                {
                    firstDisplayedScrollRow = dataGridViewClients.FirstDisplayedScrollingRowIndex;
                    if (dataGridViewClients.SelectedRows.Count > 0)
                        selectedRowIndex = dataGridViewClients.SelectedRows[0].Index;
                }

                clientsData = adminClient.GetClients();

                if (clientsData == null || clientsData.Rows.Count == 0)
                {
                    MessageBox.Show("Нет данных для отображения. Проверьте подключение к БД.",
                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridViewClients.DataSource = null;
                    return;
                }

                // Отключаем визуальные обновления
                dataGridViewClients.SuspendLayout();

                // Сохраняем источник данных
                originalClientsData = clientsData.Copy();

                // Обновляем данные без сброса настроек
                dataGridViewClients.DataSource = null;
                dataGridViewClients.DataSource = clientsData;

                // Настраиваем колонки только один раз
                if (!columnsConfigured)
                {
                    ConfigureDataGridView();
                }

                // Восстанавливаем позицию прокрутки и выделение
                if (firstDisplayedScrollRow >= 0 && firstDisplayedScrollRow < dataGridViewClients.Rows.Count)
                {
                    dataGridViewClients.FirstDisplayedScrollingRowIndex = firstDisplayedScrollRow;
                }

                if (selectedRowIndex >= 0 && selectedRowIndex < dataGridViewClients.Rows.Count)
                {
                    dataGridViewClients.Rows[selectedRowIndex].Selected = true;
                }

                // Включаем визуальные обновления
                dataGridViewClients.ResumeLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки клиентов: {ex.Message}\n\n{ex.StackTrace}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridViewClients.DataSource = null;
            }
        }

        private void LoadComboBoxData()
        {
            try
            {
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

        private void ConfigureDataGridView()
        {
            if (columnsConfigured) return; // Уже настроено
            if (dataGridViewClients.Columns.Count == 0) return;

            // Отключаем обновления во время настройки
            dataGridViewClients.SuspendLayout();

            // Сначала показываем все нужные колонки
            string[] visibleColumns = { "last_name", "first_name", "patronymic",
                        "phone_number", "status_name", "address_info", "login", "password" };

            foreach (string col in visibleColumns)
            {
                if (dataGridViewClients.Columns.Contains(col))
                {
                    dataGridViewClients.Columns[col].Visible = true;
                }
            }

            // Скрываем служебные колонки
            string[] hiddenColumns = {
                "client_id", "clent_status_id", "address_id",
                "account_id", "city", "street", "house", "entrance"
            };
            foreach (string col in hiddenColumns)
            {
                if (dataGridViewClients.Columns.Contains(col))
                {
                    dataGridViewClients.Columns[col].Visible = false;
                }
            }

            // Устанавливаем заголовки и порядок колонок
            int index = 0;

            if (dataGridViewClients.Columns.Contains("last_name"))
            {
                dataGridViewClients.Columns["last_name"].HeaderText = "Фамилия";
                dataGridViewClients.Columns["last_name"].FillWeight = 2;
                dataGridViewClients.Columns["last_name"].DisplayIndex = index++;
                dataGridViewClients.Columns["last_name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (dataGridViewClients.Columns.Contains("first_name"))
            {
                dataGridViewClients.Columns["first_name"].HeaderText = "Имя";
                dataGridViewClients.Columns["first_name"].FillWeight = 2;
                dataGridViewClients.Columns["first_name"].DisplayIndex = index++;
                dataGridViewClients.Columns["first_name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (dataGridViewClients.Columns.Contains("patronymic"))
            {
                dataGridViewClients.Columns["patronymic"].HeaderText = "Отчество";
                dataGridViewClients.Columns["patronymic"].FillWeight = 2;
                dataGridViewClients.Columns["patronymic"].DisplayIndex = index++;
                dataGridViewClients.Columns["patronymic"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (dataGridViewClients.Columns.Contains("phone_number"))
            {
                dataGridViewClients.Columns["phone_number"].HeaderText = "Телефон";
                dataGridViewClients.Columns["phone_number"].FillWeight = 2;
                dataGridViewClients.Columns["phone_number"].DisplayIndex = index++;
                dataGridViewClients.Columns["phone_number"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (dataGridViewClients.Columns.Contains("status_name"))
            {
                dataGridViewClients.Columns["status_name"].HeaderText = "Статус";
                dataGridViewClients.Columns["status_name"].FillWeight = 1;
                dataGridViewClients.Columns["status_name"].DisplayIndex = index++;
                dataGridViewClients.Columns["status_name"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (dataGridViewClients.Columns.Contains("address_info"))
            {
                dataGridViewClients.Columns["address_info"].HeaderText = "Адрес";
                dataGridViewClients.Columns["address_info"].FillWeight = 4;
                dataGridViewClients.Columns["address_info"].DisplayIndex = index++;
                dataGridViewClients.Columns["address_info"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (dataGridViewClients.Columns.Contains("login"))
            {
                dataGridViewClients.Columns["login"].HeaderText = "Логин";
                dataGridViewClients.Columns["login"].FillWeight = 2;
                dataGridViewClients.Columns["login"].DisplayIndex = index++;
                dataGridViewClients.Columns["login"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            // Добавляем колонку пароля
            if (dataGridViewClients.Columns.Contains("password"))
            {
                dataGridViewClients.Columns["password"].HeaderText = "Пароль";
                dataGridViewClients.Columns["password"].FillWeight = 2;
                dataGridViewClients.Columns["password"].DisplayIndex = index++;
                dataGridViewClients.Columns["password"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            // Настройка внешнего вида (только один раз)
            dataGridViewClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewClients.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dataGridViewClients.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewClients.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewClients.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewClients.ColumnHeadersHeight = 40;
            dataGridViewClients.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
            dataGridViewClients.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 249);
            dataGridViewClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewClients.ReadOnly = true;
            dataGridViewClients.RowHeadersVisible = false;
            dataGridViewClients.AllowUserToAddRows = false;
            dataGridViewClients.AllowUserToDeleteRows = false;

            columnsConfigured = true;
            dataGridViewClients.ResumeLayout();
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

                // Загружаем адрес
                if (row.Cells["address_info"].Value != DBNull.Value)
                    cmbAddress.Text = row.Cells["address_info"].Value.ToString();

                if (row.Cells["clent_status_id"].Value != DBNull.Value)
                    comboBoxStatus.SelectedValue = Convert.ToInt32(row.Cells["clent_status_id"].Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных клиента: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetOrCreateAddressId(string addressText)
        {
            if (string.IsNullOrWhiteSpace(addressText))
                return -1;

            // Проверяем, существует ли уже такой адрес
            foreach (DataRow row in allAddresses.Rows)
            {
                if (row["full_address"].ToString().Equals(addressText, StringComparison.OrdinalIgnoreCase))
                    return Convert.ToInt32(row["id"]);
            }

            // Если адрес не найден, парсим его и добавляем
            string[] parts = addressText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string city = parts.Length > 0 ? parts[0].Trim() : "Воронеж";
            string street = parts.Length > 1 ? parts[1].Trim() : "";
            string house = "";
            string entrance = "";

            // Парсим дом и подъезд
            for (int i = 2; i < parts.Length; i++)
            {
                string part = parts[i].Trim();
                if (part.ToLower().Contains("подъезд") || part.ToLower().Contains("под."))
                {
                    entrance = part.Replace("подъезд", "").Replace("под.", "").Replace("под", "").Trim();
                }
                else if (string.IsNullOrEmpty(house))
                {
                    house = part.Replace("д.", "").Replace("д", "").Replace("дом", "").Trim();
                }
            }

            // Проверка, что город - Воронеж
            if (!city.Equals("Воронеж", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Город должен быть Воронеж", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            if (string.IsNullOrEmpty(street) || string.IsNullOrEmpty(house))
            {
                MessageBox.Show("Не удалось распознать адрес. Используйте формат: Воронеж, Улица, д. Номер, подъезд 1",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            // Добавляем адрес
            int newAddressId = adminClient.AddAddress(city, street, house, entrance);
            if (newAddressId != -1)
            {
                // Обновляем список адресов
                LoadAllAddresses();
                SetupAutoComplete();
                MessageBox.Show($"Новый адрес добавлен: {city}, {street}, д.{house}" +
                                (string.IsNullOrEmpty(entrance) ? "" : $", подъезд {entrance}"),
                                "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return newAddressId;
        }

        private string GenerateRandomPassword(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private bool ValidateForm()
        {
            // Проверка имени
            if (string.IsNullOrWhiteSpace(textBoxFirstName.Text))
            {
                MessageBox.Show("Введите имя клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxFirstName.Focus();
                return false;
            }
            if (!IsOnlyLetters(textBoxFirstName.Text))
            {
                MessageBox.Show("Имя должно содержать только буквы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxFirstName.Focus();
                return false;
            }

            // Проверка фамилии
            if (string.IsNullOrWhiteSpace(textBoxLastName.Text))
            {
                MessageBox.Show("Введите фамилию клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLastName.Focus();
                return false;
            }
            if (!IsOnlyLetters(textBoxLastName.Text))
            {
                MessageBox.Show("Фамилия должна содержать только буквы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLastName.Focus();
                return false;
            }

            // Проверка отчества
            if (!string.IsNullOrWhiteSpace(textBoxPatronymic.Text) && !IsOnlyLetters(textBoxPatronymic.Text))
            {
                MessageBox.Show("Отчество должно содержать только буквы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPatronymic.Focus();
                return false;
            }

            // Проверка телефона
            if (string.IsNullOrWhiteSpace(textBoxPhone.Text))
            {
                MessageBox.Show("Введите телефон клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPhone.Focus();
                return false;
            }
            if (!IsValidPhone(textBoxPhone.Text))
            {
                MessageBox.Show("Введите корректный номер телефона в формате: +79001234567", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPhone.Focus();
                return false;
            }

            // Проверка адреса
            if (string.IsNullOrEmpty(cmbAddress.Text))
            {
                MessageBox.Show("Введите адрес", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbAddress.Focus();
                return false;
            }
            if (!IsValidAddress(cmbAddress.Text))
            {
                MessageBox.Show("Адрес должен начинаться с города Воронеж", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbAddress.Focus();
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

        private bool IsValidAddress(string address)
        {
            string trimmedAddress = address.Trim();
            return trimmedAddress.StartsWith("Воронеж", StringComparison.OrdinalIgnoreCase);
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
            cmbAddress.Text = "";
            if (comboBoxStatus.Items.Count > 0) comboBoxStatus.SelectedIndex = 0;
            groupBoxClientData.Text = "Данные клиента";
        }

        // ---------- ОБРАБОТЧИКИ СОБЫТИЙ ----------

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            groupBoxClientData.Visible = true;
            groupBoxClientData.Text = "Добавление нового клиента";

            label7.Visible = false;
            textBoxAccountId.Visible = false;

            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string generatedLogin = $"client_{timestamp}";
            string generatedPassword = GenerateRandomPassword(8);

            int newAccountId = adminClient.CreateAccountForClient(generatedLogin, generatedPassword);
            if (newAccountId != -1)
            {
                textBoxAccountId.Text = newAccountId.ToString();
                textBoxAccountId.ReadOnly = true;
                textBoxAccountId.BackColor = Color.LightGreen;

                MessageBox.Show($"Создан аккаунт:\nЛогин: {generatedLogin}\nПароль: {generatedPassword}\n\n" +
                                "Сохраните эти данные для передачи клиенту.",
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
            if (dataGridViewClients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите клиента для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            groupBoxClientData.Visible = true;
            groupBoxClientData.Text = "Редактирование клиента";

            label7.Visible = false;
            textBoxAccountId.Visible = false;

            DataGridViewRow selectedRow = dataGridViewClients.SelectedRows[0];
            LoadClientToForm(selectedRow);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                bool success;
                string addressText = cmbAddress.Text.Trim();

                int addressId = GetOrCreateAddressId(addressText);
                if (addressId == -1)
                {
                    MessageBox.Show("Не удалось определить адрес. Проверьте правильность ввода.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(textBoxID.Text)) // добавление
                {
                    int accountId = Convert.ToInt32(textBoxAccountId.Text.Trim());
                    if (!adminClient.AccountExists(accountId))
                    {
                        MessageBox.Show("Аккаунт не найден. Создайте нового клиента через кнопку 'Добавить'",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                else // обновление
                {
                    int clientId = Convert.ToInt32(textBoxID.Text);
                    int oldStatusId = adminClient.GetClientStatusId(clientId);
                    int selectedStatusId = (int)comboBoxStatus.SelectedValue;

                    success = adminClient.UpdateClient(
                        clientId,
                        textBoxFirstName.Text.Trim(),
                        textBoxLastName.Text.Trim(),
                        textBoxPatronymic.Text.Trim(),
                        textBoxPhone.Text.Trim(),
                        selectedStatusId,
                        addressId,
                        Convert.ToInt32(textBoxAccountId.Text.Trim())
                    );

                    // Если статус изменился на "Заблокирован" (2), добавляем в blacklist
                    if (success && oldStatusId != 2 && selectedStatusId == 2)
                    {
                        DataTable reasons = adminClient.GetBlockReasons();
                        if (reasons.Rows.Count > 0)
                        {
                            using (var blockDialog = new BlockUserDialog(reasons))
                            {
                                if (blockDialog.ShowDialog() == DialogResult.OK)
                                {
                                    adminClient.BlockClient(clientId, blockDialog.SelectedReasonId,
                                        blockDialog.StartDate, blockDialog.EndDate);
                                }
                            }
                        }
                    }
                    // Если статус изменился с "Заблокирован" на другой, убираем из blacklist
                    else if (success && oldStatusId == 2 && selectedStatusId != 2)
                    {
                        adminClient.UnblockClient(clientId);
                    }
                }

                if (success)
                {
                    MessageBox.Show(string.IsNullOrEmpty(textBoxID.Text) ? "Клиент успешно добавлен" : "Данные клиента успешно обновлены",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    groupBoxClientData.Visible = false;
                    ClearForm();
                    LoadClients();
                    LoadAllAddresses();
                    SetupAutoComplete();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                if (originalClientsData != null)
                {
                    dataGridViewClients.DataSource = null;
                    dataGridViewClients.DataSource = originalClientsData;
                    // НЕ вызываем ConfigureDataGridView() - настройки сохраняются
                }
                return;
            }

            DataView dv = new DataView(originalClientsData);
            dv.RowFilter = $"first_name LIKE '%{searchText}%' OR " +
                           $"last_name LIKE '%{searchText}%' OR " +
                           $"patronymic LIKE '%{searchText}%' OR " +
                           $"phone_number LIKE '%{searchText}%'";

            dataGridViewClients.DataSource = null;
            dataGridViewClients.DataSource = dv;
            // НЕ вызываем ConfigureDataGridView() - настройки сохраняются
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadClients();
            LoadComboBoxData();
            LoadAllAddresses();
            SetupAutoComplete();
            ClearForm();
            groupBoxClientData.Visible = false;
            // Убираем лишнее сообщение
            // MessageBox.Show("Данные обновлены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                e.Handled = true;
        }

        private void textBoxAccountId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) buttonSearch_Click(sender, e);
        }

        private void dataGridViewClients_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                groupBoxClientData.Visible = true;
                groupBoxClientData.Text = "Редактирование клиента";

                label7.Visible = false;
                textBoxAccountId.Visible = false;

                DataGridViewRow row = dataGridViewClients.Rows[e.RowIndex];
                LoadClientToForm(row);
            }
        }
    }
}