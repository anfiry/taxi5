using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    public partial class ClientOrderForm : Form
    {
        private ClientOrderData orderData;
        private int accountId;
        private int clientId;

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

            LoadClientPoints();
            LoadPromotions();
            LoadTariffs();
            LoadPointTypes();
            SetupPlaceholderTexts();
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

        private void LoadClientPoints()
        {
            DataTable dt = orderData.GetClientPoints(clientId);
            cmbStartPoint.DataSource = dt.Copy();
            cmbStartPoint.DisplayMember = "full_address";
            cmbStartPoint.ValueMember = "point_id";

            cmbEndPoint.DataSource = dt.Copy();
            cmbEndPoint.DisplayMember = "full_address";
            cmbEndPoint.ValueMember = "point_id";
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

        private void LoadPointTypes()
        {
            DataTable dt = orderData.GetPointTypes();
            cmbPointType.DataSource = dt;
            cmbPointType.DisplayMember = "name";
            cmbPointType.ValueMember = "point_tupe";
            cmbPointType.SelectedIndex = 0;
        }

        // ---------- ПЛЕЙСХОЛДЕРЫ ----------
        private void SetupPlaceholderTexts()
        {
            SetPlaceholder(txtPointName, "Название точки");
            SetPlaceholder(textBoxCity, "Город");
            SetPlaceholder(textBoxStreet, "Улица");
            SetPlaceholder(textBoxHouse, "Дом");
            SetPlaceholder(textBoxEntrance, "Подъезд (опционально)");
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
            return IsPlaceholderActive(textBox) ? "" : textBox.Text.Trim();
        }

        // ---------- КНОПКИ ----------
        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            if (cmbStartPoint.SelectedValue == null || cmbEndPoint.SelectedValue == null)
            {
                MessageBox.Show("Выберите точки отправления и назначения.", "Предупреждение",
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
                DataRowView selectedTariff = (DataRowView)cmbTariff.SelectedItem;
                decimal baseCost = Convert.ToDecimal(selectedTariff["base_cost"]);
                decimal pricePerKm = Convert.ToDecimal(selectedTariff["price_per_km"]);

                if (!double.TryParse(textBoxDistance.Text, out double distance) || distance <= 0)
                {
                    MessageBox.Show("Введите корректное расстояние (положительное число).", "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal price = baseCost + (decimal)distance * pricePerKm;

                if (cmbPromotion.SelectedValue != null && cmbPromotion.SelectedValue != DBNull.Value)
                {
                    DataRowView selectedPromo = (DataRowView)cmbPromotion.SelectedItem;
                    decimal discount = Convert.ToDecimal(selectedPromo["discont_percent"]);
                    price *= (1 - discount / 100);
                }

                textBoxPrice.Text = price.ToString("F2");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка расчёта: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSaveNewPoint_Click(object sender, EventArgs e)
        {
            string pointName = GetRealText(txtPointName);
            if (string.IsNullOrEmpty(pointName))
                pointName = "Новая точка";

            string city = GetRealText(textBoxCity);
            string street = GetRealText(textBoxStreet);
            string house = GetRealText(textBoxHouse);
            string entrance = GetRealText(textBoxEntrance);

            if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(street) || string.IsNullOrEmpty(house))
            {
                MessageBox.Show("Заполните город, улицу и дом.", "Предупреждение",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int typeId = (int)cmbPointType.SelectedValue;

            try
            {
                buttonSaveNewPoint.Enabled = false;
                int newPointId = orderData.AddPoint(clientId, pointName, typeId, city, street, house, entrance);
                if (newPointId != -1)
                {
                    MessageBox.Show("Адрес успешно добавлен!", "Информация",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearNewAddressFields();
                    SetupPlaceholderTexts();
                    LoadClientPoints();
                    cmbStartPoint.SelectedValue = newPointId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления адреса: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                buttonSaveNewPoint.Enabled = true;
            }
        }

        private void buttonCancelNewPoint_Click(object sender, EventArgs e)
        {
            ClearNewAddressFields();
            SetupPlaceholderTexts();
        }

        private void ClearNewAddressFields()
        {
            foreach (Control c in groupBoxNewAddress.Controls)
            {
                if (c is TextBox tb)
                {
                    tb.Text = "";
                }
            }
        }

        private void buttonOrder_Click(object sender, EventArgs e)
        {
            if (cmbStartPoint.SelectedValue == null || cmbEndPoint.SelectedValue == null)
            {
                MessageBox.Show("Выберите точки отправления и назначения.", "Ошибка",
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
                int orderId = orderData.CreateOrder(
                    clientId,
                    Convert.ToInt32(cmbStartPoint.SelectedValue),
                    Convert.ToInt32(cmbEndPoint.SelectedValue),
                    price,
                    promotionId,
                    tariffId
                );

                MessageBox.Show($"Заказ №{orderId} успешно создан!", "Готово",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Закрываем форму, родительская покажется через событие Closed
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании заказа: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close(); // Просто закрываем форму
        }

        // ---------- Обработчики placeholder'ов ----------
        private void txtPointName_Enter(object sender, EventArgs e) => RemovePlaceholder(txtPointName);
        private void txtPointName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPointName.Text))
                SetPlaceholder(txtPointName, "Название точки");
        }

        private void textBoxCity_Enter(object sender, EventArgs e) => RemovePlaceholder(textBoxCity);
        private void textBoxCity_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxCity.Text))
                SetPlaceholder(textBoxCity, "Город");
        }

        private void textBoxStreet_Enter(object sender, EventArgs e) => RemovePlaceholder(textBoxStreet);
        private void textBoxStreet_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxStreet.Text))
                SetPlaceholder(textBoxStreet, "Улица");
        }

        private void textBoxHouse_Enter(object sender, EventArgs e) => RemovePlaceholder(textBoxHouse);
        private void textBoxHouse_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxHouse.Text))
                SetPlaceholder(textBoxHouse, "Дом");
        }

        private void textBoxEntrance_Enter(object sender, EventArgs e) => RemovePlaceholder(textBoxEntrance);
        private void textBoxEntrance_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxEntrance.Text))
                SetPlaceholder(textBoxEntrance, "Подъезд (опционально)");
        }
    }
}