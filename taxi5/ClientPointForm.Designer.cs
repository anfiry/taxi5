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
        private System.Windows.Forms.Panel panelTop;

        private System.Windows.Forms.Label labelPointId;
        private System.Windows.Forms.TextBox textBoxPointId;
        private System.Windows.Forms.Label labelPointName;
        private System.Windows.Forms.TextBox txtPointName;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.TextBox txtAddress;
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
            this.labelAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
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
            this.panelTop = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPoints)).BeginInit();
            this.groupBoxPointData.SuspendLayout();
            this.panelNewType.SuspendLayout();
            this.panelTop.SuspendLayout();
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
            this.dataGridViewPoints.Location = new System.Drawing.Point(20, 90);
            this.dataGridViewPoints.Name = "dataGridViewPoints";
            this.dataGridViewPoints.ReadOnly = true;
            this.dataGridViewPoints.RowHeadersVisible = false;
            this.dataGridViewPoints.RowHeadersWidth = 51;
            this.dataGridViewPoints.Size = new System.Drawing.Size(810, 600);
            this.dataGridViewPoints.TabIndex = 1;
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
            this.groupBoxPointData.Controls.Add(this.labelAddress);
            this.groupBoxPointData.Controls.Add(this.txtAddress);
            this.groupBoxPointData.Controls.Add(this.labelType);
            this.groupBoxPointData.Controls.Add(this.comboBoxType);
            this.groupBoxPointData.Controls.Add(this.labelPointName);
            this.groupBoxPointData.Controls.Add(this.txtPointName);
            this.groupBoxPointData.Controls.Add(this.textBoxPointId);
            this.groupBoxPointData.Controls.Add(this.labelPointId);
            this.groupBoxPointData.Controls.Add(this.txtAddressId);
            this.groupBoxPointData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBoxPointData.Location = new System.Drawing.Point(849, 90);
            this.groupBoxPointData.Name = "groupBoxPointData";
            this.groupBoxPointData.Size = new System.Drawing.Size(495, 600);
            this.groupBoxPointData.TabIndex = 2;
            this.groupBoxPointData.TabStop = false;
            this.groupBoxPointData.Text = "Данные точки";
            this.groupBoxPointData.Visible = false;
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
            this.panelNewType.Size = new System.Drawing.Size(213, 85);
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
            this.btnSaveNewType.Location = new System.Drawing.Point(3, 40);
            this.btnSaveNewType.Name = "btnSaveNewType";
            this.btnSaveNewType.Size = new System.Drawing.Size(109, 33);
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
            this.btnCancelNewType.Location = new System.Drawing.Point(118, 40);
            this.btnCancelNewType.Name = "btnCancelNewType";
            this.btnCancelNewType.Size = new System.Drawing.Size(90, 32);
            this.btnCancelNewType.TabIndex = 2;
            this.btnCancelNewType.Text = "Отмена";
            this.btnCancelNewType.UseVisualStyleBackColor = false;
            this.btnCancelNewType.Click += new System.EventHandler(this.btnCancelNewType_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(52)))), ((int)(((byte)(247)))));
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonSave.Location = new System.Drawing.Point(71, 491);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(360, 40);
            this.buttonSave.TabIndex = 16;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(20, 150);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(54, 18);
            this.labelAddress.TabIndex = 8;
            this.labelAddress.Text = "Адрес:";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(120, 147);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(333, 24);
            this.txtAddress.TabIndex = 9;
            this.txtAddress.Enter += new System.EventHandler(this.txtAddress_Enter);
            this.txtAddress.Leave += new System.EventHandler(this.txtAddress_Leave);
            // 
            // labelType
            // 
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(20, 100);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(81, 18);
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
            this.labelPointName.Size = new System.Drawing.Size(79, 18);
            this.labelPointName.TabIndex = 2;
            this.labelPointName.Text = "Название:";
            // 
            // txtPointName
            // 
            this.txtPointName.Location = new System.Drawing.Point(120, 57);
            this.txtPointName.Name = "txtPointName";
            this.txtPointName.Size = new System.Drawing.Size(213, 24);
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
            this.txtAddressId.Location = new System.Drawing.Point(220, 27);
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
            this.buttonAdd.Location = new System.Drawing.Point(20, 15);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(124, 40);
            this.buttonAdd.TabIndex = 0;
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
            this.buttonEdit.Location = new System.Drawing.Point(161, 15);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(163, 40);
            this.buttonEdit.TabIndex = 1;
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
            this.buttonDelete.Location = new System.Drawing.Point(346, 15);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(134, 40);
            this.buttonDelete.TabIndex = 2;
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
            this.buttonRefresh.Location = new System.Drawing.Point(497, 15);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(136, 40);
            this.buttonRefresh.TabIndex = 3;
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(227)))), ((int)(((byte)(255)))));
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonSearch.Location = new System.Drawing.Point(1031, 15);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(90, 40);
            this.buttonSearch.TabIndex = 6;
            this.buttonSearch.Text = "Найти";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonBack.Location = new System.Drawing.Point(1234, 15);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(100, 40);
            this.buttonBack.TabIndex = 7;
            this.buttonBack.Text = "Назад";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.textBoxSearch.Location = new System.Drawing.Point(821, 23);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(200, 24);
            this.textBoxSearch.TabIndex = 4;
            this.textBoxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSearch_KeyDown);
            // 
            // labelSearch
            // 
            this.labelSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelSearch.AutoSize = true;
            this.labelSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.labelSearch.Location = new System.Drawing.Point(741, 28);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(62, 18);
            this.labelSearch.TabIndex = 5;
            this.labelSearch.Text = "Поиск:";
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.White;
            this.panelTop.Controls.Add(this.buttonAdd);
            this.panelTop.Controls.Add(this.buttonEdit);
            this.panelTop.Controls.Add(this.buttonDelete);
            this.panelTop.Controls.Add(this.buttonRefresh);
            this.panelTop.Controls.Add(this.buttonSearch);
            this.panelTop.Controls.Add(this.buttonBack);
            this.panelTop.Controls.Add(this.textBoxSearch);
            this.panelTop.Controls.Add(this.labelSearch);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1364, 70);
            this.panelTop.TabIndex = 3;
            // 
            // ClientPointForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1364, 710);
            this.Controls.Add(this.groupBoxPointData);
            this.Controls.Add(this.dataGridViewPoints);
            this.Controls.Add(this.panelTop);
            this.MinimumSize = new System.Drawing.Size(1200, 672);
            this.Name = "ClientPointForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Мои адреса";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPoints)).EndInit();
            this.groupBoxPointData.ResumeLayout(false);
            this.groupBoxPointData.PerformLayout();
            this.panelNewType.ResumeLayout(false);
            this.panelNewType.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}