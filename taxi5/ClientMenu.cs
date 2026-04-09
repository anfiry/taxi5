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
        private bool back = false;
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

        public void OnClosed()
        {
            if (back)
            { back = false; }
            else { Application.Exit(); }
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
            orderForm.Closed += (s, args) => orderForm.OnClosed();
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
            ClientTrackOrderForm trackForm = new ClientTrackOrderForm(this.ClientId, this.AccountId);
            trackForm.Closed += (s, args) => trackForm.OnClosed();
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
            ClientOrderHistoryForm historyForm = new ClientOrderHistoryForm(this.ClientId, this.AccountId);
            historyForm.Closed += (s, args) => historyForm.OnClosed();
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
            ClientPointForm pointForm = new ClientPointForm(this.ClientId, this.AccountId);
            pointForm.Closed += (s, args) => pointForm.OnClosed();
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
            ClientPromotionForm promoForm = new ClientPromotionForm(this.ClientId, this.AccountId);
            promoForm.Closed += (s, args) => promoForm.OnClosed();
            promoForm.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            back = true;
            loginForm.Show();
            this.Close();
        }
    }
}