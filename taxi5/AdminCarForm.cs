using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    public partial class AdminCarForm : Form
    {
        private AdminCar adminCar;
        private DataTable carsData;

        public AdminCarForm()
        {
            InitializeComponent();
            adminCar = new AdminCar();
            LoadCars();
            LoadComboBoxData();
            ClearForm();
            SetupPlaceholderTexts();
        }

        // ---------- ПЛЕЙСХОЛДЕРЫ (КАК В КЛИЕНТАХ) ----------
        private void SetupPlaceholderTexts()
        {
            SetPlaceholder(txtNewBrand, "Название марки");
            SetPlaceholder(txtNewModel, "Название модели");
            SetPlaceholder(txtNewColor, "Название цвета");
        }

        private void SetPlaceholder(TextBox textBox, string placeholder)
        {
            textBox.Tag = placeholder;
            textBox.ForeColor = Color.Gray;
            textBox.Text = placeholder;
            textBox.Font = new Font(textBox.Font, FontStyle.Italic);
        }

        private void RemovePlaceholder(TextBox textBox)
        {
            if (textBox.ForeColor == Color.Gray)
            {
                textBox.Text = "";
                textBox.ForeColor = SystemColors.WindowText;
                textBox.Font = new Font(textBox.Font, FontStyle.Regular);
            }
        }

        private bool IsPlaceholderActive(TextBox textBox)
        {
            return textBox.ForeColor == Color.Gray && textBox.Text == textBox.Tag?.ToString();
        }

        private string GetRealText(TextBox textBox)
        {
            return IsPlaceholderActive(textBox) ? "" : textBox.Text;
        }

        // ---------- ЗАГРУЗКА ДАННЫХ ----------
        private void LoadCars()
        {
            try
            {
                carsData = adminCar.GetCars();
                dataGridViewCars.DataSource = carsData;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки автомобилей: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBoxData()
        {
            try
            {
                // Бренды
                DataTable brands = adminCar.GetBrands();
                comboBoxBrand.DisplayMember = "name";
                comboBoxBrand.ValueMember = "brand_id";
                comboBoxBrand.DataSource = brands;

                // Цвета
                DataTable colors = adminCar.GetColors();
                comboBoxColor.DisplayMember = "name";
                comboBoxColor.ValueMember = "color_id";
                comboBoxColor.DataSource = colors;

                // Водители
                DataTable drivers = adminCar.GetDrivers();
                DataRow emptyRow = drivers.NewRow();
                emptyRow["driver_id"] = 0;
                emptyRow["full_name"] = "Не назначен";
                drivers.Rows.InsertAt(emptyRow, 0);
                comboBoxDriver.DisplayMember = "full_name";
                comboBoxDriver.ValueMember = "driver_id";
                comboBoxDriver.DataSource = drivers;

                // Модели (будут загружены при выборе бренда)
                comboBoxModel.DisplayMember = "name";
                comboBoxModel.ValueMember = "model_id";
                comboBoxModel.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки справочников: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- НАСТРОЙКА ТАБЛИЦЫ ----------
        private void ConfigureDataGridView()
        {
            if (dataGridViewCars.Columns.Count == 0) return;

            dataGridViewCars.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            SetColumn("car_id", "ID", 50, true);
            SetColumn("brand_name", "Марка", 100, false);
            SetColumn("model_name", "Модель", 120, false);
            SetColumn("color_name", "Цвет", 100, false);
            SetColumn("license_number", "Госномер", 90, false);
            SetColumn("region_code", "Регион", 70, false);
            SetColumn("year_of_manufacture", "Год", 70, false);
            SetColumn("driver_name", "Водитель", 180, false);
            if (dataGridViewCars.Columns["driver_id"] != null)
                dataGridViewCars.Columns["driver_id"].Visible = false;

            // Порядок колонок
            int index = 0;
            string[] order = {
                "car_id", "brand_name", "model_name", "color_name",
                "license_number", "region_code", "year_of_manufacture", "driver_name"
            };
            foreach (string colName in order)
                if (dataGridViewCars.Columns[colName] != null)
                    dataGridViewCars.Columns[colName].DisplayIndex = index++;

            // Стиль заголовков
            dataGridViewCars.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dataGridViewCars.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewCars.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewCars.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCars.ColumnHeadersHeight = 40;

            // Стиль строк
            dataGridViewCars.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
            dataGridViewCars.DefaultCellStyle.ForeColor = Color.Black;
            dataGridViewCars.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dataGridViewCars.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridViewCars.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 249);
            dataGridViewCars.RowHeadersVisible = false;
            dataGridViewCars.AllowUserToResizeRows = false;
            dataGridViewCars.ReadOnly = true;
            dataGridViewCars.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void SetColumn(string columnName, string headerText, int width, bool frozen)
        {
            if (dataGridViewCars.Columns[columnName] != null)
            {
                dataGridViewCars.Columns[columnName].HeaderText = headerText;
                dataGridViewCars.Columns[columnName].Width = width;
                dataGridViewCars.Columns[columnName].Frozen = frozen;
                dataGridViewCars.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        // ---------- ОБРАБОТЧИКИ КОМБОБОКСОВ ----------
        private void comboBoxBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxBrand.SelectedValue != null && comboBoxBrand.SelectedValue is int)
            {
                int brandId = (int)comboBoxBrand.SelectedValue;
                DataTable models = adminCar.GetModelsByBrand(brandId);
                comboBoxModel.DataSource = models;
                comboBoxModel.DisplayMember = "name";
                comboBoxModel.ValueMember = "model_id";
            }
        }

        // ---------- КНОПКИ ДОБАВЛЕНИЯ / РЕДАКТИРОВАНИЯ / УДАЛЕНИЯ ----------
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            groupBoxCarData.Text = "Добавление нового автомобиля";
            textBoxCarId.Visible = false;
            labelCarId.Visible = false;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewCars.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите автомобиль для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dataGridViewCars.SelectedRows[0];
            LoadCarToForm(row);
            groupBoxCarData.Text = "Редактирование автомобиля";
            textBoxCarId.Visible = true;
            labelCarId.Visible = true;
        }

        private void LoadCarToForm(DataGridViewRow row)
        {
            try
            {
                textBoxCarId.Text = row.Cells["car_id"].Value.ToString();

                // Бренд
                string brandName = row.Cells["brand_name"].Value.ToString();
                foreach (DataRowView item in comboBoxBrand.Items)
                    if (item["name"].ToString() == brandName)
                    {
                        comboBoxBrand.SelectedItem = item;
                        break;
                    }

                // Модель (после загрузки моделей)
                comboBoxBrand_SelectedIndexChanged(null, null);
                Application.DoEvents();

                string modelName = row.Cells["model_name"].Value.ToString();
                if (comboBoxModel.DataSource != null)
                    foreach (DataRowView item in comboBoxModel.Items)
                        if (item["name"].ToString() == modelName)
                        {
                            comboBoxModel.SelectedItem = item;
                            break;
                        }

                // Цвет
                string colorName = row.Cells["color_name"].Value.ToString();
                foreach (DataRowView item in comboBoxColor.Items)
                    if (item["name"].ToString() == colorName)
                    {
                        comboBoxColor.SelectedItem = item;
                        break;
                    }

                // Номер, регион, год
                textBoxLicenseNumber.Text = row.Cells["license_number"].Value.ToString();
                textBoxRegionCode.Text = row.Cells["region_code"].Value.ToString();
                textBoxYear.Text = row.Cells["year_of_manufacture"].Value.ToString();

                // Водитель
                int driverId = Convert.ToInt32(row.Cells["driver_id"].Value);
                if (driverId > 0)
                {
                    foreach (DataRowView item in comboBoxDriver.Items)
                        if (item["driver_id"] != DBNull.Value && (int)item["driver_id"] == driverId)
                        {
                            comboBoxDriver.SelectedItem = item;
                            break;
                        }
                }
                else
                    comboBoxDriver.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных автомобиля: {ex.Message}", "Ошибка",
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
                int brandId = (int)comboBoxBrand.SelectedValue;
                int modelId = (int)comboBoxModel.SelectedValue;
                int colorId = (int)comboBoxColor.SelectedValue;
                string licenseNumber = textBoxLicenseNumber.Text.Trim();
                string regionCode = textBoxRegionCode.Text.Trim();
                int year = int.Parse(textBoxYear.Text);

                int? driverId = null;
                if (comboBoxDriver.SelectedValue != null && (int)comboBoxDriver.SelectedValue != 0)
                    driverId = (int)comboBoxDriver.SelectedValue;

                if (string.IsNullOrEmpty(textBoxCarId.Text))
                {
                    success = adminCar.AddCar(brandId, modelId, colorId, licenseNumber, regionCode, year, driverId);
                    if (success)
                        MessageBox.Show("Автомобиль успешно добавлен", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    int carId = int.Parse(textBoxCarId.Text);
                    success = adminCar.UpdateCar(carId, brandId, modelId, colorId, licenseNumber, regionCode, year, driverId);
                    if (success)
                        MessageBox.Show("Данные автомобиля обновлены", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (success)
                {
                    ClearForm();
                    LoadCars();
                    LoadComboBoxData();
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
            if (dataGridViewCars.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите автомобиль для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dataGridViewCars.SelectedRows[0];
            int carId = Convert.ToInt32(row.Cells["car_id"].Value);
            string carInfo = $"{row.Cells["brand_name"].Value} {row.Cells["model_name"].Value} ({row.Cells["license_number"].Value})";

            DialogResult result = MessageBox.Show($"Удалить автомобиль {carInfo}?", "Подтверждение удаления",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (adminCar.DeleteCar(carId))
                {
                    MessageBox.Show("Автомобиль удалён", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCars();
                }
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadCars();
            LoadComboBoxData();
            ClearForm();
            MessageBox.Show("Данные обновлены", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string search = textBoxSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(search))
            {
                dataGridViewCars.DataSource = carsData;
                return;
            }

            DataView dv = new DataView(carsData);
            dv.RowFilter = $"brand_name LIKE '%{search}%' OR model_name LIKE '%{search}%' OR license_number LIKE '%{search}%' OR driver_name LIKE '%{search}%'";
            dataGridViewCars.DataSource = dv;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
            this.Hide();
        }

        private void ClearForm()
        {
            textBoxCarId.Clear();
            if (comboBoxBrand.Items.Count > 0) comboBoxBrand.SelectedIndex = 0;
            comboBoxModel.DataSource = null;
            if (comboBoxColor.Items.Count > 0) comboBoxColor.SelectedIndex = 0;
            textBoxLicenseNumber.Clear();
            textBoxRegionCode.Clear();
            textBoxYear.Clear();
            if (comboBoxDriver.Items.Count > 0) comboBoxDriver.SelectedIndex = 0;
            textBoxSearch.Clear();
            groupBoxCarData.Text = "Данные автомобиля";

            // Закрыть все панели добавления, если были открыты
            panelNewBrand.Visible = false;
            panelNewModel.Visible = false;
            panelNewColor.Visible = false;
            comboBoxBrand.Visible = true;
            comboBoxModel.Visible = true;
            comboBoxColor.Visible = true;
            btnNewBrand.Visible = true;
            btnNewModel.Visible = true;
            btnNewColor.Visible = true;

            SetupPlaceholderTexts();
        }

        private bool ValidateForm()
        {
            if (comboBoxBrand.SelectedValue == null)
            {
                MessageBox.Show("Выберите марку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (comboBoxModel.SelectedValue == null || comboBoxModel.Items.Count == 0)
            {
                MessageBox.Show("Выберите модель", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (comboBoxColor.SelectedValue == null)
            {
                MessageBox.Show("Выберите цвет", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxLicenseNumber.Text))
            {
                MessageBox.Show("Введите государственный номер", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxRegionCode.Text))
            {
                MessageBox.Show("Введите код региона", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!int.TryParse(textBoxYear.Text, out int year) || year < 1900 || year > DateTime.Now.Year + 1)
            {
                MessageBox.Show($"Введите корректный год (1900-{DateTime.Now.Year + 1})", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        // ---------- ДОБАВЛЕНИЕ НОВОЙ МАРКИ (ПРЯМО В ФОРМЕ) ----------
        private void btnNewBrand_Click(object sender, EventArgs e)
        {
            comboBoxBrand.Visible = false;
            btnNewBrand.Visible = false;
            panelNewBrand.Visible = true;
            panelNewBrand.BringToFront();
            txtNewBrand.Clear();
            SetPlaceholder(txtNewBrand, "Название марки");
            txtNewBrand.Focus();
        }

        private void btnSaveNewBrand_Click(object sender, EventArgs e)
        {
            string brandName = GetRealText(txtNewBrand);
            if (string.IsNullOrWhiteSpace(brandName))
            {
                MessageBox.Show("Введите название марки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int newBrandId = adminCar.AddBrand(brandName);
            if (newBrandId != -1)
            {
                DataTable brands = adminCar.GetBrands();
                comboBoxBrand.DataSource = brands;
                comboBoxBrand.DisplayMember = "name";
                comboBoxBrand.ValueMember = "brand_id";
                comboBoxBrand.SelectedValue = newBrandId;

                MessageBox.Show("Марка успешно добавлена", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                panelNewBrand.Visible = false;
                comboBoxBrand.Visible = true;
                btnNewBrand.Visible = true;
            }
        }

        private void btnCancelNewBrand_Click(object sender, EventArgs e)
        {
            panelNewBrand.Visible = false;
            comboBoxBrand.Visible = true;
            btnNewBrand.Visible = true;
        }

        // ---------- ДОБАВЛЕНИЕ НОВОЙ МОДЕЛИ ----------
        private void btnNewModel_Click(object sender, EventArgs e)
        {
            if (comboBoxBrand.SelectedValue == null)
            {
                MessageBox.Show("Сначала выберите марку", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            comboBoxModel.Visible = false;
            btnNewModel.Visible = false;
            panelNewModel.Visible = true;
            panelNewModel.BringToFront();
            txtNewModel.Clear();
            SetPlaceholder(txtNewModel, "Название модели");
            txtNewModel.Focus();
        }

        private void btnSaveNewModel_Click(object sender, EventArgs e)
        {
            string modelName = GetRealText(txtNewModel);
            if (string.IsNullOrWhiteSpace(modelName))
            {
                MessageBox.Show("Введите название модели", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int brandId = (int)comboBoxBrand.SelectedValue;
            int newModelId = adminCar.AddModel(brandId, modelName);
            if (newModelId != -1)
            {
                DataTable models = adminCar.GetModelsByBrand(brandId);
                comboBoxModel.DataSource = models;
                comboBoxModel.DisplayMember = "name";
                comboBoxModel.ValueMember = "model_id";
                comboBoxModel.SelectedValue = newModelId;

                MessageBox.Show("Модель успешно добавлена", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                panelNewModel.Visible = false;
                comboBoxModel.Visible = true;
                btnNewModel.Visible = true;
            }
        }

        private void btnCancelNewModel_Click(object sender, EventArgs e)
        {
            panelNewModel.Visible = false;
            comboBoxModel.Visible = true;
            btnNewModel.Visible = true;
        }

        // ---------- ДОБАВЛЕНИЕ НОВОГО ЦВЕТА ----------
        private void btnNewColor_Click(object sender, EventArgs e)
        {
            comboBoxColor.Visible = false;
            btnNewColor.Visible = false;
            panelNewColor.Visible = true;
            panelNewColor.BringToFront();
            txtNewColor.Clear();
            SetPlaceholder(txtNewColor, "Название цвета");
            txtNewColor.Focus();
        }

        private void btnSaveNewColor_Click(object sender, EventArgs e)
        {
            string colorName = GetRealText(txtNewColor);
            if (string.IsNullOrWhiteSpace(colorName))
            {
                MessageBox.Show("Введите название цвета", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int newColorId = adminCar.AddColor(colorName);
            if (newColorId != -1)
            {
                DataTable colors = adminCar.GetColors();
                comboBoxColor.DataSource = colors;
                comboBoxColor.DisplayMember = "name";
                comboBoxColor.ValueMember = "color_id";
                comboBoxColor.SelectedValue = newColorId;

                MessageBox.Show("Цвет успешно добавлен", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                panelNewColor.Visible = false;
                comboBoxColor.Visible = true;
                btnNewColor.Visible = true;
            }
        }

        private void btnCancelNewColor_Click(object sender, EventArgs e)
        {
            panelNewColor.Visible = false;
            comboBoxColor.Visible = true;
            btnNewColor.Visible = true;
        }

        // ---------- ПЛЕЙСХОЛДЕРЫ ДЛЯ ТЕКСТОВЫХ ПОЛЕЙ ----------
        private void txtNewBrand_Enter(object sender, EventArgs e) => RemovePlaceholder(txtNewBrand);
        private void txtNewBrand_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewBrand.Text))
                SetPlaceholder(txtNewBrand, "Название марки");
        }

        private void txtNewModel_Enter(object sender, EventArgs e) => RemovePlaceholder(txtNewModel);
        private void txtNewModel_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewModel.Text))
                SetPlaceholder(txtNewModel, "Название модели");
        }

        private void txtNewColor_Enter(object sender, EventArgs e) => RemovePlaceholder(txtNewColor);
        private void txtNewColor_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewColor.Text))
                SetPlaceholder(txtNewColor, "Название цвета");
        }

        // ---------- ОГРАНИЧЕНИЯ ВВОДА ----------
        private void textBoxYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBoxLicenseNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
                e.Handled = true;
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void textBoxRegionCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonSearch_Click(sender, e);
        }

        private void dataGridViewCars_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewCars.Rows[e.RowIndex];
                LoadCarToForm(row);
                groupBoxCarData.Text = "Редактирование автомобиля";
            }
        }
    }
}