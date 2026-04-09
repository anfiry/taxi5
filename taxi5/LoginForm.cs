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
                                    adminMenu.Closed += (s, args) => adminMenu.OnClosed();

                                    adminMenu.Show();
                                    this.Hide();
                                }
                                else if (roleId == 2) // клиент
                                {
                                    ClientMenu clientMenu = new ClientMenu(accountId); // ← передаём accountId в конструктор
                                    clientMenu.Role = "Клиент";
                                    clientMenu.UserLogin = login;
                                    clientMenu.Closed += (s, args) => clientMenu.OnClosed();

                                    clientMenu.Show();
                                    this.Hide();
                                }
                                else if (roleId == 3) // водитель
                                {
                                    DriverMenu driverMenu = new DriverMenu(accountId); // ← передаём accountId в конструктор
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

        private void RegButton_Click(object sender, EventArgs e)
        {
            RegistrationMenu registrationMenu = new RegistrationMenu();
            registrationMenu.Closed += (s, args) => registrationMenu.OnClosed();
            registrationMenu.Show();
            this.Hide();
        }
    }
}