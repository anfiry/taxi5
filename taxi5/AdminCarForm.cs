using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace taxi4
{
    public partial class AdminCarForm : Form
    {
        private AdminCar adminCar;
        private DataTable carsData;
        private DataTable allBrands;
        private DataTable allModels;
        private DataTable allColors;

        public AdminCarForm()
        {
            InitializeComponent();
            adminCar = new AdminCar();
            LoadCars();
            LoadAllData();
            SetupAutoComplete();
            ClearForm();

            // Скрываем ID
            labelCarId.Visible = false;
            textBoxCarId.Visible = false;
            groupBoxCarData.Visible = false;
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

        private void LoadAllData()
        {
            allBrands = adminCar.GetAllBrands();
            allModels = adminCar.GetAllModels();
            allColors = adminCar.GetAllColors();

            DataTable drivers = adminCar.GetDrivers();
            DataRow emptyRow = drivers.NewRow();
            emptyRow["driver_id"] = 0;
            emptyRow["full_name"] = "Не назначен";
            drivers.Rows.InsertAt(emptyRow, 0);
            comboBoxDriver.DisplayMember = "full_name";
            comboBoxDriver.ValueMember = "driver_id";
            comboBoxDriver.DataSource = drivers;
        }

        // ---------- НАСТРОЙКА АВТОДОПОЛНЕНИЯ ----------
        private void SetupAutoComplete()
        {
            // Марки
            var brandList = new AutoCompleteStringCollection();
            foreach (DataRow row in allBrands.Rows)
            {
                brandList.Add(row["name"].ToString());
            }
            txtBrand.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtBrand.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtBrand.AutoCompleteCustomSource = brandList;

            // Цвета
            var colorList = new AutoCompleteStringCollection();
            foreach (DataRow row in allColors.Rows)
            {
                colorList.Add(row["name"].ToString());
            }
            txtColor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtColor.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtColor.AutoCompleteCustomSource = colorList;

            // Модели - начальный список (все модели)
            UpdateModelAutoComplete();

            // Подписываемся на изменение текста марки
            txtBrand.TextChanged += TxtBrand_TextChanged;
        }

        private void TxtBrand_TextChanged(object sender, EventArgs e)
        {
            UpdateModelAutoComplete();
        }

        private void UpdateModelAutoComplete()
        {
            string brandName = txtBrand.Text.Trim();
            var modelList = new AutoCompleteStringCollection();

            if (!string.IsNullOrEmpty(brandName))
            {
                // Ищем ID марки
                int? brandId = null;
                foreach (DataRow row in allBrands.Rows)
                {
                    if (row["name"].ToString().Equals(brandName, StringComparison.OrdinalIgnoreCase))
                    {
                        brandId = Convert.ToInt32(row["brand_id"]);
                        break;
                    }
                }

                if (brandId.HasValue)
                {
                    // Показываем только модели выбранной марки
                    foreach (DataRow row in allModels.Rows)
                    {
                        if (row["brand_id"] != DBNull.Value && Convert.ToInt32(row["brand_id"]) == brandId.Value)
                        {
                            modelList.Add(row["name"].ToString());
                        }
                    }
                }
                else
                {
                    // Марка не найдена - показываем все модели
                    foreach (DataRow row in allModels.Rows)
                    {
                        modelList.Add(row["name"].ToString());
                    }
                }
            }
            else
            {
                // Марка не выбрана - показываем все модели
                foreach (DataRow row in allModels.Rows)
                {
                    modelList.Add(row["name"].ToString());
                }
            }

            txtModel.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtModel.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtModel.AutoCompleteCustomSource = modelList;
        }

        // ---------- МЕТОДЫ ДЛЯ ПОЛУЧЕНИЯ/СОЗДАНИЯ ID ----------
        private int GetOrCreateBrandId(string brandName)
        {
            if (string.IsNullOrWhiteSpace(brandName))
                return -1;

            // Ищем существующую марку
            foreach (DataRow row in allBrands.Rows)
            {
                if (row["name"].ToString().Equals(brandName, StringComparison.OrdinalIgnoreCase))
                    return Convert.ToInt32(row["brand_id"]);
            }

            // Добавляем новую
            int newId = adminCar.AddBrand(brandName);
            if (newId != -1)
            {
                // Обновляем список марок
                allBrands = adminCar.GetAllBrands();
                var brandList = new AutoCompleteStringCollection();
                foreach (DataRow row in allBrands.Rows)
                {
                    brandList.Add(row["name"].ToString());
                }
                txtBrand.AutoCompleteCustomSource = brandList;
                MessageBox.Show($"Марка \"{brandName}\" добавлена", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return newId;
        }

        private int GetOrCreateModelId(string modelName, int brandId)
        {
            if (string.IsNullOrWhiteSpace(modelName))
                return -1;

            // Ищем существующую модель
            foreach (DataRow row in allModels.Rows)
            {
                if (row["name"].ToString().Equals(modelName, StringComparison.OrdinalIgnoreCase))
                    return Convert.ToInt32(row["model_id"]);
            }

            // Добавляем новую
            int newId = adminCar.AddModel(modelName, brandId);
            if (newId != -1)
            {
                // Обновляем список моделей
                allModels = adminCar.GetAllModels();
                UpdateModelAutoComplete();
                MessageBox.Show($"Модель \"{modelName}\" добавлена", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return newId;
        }

        private int GetOrCreateColorId(string colorName)
        {
            if (string.IsNullOrWhiteSpace(colorName))
                return -1;

            // Ищем существующий цвет
            foreach (DataRow row in allColors.Rows)
            {
                if (row["name"].ToString().Equals(colorName, StringComparison.OrdinalIgnoreCase))
                    return Convert.ToInt32(row["color_id"]);
            }

            // Добавляем новый
            int newId = adminCar.AddColor(colorName);
            if (newId != -1)
            {
                // Обновляем список цветов
                allColors = adminCar.GetAllColors();
                var colorList = new AutoCompleteStringCollection();
                foreach (DataRow row in allColors.Rows)
                {
                    colorList.Add(row["name"].ToString());
                }
                txtColor.AutoCompleteCustomSource = colorList;
                MessageBox.Show($"Цвет \"{colorName}\" добавлен", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return newId;
        }

        // ---------- НАСТРОЙКА ТАБЛИЦЫ ----------
        private void ConfigureDataGridView()
        {
            if (dataGridViewCars.Columns.Count == 0) return;

            string[] hiddenColumns = { "car_id", "driver_id", "brand_id", "model_id", "color_id" };
            foreach (string col in hiddenColumns)
                if (dataGridViewCars.Columns.Contains(col))
                    dataGridViewCars.Columns[col].Visible = false;

            var columnsConfig = new (string Name, string Header, int Width, int DisplayIndex)[]
            {
                ("brand_name", "Марка", 120, 0),
                ("model_name", "Модель", 140, 1),
                ("color_name", "Цвет", 100, 2),
                ("license_number", "Госномер", 100, 3),
                ("region_code", "Регион", 70, 4),
                ("year_of_manufacture", "Год", 80, 5),
                ("driver_name", "Водитель", 200, 6)
            };

            foreach (var col in columnsConfig)
            {
                if (dataGridViewCars.Columns[col.Name] != null)
                {
                    dataGridViewCars.Columns[col.Name].HeaderText = col.Header;
                    dataGridViewCars.Columns[col.Name].Width = col.Width;
                    dataGridViewCars.Columns[col.Name].DisplayIndex = col.DisplayIndex;
                    dataGridViewCars.Columns[col.Name].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
            }

            dataGridViewCars.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dataGridViewCars.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewCars.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewCars.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCars.ColumnHeadersHeight = 40;
            dataGridViewCars.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
            dataGridViewCars.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 249);
            dataGridViewCars.RowHeadersVisible = false;
            dataGridViewCars.ReadOnly = true;
            dataGridViewCars.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCars.AllowUserToAddRows = false;
            dataGridViewCars.AllowUserToDeleteRows = false;
        }

        // ---------- ЗАГРУЗКА ДАННЫХ В ФОРМУ ----------
        private void LoadCarToForm(DataGridViewRow row)
        {
            try
            {
                textBoxCarId.Text = row.Cells["car_id"].Value.ToString();

                txtBrand.Text = row.Cells["brand_name"].Value.ToString();
                txtModel.Text = row.Cells["model_name"].Value.ToString();
                txtColor.Text = row.Cells["color_name"].Value.ToString();

                textBoxLicenseNumber.Text = row.Cells["license_number"].Value.ToString();
                textBoxRegionCode.Text = row.Cells["region_code"].Value.ToString();
                textBoxYear.Text = row.Cells["year_of_manufacture"].Value.ToString();

                // ИСПРАВЛЕНО: проверка на DBNull
                if (row.Cells["driver_id"].Value != DBNull.Value)
                {
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
                else
                {
                    comboBoxDriver.SelectedIndex = 0; // "Не назначен"
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных автомобиля: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- КНОПКИ ----------
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            groupBoxCarData.Visible = true;
            groupBoxCarData.Text = "Добавление нового автомобиля";
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
            groupBoxCarData.Visible = true;

            groupBoxCarData.Text = "Редактирование автомобиля";
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                bool success;

                string brandName = txtBrand.Text.Trim();
                string modelName = txtModel.Text.Trim();
                string colorName = txtColor.Text.Trim();

                int brandId = GetOrCreateBrandId(brandName);
                if (brandId == -1)
                {
                    MessageBox.Show("Не удалось определить марку", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int modelId = GetOrCreateModelId(modelName, brandId);
                if (modelId == -1)
                {
                    MessageBox.Show("Не удалось определить модель", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int colorId = GetOrCreateColorId(colorName);
                if (colorId == -1)
                {
                    MessageBox.Show("Не удалось определить цвет", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string licenseNumber = textBoxLicenseNumber.Text.Trim().ToUpper();
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
                        groupBoxCarData.Visible = false;

                    MessageBox.Show("Данные автомобиля обновлены", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (success)
                {
                    groupBoxCarData.Visible = false;

                    ClearForm();

                    LoadCars();
                    LoadAllData();
                    SetupAutoComplete();
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
                    ClearForm();
                }
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadCars();
            LoadAllData();
            SetupAutoComplete();
            ClearForm();
            groupBoxCarData.Visible = false;

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
            txtBrand.Clear();
            txtModel.Clear();
            txtColor.Clear();
            textBoxLicenseNumber.Clear();
            textBoxRegionCode.Clear();
            textBoxYear.Clear();
            textBoxSearch.Clear();
            if (comboBoxDriver.Items.Count > 0) comboBoxDriver.SelectedIndex = 0;
            groupBoxCarData.Text = "Данные автомобиля";
        }

        private bool ValidateForm()
        {
            // Проверка марки
            if (string.IsNullOrWhiteSpace(txtBrand.Text))
            {
                MessageBox.Show("Введите марку автомобиля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBrand.Focus();
                return false;
            }

            // Проверка модели
            if (string.IsNullOrWhiteSpace(txtModel.Text))
            {
                MessageBox.Show("Введите модель автомобиля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtModel.Focus();
                return false;
            }

            // Проверка цвета
            if (string.IsNullOrWhiteSpace(txtColor.Text))
            {
                MessageBox.Show("Введите цвет автомобиля", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtColor.Focus();
                return false;
            }

            // Проверка госномера (формат: ББЦЦЦБ, где Б - буква, Ц - цифра)
            if (string.IsNullOrWhiteSpace(textBoxLicenseNumber.Text))
            {
                MessageBox.Show("Введите государственный номер", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLicenseNumber.Focus();
                return false;
            }

            string licenseNumber = textBoxLicenseNumber.Text.Trim().ToUpper();
            if (!IsValidLicenseNumber(licenseNumber))
            {
                MessageBox.Show("Неверный формат госномера!\n\n" +
                                "Формат: БЦЦЦББ (6 символов)\n" +
                                "где Б - буква (А, В, Е, К, М, Н, О, Р, С, Т, У, Х)\n" +
                                "где Ц - цифра\n\n" +
                                "Примеры: А123ВС, К456ЕН, Р789УХ",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxLicenseNumber.Focus();
                textBoxLicenseNumber.SelectAll();
                return false;
            }

            // Приводим к правильному формату
            textBoxLicenseNumber.Text = licenseNumber;

            // Проверка кода региона
            if (string.IsNullOrWhiteSpace(textBoxRegionCode.Text))
            {
                MessageBox.Show("Введите код региона", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxRegionCode.Focus();
                return false;
            }

            string regionCode = textBoxRegionCode.Text.Trim();
            if (!int.TryParse(regionCode, out _))
            {
                MessageBox.Show("Код региона должен содержать только цифры", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxRegionCode.Focus();
                textBoxRegionCode.SelectAll();
                return false;
            }

            // Проверка года
            if (string.IsNullOrWhiteSpace(textBoxYear.Text))
            {
                MessageBox.Show("Введите год выпуска", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxYear.Focus();
                return false;
            }

            if (!int.TryParse(textBoxYear.Text, out int year) || year < 1900 || year > DateTime.Now.Year + 1)
            {
                MessageBox.Show($"Введите корректный год (1900-{DateTime.Now.Year + 1})", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxYear.Focus();
                textBoxYear.SelectAll();
                return false;
            }

            return true;
        }

        private bool IsValidLicenseNumber(string licenseNumber)
        {
            if (string.IsNullOrWhiteSpace(licenseNumber) || licenseNumber.Length != 6)
                return false;

            licenseNumber = licenseNumber.ToUpper();

            // Разрешенные буквы для госномера
            string allowedLetters = "АВЕКМНОРСТУХ";

            // Позиции: 0 - буквы, 123 - цифры, 45 - буква
            for (int i = 0; i < licenseNumber.Length; i++)
            {
                char c = licenseNumber[i];

                if (i == 0 || i == 4 || i == 5) // позиции букв (1,2,6 символы)
                {
                    if (!allowedLetters.Contains(c))
                        return false;
                }
                else // позиции 123 - цифры
                {
                    if (!char.IsDigit(c))
                        return false;
                }
            }

            return true;
        }



        // ---------- ОГРАНИЧЕНИЯ ВВОДА ----------
        private void textBoxYear_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void textBoxLicenseNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBoxRegionCode_KeyPress(object sender, KeyPressEventArgs e)
        {
           
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