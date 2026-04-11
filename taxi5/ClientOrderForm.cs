using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taxi4
{
    public partial class ClientOrderForm : Form
    {
        private ClientOrderData orderData;
        private int accountId;
        private int clientId;
        private DataTable allAddresses;
        private bool back = false;

        public void OnClosed()
        {
            if (back)
            { back = false; }
            else { Application.Exit(); }
        }

        public ClientOrderForm(int accountId)
        {
            InitializeComponent();

            this.accountId = accountId;
            orderData = new ClientOrderData();

            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;

            if (!LoadClientInfo())
            {
                this.Close();
                return;
            }

            LoadAllAddresses();
            LoadPromotions();
            LoadTariffs();
            SetupAutoComplete();
        }

        private bool LoadClientInfo()
        {
            DataRow clientRow = orderData.GetClientInfo(accountId);
            if (clientRow == null)
            {
                MessageBox.Show($"Клиент с account_id = {accountId} не найден в таблице client.",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            clientId = Convert.ToInt32(clientRow["client_id"]);
            labelClientName.Text = $"{clientRow["last_name"]} {clientRow["first_name"]}";
            return true;
        }

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

        private void SetupAutoComplete()
        {
            var autoCompleteList = new AutoCompleteStringCollection();
            foreach (DataRow row in allAddresses.Rows)
            {
                autoCompleteList.Add(row["full_address"].ToString());
            }

            cmbStartAddress.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbStartAddress.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbStartAddress.AutoCompleteCustomSource = autoCompleteList;

            cmbEndAddress.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbEndAddress.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cmbEndAddress.AutoCompleteCustomSource = autoCompleteList;
        }

        private void LoadPromotions()
        {
            DataTable dt = orderData.GetAvailablePromotions(clientId);

            // Создаем новую таблицу для отображения
            DataTable displayDt = new DataTable();
            displayDt.Columns.Add("promotion_id", typeof(int));
            displayDt.Columns.Add("name", typeof(string));
            displayDt.Columns.Add("discont_percent", typeof(decimal));

            // Добавляем строку "Без акции"
            DataRow emptyRow = displayDt.NewRow();
            emptyRow["promotion_id"] = DBNull.Value;
            emptyRow["name"] = "Без акции";
            emptyRow["discont_percent"] = 0;
            displayDt.Rows.Add(emptyRow);

            // Добавляем доступные акции
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = displayDt.NewRow();
                    newRow["promotion_id"] = row["promotion_id"];
                    newRow["name"] = row["name"].ToString();
                    newRow["discont_percent"] = row["discont_percent"];
                    displayDt.Rows.Add(newRow);
                }
            }

            cmbPromotion.DataSource = displayDt;
            cmbPromotion.DisplayMember = "name";
            cmbPromotion.ValueMember = "promotion_id";
            cmbPromotion.SelectedIndex = 0;
        }

        private void LoadTariffs()
        {
            DataTable dt = orderData.GetActiveTariffs();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Нет активных тарифов!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            cmbTariff.DataSource = dt;
            cmbTariff.DisplayMember = "name";
            cmbTariff.ValueMember = "tariff_id";
            cmbTariff.SelectedIndex = 0;
        }

        private void btnSelectStartPoint_Click(object sender, EventArgs e)
        {
            string selectedAddress = SelectPointFromList("Выберите точку отправления");
            if (!string.IsNullOrEmpty(selectedAddress))
                cmbStartAddress.Text = selectedAddress;
        }

        private void btnSelectEndPoint_Click(object sender, EventArgs e)
        {
            string selectedAddress = SelectPointFromList("Выберите точку назначения");
            if (!string.IsNullOrEmpty(selectedAddress))
                cmbEndAddress.Text = selectedAddress;
        }

        private string SelectPointFromList(string title)
        {
            // ИСПРАВЛЕНО: используем GetClientPointsWithNames
            DataTable points = orderData.GetClientPointsWithNames(clientId);
            if (points == null || points.Rows.Count == 0)
            {
                MessageBox.Show("У вас нет сохранённых адресов. Добавьте их в разделе «Мои адреса».", "Нет точек",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            using (var selectForm = new Form())
            {
                selectForm.Text = title;
                selectForm.Size = new Size(550, 450);
                selectForm.StartPosition = FormStartPosition.CenterParent;
                selectForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                selectForm.MaximizeBox = false;
                selectForm.MinimizeBox = false;

                ListBox listBox = new ListBox();
                listBox.Dock = DockStyle.Fill;
                listBox.Font = new Font("Microsoft Sans Serif", 10F);

                var addressMap = new Dictionary<string, string>();

                foreach (DataRow row in points.Rows)
                {
                    string pointName = row["point_name"].ToString();
                    string fullAddress = row["full_address"].ToString();
                    string displayText = $"📍 {pointName} - {fullAddress}";
                    listBox.Items.Add(displayText);
                    addressMap[displayText] = fullAddress;
                }

                Button btnOk = new Button() { Text = "✓ Выбрать", Dock = DockStyle.Bottom, Height = 40, Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold) };
                Button btnCancel = new Button() { Text = "✗ Отмена", Dock = DockStyle.Bottom, Height = 40, Font = new Font("Microsoft Sans Serif", 10F) };

                btnOk.Click += (s, e) => selectForm.DialogResult = DialogResult.OK;
                btnCancel.Click += (s, e) => selectForm.DialogResult = DialogResult.Cancel;

                selectForm.Controls.Add(listBox);
                selectForm.Controls.Add(btnOk);
                selectForm.Controls.Add(btnCancel);

                if (selectForm.ShowDialog() == DialogResult.OK && listBox.SelectedItem != null)
                {
                    string selectedDisplay = listBox.SelectedItem.ToString();
                    return addressMap[selectedDisplay];
                }
                return null;
            }
        }

        private bool ValidateAddresses()
        {
            string fromAddress = cmbStartAddress.Text.Trim();
            string toAddress = cmbEndAddress.Text.Trim();

            if (!IsValidAddressFormat(fromAddress))
            {
                MessageBox.Show("Неверный формат адреса отправления.\n\n" +
                                "Используйте формат: Воронеж, улица, д. номер\n" +
                                "Пример: Воронеж, Ленина, д.10",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!IsValidAddressFormat(toAddress))
            {
                MessageBox.Show("Неверный формат адреса назначения.\n\n" +
                                "Используйте формат: Воронеж, улица, д. номер\n" +
                                "Пример: Воронеж, Ленина, д.10",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool IsValidAddressFormat(string address)
        {
            if (string.IsNullOrWhiteSpace(address)) return false;

            if (!address.Contains("Воронеж"))
                return false;

            if (!address.Contains("д.") && !address.Contains(" д") && !address.Contains("дом"))
                return false;

            return true;
        }

        private async void buttonCalculate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbStartAddress.Text) || string.IsNullOrWhiteSpace(cmbEndAddress.Text))
            {
                MessageBox.Show("Введите адреса отправления и назначения.", "Предупреждение",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbTariff.SelectedValue == null)
            {
                MessageBox.Show("Выберите тариф.", "Предупреждение",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                buttonCalculate.Enabled = false;
                buttonCalculate.Text = "Расчёт расстояния...";

                string apiKey = ConfigurationManager.AppSettings["YandexApiKey"];

                string fromAddress = cmbStartAddress.Text.Trim();
                string toAddress = cmbEndAddress.Text.Trim();

                var startCoord = await orderData.GeocodeAddressYandex(fromAddress, apiKey);
                var endCoord = await orderData.GeocodeAddressYandex(toAddress, apiKey);

                if (startCoord.lat == null || endCoord.lat == null)
                {
                    MessageBox.Show("Не удалось определить координаты одного из адресов.");
                    return;
                }

                double distance = orderData.CalculateDistance(
                    startCoord.lat.Value, startCoord.lon.Value,
                    endCoord.lat.Value, endCoord.lon.Value);

                textBoxDistance.Text = distance.ToString("F2");
                textBoxDistance.ReadOnly = true;

                if (double.TryParse(textBoxDistance.Text, out double dist) && dist > 0)
                {
                    DataRowView selectedTariff = (DataRowView)cmbTariff.SelectedItem;
                    decimal baseCost = Convert.ToDecimal(selectedTariff["base_cost"]);
                    decimal pricePerKm = Convert.ToDecimal(selectedTariff["price_per_km"]);
                    decimal price = baseCost + (decimal)dist * pricePerKm;

                    if (cmbPromotion.SelectedValue != null && cmbPromotion.SelectedValue != DBNull.Value)
                    {
                        DataRowView selectedPromo = (DataRowView)cmbPromotion.SelectedItem;
                        decimal discount = Convert.ToDecimal(selectedPromo["discont_percent"]);
                        price *= (1 - discount / 100);
                    }

                    textBoxPrice.Text = price.ToString("F2");
                }
                else
                {
                    MessageBox.Show("Некорректное расстояние.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при расчёте: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttonCalculate.Enabled = true;
                buttonCalculate.Text = "Рассчитать";
            }
        }

        private void buttonOrder_Click(object sender, EventArgs e)
        {
            if (!ValidateAddresses())
                return;

            string fromAddress = cmbStartAddress.Text.Trim();
            string toAddress = cmbEndAddress.Text.Trim();

            if (string.IsNullOrWhiteSpace(fromAddress) || string.IsNullOrWhiteSpace(toAddress))
            {
                MessageBox.Show("Введите адреса отправления и назначения.", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbTariff.SelectedValue == null)
            {
                MessageBox.Show("Выберите тариф.", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(textBoxPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Некорректная стоимость. Выполните расчёт.", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? promotionId = null;
            if (cmbPromotion.SelectedValue != null && cmbPromotion.SelectedValue != DBNull.Value)
            {
                promotionId = Convert.ToInt32(cmbPromotion.SelectedValue);
            }

            int tariffId = Convert.ToInt32(cmbTariff.SelectedValue);

            try
            {
                int orderId = orderData.CreateOrderWithAddresses(
                    clientId,
                    fromAddress,
                    toAddress,
                    price,
                    promotionId,
                    tariffId
                );

                if (orderId > 0)
                {
                    MessageBox.Show($"Заказ №{orderId} успешно создан!", "Готово",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClientMenu ClientMenu = new ClientMenu(accountId);
                    ClientMenu.Show();
                    back = true;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании заказа: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            ClientMenu ClientMenu = new ClientMenu(accountId);
            ClientMenu.Show();
            back = true;
            this.Close();
        }
    }
}