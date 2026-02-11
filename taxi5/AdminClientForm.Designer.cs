namespace TaxiAdminApp
{
    partial class AdminClientForm
    {
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
        private System.Windows.Forms.ComboBox comboBoxAddress;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxAccountId;
        private System.Windows.Forms.Button buttonNewAddress;
        private System.Windows.Forms.Button buttonSaveAddress;
        private System.Windows.Forms.Button buttonCancelAddress;
        private System.Windows.Forms.TextBox textBoxNewCity;
        private System.Windows.Forms.TextBox textBoxNewStreet;
        private System.Windows.Forms.TextBox textBoxNewHouse;
        private System.Windows.Forms.TextBox textBoxNewEntrance;

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
            this.buttonCancelAddress = new System.Windows.Forms.Button();
            this.buttonSaveAddress = new System.Windows.Forms.Button();
            this.buttonNewAddress = new System.Windows.Forms.Button();
            this.textBoxNewEntrance = new System.Windows.Forms.TextBox();
            this.textBoxNewHouse = new System.Windows.Forms.TextBox();
            this.textBoxNewStreet = new System.Windows.Forms.TextBox();
            this.textBoxNewCity = new System.Windows.Forms.TextBox();
            this.textBoxAccountId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.comboBoxAddress = new System.Windows.Forms.ComboBox();
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
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClients)).BeginInit();
            this.groupBoxClientData.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewClients
            // 
            this.dataGridViewClients.AllowUserToAddRows = false;
            this.dataGridViewClients.AllowUserToDeleteRows = false;
            this.dataGridViewClients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewClients.Location = new System.Drawing.Point(12, 70);
            this.dataGridViewClients.Name = "dataGridViewClients";
            this.dataGridViewClients.ReadOnly = true;
            this.dataGridViewClients.RowHeadersWidth = 51;
            this.dataGridViewClients.Size = new System.Drawing.Size(619, 495);
            this.dataGridViewClients.TabIndex = 0;
            this.dataGridViewClients.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewClients_CellDoubleClick);
            // 
            // groupBoxClientData
            // 
            this.groupBoxClientData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxClientData.Controls.Add(this.buttonCancelAddress);
            this.groupBoxClientData.Controls.Add(this.buttonSaveAddress);
            this.groupBoxClientData.Controls.Add(this.buttonNewAddress);
            this.groupBoxClientData.Controls.Add(this.textBoxNewEntrance);
            this.groupBoxClientData.Controls.Add(this.textBoxNewHouse);
            this.groupBoxClientData.Controls.Add(this.textBoxNewStreet);
            this.groupBoxClientData.Controls.Add(this.textBoxNewCity);
            this.groupBoxClientData.Controls.Add(this.textBoxAccountId);
            this.groupBoxClientData.Controls.Add(this.label7);
            this.groupBoxClientData.Controls.Add(this.textBoxID);
            this.groupBoxClientData.Controls.Add(this.label9);
            this.groupBoxClientData.Controls.Add(this.buttonSave);
            this.groupBoxClientData.Controls.Add(this.comboBoxAddress);
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
            this.groupBoxClientData.Location = new System.Drawing.Point(704, 70);
            this.groupBoxClientData.Name = "groupBoxClientData";
            this.groupBoxClientData.Size = new System.Drawing.Size(390, 495);
            this.groupBoxClientData.TabIndex = 1;
            this.groupBoxClientData.TabStop = false;
            this.groupBoxClientData.Text = "Данные клиента";
            // 
            // buttonCancelAddress
            // 
            this.buttonCancelAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.buttonCancelAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancelAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.buttonCancelAddress.ForeColor = System.Drawing.Color.White;
            this.buttonCancelAddress.Location = new System.Drawing.Point(290, 350);
            this.buttonCancelAddress.Name = "buttonCancelAddress";
            this.buttonCancelAddress.Size = new System.Drawing.Size(90, 25);
            this.buttonCancelAddress.TabIndex = 26;
            this.buttonCancelAddress.Text = "Отмена";
            this.buttonCancelAddress.UseVisualStyleBackColor = false;
            this.buttonCancelAddress.Visible = false;
            this.buttonCancelAddress.Click += new System.EventHandler(this.buttonCancelAddress_Click);
            // 
            // buttonSaveAddress
            // 
            this.buttonSaveAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.buttonSaveAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.buttonSaveAddress.ForeColor = System.Drawing.Color.White;
            this.buttonSaveAddress.Location = new System.Drawing.Point(290, 320);
            this.buttonSaveAddress.Name = "buttonSaveAddress";
            this.buttonSaveAddress.Size = new System.Drawing.Size(90, 25);
            this.buttonSaveAddress.TabIndex = 25;
            this.buttonSaveAddress.Text = "Сохранить";
            this.buttonSaveAddress.UseVisualStyleBackColor = false;
            this.buttonSaveAddress.Visible = false;
            this.buttonSaveAddress.Click += new System.EventHandler(this.buttonSaveAddress_Click);
            // 
            // buttonNewAddress
            // 
            this.buttonNewAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.buttonNewAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNewAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.buttonNewAddress.ForeColor = System.Drawing.Color.White;
            this.buttonNewAddress.Location = new System.Drawing.Point(290, 285);
            this.buttonNewAddress.Name = "buttonNewAddress";
            this.buttonNewAddress.Size = new System.Drawing.Size(90, 25);
            this.buttonNewAddress.TabIndex = 24;
            this.buttonNewAddress.Text = "Новый адрес";
            this.buttonNewAddress.UseVisualStyleBackColor = false;
            this.buttonNewAddress.Click += new System.EventHandler(this.buttonNewAddress_Click);
            // 
            // textBoxNewEntrance
            // 
            this.textBoxNewEntrance.Location = new System.Drawing.Point(210, 350);
            this.textBoxNewEntrance.Name = "textBoxNewEntrance";
            this.textBoxNewEntrance.Size = new System.Drawing.Size(70, 22);
            this.textBoxNewEntrance.TabIndex = 23;
            this.textBoxNewEntrance.Visible = false;
            this.textBoxNewEntrance.Enter += new System.EventHandler(this.textBoxNewEntrance_Enter);
            this.textBoxNewEntrance.Leave += new System.EventHandler(this.textBoxNewEntrance_Leave);
            // 
            // textBoxNewHouse
            // 
            this.textBoxNewHouse.Location = new System.Drawing.Point(120, 350);
            this.textBoxNewHouse.Name = "textBoxNewHouse";
            this.textBoxNewHouse.Size = new System.Drawing.Size(70, 22);
            this.textBoxNewHouse.TabIndex = 22;
            this.textBoxNewHouse.Visible = false;
            this.textBoxNewHouse.Enter += new System.EventHandler(this.textBoxNewHouse_Enter);
            this.textBoxNewHouse.Leave += new System.EventHandler(this.textBoxNewHouse_Leave);
            // 
            // textBoxNewStreet
            // 
            this.textBoxNewStreet.Location = new System.Drawing.Point(120, 320);
            this.textBoxNewStreet.Name = "textBoxNewStreet";
            this.textBoxNewStreet.Size = new System.Drawing.Size(160, 22);
            this.textBoxNewStreet.TabIndex = 21;
            this.textBoxNewStreet.Visible = false;
            this.textBoxNewStreet.Enter += new System.EventHandler(this.textBoxNewStreet_Enter);
            this.textBoxNewStreet.Leave += new System.EventHandler(this.textBoxNewStreet_Leave);
            // 
            // textBoxNewCity
            // 
            this.textBoxNewCity.Location = new System.Drawing.Point(120, 290);
            this.textBoxNewCity.Name = "textBoxNewCity";
            this.textBoxNewCity.Size = new System.Drawing.Size(160, 22);
            this.textBoxNewCity.TabIndex = 20;
            this.textBoxNewCity.Visible = false;
            this.textBoxNewCity.Enter += new System.EventHandler(this.textBoxNewCity_Enter);
            this.textBoxNewCity.Leave += new System.EventHandler(this.textBoxNewCity_Leave);
            // 
            // textBoxAccountId
            // 
            this.textBoxAccountId.Location = new System.Drawing.Point(120, 250);
            this.textBoxAccountId.Name = "textBoxAccountId";
            this.textBoxAccountId.Size = new System.Drawing.Size(160, 22);
            this.textBoxAccountId.TabIndex = 19;
            this.textBoxAccountId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxAccountId_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 253);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 16);
            this.label7.TabIndex = 18;
            this.label7.Text = "ID аккаунта:";
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(120, 30);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.ReadOnly = true;
            this.textBoxID.Size = new System.Drawing.Size(160, 22);
            this.textBoxID.TabIndex = 17;
            this.textBoxID.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 16);
            this.label9.TabIndex = 16;
            this.label9.Text = "ID:";
            this.label9.Visible = false;
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(20, 380);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(360, 40);
            this.buttonSave.TabIndex = 15;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // comboBoxAddress
            // 
            this.comboBoxAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAddress.FormattingEnabled = true;
            this.comboBoxAddress.Location = new System.Drawing.Point(120, 290);
            this.comboBoxAddress.Name = "comboBoxAddress";
            this.comboBoxAddress.Size = new System.Drawing.Size(160, 24);
            this.comboBoxAddress.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 293);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Адрес:";
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(120, 210);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(160, 24);
            this.comboBoxStatus.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 213);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 9;
            this.label5.Text = "Статус:";
            // 
            // textBoxPhone
            // 
            this.textBoxPhone.Location = new System.Drawing.Point(120, 170);
            this.textBoxPhone.Name = "textBoxPhone";
            this.textBoxPhone.Size = new System.Drawing.Size(160, 22);
            this.textBoxPhone.TabIndex = 8;
            this.textBoxPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPhone_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Телефон:";
            // 
            // textBoxPatronymic
            // 
            this.textBoxPatronymic.Location = new System.Drawing.Point(120, 130);
            this.textBoxPatronymic.Name = "textBoxPatronymic";
            this.textBoxPatronymic.Size = new System.Drawing.Size(160, 22);
            this.textBoxPatronymic.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Отчество:";
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Location = new System.Drawing.Point(120, 90);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(160, 22);
            this.textBoxLastName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Фамилия:";
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Location = new System.Drawing.Point(120, 50);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(160, 22);
            this.textBoxFirstName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Имя:";
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.Location = new System.Drawing.Point(12, 20);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(120, 40);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonEdit.ForeColor = System.Drawing.Color.Black;
            this.buttonEdit.Location = new System.Drawing.Point(142, 20);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(120, 40);
            this.buttonEdit.TabIndex = 3;
            this.buttonEdit.Text = "Редактировать";
            this.buttonEdit.UseVisualStyleBackColor = false;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonDelete.ForeColor = System.Drawing.Color.White;
            this.buttonDelete.Location = new System.Drawing.Point(272, 20);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(120, 40);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(39)))), ((int)(((byte)(176)))));
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonRefresh.ForeColor = System.Drawing.Color.White;
            this.buttonRefresh.Location = new System.Drawing.Point(402, 20);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(120, 40);
            this.buttonRefresh.TabIndex = 5;
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(630, 30);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(180, 22);
            this.textBoxSearch.TabIndex = 6;
            this.textBoxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSearch_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(540, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 18);
            this.label8.TabIndex = 7;
            this.label8.Text = "Поиск:";
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSearch.ForeColor = System.Drawing.Color.White;
            this.buttonSearch.Location = new System.Drawing.Point(820, 20);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(90, 40);
            this.buttonSearch.TabIndex = 8;
            this.buttonSearch.Text = "Найти";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonBack.ForeColor = System.Drawing.Color.White;
            this.buttonBack.Location = new System.Drawing.Point(920, 20);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(120, 40);
            this.buttonBack.TabIndex = 9;
            this.buttonBack.Text = "Назад";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // AdminClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1106, 577);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.groupBoxClientData);
            this.Controls.Add(this.dataGridViewClients);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.Name = "AdminClientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление клиентами";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClients)).EndInit();
            this.groupBoxClientData.ResumeLayout(false);
            this.groupBoxClientData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}