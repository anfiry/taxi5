using Npgsql;
using System;
using System.Data;
using System.Windows.Forms;

namespace taxi4
{
    public partial class RegistrationMenu : Form
    {
        public RegistrationMenu()
        {
            InitializeComponent();
            LoadAddresses();
            SetupAddressAutoComplete();
        }

        

        private bool back = false;
        private string connectionString = "Server=localhost;Port=5432;Database=taxi4;User Id=postgres;Password=123";
        private string currentPhoneNumber;
        private string currentVerificationCode;
        private DataTable allAddresses;


        public void OnClosed()
        {
            if (back)
            { back = false; }
            else { Application.Exit(); }
        }

        // Загрузка всех адресов из БД
        private void LoadAddresses()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        CONCAT(city, ', ', street, ', д.', house,
                               CASE WHEN entrance IS NOT NULL AND entrance != '' 
                                    THEN ', подъезд ' || entrance ELSE '' END) AS full_address,
                        address_id,
                        city,
                        street,
                        house,
                        entrance
                    FROM address
                    ORDER BY city, street, house";
                using (var adapter = new NpgsqlDataAdapter(query, conn))
                {
                    allAddresses = new DataTable();
                    adapter.Fill(allAddresses);
                }
            }
        }

        // Настройка автодополнения для ComboBox
        private void SetupAddressAutoComplete()
        {
            var autoCompleteList = new AutoCompleteStringCollection();
            foreach (DataRow row in allAddresses.Rows)
            {
                autoCompleteList.Add(row["full_address"].ToString());
            }

            cmbAddress.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbAddress.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbAddress.AutoCompleteCustomSource = autoCompleteList;
        }

        // Кнопка "Получить код"
        private void btnSendCode_Click(object sender, EventArgs e)
        {
            string phone_number = txtPhone.Text.Trim();

            if (phone_number.Length != 12 || !phone_number.StartsWith("+79"))
            {
                MessageBox.Show("Введите корректный номер телефона (12 символов, начинается с +79)",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string checkQuery = "SELECT COUNT(*) FROM client WHERE phone_number = @phone_number";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(checkQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@phone_number", phone_number);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count > 0)
                        {
                            MessageBox.Show("Этот номер телефона уже зарегистрирован",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }

                currentVerificationCode = new Random().Next(100000, 999999).ToString();
                currentPhoneNumber = phone_number;

                MessageBox.Show($"Код подтверждения: {currentVerificationCode}\nДля номера: {phone_number}",
                    "Код подтверждения",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtVerificationCode.Enabled = true;
                btnVerifyCode.Enabled = true;
                txtVerificationCode.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка проверки телефона: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Кнопка "Подтвердить код"
        private void btnVerifyCode_Click(object sender, EventArgs e)
        {
            string enteredCode = txtVerificationCode.Text.Trim();

            if (enteredCode == currentVerificationCode)
            {
                MessageBox.Show("Номер телефона подтвержден", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtFirstName.Enabled = true;
                txtLastName.Enabled = true;
                txtPatronymic.Enabled = true;
                txtLogin.Enabled = true;
                txtPassword.Enabled = true;
                txtConfirmPassword.Enabled = true;
                cmbAddress.Enabled = true;
                btnRegister.Enabled = true;

                txtFirstName.Focus();
            }
            else
            {
                MessageBox.Show("Неверный код", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtVerificationCode.SelectAll();
                txtVerificationCode.Focus();
            }
        }

        // Кнопка "Зарегистрироваться"
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Проверки
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Введите имя", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Введите фамилию", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLogin.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Введите пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Focus();
                txtConfirmPassword.SelectAll();
                return;
            }

            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Пароль должен быть не менее 6 символов", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                txtPassword.SelectAll();
                return;
            }

            // Проверка адреса
            string fullAddress = cmbAddress.Text.Trim();
            if (string.IsNullOrWhiteSpace(fullAddress))
            {
                MessageBox.Show("Введите адрес", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAddress.Focus();
                return;
            }

            // Разбор адреса
            string city = "";
            string street = "";
            string house = "";
            string entrance = "";

            try
            {
                string[] parts = fullAddress.Split(',');
                if (parts.Length >= 3)
                {
                    city = parts[0].Trim();
                    street = parts[1].Trim();

                    string housePart = parts[2].Trim();
                    if (housePart.StartsWith("д.") || housePart.StartsWith("д"))
                    {
                        house = housePart.Replace("д.", "").Replace("д", "").Trim();
                    }
                    else
                    {
                        house = housePart;
                    }

                    if (parts.Length >= 4 && parts[3].Trim().Contains("подъезд"))
                    {
                        entrance = parts[3].Trim().Replace("подъезд", "").Trim();
                    }
                }
                else
                {
                    MessageBox.Show("Неверный формат адреса. Используйте формат: Город, улица, д. номер, подъезд номер",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при разборе адреса: {ex.Message}\n\nИспользуйте формат: Город, улица, д. номер, подъезд номер",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // 1. Проверка логина
                            string checkLoginQuery = "SELECT COUNT(*) FROM account WHERE login = @login";
                            using (NpgsqlCommand checkCmd = new NpgsqlCommand(checkLoginQuery, connection))
                            {
                                checkCmd.Transaction = transaction;
                                checkCmd.Parameters.AddWithValue("@login", txtLogin.Text);
                                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                                if (count > 0)
                                {
                                    MessageBox.Show("Этот логин уже занят. Пожалуйста, выберите другой.",
                                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    txtLogin.Focus();
                                    txtLogin.SelectAll();
                                    return;
                                }
                            }

                            // 2. Регистрация в таблице account
                            string accountQuery = @"
                                INSERT INTO account 
                                (role_id, login, password, confirmation) 
                                VALUES 
                                (2, @login, @password, true) 
                                RETURNING account_id";

                            int accountId;

                            using (NpgsqlCommand cmd = new NpgsqlCommand(accountQuery, connection))
                            {
                                cmd.Transaction = transaction;
                                cmd.Parameters.AddWithValue("@login", txtLogin.Text);
                                cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                                accountId = Convert.ToInt32(cmd.ExecuteScalar());
                            }

                            // 3. СОЗДАНИЕ АДРЕСА
                            int addressId;
                            string addressQuery = @"
                                INSERT INTO address 
                                (city, street, house, entrance) 
                                VALUES 
                                (@city, @street, @house, @entrance) 
                                RETURNING address_id";

                            using (NpgsqlCommand addressCmd = new NpgsqlCommand(addressQuery, connection))
                            {
                                addressCmd.Transaction = transaction;
                                addressCmd.Parameters.AddWithValue("@city", city);
                                addressCmd.Parameters.AddWithValue("@street", street);
                                addressCmd.Parameters.AddWithValue("@house", house);
                                addressCmd.Parameters.AddWithValue("@entrance", string.IsNullOrEmpty(entrance) ? (object)DBNull.Value : entrance);

                                addressId = Convert.ToInt32(addressCmd.ExecuteScalar());
                            }

                            // 4. Регистрация в таблице client
                            string clientQuery = @"
                                INSERT INTO client 
                                (clent_status_id, account_id, address_id, first_name, last_name, patronymic, phone_number) 
                                VALUES 
                                (1, @account_id, @address_id, @first_name, @last_name, @patronymic, @phone_number) 
                                RETURNING client_id";

                            int clientId;

                            using (NpgsqlCommand clientCmd = new NpgsqlCommand(clientQuery, connection))
                            {
                                clientCmd.Transaction = transaction;
                                clientCmd.Parameters.AddWithValue("@account_id", accountId);
                                clientCmd.Parameters.AddWithValue("@address_id", addressId);
                                clientCmd.Parameters.AddWithValue("@first_name", txtFirstName.Text);
                                clientCmd.Parameters.AddWithValue("@last_name", txtLastName.Text);

                                if (!string.IsNullOrWhiteSpace(txtPatronymic.Text))
                                    clientCmd.Parameters.AddWithValue("@patronymic", txtPatronymic.Text);
                                else
                                    clientCmd.Parameters.AddWithValue("@patronymic", DBNull.Value);

                                clientCmd.Parameters.AddWithValue("@phone_number", currentPhoneNumber);

                                clientId = Convert.ToInt32(clientCmd.ExecuteScalar());
                            }

                            transaction.Commit();

                            MessageBox.Show(
                                $"Регистрация успешна!\n\n" +
                                $"ID клиента: {clientId}\n" +
                                $"Логин: {txtLogin.Text}\n" +
                                $"Телефон: {currentPhoneNumber}\n" +
                                $"Имя: {txtFirstName.Text} {txtLastName.Text} " +
                                (string.IsNullOrWhiteSpace(txtPatronymic.Text) ? "" : " " + txtPatronymic.Text) +
                                $"\nАдрес: {fullAddress}",
                                "Регистрация завершена",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            LoginForm loginForm = new LoginForm();
                            loginForm.Show();
                            this.Hide();
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                transaction.Rollback();
                            }
                            catch (Exception rollbackEx)
                            {
                                MessageBox.Show($"Ошибка отката транзакции: {rollbackEx.Message}",
                                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            if (ex.Message.Contains("client_status_id"))
                            {
                                MessageBox.Show($"Ошибка: столбец 'client_status_id' не найден. Проверьте структуру таблицы 'client' в базе данных.",
                                    "Ошибка базы данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                MessageBox.Show($"Ошибка при регистрации: {ex.Message}", "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка регистрации: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Кнопка "Отмена"
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            back = true;
            this.Close();
        }
    }
}