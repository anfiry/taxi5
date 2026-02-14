using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taxi4
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            this.PassField.AutoSize = false;
            this.PassField.Size = new Size(this.PassField.Size.Width, 44);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            меню_водителя driverMenu = new меню_водителя();
            driverMenu.Show();
                }
    }
}
