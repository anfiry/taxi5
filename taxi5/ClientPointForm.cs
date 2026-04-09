using Npgsql;
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
        private DataTable allAddresses;
        private int clientId;
        private int accountId;
        private bool back = false;


        public ClientPointForm(int clientId, int accountId)
        {
            InitializeComponent();
            this.clientId = clientId;
            this.accountId = accountId;

            clientPoint = new ClientPoint();

            // Настройка полноэкранного режима
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MinimumSize = new Size(1200, 672);

            LoadAllAddresses();
            LoadPoints();
            LoadPointTypes();
            SetupAutoComplete();
            ClearForm();
            SetupPlaceholderTexts();

            // Скрываем правую панель
            groupBoxPointData.Visible = false;
        }

        public void OnClosed()
        {
            if (back)
            { back = false; }
            else { Application.Exit(); }
        }

        // ---------- ЗАГРУЗКА АДРЕСОВ ДЛЯ АВТОДОПОЛНЕНИЯ ----------
        private void LoadAllAddresses()
        {
            string connStr = "Host=localhost;Port=5432;Database=taxi4;Username=postgres;Password=123";
            using (var conn = new NpgsqlConnection(connStr))
            {
                conn.Open();
                string query = @"
                    SELECT 
                        CONCAT(city, ', ', street, ', д.', house,
                               CASE WHEN entrance IS NOT NULL AND entrance != '' 
                                    THEN ', подъезд ' || entrance ELSE '' END) AS full_address,
                        address_id
                    FROM address
                    ORDER BY city, street, house";
                using (var adapter = new NpgsqlDataAdapter(query, conn))
                {
                    allAddresses = new DataTable();
                    adapter.Fill(allAddresses);
                }
            }
        }

        // ---------- НАСТРОЙКА АВТОДОПОЛНЕНИЯ ----------
        private void SetupAutoComplete()
        {
            var autoCompleteList = new AutoCompleteStringCollection();
            foreach (DataRow row in allAddresses.Rows)
            {
                autoCompleteList.Add(row["full_address"].ToString());
            }

            txtAddress.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtAddress.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtAddress.AutoCompleteCustomSource = autoCompleteList;
        }

        // ---------- ПЛЕЙСХОЛДЕРЫ ----------
        private void SetupPlaceholderTexts()
        {
            SetPlaceholder(txtNewType, "Название типа");
            SetPlaceholder(txtPointName, "Название точки");
            SetPlaceholder(txtAddress, "Воронеж, улица, д. номер, подъезд");
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

            dataGridViewPoints.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Скрываем служебные колонки
            if (dataGridViewPoints.Columns.Contains("address_id"))
                dataGridViewPoints.Columns["address_id"].Visible = false;
            if (dataGridViewPoints.Columns.Contains("point_id"))
                dataGridViewPoints.Columns["point_id"].Visible = false;

            // Настройка колонок
            SetColumnFill("point_name", "Название", 20);
            SetColumnFill("type_name", "Тип", 15);
            SetColumnFill("full_address", "Адрес", 60);  // ОДИН СТОЛБЕЦ ДЛЯ АДРЕСА

            // Стиль заголовков
            dataGridViewPoints.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dataGridViewPoints.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewPoints.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridViewPoints.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewPoints.ColumnHeadersHeight = 40;

            // Стиль строк
            dataGridViewPoints.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9);
            dataGridViewPoints.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 249);
            dataGridViewPoints.RowHeadersVisible = false;
            dataGridViewPoints.ReadOnly = true;
            dataGridViewPoints.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPoints.AllowUserToAddRows = false;
            dataGridViewPoints.AllowUserToDeleteRows = false;
        }

        private void SetColumnFill(string columnName, string headerText, int fillWeight)
        {
            if (dataGridViewPoints.Columns[columnName] != null)
            {
                dataGridViewPoints.Columns[columnName].HeaderText = headerText;
                dataGridViewPoints.Columns[columnName].FillWeight = fillWeight;
                dataGridViewPoints.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
        }

        // ---------- ПОЛУЧЕНИЕ ID АДРЕСА ----------
        private int GetOrCreateAddressId(string addressText)
        {
            if (string.IsNullOrWhiteSpace(addressText))
                return -1;

            // Проверяем существующий адрес
            foreach (DataRow row in allAddresses.Rows)
            {
                if (row["full_address"].ToString().Equals(addressText, StringComparison.OrdinalIgnoreCase))
                    return Convert.ToInt32(row["address_id"]);
            }

            // Парсим адрес
            string[] parts = addressText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 3)
            {
                MessageBox.Show("Адрес должен содержать город, улицу и номер дома.\nПример: Воронеж, Ленина, д.10",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            string city = parts[0].Trim();
            string street = parts[1].Trim();
            string house = "";
            string entrance = "";

            for (int i = 2; i < parts.Length; i++)
            {
                string part = parts[i].Trim().ToLower();
                if (part.Contains("подъезд") || part.Contains("под.") || part.Contains("под"))
                {
                    entrance = part.Replace("подъезд", "").Replace("под.", "").Replace("под", "").Trim();
                }
                else if (string.IsNullOrEmpty(house))
                {
                    string tempHouse = part.Replace("д.", "").Replace("д", "").Replace("дом", "").Trim();
                    if (!string.IsNullOrEmpty(tempHouse))
                    {
                        house = tempHouse;
                    }
                }
            }

            if (string.IsNullOrEmpty(house))
            {
                MessageBox.Show("Не удалось определить номер дома.\nИспользуйте формат: Воронеж, Ленина, д.10",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            if (!city.Equals("Воронеж", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Доставка осуществляется только по городу Воронеж",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            // Добавляем новый адрес
            string connStr = "Host=localhost;Port=5432;Database=taxi4;Username=postgres;Password=123";
            using (var conn = new NpgsqlConnection(connStr))
            {
                conn.Open();
                string insertQuery = @"
                    INSERT INTO address (city, street, house, entrance) 
                    VALUES (@city, @street, @house, @entrance) 
                    RETURNING address_id";
                using (var cmd = new NpgsqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@city", city);
                    cmd.Parameters.AddWithValue("@street", street);
                    cmd.Parameters.AddWithValue("@house", house);
                    cmd.Parameters.AddWithValue("@entrance", string.IsNullOrEmpty(entrance) ? (object)DBNull.Value : entrance);
                    int newId = Convert.ToInt32(cmd.ExecuteScalar());

                    // Обновляем список адресов
                    LoadAllAddresses();
                    SetupAutoComplete();
                    return newId;
                }
            }
        }

        // ---------- КНОПКИ ----------
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            groupBoxPointData.Visible = true;
            groupBoxPointData.Text = "Добавление новой точки";
            textBoxPointId.Visible = false;
            labelPointId.Visible = false;
            txtAddressId.Visible = false;
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
            groupBoxPointData.Visible = true;
            groupBoxPointData.Text = "Редактирование точки";
            textBoxPointId.Visible = false;
            labelPointId.Visible = false;
            txtAddressId.Visible = false;
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

                // Адрес уже в одном столбце
                txtAddress.Text = row.Cells["full_address"].Value.ToString();

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
                string addressText = GetRealText(txtAddress);

                int addressId = GetOrCreateAddressId(addressText);
                if (addressId == -1) return;

                // Парсим адрес для сохранения в таблицу point (из одного поля)
                string[] parts = addressText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string city = parts.Length > 0 ? parts[0].Trim() : "Воронеж";
                string street = parts.Length > 1 ? parts[1].Trim() : "";
                string house = "";
                string entrance = "";

                for (int i = 2; i < parts.Length; i++)
                {
                    string part = parts[i].Trim().ToLower();
                    if (part.Contains("подъезд") || part.Contains("под.") || part.Contains("под"))
                    {
                        entrance = part.Replace("подъезд", "").Replace("под.", "").Replace("под", "").Trim();
                    }
                    else if (string.IsNullOrEmpty(house))
                    {
                        house = part.Replace("д.", "").Replace("д", "").Replace("дом", "").Trim();
                    }
                }

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
                    LoadAllAddresses();
                    SetupAutoComplete();
                    groupBoxPointData.Visible = false;
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
                    LoadAllAddresses();
                    SetupAutoComplete();
                }
            }
        }


        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadPoints();
            LoadPointTypes();
            LoadAllAddresses();
            SetupAutoComplete();
            ClearForm();
            groupBoxPointData.Visible = false;
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
            ClientMenu ClientMenu = new ClientMenu(accountId);
            ClientMenu.Show();
            back = true;
            this.Close();
        }

        private void ClearForm()
        {
            textBoxPointId.Clear();
            txtAddressId.Clear();
            txtPointName.Clear();
            txtAddress.Clear();
            if (comboBoxType.Items.Count > 0) comboBoxType.SelectedIndex = 0;
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
            if (string.IsNullOrWhiteSpace(GetRealText(txtAddress)))
            {
                MessageBox.Show("Введите адрес", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return false;
            }
            string address = GetRealText(txtAddress);
            if (!address.Contains("Воронеж"))
            {
                MessageBox.Show("Адрес должен начинаться с города Воронеж", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
                return false;
            }
            if (!address.Contains("д.") && !address.Contains("д "))
            {
                MessageBox.Show("Укажите номер дома (пример: д.10)", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAddress.Focus();
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

        private void txtAddress_Enter(object sender, EventArgs e) => RemovePlaceholder(txtAddress);
        private void txtAddress_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
                SetPlaceholder(txtAddress, "Воронеж, улица, д. номер, подъезд");
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
                groupBoxPointData.Visible = true;
                groupBoxPointData.Text = "Редактирование точки";
                textBoxPointId.Visible = false;
                labelPointId.Visible = false;
                txtAddressId.Visible = false;
            }
        }
    }
}