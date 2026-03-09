using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taxi4
{
    public partial class ClientPromotionForm : Form
    {
        private ClientPromotionData promotionData;
        private int clientId;

        // Конструктор для дизайнера (без параметров)
        public ClientPromotionForm()
        {
            InitializeComponent();
        }

        // Основной конструктор, используемый в коде
        public ClientPromotionForm(int clientId) : this()
        {
            this.clientId = clientId;

            if (!DesignMode)
            {
                promotionData = new ClientPromotionData();
                LoadPromotions();
                ConfigureDataGridView();
            }
        }

        private void LoadPromotions()
        {
            try
            {
                DataTable dt = promotionData.GetAvailablePromotions(clientId);
                dataGridViewPromotions.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки акций: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            if (dataGridViewPromotions.Columns.Count == 0) return;

            dataGridViewPromotions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            // Настройка колонок
            SetColumn("promotion_id", "ID", 40, true);
            SetColumn("promotion_name", "Название", 150, false);
            SetColumn("discount", "Скидка, %", 70, false);
            SetColumn("start_date", "Начало", 80, false);
            SetColumn("end_date", "Окончание", 80, false);
            SetColumn("description", "Описание", 200, false);
            SetColumn("conditions", "Условия", 200, false);

            // Форматирование дат
            if (dataGridViewPromotions.Columns["start_date"] != null)
                dataGridViewPromotions.Columns["start_date"].DefaultCellStyle.Format = "dd.MM.yyyy";
            if (dataGridViewPromotions.Columns["end_date"] != null)
                dataGridViewPromotions.Columns["end_date"].DefaultCellStyle.Format = "dd.MM.yyyy";
            if (dataGridViewPromotions.Columns["discount"] != null)
                dataGridViewPromotions.Columns["discount"].DefaultCellStyle.Format = "F1";

            // Порядок колонок
            int index = 0;
            string[] order = { "promotion_id", "promotion_name", "discount", "start_date", "end_date", "description", "conditions" };
            foreach (string colName in order)
                if (dataGridViewPromotions.Columns[colName] != null)
                    dataGridViewPromotions.Columns[colName].DisplayIndex = index++;

            // Стиль заголовков (как в других формах)
            dataGridViewPromotions.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dataGridViewPromotions.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewPromotions.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewPromotions.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewPromotions.ColumnHeadersHeight = 40;

            // Стиль строк
            dataGridViewPromotions.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
            dataGridViewPromotions.DefaultCellStyle.ForeColor = Color.Black;
            dataGridViewPromotions.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dataGridViewPromotions.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridViewPromotions.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 249);
            dataGridViewPromotions.RowHeadersVisible = false;
            dataGridViewPromotions.AllowUserToResizeRows = false;
            dataGridViewPromotions.ReadOnly = true;
            dataGridViewPromotions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void SetColumn(string columnName, string headerText, int width, bool frozen)
        {
            if (dataGridViewPromotions.Columns[columnName] != null)
            {
                dataGridViewPromotions.Columns[columnName].HeaderText = headerText;
                dataGridViewPromotions.Columns[columnName].Width = width;
                dataGridViewPromotions.Columns[columnName].Frozen = frozen;
                dataGridViewPromotions.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadPromotions();
        }

        private void dataGridViewPromotions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int promoId = Convert.ToInt32(dataGridViewPromotions.Rows[e.RowIndex].Cells["promotion_id"].Value);
                ShowPromotionDetails(promoId);
            }
        }

        private void ShowPromotionDetails(int promoId)
        {
            DataRow details = promotionData.GetPromotionDetails(promoId);
            if (details != null)
            {
                string message = $"Акция: {details["promotion_name"]}\n" +
                                 $"Скидка: {details["discount"]}%\n" +
                                 $"Период: {Convert.ToDateTime(details["start_date"]):dd.MM.yyyy} - {Convert.ToDateTime(details["end_date"]):dd.MM.yyyy}\n" +
                                 $"Описание: {details["description"]}\n" +
                                 $"Условия: {details["conditions"]}";
                MessageBox.Show(message, "Детали акции", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}