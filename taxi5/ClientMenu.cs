using System;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace TaxiClientApp
{
    public partial class MainForm : Form
    {
        private string connectionString = "Host=localhost;Port=5432;Database=taxi5;Username=postgres;Password=135246";
        private int currentClientId = 1;

        public MainForm()
        {
            InitializeComponent(); // Вызов из Designer.cs
            SetupForm(); // Наша настройка формы
            LoadClientData();
        }

        private void SetupForm()
        {
            // Настройка меню
            SetupMenu();

            // Настройка панели приветствия
            SetupWelcomePanel();

            // Настройка панели быстрого доступа
            SetupQuickAccessPanel();

            // Настройка основной панели
            SetupMainPanel();
        }

        private void SetupMenu()
        {
            // Меню "Файл"
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("Файл");
            ToolStripMenuItem exitItem = new ToolStripMenuItem("Выход", null, (s, e) => Application.Exit());
            fileMenu.DropDownItems.Add(exitItem);

            // Меню "Заказы"
            ToolStripMenuItem ordersMenu = new ToolStripMenuItem("Заказы");
            ToolStripMenuItem newOrderItem = new ToolStripMenuItem("Новый заказ", null, (s, e) => ShowNewOrderForm());
            ToolStripMenuItem myOrdersItem = new ToolStripMenuItem("Мои заказы", null, (s, e) => ShowMyOrdersForm());
            ordersMenu.DropDownItems.Add(newOrderItem);
            ordersMenu.DropDownItems.Add(myOrdersItem);

            // Меню "Профиль"
            ToolStripMenuItem profileMenu = new ToolStripMenuItem("Профиль");
            ToolStripMenuItem viewProfileItem = new ToolStripMenuItem("Мой профиль", null, (s, e) => ShowProfileForm());
            ToolStripMenuItem editProfileItem = new ToolStripMenuItem("Редактировать профиль", null, (s, e) => ShowEditProfileForm());
            profileMenu.DropDownItems.Add(viewProfileItem);
            profileMenu.DropDownItems.Add(editProfileItem);

            // Добавление меню
            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(ordersMenu);
            menuStrip.Items.Add(profileMenu);
        }

        private void SetupWelcomePanel()
        {
            welcomePanel.BackColor = Color.LightBlue;
            welcomeLabel.Text = "Добро пожаловать в службу такси!";
            welcomeLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            welcomeLabel.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void SetupQuickAccessPanel()
        {
            newOrderButton.Text = "🚕 Новый заказ";
            newOrderButton.BackColor = Color.Green;
            newOrderButton.ForeColor = Color.White;
            newOrderButton.Font = new Font("Arial", 10, FontStyle.Bold);
            newOrderButton.Click += (s, e) => ShowNewOrderForm();

            myOrdersButton.Text = "📋 Мои заказы";
            myOrdersButton.BackColor = Color.Blue;
            myOrdersButton.ForeColor = Color.White;
            myOrdersButton.Font = new Font("Arial", 10, FontStyle.Bold);
            myOrdersButton.Click += (s, e) => ShowMyOrdersForm();

            profileButton.Text = "👤 Профиль";
            profileButton.BackColor = Color.Orange;
            profileButton.ForeColor = Color.White;
            profileButton.Font = new Font("Arial", 10, FontStyle.Bold);
            profileButton.Click += (s, e) => ShowProfileForm();
        }

        private void SetupMainPanel()
        {
            mainPanel.BackColor = SystemColors.Control;
        }

        private void LoadClientData()
        {
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT 
                            c.first_name, 
                            c.last_name, 
                            cs.status_name,
                            a.city,
                            a.street,
                            a.house
                        FROM client c
                        JOIN clent_status cs ON c.clent_status_id = cs.clent_status_id
                        JOIN address a ON c.address_id = a.address_id
                        WHERE c.client_id = @clientId";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@clientId", currentClientId);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string firstName = reader.GetString(0);
                                string lastName = reader.GetString(1);
                                string status = reader.GetString(2);
                                string city = reader.GetString(3);
                                string street = reader.GetString(4);
                                string house = reader.GetString(5);

                                // Отображаем информацию на форме
                                DisplayClientInfo(firstName, lastName, status, city, street, house);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayClientInfo(string firstName, string lastName, string status, string city, string street, string house)
        {
            // Создаем панель с информацией о клиенте
            Panel infoPanel = new Panel
            {
                Size = new Size(400, 150),
                Location = new Point(20, 20),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            Label nameLabel = new Label
            {
                Text = $"Клиент: {firstName} {lastName}",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };

            Label statusLabel = new Label
            {
                Text = $"Статус: {status}",
                Font = new Font("Arial", 10),
                Location = new Point(10, 40),
                AutoSize = true
            };

            Label addressLabel = new Label
            {
                Text = $"Адрес: {city}, {street}, {house}",
                Font = new Font("Arial", 10),
                Location = new Point(10, 70),
                AutoSize = true
            };

            infoPanel.Controls.Add(nameLabel);
            infoPanel.Controls.Add(statusLabel);
            infoPanel.Controls.Add(addressLabel);

            mainPanel.Controls.Add(infoPanel);
        }

        private void ShowNewOrderForm()
        {
            MessageBox.Show("Форма нового заказа", "Информация");
        }

        private void ShowMyOrdersForm()
        {
            MessageBox.Show("Форма моих заказов", "Информация");
        }

        private void ShowProfileForm()
        {
            MessageBox.Show("Форма профиля", "Информация");
        }

        private void ShowEditProfileForm()
        {
            MessageBox.Show("Форма редактирования профиля", "Информация");
        }

        private void profileButton_Click(object sender, EventArgs e)
        {

        }

        private void quickAccessPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void welcomeLabel_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void newOrderButton_Click(object sender, EventArgs e)
        {

        }
    }
}