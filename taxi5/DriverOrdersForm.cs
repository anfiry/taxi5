using Npgsql;
using System;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace taxi4
{
    public partial class DriverOrdersForm : Form
    {
        private int driverId;
        private string connectionString;
        private int accountId;
        private bool back = false;

        public DriverOrdersForm()
        {
            InitializeComponent();
        }

        public void OnClosed()
        {
            if (back)
            { back = false; }
            else { Application.Exit(); }
        }

        public DriverOrdersForm(int driverId, string connectionString, int accountId) : this()
        {
            this.driverId = driverId;
            this.connectionString = connectionString;
            this.accountId = accountId;

            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(900, 600);

            this.Resize += DriverOrdersForm_Resize;

            if (HasActiveOrder())
            {
                ShowActiveOrderMessage();
            }
            else
            {
                LoadAvailableOrders();
            }
        }

        private void DriverOrdersForm_Resize(object sender, EventArgs e)
        {
            if (!HasActiveOrder())
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
                      AND order_status = 2
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
            flowLayoutPanel.Controls.Clear();
            lblActiveOrderInfo.Visible = true;

            Label lblMessage = new Label
            {
                Text = "Выполняется активный заказ.\n",
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Italic),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(10, 10)
            };
            flowLayoutPanel.Controls.Add(lblMessage);

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
                    Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold)
                };
                btnCompleteOrder.FlatAppearance.BorderSize = 0;
                btnCompleteOrder.Click += (sender, evt) =>
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
                                        "2",
                                        driverId,
                                        connectionString,
                                        accountId
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
                flowLayoutPanel.Controls.Add(btnCompleteOrder);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            DriverMenu driverMenu = new DriverMenu(accountId);
            back = true;

            driverMenu.Show();
            this.Close();
        }

        private void LoadAvailableOrders()
        {
            flowLayoutPanel.Controls.Clear();
            lblActiveOrderInfo.Visible = false;

            int panelWidth = flowLayoutPanel.ClientSize.Width - 40;
            int cardWidth = Math.Max(panelWidth, 700);

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
                  AND o.order_status = 1
                ORDER BY o.order_datetime DESC
                LIMIT 20;";

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

                            Panel orderCard = new Panel
                            {
                                Width = cardWidth - 20,
                                Height = 110,
                                BackColor = Color.FromArgb(245, 245, 250),
                                BorderStyle = BorderStyle.FixedSingle,
                                Margin = new Padding(0, 0, 0, 10)
                            };

                            Label lblFrom = new Label
                            {
                                Text = $"📍 {from}",
                                Location = new Point(15, 12),
                                Width = cardWidth - 180,
                                Height = 22,
                                Font = new Font("Microsoft Sans Serif", 10F),
                                ForeColor = Color.Black
                            };

                            Label lblTo = new Label
                            {
                                Text = $"➔ {to}",
                                Location = new Point(15, 38),
                                Width = cardWidth - 180,
                                Height = 22,
                                Font = new Font("Microsoft Sans Serif", 10F),
                                ForeColor = Color.Black
                            };

                            Label lblTariff = new Label
                            {
                                Text = $"🚗 {tariff}",
                                Location = new Point(15, 64),
                                Width = 150,
                                Height = 22,
                                Font = new Font("Microsoft Sans Serif", 10F),
                                ForeColor = Color.FromArgb(100, 100, 100)
                            };

                            Label lblPrice = new Label
                            {
                                Text = $"{cost:N0} ₽",
                                Location = new Point(cardWidth - 190, 25),
                                Width = 100,
                                Height = 30,
                                Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold),
                                ForeColor = Color.Green,
                                TextAlign = ContentAlignment.MiddleRight
                            };

                            Button btnDetails = new Button
                            {
                                Text = "Подробнее →",
                                Location = new Point(cardWidth - 190, 60),
                                Size = new Size(100, 35),
                                BackColor = Color.FromArgb(192, 176, 212),
                                ForeColor = Color.White,
                                FlatStyle = FlatStyle.Flat,
                                Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold),
                                Cursor = Cursors.Hand,
                                Tag = orderId
                            };
                            btnDetails.FlatAppearance.BorderSize = 0;
                            btnDetails.Click += (senderBtn, evtBtn) =>
                            {
                                DriverOrderDetailsForm detailsForm = new DriverOrderDetailsForm(
                                    orderId,
                                    from,
                                    to,
                                    cost.ToString(),
                                    "1",
                                    driverId,
                                    connectionString,
                                    accountId
                                );

                                detailsForm.Closed += (s, args) => detailsForm.OnClosed();
                                detailsForm.Show();
                                this.Hide();
                            };

                            orderCard.Controls.AddRange(new Control[] { lblFrom, lblTo, lblTariff, lblPrice, btnDetails });
                            flowLayoutPanel.Controls.Add(orderCard);
                        }
                    }
                }
            }

            if (flowLayoutPanel.Controls.Count == 0)
            {
                Label lblEmpty = new Label
                {
                    Text = "Нет доступных заказов.",
                    Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Location = new Point(10, 10)
                };
                flowLayoutPanel.Controls.Add(lblEmpty);
            }
        }
    }
}