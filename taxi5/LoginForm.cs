using Npgsql;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    public partial class LoginForm : Form
    {
        private string connectionString = "Server=localhost;Port=5432;Database=taxi4;User Id=postgres;Password=123";
        private bool back = false;
        public LoginForm()
        {
            InitializeComponent();
            this.PassField.AutoSize = false;
            this.PassField.Size = new Size(this.PassField.Size.Width, 44);

            this.Closed += (s, args) => OnClosed();

            this.KeyDown += LoginForm_KeyDown;
            this.KeyPreview = true;
        }

        public void OnClosed()
        {
            if (back)
            { back = false; }
            else { Application.Exit(); }
        }

        private void InButton_Click(object sender, EventArgs e)
        {
            string login = LoginField.Text.Trim();
            string password = PassField.Text.Trim();

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка");
                return;
            }

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT role_id, account_id 
                FROM account 
                WHERE login = @login 
                AND password = @password 
                AND confirmation = true";

                    int roleId = -1;
                    int accountId = -1;

                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                roleId = reader.GetInt32(0);
                                accountId = reader.GetInt32(1);
                            }
                            else
                            {
                                MessageBox.Show("Неверный логин или пароль", "Ошибка");
                                return;
                            }
                        }
                    }

                    // Проверка блокировки для клиентов и водителей
                    string blockedMessage = IsUserBlocked(accountId, roleId);
                    if (!string.IsNullOrEmpty(blockedMessage))
                    {
                        MessageBox.Show(blockedMessage, "Доступ запрещён", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Успешный вход
                    if (roleId == 1) // администратор
                    {
                        AdminMenu adminMenu = new AdminMenu();
                        adminMenu.Role = "Администратор";
                        adminMenu.AccountId = accountId;
                        adminMenu.UserLogin = login;
                        adminMenu.Closed += (s, args) => adminMenu.OnClosed();
                        adminMenu.Show();
                        this.Hide();
                    }
                    else if (roleId == 2) // клиент
                    {
                        ClientMenu clientMenu = new ClientMenu(accountId);
                        clientMenu.Role = "Клиент";
                        clientMenu.UserLogin = login;
                        clientMenu.Closed += (s, args) => clientMenu.OnClosed();
                        clientMenu.Show();
                        this.Hide();
                    }
                    else if (roleId == 3) // водитель
                    {
                        DriverMenu driverMenu = new DriverMenu(accountId);
                        driverMenu.Role = "Водитель";
                        driverMenu.UserLogin = login;
                        driverMenu.Closed += (s, args) => driverMenu.OnClosed();
                        driverMenu.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Неизвестная роль пользователя", "Ошибка");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InButton_Click(sender, e);
            }
        }

        private string IsUserBlocked(int accountId, int roleId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    if (roleId == 2) // Клиент
                    {
                        string query = @"
                    SELECT cs.status_name 
                    FROM client c
                    JOIN clent_status cs ON c.clent_status_id = cs.clent_status_id
                    WHERE c.account_id = @accountId";

                        using (var cmd = new NpgsqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@accountId", accountId);
                            var result = cmd.ExecuteScalar();
                            if (result != null && result.ToString().ToLower() == "заблокирован")
                                return "Ваш аккаунт заблокирован. Обратитесь к администратору для выяснения причин.";
                        }
                    }
                    else if (roleId == 3) // Водитель
                    {
                        string query = @"
                    SELECT ds.status_name 
                    FROM driver d
                    JOIN driver_status ds ON d.driver_status_id = ds.driver_status_id
                    WHERE d.account_id = @accountId";

                        using (var cmd = new NpgsqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@accountId", accountId);
                            var result = cmd.ExecuteScalar();
                            if (result != null && result.ToString().ToLower() == "заблокирован")
                                return "Ваш аккаунт заблокирован. Обратитесь к администратору для выяснения причин.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка проверки блокировки: {ex.Message}");
            }
            return null;
        }

        private void RegButton_Click(object sender, EventArgs e)
        {
            RegistrationMenu registrationMenu = new RegistrationMenu();
            registrationMenu.Closed += (s, args) => registrationMenu.OnClosed();
            registrationMenu.Show();
            this.Hide();
        }
    }
}