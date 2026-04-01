namespace taxi4
{
    partial class ClientOrderForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelClientInfo = new System.Windows.Forms.Label();
            this.labelClientName = new System.Windows.Forms.Label();
            this.groupBoxRoute = new System.Windows.Forms.GroupBox();
            this.btnSelectEndPoint = new System.Windows.Forms.Button();
            this.btnSelectStartPoint = new System.Windows.Forms.Button();
            this.cmbEndAddress = new System.Windows.Forms.ComboBox();
            this.cmbStartAddress = new System.Windows.Forms.ComboBox();
            this.labelEnd = new System.Windows.Forms.Label();
            this.labelStart = new System.Windows.Forms.Label();
            this.groupBoxDetails = new System.Windows.Forms.GroupBox();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.textBoxPayment = new System.Windows.Forms.TextBox();
            this.labelPayment = new System.Windows.Forms.Label();
            this.textBoxPrice = new System.Windows.Forms.TextBox();
            this.labelPrice = new System.Windows.Forms.Label();
            this.textBoxDistance = new System.Windows.Forms.TextBox();
            this.labelDistance = new System.Windows.Forms.Label();
            this.cmbTariff = new System.Windows.Forms.ComboBox();
            this.labelTariff = new System.Windows.Forms.Label();
            this.cmbPromotion = new System.Windows.Forms.ComboBox();
            this.labelPromo = new System.Windows.Forms.Label();
            this.buttonOrder = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.groupBoxRoute.SuspendLayout();
            this.groupBoxDetails.SuspendLayout();
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
            this.groupBoxRoute.Controls.Add(this.btnSelectEndPoint);
            this.groupBoxRoute.Controls.Add(this.btnSelectStartPoint);
            this.groupBoxRoute.Controls.Add(this.cmbEndAddress);
            this.groupBoxRoute.Controls.Add(this.cmbStartAddress);
            this.groupBoxRoute.Controls.Add(this.labelEnd);
            this.groupBoxRoute.Controls.Add(this.labelStart);
            this.groupBoxRoute.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBoxRoute.Location = new System.Drawing.Point(30, 100);
            this.groupBoxRoute.Name = "groupBoxRoute";
            this.groupBoxRoute.Size = new System.Drawing.Size(540, 130);
            this.groupBoxRoute.TabIndex = 3;
            this.groupBoxRoute.TabStop = false;
            this.groupBoxRoute.Text = "Маршрут";

            // btnSelectEndPoint
            this.btnSelectEndPoint.Location = new System.Drawing.Point(490, 75);
            this.btnSelectEndPoint.Name = "btnSelectEndPoint";
            this.btnSelectEndPoint.Size = new System.Drawing.Size(30, 26);
            this.btnSelectEndPoint.TabIndex = 5;
            this.btnSelectEndPoint.Text = "⭐";
            this.btnSelectEndPoint.UseVisualStyleBackColor = true;
            this.btnSelectEndPoint.Click += new System.EventHandler(this.btnSelectEndPoint_Click);

            // btnSelectStartPoint
            this.btnSelectStartPoint.Location = new System.Drawing.Point(490, 35);
            this.btnSelectStartPoint.Name = "btnSelectStartPoint";
            this.btnSelectStartPoint.Size = new System.Drawing.Size(30, 26);
            this.btnSelectStartPoint.TabIndex = 4;
            this.btnSelectStartPoint.Text = "⭐";
            this.btnSelectStartPoint.UseVisualStyleBackColor = true;
            this.btnSelectStartPoint.Click += new System.EventHandler(this.btnSelectStartPoint_Click);

            // cmbEndAddress
            this.cmbEndAddress.FormattingEnabled = true;
            this.cmbEndAddress.Location = new System.Drawing.Point(130, 77);
            this.cmbEndAddress.Name = "cmbEndAddress";
            this.cmbEndAddress.Size = new System.Drawing.Size(350, 26);
            this.cmbEndAddress.TabIndex = 3;

            // cmbStartAddress
            this.cmbStartAddress.FormattingEnabled = true;
            this.cmbStartAddress.Location = new System.Drawing.Point(130, 37);
            this.cmbStartAddress.Name = "cmbStartAddress";
            this.cmbStartAddress.Size = new System.Drawing.Size(350, 26);
            this.cmbStartAddress.TabIndex = 2;

            // labelEnd
            this.labelEnd.AutoSize = true;
            this.labelEnd.Location = new System.Drawing.Point(20, 80);
            this.labelEnd.Name = "labelEnd";
            this.labelEnd.Size = new System.Drawing.Size(86, 18);
            this.labelEnd.TabIndex = 1;
            this.labelEnd.Text = "Назначение:";

            // labelStart
            this.labelStart.AutoSize = true;
            this.labelStart.Location = new System.Drawing.Point(20, 40);
            this.labelStart.Name = "labelStart";
            this.labelStart.Size = new System.Drawing.Size(87, 18);
            this.labelStart.TabIndex = 0;
            this.labelStart.Text = "Отправление:";

            // groupBoxDetails
            this.groupBoxDetails.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBoxDetails.Controls.Add(this.buttonCalculate);
            this.groupBoxDetails.Controls.Add(this.textBoxPayment);
            this.groupBoxDetails.Controls.Add(this.labelPayment);
            this.groupBoxDetails.Controls.Add(this.textBoxPrice);
            this.groupBoxDetails.Controls.Add(this.labelPrice);
            this.groupBoxDetails.Controls.Add(this.textBoxDistance);
            this.groupBoxDetails.Controls.Add(this.labelDistance);
            this.groupBoxDetails.Controls.Add(this.cmbTariff);
            this.groupBoxDetails.Controls.Add(this.labelTariff);
            this.groupBoxDetails.Controls.Add(this.cmbPromotion);
            this.groupBoxDetails.Controls.Add(this.labelPromo);
            this.groupBoxDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.groupBoxDetails.Location = new System.Drawing.Point(30, 250);
            this.groupBoxDetails.Name = "groupBoxDetails";
            this.groupBoxDetails.Size = new System.Drawing.Size(540, 220);
            this.groupBoxDetails.TabIndex = 4;
            this.groupBoxDetails.TabStop = false;
            this.groupBoxDetails.Text = "Детали заказа";

            // buttonCalculate
            this.buttonCalculate.BackColor = System.Drawing.Color.FromArgb(230, 227, 255);
            this.buttonCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonCalculate.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonCalculate.Location = new System.Drawing.Point(400, 180);
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.Size = new System.Drawing.Size(120, 30);
            this.buttonCalculate.TabIndex = 10;
            this.buttonCalculate.Text = "Рассчитать";
            this.buttonCalculate.UseVisualStyleBackColor = false;
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);

            // textBoxPayment
            this.textBoxPayment.Location = new System.Drawing.Point(120, 147);
            this.textBoxPayment.Name = "textBoxPayment";
            this.textBoxPayment.ReadOnly = true;
            this.textBoxPayment.Size = new System.Drawing.Size(150, 24);
            this.textBoxPayment.TabIndex = 9;
            this.textBoxPayment.Text = "Наличные";

            // labelPayment
            this.labelPayment.AutoSize = true;
            this.labelPayment.Location = new System.Drawing.Point(20, 150);
            this.labelPayment.Name = "labelPayment";
            this.labelPayment.Size = new System.Drawing.Size(94, 18);
            this.labelPayment.TabIndex = 8;
            this.labelPayment.Text = "Метод оплаты:";

            // textBoxPrice
            this.textBoxPrice.Location = new System.Drawing.Point(120, 107);
            this.textBoxPrice.Name = "textBoxPrice";
            this.textBoxPrice.ReadOnly = true;
            this.textBoxPrice.Size = new System.Drawing.Size(150, 24);
            this.textBoxPrice.TabIndex = 7;
            this.textBoxPrice.Text = "0.00";

            // labelPrice
            this.labelPrice.AutoSize = true;
            this.labelPrice.Location = new System.Drawing.Point(20, 110);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(85, 18);
            this.labelPrice.TabIndex = 6;
            this.labelPrice.Text = "Стоимость:";

            // textBoxDistance
            this.textBoxDistance.Location = new System.Drawing.Point(120, 67);
            this.textBoxDistance.Name = "textBoxDistance";
            this.textBoxDistance.Size = new System.Drawing.Size(150, 24);
            this.textBoxDistance.TabIndex = 5;
            this.textBoxDistance.Text = "5.0";

            // labelDistance
            this.labelDistance.AutoSize = true;
            this.labelDistance.Location = new System.Drawing.Point(20, 70);
            this.labelDistance.Name = "labelDistance";
            this.labelDistance.Size = new System.Drawing.Size(77, 18);
            this.labelDistance.TabIndex = 4;
            this.labelDistance.Text = "Расстояние:";

            // cmbTariff
            this.cmbTariff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTariff.FormattingEnabled = true;
            this.cmbTariff.Location = new System.Drawing.Point(120, 27);
            this.cmbTariff.Name = "cmbTariff";
            this.cmbTariff.Size = new System.Drawing.Size(200, 26);
            this.cmbTariff.TabIndex = 3;

            // labelTariff
            this.labelTariff.AutoSize = true;
            this.labelTariff.Location = new System.Drawing.Point(20, 30);
            this.labelTariff.Name = "labelTariff";
            this.labelTariff.Size = new System.Drawing.Size(53, 18);
            this.labelTariff.TabIndex = 2;
            this.labelTariff.Text = "Тариф:";

            // cmbPromotion
            this.cmbPromotion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPromotion.FormattingEnabled = true;
            this.cmbPromotion.Location = new System.Drawing.Point(360, 27);
            this.cmbPromotion.Name = "cmbPromotion";
            this.cmbPromotion.Size = new System.Drawing.Size(160, 26);
            this.cmbPromotion.TabIndex = 1;

            // labelPromo
            this.labelPromo.AutoSize = true;
            this.labelPromo.Location = new System.Drawing.Point(300, 30);
            this.labelPromo.Name = "labelPromo";
            this.labelPromo.Size = new System.Drawing.Size(53, 18);
            this.labelPromo.TabIndex = 0;
            this.labelPromo.Text = "Акция:";

            // buttonOrder
            this.buttonOrder.BackColor = System.Drawing.Color.FromArgb(218, 52, 247);
            this.buttonOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.buttonOrder.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.buttonOrder.Location = new System.Drawing.Point(550, 490);
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
            this.buttonBack.Location = new System.Drawing.Point(720, 490);
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
            this.ClientSize = new System.Drawing.Size(1035, 564);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonOrder);
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
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelClientInfo;
        private System.Windows.Forms.Label labelClientName;
        private System.Windows.Forms.GroupBox groupBoxRoute;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.ComboBox cmbStartAddress;
        private System.Windows.Forms.Label labelEnd;
        private System.Windows.Forms.ComboBox cmbEndAddress;
        private System.Windows.Forms.Button btnSelectStartPoint;
        private System.Windows.Forms.Button btnSelectEndPoint;
        private System.Windows.Forms.GroupBox groupBoxDetails;
        private System.Windows.Forms.Label labelPromo;
        private System.Windows.Forms.ComboBox cmbPromotion;
        private System.Windows.Forms.Label labelTariff;
        private System.Windows.Forms.ComboBox cmbTariff;
        private System.Windows.Forms.Label labelDistance;
        private System.Windows.Forms.TextBox textBoxDistance;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.TextBox textBoxPrice;
        private System.Windows.Forms.Label labelPayment;
        private System.Windows.Forms.TextBox textBoxPayment;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.Button buttonOrder;
        private System.Windows.Forms.Button buttonBack;
    }
}