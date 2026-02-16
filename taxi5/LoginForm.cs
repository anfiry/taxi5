using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaxiClientApp;

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
                SELECT role_id 
                FROM account 
                WHERE login = @login 
                AND password = @password 
                AND confirmation = true";

                    using (var cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@login", login);
                        cmd.Parameters.AddWithValue("@password", password);

                        var role = cmd.ExecuteScalar();

                        if (role != null)
                        {
                            // Ваш существующий код
                            if (role.ToString() == "1")
                            {
                                AdminMenu adminMenu = new AdminMenu();
                                adminMenu.Role = "Администратор";
                                adminMenu.Closed += (s, args) => Close();
                                adminMenu.Show();
                                Hide();
                            }
                            else if (role.ToString() == "2")
                            {
                                MainForm clientMenu = new MainForm();
                                clientMenu.Role = "Клиент";
                                clientMenu.Closed += (s, args) => Close();
                                clientMenu.Show();
                                Hide();
                            }
                            else if (role.ToString() == "3")
                            {
                                DriverMenu driverMenu = new DriverMenu();
                                driverMenu.Role = "Водитель";
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
