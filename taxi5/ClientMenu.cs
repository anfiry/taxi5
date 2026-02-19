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

        // Конструктор с параметром accountId
        public ClientMenu(int accountId)
        {
            InitializeComponent();
            AccountId = accountId;
            LoadClientId();
        }

        // Для совместимости с дизайнером (не удалять)
        public ClientMenu()
        {
            InitializeComponent();
        }

        private void LoadClientId()
        {
            string connectionString = "Server=localhost;Port=5432;Database=taxi4;User Id=postgres;Password=123";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT client_id FROM client WHERE account_id = @accountId";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@accountId", AccountId);
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                        ClientId = Convert.ToInt32(result);
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
            // TODO: открыть форму отслеживания поездки
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            if (ClientId == 0)
            {
                MessageBox.Show("Не удалось определить ID клиента", "Ошибка");
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
                MessageBox.Show("Не удалось определить ID клиента", "Ошибка");
                return;
            }
            ClientPointForm pointForm = new ClientPointForm(this.ClientId);
            pointForm.Closed += (s, args) => this.Show();
            pointForm.Show();
            this.Hide();
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