using System;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace TaxiClientApp
{
    public partial class NewOrderForm : Form
    {
        private int clientId;
        private string connectionString = "Server=localhost;Port=5432;Database=taxi5;User Id=postgres;Password=135246";
        private TextBox txtFrom, txtTo;
        private ComboBox cmbTariff, cmbPayment;
        private Label lblEstimatedCost;
        private Button btnCreate;

        public NewOrderForm(int clientId, string connectionString = null)
        {
            this.clientId = clientId;
            this.connectionString = connectionString ?? this.connectionString;
            InitializeComponent();
            LoadTariffs();
            LoadPaymentMethods();
        }

        private void InitializeComponent()
        {
            this.Text = "Новый заказ";
            this.Size = new Size(550, 480);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10F);

            var lblHeader = new Label
            {
                Text = "🚖 Оформление поездки",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219),
                Location = new Point(20, 20),
                Size = new Size(350, 40)
            };

            var tbl = new TableLayoutPanel
            {
                Location = new Point(20, 70),
                Size = new Size(490, 300),
                ColumnCount = 2,
                RowCount = 6,
                Padding = new Padding(5),
                BackColor = Color.White
            };
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            for (int i = 0; i < 6; i++)
                tbl.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));

            tbl.Controls.Add(new Label { Text = "Откуда:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill }, 0, 0);
            txtFrom = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F), Text = "Воронеж, " };
            SetPlaceholder(txtFrom, "Воронеж, ул. Ленина, д. 1");
            tbl.Controls.Add(txtFrom, 1, 0);

            tbl.Controls.Add(new Label { Text = "Куда:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill }, 0, 1);
            txtTo = new TextBox { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F), Text = "Воронеж, " };
            SetPlaceholder(txtTo, "Воронеж, Московский пр., д. 10");
            tbl.Controls.Add(txtTo, 1, 1);

            tbl.Controls.Add(new Label { Text = "Тариф:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill }, 0, 2);
            cmbTariff = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.White
            };
            cmbTariff.SelectedIndexChanged += (s, e) => CalculateCost();
            tbl.Controls.Add(cmbTariff, 1, 2);

            tbl.Controls.Add(new Label { Text = "Оплата:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill }, 0, 3);
            cmbPayment = new ComboBox
            {
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.White
            };
            tbl.Controls.Add(cmbPayment, 1, 3);

            tbl.Controls.Add(new Label { Text = "Примерная стоимость:", TextAlign = ContentAlignment.MiddleRight, Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F, FontStyle.Bold) }, 0, 4);
            lblEstimatedCost = new Label
            {
                Text = "0 ₽",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(46, 204, 113)
            };
            tbl.Controls.Add(lblEstimatedCost, 1, 4);

            btnCreate = new Button
            {
                Text = "🚕 Заказать такси",
                Dock = DockStyle.Fill,
                Height = 50,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCreate.MouseEnter += (s, e) => btnCreate.BackColor = ControlPaint.Light(Color.FromArgb(46, 204, 113), 0.2f);
            btnCreate.MouseLeave += (s, e) => btnCreate.BackColor = Color.FromArgb(46, 204, 113);
            btnCreate.Click += BtnCreateOrder_Click;
            tbl.Controls.Add(btnCreate, 1, 5);

            this.Controls.Add(lblHeader);
            this.Controls.Add(tbl);
        }

        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.ForeColor = Color.Gray;
            textBox.Text = placeholder;
            textBox.GotFocus += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };
            textBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };
        }

        private void LoadTariffs()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT tariff_id, name, base_cost FROM tariff WHERE tariff_status = 1";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            decimal cost = reader.GetDecimal(2);
                            cmbTariff.Items.Add(new TariffItem(id, $"{name} — {cost} ₽ + за км", cost));
                        }
                    }
                }
                if (cmbTariff.Items.Count > 0) cmbTariff.SelectedIndex = 0;
            }
            catch
            {
                cmbTariff.Items.Add(new TariffItem(1, "Эконом — 150 ₽ + за км", 150));
                cmbTariff.Items.Add(new TariffItem(2, "Комфорт — 250 ₽ + за км", 250));
                cmbTariff.SelectedIndex = 0;
            }
        }

        private void LoadPaymentMethods()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT method_id, method_name FROM payment_method";
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string icon = id == 1 ? "💰 " : id == 2 ? "💳 " : "📱 ";
                            cmbPayment.Items.Add(new PaymentItem(id, icon + name));
                        }
                    }
                }
                if (cmbPayment.Items.Count > 0) cmbPayment.SelectedIndex = 0;
            }
            catch
            {
                cmbPayment.Items.Add(new PaymentItem(1, "💰 Наличные"));
                cmbPayment.Items.Add(new PaymentItem(2, "💳 Карта"));
                cmbPayment.SelectedIndex = 0;
            }
        }

        private void CalculateCost()
        {
            if (cmbTariff.SelectedItem is TariffItem tariff)
            {
                Random rnd = new Random();
                decimal distanceCost = rnd.Next(50, 300);
                decimal total = tariff.BaseCost + distanceCost;
                lblEstimatedCost.Text = $"{total:F0} ₽";
            }
        }

        private void BtnCreateOrder_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFrom.Text) || txtFrom.Text.StartsWith("Воронеж,") && txtFrom.Text.Length < 10 ||
                string.IsNullOrWhiteSpace(txtTo.Text) || txtTo.Text.StartsWith("Воронеж,") && txtTo.Text.Length < 10)
            {
                MessageBox.Show("Пожалуйста, укажите корректные адреса отправления и назначения.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    int driverId = GetAvailableDriver(conn);
                    if (driverId == 0)
                    {
                        MessageBox.Show("К сожалению, сейчас нет свободных водителей. Попробуйте позже.", "Нет водителей", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int addressFromId = CreateAddress(conn, txtFrom.Text);
                    int addressToId = CreateAddress(conn, txtTo.Text);

                    var tariff = cmbTariff.SelectedItem as TariffItem;
                    var payment = cmbPayment.SelectedItem as PaymentItem;

                    decimal finalCost = tariff.BaseCost + new Random().Next(50, 300);

                    string sql = @"
                        INSERT INTO ""Order"" 
                        (client_id, driver_id, tariff_id, order_status, payment_method, 
                         address_from, address_to, order_datetime, final_cost)
                        VALUES 
                        (@clientId, @driverId, @tariffId, 1, @paymentMethod, 
                         @addressFrom, @addressTo, NOW(), @finalCost)
                        RETURNING order_id";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@clientId", clientId);
                        cmd.Parameters.AddWithValue("@driverId", driverId);
                        cmd.Parameters.AddWithValue("@tariffId", tariff.Id);
                        cmd.Parameters.AddWithValue("@paymentMethod", payment.Id);
                        cmd.Parameters.AddWithValue("@addressFrom", addressFromId);
                        cmd.Parameters.AddWithValue("@addressTo", addressToId);
                        cmd.Parameters.AddWithValue("@finalCost", finalCost);

                        int orderId = Convert.ToInt32(cmd.ExecuteScalar());

                        MessageBox.Show($"✅ Заказ №{orderId} создан!\n\n" +
                                      $"Маршрут: {txtFrom.Text} → {txtTo.Text}\n" +
                                      $"Стоимость: {finalCost} ₽\n" +
                                      $"Водитель назначен, ожидайте.",
                                      "Заказ принят", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании заказа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetAvailableDriver(NpgsqlConnection conn)
        {
            string sql = @"
                SELECT driver_id FROM driver 
                WHERE driver_status_id = 1 
                AND driver_id NOT IN (
                    SELECT driver_id FROM work_schedule 
                    WHERE shift_date = CURRENT_DATE AND shift_status_id = 2
                )
                LIMIT 1";
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        private int CreateAddress(NpgsqlConnection conn, string addressText)
        {
            string[] parts = addressText.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string city = parts.Length > 0 ? parts[0] : "Воронеж";
            string street = parts.Length > 1 ? parts[1] : "Неизвестная";
            string house = parts.Length > 2 ? parts[2] : "1";
            string entrance = "1";

            string sql = @"
                INSERT INTO address (city, street, house, entrance)
                VALUES (@city, @street, @house, @entrance)
                RETURNING address_id";
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@street", street);
                cmd.Parameters.AddWithValue("@house", house);
                cmd.Parameters.AddWithValue("@entrance", entrance);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private class TariffItem
        {
            public int Id { get; }
            public string Name { get; }
            public decimal BaseCost { get; }
            public TariffItem(int id, string name, decimal baseCost) { Id = id; Name = name; BaseCost = baseCost; }
            public override string ToString() => Name;
        }

        private class PaymentItem
        {
            public int Id { get; }
            public string Name { get; }
            public PaymentItem(int id, string name) { Id = id; Name = name; }
            public override string ToString() => Name;
        }
    }
}