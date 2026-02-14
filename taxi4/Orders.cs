using System;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace taxi4
{
    public partial class Orders : Form
    {
        private int driverId;
        private string connectionString;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btnBack;
        private Label lblActiveOrderInfo;

        public Orders()
        {
            InitializeComponent();
        }

        public Orders(int driverId, string connectionString)
        {
            this.driverId = driverId;
            this.connectionString = connectionString;

            this.Text = "Доступные заказы";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(241, 59, 198);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            CreateFlowLayoutPanel();
            CreateBackButton();
            CreateActiveOrderLabel();

            // Проверяем, есть ли у водителя активный заказ
            if (HasActiveOrder())
            {
                ShowActiveOrderMessage();
            }
            else
            {
                LoadAvailableOrders();
            }
        }

        // Создание FlowLayoutPanel
        private void CreateFlowLayoutPanel()
        {
            flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = Color.White;
            flowLayoutPanel1.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanel1.Padding = new Padding(10);
            flowLayoutPanel1.Margin = new Padding(0);
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.WrapContents = false;
            this.Controls.Add(flowLayoutPanel1);
        }

        // Создание кнопки "Назад"
        private void CreateBackButton()
        {
            btnBack = new Button();
            btnBack.Text = "← Назад";
            btnBack.Font = new Font("Arial", 11, FontStyle.Bold);
            btnBack.Size = new Size(100, 40);
            btnBack.BackColor = Color.FromArgb(192, 176, 212);
            btnBack.ForeColor = Color.White;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Cursor = Cursors.Hand;
            btnBack.Dock = DockStyle.Bottom;
            btnBack.Height = 50;
            btnBack.Click += BtnBack_Click;
            this.Controls.Add(btnBack);
            flowLayoutPanel1.SendToBack();
            btnBack.BringToFront();
        }

        // Создание метки для информации об активном заказе
        private void CreateActiveOrderLabel()
        {
            lblActiveOrderInfo = new Label();
            lblActiveOrderInfo.Dock = DockStyle.Top;
            lblActiveOrderInfo.Height = 40;
            lblActiveOrderInfo.BackColor = Color.FromArgb(255, 200, 200);
            lblActiveOrderInfo.ForeColor = Color.DarkRed;
            lblActiveOrderInfo.Font = new Font("Arial", 11, FontStyle.Bold);
            lblActiveOrderInfo.TextAlign = ContentAlignment.MiddleCenter;
            lblActiveOrderInfo.Visible = false;
            this.Controls.Add(lblActiveOrderInfo);
            lblActiveOrderInfo.BringToFront();
        }

        // Проверка наличия активного заказа у водителя
        private bool HasActiveOrder()
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT COUNT(*)
                    FROM ""Order""
                    WHERE driver_id = @driver_id
                      AND order_status = (SELECT order_status_id FROM order_status WHERE name = 'В процессе' LIMIT 1)";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@driver_id", driverId);
                    long count = (long)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        // Показать сообщение об активном заказе
        private void ShowActiveOrderMessage()
        {
            flowLayoutPanel1.Controls.Clear();
            lblActiveOrderInfo.Visible = true;
            lblActiveOrderInfo.Text = "⚠️ У вас уже есть активный заказ. Завершите его, чтобы видеть новые заказы.";

            Label lblMessage = new Label();
            lblMessage.Text = "Выполняется активный заказ.\n\nПожалуйста, завершите его через форму заказа.";
            lblMessage.Font = new Font("Arial", 12, FontStyle.Italic);
            lblMessage.ForeColor = Color.Gray;
            lblMessage.AutoSize = true;
            lblMessage.Location = new Point(10, 10);
            flowLayoutPanel1.Controls.Add(lblMessage);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Загрузка доступных заказов (только если нет активного)
        private void LoadAvailableOrders()
        {
            flowLayoutPanel1.Controls.Clear();
            lblActiveOrderInfo.Visible = false;

            string query = @"
                SELECT 
                    o.order_id,
                    a_from.city || ', ' || a_from.street || ', д. ' || a_from.house AS from_address,
                    a_to.city || ', ' || a_to.street || ', д. ' || a_to.house AS to_address,
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

                            // Создаём карточку заказа (как и раньше)
                            Panel orderPanel = new Panel();
                            orderPanel.Size = new Size(flowLayoutPanel1.ClientSize.Width - 30, 110);
                            orderPanel.BackColor = Color.FromArgb(245, 245, 250);
                            orderPanel.BorderStyle = BorderStyle.FixedSingle;
                            orderPanel.Margin = new Padding(5);

                            Label lblRoute = new Label();
                            lblRoute.Text = $"{from}\n➔ {to}";
                            lblRoute.Location = new Point(10, 10);
                            lblRoute.Size = new Size(300, 40);
                            lblRoute.Font = new Font("Arial", 10);

                            Label lblPrice = new Label();
                            lblPrice.Text = $"{cost:N0} ₽";
                            lblPrice.Location = new Point(320, 10);
                            lblPrice.Size = new Size(100, 25);
                            lblPrice.Font = new Font("Arial", 12, FontStyle.Bold);
                            lblPrice.ForeColor = Color.Green;

                            Label lblTariff = new Label();
                            lblTariff.Text = $"🚗 {tariff}";
                            lblTariff.Location = new Point(320, 40);
                            lblTariff.Size = new Size(100, 25);

                            Button btnDetails = new Button();
                            btnDetails.Text = "Подробнее →";
                            btnDetails.Location = new Point(450, 35);
                            btnDetails.Size = new Size(120, 40);
                            btnDetails.BackColor = Color.FromArgb(192, 176, 212);
                            btnDetails.ForeColor = Color.White;
                            btnDetails.FlatStyle = FlatStyle.Flat;
                            btnDetails.FlatAppearance.BorderSize = 0;
                            btnDetails.Font = new Font("Arial", 10, FontStyle.Bold);
                            btnDetails.Tag = orderId;

                            btnDetails.Click += (s, e) =>
                            {
                                OrderDetailsForm detailsForm = new OrderDetailsForm(
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
                                    // После завершения заказа проверяем, нет ли других активных
                                    if (HasActiveOrder())
                                    {
                                        ShowActiveOrderMessage();
                                    }
                                    else
                                    {
                                        LoadAvailableOrders();
                                    }
                                }
                            };

                            orderPanel.Controls.Add(lblRoute);
                            orderPanel.Controls.Add(lblPrice);
                            orderPanel.Controls.Add(lblTariff);
                            orderPanel.Controls.Add(btnDetails);

                            flowLayoutPanel1.Controls.Add(orderPanel);
                        }
                    }
                }
            }

            if (flowLayoutPanel1.Controls.Count == 0)
            {
                Label lblEmpty = new Label();
                lblEmpty.Text = "Нет доступных заказов.";
                lblEmpty.Font = new Font("Arial", 12, FontStyle.Italic);
                lblEmpty.ForeColor = Color.Gray;
                lblEmpty.AutoSize = true;
                lblEmpty.Location = new Point(10, 10);
                flowLayoutPanel1.Controls.Add(lblEmpty);
            }
        }

       
        private void Orders_Load(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void button2_Click_1(object sender, EventArgs e) { }
        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void button2_Click_2(object sender, EventArgs e) { }
        private void button4_Click(object sender, EventArgs e) { }
        private void button6_Click(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void button2_Click_3(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}