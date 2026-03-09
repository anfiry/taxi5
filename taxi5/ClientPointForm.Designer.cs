namespace taxi4
{
    partial class ClientPointForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewPoints;
        private System.Windows.Forms.GroupBox groupBoxPointData;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label labelSearch;

        private System.Windows.Forms.Label labelPointId;
        private System.Windows.Forms.TextBox textBoxPointId;
        private System.Windows.Forms.Label labelPointName;
        private System.Windows.Forms.TextBox txtPointName;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label labelCity;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label labelStreet;
        private System.Windows.Forms.TextBox txtStreet;
        private System.Windows.Forms.Label labelHouse;
        private System.Windows.Forms.TextBox txtHouse;
        private System.Windows.Forms.Label labelEntrance;
        private System.Windows.Forms.TextBox txtEntrance;
        private System.Windows.Forms.TextBox txtAddressId;

        private System.Windows.Forms.Button btnNewType;
        private System.Windows.Forms.Panel panelNewType;
        private System.Windows.Forms.TextBox txtNewType;
        private System.Windows.Forms.Button btnSaveNewType;
        private System.Windows.Forms.Button btnCancelNewType;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewPoints = new System.Windows.Forms.DataGridView();
            this.groupBoxPointData = new System.Windows.Forms.GroupBox();
            this.btnNewType = new System.Windows.Forms.Button();
            this.panelNewType = new System.Windows.Forms.Panel();
            this.txtNewType = new System.Windows.Forms.TextBox();
            this.btnSaveNewType = new System.Windows.Forms.Button();
            this.btnCancelNewType = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelEntrance = new System.Windows.Forms.Label();
            this.txtEntrance = new System.Windows.Forms.TextBox();
            this.labelHouse = new System.Windows.Forms.Label();
            this.txtHouse = new System.Windows.Forms.TextBox();
            this.labelStreet = new System.Windows.Forms.Label();
            this.txtStreet = new System.Windows.Forms.TextBox();
            this.labelCity = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.labelType = new System.Windows.Forms.Label();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.labelPointName = new System.Windows.Forms.Label();
            this.txtPointName = new System.Windows.Forms.TextBox();
            this.textBoxPointId = new System.Windows.Forms.TextBox();
            this.labelPointId = new System.Windows.Forms.Label();
            this.txtAddressId = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.labelSearch = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPoints)).BeginInit();
            this.groupBoxPointData.SuspendLayout();
            this.panelNewType.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewPoints
            // 
            this.dataGridViewPoints.AllowUserToAddRows = false;
            this.dataGridViewPoints.AllowUserToDeleteRows = false;
            this.dataGridViewPoints.AllowUserToResizeRows = false;
            this.dataGridViewPoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPoints.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewPoints.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewPoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPoints.Location = new System.Drawing.Point(12, 70);
            this.dataGridViewPoints.Name = "dataGridViewPoints";
            this.dataGridViewPoints.ReadOnly = true;
            this.dataGridViewPoints.RowHeadersVisible = false;
            this.dataGridViewPoints.RowHeadersWidth = 51;
            this.dataGridViewPoints.Size = new System.Drawing.Size(750, 543);
            this.dataGridViewPoints.TabIndex = 0;
            this.dataGridViewPoints.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPoints_CellDoubleClick);
            // 
            // groupBoxPointData
            // 
            this.groupBoxPointData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPointData.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBoxPointData.Controls.Add(this.btnNewType);
            this.groupBoxPointData.Controls.Add(this.panelNewType);
            this.groupBoxPointData.Controls.Add(this.buttonSave);
            this.groupBoxPointData.Controls.Add(this.labelEntrance);
            this.groupBoxPointData.Controls.Add(this.txtEntrance);
            this.groupBoxPointData.Controls.Add(this.labelHouse);
            this.groupBoxPointData.Controls.Add(this.txtHouse);
            this.groupBoxPointData.Controls.Add(this.labelStreet);
            this.groupBoxPointData.Controls.Add(this.txtStreet);
            this.groupBoxPointData.Controls.Add(this.labelCity);
            this.groupBoxPointData.Controls.Add(this.txtCity);
            this.groupBoxPointData.Controls.Add(this.labelType);
            this.groupBoxPointData.Controls.Add(this.comboBoxType);
            this.groupBoxPointData.Controls.Add(this.labelPointName);
            this.groupBoxPointData.Controls.Add(this.txtPointName);
            this.groupBoxPointData.Controls.Add(this.textBoxPointId);
            this.groupBoxPointData.Controls.Add(this.labelPointId);
            this.groupBoxPointData.Controls.Add(this.txtAddressId);
            this.groupBoxPointData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBoxPointData.Location = new System.Drawing.Point(780, 70);
            this.groupBoxPointData.Name = "groupBoxPointData";
            this.groupBoxPointData.Size = new System.Drawing.Size(390, 543);
            this.groupBoxPointData.TabIndex = 1;
            this.groupBoxPointData.TabStop = false;
            this.groupBoxPointData.Text = "Данные точки";
            // 
            // btnNewType
            // 
            this.btnNewType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.btnNewType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewType.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnNewType.ForeColor = System.Drawing.Color.White;
            this.btnNewType.Location = new System.Drawing.Point(290, 97);
            this.btnNewType.Name = "btnNewType";
            this.btnNewType.Size = new System.Drawing.Size(30, 26);
            this.btnNewType.TabIndex = 42;
            this.btnNewType.Text = "+";
            this.btnNewType.UseVisualStyleBackColor = false;
            this.btnNewType.Click += new System.EventHandler(this.btnNewType_Click);
            // 
            // panelNewType
            // 
            this.panelNewType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelNewType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNewType.Controls.Add(this.txtNewType);
            this.panelNewType.Controls.Add(this.btnSaveNewType);
            this.panelNewType.Controls.Add(this.btnCancelNewType);
            this.panelNewType.Location = new System.Drawing.Point(120, 97);
            this.panelNewType.Name = "panelNewType";
            this.panelNewType.Size = new System.Drawing.Size(200, 60);
            this.panelNewType.TabIndex = 43;
            this.panelNewType.Visible = false;
            // 
            // txtNewType
            // 
            this.txtNewType.Location = new System.Drawing.Point(10, 10);
            this.txtNewType.Name = "txtNewType";
            this.txtNewType.Size = new System.Drawing.Size(180, 24);
            this.txtNewType.TabIndex = 0;
            this.txtNewType.Enter += new System.EventHandler(this.txtNewType_Enter);
            this.txtNewType.Leave += new System.EventHandler(this.txtNewType_Leave);
            // 
            // btnSaveNewType
            // 
            this.btnSaveNewType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(52)))), ((int)(((byte)(247)))));
            this.btnSaveNewType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveNewType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnSaveNewType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.btnSaveNewType.Location = new System.Drawing.Point(10, 35);
            this.btnSaveNewType.Name = "btnSaveNewType";
            this.btnSaveNewType.Size = new System.Drawing.Size(80, 25);
            this.btnSaveNewType.TabIndex = 1;
            this.btnSaveNewType.Text = "Сохранить";
            this.btnSaveNewType.UseVisualStyleBackColor = false;
            this.btnSaveNewType.Click += new System.EventHandler(this.btnSaveNewType_Click);
            // 
            // btnCancelNewType
            // 
            this.btnCancelNewType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.btnCancelNewType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelNewType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnCancelNewType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.btnCancelNewType.Location = new System.Drawing.Point(100, 35);
            this.btnCancelNewType.Name = "btnCancelNewType";
            this.btnCancelNewType.Size = new System.Drawing.Size(80, 25);
            this.btnCancelNewType.TabIndex = 2;
            this.btnCancelNewType.Text = "Отмена";
            this.btnCancelNewType.UseVisualStyleBackColor = false;
            this.btnCancelNewType.Click += new System.EventHandler(this.btnCancelNewType_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(52)))), ((int)(((byte)(247)))));
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonSave.Location = new System.Drawing.Point(20, 350);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(360, 40);
            this.buttonSave.TabIndex = 16;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelEntrance
            // 
            this.labelEntrance.AutoSize = true;
            this.labelEntrance.Location = new System.Drawing.Point(20, 310);
            this.labelEntrance.Name = "labelEntrance";
            this.labelEntrance.Size = new System.Drawing.Size(62, 18);
            this.labelEntrance.TabIndex = 14;
            this.labelEntrance.Text = "Подъезд:";
            // 
            // txtEntrance
            // 
            this.txtEntrance.Location = new System.Drawing.Point(120, 307);
            this.txtEntrance.Name = "txtEntrance";
            this.txtEntrance.Size = new System.Drawing.Size(160, 24);
            this.txtEntrance.TabIndex = 15;
            this.txtEntrance.Enter += new System.EventHandler(this.txtEntrance_Enter);
            this.txtEntrance.Leave += new System.EventHandler(this.txtEntrance_Leave);
            // 
            // labelHouse
            // 
            this.labelHouse.AutoSize = true;
            this.labelHouse.Location = new System.Drawing.Point(20, 270);
            this.labelHouse.Name = "labelHouse";
            this.labelHouse.Size = new System.Drawing.Size(42, 18);
            this.labelHouse.TabIndex = 12;
            this.labelHouse.Text = "Дом:";
            // 
            // txtHouse
            // 
            this.txtHouse.Location = new System.Drawing.Point(120, 267);
            this.txtHouse.Name = "txtHouse";
            this.txtHouse.Size = new System.Drawing.Size(160, 24);
            this.txtHouse.TabIndex = 13;
            this.txtHouse.Enter += new System.EventHandler(this.txtHouse_Enter);
            this.txtHouse.Leave += new System.EventHandler(this.txtHouse_Leave);
            // 
            // labelStreet
            // 
            this.labelStreet.AutoSize = true;
            this.labelStreet.Location = new System.Drawing.Point(20, 230);
            this.labelStreet.Name = "labelStreet";
            this.labelStreet.Size = new System.Drawing.Size(49, 18);
            this.labelStreet.TabIndex = 10;
            this.labelStreet.Text = "Улица:";
            // 
            // txtStreet
            // 
            this.txtStreet.Location = new System.Drawing.Point(120, 227);
            this.txtStreet.Name = "txtStreet";
            this.txtStreet.Size = new System.Drawing.Size(200, 24);
            this.txtStreet.TabIndex = 11;
            this.txtStreet.Enter += new System.EventHandler(this.txtStreet_Enter);
            this.txtStreet.Leave += new System.EventHandler(this.txtStreet_Leave);
            // 
            // labelCity
            // 
            this.labelCity.AutoSize = true;
            this.labelCity.Location = new System.Drawing.Point(20, 190);
            this.labelCity.Name = "labelCity";
            this.labelCity.Size = new System.Drawing.Size(50, 18);
            this.labelCity.TabIndex = 8;
            this.labelCity.Text = "Город:";
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(120, 187);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(200, 24);
            this.txtCity.TabIndex = 9;
            this.txtCity.Enter += new System.EventHandler(this.txtCity_Enter);
            this.txtCity.Leave += new System.EventHandler(this.txtCity_Leave);
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(20, 100);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(85, 18);
            this.labelType.TabIndex = 4;
            this.labelType.Text = "Тип точки:";
            // 
            // comboBoxType
            // 
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxType.FormattingEnabled = true;
            this.comboBoxType.Location = new System.Drawing.Point(120, 97);
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.Size = new System.Drawing.Size(160, 26);
            this.comboBoxType.TabIndex = 5;
            // 
            // labelPointName
            // 
            this.labelPointName.AutoSize = true;
            this.labelPointName.Location = new System.Drawing.Point(20, 60);
            this.labelPointName.Name = "labelPointName";
            this.labelPointName.Size = new System.Drawing.Size(58, 18);
            this.labelPointName.TabIndex = 2;
            this.labelPointName.Text = "Название:";
            // 
            // txtPointName
            // 
            this.txtPointName.Location = new System.Drawing.Point(120, 57);
            this.txtPointName.Name = "txtPointName";
            this.txtPointName.Size = new System.Drawing.Size(200, 24);
            this.txtPointName.TabIndex = 3;
            this.txtPointName.Enter += new System.EventHandler(this.txtPointName_Enter);
            this.txtPointName.Leave += new System.EventHandler(this.txtPointName_Leave);
            // 
            // textBoxPointId
            // 
            this.textBoxPointId.Location = new System.Drawing.Point(120, 27);
            this.textBoxPointId.Name = "textBoxPointId";
            this.textBoxPointId.ReadOnly = true;
            this.textBoxPointId.Size = new System.Drawing.Size(80, 24);
            this.textBoxPointId.TabIndex = 1;
            this.textBoxPointId.Visible = false;
            // 
            // labelPointId
            // 
            this.labelPointId.AutoSize = true;
            this.labelPointId.Location = new System.Drawing.Point(20, 30);
            this.labelPointId.Name = "labelPointId";
            this.labelPointId.Size = new System.Drawing.Size(26, 18);
            this.labelPointId.TabIndex = 0;
            this.labelPointId.Text = "ID:";
            this.labelPointId.Visible = false;
            // 
            // txtAddressId
            // 
            this.txtAddressId.Location = new System.Drawing.Point(250, 27);
            this.txtAddressId.Name = "txtAddressId";
            this.txtAddressId.ReadOnly = true;
            this.txtAddressId.Size = new System.Drawing.Size(50, 24);
            this.txtAddressId.TabIndex = 17;
            this.txtAddressId.Visible = false;
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
            this.buttonEdit.Size = new System.Drawing.Size(152, 40);
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
            this.buttonDelete.Location = new System.Drawing.Point(309, 19);
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
            this.buttonRefresh.Location = new System.Drawing.Point(435, 20);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(120, 40);
            this.buttonRefresh.TabIndex = 5;
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(227)))), ((int)(((byte)(255)))));
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonSearch.Location = new System.Drawing.Point(867, 20);
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
            this.buttonBack.Location = new System.Drawing.Point(980, 20);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(120, 40);
            this.buttonBack.TabIndex = 9;
            this.buttonBack.Text = "Назад";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textBoxSearch.Location = new System.Drawing.Point(666, 27);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(180, 24);
            this.textBoxSearch.TabIndex = 6;
            this.textBoxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSearch_KeyDown);
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.labelSearch.Location = new System.Drawing.Point(587, 33);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(62, 18);
            this.labelSearch.TabIndex = 7;
            this.labelSearch.Text = "Поиск:";
            // 
            // ClientPointForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1182, 625);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.groupBoxPointData);
            this.Controls.Add(this.dataGridViewPoints);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.MinimumSize = new System.Drawing.Size(1200, 672);
            this.Name = "ClientPointForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Мои точки";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPoints)).EndInit();
            this.groupBoxPointData.ResumeLayout(false);
            this.groupBoxPointData.PerformLayout();
            this.panelNewType.ResumeLayout(false);
            this.panelNewType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}