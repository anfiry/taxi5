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
        private bool columnsConfigured = false;
        private bool back = false;

        public AdminClientForm()
        {
            InitializeComponent();
            adminClient = new AdminClient();

            originalClientsData = new DataTable();

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

            // Скрываем поле аккаунта
            label7.Visible = false;
            textBoxAccountId.Visible = false;
        }

        public void OnClosed()
        {
            if (back)
            { back = false; }
            else { Application.Exit(); }
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
        private void LoadAllAddresses()
        {
            allAddresses = adminClient.GetAddresses();
        }

        // ---------- ЗАГРУЗКА КЛИЕНТОВ ----------
        private void LoadClients()
        {
            try
            {
                clientsData = adminClient.GetClients();
                originalClientsData = clientsData.Copy(); // ← СОХРАНЯЕМ КОПИЮ
                dataGridViewClients.DataSource = clientsData;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки клиентов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- НАСТРОЙКА ТАБЛИЦЫ ----------
        private void ConfigureDataGridView()
        {
            if (columnsConfigured) return;
            if (dataGridViewClients.Columns.Count == 0) return;

            dataGridViewClients.SuspendLayout();

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

            // Устанавливаем заголовки, порядок и ШИРИНУ колонок
            int index = 0;

            if (dataGridViewClients.Columns.Contains("last_name"))
            {
                dataGridViewClients.Columns["last_name"].HeaderText = "Фамилия";
                dataGridViewClients.Columns["last_name"].DisplayIndex = index++;
                dataGridViewClients.Columns["last_name"].Width = 150;
            }

            if (dataGridViewClients.Columns.Contains("first_name"))
            {
                dataGridViewClients.Columns["first_name"].HeaderText = "Имя";
                dataGridViewClients.Columns["first_name"].DisplayIndex = index++;
                dataGridViewClients.Columns["first_name"].Width = 150;
            }

            if (dataGridViewClients.Columns.Contains("patronymic"))
            {
                dataGridViewClients.Columns["patronymic"].HeaderText = "Отчество";
                dataGridViewClients.Columns["patronymic"].DisplayIndex = index++;
                dataGridViewClients.Columns["patronymic"].Width = 150;
            }

            if (dataGridViewClients.Columns.Contains("phone_number"))
            {
                dataGridViewClients.Columns["phone_number"].HeaderText = "Телефон";
                dataGridViewClients.Columns["phone_number"].DisplayIndex = index++;
                dataGridViewClients.Columns["phone_number"].Width = 150;
            }

            if (dataGridViewClients.Columns.Contains("status_name"))
            {
                dataGridViewClients.Columns["status_name"].HeaderText = "Статус";
                dataGridViewClients.Columns["status_name"].DisplayIndex = index++;
                dataGridViewClients.Columns["status_name"].Width = 120;
            }

            if (dataGridViewClients.Columns.Contains("full_address"))  // ← ИСПРАВЛЕНО: full_address
            {
                dataGridViewClients.Columns["full_address"].HeaderText = "Адрес";
                dataGridViewClients.Columns["full_address"].DisplayIndex = index++;
                dataGridViewClients.Columns["full_address"].Width = 450;  // ← ШИРОКАЯ КОЛОНКА
            }

            if (dataGridViewClients.Columns.Contains("login"))
            {
                dataGridViewClients.Columns["login"].HeaderText = "Логин";
                dataGridViewClients.Columns["login"].DisplayIndex = index++;
                dataGridViewClients.Columns["login"].Width = 150;
            }

            if (dataGridViewClients.Columns.Contains("password"))
            {
                dataGridViewClients.Columns["password"].HeaderText = "Пароль";
                dataGridViewClients.Columns["password"].DisplayIndex = index++;
                dataGridViewClients.Columns["password"].Width = 150;
            }

            // Настройка внешнего вида
            dataGridViewClients.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridViewClients.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dataGridViewClients.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewClients.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewClients.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewClients.ColumnHeadersHeight = 45;
            dataGridViewClients.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
            dataGridViewClients.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 249);
            dataGridViewClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewClients.ReadOnly = true;
            dataGridViewClients.RowHeadersVisible = false;
            dataGridViewClients.AllowUserToAddRows = false;
            dataGridViewClients.AllowUserToDeleteRows = false;
            dataGridViewClients.ScrollBars = ScrollBars.Both;

            columnsConfigured = true;
            dataGridViewClients.ResumeLayout();
        }

        // ---------- МЕТОД ДЛЯ ОБНОВЛЕНИЯ ДАННЫХ БЕЗ СБРОСА НАСТРОЕК ----------
        private void RefreshDataGridView(DataTable data)
        {
            dataGridViewClients.DataSource = null;
            dataGridViewClients.DataSource = data;
            // Настройки колонок НЕ сбрасываются, так как columnsConfigured = true
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

                // Загружаем адрес - ИСПРАВЛЕНО: full_address
                if (row.Cells["full_address"].Value != DBNull.Value)
                    cmbAddress.Text = row.Cells["full_address"].Value.ToString();

                if (row.Cells["clent_status_id"].Value != DBNull.Value)
                    comboBoxStatus.SelectedValue = Convert.ToInt32(row.Cells["clent_status_id"].Value);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных клиента: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ... остальные методы (GetOrCreateAddressId, GenerateRandomPassword, ValidateForm, IsOnlyLetters, IsValidPhone, IsValidAddress, ClearForm) остаются без изменений ...

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchText = textBoxSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                // Возвращаем оригинальные данные
                if (originalClientsData != null)
                {
                    RefreshDataGridView(originalClientsData);
                }
                return;
            }

            DataView dv = new DataView(originalClientsData.Copy()); // ← КОПИРУЕМ, ЧТОБЫ НЕ ПОРТИТЬ ОРИГИНАЛ
            dv.RowFilter = $"first_name LIKE '%{searchText}%' OR " +
                           $"last_name LIKE '%{searchText}%' OR " +
                           $"patronymic LIKE '%{searchText}%' OR " +
                           $"phone_number LIKE '%{searchText}%'";

            DataTable filteredData = dv.ToTable();
            RefreshDataGridView(filteredData);
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadClients(); // Перезагружаем с настройкой колонок
            LoadComboBoxData();
            LoadAllAddresses();
            SetupAutoComplete();
            ClearForm();
            groupBoxClientData.Visible = false;
        }

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

        private string GenerateRandomPassword(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(textBoxFirstName.Text))
            {
                MessageBox.Show("Введите имя клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxLastName.Text))
            {
                MessageBox.Show("Введите фамилию клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLastName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxPhone.Text))
            {
                MessageBox.Show("Введите телефон клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPhone.Focus();
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

            // Парсим адрес
            string[] parts = addressText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string city = parts.Length > 0 ? parts[0].Trim() : "Воронеж";
            string street = parts.Length > 1 ? parts[1].Trim() : "";
            string house = "";
            string entrance = "";

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

            if (string.IsNullOrEmpty(street) || string.IsNullOrEmpty(house))
            {
                MessageBox.Show("Не удалось распознать адрес.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            // Добавляем адрес
            return adminClient.AddAddress(city, street, house, entrance);
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

        private void buttonBack_Click(object sender, EventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
            back = true;
            this.Close();
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