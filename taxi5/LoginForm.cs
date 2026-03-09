using Npgsql;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    public partial class LoginForm : Form
    {
        private string connectionString = "Server=localhost;Port=5432;Database=taxi4;User Id=postgres;Password=123";

        public LoginForm()
        {
            InitializeComponent();
            this.PassField.AutoSize = false;
            this.PassField.Size = new Size(this.PassField.Size.Width, 44);
        }

        private void CloseButton_Click(object sender, EventArgs e)
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

                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@password", password);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int roleId = reader.GetInt32(0);
                                int accountId = reader.GetInt32(1);

                                if (roleId == 1) // администратор
                                {
                                    AdminMenu adminMenu = new AdminMenu();
                                    adminMenu.Role = "Администратор";
                                    adminMenu.AccountId = accountId;
                                    adminMenu.UserLogin = login;
                                    adminMenu.Closed += (s, args) => Close();
                                    adminMenu.Show();
                                    Hide();
                                }
                                else if (roleId == 2) // клиент
                                {
                                    // Передаём accountId в конструктор
                                    ClientMenu clientMenu = new ClientMenu(accountId);
                                    clientMenu.Role = "Клиент";
                                    clientMenu.UserLogin = login;
                                    clientMenu.Closed += (s, args) => Close();
                                    clientMenu.Show();
                                    Hide();
                                }
                                else if (roleId == 3) // водитель
                                {
                                    DriverMenu driverMenu = new DriverMenu();
                                    driverMenu.Role = "Водитель";
                                    driverMenu.AccountId = accountId;
                                    driverMenu.UserLogin = login;
                                    driverMenu.Closed += (s, args) => Close();
                                    driverMenu.Show();
                                    Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Неизвестная роль пользователя", "Ошибка");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Неверный логин или пароль", "Ошибка");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegistrationMenu registrationMenu = new RegistrationMenu();
            registrationMenu.Show();
            this.Hide();
        }
    }
}