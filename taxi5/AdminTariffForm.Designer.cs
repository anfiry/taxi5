namespace taxi4
{
    partial class AdminTariffForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewTariffs;
        private System.Windows.Forms.GroupBox groupBoxTariffData;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label label8;

        private System.Windows.Forms.Label labelTariffId;
        private System.Windows.Forms.TextBox textBoxTariffId;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelBaseCost;
        private System.Windows.Forms.TextBox textBoxBaseCost;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ComboBox comboBoxStatus;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewTariffs = new System.Windows.Forms.DataGridView();
            this.groupBoxTariffData = new System.Windows.Forms.GroupBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.textBoxBaseCost = new System.Windows.Forms.TextBox();
            this.labelBaseCost = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxTariffId = new System.Windows.Forms.TextBox();
            this.labelTariffId = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTariffs)).BeginInit();
            this.groupBoxTariffData.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewTariffs
            // 
            this.dataGridViewTariffs.AllowUserToAddRows = false;
            this.dataGridViewTariffs.AllowUserToDeleteRows = false;
            this.dataGridViewTariffs.AllowUserToResizeRows = false;
            this.dataGridViewTariffs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTariffs.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewTariffs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewTariffs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTariffs.Location = new System.Drawing.Point(12, 70);
            this.dataGridViewTariffs.Name = "dataGridViewTariffs";
            this.dataGridViewTariffs.ReadOnly = true;
            this.dataGridViewTariffs.RowHeadersVisible = false;
            this.dataGridViewTariffs.RowHeadersWidth = 51;
            this.dataGridViewTariffs.Size = new System.Drawing.Size(650, 543);
            this.dataGridViewTariffs.TabIndex = 0;
            this.dataGridViewTariffs.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTariffs_CellDoubleClick);
            // 
            // groupBoxTariffData
            // 
            this.groupBoxTariffData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTariffData.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBoxTariffData.Controls.Add(this.buttonSave);
            this.groupBoxTariffData.Controls.Add(this.labelStatus);
            this.groupBoxTariffData.Controls.Add(this.comboBoxStatus);
            this.groupBoxTariffData.Controls.Add(this.textBoxBaseCost);
            this.groupBoxTariffData.Controls.Add(this.labelBaseCost);
            this.groupBoxTariffData.Controls.Add(this.textBoxName);
            this.groupBoxTariffData.Controls.Add(this.labelName);
            this.groupBoxTariffData.Controls.Add(this.textBoxTariffId);
            this.groupBoxTariffData.Controls.Add(this.labelTariffId);
            this.groupBoxTariffData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBoxTariffData.Location = new System.Drawing.Point(680, 70);
            this.groupBoxTariffData.Name = "groupBoxTariffData";
            this.groupBoxTariffData.Size = new System.Drawing.Size(380, 543);
            this.groupBoxTariffData.TabIndex = 1;
            this.groupBoxTariffData.TabStop = false;
            this.groupBoxTariffData.Text = "Данные тарифа";
            // 
            // buttonSave
            // 
            this.buttonSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(52)))), ((int)(((byte)(247)))));
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonSave.Location = new System.Drawing.Point(20, 200);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(340, 40);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = false;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(20, 150);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(60, 18);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "Статус:";
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(120, 147);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(230, 26);
            this.comboBoxStatus.TabIndex = 7;
            // 
            // textBoxBaseCost
            // 
            this.textBoxBaseCost.Location = new System.Drawing.Point(160, 107);
            this.textBoxBaseCost.Name = "textBoxBaseCost";
            this.textBoxBaseCost.Size = new System.Drawing.Size(190, 24);
            this.textBoxBaseCost.TabIndex = 5;
            this.textBoxBaseCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxBaseCost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxBaseCost_KeyPress);
            // 
            // labelBaseCost
            // 
            this.labelBaseCost.AutoSize = true;
            this.labelBaseCost.Location = new System.Drawing.Point(20, 110);
            this.labelBaseCost.Name = "labelBaseCost";
            this.labelBaseCost.Size = new System.Drawing.Size(150, 18);
            this.labelBaseCost.TabIndex = 4;
            this.labelBaseCost.Text = "Базовая стоимость:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(120, 67);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(230, 24);
            this.textBoxName.TabIndex = 3;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(20, 70);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(79, 18);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "Название:";
            // 
            // textBoxTariffId
            // 
            this.textBoxTariffId.Location = new System.Drawing.Point(120, 27);
            this.textBoxTariffId.Name = "textBoxTariffId";
            this.textBoxTariffId.ReadOnly = true;
            this.textBoxTariffId.Size = new System.Drawing.Size(80, 24);
            this.textBoxTariffId.TabIndex = 1;
            this.textBoxTariffId.Visible = false;
            // 
            // labelTariffId
            // 
            this.labelTariffId.AutoSize = true;
            this.labelTariffId.Location = new System.Drawing.Point(20, 30);
            this.labelTariffId.Name = "labelTariffId";
            this.labelTariffId.Size = new System.Drawing.Size(26, 18);
            this.labelTariffId.TabIndex = 0;
            this.labelTariffId.Text = "ID:";
            this.labelTariffId.Visible = false;
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
            this.buttonEdit.Size = new System.Drawing.Size(120, 40);
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
            this.buttonRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(228)))), ((int)(((byte)(255)))));
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonRefresh.Location = new System.Drawing.Point(402, 20);
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
            this.buttonSearch.Location = new System.Drawing.Point(824, 20);
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
            this.buttonBack.Location = new System.Drawing.Point(920, 20);
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
            this.textBoxSearch.Location = new System.Drawing.Point(620, 30);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(180, 24);
            this.textBoxSearch.TabIndex = 6;
            this.textBoxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSearch_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(552, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 18);
            this.label8.TabIndex = 7;
            this.label8.Text = "Поиск:";
            // 
            // AdminTariffForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1072, 625);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.groupBoxTariffData);
            this.Controls.Add(this.dataGridViewTariffs);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.MinimumSize = new System.Drawing.Size(1090, 672);
            this.Name = "AdminTariffForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление тарифами";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTariffs)).EndInit();
            this.groupBoxTariffData.ResumeLayout(false);
            this.groupBoxTariffData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}