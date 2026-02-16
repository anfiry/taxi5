using System;
using System.Windows.Forms;

namespace taxi4
{
    public partial class ClientMenu : Form
    {
        public string Role { get; set; }
        public int AccountId { get; set; }
        public string UserLogin { get; set; }

        public ClientMenu()
        {
            InitializeComponent();
        }

        private void btnNewOrder_Click(object sender, EventArgs e)
        {
            ClientOrderForm orderForm = new ClientOrderForm(this.AccountId);
            orderForm.Show();
            this.Hide();
        }

        private void btnTrackOrder_Click(object sender, EventArgs e)
        {
            // TODO: открыть форму отслеживания поездки
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            // TODO: открыть форму истории поездок
        }

        private void btnMyAddresses_Click(object sender, EventArgs e)
        {
            // TODO: открыть форму моих адресов (точки и метки)
        }

        private void btnPromotions_Click(object sender, EventArgs e)
        {
            // TODO: открыть форму промоакций и скидок
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }
    }
}