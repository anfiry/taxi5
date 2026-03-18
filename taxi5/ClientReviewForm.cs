using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace taxi4
{
    public partial class ClientReviewForm : Form
    {
        private int orderId;
        private int clientId;
        private int driverId;
        private ClientOrderHistoryData historyData;

        public ClientReviewForm(int orderId, int clientId, int driverId)
        {
            InitializeComponent();
            this.orderId = orderId;
            this.clientId = clientId;
            this.driverId = driverId;
            this.historyData = new ClientOrderHistoryData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            decimal rating = numericRating.Value;
            string comment = txtComment.Text.Trim();

            if (rating < 1 || rating > 5)
            {
                MessageBox.Show("Оценка должна быть от 1 до 5", "Ошибка");
                return;
            }

            if (historyData.SaveReview(orderId, clientId, driverId, rating, comment))
            {
                MessageBox.Show("Отзыв сохранён", "Успех");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Не удалось сохранить отзыв", "Ошибка");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}