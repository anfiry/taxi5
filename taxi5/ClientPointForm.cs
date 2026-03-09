using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    public partial class ClientPointForm : Form
    {
        private ClientPoint clientPoint;
        private DataTable pointsData;
        private int clientId;

        public ClientPointForm(int clientId)
        {
            InitializeComponent();
            this.clientId = clientId;
            clientPoint = new ClientPoint();
            LoadPoints();
            LoadPointTypes();
            ClearForm();
            SetupPlaceholderTexts();
        }

        // ---------- ПЛЕЙСХОЛДЕРЫ ----------
        private void SetupPlaceholderTexts()
        {
            SetPlaceholder(txtNewType, "Название типа");
            SetPlaceholder(txtPointName, "Название точки");
            SetPlaceholder(txtCity, "Город");
            SetPlaceholder(txtStreet, "Улица");
            SetPlaceholder(txtHouse, "Дом");
            SetPlaceholder(txtEntrance, "Подъезд");
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
        private void LoadPoints()
        {
            try
            {
                pointsData = clientPoint.GetPointsByClient(clientId);
                dataGridViewPoints.DataSource = pointsData;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки точек: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPointTypes()
        {
            try
            {
                DataTable types = clientPoint.GetPointTypes();
                comboBoxType.DisplayMember = "name";
                comboBoxType.ValueMember = "point_tupe";
                comboBoxType.DataSource = types;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки типов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ---------- НАСТРОЙКА ТАБЛИЦЫ ----------
        private void ConfigureDataGridView()
        {
            if (dataGridViewPoints.Columns.Count == 0) return;

            dataGridViewPoints.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            SetColumn("point_id", "ID", 40, true);
            SetColumn("point_name", "Название", 150, false);
            SetColumn("type_name", "Тип", 100, false);
            SetColumn("city", "Город", 120, false);
            SetColumn("street", "Улица", 150, false);
            SetColumn("house", "Дом", 60, false);
            SetColumn("entrance", "Подъезд", 70, false);
            if (dataGridViewPoints.Columns["address_id"] != null)
                dataGridViewPoints.Columns["address_id"].Visible = false;

            int index = 0;
            string[] order = { "point_id", "point_name", "type_name", "city", "street", "house", "entrance" };
            foreach (string colName in order)
                if (dataGridViewPoints.Columns[colName] != null)
                    dataGridViewPoints.Columns[colName].DisplayIndex = index++;

            dataGridViewPoints.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dataGridViewPoints.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewPoints.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewPoints.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewPoints.ColumnHeadersHeight = 40;

            dataGridViewPoints.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
            dataGridViewPoints.DefaultCellStyle.ForeColor = Color.Black;
            dataGridViewPoints.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dataGridViewPoints.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridViewPoints.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 249);
            dataGridViewPoints.RowHeadersVisible = false;
            dataGridViewPoints.AllowUserToResizeRows = false;
            dataGridViewPoints.ReadOnly = true;
            dataGridViewPoints.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void SetColumn(string columnName, string headerText, int width, bool frozen)
        {
            if (dataGridViewPoints.Columns[columnName] != null)
            {
                dataGridViewPoints.Columns[columnName].HeaderText = headerText;
                dataGridViewPoints.Columns[columnName].Width = width;
                dataGridViewPoints.Columns[columnName].Frozen = frozen;
                dataGridViewPoints.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        // ---------- КНОПКИ ----------
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            groupBoxPointData.Text = "Добавление новой точки";
            textBoxPointId.Visible = false;
            labelPointId.Visible = false;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewPoints.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите точку для редактирования", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dataGridViewPoints.SelectedRows[0];
            LoadPointToForm(row);
            groupBoxPointData.Text = "Редактирование точки";
            textBoxPointId.Visible = true;
            labelPointId.Visible = true;
        }

        private void LoadPointToForm(DataGridViewRow row)
        {
            try
            {
                textBoxPointId.Text = row.Cells["point_id"].Value.ToString();
                txtPointName.Text = row.Cells["point_name"].Value.ToString();

                string typeName = row.Cells["type_name"].Value.ToString();
                foreach (DataRowView item in comboBoxType.Items)
                    if (item["name"].ToString() == typeName)
                    {
                        comboBoxType.SelectedItem = item;
                        break;
                    }

                txtCity.Text = row.Cells["city"].Value.ToString();
                txtStreet.Text = row.Cells["street"].Value.ToString();
                txtHouse.Text = row.Cells["house"].Value.ToString();
                txtEntrance.Text = row.Cells["entrance"].Value.ToString();

                if (row.Cells["address_id"].Value != DBNull.Value)
                    txtAddressId.Text = row.Cells["address_id"].Value.ToString();
                else
                    txtAddressId.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных точки: {ex.Message}", "Ошибка",
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
                string pointName = GetRealText(txtPointName);
                int typeId = (int)comboBoxType.SelectedValue;
                string city = GetRealText(txtCity);
                string street = GetRealText(txtStreet);
                string house = GetRealText(txtHouse);
                string entrance = GetRealText(txtEntrance);

                if (string.IsNullOrEmpty(textBoxPointId.Text)) // добавление
                {
                    success = clientPoint.AddPoint(clientId, pointName, typeId, city, street, house, entrance);
                    if (success)
                        MessageBox.Show("Точка успешно добавлена", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // редактирование
                {
                    int pointId = int.Parse(textBoxPointId.Text);
                    int addressId = int.Parse(txtAddressId.Text);
                    success = clientPoint.UpdatePoint(pointId, pointName, typeId, city, street, house, entrance, addressId);
                    if (success)
                        MessageBox.Show("Данные точки обновлены", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (success)
                {
                    ClearForm();
                    LoadPoints();
                    LoadPointTypes();
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
            if (dataGridViewPoints.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите точку для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataGridViewRow row = dataGridViewPoints.SelectedRows[0];
            int pointId = Convert.ToInt32(row.Cells["point_id"].Value);
            string pointInfo = row.Cells["point_name"].Value.ToString();

            DialogResult result = MessageBox.Show($"Удалить точку '{pointInfo}'?", "Подтверждение удаления",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                if (clientPoint.DeletePoint(pointId))
                {
                    MessageBox.Show("Точка удалена", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadPoints();
                }
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadPoints();
            LoadPointTypes();
            ClearForm();
            MessageBox.Show("Данные обновлены", "Информация",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string search = textBoxSearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(search))
            {
                dataGridViewPoints.DataSource = pointsData;
                return;
            }

            DataView dv = new DataView(pointsData);
            dv.RowFilter = $"point_name LIKE '%{search}%' OR city LIKE '%{search}%' OR street LIKE '%{search}%'";
            dataGridViewPoints.DataSource = dv;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close(); // просто закрываем форму – ClientMenu покажется через событие Closed
        }

        private void ClearForm()
        {
            textBoxPointId.Clear();
            txtAddressId.Clear();
            txtPointName.Clear();
            if (comboBoxType.Items.Count > 0) comboBoxType.SelectedIndex = 0;
            txtCity.Clear();
            txtStreet.Clear();
            txtHouse.Clear();
            txtEntrance.Clear();
            textBoxSearch.Clear();
            groupBoxPointData.Text = "Данные точки";

            panelNewType.Visible = false;
            comboBoxType.Visible = true;
            btnNewType.Visible = true;

            SetupPlaceholderTexts();
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(GetRealText(txtPointName)))
            {
                MessageBox.Show("Введите название точки", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPointName.Focus();
                return false;
            }
            if (comboBoxType.SelectedValue == null)
            {
                MessageBox.Show("Выберите тип точки", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(GetRealText(txtCity)) ||
                string.IsNullOrWhiteSpace(GetRealText(txtStreet)) ||
                string.IsNullOrWhiteSpace(GetRealText(txtHouse)))
            {
                MessageBox.Show("Заполните город, улицу и дом", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        // ---------- ДОБАВЛЕНИЕ НОВОГО ТИПА ----------
        private void btnNewType_Click(object sender, EventArgs e)
        {
            comboBoxType.Visible = false;
            btnNewType.Visible = false;
            panelNewType.Visible = true;
            panelNewType.BringToFront();
            txtNewType.Clear();
            SetPlaceholder(txtNewType, "Название типа");
            txtNewType.Focus();
        }

        private void btnSaveNewType_Click(object sender, EventArgs e)
        {
            string typeName = GetRealText(txtNewType);
            if (string.IsNullOrWhiteSpace(typeName))
            {
                MessageBox.Show("Введите название типа", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int newTypeId = clientPoint.AddPointType(typeName);
            if (newTypeId != -1)
            {
                DataTable types = clientPoint.GetPointTypes();
                comboBoxType.DataSource = types;
                comboBoxType.DisplayMember = "name";
                comboBoxType.ValueMember = "point_tupe";
                comboBoxType.SelectedValue = newTypeId;

                MessageBox.Show("Тип точки успешно добавлен", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                panelNewType.Visible = false;
                comboBoxType.Visible = true;
                btnNewType.Visible = true;
            }
        }

        private void btnCancelNewType_Click(object sender, EventArgs e)
        {
            panelNewType.Visible = false;
            comboBoxType.Visible = true;
            btnNewType.Visible = true;
        }

        // ---------- ПЛЕЙСХОЛДЕРЫ ДЛЯ ТЕКСТОВЫХ ПОЛЕЙ ----------
        private void txtNewType_Enter(object sender, EventArgs e) => RemovePlaceholder(txtNewType);
        private void txtNewType_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewType.Text))
                SetPlaceholder(txtNewType, "Название типа");
        }

        private void txtPointName_Enter(object sender, EventArgs e) => RemovePlaceholder(txtPointName);
        private void txtPointName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPointName.Text))
                SetPlaceholder(txtPointName, "Название");
        }

        private void txtCity_Enter(object sender, EventArgs e) => RemovePlaceholder(txtCity);
        private void txtCity_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCity.Text))
                SetPlaceholder(txtCity, "Город");
        }

        private void txtStreet_Enter(object sender, EventArgs e) => RemovePlaceholder(txtStreet);
        private void txtStreet_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStreet.Text))
                SetPlaceholder(txtStreet, "Улица");
        }

        private void txtHouse_Enter(object sender, EventArgs e) => RemovePlaceholder(txtHouse);
        private void txtHouse_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHouse.Text))
                SetPlaceholder(txtHouse, "Дом");
        }

        private void txtEntrance_Enter(object sender, EventArgs e) => RemovePlaceholder(txtEntrance);
        private void txtEntrance_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEntrance.Text))
                SetPlaceholder(txtEntrance, "Подъезд");
        }

        // ---------- ОГРАНИЧЕНИЯ ВВОДА ----------
        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonSearch_Click(sender, e);
        }

        private void dataGridViewPoints_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewPoints.Rows[e.RowIndex];
                LoadPointToForm(row);
                groupBoxPointData.Text = "Редактирование точки";
                textBoxPointId.Visible = true;
                labelPointId.Visible = true;
            }
        }
    }
}