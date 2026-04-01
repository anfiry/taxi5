using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Npgsql;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Configuration; // для чтения ключа из App.config

namespace taxi4
{
    public partial class ClientOrderForm : Form
    {
        private ClientOrderData orderData;
        private int accountId;
        private int clientId;
        private DataTable allAddresses; // для автодополнения

        public ClientOrderForm(int accountId)
        {
            InitializeComponent();
            this.accountId = accountId;
            orderData = new ClientOrderData();

            if (!LoadClientInfo())
            {
                this.Close();
                return;
            }

            LoadAllAddresses();      // загружаем все адреса для подсказок
            LoadPromotions();
            LoadTariffs();
            SetupAutoComplete();     // настраиваем автодополнение
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
            DataRow emptyRow = dt.NewRow();
            emptyRow["promotion_id"] = DBNull.Value;
            emptyRow["name"] = "Без акции";
            emptyRow["discont_percent"] = 0;
            dt.Rows.InsertAt(emptyRow, 0);

            cmbPromotion.DataSource = dt;
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

        // ---------- КНОПКИ ВЫБОРА ИЗ СОХРАНЁННЫХ ТОЧЕК ----------
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
            DataTable points = orderData.GetClientPoints(clientId);
            if (points == null || points.Rows.Count == 0)
            {
                MessageBox.Show("У вас нет сохранённых адресов. Добавьте их в разделе «Мои точки».", "Нет точек",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            using (var selectForm = new Form())
            {
                selectForm.Text = title;
                selectForm.Size = new Size(400, 300);
                selectForm.StartPosition = FormStartPosition.CenterParent;
                selectForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                selectForm.MaximizeBox = false;
                selectForm.MinimizeBox = false;

                ListBox listBox = new ListBox();
                listBox.Dock = DockStyle.Fill;
                listBox.DisplayMember = "full_address";
                listBox.DataSource = points.Copy();

                Button btnOk = new Button() { Text = "Выбрать", Dock = DockStyle.Bottom, Height = 35 };
                Button btnCancel = new Button() { Text = "Отмена", Dock = DockStyle.Bottom, Height = 35 };
                btnOk.Click += (s, e) => selectForm.DialogResult = DialogResult.OK;
                btnCancel.Click += (s, e) => selectForm.DialogResult = DialogResult.Cancel;

                selectForm.Controls.Add(listBox);
                selectForm.Controls.Add(btnOk);
                selectForm.Controls.Add(btnCancel);

                if (selectForm.ShowDialog() == DialogResult.OK && listBox.SelectedItem != null)
                {
                    DataRowView row = (DataRowView)listBox.SelectedItem;
                    return row["full_address"].ToString();
                }
                return null;
            }
        }

        // ---------- РАСЧЁТ СТОИМОСТИ ----------
        private async void buttonCalculate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Начало расчёта");
            // Проверяем, что адреса введены
            if (string.IsNullOrWhiteSpace(cmbStartAddress.Text) || string.IsNullOrWhiteSpace(cmbEndAddress.Text))
            {
                MessageBox.Show("Введите адреса отправления и назначения.", "Предупреждение",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Проверяем, что выбран тариф
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

                // Получаем ключ из App.config
                string apiKey = System.Configuration.ConfigurationManager.AppSettings["YandexApiKey"];

                string fromAddress = cmbStartAddress.Text.Trim();
                string toAddress = cmbEndAddress.Text.Trim();

                // Получаем координаты
                var startCoord = await orderData.GeocodeAddressYandex(fromAddress, apiKey);
                var endCoord = await orderData.GeocodeAddressYandex(toAddress, apiKey);

                if (startCoord.lat == null || endCoord.lat == null)
                {
                    MessageBox.Show("Не удалось определить координаты одного из адресов.");
                    return;
                }

                // Расстояние по прямой (формула гаверсинуса)
                double distance = orderData.CalculateDistance(
                    startCoord.lat.Value, startCoord.lon.Value,
                    endCoord.lat.Value, endCoord.lon.Value);

                textBoxDistance.Text = distance.ToString("F2");
                textBoxDistance.ReadOnly = true;

                // Расчёт стоимости (та же формула, что и раньше)
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

        // ---------- СОЗДАНИЕ ЗАКАЗА ----------
        private void buttonOrder_Click(object sender, EventArgs e)
        {
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
                promotionId = Convert.ToInt32(cmbPromotion.SelectedValue);

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

                MessageBox.Show($"Заказ №{orderId} успешно создан!", "Готово",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании заказа: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}