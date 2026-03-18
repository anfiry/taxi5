namespace taxi4
{
    partial class AdminDriverForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewDrivers;
        private System.Windows.Forms.GroupBox groupBoxDriverData;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label8;

        // Элементы данных водителя
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
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxAccountId;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxWorkExperience;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxLicenseSeries;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBoxLicenseNumber;
        private System.Windows.Forms.Button buttonViewCars;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewDrivers = new System.Windows.Forms.DataGridView();
            this.groupBoxDriverData = new System.Windows.Forms.GroupBox();
            this.buttonViewCars = new System.Windows.Forms.Button();
            this.textBoxLicenseNumber = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxLicenseSeries = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxWorkExperience = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxAccountId = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDrivers)).BeginInit();
            this.groupBoxDriverData.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewDrivers
            // 
            this.dataGridViewDrivers.AllowUserToAddRows = false;
            this.dataGridViewDrivers.AllowUserToDeleteRows = false;
            this.dataGridViewDrivers.AllowUserToResizeRows = false;
            this.dataGridViewDrivers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewDrivers.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewDrivers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewDrivers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDrivers.Location = new System.Drawing.Point(12, 70);
            this.dataGridViewDrivers.Name = "dataGridViewDrivers";
            this.dataGridViewDrivers.ReadOnly = true;
            this.dataGridViewDrivers.RowHeadersVisible = false;
            this.dataGridViewDrivers.RowHeadersWidth = 51;
            this.dataGridViewDrivers.Size = new System.Drawing.Size(750, 565);
            this.dataGridViewDrivers.TabIndex = 0;
            this.dataGridViewDrivers.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDrivers_CellDoubleClick);
            // 
            // groupBoxDriverData
            // 
            this.groupBoxDriverData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDriverData.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBoxDriverData.Controls.Add(this.buttonViewCars);
            this.groupBoxDriverData.Controls.Add(this.textBoxLicenseNumber);
            this.groupBoxDriverData.Controls.Add(this.label12);
            this.groupBoxDriverData.Controls.Add(this.textBoxLicenseSeries);
            this.groupBoxDriverData.Controls.Add(this.label11);
            this.groupBoxDriverData.Controls.Add(this.textBoxWorkExperience);
            this.groupBoxDriverData.Controls.Add(this.label10);
            this.groupBoxDriverData.Controls.Add(this.textBoxAccountId);
            this.groupBoxDriverData.Controls.Add(this.label7);
            this.groupBoxDriverData.Controls.Add(this.textBoxID);
            this.groupBoxDriverData.Controls.Add(this.label9);
            this.groupBoxDriverData.Controls.Add(this.buttonSave);
            this.groupBoxDriverData.Controls.Add(this.comboBoxStatus);
            this.groupBoxDriverData.Controls.Add(this.label5);
            this.groupBoxDriverData.Controls.Add(this.textBoxPhone);
            this.groupBoxDriverData.Controls.Add(this.label4);
            this.groupBoxDriverData.Controls.Add(this.textBoxPatronymic);
            this.groupBoxDriverData.Controls.Add(this.label3);
            this.groupBoxDriverData.Controls.Add(this.textBoxLastName);
            this.groupBoxDriverData.Controls.Add(this.label2);
            this.groupBoxDriverData.Controls.Add(this.textBoxFirstName);
            this.groupBoxDriverData.Controls.Add(this.label1);
            this.groupBoxDriverData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBoxDriverData.Location = new System.Drawing.Point(780, 70);
            this.groupBoxDriverData.Name = "groupBoxDriverData";
            this.groupBoxDriverData.Size = new System.Drawing.Size(390, 565);
            this.groupBoxDriverData.TabIndex = 1;
            this.groupBoxDriverData.TabStop = false;
            this.groupBoxDriverData.Text = "Данные водителя";
            // 
            // buttonViewCars
            // 
            this.buttonViewCars.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(228)))), ((int)(((byte)(255)))));
            this.buttonViewCars.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonViewCars.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonViewCars.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonViewCars.Location = new System.Drawing.Point(20, 470);
            this.buttonViewCars.Name = "buttonViewCars";
            this.buttonViewCars.Size = new System.Drawing.Size(360, 35);
            this.buttonViewCars.TabIndex = 20;
            this.buttonViewCars.Text = "Просмотреть автомобили";
            this.buttonViewCars.UseVisualStyleBackColor = false;
            this.buttonViewCars.Click += new System.EventHandler(this.buttonViewCars_Click);
            // 
            // textBoxLicenseNumber
            // 
            this.textBoxLicenseNumber.Location = new System.Drawing.Point(120, 350);
            this.textBoxLicenseNumber.MaxLength = 6;
            this.textBoxLicenseNumber.Name = "textBoxLicenseNumber";
            this.textBoxLicenseNumber.Size = new System.Drawing.Size(200, 24);
            this.textBoxLicenseNumber.TabIndex = 16;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(20, 353);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(95, 18);
            this.label12.TabIndex = 17;
            this.label12.Text = "Номер прав:";
            // 
            // textBoxLicenseSeries
            // 
            this.textBoxLicenseSeries.Location = new System.Drawing.Point(120, 310);
            this.textBoxLicenseSeries.MaxLength = 4;
            this.textBoxLicenseSeries.Name = "textBoxLicenseSeries";
            this.textBoxLicenseSeries.Size = new System.Drawing.Size(200, 24);
            this.textBoxLicenseSeries.TabIndex = 14;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 313);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 18);
            this.label11.TabIndex = 15;
            this.label11.Text = "Серия прав:";
            // 
            // textBoxWorkExperience
            // 
            this.textBoxWorkExperience.Location = new System.Drawing.Point(120, 270);
            this.textBoxWorkExperience.Name = "textBoxWorkExperience";
            this.textBoxWorkExperience.Size = new System.Drawing.Size(200, 24);
            this.textBoxWorkExperience.TabIndex = 12;
            this.textBoxWorkExperience.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWorkExperience_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 273);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 18);
            this.label10.TabIndex = 13;
            this.label10.Text = "Стаж (лет):";
            // 
            // textBoxAccountId
            // 
            this.textBoxAccountId.Location = new System.Drawing.Point(120, 230);
            this.textBoxAccountId.Name = "textBoxAccountId";
            this.textBoxAccountId.Size = new System.Drawing.Size(200, 24);
            this.textBoxAccountId.TabIndex = 10;
            this.textBoxAccountId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxAccountId_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 233);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 18);
            this.label7.TabIndex = 11;
            this.label7.Text = "ID аккаунта:";
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(120, 390);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.ReadOnly = true;
            this.textBoxID.Size = new System.Drawing.Size(80, 24);
            this.textBoxID.TabIndex = 18;
            this.textBoxID.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 393);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(26, 18);
            this.label9.TabIndex = 19;
            this.label9.Text = "ID:";
            this.label9.Visible = false;
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(52)))), ((int)(((byte)(247)))));
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonSave.Location = new System.Drawing.Point(20, 520);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(360, 35);
            this.buttonSave.TabIndex = 21;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(120, 190);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(200, 26);
            this.comboBoxStatus.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 193);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 18);
            this.label5.TabIndex = 9;
            this.label5.Text = "Статус:";
            // 
            // textBoxPhone
            // 
            this.textBoxPhone.Location = new System.Drawing.Point(120, 150);
            this.textBoxPhone.Name = "textBoxPhone";
            this.textBoxPhone.Size = new System.Drawing.Size(200, 24);
            this.textBoxPhone.TabIndex = 6;
            this.textBoxPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPhone_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "Телефон:";
            // 
            // textBoxPatronymic
            // 
            this.textBoxPatronymic.Location = new System.Drawing.Point(120, 110);
            this.textBoxPatronymic.Name = "textBoxPatronymic";
            this.textBoxPatronymic.Size = new System.Drawing.Size(200, 24);
            this.textBoxPatronymic.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Отчество:";
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Location = new System.Drawing.Point(120, 70);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(200, 24);
            this.textBoxLastName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Фамилия:";
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Location = new System.Drawing.Point(120, 30);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(200, 24);
            this.textBoxFirstName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Имя:";
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(228)))), ((int)(((byte)(255)))));
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonAdd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
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
            this.buttonEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(227)))), ((int)(((byte)(255)))));
            this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonEdit.Location = new System.Drawing.Point(142, 20);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(160, 40);
            this.buttonEdit.TabIndex = 3;
            this.buttonEdit.Text = "Редактировать";
            this.buttonEdit.UseVisualStyleBackColor = false;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonDelete.Location = new System.Drawing.Point(318, 20);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(120, 40);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(228)))), ((int)(((byte)(255)))));
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonRefresh.Location = new System.Drawing.Point(453, 20);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(120, 40);
            this.buttonRefresh.TabIndex = 5;
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textBoxSearch.Location = new System.Drawing.Point(680, 30);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(180, 24);
            this.textBoxSearch.TabIndex = 6;
            this.textBoxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSearch_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(599, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 18);
            this.label8.TabIndex = 7;
            this.label8.Text = "Поиск:";
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(227)))), ((int)(((byte)(255)))));
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonSearch.Location = new System.Drawing.Point(876, 22);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(90, 40);
            this.buttonSearch.TabIndex = 8;
            this.buttonSearch.Text = "Найти";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonBack.Location = new System.Drawing.Point(990, 20);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(120, 40);
            this.buttonBack.TabIndex = 9;
            this.buttonBack.Text = "Назад";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // AdminDriverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1182, 647);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.groupBoxDriverData);
            this.Controls.Add(this.dataGridViewDrivers);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.MinimumSize = new System.Drawing.Size(1200, 672);
            this.Name = "AdminDriverForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление водителями";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDrivers)).EndInit();
            this.groupBoxDriverData.ResumeLayout(false);
            this.groupBoxDriverData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}