using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    public partial class ClientPromotionForm : Form
    {
        private ClientPromotionData promotionData;
        private int clientId;

        public ClientPromotionForm()
        {
            InitializeComponent();
        }

        public ClientPromotionForm(int clientId)
        {
            InitializeComponent();

            this.clientId = clientId;

            // ЗАЩИТА ОТ ДИЗАЙНЕРА
            if (DesignMode) return;

            // Настройка полноэкранного режима
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(1100, 640);

            promotionData = new ClientPromotionData();
            LoadPromotions();
            ConfigureDataGridView();
        }

        private void LoadPromotions()
        {
            try
            {
                if (promotionData == null) return;

                DataTable dt = promotionData.GetAvailablePromotions(clientId);
                dataGridViewPromotions.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    ShowEmptyMessage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки акций: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowEmptyMessage()
        {
            Label lblEmpty = new Label();
            lblEmpty.Text = "Нет доступных акций";
            lblEmpty.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Italic);
            lblEmpty.ForeColor = Color.Gray;
            lblEmpty.Dock = DockStyle.Fill;
            lblEmpty.TextAlign = ContentAlignment.MiddleCenter;
            dataGridViewPromotions.Controls.Add(lblEmpty);
        }

        private void ConfigureDataGridView()
        {
            if (dataGridViewPromotions.Columns.Count == 0) return;

            dataGridViewPromotions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Скрываем колонку ID
            if (dataGridViewPromotions.Columns.Contains("promotion_id"))
                dataGridViewPromotions.Columns["promotion_id"].Visible = false;

            // Настройка колонок
            SetColumnFill("promotion_name", "Название", 20);
            SetColumnFill("discount", "Скидка, %", 8);
            SetColumnFill("start_date", "Начало", 10);
            SetColumnFill("end_date", "Окончание", 10);
            SetColumnFill("description", "Описание", 30);
            SetColumnFill("conditions", "Условия", 22);

            // Форматирование
            if (dataGridViewPromotions.Columns["start_date"] != null)
                dataGridViewPromotions.Columns["start_date"].DefaultCellStyle.Format = "dd.MM.yyyy";
            if (dataGridViewPromotions.Columns["end_date"] != null)
                dataGridViewPromotions.Columns["end_date"].DefaultCellStyle.Format = "dd.MM.yyyy";
            if (dataGridViewPromotions.Columns["discount"] != null)
                dataGridViewPromotions.Columns["discount"].DefaultCellStyle.Format = "F0";

            // Выравнивание
            if (dataGridViewPromotions.Columns["discount"] != null)
                dataGridViewPromotions.Columns["discount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dataGridViewPromotions.Columns["start_date"] != null)
                dataGridViewPromotions.Columns["start_date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (dataGridViewPromotions.Columns["end_date"] != null)
                dataGridViewPromotions.Columns["end_date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Стиль заголовков
            dataGridViewPromotions.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dataGridViewPromotions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewPromotions.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewPromotions.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewPromotions.ColumnHeadersHeight = 40;

            // Стиль строк
            dataGridViewPromotions.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
            dataGridViewPromotions.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 249);
            dataGridViewPromotions.RowHeadersVisible = false;
            dataGridViewPromotions.ReadOnly = true;
            dataGridViewPromotions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPromotions.AllowUserToAddRows = false;
            dataGridViewPromotions.AllowUserToDeleteRows = false;
        }

        private void SetColumnFill(string columnName, string headerText, int fillWeight)
        {
            if (dataGridViewPromotions.Columns[columnName] != null)
            {
                dataGridViewPromotions.Columns[columnName].HeaderText = headerText;
                dataGridViewPromotions.Columns[columnName].FillWeight = fillWeight;
                dataGridViewPromotions.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void dataGridViewPromotions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && promotionData != null)
            {
                int promoId = Convert.ToInt32(dataGridViewPromotions.Rows[e.RowIndex].Cells["promotion_id"].Value);
                ShowPromotionDetails(promoId);
            }
        }

        private void ShowPromotionDetails(int promoId)
        {
            if (promotionData == null) return;

            DataRow details = promotionData.GetPromotionDetails(promoId);
            if (details != null)
            {
                string message = $"Акция: {details["promotion_name"]}\n" +
                                 $"Скидка: {details["discount"]}%\n" +
                                 $"Период: {Convert.ToDateTime(details["start_date"]):dd.MM.yyyy} - {Convert.ToDateTime(details["end_date"]):dd.MM.yyyy}\n\n" +
                                 $"Описание: {details["description"]}\n\n" +
                                 $"Условия: {details["conditions"]}";

                MessageBox.Show(message, "Детали акции", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadPromotions();
            ConfigureDataGridView();
        }
    }
}