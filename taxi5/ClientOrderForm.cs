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

            // Загружаем данные клиента сразу при открытии
            if (!LoadClientInfo())
            {
                // Если клиент не найден – закрываем форму
                this.Close();
                return;
            }

            LoadClientPoints();
            LoadPromotions();
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
            cmbStartPoint.DisplayMember = "address";
            cmbStartPoint.ValueMember = "point_id";

            cmbEndPoint.DataSource = dt.Copy();
            cmbEndPoint.DisplayMember = "address";
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

        // Placeholder-методы (как в AdminClientForm)
        private void SetupPlaceholderTexts()
        {
            SetPlaceholder(textBoxCity, "Город");
            SetPlaceholder(textBoxStreet, "Улица");
            SetPlaceholder(textBoxHouse, "Дом");
            SetPlaceholder(textBoxEntrance, "Подъезд (опционально)");
            SetPlaceholder(textBoxType, "Метка (дом, работа...)");
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

        // Обработчики событий
        private void buttonCalculate_Click(object sender, EventArgs e)
        {
            decimal basePrice = 100; // Заглушка, можно заменить реальным расчётом
            decimal discount = 0;

            if (cmbPromotion.SelectedValue != null && cmbPromotion.SelectedValue != DBNull.Value)
            {
                DataRowView selectedPromo = (DataRowView)cmbPromotion.SelectedItem;
                discount = Convert.ToDecimal(selectedPromo["discont_percent"]);
            }

            decimal finalPrice = basePrice * (1 - discount / 100);
            textBoxPrice.Text = finalPrice.ToString("F2");
        }

        private void buttonAddNewPoint_Click(object sender, EventArgs e)
        {
            panelNewAddress.Visible = true;
            buttonAddNewPoint.Enabled = false;
        }

        private async void buttonSaveNewPoint_Click(object sender, EventArgs e)
        {
            string city = GetRealText(textBoxCity);
            string street = GetRealText(textBoxStreet);
            string house = GetRealText(textBoxHouse);
            string entrance = GetRealText(textBoxEntrance);
            string type = GetRealText(textBoxType);

            if (string.IsNullOrEmpty(city) || string.IsNullOrEmpty(street) || string.IsNullOrEmpty(house))
            {
                MessageBox.Show("Заполните город, улицу и дом.", "Предупреждение",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                buttonSaveNewPoint.Enabled = false; // Блокируем на время запроса
                int newPointId = await orderData.AddPointWithGeocodingAsync(clientId, city, street, house, entrance, type);
                if (newPointId != -1)
                {
                    MessageBox.Show("Адрес успешно добавлен с координатами!", "Информация",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    panelNewAddress.Visible = false;
                    buttonAddNewPoint.Enabled = true;
                    LoadClientPoints(); // обновить списки
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
            panelNewAddress.Visible = false;
            buttonAddNewPoint.Enabled = true;
            ClearNewAddressFields();
            SetupPlaceholderTexts();
        }

        private void ClearNewAddressFields()
        {
            foreach (Control c in panelNewAddress.Controls)
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

            if (!decimal.TryParse(textBoxPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Некорректная стоимость. Выполните расчёт.", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? promotionId = null;
            if (cmbPromotion.SelectedValue != null && cmbPromotion.SelectedValue != DBNull.Value)
                promotionId = Convert.ToInt32(cmbPromotion.SelectedValue);

            try
            {
                int orderId = orderData.CreateOrder(
                    clientId,
                    Convert.ToInt32(cmbStartPoint.SelectedValue),
                    Convert.ToInt32(cmbEndPoint.SelectedValue),
                    price,
                    promotionId
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

        // Placeholder events
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
        private void textBoxType_Enter(object sender, EventArgs e) => RemovePlaceholder(textBoxType);
        private void textBoxType_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxType.Text))
                SetPlaceholder(textBoxType, "Метка (дом, работа...)");
        }
    }
}