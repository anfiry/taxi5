namespace taxi4
{
    partial class AdminCarForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewCars;
        private System.Windows.Forms.GroupBox groupBoxCarData;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labelBrand;
        private System.Windows.Forms.TextBox txtBrand;  // изменено
        private System.Windows.Forms.Label labelModel;
        private System.Windows.Forms.TextBox txtModel;  // изменено
        private System.Windows.Forms.Label labelColor;
        private System.Windows.Forms.TextBox txtColor;  // изменено
        private System.Windows.Forms.Label labelLicenseNumber;
        private System.Windows.Forms.TextBox textBoxLicenseNumber;
        private System.Windows.Forms.Label labelRegionCode;
        private System.Windows.Forms.TextBox textBoxRegionCode;
        private System.Windows.Forms.Label labelYear;
        private System.Windows.Forms.TextBox textBoxYear;
        private System.Windows.Forms.Label labelDriver;
        private System.Windows.Forms.ComboBox comboBoxDriver;
        private System.Windows.Forms.TextBox textBoxCarId;
        private System.Windows.Forms.Label labelCarId;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewCars = new System.Windows.Forms.DataGridView();
            this.groupBoxCarData = new System.Windows.Forms.GroupBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelDriver = new System.Windows.Forms.Label();
            this.comboBoxDriver = new System.Windows.Forms.ComboBox();
            this.textBoxYear = new System.Windows.Forms.TextBox();
            this.labelYear = new System.Windows.Forms.Label();
            this.textBoxRegionCode = new System.Windows.Forms.TextBox();
            this.labelRegionCode = new System.Windows.Forms.Label();
            this.textBoxLicenseNumber = new System.Windows.Forms.TextBox();
            this.labelLicenseNumber = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.labelColor = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.labelModel = new System.Windows.Forms.Label();
            this.txtBrand = new System.Windows.Forms.TextBox();
            this.labelBrand = new System.Windows.Forms.Label();
            this.textBoxCarId = new System.Windows.Forms.TextBox();
            this.labelCarId = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCars)).BeginInit();
            this.groupBoxCarData.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewCars
            // 
            this.dataGridViewCars.AllowUserToAddRows = false;
            this.dataGridViewCars.AllowUserToDeleteRows = false;
            this.dataGridViewCars.AllowUserToResizeRows = false;
            this.dataGridViewCars.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridViewCars.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewCars.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewCars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCars.Location = new System.Drawing.Point(146, 112);
            this.dataGridViewCars.Name = "dataGridViewCars";
            this.dataGridViewCars.ReadOnly = true;
            this.dataGridViewCars.RowHeadersVisible = false;
            this.dataGridViewCars.Size = new System.Drawing.Size(916, 728);
            this.dataGridViewCars.TabIndex = 0;
            this.dataGridViewCars.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCars_CellDoubleClick);
            // 
            // groupBoxCarData
            // 
            this.groupBoxCarData.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBoxCarData.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBoxCarData.Controls.Add(this.buttonSave);
            this.groupBoxCarData.Controls.Add(this.labelDriver);
            this.groupBoxCarData.Controls.Add(this.comboBoxDriver);
            this.groupBoxCarData.Controls.Add(this.textBoxYear);
            this.groupBoxCarData.Controls.Add(this.labelYear);
            this.groupBoxCarData.Controls.Add(this.textBoxRegionCode);
            this.groupBoxCarData.Controls.Add(this.labelRegionCode);
            this.groupBoxCarData.Controls.Add(this.textBoxLicenseNumber);
            this.groupBoxCarData.Controls.Add(this.labelLicenseNumber);
            this.groupBoxCarData.Controls.Add(this.txtColor);
            this.groupBoxCarData.Controls.Add(this.labelColor);
            this.groupBoxCarData.Controls.Add(this.txtModel);
            this.groupBoxCarData.Controls.Add(this.labelModel);
            this.groupBoxCarData.Controls.Add(this.txtBrand);
            this.groupBoxCarData.Controls.Add(this.labelBrand);
            this.groupBoxCarData.Controls.Add(this.textBoxCarId);
            this.groupBoxCarData.Controls.Add(this.labelCarId);
            this.groupBoxCarData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBoxCarData.Location = new System.Drawing.Point(1119, 112);
            this.groupBoxCarData.Name = "groupBoxCarData";
            this.groupBoxCarData.Size = new System.Drawing.Size(487, 520);
            this.groupBoxCarData.TabIndex = 1;
            this.groupBoxCarData.TabStop = false;
            this.groupBoxCarData.Text = "Данные автомобиля";
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(218, 52, 247);
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSave.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonSave.Location = new System.Drawing.Point(71, 460);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(360, 40);
            this.buttonSave.TabIndex = 16;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelDriver
            // 
            this.labelDriver.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelDriver.AutoSize = true;
            this.labelDriver.Location = new System.Drawing.Point(68, 400);
            this.labelDriver.Name = "labelDriver";
            this.labelDriver.Size = new System.Drawing.Size(80, 18);
            this.labelDriver.TabIndex = 14;
            this.labelDriver.Text = "Водитель:";
            // 
            // comboBoxDriver
            // 
            this.comboBoxDriver.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDriver.FormattingEnabled = true;
            this.comboBoxDriver.Location = new System.Drawing.Point(168, 397);
            this.comboBoxDriver.Name = "comboBoxDriver";
            this.comboBoxDriver.Size = new System.Drawing.Size(250, 26);
            this.comboBoxDriver.TabIndex = 15;
            // 
            // textBoxYear
            // 
            this.textBoxYear.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxYear.Location = new System.Drawing.Point(168, 350);
            this.textBoxYear.MaxLength = 4;
            this.textBoxYear.Name = "textBoxYear";
            this.textBoxYear.Size = new System.Drawing.Size(100, 24);
            this.textBoxYear.TabIndex = 13;
            this.textBoxYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxYear_KeyPress);
            // 
            // labelYear
            // 
            this.labelYear.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelYear.AutoSize = true;
            this.labelYear.Location = new System.Drawing.Point(68, 353);
            this.labelYear.Name = "labelYear";
            this.labelYear.Size = new System.Drawing.Size(101, 18);
            this.labelYear.TabIndex = 12;
            this.labelYear.Text = "Год выпуска:";
            // 
            // textBoxRegionCode
            // 
            this.textBoxRegionCode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxRegionCode.Location = new System.Drawing.Point(318, 310);
            this.textBoxRegionCode.MaxLength = 3;
            this.textBoxRegionCode.Name = "textBoxRegionCode";
            this.textBoxRegionCode.Size = new System.Drawing.Size(50, 24);
            this.textBoxRegionCode.TabIndex = 11;
            this.textBoxRegionCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxRegionCode_KeyPress);
            // 
            // labelRegionCode
            // 
            this.labelRegionCode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelRegionCode.AutoSize = true;
            this.labelRegionCode.Location = new System.Drawing.Point(276, 313);
            this.labelRegionCode.Name = "labelRegionCode";
            this.labelRegionCode.Size = new System.Drawing.Size(36, 18);
            this.labelRegionCode.TabIndex = 10;
            this.labelRegionCode.Text = "Рег:";
            // 
            // textBoxLicenseNumber
            // 
            this.textBoxLicenseNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxLicenseNumber.Location = new System.Drawing.Point(168, 310);
            this.textBoxLicenseNumber.MaxLength = 6;
            this.textBoxLicenseNumber.Name = "textBoxLicenseNumber";
            this.textBoxLicenseNumber.Size = new System.Drawing.Size(100, 24);
            this.textBoxLicenseNumber.TabIndex = 9;
            this.textBoxLicenseNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxLicenseNumber_KeyPress);
            // 
            // labelLicenseNumber
            // 
            this.labelLicenseNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelLicenseNumber.AutoSize = true;
            this.labelLicenseNumber.Location = new System.Drawing.Point(68, 313);
            this.labelLicenseNumber.Name = "labelLicenseNumber";
            this.labelLicenseNumber.Size = new System.Drawing.Size(82, 18);
            this.labelLicenseNumber.TabIndex = 8;
            this.labelLicenseNumber.Text = "Госномер:";
            // 
            // txtColor
            // 
            this.txtColor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtColor.Location = new System.Drawing.Point(168, 260);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(250, 24);
            this.txtColor.TabIndex = 7;
            // 
            // labelColor
            // 
            this.labelColor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelColor.AutoSize = true;
            this.labelColor.Location = new System.Drawing.Point(68, 263);
            this.labelColor.Name = "labelColor";
            this.labelColor.Size = new System.Drawing.Size(46, 18);
            this.labelColor.TabIndex = 6;
            this.labelColor.Text = "Цвет:";
            // 
            // txtModel
            // 
            this.txtModel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtModel.Location = new System.Drawing.Point(168, 210);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(250, 24);
            this.txtModel.TabIndex = 5;
            // 
            // labelModel
            // 
            this.labelModel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelModel.AutoSize = true;
            this.labelModel.Location = new System.Drawing.Point(68, 213);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(68, 18);
            this.labelModel.TabIndex = 4;
            this.labelModel.Text = "Модель:";
            // 
            // txtBrand
            // 
            this.txtBrand.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBrand.Location = new System.Drawing.Point(168, 160);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(250, 24);
            this.txtBrand.TabIndex = 3;
            // 
            // labelBrand
            // 
            this.labelBrand.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelBrand.AutoSize = true;
            this.labelBrand.Location = new System.Drawing.Point(68, 163);
            this.labelBrand.Name = "labelBrand";
            this.labelBrand.Size = new System.Drawing.Size(57, 18);
            this.labelBrand.TabIndex = 2;
            this.labelBrand.Text = "Марка:";
            // 
            // textBoxCarId
            // 
            this.textBoxCarId.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxCarId.Location = new System.Drawing.Point(168, 110);
            this.textBoxCarId.Name = "textBoxCarId";
            this.textBoxCarId.ReadOnly = true;
            this.textBoxCarId.Size = new System.Drawing.Size(80, 24);
            this.textBoxCarId.TabIndex = 1;
            this.textBoxCarId.Visible = false;
            // 
            // labelCarId
            // 
            this.labelCarId.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelCarId.AutoSize = true;
            this.labelCarId.Location = new System.Drawing.Point(68, 113);
            this.labelCarId.Name = "labelCarId";
            this.labelCarId.Size = new System.Drawing.Size(26, 18);
            this.labelCarId.TabIndex = 0;
            this.labelCarId.Text = "ID:";
            this.labelCarId.Visible = false;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(251, 228, 255);
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonAdd.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonAdd.Location = new System.Drawing.Point(165, 39);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(120, 40);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonEdit.BackColor = System.Drawing.Color.FromArgb(230, 227, 255);
            this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonEdit.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonEdit.Location = new System.Drawing.Point(311, 39);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(152, 40);
            this.buttonEdit.TabIndex = 3;
            this.buttonEdit.Text = "Редактировать";
            this.buttonEdit.UseVisualStyleBackColor = false;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonDelete.BackColor = System.Drawing.Color.FromArgb(255, 227, 227);
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonDelete.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonDelete.Location = new System.Drawing.Point(490, 39);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(120, 40);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonRefresh.BackColor = System.Drawing.Color.FromArgb(251, 228, 255);
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonRefresh.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonRefresh.Location = new System.Drawing.Point(639, 39);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(120, 40);
            this.buttonRefresh.TabIndex = 5;
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSearch.BackColor = System.Drawing.Color.FromArgb(230, 227, 255);
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSearch.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonSearch.Location = new System.Drawing.Point(1225, 39);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(90, 40);
            this.buttonSearch.TabIndex = 8;
            this.buttonSearch.Text = "Найти";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonBack.BackColor = System.Drawing.Color.FromArgb(255, 227, 227);
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonBack.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonBack.Location = new System.Drawing.Point(1457, 39);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(120, 40);
            this.buttonBack.TabIndex = 9;
            this.buttonBack.Text = "Назад";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textBoxSearch.Location = new System.Drawing.Point(989, 47);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(180, 24);
            this.textBoxSearch.TabIndex = 6;
            this.textBoxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSearch_KeyDown);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(921, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 18);
            this.label8.TabIndex = 7;
            this.label8.Text = "Поиск:";
            // 
            // AdminCarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1718, 852);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.groupBoxCarData);
            this.Controls.Add(this.dataGridViewCars);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.MinimumSize = new System.Drawing.Size(1200, 672);
            this.Name = "AdminCarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление автомобилями";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCars)).EndInit();
            this.groupBoxCarData.ResumeLayout(false);
            this.groupBoxCarData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}