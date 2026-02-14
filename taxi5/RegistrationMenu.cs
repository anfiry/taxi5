using Npgsql;
using System;
using System.Windows.Forms;

namespace taxi4
{
    public partial class RegistrationMenu : Form
    {
        public RegistrationMenu()
        {
            InitializeComponent();
        }

        private string connectionString = "Server=localhost;Port=5432;Database=taxi4;User Id=postgres;Password=123";
        private string currentPhoneNumber;
        private string currentVerificationCode;

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

            // Проверка регистрации телефона
            try
            {
                // Используем локальное соединение
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

                // Генерация кода
                currentVerificationCode = new Random().Next(100000, 999999).ToString();
                currentPhoneNumber = phone_number;

                // Показываем код подтверждения в MessageBox
                MessageBox.Show($"Код подтверждения: {currentVerificationCode}\nДля номера: {phone_number}",
                    "Код подтверждения",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Активируем поле для кода
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

                // Активируем поля для регистрации
                txtFirstName.Enabled = true;
                txtLastName.Enabled = true;
                txtPatronymic.Enabled = true;
                txtLogin.Enabled = true;
                txtPassword.Enabled = true;
                txtConfirmPassword.Enabled = true;
                textCity.Enabled = true;
                textStreet.Enabled = true;
                textHouse.Enabled = true;
                textEntrance.Enabled = true;
                btnRegister.Enabled = true;

                // Фокусируемся на первом поле
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

            // Проверка полей адреса
            if (string.IsNullOrWhiteSpace(textCity.Text) ||
                string.IsNullOrWhiteSpace(textStreet.Text) ||
                string.IsNullOrWhiteSpace(textHouse.Text) ||
                string.IsNullOrWhiteSpace(textEntrance.Text))
            {
                MessageBox.Show("Заполните все поля адреса", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Начинаем транзакцию
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

                            // 3. СОЗДАНИЕ АДРЕСА (НОВАЯ ЧАСТЬ)
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
                                addressCmd.Parameters.AddWithValue("@city", textCity.Text.Trim());
                                addressCmd.Parameters.AddWithValue("@street", textStreet.Text.Trim());
                                addressCmd.Parameters.AddWithValue("@house", textHouse.Text.Trim());
                                addressCmd.Parameters.AddWithValue("@entrance", textEntrance.Text.Trim());

                                addressId = Convert.ToInt32(addressCmd.ExecuteScalar());
                            }

                            // 4. Регистрация в таблице client (ИСПРАВЛЕНА ОПЕЧАТКА)
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
                                clientCmd.Parameters.AddWithValue("@address_id", addressId); // Используем созданный адрес
                                clientCmd.Parameters.AddWithValue("@first_name", txtFirstName.Text);
                                clientCmd.Parameters.AddWithValue("@last_name", txtLastName.Text);

                                // Обработка отчества
                                if (!string.IsNullOrWhiteSpace(txtPatronymic.Text))
                                    clientCmd.Parameters.AddWithValue("@patronymic", txtPatronymic.Text);
                                else
                                    clientCmd.Parameters.AddWithValue("@patronymic", DBNull.Value);

                                // Используем номер с "+"
                                clientCmd.Parameters.AddWithValue("@phone_number", currentPhoneNumber);

                                clientId = Convert.ToInt32(clientCmd.ExecuteScalar());
                            }

                            // 5. Фиксируем транзакцию
                            transaction.Commit();

                            MessageBox.Show(
                                $"Регистрация успешна!\n\n" +
                                $"ID клиента: {clientId}\n" +
                                $"Логин: {txtLogin.Text}\n" +
                                $"Телефон: {currentPhoneNumber}\n" +
                                $"Имя: {txtFirstName.Text} {txtLastName.Text} " +
                                (string.IsNullOrWhiteSpace(txtPatronymic.Text) ? "" : " " + txtPatronymic.Text) +
                                $"\nАдрес: {textCity.Text}, {textStreet.Text}, д.{textHouse.Text}, подъезд {textEntrance.Text}",
                                "Регистрация завершена",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            LoginForm loginForm = new LoginForm();
                            loginForm.Show();
                            this.Hide();
                        }
                        catch (Exception ex)
                        {
                            // Откат транзакции при ошибке
                            try
                            {
                                transaction.Rollback();
                            }
                            catch (Exception rollbackEx)
                            {
                                MessageBox.Show($"Ошибка отката транзакции: {rollbackEx.Message}",
                                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            // Проверка на конкретную ошибку
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
            this.Close();
        }
    }
}