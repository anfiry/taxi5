using System;
using System.Windows.Forms;
using taxi4;

namespace taxi4
{
    public partial class AdminMenu : Form
    {
        public string Role { get; set; }
        public int AccountId { get; set; }
        public string UserLogin { get; set; }

        public AdminMenu()
        {
            InitializeComponent();
        }

        // Кнопка "Назад" – просто закрывает меню, LoginForm покажется через событие Closed
        private void AdmButtonCancel_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Closed += (s, args) => this.Show();
            loginForm.Show();
            this.Hide();
        }
            
       
        // Кнопка "Водители"
        private void AdmButtonDrivers_Click(object sender, EventArgs e)
        {
            AdminDriverForm driverForm = new AdminDriverForm();
            driverForm.Closed += (s, args) => this.Show();
            driverForm.Show();
            this.Hide();
        }

        // Кнопка "Автомобили"
        private void AdmButtonCar_Click(object sender, EventArgs e)
        {
            AdminCarForm carForm = new AdminCarForm();
            carForm.Closed += (s, args) => this.Show();
            carForm.Show();
            this.Hide();
        }

        // Кнопка "Тарифы"
        private void AdmButtonTarifs_Click(object sender, EventArgs e)
        {
            AdminTariffForm tariffForm = new AdminTariffForm();
            tariffForm.Closed += (s, args) => this.Show();
            tariffForm.Show();
            this.Hide();
        }

        // Кнопка "Заказы"
        private void AdmButtonOrders_Click(object sender, EventArgs e)
        {
            AdminOrderForm orderForm = new AdminOrderForm();
            orderForm.Closed += (s, args) => this.Show();
            orderForm.Show();
            this.Hide();
        }

        // Кнопка "Акции"
        private void AdmButtonPromotion_Click(object sender, EventArgs e)
        {
            AdminPromotionForm promotionForm = new AdminPromotionForm();
            promotionForm.Closed += (s, args) => this.Show();
            promotionForm.Show();
            this.Hide();
        }

        private void AdmButtonClient_Click_1(object sender, EventArgs e)
        {
            AdminClientForm clientForm = new AdminClientForm();
            clientForm.Closed += (s, args) => this.Show();
            clientForm.Show();
            this.Hide();
        }
    }
}