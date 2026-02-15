using System;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace TaxiClientApp
{
    public partial class ProfileForm : Form
    {
        private int clientId;
        private string connectionString = "Server=localhost;Port=5432;Database=taxi5;User Id=postgres;Password=135246";

        private TextBox txtFirstName, txtLastName, txtPatronymic;
        private TextBox txtPhone, txtEmail;
        private TextBox txtCity, txtStreet, txtHouse, txtEntrance;
        private Label lblStatusValue;
        private Button btnSave, btnChangePassword, btnCancel;

        public ProfileForm(int clientId, string connectionString = null)
        {
            this.clientId = clientId;
            this.connectionString = connectionString ?? this.connectionString;
            InitializeComponent();
            LoadClientData();
        }

        private void InitializeComponent()
        {
            this.Text = "Мой профиль";
            this.Size = new Size(600, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Font = new Font("Segoe UI", 10F);

            var lblHeader = new Label
            {
                Text = "👤 Личные данные",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(20, 20),
                Size = new Size(300, 40)
            };

            var mainPanel = new TableLayoutPanel
            {
                Location = new Point(20, 70),
                Size = new Size(540, 450),
                ColumnCount = 2,
                RowCount = 10,
                Padding = new Padding(10),
                BackColor = Color.White,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            for (int i = 0; i < 10; i++)
                mainPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

            AddLabelAndControl(mainPanel, "Фамилия:", 0, out txtLastName);
            AddLabelAndControl(mainPanel, "Имя:", 1, out txtFirstName);
            AddLabelAndControl(mainPanel, "Отчество:", 2, out txtPatronymic);
            AddLabelAndControl(mainPanel, "Телефон:", 3, out txtPhone);
            AddLabelAndControl(mainPanel, "Email:", 4, out txtEmail);
            AddLabelAndControl(mainPanel, "Город:", 5, out txtCity);
            AddLabelAndControl(mainPanel, "Улица:", 6, out txtStreet);
            AddLabelAndControl(mainPanel, "Дом:", 7, out txtHouse);
            AddLabelAndControl(mainPanel, "Подъезд:", 8, out txtEntrance);

            var lblStatusTitle = new Label
            {
                Text = "Статус:",
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 100)
            };
            mainPanel.Controls.Add(lblStatusTitle, 0, 9);

            lblStatusValue = new Label
            {
                Text = "",
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            mainPanel.Controls.Add(lblStatusValue, 1, 9);

            var pnlButtons = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 80,
                BackColor = Color.FromArgb(245, 247, 250),
                Padding = new Padding(20)
            };

            btnSave = CreateModernButton("💾 Сохранить", Color.FromArgb(46, 204, 113));
            btnSave.Location = new Point(20, 20);
            btnSave.Click += BtnSave_Click;

            btnChangePassword = CreateModernButton("🔐 Сменить пароль", Color.FromArgb(155, 89, 182));
            btnChangePassword.Location = new Point(210, 20);
            btnChangePassword.Click += BtnChangePassword_Click;

            btnCancel = CreateModernButton("✖ Закрыть", Color.FromArgb(149, 165, 166));
            btnCancel.Location = new Point(400, 20);
            btnCancel.Click += (s, e) => this.Close();

            pnlButtons.Controls.AddRange(new Control[] { btnSave, btnChangePassword, btnCancel });

            this.Controls.Add(lblHeader);
            this.Controls.Add(mainPanel);
            this.Controls.Add(pnlButtons);
        }

        private void AddLabelAndControl(TableLayoutPanel panel, string labelText, int row, out TextBox textBox)
        {
            var label = new Label
            {
                Text = labelText,
                TextAlign = ContentAlignment.MiddleRight,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 100, 100)
            };
            panel.Controls.Add(label, 0, row);

            textBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10F),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };
            panel.Controls.Add(textBox, 1, row);
        }

        private Button CreateModernButton(string text, Color backColor)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(160, 40),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = backColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.MouseEnter += (s, e) => btn.BackColor = ControlPaint.Light(backColor, 0.2f);
            btn.MouseLeave += (s, e) => btn.BackColor = backColor;
            return btn;
        }

        private void LoadClientData()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"
                        SELECT 
                            c.first_name, c.last_name, c.patronymic,
                            c.phone_number,
                            a.city, a.street, a.house, a.entrance,
                            cs.status_name,
                            acc.login AS email
                        FROM client c
                        JOIN address a ON c.address_id = a.address_id
                        JOIN client_status cs ON c.client_status_id = cs.client_status_id
                        JOIN account acc ON c.account_id = acc.account_id
                        WHERE c.client_id = @id";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", clientId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtFirstName.Text = reader["first_name"].ToString();
                                txtLastName.Text = reader["last_name"].ToString();
                                txtPatronymic.Text = reader["patronymic"].ToString();
                                txtPhone.Text = reader["phone_number"].ToString();
                                txtEmail.Text = reader["email"].ToString();
                                txtCity.Text = reader["city"].ToString();
                                txtStreet.Text = reader["street"].ToString();
                                txtHouse.Text = reader["house"].ToString();
                                txtEntrance.Text = reader["entrance"].ToString();
                                string status = reader["status_name"].ToString();
                                lblStatusValue.Text = status;
                                lblStatusValue.ForeColor = status == "Активен" ? Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки профиля: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text) || string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Имя и фамилия обязательны для заполнения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    string updateAddressSql = @"
                        UPDATE address 
                        SET city = @city, street = @street, house = @house, entrance = @entrance
                        WHERE address_id = (SELECT address_id FROM client WHERE client_id = @clientId)";

                    using (var cmd = new NpgsqlCommand(updateAddressSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@city", txtCity.Text);
                        cmd.Parameters.AddWithValue("@street", txtStreet.Text);
                        cmd.Parameters.AddWithValue("@house", txtHouse.Text);
                        cmd.Parameters.AddWithValue("@entrance", txtEntrance.Text);
                        cmd.Parameters.AddWithValue("@clientId", clientId);
                        cmd.ExecuteNonQuery();
                    }

                    string updateClientSql = @"
                        UPDATE client 
                        SET first_name = @firstName, last_name = @lastName, patronymic = @patronymic, phone_number = @phone
                        WHERE client_id = @clientId";

                    using (var cmd = new NpgsqlCommand(updateClientSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                        cmd.Parameters.AddWithValue("@lastName", txtLastName.Text);
                        cmd.Parameters.AddWithValue("@patronymic", string.IsNullOrWhiteSpace(txtPatronymic.Text) ? DBNull.Value : (object)txtPatronymic.Text);
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cmd.Parameters.AddWithValue("@clientId", clientId);
                        cmd.ExecuteNonQuery();
                    }

                    string updateAccountSql = @"
                        UPDATE account 
                        SET login = @email 
                        WHERE account_id = (SELECT account_id FROM client WHERE client_id = @clientId)";

                    using (var cmd = new NpgsqlCommand(updateAccountSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@clientId", clientId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Данные успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnChangePassword_Click(object sender, EventArgs e)
        {
            using (var form = new ChangePasswordForm(clientId, connectionString))
                form.ShowDialog();
        }
    }

    public class ChangePasswordForm : Form
    {
        private int clientId;
        private string connectionString;
        private TextBox txtCurrent, txtNew, txtConfirm;

        public ChangePasswordForm(int clientId, string connectionString)
        {
            this.clientId = clientId;
            this.connectionString = connectionString;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Смена пароля";
            this.Size = new Size(450, 300);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            var tbl = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30),
                ColumnCount = 2,
                RowCount = 4,
                BackColor = Color.White
            };
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            for (int i = 0; i < 4; i++)
                tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));

            tbl.Controls.Add(new Label { Text = "Текущий пароль:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill }, 0, 0);
            txtCurrent = new TextBox { Dock = DockStyle.Fill, PasswordChar = '*', Font = new Font("Segoe UI", 10F) };
            tbl.Controls.Add(txtCurrent, 1, 0);

            tbl.Controls.Add(new Label { Text = "Новый пароль:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill }, 0, 1);
            txtNew = new TextBox { Dock = DockStyle.Fill, PasswordChar = '*', Font = new Font("Segoe UI", 10F) };
            tbl.Controls.Add(txtNew, 1, 1);

            tbl.Controls.Add(new Label { Text = "Подтверждение:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill }, 0, 2);
            txtConfirm = new TextBox { Dock = DockStyle.Fill, PasswordChar = '*', Font = new Font("Segoe UI", 10F) };
            tbl.Controls.Add(txtConfirm, 1, 2);

            var pnlButtons = new Panel { Dock = DockStyle.Bottom, Height = 70, BackColor = Color.FromArgb(245, 247, 250), Padding = new Padding(20) };
            var btnSave = new Button
            {
                Text = "Сохранить",
                Size = new Size(120, 35),
                Location = new Point(150, 15),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSave.Click += BtnSave_Click;
            pnlButtons.Controls.Add(btnSave);

            var btnCancel = new Button
            {
                Text = "Отмена",
                Size = new Size(100, 35),
                Location = new Point(290, 15),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCancel.Click += (s, e) => this.Close();
            pnlButtons.Controls.Add(btnCancel);

            this.Controls.Add(tbl);
            this.Controls.Add(pnlButtons);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (txtNew.Text != txtConfirm.Text)
            {
                MessageBox.Show("Новый пароль и подтверждение не совпадают.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtNew.Text.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать минимум 6 символов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string checkSql = "SELECT password FROM account WHERE account_id = (SELECT account_id FROM client WHERE client_id = @id)";
                    using (var cmd = new NpgsqlCommand(checkSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", clientId);
                        var currentHash = cmd.ExecuteScalar()?.ToString();
                        if (currentHash != txtCurrent.Text)
                        {
                            MessageBox.Show("Неверный текущий пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string updateSql = "UPDATE account SET password = @pwd WHERE account_id = (SELECT account_id FROM client WHERE client_id = @id)";
                    using (var cmd = new NpgsqlCommand(updateSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@pwd", txtNew.Text);
                        cmd.Parameters.AddWithValue("@id", clientId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Пароль успешно изменён!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}