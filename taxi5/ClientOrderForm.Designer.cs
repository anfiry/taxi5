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
            this.labelTariff = new System.Windows.Forms.Label();
            this.cmbTariff = new System.Windows.Forms.ComboBox();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.labelPrice = new System.Windows.Forms.Label();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.labelDistance = new System.Windows.Forms.Label();
            this.textBoxDistance = new System.Windows.Forms.TextBox();
            this.labelPayment = new System.Windows.Forms.Label();
            this.textBoxPayment = new System.Windows.Forms.TextBox();
            this.groupBoxNewAddress = new System.Windows.Forms.GroupBox();
            this.labelPointName = new System.Windows.Forms.Label();
            this.txtPointName = new System.Windows.Forms.TextBox();
            this.labelCity = new System.Windows.Forms.Label();
            this.textBoxCity = new System.Windows.Forms.TextBox();
            this.labelStreet = new System.Windows.Forms.Label();
            this.textBoxStreet = new System.Windows.Forms.TextBox();
            this.labelHouse = new System.Windows.Forms.Label();
            this.textBoxHouse = new System.Windows.Forms.TextBox();
            this.labelEntrance = new System.Windows.Forms.Label();
            this.textBoxEntrance = new System.Windows.Forms.TextBox();
            this.labelType = new System.Windows.Forms.Label();
            this.cmbPointType = new System.Windows.Forms.ComboBox();
            this.buttonSaveNewPoint = new System.Windows.Forms.Button();
            this.buttonCancelNewPoint = new System.Windows.Forms.Button();
            this.buttonOrder = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.groupBoxRoute.SuspendLayout();
            this.groupBoxDetails.SuspendLayout();
            this.groupBoxNewAddress.SuspendLayout();
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
            this.groupBoxRoute.Size = new System.Drawing.Size(500, 130);
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
            this.cmbStartPoint.Size = new System.Drawing.Size(360, 26);
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
            this.cmbEndPoint.Size = new System.Drawing.Size(360, 26);
            this.cmbEndPoint.TabIndex = 3;

            // groupBoxDetails
            this.groupBoxDetails.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBoxDetails.Controls.Add(this.labelPromo);
            this.groupBoxDetails.Controls.Add(this.cmbPromotion);
            this.groupBoxDetails.Controls.Add(this.labelTariff);
            this.groupBoxDetails.Controls.Add(this.cmbTariff);
            this.groupBoxDetails.Controls.Add(this.buttonCalculate);
            this.groupBoxDetails.Controls.Add(this.labelPrice);
            this.groupBoxDetails.Controls.Add(this.textBoxPrice);
            this.groupBoxDetails.Controls.Add(this.labelDistance);
            this.groupBoxDetails.Controls.Add(this.textBoxDistance);
            this.groupBoxDetails.Controls.Add(this.labelPayment);
            this.groupBoxDetails.Controls.Add(this.textBoxPayment);
            this.groupBoxDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBoxDetails.Location = new System.Drawing.Point(30, 240);
            this.groupBoxDetails.Name = "groupBoxDetails";
            this.groupBoxDetails.Size = new System.Drawing.Size(500, 200);
            this.groupBoxDetails.TabIndex = 4;
            this.groupBoxDetails.TabStop = false;
            this.groupBoxDetails.Text = "Детали заказа";

            // labelPromo
            this.labelPromo.AutoSize = true;
            this.labelPromo.Location = new System.Drawing.Point(20, 30);
            this.labelPromo.Name = "labelPromo";
            this.labelPromo.Size = new System.Drawing.Size(59, 18);
            this.labelPromo.TabIndex = 0;
            this.labelPromo.Text = "Акция:";

            // cmbPromotion
            this.cmbPromotion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPromotion.FormattingEnabled = true;
            this.cmbPromotion.Location = new System.Drawing.Point(120, 27);
            this.cmbPromotion.Name = "cmbPromotion";
            this.cmbPromotion.Size = new System.Drawing.Size(250, 26);
            this.cmbPromotion.TabIndex = 1;

            // labelTariff
            this.labelTariff.AutoSize = true;
            this.labelTariff.Location = new System.Drawing.Point(20, 70);
            this.labelTariff.Name = "labelTariff";
            this.labelTariff.Size = new System.Drawing.Size(53, 18);
            this.labelTariff.TabIndex = 2;
            this.labelTariff.Text = "Тариф:";

            // cmbTariff
            this.cmbTariff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTariff.FormattingEnabled = true;
            this.cmbTariff.Location = new System.Drawing.Point(120, 67);
            this.cmbTariff.Name = "cmbTariff";
            this.cmbTariff.Size = new System.Drawing.Size(250, 26);
            this.cmbTariff.TabIndex = 3;

            // buttonCalculate
            this.buttonCalculate.BackColor = System.Drawing.Color.FromArgb(230, 227, 255);
            this.buttonCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.buttonCalculate.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonCalculate.Location = new System.Drawing.Point(380, 60);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(100, 30);
            this.buttonCalculate.TabIndex = 4;
            this.buttonCalculate.Text = "Рассчитать";
            this.buttonCalculate.UseVisualStyleBackColor = false;
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);

            // labelPrice
            this.labelPrice.AutoSize = true;
            this.labelPrice.Location = new System.Drawing.Point(20, 110);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(85, 18);
            this.labelPrice.TabIndex = 5;
            this.labelPrice.Text = "Стоимость:";

            // textBoxPrice
            this.textBoxPrice.Location = new System.Drawing.Point(120, 107);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.ReadOnly = true;
            this.textBoxPrice.Size = new System.Drawing.Size(150, 24);
            this.textBoxPrice.TabIndex = 6;
            this.textBoxPrice.Text = "0.00";

            // labelDistance
            this.labelDistance.AutoSize = true;
            this.labelDistance.Location = new System.Drawing.Point(280, 110);
            this.labelDistance.Name = "labelDistance";
            this.labelDistance.Size = new System.Drawing.Size(77, 18);
            this.labelDistance.TabIndex = 7;
            this.labelDistance.Text = "Расстояние:";

            // textBoxDistance
            this.textBoxDistance.Location = new System.Drawing.Point(360, 107);
            this.textBoxDistance.Name = "textBoxDistance";
            this.textBoxDistance.Size = new System.Drawing.Size(120, 24);
            this.textBoxDistance.TabIndex = 8;
            this.textBoxDistance.Text = "5.0";

            // labelPayment
            this.labelPayment.AutoSize = true;
            this.labelPayment.Location = new System.Drawing.Point(20, 150);
            this.labelPayment.Name = "labelPayment";
            this.labelPayment.Size = new System.Drawing.Size(94, 18);
            this.labelPayment.TabIndex = 9;
            this.labelPayment.Text = "Метод оплаты:";

            // textBoxPayment
            this.textBoxPayment.Location = new System.Drawing.Point(120, 147);
            this.textBoxPayment.Name = "textBoxPayment";
            this.textBoxPayment.ReadOnly = true;
            this.textBoxPayment.Size = new System.Drawing.Size(150, 24);
            this.textBoxPayment.TabIndex = 10;
            this.textBoxPayment.Text = "Наличные";

            // groupBoxNewAddress
            this.groupBoxNewAddress.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBoxNewAddress.Controls.Add(this.labelPointName);
            this.groupBoxNewAddress.Controls.Add(this.txtPointName);
            this.groupBoxNewAddress.Controls.Add(this.labelCity);
            this.groupBoxNewAddress.Controls.Add(this.textBoxCity);
            this.groupBoxNewAddress.Controls.Add(this.labelStreet);
            this.groupBoxNewAddress.Controls.Add(this.textBoxStreet);
            this.groupBoxNewAddress.Controls.Add(this.labelHouse);
            this.groupBoxNewAddress.Controls.Add(this.textBoxHouse);
            this.groupBoxNewAddress.Controls.Add(this.labelEntrance);
            this.groupBoxNewAddress.Controls.Add(this.textBoxEntrance);
            this.groupBoxNewAddress.Controls.Add(this.labelType);
            this.groupBoxNewAddress.Controls.Add(this.cmbPointType);
            this.groupBoxNewAddress.Controls.Add(this.buttonSaveNewPoint);
            this.groupBoxNewAddress.Controls.Add(this.buttonCancelNewPoint);
            this.groupBoxNewAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBoxNewAddress.Location = new System.Drawing.Point(550, 100);
            this.groupBoxNewAddress.Name = "groupBoxNewAddress";
            this.groupBoxNewAddress.Size = new System.Drawing.Size(360, 340);
            this.groupBoxNewAddress.TabIndex = 9;
            this.groupBoxNewAddress.TabStop = false;
            this.groupBoxNewAddress.Text = "Добавить новый адрес";

            // labelPointName
            this.labelPointName.AutoSize = true;
            this.labelPointName.Location = new System.Drawing.Point(20, 30);
            this.labelPointName.Name = "labelPointName";
            this.labelPointName.Size = new System.Drawing.Size(58, 18);
            this.labelPointName.TabIndex = 0;
            this.labelPointName.Text = "Название:";

            // txtPointName
            this.txtPointName.Location = new System.Drawing.Point(120, 27);
            this.txtPointName.Name = "txtPointName";
            this.txtPointName.Size = new System.Drawing.Size(220, 24);
            this.txtPointName.TabIndex = 1;
            this.txtPointName.Enter += new System.EventHandler(this.txtPointName_Enter);
            this.txtPointName.Leave += new System.EventHandler(this.txtPointName_Leave);

            // labelCity
            this.labelCity.AutoSize = true;
            this.labelCity.Location = new System.Drawing.Point(20, 70);
            this.labelCity.Name = "labelCity";
            this.labelCity.Size = new System.Drawing.Size(50, 18);
            this.labelCity.TabIndex = 2;
            this.labelCity.Text = "Город:";

            // textBoxCity
            this.textBoxCity.Location = new System.Drawing.Point(120, 67);
            this.textBoxCity.Name = "textBoxCity";
            this.textBoxCity.Size = new System.Drawing.Size(220, 24);
            this.textBoxCity.TabIndex = 3;
            this.textBoxCity.Enter += new System.EventHandler(this.textBoxCity_Enter);
            this.textBoxCity.Leave += new System.EventHandler(this.textBoxCity_Leave);

            // labelStreet
            this.labelStreet.AutoSize = true;
            this.labelStreet.Location = new System.Drawing.Point(20, 110);
            this.labelStreet.Name = "labelStreet";
            this.labelStreet.Size = new System.Drawing.Size(49, 18);
            this.labelStreet.TabIndex = 4;
            this.labelStreet.Text = "Улица:";

            // textBoxStreet
            this.textBoxStreet.Location = new System.Drawing.Point(120, 107);
            this.textBoxStreet.Name = "textBoxStreet";
            this.textBoxStreet.Size = new System.Drawing.Size(220, 24);
            this.textBoxStreet.TabIndex = 5;
            this.textBoxStreet.Enter += new System.EventHandler(this.textBoxStreet_Enter);
            this.textBoxStreet.Leave += new System.EventHandler(this.textBoxStreet_Leave);

            // labelHouse
            this.labelHouse.AutoSize = true;
            this.labelHouse.Location = new System.Drawing.Point(20, 150);
            this.labelHouse.Name = "labelHouse";
            this.labelHouse.Size = new System.Drawing.Size(42, 18);
            this.labelHouse.TabIndex = 6;
            this.labelHouse.Text = "Дом:";

            // textBoxHouse
            this.textBoxHouse.Location = new System.Drawing.Point(120, 147);
            this.textBoxHouse.Name = "textBoxHouse";
            this.textBoxHouse.Size = new System.Drawing.Size(100, 24);
            this.textBoxHouse.TabIndex = 7;
            this.textBoxHouse.Enter += new System.EventHandler(this.textBoxHouse_Enter);
            this.textBoxHouse.Leave += new System.EventHandler(this.textBoxHouse_Leave);

            // labelEntrance
            this.labelEntrance.AutoSize = true;
            this.labelEntrance.Location = new System.Drawing.Point(230, 150);
            this.labelEntrance.Name = "labelEntrance";
            this.labelEntrance.Size = new System.Drawing.Size(62, 18);
            this.labelEntrance.TabIndex = 8;
            this.labelEntrance.Text = "Подъезд:";

            // textBoxEntrance
            this.textBoxEntrance.Location = new System.Drawing.Point(290, 147);
            this.textBoxEntrance.Name = "textBoxEntrance";
            this.textBoxEntrance.Size = new System.Drawing.Size(50, 24);
            this.textBoxEntrance.TabIndex = 9;
            this.textBoxEntrance.Enter += new System.EventHandler(this.textBoxEntrance_Enter);
            this.textBoxEntrance.Leave += new System.EventHandler(this.textBoxEntrance_Leave);

            // labelType
            this.labelType.AutoSize = true;
            this.labelType.Location = new System.Drawing.Point(20, 190);
            this.labelType.Name = "labelType";
            this.labelType.Size = new System.Drawing.Size(85, 18);
            this.labelType.TabIndex = 10;
            this.labelType.Text = "Тип точки:";

            // cmbPointType
            this.cmbPointType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPointType.FormattingEnabled = true;
            this.cmbPointType.Location = new System.Drawing.Point(120, 187);
            this.cmbPointType.Name = "cmbPointType";
            this.cmbPointType.Size = new System.Drawing.Size(220, 26);
            this.cmbPointType.TabIndex = 11;

            // buttonSaveNewPoint
            this.buttonSaveNewPoint.BackColor = System.Drawing.Color.FromArgb(230, 227, 255);
            this.buttonSaveNewPoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSaveNewPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonSaveNewPoint.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonSaveNewPoint.Location = new System.Drawing.Point(120, 230);
            this.buttonSaveNewPoint.Name = "buttonSaveNewPoint";
            this.buttonSaveNewPoint.Size = new System.Drawing.Size(100, 35);
            this.buttonSaveNewPoint.TabIndex = 12;
            this.buttonSaveNewPoint.Text = "Сохранить";
            this.buttonSaveNewPoint.UseVisualStyleBackColor = false;
            this.buttonSaveNewPoint.Click += new System.EventHandler(this.buttonSaveNewPoint_Click);

            // buttonCancelNewPoint
            this.buttonCancelNewPoint.BackColor = System.Drawing.Color.FromArgb(255, 227, 227);
            this.buttonCancelNewPoint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancelNewPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonCancelNewPoint.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonCancelNewPoint.Location = new System.Drawing.Point(240, 230);
            this.buttonCancelNewPoint.Name = "buttonCancelNewPoint";
            this.buttonCancelNewPoint.Size = new System.Drawing.Size(100, 35);
            this.buttonCancelNewPoint.TabIndex = 13;
            this.buttonCancelNewPoint.Text = "Очистить";
            this.buttonCancelNewPoint.UseVisualStyleBackColor = false;
            this.buttonCancelNewPoint.Click += new System.EventHandler(this.buttonCancelNewPoint_Click);

            // buttonOrder
            this.buttonOrder.BackColor = System.Drawing.Color.FromArgb(218, 52, 247);
            this.buttonOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.buttonOrder.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonOrder.Location = new System.Drawing.Point(670, 460);
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
            this.buttonBack.Location = new System.Drawing.Point(830, 460);
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
            this.ClientSize = new System.Drawing.Size(950, 540);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonOrder);
            this.Controls.Add(this.groupBoxNewAddress);
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
            this.groupBoxNewAddress.ResumeLayout(false);
            this.groupBoxNewAddress.PerformLayout();
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
        private System.Windows.Forms.Label labelTariff;
        private System.Windows.Forms.ComboBox cmbTariff;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.Label labelDistance;
        private System.Windows.Forms.TextBox textBoxDistance;
        private System.Windows.Forms.Label labelPayment;
        private System.Windows.Forms.TextBox textBoxPayment;
        private System.Windows.Forms.GroupBox groupBoxNewAddress;
        private System.Windows.Forms.Label labelPointName;
        private System.Windows.Forms.TextBox txtPointName;
        private System.Windows.Forms.Label labelCity;
        private System.Windows.Forms.TextBox textBoxCity;
        private System.Windows.Forms.Label labelStreet;
        private System.Windows.Forms.TextBox textBoxStreet;
        private System.Windows.Forms.Label labelHouse;
        private System.Windows.Forms.TextBox textBoxHouse;
        private System.Windows.Forms.Label labelEntrance;
        private System.Windows.Forms.TextBox textBoxEntrance;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.ComboBox cmbPointType;
        private System.Windows.Forms.Button buttonSaveNewPoint;
        private System.Windows.Forms.Button buttonCancelNewPoint;
        private System.Windows.Forms.Button buttonOrder;
        private System.Windows.Forms.Button buttonBack;
    }
}