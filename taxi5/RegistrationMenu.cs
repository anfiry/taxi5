using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;
using taxi4.Services;

namespace taxi4
{
    public partial class RegistrationMenu : Form
    {
        public RegistrationMenu()
        {
            InitializeComponent();

            // Загружаем строку подключения из конфига (если есть)
            try
            {
                string configConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
                if (!string.IsNullOrEmpty(configConnectionString))
                {
                    connectionString = configConnectionString;
                }
            }
            catch
            {
                // Оставляем значение по умолчанию
            }

            InitializeTelegramBot();
        }

        private string connectionString = "Server=localhost;Port=5432;Database=taxi4;User Id=postgres;Password=123";
        private string currentPhoneNumber;
        private string currentVerificationCode;
        private TelegramBotService _telegramBot;

        private void InitializeTelegramBot()
        {
            try
            {
                // Используйте ключи из App.config, а не сами значения
                string botToken = ConfigurationManager.AppSettings["TelegramBotToken"];
                string chatIdStr = ConfigurationManager.AppSettings["TelegramChatId"];

                if (!string.IsNullOrEmpty(botToken) &&
                    !string.IsNullOrEmpty(chatIdStr) &&
                    long.TryParse(chatIdStr, out long chatId))
                {
                    _telegramBot = new TelegramBotService(botToken, chatId);
                }
                else
                {
                    // Если настройки не найдены, используем демо-режим
                    _telegramBot = null;
                    Console.WriteLine("Telegram бот не настроен. Используется демо-режим.");
                }
            }
            catch
            {
                _telegramBot = null;
            }
        }

        // Кнопка "Получить код"
        private async void btnSendCode_Click(object sender, EventArgs e)
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

                // Пытаемся отправить код через Telegram
                bool telegramSent = false;
                if (_telegramBot != null)
                {
                    telegramSent = await _telegramBot.SendVerificationCodeAsync(phone_number, currentVerificationCode);
                }

                if (telegramSent)
                {
                    MessageBox.Show($"Код подтверждения отправлен в Telegram!\n\n" +
                                  $"Код: {currentVerificationCode}",
                        "Код отправлен",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    // Fallback: показываем код в MessageBox
                    MessageBox.Show($"Код подтверждения: {currentVerificationCode}\n(Telegram недоступен, демо-режим)",
                        "Код отправлен",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }

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
        private async void btnRegister_Click(object sender, EventArgs e)
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

                            // 3. Регистрация в таблице client
                            // ВАЖНО: В запросе пропущено client_status_id, исправлено
                            string clientQuery = @"
                                INSERT INTO client 
                                (client_status_id, account_id, address_id, first_name, last_name, patronymic, phone_number) 
                                VALUES 
                                (1, @account_id, @address_id, @first_name, @last_name, @patronymic, @phone_number) 
                                RETURNING client_id";

                            int clientId;

                            using (NpgsqlCommand clientCmd = new NpgsqlCommand(clientQuery, connection))
                            {
                                clientCmd.Transaction = transaction;
                                clientCmd.Parameters.AddWithValue("@account_id", accountId);
                                clientCmd.Parameters.AddWithValue("@address_id", DBNull.Value);
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

                            // 4. Фиксируем транзакцию
                            transaction.Commit();

                            // Отправляем уведомление в Telegram о регистрации
                            if (_telegramBot != null)
                            {
                                try
                                {
                                    await _telegramBot.SendRegistrationNotificationAsync(
                                        currentPhoneNumber,
                                        txtFirstName.Text,
                                        txtLastName.Text,
                                        txtLogin.Text
                                    );
                                }
                                catch
                                {
                                    // Игнорируем ошибки отправки в Telegram
                                }
                            }

                            MessageBox.Show(
                                $"Регистрация успешна!\n\n" +
                                $"ID клиента: {clientId}\n" +
                                $"Логин: {txtLogin.Text}\n" +
                                $"Телефон: {currentPhoneNumber}\n" +
                                $"Имя: {txtFirstName.Text} {txtLastName.Text} " +
                                (string.IsNullOrWhiteSpace(txtPatronymic.Text) ? "" : " " + txtPatronymic.Text),
                                "Регистрация завершена",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            this.DialogResult = DialogResult.OK;
                            this.Close();
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

                            throw new Exception($"Ошибка при регистрации: {ex.Message}");
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