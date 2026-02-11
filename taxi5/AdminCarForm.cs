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
        }

        // Загрузка списка автомобилей
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

        // Настройка внешнего вида таблицы
        private void ConfigureDataGridView()
        {
            if (dataGridViewCars.Columns.Count == 0) return;

            // Русские заголовки
            dataGridViewCars.Columns["car_id"].HeaderText = "ID";
            dataGridViewCars.Columns["brand_name"].HeaderText = "Марка";
            dataGridViewCars.Columns["model_name"].HeaderText = "Модель";
            dataGridViewCars.Columns["color_name"].HeaderText = "Цвет";
            dataGridViewCars.Columns["license_number"].HeaderText = "Госномер";
            dataGridViewCars.Columns["region_code"].HeaderText = "Регион";
            dataGridViewCars.Columns["year_of_manufacture"].HeaderText = "Год";
            dataGridViewCars.Columns["driver_name"].HeaderText = "Водитель";
            dataGridViewCars.Columns["driver_id"].Visible = false;

            // Ширина колонок
            dataGridViewCars.Columns["car_id"].Width = 50;
            dataGridViewCars.Columns["brand_name"].Width = 100;
            dataGridViewCars.Columns["model_name"].Width = 120;
            dataGridViewCars.Columns["color_name"].Width = 100;
            dataGridViewCars.Columns["license_number"].Width = 90;
            dataGridViewCars.Columns["region_code"].Width = 70;
            dataGridViewCars.Columns["year_of_manufacture"].Width = 70;
            dataGridViewCars.Columns["driver_name"].Width = 180;

            // Стиль
            dataGridViewCars.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dataGridViewCars.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewCars.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewCars.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCars.ColumnHeadersHeight = 40;

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

        // Загрузка данных для комбобоксов
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

        // Обработчик изменения бренда — подгружаем модели
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

        // Кнопка Добавить
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            groupBoxCarData.Text = "Добавление нового автомобиля";
            textBoxCarId.Visible = false;
            labelCarId.Visible = false;
        }

        // Кнопка Редактировать
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

        // Загрузка данных выбранного автомобиля в форму
        private void LoadCarToForm(DataGridViewRow row)
        {
            try
            {
                textBoxCarId.Text = row.Cells["car_id"].Value.ToString();

                // Бренд
                string brandName = row.Cells["brand_name"].Value.ToString();
                foreach (DataRowView item in comboBoxBrand.Items)
                {
                    if (item["name"].ToString() == brandName)
                    {
                        comboBoxBrand.SelectedItem = item;
                        break;
                    }
                }

                // После выбора бренда загрузятся модели. Нужно подождать событие или принудительно вызвать
                // Применим хак: после выбора бренда вручную установим модель
                comboBoxBrand_SelectedIndexChanged(null, null);
                Application.DoEvents();

                // Модель
                string modelName = row.Cells["model_name"].Value.ToString();
                if (comboBoxModel.DataSource != null)
                {
                    foreach (DataRowView item in comboBoxModel.Items)
                    {
                        if (item["name"].ToString() == modelName)
                        {
                            comboBoxModel.SelectedItem = item;
                            break;
                        }
                    }
                }

                // Цвет
                string colorName = row.Cells["color_name"].Value.ToString();
                foreach (DataRowView item in comboBoxColor.Items)
                {
                    if (item["name"].ToString() == colorName)
                    {
                        comboBoxColor.SelectedItem = item;
                        break;
                    }
                }

                // Номер и регион
                textBoxLicenseNumber.Text = row.Cells["license_number"].Value.ToString();
                textBoxRegionCode.Text = row.Cells["region_code"].Value.ToString();
                textBoxYear.Text = row.Cells["year_of_manufacture"].Value.ToString();

                // Водитель
                int driverId = Convert.ToInt32(row.Cells["driver_id"].Value);
                if (driverId > 0)
                {
                    foreach (DataRowView item in comboBoxDriver.Items)
                    {
                        if (item["driver_id"] != DBNull.Value && (int)item["driver_id"] == driverId)
                        {
                            comboBoxDriver.SelectedItem = item;
                            break;
                        }
                    }
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

        // Кнопка Сохранить
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

                if (string.IsNullOrEmpty(textBoxCarId.Text)) // Добавление
                {
                    success = adminCar.AddCar(brandId, modelId, colorId, licenseNumber, regionCode, year, driverId);
                    if (success)
                        MessageBox.Show("Автомобиль успешно добавлен", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // Обновление
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
                    LoadComboBoxData(); // обновить справочники (водители, цвета и т.д.)
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Кнопка Удалить
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

            DialogResult result = MessageBox.Show(
                $"Удалить автомобиль {carInfo}?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

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

        // Кнопка Обновить
        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadCars();
            LoadComboBoxData();
            ClearForm();
            MessageBox.Show("Данные обновлены", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Кнопка Поиск
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

        // Кнопка Назад
        private void buttonBack_Click(object sender, EventArgs e)
        {
            AdminMenu adminMenu = new AdminMenu();
            adminMenu.Show();
            this.Hide();
        }

        // Очистка формы
        private void ClearForm()
        {
            textBoxCarId.Clear();
            if (comboBoxBrand.Items.Count > 0) comboBoxBrand.SelectedIndex = 0;
            comboBoxModel.DataSource = null;
            comboBoxColor.SelectedIndex = 0;
            textBoxLicenseNumber.Clear();
            textBoxRegionCode.Clear();
            textBoxYear.Clear();
            if (comboBoxDriver.Items.Count > 0) comboBoxDriver.SelectedIndex = 0;
            textBoxSearch.Clear();
            groupBoxCarData.Text = "Данные автомобиля";
        }

        // Валидация формы
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

        // Ограничения ввода
        private void textBoxYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void textBoxLicenseNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешены буквы, цифры, дефис, пробел
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
                e.Handled = true;
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