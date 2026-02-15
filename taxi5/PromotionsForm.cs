using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;

namespace TaxiClientApp
{
    public partial class PromotionsForm : Form
    {
        private readonly int clientId;
        private string connectionString = "Server=localhost;Port=5432;Database=taxi5;User Id=postgres;Password=135246";

        private DataGridView dgvPromotions;
        private TextBox txtSearch;
        private Button btnRefresh;

        public PromotionsForm(int clientId, string connectionString = null)
        {
            this.clientId = clientId;
            this.connectionString = connectionString ?? this.connectionString;
            InitializeComponent();
            LoadPromotions();
        }

        private void InitializeComponent()
        {
            this.Text = "Мои акции";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            this.MinimumSize = new Size(800, 400);

            // Панель инструментов
            var toolPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            btnRefresh = CreateToolButton("⟳ Обновить", Color.FromArgb(52, 152, 219));
            btnRefresh.Location = new Point(0, 15);
            btnRefresh.Click += (s, e) => LoadPromotions();

            txtSearch = new TextBox
            {
                Location = new Point(150, 20),
                Size = new Size(250, 30),
                Font = new Font("Segoe UI", 10F),
                Text = "🔍 Поиск по названию...",
                ForeColor = Color.Gray
            };
            txtSearch.Enter += (s, e) =>
            {
                if (txtSearch.Text == "🔍 Поиск по названию...")
                {
                    txtSearch.Text = "";
                    txtSearch.ForeColor = Color.Black;
                }
            };
            txtSearch.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "🔍 Поиск по названию...";
                    txtSearch.ForeColor = Color.Gray;
                }
            };
            txtSearch.TextChanged += (s, e) => LoadPromotions();

            toolPanel.Controls.AddRange(new Control[] { btnRefresh, txtSearch });

            // DataGridView
            dgvPromotions = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                RowTemplate = { Height = 35 },
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(64, 64, 64)
            };

            dgvPromotions.EnableHeadersVisualStyles = false;
            dgvPromotions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dgvPromotions.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvPromotions.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvPromotions.ColumnHeadersHeight = 40;
            dgvPromotions.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 249, 249);

            // Добавляем на форму
            this.Controls.Add(dgvPromotions);
            this.Controls.Add(toolPanel);
        }

        private Button CreateToolButton(string text, Color backColor)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(120, 35),
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

        private void LoadPromotions()
        {
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = @"
                        SELECT 
                            p.description AS ""Название"",
                            cp.assigned_percent || '%' AS ""Скидка"",
                            TO_CHAR(p.start_date, 'DD.MM.YYYY') AS ""Начало"",
                            TO_CHAR(p.end_date, 'DD.MM.YYYY') AS ""Окончание"",
                            p.conditions AS ""Условия""
                        FROM clent_promotion cp
                        JOIN promotion p ON cp.promotion_id = p.promotion_id
                        WHERE cp.clent_id = @clientId";

                    if (!string.IsNullOrWhiteSpace(txtSearch.Text) &&
                        txtSearch.Text != "🔍 Поиск по названию...")
                    {
                        sql += " AND p.description ILIKE @search";
                    }

                    sql += " ORDER BY p.end_date ASC";

                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@clientId", clientId);

                        if (!string.IsNullOrWhiteSpace(txtSearch.Text) &&
                            txtSearch.Text != "🔍 Поиск по названию...")
                        {
                            cmd.Parameters.AddWithValue("@search", $"%{txtSearch.Text}%");
                        }

                        using (var adapter = new NpgsqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dgvPromotions.DataSource = dt;
                        }
                    }
                }

                // Настройка ширины колонок
                if (dgvPromotions.Columns.Count > 0)
                {
                    dgvPromotions.Columns["Название"].Width = 250;
                    dgvPromotions.Columns["Скидка"].Width = 80;
                    dgvPromotions.Columns["Начало"].Width = 100;
                    dgvPromotions.Columns["Окончание"].Width = 100;
                    dgvPromotions.Columns["Условия"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки акций: {ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
