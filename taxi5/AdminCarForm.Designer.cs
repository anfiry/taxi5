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

        // Элементы группы данных автомобиля
        private System.Windows.Forms.Label labelCarId;
        private System.Windows.Forms.TextBox textBoxCarId;
        private System.Windows.Forms.Label labelBrand;
        private System.Windows.Forms.ComboBox comboBoxBrand;
        private System.Windows.Forms.Label labelModel;
        private System.Windows.Forms.ComboBox comboBoxModel;
        private System.Windows.Forms.Label labelColor;
        private System.Windows.Forms.ComboBox comboBoxColor;
        private System.Windows.Forms.Label labelLicenseNumber;
        private System.Windows.Forms.TextBox textBoxLicenseNumber;
        private System.Windows.Forms.Label labelRegionCode;
        private System.Windows.Forms.TextBox textBoxRegionCode;
        private System.Windows.Forms.Label labelYear;
        private System.Windows.Forms.TextBox textBoxYear;
        private System.Windows.Forms.Label labelDriver;
        private System.Windows.Forms.ComboBox comboBoxDriver;

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
            this.comboBoxColor = new System.Windows.Forms.ComboBox();
            this.labelColor = new System.Windows.Forms.Label();
            this.comboBoxModel = new System.Windows.Forms.ComboBox();
            this.labelModel = new System.Windows.Forms.Label();
            this.comboBoxBrand = new System.Windows.Forms.ComboBox();
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

            // dataGridViewCars
            this.dataGridViewCars.AllowUserToAddRows = false;
            this.dataGridViewCars.AllowUserToDeleteRows = false;
            this.dataGridViewCars.AllowUserToResizeRows = false;
            this.dataGridViewCars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewCars.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dataGridViewCars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewCars.Location = new System.Drawing.Point(12, 70);
            this.dataGridViewCars.Name = "dataGridViewCars";
            this.dataGridViewCars.ReadOnly = true;
            this.dataGridViewCars.RowHeadersVisible = false;
            this.dataGridViewCars.RowHeadersWidth = 51;
            this.dataGridViewCars.Size = new System.Drawing.Size(700, 543);
            this.dataGridViewCars.TabIndex = 0;
            this.dataGridViewCars.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCars_CellDoubleClick);

            // groupBoxCarData
            this.groupBoxCarData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCarData.Controls.Add(this.buttonSave);
            this.groupBoxCarData.Controls.Add(this.labelDriver);
            this.groupBoxCarData.Controls.Add(this.comboBoxDriver);
            this.groupBoxCarData.Controls.Add(this.textBoxYear);
            this.groupBoxCarData.Controls.Add(this.labelYear);
            this.groupBoxCarData.Controls.Add(this.textBoxRegionCode);
            this.groupBoxCarData.Controls.Add(this.labelRegionCode);
            this.groupBoxCarData.Controls.Add(this.textBoxLicenseNumber);
            this.groupBoxCarData.Controls.Add(this.labelLicenseNumber);
            this.groupBoxCarData.Controls.Add(this.comboBoxColor);
            this.groupBoxCarData.Controls.Add(this.labelColor);
            this.groupBoxCarData.Controls.Add(this.comboBoxModel);
            this.groupBoxCarData.Controls.Add(this.labelModel);
            this.groupBoxCarData.Controls.Add(this.comboBoxBrand);
            this.groupBoxCarData.Controls.Add(this.labelBrand);
            this.groupBoxCarData.Controls.Add(this.textBoxCarId);
            this.groupBoxCarData.Controls.Add(this.labelCarId);
            this.groupBoxCarData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBoxCarData.Location = new System.Drawing.Point(720, 70);
            this.groupBoxCarData.Name = "groupBoxCarData";
            this.groupBoxCarData.Size = new System.Drawing.Size(390, 543);
            this.groupBoxCarData.TabIndex = 1;
            this.groupBoxCarData.TabStop = false;
            this.groupBoxCarData.Text = "Данные автомобиля";

            // labelCarId
            this.labelCarId.AutoSize = true;
            this.labelCarId.Location = new System.Drawing.Point(20, 30);
            this.labelCarId.Name = "labelCarId";
            this.labelCarId.Size = new System.Drawing.Size(25, 18);
            this.labelCarId.TabIndex = 0;
            this.labelCarId.Text = "ID:";
            this.labelCarId.Visible = false;

            // textBoxCarId
            this.textBoxCarId.Location = new System.Drawing.Point(120, 27);
            this.textBoxCarId.Name = "textBoxCarId";
            this.textBoxCarId.ReadOnly = true;
            this.textBoxCarId.Size = new System.Drawing.Size(160, 24);
            this.textBoxCarId.TabIndex = 1;
            this.textBoxCarId.Visible = false;

            // labelBrand
            this.labelBrand.AutoSize = true;
            this.labelBrand.Location = new System.Drawing.Point(20, 70);
            this.labelBrand.Name = "labelBrand";
            this.labelBrand.Size = new System.Drawing.Size(55, 18);
            this.labelBrand.TabIndex = 2;
            this.labelBrand.Text = "Марка:";

            // comboBoxBrand
            this.comboBoxBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBrand.FormattingEnabled = true;
            this.comboBoxBrand.Location = new System.Drawing.Point(120, 67);
            this.comboBoxBrand.Name = "comboBoxBrand";
            this.comboBoxBrand.Size = new System.Drawing.Size(160, 26);
            this.comboBoxBrand.TabIndex = 3;
            this.comboBoxBrand.SelectedIndexChanged += new System.EventHandler(this.comboBoxBrand_SelectedIndexChanged);

            // labelModel
            this.labelModel.AutoSize = true;
            this.labelModel.Location = new System.Drawing.Point(20, 110);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(62, 18);
            this.labelModel.TabIndex = 4;
            this.labelModel.Text = "Модель:";

            // comboBoxModel
            this.comboBoxModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModel.FormattingEnabled = true;
            this.comboBoxModel.Location = new System.Drawing.Point(120, 107);
            this.comboBoxModel.Name = "comboBoxModel";
            this.comboBoxModel.Size = new System.Drawing.Size(160, 26);
            this.comboBoxModel.TabIndex = 5;

            // labelColor
            this.labelColor.AutoSize = true;
            this.labelColor.Location = new System.Drawing.Point(20, 150);
            this.labelColor.Name = "labelColor";
            this.labelColor.Size = new System.Drawing.Size(49, 18);
            this.labelColor.TabIndex = 6;
            this.labelColor.Text = "Цвет:";

            // comboBoxColor
            this.comboBoxColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxColor.FormattingEnabled = true;
            this.comboBoxColor.Location = new System.Drawing.Point(120, 147);
            this.comboBoxColor.Name = "comboBoxColor";
            this.comboBoxColor.Size = new System.Drawing.Size(160, 26);
            this.comboBoxColor.TabIndex = 7;

            // labelLicenseNumber
            this.labelLicenseNumber.AutoSize = true;
            this.labelLicenseNumber.Location = new System.Drawing.Point(20, 190);
            this.labelLicenseNumber.Name = "labelLicenseNumber";
            this.labelLicenseNumber.Size = new System.Drawing.Size(88, 18);
            this.labelLicenseNumber.TabIndex = 8;
            this.labelLicenseNumber.Text = "Госномер:";

            // textBoxLicenseNumber
            this.textBoxLicenseNumber.Location = new System.Drawing.Point(120, 187);
            this.textBoxLicenseNumber.MaxLength = 6;
            this.textBoxLicenseNumber.Name = "textBoxLicenseNumber";
            this.textBoxLicenseNumber.Size = new System.Drawing.Size(100, 24);
            this.textBoxLicenseNumber.TabIndex = 9;
            this.textBoxLicenseNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxLicenseNumber_KeyPress);

            // labelRegionCode
            this.labelRegionCode.AutoSize = true;
            this.labelRegionCode.Location = new System.Drawing.Point(230, 190);
            this.labelRegionCode.Name = "labelRegionCode";
            this.labelRegionCode.Size = new System.Drawing.Size(28, 18);
            this.labelRegionCode.TabIndex = 10;
            this.labelRegionCode.Text = "Рег:";

            // textBoxRegionCode
            this.textBoxRegionCode.Location = new System.Drawing.Point(260, 187);
            this.textBoxRegionCode.MaxLength = 3;
            this.textBoxRegionCode.Name = "textBoxRegionCode";
            this.textBoxRegionCode.Size = new System.Drawing.Size(50, 24);
            this.textBoxRegionCode.TabIndex = 11;
            this.textBoxRegionCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxRegionCode_KeyPress);

            // labelYear
            this.labelYear.AutoSize = true;
            this.labelYear.Location = new System.Drawing.Point(20, 230);
            this.labelYear.Name = "labelYear";
            this.labelYear.Size = new System.Drawing.Size(98, 18);
            this.labelYear.TabIndex = 12;
            this.labelYear.Text = "Год выпуска:";

            // textBoxYear
            this.textBoxYear.Location = new System.Drawing.Point(120, 227);
            this.textBoxYear.MaxLength = 4;
            this.textBoxYear.Name = "textBoxYear";
            this.textBoxYear.Size = new System.Drawing.Size(160, 24);
            this.textBoxYear.TabIndex = 13;
            this.textBoxYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxYear_KeyPress);

            // labelDriver
            this.labelDriver.AutoSize = true;
            this.labelDriver.Location = new System.Drawing.Point(20, 270);
            this.labelDriver.Name = "labelDriver";
            this.labelDriver.Size = new System.Drawing.Size(69, 18);
            this.labelDriver.TabIndex = 14;
            this.labelDriver.Text = "Водитель:";

            // comboBoxDriver
            this.comboBoxDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDriver.FormattingEnabled = true;
            this.comboBoxDriver.Location = new System.Drawing.Point(120, 267);
            this.comboBoxDriver.Name = "comboBoxDriver";
            this.comboBoxDriver.Size = new System.Drawing.Size(190, 26);
            this.comboBoxDriver.TabIndex = 15;

            // buttonSave
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSave.ForeColor = System.Drawing.Color.White;
            this.buttonSave.Location = new System.Drawing.Point(20, 320);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(360, 40);
            this.buttonSave.TabIndex = 16;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);

            // buttonAdd
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
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

            // buttonEdit
            this.buttonEdit.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
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

            // buttonDelete
            this.buttonDelete.BackColor = System.Drawing.Color.FromArgb(244, 67, 54);
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

            // buttonRefresh
            this.buttonRefresh.BackColor = System.Drawing.Color.FromArgb(156, 39, 176);
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

            // textBoxSearch
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textBoxSearch.Location = new System.Drawing.Point(630, 30);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(180, 24);
            this.textBoxSearch.TabIndex = 6;
            this.textBoxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSearch_KeyDown);

            // label8
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(540, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 18);
            this.label8.TabIndex = 7;
            this.label8.Text = "Поиск:";

            // buttonSearch
            this.buttonSearch.BackColor = System.Drawing.Color.FromArgb(0, 150, 136);
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

            // buttonBack
            this.buttonBack.BackColor = System.Drawing.Color.FromArgb(158, 158, 158);
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

            // AdminCarForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1120, 625);
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
            this.MinimumSize = new System.Drawing.Size(1136, 664);
            this.Name = "AdminCarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление автомобилями";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCars)).EndInit();
            this.groupBoxCarData.ResumeLayout(false);
            this.groupBoxCarData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}