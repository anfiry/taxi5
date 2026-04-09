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
    public partial class BlockUserDialog : Form
    {
        public int SelectedReasonId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public BlockUserDialog(DataTable reasons)
        {
            InitializeComponent();
            comboBoxReason.DataSource = reasons;
            comboBoxReason.DisplayMember = "reason_text";
            comboBoxReason.ValueMember = "reason_id";

            dateTimePickerStart.Value = DateTime.Now;
            checkBoxIndefinite.Checked = true;
            dateTimePickerEnd.Enabled = false;
        }

        private void checkBoxIndefinite_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerEnd.Enabled = !checkBoxIndefinite.Checked;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (comboBoxReason.SelectedValue == null)
            {
                MessageBox.Show("Выберите причину блокировки", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SelectedReasonId = (int)comboBoxReason.SelectedValue;
            StartDate = dateTimePickerStart.Value.Date;
            EndDate = checkBoxIndefinite.Checked ? (DateTime?)null : dateTimePickerEnd.Value.Date;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}