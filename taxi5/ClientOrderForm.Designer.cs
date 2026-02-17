namespace taxi4
{
    partial class ClientOrderForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelClientInfo = new System.Windows.Forms.Label();
            this.labelClientName = new System.Windows.Forms.Label();
            this.groupBoxRoute = new System.Windows.Forms.GroupBox();
            this.labelStart = new System.Windows.Forms.Label();
            this.cmbStartPoint = new System.Windows.Forms.ComboBox();
            this.labelEnd = new System.Windows.Forms.Label();
            this.cmbEndPoint = new System.Windows.Forms.ComboBox();
            this.groupBoxDetails = new System.Windows.Forms.GroupBox();
            this.labelPromo = new System.Windows.Forms.Label();
            this.cmbPromotion = new System.Windows.Forms.ComboBox();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.labelPrice = new System.Windows.Forms.Label();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.labelPayment = new System.Windows.Forms.Label();
            this.textBoxPayment = new System.Windows.Forms.TextBox();
            this.buttonAddNewPoint = new System.Windows.Forms.Button();
            this.panelNewAddress = new System.Windows.Forms.Panel();
            this.labelCity = new System.Windows.Forms.Label();
            this.textBoxCity = new System.Windows.Forms.TextBox();
            this.labelStreet = new System.Windows.Forms.Label();
            this.textBoxStreet = new System.Windows.Forms.TextBox();
            this.labelHouse = new System.Windows.Forms.Label();
            this.textBoxHouse = new System.Windows.Forms.TextBox();
            this.labelEntrance = new System.Windows.Forms.Label();
            this.textBoxEntrance = new System.Windows.Forms.TextBox();
            this.labelType = new System.Windows.Forms.Label();
            this.textBoxType = new System.Windows.Forms.TextBox();
            this.buttonSaveNewPoint = new System.Windows.Forms.Button();
            this.buttonCancelNewPoint = new System.Windows.Forms.Button();
            this.buttonOrder = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.groupBoxRoute.SuspendLayout();
            this.groupBoxDetails.SuspendLayout();
            this.panelNewAddress.SuspendLayout();
            this.SuspendLayout();

            // labelTitle
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.labelTitle.Location = new System.Drawing.Point(30, 20);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(243, 31);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Оформление заказа";

            // labelClientInfo
            this.labelClientInfo.AutoSize = true;
            this.labelClientInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.labelClientInfo.Location = new System.Drawing.Point(30, 60);
            this.labelClientInfo.Name = "labelClientInfo";
            this.labelClientInfo.Size = new System.Drawing.Size(72, 20);
            this.labelClientInfo.TabIndex = 1;
            this.labelClientInfo.Text = "Клиент:";

            // labelClientName
            this.labelClientName.AutoSize = true;
            this.labelClientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.labelClientName.Location = new System.Drawing.Point(110, 60);
            this.labelClientName.Name = "labelClientName";
            this.labelClientName.Size = new System.Drawing.Size(0, 20);
            this.labelClientName.TabIndex = 2;

            // groupBoxRoute
            this.groupBoxRoute.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBoxRoute.Controls.Add(this.labelStart);
            this.groupBoxRoute.Controls.Add(this.cmbStartPoint);
            this.groupBoxRoute.Controls.Add(this.labelEnd);
            this.groupBoxRoute.Controls.Add(this.cmbEndPoint);
            this.groupBoxRoute.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBoxRoute.Location = new System.Drawing.Point(30, 100);
            this.groupBoxRoute.Name = "groupBoxRoute";
            this.groupBoxRoute.Size = new System.Drawing.Size(540, 130);
            this.groupBoxRoute.TabIndex = 3;
            this.groupBoxRoute.TabStop = false;
            this.groupBoxRoute.Text = "Маршрут";

            // labelStart
            this.labelStart.AutoSize = true;
            this.labelStart.Location = new System.Drawing.Point(20, 40);
            this.labelStart.Name = "labelStart";
            this.labelStart.Size = new System.Drawing.Size(87, 18);
            this.labelStart.TabIndex = 0;
            this.labelStart.Text = "Отправление:";

            // cmbStartPoint
            this.cmbStartPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStartPoint.FormattingEnabled = true;
            this.cmbStartPoint.Location = new System.Drawing.Point(120, 37);
            this.cmbStartPoint.Name = "cmbStartPoint";
            this.cmbStartPoint.Size = new System.Drawing.Size(390, 26);
            this.cmbStartPoint.TabIndex = 1;

            // labelEnd
            this.labelEnd.AutoSize = true;
            this.labelEnd.Location = new System.Drawing.Point(20, 80);
            this.labelEnd.Name = "labelEnd";
            this.labelEnd.Size = new System.Drawing.Size(86, 18);
            this.labelEnd.TabIndex = 2;
            this.labelEnd.Text = "Назначение:";

            // cmbEndPoint
            this.cmbEndPoint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEndPoint.FormattingEnabled = true;
            this.cmbEndPoint.Location = new System.Drawing.Point(120, 77);
            this.cmbEndPoint.Name = "cmbEndPoint";
            this.cmbEndPoint.Size = new System.Drawing.Size(390, 26);
            this.cmbEndPoint.TabIndex = 3;

            // groupBoxDetails
            this.groupBoxDetails.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBoxDetails.Controls.Add(this.labelPromo);
            this.groupBoxDetails.Controls.Add(this.cmbPromotion);
            this.groupBoxDetails.Controls.Add(this.buttonCalculate);
            this.groupBoxDetails.Controls.Add(this.labelPrice);
            this.groupBoxDetails.Controls.Add(this.textBoxPrice);
            this.groupBoxDetails.Controls.Add(this.labelPayment);
            this.groupBoxDetails.Controls.Add(this.textBoxPayment);
            this.groupBoxDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBoxDetails.Location = new System.Drawing.Point(30, 250);
            this.groupBoxDetails.Name = "groupBoxDetails";
            this.groupBoxDetails.Size = new System.Drawing.Size(540, 160);
            this.groupBoxDetails.TabIndex = 4;
            this.groupBoxDetails.TabStop = false;
            this.groupBoxDetails.Text = "Детали заказа";

            // labelPromo
            this.labelPromo.AutoSize = true;
            this.labelPromo.Location = new System.Drawing.Point(20, 40);
            this.labelPromo.Name = "labelPromo";
            this.labelPromo.Size = new System.Drawing.Size(59, 18);
            this.labelPromo.TabIndex = 0;
            this.labelPromo.Text = "Акция:";

            // cmbPromotion
            this.cmbPromotion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPromotion.FormattingEnabled = true;
            this.cmbPromotion.Location = new System.Drawing.Point(120, 37);
            this.cmbPromotion.Name = "cmbPromotion";
            this.cmbPromotion.Size = new System.Drawing.Size(280, 26);
            this.cmbPromotion.TabIndex = 1;

            // buttonCalculate
            this.buttonCalculate.BackColor = System.Drawing.Color.FromArgb(230, 227, 255);
            this.buttonCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.buttonCalculate.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonCalculate.Location = new System.Drawing.Point(420, 35);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(100, 30);
            this.buttonCalculate.TabIndex = 2;
            this.buttonCalculate.Text = "Рассчитать";
            this.buttonCalculate.UseVisualStyleBackColor = false;
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);

            // labelPrice
            this.labelPrice.AutoSize = true;
            this.labelPrice.Location = new System.Drawing.Point(20, 80);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(85, 18);
            this.labelPrice.TabIndex = 3;
            this.labelPrice.Text = "Стоимость:";

            // textBoxPrice
            this.textBoxPrice.Location = new System.Drawing.Point(120, 77);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.ReadOnly = true;
            this.textBoxPrice.Size = new System.Drawing.Size(150, 24);
            this.textBoxPrice.TabIndex = 4;
            this.textBoxPrice.Text = "0.00";

            // labelPayment
            this.labelPayment.AutoSize = true;
            this.labelPayment.Location = new System.Drawing.Point(20, 120);
            this.labelPayment.Name = "labelPayment";
            this.labelPayment.Size = new System.Drawing.Size(94, 18);
            this.labelPayment.TabIndex = 5;
            this.labelPayment.Text = "Метод оплаты:";

            // textBoxPayment
            this.textBoxPayment.Location = new System.Drawing.Point(120, 117);
            this.textBoxPayment.Name = "textBoxPayment";
            this.textBoxPayment.ReadOnly = true;
            this.textBoxPayment.Size = new System.Drawing.Size(150, 24);
            this.textBoxPayment.TabIndex = 6;
            this.textBoxPayment.Text = "Наличные";

            // buttonAddNewPoint
            this.buttonAddNewPoint.BackColor = System.Drawing.Color.FromArgb(251, 228, 255);
            this.buttonAddNewPoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddNewPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonAddNewPoint.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonAddNewPoint.Location = new System.Drawing.Point(30, 430);
            this.buttonAddNewPoint.Name = "buttonAddNewPoint";
            this.buttonAddNewPoint.Size = new System.Drawing.Size(200, 35);
            this.buttonAddNewPoint.TabIndex = 5;
            this.buttonAddNewPoint.Text = "Новый адрес";
            this.buttonAddNewPoint.UseVisualStyleBackColor = false;
            this.buttonAddNewPoint.Click += new System.EventHandler(this.buttonAddNewPoint_Click);

            // panelNewAddress
            this.panelNewAddress.BackColor = System.Drawing.Color.FromArgb(245, 245, 250);
            this.panelNewAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNewAddress.Controls.Add(this.labelCity);
            this.panelNewAddress.Controls.Add(this.textBoxCity);
            this.panelNewAddress.Controls.Add(this.labelStreet);
            this.panelNewAddress.Controls.Add(this.textBoxStreet);
            this.panelNewAddress.Controls.Add(this.labelHouse);
            this.panelNewAddress.Controls.Add(this.textBoxHouse);
            this.panelNewAddress.Controls.Add(this.labelEntrance);
            this.panelNewAddress.Controls.Add(this.textBoxEntrance);
            this.panelNewAddress.Controls.Add(this.labelType);
            this.panelNewAddress.Controls.Add(this.textBoxType);
            this.panelNewAddress.Controls.Add(this.buttonSaveNewPoint);
            this.panelNewAddress.Controls.Add(this.buttonCancelNewPoint);
            this.panelNewAddress.Location = new System.Drawing.Point(30, 470);
            this.panelNewAddress.Name = "panelNewAddress";
            this.panelNewAddress.Size = new System.Drawing.Size(540, 200);
            this.panelNewAddress.TabIndex = 6;
            this.panelNewAddress.Visible = false;

            // labelCity
            this.labelCity.AutoSize = true;
            this.labelCity.Location = new System.Drawing.Point(20, 20);
            this.labelCity.Name = "labelCity";
            this.labelCity.Size = new System.Drawing.Size(47, 17);
            this.labelCity.TabIndex = 0;
            this.labelCity.Text = "Город:";

            // textBoxCity
            this.textBoxCity.Location = new System.Drawing.Point(100, 17);
            this.textBoxCity.Name = "textBoxCity";
            this.textBoxCity.Size = new System.Drawing.Size(180, 22);
            this.textBoxCity.TabIndex = 1;
            this.textBoxCity.Enter += new System.EventHandler(this.textBoxCity_Enter);
            this.textBoxCity.Leave += new System.EventHandler(this.textBoxCity_Leave);

            // labelStreet
            this.labelStreet.AutoSize = true;
            this.labelStreet.Location = new System.Drawing.Point(20, 50);
            this.labelStreet.Name = "labelStreet";
            this.labelStreet.Size = new System.Drawing.Size(48, 17);
            this.labelStreet.TabIndex = 2;
            this.labelStreet.Text = "Улица:";

            // textBoxStreet
            this.textBoxStreet.Location = new System.Drawing.Point(100, 47);
            this.textBoxStreet.Name = "textBoxStreet";
            this.textBoxStreet.Size = new System.Drawing.Size(180, 22);
            this.textBoxStreet.TabIndex = 3;
            this.textBoxStreet.Enter += new System.EventHandler(this.textBoxStreet_Enter);
            this.textBoxStreet.Leave += new System.EventHandler(this.textBoxStreet_Leave);

            // labelHouse
            this.labelHouse.AutoSize = true;
            this.labelHouse.Location = new System.Drawing.Point(20, 80);
            this.labelHouse.Name = "labelHouse";
            this.labelHouse.Size = new System.Drawing.Size(37, 17);
            this.labelHouse.TabIndex = 4;
            this.labelHouse.Text = "Дом:";

            // textBoxHouse
            this.textBoxHouse.Location = new System.Drawing.Point(100, 77);
            this.textBoxHouse.Name = "textBoxHouse";
            this.textBoxHouse.Size = new System.Drawing.Size(80, 22);
            this.textBoxHouse.TabIndex = 5;
            this.textBoxHouse.Enter += new System.EventHandler(this.textBoxHouse_Enter);
            this.textBoxHouse.Leave += new System.EventHandler(this.textBoxHouse_Leave);

            // labelEntrance
            this.labelEntrance.AutoSize = true;
            this.labelEntrance.Location = new System.Drawing.Point(200, 80);
            this.labelEntrance.Name = "labelEntrance";
            this.labelEntrance.Size = new System.Drawing.Size(63, 17);
            this.labelEntrance.TabIndex = 6;
            this.labelEntrance.Text = "Подъезд:";

            // textBoxEntrance
            this.textBoxEntrance.Location = new System.Drawing.Point(270, 77);
            this.textBoxEntrance.Name = "textBoxEntrance";
            this.textBoxEntrance.Size = new System.Drawing.Size(50, 22);
            this.textBoxEntrance.TabIndex = 7;
            this.textBoxEntrance.Enter += new System.EventHandler(this.textBoxEntrance_Enter);
            this.textBoxEntrance.Leave += new System.EventHandler(this.textBoxEntrance_Leave);

            // labelType
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(20, 110);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(50, 17);
            this.labelType.TabIndex = 8;
            this.labelType.Text = "Метка:";

            // textBoxType
            this.textBoxType.Location = new System.Drawing.Point(100, 107);
            this.textBoxType.Name = "textBoxType";
            this.textBoxType.Size = new System.Drawing.Size(150, 22);
            this.textBoxType.TabIndex = 9;
            this.textBoxType.Enter += new System.EventHandler(this.textBoxType_Enter);
            this.textBoxType.Leave += new System.EventHandler(this.textBoxType_Leave);

            // buttonSaveNewPoint
            this.buttonSaveNewPoint.BackColor = System.Drawing.Color.FromArgb(230, 227, 255);
            this.buttonSaveNewPoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveNewPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.buttonSaveNewPoint.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonSaveNewPoint.Location = new System.Drawing.Point(330, 150);
            this.buttonSaveNewPoint.Name = "buttonSaveNewPoint";
            this.buttonSaveNewPoint.Size = new System.Drawing.Size(90, 30);
            this.buttonSaveNewPoint.TabIndex = 10;
            this.buttonSaveNewPoint.Text = "Сохранить";
            this.buttonSaveNewPoint.UseVisualStyleBackColor = false;
            this.buttonSaveNewPoint.Click += new System.EventHandler(this.buttonSaveNewPoint_Click);

            // buttonCancelNewPoint
            this.buttonCancelNewPoint.BackColor = System.Drawing.Color.FromArgb(255, 227, 227);
            this.buttonCancelNewPoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancelNewPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.buttonCancelNewPoint.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonCancelNewPoint.Location = new System.Drawing.Point(430, 150);
            this.buttonCancelNewPoint.Name = "buttonCancelNewPoint";
            this.buttonCancelNewPoint.Size = new System.Drawing.Size(80, 30);
            this.buttonCancelNewPoint.TabIndex = 11;
            this.buttonCancelNewPoint.Text = "Отмена";
            this.buttonCancelNewPoint.UseVisualStyleBackColor = false;
            this.buttonCancelNewPoint.Click += new System.EventHandler(this.buttonCancelNewPoint_Click);

            // buttonOrder
            this.buttonOrder.BackColor = System.Drawing.Color.FromArgb(218, 52, 247);
            this.buttonOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.buttonOrder.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonOrder.Location = new System.Drawing.Point(290, 690);
            this.buttonOrder.Name = "buttonOrder";
            this.buttonOrder.Size = new System.Drawing.Size(150, 45);
            this.buttonOrder.TabIndex = 7;
            this.buttonOrder.Text = "Заказать";
            this.buttonOrder.UseVisualStyleBackColor = false;
            this.buttonOrder.Click += new System.EventHandler(this.buttonOrder_Click);

            // buttonBack
            this.buttonBack.BackColor = System.Drawing.Color.FromArgb(255, 227, 227);
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.buttonBack.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonBack.Location = new System.Drawing.Point(460, 690);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(100, 45);
            this.buttonBack.TabIndex = 8;
            this.buttonBack.Text = "Назад";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);

            // ClientOrderForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(600, 760);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonOrder);
            this.Controls.Add(this.panelNewAddress);
            this.Controls.Add(this.buttonAddNewPoint);
            this.Controls.Add(this.groupBoxDetails);
            this.Controls.Add(this.groupBoxRoute);
            this.Controls.Add(this.labelClientName);
            this.Controls.Add(this.labelClientInfo);
            this.Controls.Add(this.labelTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ClientOrderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Заказ такси";
            this.groupBoxRoute.ResumeLayout(false);
            this.groupBoxRoute.PerformLayout();
            this.groupBoxDetails.ResumeLayout(false);
            this.groupBoxDetails.PerformLayout();
            this.panelNewAddress.ResumeLayout(false);
            this.panelNewAddress.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelClientInfo;
        private System.Windows.Forms.Label labelClientName;
        private System.Windows.Forms.GroupBox groupBoxRoute;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.ComboBox cmbStartPoint;
        private System.Windows.Forms.Label labelEnd;
        private System.Windows.Forms.ComboBox cmbEndPoint;
        private System.Windows.Forms.GroupBox groupBoxDetails;
        private System.Windows.Forms.Label labelPromo;
        private System.Windows.Forms.ComboBox cmbPromotion;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.Label labelPayment;
        private System.Windows.Forms.TextBox textBoxPayment;
        private System.Windows.Forms.Button buttonAddNewPoint;
        private System.Windows.Forms.Panel panelNewAddress;
        private System.Windows.Forms.Label labelCity;
        private System.Windows.Forms.TextBox textBoxCity;
        private System.Windows.Forms.Label labelStreet;
        private System.Windows.Forms.TextBox textBoxStreet;
        private System.Windows.Forms.Label labelHouse;
        private System.Windows.Forms.TextBox textBoxHouse;
        private System.Windows.Forms.Label labelEntrance;
        private System.Windows.Forms.TextBox textBoxEntrance;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.TextBox textBoxType;
        private System.Windows.Forms.Button buttonSaveNewPoint;
        private System.Windows.Forms.Button buttonCancelNewPoint;
        private System.Windows.Forms.Button buttonOrder;
        private System.Windows.Forms.Button buttonBack;
    }
}