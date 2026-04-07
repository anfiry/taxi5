using Npgsql;
using System;
using System.Windows.Forms;

namespace taxi4
{
    public partial class ClientMenu : Form
    {
        public string Role { get; set; }
        public int AccountId { get; private set; }
        public string UserLogin { get; set; }
        public int ClientId { get; private set; }

        public ClientMenu(int accountId)
        {
            InitializeComponent();
            AccountId = accountId;

            // Настройка полноэкранного режима
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            LoadClientId();
        }

        public ClientMenu()
        {
            InitializeComponent();
        }

        private void LoadClientId()
        {
            string connectionString = "Host=localhost;Port=5432;Database=taxi4;Username=postgres;Password=123";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT client_id FROM client WHERE account_id = @accountId";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@accountId", AccountId);
                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        ClientId = Convert.ToInt32(result);
                    else
                        ClientId = 0;
                }
            }
        }

        private void btnNewOrder_Click(object sender, EventArgs e)
        {
            ClientOrderForm orderForm = new ClientOrderForm(this.AccountId);
            orderForm.Closed += (s, args) => this.Show();
            orderForm.Show();
            this.Hide();
        }

        private void btnTrackOrder_Click(object sender, EventArgs e)
        {
            if (ClientId == 0)
            {
                MessageBox.Show("Не удалось определить ID клиента. Перезайдите в систему.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ClientTrackOrderForm trackForm = new ClientTrackOrderForm(this.ClientId);
            trackForm.Closed += (s, args) => this.Show();
            trackForm.Show();
            this.Hide();
        }
      
        
        private void btnHistory_Click(object sender, EventArgs e)
        {
            if (ClientId == 0)
            {
                MessageBox.Show("Не удалось определить ID клиента. Перезайдите в систему.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ClientOrderHistoryForm historyForm = new ClientOrderHistoryForm(this.ClientId);
            historyForm.Closed += (s, args) => this.Show();
            historyForm.Show();
            this.Hide();
        }

        private void btnMyAddresses_Click(object sender, EventArgs e)
        {
            if (ClientId == 0)
            {
                MessageBox.Show("Не удалось определить ID клиента. Перезайдите в систему.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ClientPointForm pointForm = new ClientPointForm(this.ClientId);
            pointForm.Closed += (s, args) => this.Show();
            pointForm.Show();
            this.Hide();
        }

        private void btnPromotions_Click(object sender, EventArgs e)
        {
            if (ClientId == 0)
            {
                MessageBox.Show("Не удалось определить ID клиента. Перезайдите в систему.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ClientPromotionForm promoForm = new ClientPromotionForm(this.ClientId);
            promoForm.Closed += (s, args) => this.Show();
            promoForm.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Closed += (s, args) => this.Show();
            loginForm.Show();
            this.Hide();
        }
    }
}