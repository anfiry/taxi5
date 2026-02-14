using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaxiAdminApp;

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

        private void AdmButtonCancel_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void AdmButtonClient_Click(object sender, EventArgs e)
        {
            {
                AdminClientForm clientForm = new AdminClientForm();
                clientForm.Show();
                this.Hide();
            }
        }

        private void AdmButtonDrivers_Click(object sender, EventArgs e)
        {
            AdminDriverForm driverForm = new AdminDriverForm();
            driverForm.Show();
            this.Hide();

        }

        private void AdmButtonCar_Click(object sender, EventArgs e)
        {
            AdminCarForm carForm = new AdminCarForm();
            carForm.Show();
            this.Hide();
        }

        private void AdmButtonTarifs_Click(object sender, EventArgs e)
        {
            AdminTariffForm tariffForm = new AdminTariffForm();
            tariffForm.Show();
            this.Hide();

        }

        private void AdmButtonOrders_Click(object sender, EventArgs e)
        {
            AdminOrderForm orderForm = new AdminOrderForm();
            orderForm.Show();
            this.Hide();
        }

        private void AdmButtonPromotion_Click(object sender, EventArgs e)
        {
            AdminPromotionForm promotionForm = new AdminPromotionForm();
            promotionForm.Show();
            this.Hide();
        }
    }
}
