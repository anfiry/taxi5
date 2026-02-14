using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    public partial class AdminPromotionForm : Form
    {
        private AdminPromotion adminPromotion;
        private DataTable promotionsData;

        public AdminPromotionForm()
        {
            InitializeComponent();
            adminPromotion = new AdminPromotion();
            LoadPromotions();
            ClearForm();
        }

        // ---------- ЗАГРУЗКА АКЦИЙ ----------
        private void LoadPromotions()
        {
            try
            {
                promotionsData = adminPromotion.GetPromotions();
                dataGridViewPromotions.DataSource = promotionsData;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки акций: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- НАСТРОЙКА ТАБЛИЦЫ ----------
        private void ConfigureDataGridView()
        {
            if (dataGridViewPromotions.Columns.Count == 0) return;

            dataGridViewPromotions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            SetColumn("promotion_id", "ID", 50, true);
            SetColumn("name", "Название", 200, false);
            SetColumn("discont_percent", "Скидка %", 80, false);
            SetColumn("start_date", "Начало", 100, false);
            SetColumn("end_date", "Окончание", 100, false);
            SetColumn("description", "Описание", 250, false);
            SetColumn("conditions", "Условия", 250, false);

            // Порядок колонок
            int index = 0;
            string[] order = {
                "promotion_id", "name", "discont_percent", "start_date",
                "end_date", "description", "conditions"
            };
            foreach (string colName in order)
            {
                if (dataGridViewPromotions.Columns[colName] != null)
                    dataGridViewPromotions.Columns[colName].DisplayIndex = index++;
            }

            // Выравнивание
            if (dataGridViewPromotions.Columns["discont_percent"] != null)
                dataGridViewPromotions.Columns["discont_percent"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

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
                dataGridViewPromotions.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        // ---------- ДЕЙСТВИЯ ----------
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            groupBoxPromotionData.Text = "Добавление новой акции";
            textBoxPromotionId.Visible = false;
            labelPromotionId.Visible = false;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewPromotions.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите акцию для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dataGridViewPromotions.SelectedRows[0];
            LoadPromotionToForm(row);
            groupBoxPromotionData.Text = "Редактирование акции";
            textBoxPromotionId.Visible = true;
            labelPromotionId.Visible = true;
        }

        private void LoadPromotionToForm(DataGridViewRow row)
        {
            try
            {
                textBoxPromotionId.Text = row.Cells["promotion_id"].Value.ToString();
                textBoxName.Text = row.Cells["name"].Value.ToString();
                textBoxDiscount.Text = row.Cells["discont_percent"].Value.ToString().Replace('.', ',');

                // Даты (хранятся в формате dd.MM.yyyy после GetPromotions)
                if (DateTime.TryParseExact(row.Cells["start_date"].Value.ToString(), "dd.MM.yyyy", null,
                    System.Globalization.DateTimeStyles.None, out DateTime start))
                    dateTimePickerStart.Value = start;
                else
                    dateTimePickerStart.Value = DateTime.Now;

                if (DateTime.TryParseExact(row.Cells["end_date"].Value.ToString(), "dd.MM.yyyy", null,
                    System.Globalization.DateTimeStyles.None, out DateTime end))
                    dateTimePickerEnd.Value = end;
                else
                    dateTimePickerEnd.Value = DateTime.Now.AddMonths(1);

                textBoxDescription.Text = row.Cells["description"].Value?.ToString() ?? "";
                textBoxConditions.Text = row.Cells["conditions"].Value?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных акции: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                bool success;
                string name = textBoxName.Text.Trim();
                decimal discount = decimal.Parse(textBoxDiscount.Text);
                DateTime start = dateTimePickerStart.Value.Date;
                DateTime end = dateTimePickerEnd.Value.Date;
                string description = textBoxDescription.Text.Trim();
                string conditions = textBoxConditions.Text.Trim();

                if (string.IsNullOrEmpty(textBoxPromotionId.Text))
                {
                    success = adminPromotion.AddPromotion(name, discount, description, start, end, conditions);
                    if (success)
                        MessageBox.Show("Акция успешно добавлена", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    int promotionId = int.Parse(textBoxPromotionId.Text);
                    success = adminPromotion.UpdatePromotion(promotionId, name, discount, description, start, end, conditions);
                    if (success)
                        MessageBox.Show("Акция успешно обновлена", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (success)
                {
                    ClearForm();
                    LoadPromotions();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewPromotions.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите акцию для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dataGridViewPromotions.SelectedRows[0];
            int promotionId = Convert.ToInt32(row.Cells["promotion_id"].Value);
            string promotionName = row.Cells["name"].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Удалить акцию «{promotionName}»?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (adminPromotion.DeletePromotion(promotionId))
                {
                    MessageBox.Show("Акция удалена", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPromotions();
                    ClearForm();
                }
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadPromotions();
            ClearForm();
            MessageBox.Show("Данные обновлены", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string search = textBoxSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(search))
            {
                dataGridViewPromotions.DataSource = promotionsData;
                return;
            }

            DataView dv = new DataView(promotionsData);
            dv.RowFilter = $"name LIKE '%{search}%' OR description LIKE '%{search}%'";
            dataGridViewPromotions.DataSource = dv;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
            this.Hide();
        }

        private void ClearForm()
        {
            textBoxPromotionId.Clear();
            textBoxName.Clear();
            textBoxDiscount.Clear();
            textBoxDescription.Clear();
            textBoxConditions.Clear();
            textBoxSearch.Clear();

            dateTimePickerStart.Value = DateTime.Now;
            dateTimePickerEnd.Value = DateTime.Now.AddMonths(1);

            groupBoxPromotionData.Text = "Данные акции";
            dataGridViewPromotions.DataSource = promotionsData;
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Введите название акции", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxDiscount.Text))
            {
                MessageBox.Show("Введите процент скидки", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxDiscount.Focus();
                return false;
            }

            if (!decimal.TryParse(textBoxDiscount.Text, out decimal discount) || discount < 0 || discount > 100)
            {
                MessageBox.Show("Введите корректный процент скидки (0-100)", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxDiscount.Focus();
                return false;
            }

            if (dateTimePickerStart.Value.Date > dateTimePickerEnd.Value.Date)
            {
                MessageBox.Show("Дата окончания не может быть раньше даты начала", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        // ---------- ОГРАНИЧЕНИЯ ВВОДА ----------
        private void textBoxDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.' && !char.IsControl(e.KeyChar))
                e.Handled = true;
            if (e.KeyChar == '.')
                e.KeyChar = ',';
        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonSearch_Click(sender, e);
        }

        private void dataGridViewPromotions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewPromotions.Rows[e.RowIndex];
                LoadPromotionToForm(row);
                groupBoxPromotionData.Text = "Редактирование акции";
            }
        }
    }
}