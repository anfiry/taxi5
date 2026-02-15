using System;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace TaxiClientApp
{
    public partial class EditProfileForm : Form
    {
        private int clientId;
        private string connectionString;

        private TextBox firstNameTextBox;
        private TextBox lastNameTextBox;
        private TextBox patronymicTextBox;
        private TextBox phoneTextBox;
        private TextBox cityTextBox;
        private TextBox streetTextBox;
        private TextBox houseTextBox;
        private TextBox entranceTextBox;

        public EditProfileForm(int clientId, string connectionString)
        {
            this.clientId = clientId;
            this.connectionString = connectionString;
            InitializeComponent();
            LoadCurrentData();
        }

        private void InitializeComponent()
        {
            this.Text = "Редактирование профиля";
            this.Size = new Size(500, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Основной контейнер с прокруткой
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            TableLayoutPanel mainTable = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Padding = new Padding(20),
                ColumnCount = 2,
                RowCount = 9,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            mainTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));

            // Имя
            mainTable.Controls.Add(new Label
            {
                Text = "Имя*:",
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Arial", 10)
            }, 0, 0);

            firstNameTextBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 10)
            };
            mainTable.Controls.Add(firstNameTextBox, 1, 0);

            // Фамилия
            mainTable.Controls.Add(new Label
            {
                Text = "Фамилия*:",
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Arial", 10)
            }, 0, 1);

            lastNameTextBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 10)
            };
            mainTable.Controls.Add(lastNameTextBox, 1, 1);

            // Отчество
            mainTable.Controls.Add(new Label
            {
                Text = "Отчество:",
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Arial", 10)
            }, 0, 2);

            patronymicTextBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 10)
            };
            mainTable.Controls.Add(patronymicTextBox, 1, 2);

            // Телефон
            mainTable.Controls.Add(new Label
            {
                Text = "Телефон*:",
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Arial", 10)
            }, 0, 3);

            phoneTextBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 10)
            };
            mainTable.Controls.Add(phoneTextBox, 1, 3);

            // Заголовок адреса
            Label addressLabel = new Label
            {
                Text = "Адрес:",
                Font = new Font("Arial", 11, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Height = 30
            };
            mainTable.SetColumnSpan(addressLabel, 2);
            mainTable.Controls.Add(addressLabel, 0, 4);

            // Город
            mainTable.Controls.Add(new Label
            {
                Text = "Город*:",
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Arial", 10)
            }, 0, 5);

            cityTextBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 10)
            };
            mainTable.Controls.Add(cityTextBox, 1, 5);

            // Улица
            mainTable.Controls.Add(new Label
            {
                Text = "Улица*:",
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Arial", 10)
            }, 0, 6);

            streetTextBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 10)
            };
            mainTable.Controls.Add(streetTextBox, 1, 6);

            // Дом
            mainTable.Controls.Add(new Label
            {
                Text = "Дом*:",
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Arial", 10)
            }, 0, 7);

            houseTextBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 10)
            };
            mainTable.Controls.Add(houseTextBox, 1, 7);

            // Подъезд
            mainTable.Controls.Add(new Label
            {
                Text = "Подъезд:",
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Arial", 10)
            }, 0, 8);

            entranceTextBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Arial", 10)
            };
            mainTable.Controls.Add(entranceTextBox, 1, 8);

            for (int i = 0; i < 9; i++)
            {
                mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            }

            mainTable.RowStyles[4] = new RowStyle(SizeType.Absolute, 50); // Для заголовка адреса

            // Кнопки
            Panel buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 70,
                Padding = new Padding(20),
                BackColor = Color.WhiteSmoke
            };

            Button saveButton = new Button
            {
                Text = "Сохранить",
                Size = new Size(120, 40),
                Font = new Font("Arial", 10, FontStyle.Bold),
                BackColor = Color.Green,
                ForeColor = Color.White,
                Anchor = AnchorStyles.Right
            };
            saveButton.Click += (s, e) => SaveChanges();

            Button cancelButton = new Button
            {
                Text = "Отмена",
                Size = new Size(100, 40),
                Anchor = AnchorStyles.Right,
                Margin = new Padding(0, 0, 10, 0)
            };
            cancelButton.Click += (s, e) => this.Close();

            Label requiredLabel = new Label
            {
                Text = "* - обязательные поля",
                Font = new Font("Arial", 8),
                ForeColor = Color.Gray,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Left
            };

            buttonPanel.Controls.Add(requiredLabel);
            buttonPanel.Controls.Add(saveButton);
            buttonPanel.Controls.Add(cancelButton);

            mainPanel.Controls.Add(mainTable);
            this.Controls.Add(mainPanel);
            this.Controls.Add(buttonPanel);
        }

        private void LoadCurrentData()
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
                            COALESCE(c.patronymic, '') as patronymic,
                            c.phone_number,
                            a.city,
                            a.street,
                            a.house,
                            a.entrance
                        FROM client c
                        JOIN address a ON c.address_id = a.address_id
                        WHERE c.client_id = @clientId";

                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@clientId", clientId);

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                firstNameTextBox.Text = reader["first_name"].ToString();
                                lastNameTextBox.Text = reader["last_name"].ToString();
                                patronymicTextBox.Text = reader["patronymic"].ToString();
                                phoneTextBox.Text = reader["phone_number"].ToString();
                                cityTextBox.Text = reader["city"].ToString();
                                streetTextBox.Text = reader["street"].ToString();
                                houseTextBox.Text = reader["house"].ToString();
                                entranceTextBox.Text = reader["entrance"].ToString();
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

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(firstNameTextBox.Text))
            {
                MessageBox.Show("Поле 'Имя' обязательно для заполнения", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                firstNameTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(lastNameTextBox.Text))
            {
                MessageBox.Show("Поле 'Фамилия' обязательно для заполнения", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                lastNameTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(phoneTextBox.Text))
            {
                MessageBox.Show("Поле 'Телефон' обязательно для заполнения", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                phoneTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cityTextBox.Text))
            {
                MessageBox.Show("Поле 'Город' обязательно для заполнения", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cityTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(streetTextBox.Text))
            {
                MessageBox.Show("Поле 'Улица' обязательно для заполнения", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                streetTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(houseTextBox.Text))
            {
                MessageBox.Show("Поле 'Дом' обязательно для заполнения", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                houseTextBox.Focus();
                return false;
            }

            // Проверка формата телефона (базовая)
            if (!System.Text.RegularExpressions.Regex.IsMatch(phoneTextBox.Text, @"^[\+\d\s\-\(\)]+$"))
            {
                MessageBox.Show("Некорректный формат телефона", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                phoneTextBox.Focus();
                return false;
            }

            return true;
        }

        private void SaveChanges()
        {
            if (!ValidateInput())
                return;

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Начинаем транзакцию
                    using (NpgsqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Получаем текущий address_id клиента
                            string getAddressIdQuery = "SELECT address_id FROM client WHERE client_id = @clientId";
                            int currentAddressId;

                            using (NpgsqlCommand command = new NpgsqlCommand(getAddressIdQuery, connection))
                            {
                                command.Transaction = transaction;
                                command.Parameters.AddWithValue("@clientId", clientId);
                                currentAddressId = Convert.ToInt32(command.ExecuteScalar());
                            }

                            // Обновляем адрес
                            string updateAddressQuery = @"
                                UPDATE address 
                                SET city = @city, 
                                    street = @street, 
                                    house = @house, 
                                    entrance = @entrance
                                WHERE address_id = @addressId";

                            using (NpgsqlCommand command = new NpgsqlCommand(updateAddressQuery, connection))
                            {
                                command.Transaction = transaction;
                                command.Parameters.AddWithValue("@city", cityTextBox.Text.Trim());
                                command.Parameters.AddWithValue("@street", streetTextBox.Text.Trim());
                                command.Parameters.AddWithValue("@house", houseTextBox.Text.Trim());
                                command.Parameters.AddWithValue("@entrance", entranceTextBox.Text.Trim());
                                command.Parameters.AddWithValue("@addressId", currentAddressId);

                                command.ExecuteNonQuery();
                            }

                            // Обновляем данные клиента
                            string updateClientQuery = @"
                                UPDATE client 
                                SET first_name = @firstName, 
                                    last_name = @lastName, 
                                    patronymic = @patronymic, 
                                    phone_number = @phoneNumber
                                WHERE client_id = @clientId";

                            using (NpgsqlCommand command = new NpgsqlCommand(updateClientQuery, connection))
                            {
                                command.Transaction = transaction;
                                command.Parameters.AddWithValue("@firstName", firstNameTextBox.Text.Trim());
                                command.Parameters.AddWithValue("@lastName", lastNameTextBox.Text.Trim());
                                command.Parameters.AddWithValue("@patronymic",
                                    string.IsNullOrWhiteSpace(patronymicTextBox.Text) ?
                                    (object)DBNull.Value : patronymicTextBox.Text.Trim());
                                command.Parameters.AddWithValue("@phoneNumber", phoneTextBox.Text.Trim());
                                command.Parameters.AddWithValue("@clientId", clientId);

                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    // Коммитим транзакцию
                                    transaction.Commit();

                                    MessageBox.Show("Данные успешно сохранены!", "Успех",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    this.DialogResult = DialogResult.OK;
                                    this.Close();
                                }
                                else
                                {
                                    throw new Exception("Не удалось обновить данные клиента");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Откатываем транзакцию при ошибке
                            transaction.Rollback();
                            throw new Exception($"Ошибка сохранения данных: {ex.Message}", ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}