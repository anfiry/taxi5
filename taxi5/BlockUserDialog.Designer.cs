using System;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    partial class BlockUserDialog
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox comboBoxReason;
        private DateTimePicker dateTimePickerStart;
        private DateTimePicker dateTimePickerEnd;
        private CheckBox checkBoxIndefinite;
        private Button buttonOK;
        private Button buttonCancel;
        private Label labelReason;
        private Label labelStart;
        private Label labelEnd;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.comboBoxReason = new ComboBox();
            this.dateTimePickerStart = new DateTimePicker();
            this.dateTimePickerEnd = new DateTimePicker();
            this.checkBoxIndefinite = new CheckBox();
            this.buttonOK = new Button();
            this.buttonCancel = new Button();
            this.labelReason = new Label();
            this.labelStart = new Label();
            this.labelEnd = new Label();
            this.SuspendLayout();

            // labelReason
            this.labelReason.AutoSize = true;
            this.labelReason.Location = new Point(12, 15);
            this.labelReason.Text = "Причина блокировки:";

            // comboBoxReason
            this.comboBoxReason.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxReason.Location = new Point(12, 35);
            this.comboBoxReason.Size = new Size(360, 24);

            // labelStart
            this.labelStart.AutoSize = true;
            this.labelStart.Location = new Point(12, 70);
            this.labelStart.Text = "Дата начала:";

            // dateTimePickerStart
            this.dateTimePickerStart.Location = new Point(12, 90);
            this.dateTimePickerStart.Size = new Size(200, 24);
            this.dateTimePickerStart.Format = DateTimePickerFormat.Short;

            // labelEnd
            this.labelEnd.AutoSize = true;
            this.labelEnd.Location = new Point(12, 125);
            this.labelEnd.Text = "Дата окончания:";

            // dateTimePickerEnd
            this.dateTimePickerEnd.Location = new Point(12, 145);
            this.dateTimePickerEnd.Size = new Size(200, 24);
            this.dateTimePickerEnd.Format = DateTimePickerFormat.Short;

            // checkBoxIndefinite
            this.checkBoxIndefinite.AutoSize = true;
            this.checkBoxIndefinite.Location = new Point(12, 175);
            this.checkBoxIndefinite.Text = "Бессрочно";
            this.checkBoxIndefinite.CheckedChanged += new EventHandler(this.checkBoxIndefinite_CheckedChanged);

            // buttonOK
            this.buttonOK.Location = new Point(150, 210);
            this.buttonOK.Size = new Size(100, 30);
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new EventHandler(this.buttonOK_Click);

            // buttonCancel
            this.buttonCancel.Location = new Point(270, 210);
            this.buttonCancel.Size = new Size(100, 30);
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);

            // BlockUserDialog
            this.ClientSize = new Size(400, 260);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.checkBoxIndefinite);
            this.Controls.Add(this.dateTimePickerEnd);
            this.Controls.Add(this.labelEnd);
            this.Controls.Add(this.dateTimePickerStart);
            this.Controls.Add(this.labelStart);
            this.Controls.Add(this.comboBoxReason);
            this.Controls.Add(this.labelReason);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Блокировка пользователя";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}