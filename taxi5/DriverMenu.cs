using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace taxi4
{
    public partial class DriverMenu : Form
    {
        public string Role { get; set; }
        public int AccountId { get; set; }
        public string UserLogin { get; set; }
        public DriverMenu()
        {
            InitializeComponent();
        }
    }
}
