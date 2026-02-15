using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace TaxiClientApp
{
    public partial class MyOrdersForm : Form
    {
        private readonly int clientId;
        private string connectionString = "Server=localhost;Port=5432;Database=taxi5;User Id=postgres;Password=135246";

        private DataGridView dgvOrders;
        private ComboBox cmbStatusFilter;
        private TextBox txtSearch;
        private Button btnRefresh;
        private Button btnCancelOrder;
        private Button btnDetails;

        public MyOrdersForm(int clientId, string connectionString = null)
        {
            this.clientId = clientId;
            this.connectionString = connectionString ?? this.connectionString;
            InitializeComponent();
            LoadOrders();
        }

        private void InitializeComponent()
        {
            this.Text = "Мои заказы";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.MinimumSize = new Size(900, 500);

            var toolPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            btnRefresh = CreateToolButton("⟳ Обновить", Color.FromArgb(52, 152, 219));
            btnRefresh.Location = new Point(0, 20);
            btnRefresh.Click += (s, e) => LoadOrders();

            var lblStatus = new Label
            {
                Text = "Статус:",
                Location = new Point(140, 25),
                Size = new Size(60, 30),
                TextAlign = ContentAlignment.MiddleRight,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 80, 80)
            };

            cmbStatusFilter = new ComboBox
            {
                Location = new Point(205, 20),
                Size = new Size(160, 30),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.White
            };
            cmbStatusFilter.Items.AddRange(new[] { "Все", "Создан", "В процессе", "Завершён", "Отменён" });
            cmbStatusFilter.SelectedIndex = 0;
            cmbStatusFilter.SelectedIndexChanged += (s, e) => LoadOrders();

            txtSearch = new TextBox
            {
                Location = new Point(380, 20),
                Size = new Size(250, 30),
                Font = new Font("Segoe UI", 10F),
                Text = "🔍 Поиск по адресу...",
                ForeColor = Color.Gray
            };
            txtSearch.Enter += (s, e) =>
            {
                if (txtSearch.Text == "🔍 Поиск по адресу...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };
            txtSearch.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "🔍 Поиск по адресу...";
                    txtSearch.ForeColor = Color.Gray;
                }
            };
            txtSearch.TextChanged += (s, e) => LoadOrders();

            btnCancelOrder = CreateToolButton("✖ Отменить", Color.FromArgb(231, 76, 60));
            btnCancelOrder.Location = new Point(650, 20);
            btnCancelOrder.Click += BtnCancelOrder_Click;

            btnDetails = CreateToolButton("📄 Подробнее", Color.FromArgb(46, 204, 113));
            btnDetails.Location = new Point(800, 20);
            btnDetails.Click += BtnDetails_Click;

            toolPanel.Controls.AddRange(new Control[]
            {
                btnRefresh,
                lblStatus,
                cmbStatusFilter,
                txtSearch,
                btnCancelOrder,
                btnDetails
            });

            dgvOrders = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                RowTemplate = { Height = 40 },
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(64, 64, 64)
            };

            dgvOrders.EnableHeadersVisualStyles = false;
            dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvOrders.ColumnHeadersHeight = 50;
            dgvOrders.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 249, 249);
            dgvOrders.CellFormatting += DgvOrders_CellFormatting;

            this.Controls.Add(dgvOrders);
            this.Controls.Add(toolPanel);
        }

        private Button CreateToolButton(string text, Color backColor)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(130, 40),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = backColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btn.MouseEnter += (s, e) => btn.BackColor = ControlPaint.Light(backColor, 0.2f);
            btn.MouseLeave += (s, e) => btn.BackColor = backColor;
            return btn;
        }

        private void LoadOrders()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"
                        SELECT 
                            o.order_id AS ""№"",
                            TO_CHAR(o.order_datetime, 'DD.MM.YYYY HH24:MI') AS ""Дата"",
                            af.city || ', ' || af.street || ', д. ' || af.house AS ""Откуда"",
                            at.city || ', ' || at.street || ', д. ' || at.house AS ""Куда"",
                            os.name AS ""Статус"",
                            COALESCE(o.final_cost, 0) || ' ₽' AS ""Стоимость"",
                            COALESCE(d.last_name || ' ' || d.first_name, 'Не назначен') AS ""Водитель"",
                            COALESCE(b.name || ' ' || m.name, '—') AS ""Автомобиль""
                        FROM ""Order"" o
                        JOIN address af ON o.address_from = af.address_id
                        JOIN address at ON o.address_to = at.address_id
                        JOIN order_status os ON o.order_status = os.order_status_id
                        LEFT JOIN driver d ON o.driver_id = d.driver_id
                        LEFT JOIN LATERAL (
                            SELECT c.brand_id, c.model_id
                            FROM car c
                            WHERE c.driver_id = d.driver_id
                            LIMIT 1
                        ) car_data ON true
                        LEFT JOIN brand b ON car_data.brand_id = b.brand_id
                        LEFT JOIN model m ON car_data.model_id = m.model_id
                        WHERE o.client_id = @clientId";

                    if (cmbStatusFilter.SelectedIndex > 0)
                    {
                        string selectedStatus = cmbStatusFilter.SelectedItem.ToString();
                        sql += $" AND os.name ILIKE '%{selectedStatus}%'";
                    }

                    if (!string.IsNullOrWhiteSpace(txtSearch.Text) &&
                        txtSearch.Text != "🔍 Поиск по адресу...")
                    {
                        sql += @" AND (af.city ILIKE @search OR af.street ILIKE @search OR
                                     at.city ILIKE @search OR at.street ILIKE @search)";
                    }

                    sql += " ORDER BY o.order_datetime DESC LIMIT 100";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@clientId", clientId);

                        if (!string.IsNullOrWhiteSpace(txtSearch.Text) &&
                            txtSearch.Text != "🔍 Поиск по адресу...")
                        {
                            cmd.Parameters.AddWithValue("@search", $"%{txtSearch.Text}%");
                        }

                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvOrders.DataSource = dt;
                        }
                    }
                }

                if (dgvOrders.Columns.Count > 0)
                {
                    dgvOrders.Columns["№"].Width = 60;
                    dgvOrders.Columns["Дата"].Width = 130;
                    dgvOrders.Columns["Откуда"].Width = 200;
                    dgvOrders.Columns["Куда"].Width = 200;
                    dgvOrders.Columns["Статус"].Width = 100;
                    dgvOrders.Columns["Стоимость"].Width = 90;
                    dgvOrders.Columns["Водитель"].Width = 150;
                    dgvOrders.Columns["Автомобиль"].Width = 150;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заказов: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvOrders.Columns[e.ColumnIndex].Name == "Статус" && e.Value != null)
            {
                string status = e.Value.ToString();

                if (status.Contains("Создан"))
                    e.CellStyle.ForeColor = Color.FromArgb(52, 152, 219);
                else if (status.Contains("В процессе"))
                    e.CellStyle.ForeColor = Color.FromArgb(241, 196, 15);
                else if (status.Contains("Завершён"))
                    e.CellStyle.ForeColor = Color.FromArgb(46, 204, 113);
                else if (status.Contains("Отменён"))
                    e.CellStyle.ForeColor = Color.FromArgb(231, 76, 60);

                e.CellStyle.Font = new Font(dgvOrders.Font, FontStyle.Bold);
            }
        }

        private void BtnCancelOrder_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заказ для отмены.",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["№"].Value);
            string status = dgvOrders.SelectedRows[0].Cells["Статус"].Value.ToString();

            if (status.Contains("Завершён") || status.Contains("Отменён"))
            {
                MessageBox.Show("Нельзя отменить завершённый или уже отменённый заказ.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Вы уверены, что хотите отменить заказ №{orderId}?",
                "Подтверждение отмены", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (var conn = new NpgsqlConnection(connectionString))
                    {
                        conn.Open();

                        string getStatusSql = "SELECT order_status_id FROM order_status WHERE name ILIKE '%отмен%' LIMIT 1";
                        int cancelStatusId;

                        using (var cmdStatus = new NpgsqlCommand(getStatusSql, conn))
                        {
                            object result = cmdStatus.ExecuteScalar();
                            cancelStatusId = result != null ? Convert.ToInt32(result) : 4;
                        }

                        string updateSql = "UPDATE \"Order\" SET order_status = @status WHERE order_id = @id";
                        using (var cmdUpdate = new NpgsqlCommand(updateSql, conn))
                        {
                            cmdUpdate.Parameters.AddWithValue("@status", cancelStatusId);
                            cmdUpdate.Parameters.AddWithValue("@id", orderId);
                            cmdUpdate.ExecuteNonQuery();
                        }

                        MessageBox.Show($"Заказ №{orderId} отменён.",
                            "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadOrders();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при отмене заказа: {ex.Message}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnDetails_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите заказ для просмотра деталей.",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int orderId = Convert.ToInt32(dgvOrders.SelectedRows[0].Cells["№"].Value);
            MessageBox.Show($"Детали заказа №{orderId}\n\n(Здесь будет полная информация о поездке)",
                "Информация о заказе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}