using System;
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
        private DataTable allAddresses;      // для автодополнения

        public AdminClientForm()
        {
            InitializeComponent();
            adminClient = new AdminClient();
            LoadClients();
            LoadComboBoxData();
            LoadAllAddresses();
            ClearForm();
            SetupAutoComplete();
            SetupPlaceholderTexts(); // если нужны

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

        private void SetupPlaceholderTexts()
        {
            // Можно добавить плейсхолдер для cmbAddress, если нужно
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

        private void LoadAllAddresses()
        {
            allAddresses = adminClient.GetAddresses();
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
            if (dataGridViewClients.Columns.Count == 0) return;


            if (dataGridViewClients.Columns["client_id"] != null)
                dataGridViewClients.Columns["client_id"].Visible = false;

            // Используем SetColumnFill для всех колонок (с весами)
            SetColumn("client_id", "ID",1,  true);
            SetColumnFill("last_name", "Фамилия", 2, true);
            SetColumnFill("first_name", "Имя", 2, true);
            SetColumnFill("patronymic", "Отчество", 2, false);
            SetColumnFill("phone_number", "Телефон", 2, false);
            SetColumnFill("status_name", "Статус", 1, false);
            SetColumnFill("address_info", "Адрес", 4, false);
            SetColumnFill("login", "Логин", 2, false);

            string[] hiddenColumns = { "client_id", "clent_status_id", "address_id", "account_id", "city", "street", "house", "entrance" };
            foreach (string col in hiddenColumns)
                if (dataGridViewClients.Columns.Contains(col))
                    dataGridViewClients.Columns[col].Visible = false;

            int index = 0;
            string[] order = { "last_name", "first_name", "patronymic", "phone_number",
                       "status_name", "address_info", "login" };
            foreach (string colName in order)
                if (dataGridViewClients.Columns[colName] != null)
                    dataGridViewClients.Columns[colName].DisplayIndex = index++;

            // Включаем режим заполнения
            dataGridViewClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Стиль заголовков
            dataGridViewClients.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dataGridViewClients.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewClients.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewClients.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewClients.ColumnHeadersHeight = 40;

            // Стиль строк
            dataGridViewClients.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
            dataGridViewClients.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 249);
            dataGridViewClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewClients.ReadOnly = true;
            dataGridViewClients.RowHeadersVisible = false;
        }



        private void SetColumnFill(string columnName, string headerText, int fillWeight, bool frozen)
        {
            if (dataGridViewClients.Columns[columnName] != null)
            {
                dataGridViewClients.Columns[columnName].HeaderText = headerText;
                dataGridViewClients.Columns[columnName].FillWeight = fillWeight;
                dataGridViewClients.Columns[columnName].Frozen = frozen;
                dataGridViewClients.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void SetColumn(string columnName, string headerText, int width, bool frozen)
        {
            if (dataGridViewClients.Columns[columnName] != null)
            {
                dataGridViewClients.Columns[columnName].HeaderText = headerText;
                dataGridViewClients.Columns[columnName].Width = width;
                dataGridViewClients.Columns[columnName].Frozen = frozen;
                dataGridViewClients.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        // Метод для получения или создания адреса
        // Метод для получения или создания адреса (находится в AdminClientForm.cs)
        private int GetOrCreateAddressId(string addressText)
        {
            if (string.IsNullOrWhiteSpace(addressText))
                return -1;

            // Проверяем, существует ли уже такой адрес в таблице (с учётом подъезда)
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
                // Обновляем список адресов для автодополнения
                LoadAllAddresses();
                SetupAutoComplete();
                MessageBox.Show($"Новый адрес добавлен: {city}, {street}, д.{house}" +
                                (string.IsNullOrEmpty(entrance) ? "" : $", подъезд {entrance}"),
                                "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return newAddressId;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            groupBoxClientData.Visible = true;
            groupBoxClientData.Text = "Добавление нового клиента";

            // Показываем поле для ID аккаунта
            label7.Visible = true;
            textBoxAccountId.Visible = true;

            // Генерируем логин и пароль автоматически
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string generatedLogin = $"client_{timestamp}";
            string generatedPassword = GenerateRandomPassword(8);

            int newAccountId = adminClient.CreateAccountForClient(generatedLogin, generatedPassword);
            if (newAccountId != -1)
            {
                textBoxAccountId.Text = newAccountId.ToString();
                textBoxAccountId.ReadOnly = true;
                textBoxAccountId.BackColor = Color.LightGreen;

                // Отображаем сгенерированные логин и пароль (для администратора)
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

        private string GenerateRandomPassword(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
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

            // Скрываем поле аккаунта при редактировании
            label7.Visible = false;
            textBoxAccountId.Visible = false;

            DataGridViewRow selectedRow = dataGridViewClients.SelectedRows[0];
            LoadClientToForm(selectedRow);
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

                if (string.IsNullOrEmpty(textBoxID.Text))
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
                else
                {
                    success = adminClient.UpdateClient(
                        Convert.ToInt32(textBoxID.Text),
                        textBoxFirstName.Text.Trim(),
                        textBoxLastName.Text.Trim(),
                        textBoxPatronymic.Text.Trim(),
                        textBoxPhone.Text.Trim(),
                        (int)comboBoxStatus.SelectedValue,
                        addressId,
                        Convert.ToInt32(textBoxAccountId.Text.Trim())
                    );
                }

                if (success)
                {
                    MessageBox.Show(string.IsNullOrEmpty(textBoxID.Text) ? "Клиент успешно добавлен" : "Данные клиента успешно обновлены",
                        "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Скрываем правую панель после сохранения
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

        // ---------- БЛОКИРОВКА/РАЗБЛОКИРОВКА (без изменений) ----------
        private void buttonBlock_Click(object sender, EventArgs e)
        {
            // ... код без изменений ...
        }

        private void buttonUnblock_Click(object sender, EventArgs e)
        {
            // ... код без изменений ...
        }

        // ---------- ОСТАЛЬНЫЕ МЕТОДЫ ----------
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadClients();
            LoadComboBoxData();
            LoadAllAddresses();
            SetupAutoComplete();
            ClearForm();
            groupBoxClientData.Visible = false;
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
                groupBoxClientData.Visible = true;
                groupBoxClientData.Text = "Редактирование клиента";

                // Скрываем поле аккаунта при редактировании
                label7.Visible = false;
                textBoxAccountId.Visible = false;

                DataGridViewRow row = dataGridViewClients.Rows[e.RowIndex];
                LoadClientToForm(row);
            }
        }

        private bool ValidateForm()
        {
            // Проверка имени (только буквы)
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
                textBoxFirstName.SelectAll();
                return false;
            }

            // Проверка фамилии (только буквы)
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
                MessageBox.Show("Введите телефон клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            // Проверка ID аккаунта (только при добавлении)
            if (string.IsNullOrWhiteSpace(textBoxAccountId.Text) && label7.Visible)
            {
                MessageBox.Show("ID аккаунта отсутствует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!string.IsNullOrEmpty(textBoxAccountId.Text) && label7.Visible && !int.TryParse(textBoxAccountId.Text, out _))
            {
                MessageBox.Show("ID аккаунта должен быть числом", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxAccountId.Focus();
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
                cmbAddress.SelectAll();
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

        // Вспомогательные методы для проверок
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
            // Убираем все нецифровые символы для проверки
            string digits = new string(phone.Where(char.IsDigit).ToArray());

            // Проверяем, что номер начинается с 79 и имеет 11 цифр (без +)
            if (digits.Length != 11) return false;
            if (!digits.StartsWith("79")) return false;

            return true;
        }

        private bool IsValidAddress(string address)
        {
            // Проверяем, что адрес начинается с "Воронеж" (без учёта регистра)
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



    }
}