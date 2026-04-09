using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    public partial class RouteViewForm : Form
    {
        private string startAddress;
        private string endAddress;
        private WebBrowser webBrowser;
        private Button btnOpenInYandex;

        public RouteViewForm(string from, string to)
        {
            InitializeComponent();
            this.startAddress = from;
            this.endAddress = to;
            this.Text = "Маршрут";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeComponents();
            LoadMapWithRoute();
        }

        private void InitializeComponents()
        {
            // Основной WebBrowser
            webBrowser = new WebBrowser();
            webBrowser.Dock = DockStyle.Fill;
            webBrowser.ScriptErrorsSuppressed = true;

            // Кнопка "Открыть в Яндекс.Картах"
            btnOpenInYandex = new Button();
            btnOpenInYandex.Text = "🗺️ ОТКРЫТЬ В ЯНДЕКС.КАРТАХ";
            btnOpenInYandex.Size = new Size(220, 45);
            btnOpenInYandex.Location = new Point(10, 10); // можно разместить где угодно, но лучше внизу или сверху
            btnOpenInYandex.BackColor = Color.FromArgb(52, 152, 219);
            btnOpenInYandex.ForeColor = Color.White;
            btnOpenInYandex.Font = new Font("Arial", 10, FontStyle.Bold);
            btnOpenInYandex.FlatStyle = FlatStyle.Flat;
            btnOpenInYandex.FlatAppearance.BorderSize = 0;
            btnOpenInYandex.Cursor = Cursors.Hand;
            btnOpenInYandex.Click += BtnOpenInYandex_Click;

            // Панель для кнопки (можно разместить сверху)
            Panel topPanel = new Panel();
            topPanel.Dock = DockStyle.Top;
            topPanel.Height = 60;
            topPanel.BackColor = Color.FromArgb(245, 245, 245);
            topPanel.Controls.Add(btnOpenInYandex);
            btnOpenInYandex.Location = new Point(10, 8); // центрирование

            this.Controls.Add(webBrowser);
            this.Controls.Add(topPanel);
        }

        private void LoadMapWithRoute()
        {
            try
            {
                string encodedStart = Uri.EscapeDataString(startAddress);
                string encodedEnd = Uri.EscapeDataString(endAddress);
                string yandexMapsUrl = $"https://yandex.ru/maps/?rtext={encodedStart}~{encodedEnd}&rtt=auto&z=13";
                webBrowser.Navigate(yandexMapsUrl);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки карты: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnOpenInYandex_Click(object sender, EventArgs e)
        {
            try
            {
                string encodedStart = Uri.EscapeDataString(startAddress);
                string encodedEnd = Uri.EscapeDataString(endAddress);
                string url = $"https://yandex.ru/maps/?rtext={encodedStart}~{encodedEnd}&rtt=auto";
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть Яндекс.Карты: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}