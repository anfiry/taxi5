using System;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    partial class AdminClientForm
    {
        private System.Windows.Forms.ToolTip toolTip;
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewClients;
        private System.Windows.Forms.GroupBox groupBoxClientData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFirstName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLastName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxPatronymic;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPhone;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbAddress;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label ID1;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxAccountId;
        private System.Windows.Forms.Button buttonBlock;
        private System.Windows.Forms.Button buttonUnblock;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewClients = new System.Windows.Forms.DataGridView();
            this.groupBoxClientData = new System.Windows.Forms.GroupBox();
            this.cmbAddress = new System.Windows.Forms.ComboBox();
            this.textBoxAccountId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.ID1 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxPhone = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPatronymic = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonBlock = new System.Windows.Forms.Button();
            this.buttonUnblock = new System.Windows.Forms.Button();
            this.groupBoxClientData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClients)).BeginInit();
            this.SuspendLayout();

            // dataGridViewClients
            this.dataGridViewClients.AllowUserToAddRows = false;
            this.dataGridViewClients.AllowUserToDeleteRows = false;
            this.dataGridViewClients.AllowUserToResizeRows = false;
            this.dataGridViewClients.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridViewClients.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewClients.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewClients.Location = new System.Drawing.Point(25, 116);
            this.dataGridViewClients.Name = "dataGridViewClients";
            this.dataGridViewClients.ReadOnly = true;
            this.dataGridViewClients.RowHeadersVisible = false;
            this.dataGridViewClients.Size = new System.Drawing.Size(999, 691);
            this.dataGridViewClients.TabIndex = 0;
            this.dataGridViewClients.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewClients_CellDoubleClick);

            // groupBoxClientData
            this.groupBoxClientData.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBoxClientData.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBoxClientData.Controls.Add(this.cmbAddress);
            this.groupBoxClientData.Controls.Add(this.textBoxAccountId);
            this.groupBoxClientData.Controls.Add(this.label7);
            this.groupBoxClientData.Controls.Add(this.textBoxID);
            this.groupBoxClientData.Controls.Add(this.ID1);
            this.groupBoxClientData.Controls.Add(this.buttonSave);
            this.groupBoxClientData.Controls.Add(this.label6);
            this.groupBoxClientData.Controls.Add(this.comboBoxStatus);
            this.groupBoxClientData.Controls.Add(this.label5);
            this.groupBoxClientData.Controls.Add(this.textBoxPhone);
            this.groupBoxClientData.Controls.Add(this.label4);
            this.groupBoxClientData.Controls.Add(this.textBoxPatronymic);
            this.groupBoxClientData.Controls.Add(this.label3);
            this.groupBoxClientData.Controls.Add(this.textBoxLastName);
            this.groupBoxClientData.Controls.Add(this.label2);
            this.groupBoxClientData.Controls.Add(this.textBoxFirstName);
            this.groupBoxClientData.Controls.Add(this.label1);
            this.groupBoxClientData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBoxClientData.Location = new System.Drawing.Point(1065, 116);
            this.groupBoxClientData.Name = "groupBoxClientData";
            this.groupBoxClientData.Size = new System.Drawing.Size(422, 691);
            this.groupBoxClientData.TabIndex = 1;
            this.groupBoxClientData.TabStop = false;
            this.groupBoxClientData.Text = "Данные клиента";

            // cmbAddress
            this.cmbAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cmbAddress.FormattingEnabled = true;
            this.cmbAddress.Location = new System.Drawing.Point(136, 388);
            this.cmbAddress.Name = "cmbAddress";
            this.cmbAddress.Size = new System.Drawing.Size(260, 26);
            this.cmbAddress.TabIndex = 12;

            // textBoxAccountId
            this.textBoxAccountId.Location = new System.Drawing.Point(136, 348);
            this.textBoxAccountId.Name = "textBoxAccountId";
            this.textBoxAccountId.Size = new System.Drawing.Size(200, 24);
            this.textBoxAccountId.TabIndex = 19;
            this.textBoxAccountId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxAccountId_KeyPress);

            // label7
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 351);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 18);
            this.label7.Text = "ID аккаунта:";

            // textBoxID
            this.textBoxID.Location = new System.Drawing.Point(136, 128);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.ReadOnly = true;
            this.textBoxID.Size = new System.Drawing.Size(80, 24);
            this.textBoxID.Visible = false;

            // ID1
            this.ID1.AutoSize = true;
            this.ID1.Location = new System.Drawing.Point(36, 131);
            this.ID1.Name = "ID1";
            this.ID1.Size = new System.Drawing.Size(26, 18);
            this.ID1.Text = "ID:";
            this.ID1.Visible = false;

            // buttonSave
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(218, 52, 247);
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSave.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonSave.Location = new System.Drawing.Point(36, 548);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(360, 35);
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);

            // label6
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 391);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 18);
            this.label6.Text = "Адрес:";

            // comboBoxStatus
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(136, 288);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(200, 26);
            this.comboBoxStatus.TabIndex = 10;

            // label5
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(36, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 18);
            this.label5.Text = "Статус:";

            // textBoxPhone
            this.textBoxPhone.Location = new System.Drawing.Point(136, 248);
            this.textBoxPhone.Name = "textBoxPhone";
            this.textBoxPhone.Size = new System.Drawing.Size(200, 24);
            this.textBoxPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPhone_KeyPress);

            // label4
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 251);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 18);
            this.label4.Text = "Телефон:";

            // textBoxPatronymic
            this.textBoxPatronymic.Location = new System.Drawing.Point(136, 208);
            this.textBoxPatronymic.Name = "textBoxPatronymic";
            this.textBoxPatronymic.Size = new System.Drawing.Size(200, 24);

            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 18);
            this.label3.Text = "Отчество:";

            // textBoxLastName
            this.textBoxLastName.Location = new System.Drawing.Point(136, 168);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(200, 24);

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 18);
            this.label2.Text = "Фамилия:";

            // textBoxFirstName
            this.textBoxFirstName.Location = new System.Drawing.Point(136, 128);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(200, 24);

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 18);
            this.label1.Text = "Имя:";

            // buttonAdd
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(251, 228, 255);
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonAdd.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonAdd.Location = new System.Drawing.Point(46, 42);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(120, 40);
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);

            // buttonEdit
            this.buttonEdit.BackColor = System.Drawing.Color.FromArgb(230, 227, 255);
            this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonEdit.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonEdit.Location = new System.Drawing.Point(201, 42);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(159, 40);
            this.buttonEdit.Text = "Редактировать";
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);

            // buttonRefresh
            this.buttonRefresh.BackColor = System.Drawing.Color.FromArgb(251, 228, 255);
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonRefresh.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonRefresh.Location = new System.Drawing.Point(726, 42);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(120, 40);
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);

            // textBoxSearch
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textBoxSearch.Location = new System.Drawing.Point(977, 50);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(180, 24);
            this.textBoxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSearch_KeyDown);

            // label8
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(906, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 18);
            this.label8.Text = "Поиск:";

            // buttonSearch
            this.buttonSearch.BackColor = System.Drawing.Color.FromArgb(230, 227, 255);
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSearch.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonSearch.Location = new System.Drawing.Point(1181, 42);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(90, 40);
            this.buttonSearch.Text = "Найти";
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);

            // buttonBack
            this.buttonBack.BackColor = System.Drawing.Color.FromArgb(255, 227, 227);
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonBack.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonBack.Location = new System.Drawing.Point(1355, 42);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(120, 40);
            this.buttonBack.Text = "Назад";
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);

            // buttonBlock
            this.buttonBlock.BackColor = System.Drawing.Color.FromArgb(255, 200, 200);
            this.buttonBlock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonBlock.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonBlock.Location = new System.Drawing.Point(390, 42);
            this.buttonBlock.Name = "buttonBlock";
            this.buttonBlock.Size = new System.Drawing.Size(144, 40);
            this.buttonBlock.Text = "Заблокировать";
            this.buttonBlock.Click += new System.EventHandler(this.buttonBlock_Click);

            // buttonUnblock
            this.buttonUnblock.BackColor = System.Drawing.Color.FromArgb(200, 255, 200);
            this.buttonUnblock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUnblock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonUnblock.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonUnblock.Location = new System.Drawing.Point(550, 42);
            this.buttonUnblock.Name = "buttonUnblock";
            this.buttonUnblock.Size = new System.Drawing.Size(155, 40);
            this.buttonUnblock.Text = "Разблокировать";
            this.buttonUnblock.Click += new System.EventHandler(this.buttonUnblock_Click);

            // AdminClientForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1510, 848);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonUnblock);
            this.Controls.Add(this.buttonBlock);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.groupBoxClientData);
            this.Controls.Add(this.dataGridViewClients);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.MinimumSize = new System.Drawing.Size(1200, 672);
            this.Name = "AdminClientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление клиентами";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBoxClientData.ResumeLayout(false);
            this.groupBoxClientData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClients)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}