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

        // Элементы данных автомобиля
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

        // --- КНОПКИ "НОВЫЙ" (СПРАВА ОТ КОМБОБОКСОВ) ---
        private System.Windows.Forms.Button btnNewBrand;
        private System.Windows.Forms.Button btnNewModel;
        private System.Windows.Forms.Button btnNewColor;

        // --- ПАНЕЛИ ДЛЯ ДОБАВЛЕНИЯ НОВЫХ ЗАПИСЕЙ (ПОЯВЛЯЮТСЯ НА МЕСТЕ КОМБОБОКСОВ) ---
        private System.Windows.Forms.Panel panelNewBrand;
        private System.Windows.Forms.TextBox txtNewBrand;
        private System.Windows.Forms.Button btnSaveNewBrand;
        private System.Windows.Forms.Button btnCancelNewBrand;

        private System.Windows.Forms.Panel panelNewModel;
        private System.Windows.Forms.TextBox txtNewModel;
        private System.Windows.Forms.Button btnSaveNewModel;
        private System.Windows.Forms.Button btnCancelNewModel;

        private System.Windows.Forms.Panel panelNewColor;
        private System.Windows.Forms.TextBox txtNewColor;
        private System.Windows.Forms.Button btnSaveNewColor;
        private System.Windows.Forms.Button btnCancelNewColor;

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

            // --- КНОПКИ "НОВЫЙ" ---
            this.btnNewBrand = new System.Windows.Forms.Button();
            this.btnNewModel = new System.Windows.Forms.Button();
            this.btnNewColor = new System.Windows.Forms.Button();

            // --- ПАНЕЛИ ---
            this.panelNewBrand = new System.Windows.Forms.Panel();
            this.txtNewBrand = new System.Windows.Forms.TextBox();
            this.btnSaveNewBrand = new System.Windows.Forms.Button();
            this.btnCancelNewBrand = new System.Windows.Forms.Button();

            this.panelNewModel = new System.Windows.Forms.Panel();
            this.txtNewModel = new System.Windows.Forms.TextBox();
            this.btnSaveNewModel = new System.Windows.Forms.Button();
            this.btnCancelNewModel = new System.Windows.Forms.Button();

            this.panelNewColor = new System.Windows.Forms.Panel();
            this.txtNewColor = new System.Windows.Forms.TextBox();
            this.btnSaveNewColor = new System.Windows.Forms.Button();
            this.btnCancelNewColor = new System.Windows.Forms.Button();

            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCars)).BeginInit();
            this.groupBoxCarData.SuspendLayout();
            this.panelNewBrand.SuspendLayout();
            this.panelNewModel.SuspendLayout();
            this.panelNewColor.SuspendLayout();
            this.SuspendLayout();

            // 
            // dataGridViewCars
            // 
            this.dataGridViewCars.AllowUserToAddRows = false;
            this.dataGridViewCars.AllowUserToDeleteRows = false;
            this.dataGridViewCars.AllowUserToResizeRows = false;
            this.dataGridViewCars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewCars.AutoGenerateColumns = true;
            this.dataGridViewCars.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dataGridViewCars.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewCars.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewCars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCars.Location = new System.Drawing.Point(12, 70);
            this.dataGridViewCars.Name = "dataGridViewCars";
            this.dataGridViewCars.ReadOnly = true;
            this.dataGridViewCars.RowHeadersVisible = false;
            this.dataGridViewCars.RowHeadersWidth = 51;
            this.dataGridViewCars.Size = new System.Drawing.Size(750, 543);
            this.dataGridViewCars.TabIndex = 0;
            this.dataGridViewCars.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCars_CellDoubleClick);

            // 
            // groupBoxCarData
            // 
            this.groupBoxCarData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxCarData.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBoxCarData.Controls.Add(this.btnNewColor);
            this.groupBoxCarData.Controls.Add(this.btnNewModel);
            this.groupBoxCarData.Controls.Add(this.btnNewBrand);
            this.groupBoxCarData.Controls.Add(this.panelNewColor);
            this.groupBoxCarData.Controls.Add(this.panelNewModel);
            this.groupBoxCarData.Controls.Add(this.panelNewBrand);
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
            this.groupBoxCarData.Location = new System.Drawing.Point(780, 70);
            this.groupBoxCarData.Name = "groupBoxCarData";
            this.groupBoxCarData.Size = new System.Drawing.Size(390, 543);
            this.groupBoxCarData.TabIndex = 1;
            this.groupBoxCarData.TabStop = false;
            this.groupBoxCarData.Text = "Данные автомобиля";

            // 
            // labelCarId
            // 
            this.labelCarId.AutoSize = true;
            this.labelCarId.Location = new System.Drawing.Point(20, 30);
            this.labelCarId.Name = "labelCarId";
            this.labelCarId.Size = new System.Drawing.Size(25, 18);
            this.labelCarId.TabIndex = 0;
            this.labelCarId.Text = "ID:";
            this.labelCarId.Visible = false;

            // 
            // textBoxCarId
            // 
            this.textBoxCarId.Location = new System.Drawing.Point(120, 27);
            this.textBoxCarId.Name = "textBoxCarId";
            this.textBoxCarId.ReadOnly = true;
            this.textBoxCarId.Size = new System.Drawing.Size(80, 24);
            this.textBoxCarId.TabIndex = 1;
            this.textBoxCarId.Visible = false;

            // 
            // labelBrand
            // 
            this.labelBrand.AutoSize = true;
            this.labelBrand.Location = new System.Drawing.Point(20, 70);
            this.labelBrand.Name = "labelBrand";
            this.labelBrand.Size = new System.Drawing.Size(55, 18);
            this.labelBrand.TabIndex = 2;
            this.labelBrand.Text = "Марка:";

            // 
            // comboBoxBrand
            // 
            this.comboBoxBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBrand.FormattingEnabled = true;
            this.comboBoxBrand.Location = new System.Drawing.Point(120, 67);
            this.comboBoxBrand.Name = "comboBoxBrand";
            this.comboBoxBrand.Size = new System.Drawing.Size(160, 26);
            this.comboBoxBrand.TabIndex = 3;
            this.comboBoxBrand.SelectedIndexChanged += new System.EventHandler(this.comboBoxBrand_SelectedIndexChanged);

            // 
            // btnNewBrand
            // 
            this.btnNewBrand.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            this.btnNewBrand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewBrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnNewBrand.ForeColor = System.Drawing.Color.White;
            this.btnNewBrand.Location = new System.Drawing.Point(290, 67);
            this.btnNewBrand.Name = "btnNewBrand";
            this.btnNewBrand.Size = new System.Drawing.Size(30, 26);
            this.btnNewBrand.TabIndex = 40;
            this.btnNewBrand.Text = "+";
            this.btnNewBrand.UseVisualStyleBackColor = false;
            this.btnNewBrand.Click += new System.EventHandler(this.btnNewBrand_Click);

            // 
            // panelNewBrand
            // 
            this.panelNewBrand.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.panelNewBrand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNewBrand.Controls.Add(this.txtNewBrand);
            this.panelNewBrand.Controls.Add(this.btnSaveNewBrand);
            this.panelNewBrand.Controls.Add(this.btnCancelNewBrand);
            this.panelNewBrand.Location = new System.Drawing.Point(120, 67);
            this.panelNewBrand.Name = "panelNewBrand";
            this.panelNewBrand.Size = new System.Drawing.Size(200, 60);
            this.panelNewBrand.TabIndex = 41;
            this.panelNewBrand.Visible = false;

            // 
            // txtNewBrand
            // 
            this.txtNewBrand.Location = new System.Drawing.Point(10, 10);
            this.txtNewBrand.Name = "txtNewBrand";
            this.txtNewBrand.Size = new System.Drawing.Size(180, 24);
            this.txtNewBrand.TabIndex = 0;
            this.txtNewBrand.Enter += new System.EventHandler(this.txtNewBrand_Enter);
            this.txtNewBrand.Leave += new System.EventHandler(this.txtNewBrand_Leave);

            // 
            // btnSaveNewBrand
            // 
            this.btnSaveNewBrand.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnSaveNewBrand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveNewBrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnSaveNewBrand.ForeColor = System.Drawing.Color.White;
            this.btnSaveNewBrand.Location = new System.Drawing.Point(10, 35);
            this.btnSaveNewBrand.Name = "btnSaveNewBrand";
            this.btnSaveNewBrand.Size = new System.Drawing.Size(80, 25);
            this.btnSaveNewBrand.TabIndex = 1;
            this.btnSaveNewBrand.Text = "Сохранить";
            this.btnSaveNewBrand.UseVisualStyleBackColor = false;
            this.btnSaveNewBrand.Click += new System.EventHandler(this.btnSaveNewBrand_Click);

            // 
            // btnCancelNewBrand
            // 
            this.btnCancelNewBrand.BackColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.btnCancelNewBrand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelNewBrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnCancelNewBrand.ForeColor = System.Drawing.Color.White;
            this.btnCancelNewBrand.Location = new System.Drawing.Point(100, 35);
            this.btnCancelNewBrand.Name = "btnCancelNewBrand";
            this.btnCancelNewBrand.Size = new System.Drawing.Size(80, 25);
            this.btnCancelNewBrand.TabIndex = 2;
            this.btnCancelNewBrand.Text = "Отмена";
            this.btnCancelNewBrand.UseVisualStyleBackColor = false;
            this.btnCancelNewBrand.Click += new System.EventHandler(this.btnCancelNewBrand_Click);

            // 
            // labelModel
            // 
            this.labelModel.AutoSize = true;
            this.labelModel.Location = new System.Drawing.Point(20, 110);
            this.labelModel.Name = "labelModel";
            this.labelModel.Size = new System.Drawing.Size(62, 18);
            this.labelModel.TabIndex = 4;
            this.labelModel.Text = "Модель:";

            // 
            // comboBoxModel
            // 
            this.comboBoxModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxModel.FormattingEnabled = true;
            this.comboBoxModel.Location = new System.Drawing.Point(120, 107);
            this.comboBoxModel.Name = "comboBoxModel";
            this.comboBoxModel.Size = new System.Drawing.Size(160, 26);
            this.comboBoxModel.TabIndex = 5;

            // 
            // btnNewModel
            // 
            this.btnNewModel.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            this.btnNewModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnNewModel.ForeColor = System.Drawing.Color.White;
            this.btnNewModel.Location = new System.Drawing.Point(290, 107);
            this.btnNewModel.Name = "btnNewModel";
            this.btnNewModel.Size = new System.Drawing.Size(30, 26);
            this.btnNewModel.TabIndex = 42;
            this.btnNewModel.Text = "+";
            this.btnNewModel.UseVisualStyleBackColor = false;
            this.btnNewModel.Click += new System.EventHandler(this.btnNewModel_Click);

            // 
            // panelNewModel
            // 
            this.panelNewModel.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.panelNewModel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNewModel.Controls.Add(this.txtNewModel);
            this.panelNewModel.Controls.Add(this.btnSaveNewModel);
            this.panelNewModel.Controls.Add(this.btnCancelNewModel);
            this.panelNewModel.Location = new System.Drawing.Point(120, 107);
            this.panelNewModel.Name = "panelNewModel";
            this.panelNewModel.Size = new System.Drawing.Size(200, 60);
            this.panelNewModel.TabIndex = 43;
            this.panelNewModel.Visible = false;

            // 
            // txtNewModel
            // 
            this.txtNewModel.Location = new System.Drawing.Point(10, 10);
            this.txtNewModel.Name = "txtNewModel";
            this.txtNewModel.Size = new System.Drawing.Size(180, 24);
            this.txtNewModel.TabIndex = 0;
            this.txtNewModel.Enter += new System.EventHandler(this.txtNewModel_Enter);
            this.txtNewModel.Leave += new System.EventHandler(this.txtNewModel_Leave);

            // 
            // btnSaveNewModel
            // 
            this.btnSaveNewModel.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnSaveNewModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveNewModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnSaveNewModel.ForeColor = System.Drawing.Color.White;
            this.btnSaveNewModel.Location = new System.Drawing.Point(10, 35);
            this.btnSaveNewModel.Name = "btnSaveNewModel";
            this.btnSaveNewModel.Size = new System.Drawing.Size(80, 25);
            this.btnSaveNewModel.TabIndex = 1;
            this.btnSaveNewModel.Text = "Сохранить";
            this.btnSaveNewModel.UseVisualStyleBackColor = false;
            this.btnSaveNewModel.Click += new System.EventHandler(this.btnSaveNewModel_Click);

            // 
            // btnCancelNewModel
            // 
            this.btnCancelNewModel.BackColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.btnCancelNewModel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelNewModel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnCancelNewModel.ForeColor = System.Drawing.Color.White;
            this.btnCancelNewModel.Location = new System.Drawing.Point(100, 35);
            this.btnCancelNewModel.Name = "btnCancelNewModel";
            this.btnCancelNewModel.Size = new System.Drawing.Size(80, 25);
            this.btnCancelNewModel.TabIndex = 2;
            this.btnCancelNewModel.Text = "Отмена";
            this.btnCancelNewModel.UseVisualStyleBackColor = false;
            this.btnCancelNewModel.Click += new System.EventHandler(this.btnCancelNewModel_Click);

            // 
            // labelColor
            // 
            this.labelColor.AutoSize = true;
            this.labelColor.Location = new System.Drawing.Point(20, 150);
            this.labelColor.Name = "labelColor";
            this.labelColor.Size = new System.Drawing.Size(49, 18);
            this.labelColor.TabIndex = 6;
            this.labelColor.Text = "Цвет:";

            // 
            // comboBoxColor
            // 
            this.comboBoxColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxColor.FormattingEnabled = true;
            this.comboBoxColor.Location = new System.Drawing.Point(120, 147);
            this.comboBoxColor.Name = "comboBoxColor";
            this.comboBoxColor.Size = new System.Drawing.Size(160, 26);
            this.comboBoxColor.TabIndex = 7;

            // 
            // btnNewColor
            // 
            this.btnNewColor.BackColor = System.Drawing.Color.FromArgb(33, 150, 243);
            this.btnNewColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnNewColor.ForeColor = System.Drawing.Color.White;
            this.btnNewColor.Location = new System.Drawing.Point(290, 147);
            this.btnNewColor.Name = "btnNewColor";
            this.btnNewColor.Size = new System.Drawing.Size(30, 26);
            this.btnNewColor.TabIndex = 44;
            this.btnNewColor.Text = "+";
            this.btnNewColor.UseVisualStyleBackColor = false;
            this.btnNewColor.Click += new System.EventHandler(this.btnNewColor_Click);

            // 
            // panelNewColor
            // 
            this.panelNewColor.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            this.panelNewColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNewColor.Controls.Add(this.txtNewColor);
            this.panelNewColor.Controls.Add(this.btnSaveNewColor);
            this.panelNewColor.Controls.Add(this.btnCancelNewColor);
            this.panelNewColor.Location = new System.Drawing.Point(120, 147);
            this.panelNewColor.Name = "panelNewColor";
            this.panelNewColor.Size = new System.Drawing.Size(200, 60);
            this.panelNewColor.TabIndex = 45;
            this.panelNewColor.Visible = false;

            // 
            // txtNewColor
            // 
            this.txtNewColor.Location = new System.Drawing.Point(10, 10);
            this.txtNewColor.Name = "txtNewColor";
            this.txtNewColor.Size = new System.Drawing.Size(180, 24);
            this.txtNewColor.TabIndex = 0;
            this.txtNewColor.Enter += new System.EventHandler(this.txtNewColor_Enter);
            this.txtNewColor.Leave += new System.EventHandler(this.txtNewColor_Leave);

            // 
            // btnSaveNewColor
            // 
            this.btnSaveNewColor.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.btnSaveNewColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveNewColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnSaveNewColor.ForeColor = System.Drawing.Color.White;
            this.btnSaveNewColor.Location = new System.Drawing.Point(10, 35);
            this.btnSaveNewColor.Name = "btnSaveNewColor";
            this.btnSaveNewColor.Size = new System.Drawing.Size(80, 25);
            this.btnSaveNewColor.TabIndex = 1;
            this.btnSaveNewColor.Text = "Сохранить";
            this.btnSaveNewColor.UseVisualStyleBackColor = false;
            this.btnSaveNewColor.Click += new System.EventHandler(this.btnSaveNewColor_Click);

            // 
            // btnCancelNewColor
            // 
            this.btnCancelNewColor.BackColor = System.Drawing.Color.FromArgb(158, 158, 158);
            this.btnCancelNewColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelNewColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btnCancelNewColor.ForeColor = System.Drawing.Color.White;
            this.btnCancelNewColor.Location = new System.Drawing.Point(100, 35);
            this.btnCancelNewColor.Name = "btnCancelNewColor";
            this.btnCancelNewColor.Size = new System.Drawing.Size(80, 25);
            this.btnCancelNewColor.TabIndex = 2;
            this.btnCancelNewColor.Text = "Отмена";
            this.btnCancelNewColor.UseVisualStyleBackColor = false;
            this.btnCancelNewColor.Click += new System.EventHandler(this.btnCancelNewColor_Click);

            // 
            // labelLicenseNumber
            // 
            this.labelLicenseNumber.AutoSize = true;
            this.labelLicenseNumber.Location = new System.Drawing.Point(20, 190);
            this.labelLicenseNumber.Name = "labelLicenseNumber";
            this.labelLicenseNumber.Size = new System.Drawing.Size(88, 18);
            this.labelLicenseNumber.TabIndex = 8;
            this.labelLicenseNumber.Text = "Госномер:";

            // 
            // textBoxLicenseNumber
            // 
            this.textBoxLicenseNumber.Location = new System.Drawing.Point(120, 187);
            this.textBoxLicenseNumber.MaxLength = 6;
            this.textBoxLicenseNumber.Name = "textBoxLicenseNumber";
            this.textBoxLicenseNumber.Size = new System.Drawing.Size(100, 24);
            this.textBoxLicenseNumber.TabIndex = 9;
            this.textBoxLicenseNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxLicenseNumber_KeyPress);

            // 
            // labelRegionCode
            // 
            this.labelRegionCode.AutoSize = true;
            this.labelRegionCode.Location = new System.Drawing.Point(230, 190);
            this.labelRegionCode.Name = "labelRegionCode";
            this.labelRegionCode.Size = new System.Drawing.Size(28, 18);
            this.labelRegionCode.TabIndex = 10;
            this.labelRegionCode.Text = "Рег:";

            // 
            // textBoxRegionCode
            // 
            this.textBoxRegionCode.Location = new System.Drawing.Point(260, 187);
            this.textBoxRegionCode.MaxLength = 3;
            this.textBoxRegionCode.Name = "textBoxRegionCode";
            this.textBoxRegionCode.Size = new System.Drawing.Size(50, 24);
            this.textBoxRegionCode.TabIndex = 11;
            this.textBoxRegionCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxRegionCode_KeyPress);

            // 
            // labelYear
            // 
            this.labelYear.AutoSize = true;
            this.labelYear.Location = new System.Drawing.Point(20, 230);
            this.labelYear.Name = "labelYear";
            this.labelYear.Size = new System.Drawing.Size(98, 18);
            this.labelYear.TabIndex = 12;
            this.labelYear.Text = "Год выпуска:";

            // 
            // textBoxYear
            // 
            this.textBoxYear.Location = new System.Drawing.Point(120, 227);
            this.textBoxYear.MaxLength = 4;
            this.textBoxYear.Name = "textBoxYear";
            this.textBoxYear.Size = new System.Drawing.Size(160, 24);
            this.textBoxYear.TabIndex = 13;
            this.textBoxYear.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxYear_KeyPress);

            // 
            // labelDriver
            // 
            this.labelDriver.AutoSize = true;
            this.labelDriver.Location = new System.Drawing.Point(20, 270);
            this.labelDriver.Name = "labelDriver";
            this.labelDriver.Size = new System.Drawing.Size(69, 18);
            this.labelDriver.TabIndex = 14;
            this.labelDriver.Text = "Водитель:";

            // 
            // comboBoxDriver
            // 
            this.comboBoxDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDriver.FormattingEnabled = true;
            this.comboBoxDriver.Location = new System.Drawing.Point(120, 267);
            this.comboBoxDriver.Name = "comboBoxDriver";
            this.comboBoxDriver.Size = new System.Drawing.Size(190, 26);
            this.comboBoxDriver.TabIndex = 15;

            // 
            // buttonSave
            // 
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

            // 
            // buttonAdd
            // 
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

            // 
            // buttonEdit
            // 
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

            // 
            // buttonDelete
            // 
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

            // 
            // buttonRefresh
            // 
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

            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textBoxSearch.Location = new System.Drawing.Point(630, 30);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(180, 24);
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

            // 
            // buttonBack
            // 
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

            // 
            // AdminCarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1182, 625);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCars)).EndInit();
            this.groupBoxCarData.ResumeLayout(false);
            this.groupBoxCarData.PerformLayout();
            this.panelNewBrand.ResumeLayout(false);
            this.panelNewBrand.PerformLayout();
            this.panelNewModel.ResumeLayout(false);
            this.panelNewModel.PerformLayout();
            this.panelNewColor.ResumeLayout(false);
            this.panelNewColor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}