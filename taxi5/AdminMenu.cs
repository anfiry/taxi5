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
        private bool back = false;
        public AdminMenu()
        {
            InitializeComponent();
        }

        public void OnClosed()
        {
            if (back)
            { back = false; }
            else { Application.Exit(); }
        }

        // Кнопка "Назад" – просто закрывает меню, LoginForm покажется через событие Closed
        private void AdmButtonCancel_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            back = true;
            loginForm.Show();
            this.Close();
        }
            
       
        // Кнопка "Водители"
        private void AdmButtonDrivers_Click(object sender, EventArgs e)
        {
            AdminDriverForm driverForm = new AdminDriverForm();
            driverForm.Closed += (s, args) => driverForm.OnClosed();
            driverForm.Show();
            this.Hide();
        }

        // Кнопка "Автомобили"
        private void AdmButtonCar_Click(object sender, EventArgs e)
        {
            AdminCarForm carForm = new AdminCarForm();
            carForm.Closed += (s, args) => carForm.OnClosed();
            carForm.Show();
            this.Hide();
        }

        // Кнопка "Тарифы"
        private void AdmButtonTarifs_Click(object sender, EventArgs e)
        {
            AdminTariffForm tariffForm = new AdminTariffForm();
            tariffForm.Closed += (s, args) => tariffForm.OnClosed();
            tariffForm.Show();
            this.Hide();
        }

        // Кнопка "Заказы"
        private void AdmButtonOrders_Click(object sender, EventArgs e)
        {
            AdminOrderForm orderForm = new AdminOrderForm();
            orderForm.Closed += (s, args) => orderForm.OnClosed();
            orderForm.Show();
            this.Hide();
        }

        // Кнопка "Акции"
        private void AdmButtonPromotion_Click(object sender, EventArgs e)
        {
            AdminPromotionForm promotionForm = new AdminPromotionForm();
            promotionForm.Closed += (s, args) => promotionForm.OnClosed();
            promotionForm.Show();
            this.Hide();
        }

        private void AdmButtonClient_Click_1(object sender, EventArgs e)
        {
            AdminClientForm clientForm = new AdminClientForm();
            clientForm.Closed += (s, args) => clientForm.OnClosed();
            clientForm.Show();
            this.Hide();
            
        }
    }
}