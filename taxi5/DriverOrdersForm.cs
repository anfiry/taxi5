using System;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace taxi4
{
    public partial class DriverOrdersForm : Form
    {
        private int driverId;
        private string connectionString;

        public DriverOrdersForm()
        {
            InitializeComponent();
        }

        public DriverOrdersForm(int driverId, string connectionString) : this()
        {
            this.driverId = driverId;
            this.connectionString = connectionString;

            this.ClientSize = new Size(900, 600);
            this.MinimumSize = new Size(900, 600);

            if (HasActiveOrder())
            {
                ShowActiveOrderMessage();
            }
            else
            {
                LoadAvailableOrders();
            }
        }

        private int? GetActiveOrderId()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT order_id
                    FROM ""Order""
                    WHERE driver_id = @driver_id
                      AND order_status = (SELECT order_status_id FROM order_status WHERE name = 'В процессе' LIMIT 1)
                    LIMIT 1";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@driver_id", driverId);
                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        return Convert.ToInt32(result);
                    return null;
                }
            }
        }

        private bool HasActiveOrder()
        {
            return GetActiveOrderId().HasValue;
        }

        private void ShowActiveOrderMessage()
        {
            flowLayoutPanel1.Controls.Clear();
            lblActiveOrderInfo.Visible = true;
            lblActiveOrderInfo.Text = "⚠️ У вас уже есть активный заказ. Завершите его, чтобы видеть новые заказы.";

            Label lblMessage = new Label
            {
                Text = "Выполняется активный заказ.\n",
                Font = new Font("Arial", 12, FontStyle.Italic),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(10, 10)
            };
            flowLayoutPanel1.Controls.Add(lblMessage);

            int? activeOrderId = GetActiveOrderId();
            if (activeOrderId.HasValue)
            {
                Button btnCompleteOrder = new Button
                {
                    Text = "Перейти к активному заказу",
                    Location = new Point(10, 100),
                    Size = new Size(250, 50),
                    BackColor = Color.FromArgb(46, 204, 113),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Arial", 11, FontStyle.Bold)
                };
                btnCompleteOrder.FlatAppearance.BorderSize = 0;
                btnCompleteOrder.Click += (s, e) =>
                {
                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = @"
                            SELECT 
                                a_from.city || ', ' || a_from.street || ', д. ' || a_from.house AS from_address,
                                a_to.city || ', ' || a_to.street || ', д. ' || a_to.house AS to_address,
                                o.final_cost,
                                t.name AS tariff_name
                            FROM ""Order"" o
                            JOIN address a_from ON o.address_from = a_from.address_id
                            JOIN address a_to ON o.address_to = a_to.address_id
                            JOIN tariff t ON o.tariff_id = t.tariff_id
                            WHERE o.order_id = @order_id";
                        using (var cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@order_id", activeOrderId.Value);
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string from = reader.GetString(0);
                                    string to = reader.GetString(1);
                                    decimal cost = reader.GetDecimal(2);
                                    string tariff = reader.GetString(3);

                                    DriverOrderDetailsForm detailsForm = new DriverOrderDetailsForm(
                                        activeOrderId.Value,
                                        from,
                                        to,
                                        cost.ToString(),
                                        "1",
                                        driverId,
                                        connectionString
                                    );

                                    if (detailsForm.ShowDialog() == DialogResult.OK)
                                    {
                                        if (!HasActiveOrder())
                                            LoadAvailableOrders();
                                        else
                                            ShowActiveOrderMessage();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Не удалось загрузить данные активного заказа.", "Ошибка",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                };
                flowLayoutPanel1.Controls.Add(btnCompleteOrder);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadAvailableOrders()
        {
            flowLayoutPanel1.Controls.Clear();
            lblActiveOrderInfo.Visible = false;

            string query = @"
                SELECT 
                    o.order_id,
                    COALESCE(a_from.city, '') || ', ' || COALESCE(a_from.street, '') || ', д. ' || COALESCE(a_from.house, '') AS from_address,
                    COALESCE(a_to.city, '') || ', ' || COALESCE(a_to.street, '') || ', д. ' || COALESCE(a_to.house, '') AS to_address,
                    o.final_cost,
                    t.name AS tariff_name
                FROM ""Order"" o
                JOIN address a_from ON o.address_from = a_from.address_id
                JOIN address a_to ON o.address_to = a_to.address_id
                JOIN tariff t ON o.tariff_id = t.tariff_id
                WHERE o.driver_id IS NULL
                  AND o.order_status = (SELECT order_status_id FROM order_status WHERE name = 'Создан' LIMIT 1)
                ORDER BY o.order_datetime DESC
                LIMIT 10;";

            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int orderId = reader.GetInt32(0);
                            string from = reader.GetString(1);
                            string to = reader.GetString(2);
                            decimal cost = reader.GetDecimal(3);
                            string tariff = reader.GetString(4);

                            Panel orderPanel = new Panel
                            {
                                Width = 850,
                                Height = 120,
                                BackColor = Color.FromArgb(245, 245, 250),
                                BorderStyle = BorderStyle.FixedSingle,
                                Margin = new Padding(5)
                            };

                            Label lblFrom = new Label
                            {
                                Text = $"📍 {from}",
                                Location = new Point(10, 10),
                                Width = 400,
                                Height = 20,
                                Font = new Font("Arial", 10)
                            };

                            Label lblTo = new Label
                            {
                                Text = $"➔ {to}",
                                Location = new Point(10, 35),
                                Width = 400,
                                Height = 20,
                                Font = new Font("Arial", 10)
                            };

                            Label lblPrice = new Label
                            {
                                Text = $"{cost:N0} ₽",
                                Location = new Point(500, 15),
                                Width = 120,
                                Height = 30,
                                Font = new Font("Arial", 14, FontStyle.Bold),
                                ForeColor = Color.Green,
                                TextAlign = ContentAlignment.MiddleRight
                            };

                            Label lblTariff = new Label
                            {
                                Text = $"🚗 {tariff}",
                                Location = new Point(500, 50),
                                Width = 120,
                                Height = 20,
                                Font = new Font("Arial", 10),
                                TextAlign = ContentAlignment.MiddleRight
                            };

                            Button btnDetails = new Button
                            {
                                Text = "Подробнее →",
                                Location = new Point(650, 35),
                                Size = new Size(140, 45),
                                BackColor = Color.FromArgb(192, 176, 212),
                                ForeColor = Color.White,
                                FlatStyle = FlatStyle.Flat,
                                Font = new Font("Arial", 10, FontStyle.Bold),
                                Tag = orderId
                            };
                            btnDetails.FlatAppearance.BorderSize = 0;
                            btnDetails.Click += (s, e) =>
                            {
                                DriverOrderDetailsForm detailsForm = new DriverOrderDetailsForm(
                                    orderId,
                                    from,
                                    to,
                                    cost.ToString(),
                                    "1",
                                    driverId,
                                    connectionString
                                );

                                if (detailsForm.ShowDialog() == DialogResult.OK)
                                {
                                    if (HasActiveOrder())
                                        ShowActiveOrderMessage();
                                    else
                                        LoadAvailableOrders();
                                }
                            };

                            orderPanel.Controls.AddRange(new Control[] { lblFrom, lblTo, lblPrice, lblTariff, btnDetails });
                            flowLayoutPanel1.Controls.Add(orderPanel);
                        }
                    }
                }
            }

            if (flowLayoutPanel1.Controls.Count == 0)
            {
                Label lblEmpty = new Label
                {
                    Text = "Нет доступных заказов.",
                    Font = new Font("Arial", 12, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Location = new Point(10, 10)
                };
                flowLayoutPanel1.Controls.Add(lblEmpty);
            }
        }
    }
}