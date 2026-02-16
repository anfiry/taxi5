namespace taxi4
{
    partial class ClientMenu
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

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnMyAddresses = new System.Windows.Forms.Button();
            this.btnPromotions = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnTrackOrder = new System.Windows.Forms.Button();
            this.btnNewOrder = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();

            // panel1
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.btnMyAddresses);
            this.panel1.Controls.Add(this.btnPromotions);
            this.panel1.Controls.Add(this.btnHistory);
            this.panel1.Controls.Add(this.btnTrackOrder);
            this.panel1.Controls.Add(this.btnNewOrder);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Noto Sans Georgian", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1072, 608);
            this.panel1.TabIndex = 0;

            // btnBack
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(230, 227, 255);
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(227, 255, 233);
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(251, 228, 255);
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBack.Location = new System.Drawing.Point(445, 470);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(182, 67);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "Назад";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);

            // btnMyAddresses
            this.btnMyAddresses.BackColor = System.Drawing.Color.FromArgb(251, 228, 255);
            this.btnMyAddresses.FlatAppearance.BorderSize = 0;
            this.btnMyAddresses.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(227, 255, 233);
            this.btnMyAddresses.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(230, 227, 255);
            this.btnMyAddresses.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMyAddresses.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMyAddresses.Location = new System.Drawing.Point(425, 307);
            this.btnMyAddresses.Name = "btnMyAddresses";
            this.btnMyAddresses.Size = new System.Drawing.Size(222, 67);
            this.btnMyAddresses.TabIndex = 5;
            this.btnMyAddresses.Text = "Мои адреса";
            this.btnMyAddresses.UseVisualStyleBackColor = false;
            this.btnMyAddresses.Click += new System.EventHandler(this.btnMyAddresses_Click);

            // btnPromotions
            this.btnPromotions.BackColor = System.Drawing.Color.FromArgb(251, 228, 255);
            this.btnPromotions.FlatAppearance.BorderSize = 0;
            this.btnPromotions.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(227, 255, 233);
            this.btnPromotions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(230, 227, 255);
            this.btnPromotions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPromotions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPromotions.Location = new System.Drawing.Point(87, 307);
            this.btnPromotions.Name = "btnPromotions";
            this.btnPromotions.Size = new System.Drawing.Size(222, 67);
            this.btnPromotions.TabIndex = 4;
            this.btnPromotions.Text = "Промоакции и скидки";
            this.btnPromotions.UseVisualStyleBackColor = false;
            this.btnPromotions.Click += new System.EventHandler(this.btnPromotions_Click);

            // btnHistory
            this.btnHistory.BackColor = System.Drawing.Color.FromArgb(251, 228, 255);
            this.btnHistory.FlatAppearance.BorderSize = 0;
            this.btnHistory.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(227, 255, 233);
            this.btnHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(230, 227, 255);
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnHistory.Location = new System.Drawing.Point(425, 138);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(222, 67);
            this.btnHistory.TabIndex = 2;
            this.btnHistory.Text = "История поездок";
            this.btnHistory.UseVisualStyleBackColor = false;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);

            // btnTrackOrder
            this.btnTrackOrder.BackColor = System.Drawing.Color.FromArgb(251, 228, 255);
            this.btnTrackOrder.FlatAppearance.BorderSize = 0;
            this.btnTrackOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(227, 255, 233);
            this.btnTrackOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(230, 227, 255);
            this.btnTrackOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrackOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnTrackOrder.Location = new System.Drawing.Point(763, 138);
            this.btnTrackOrder.Name = "btnTrackOrder";
            this.btnTrackOrder.Size = new System.Drawing.Size(222, 67);
            this.btnTrackOrder.TabIndex = 1;
            this.btnTrackOrder.Text = "Отслеживание поездки";
            this.btnTrackOrder.UseVisualStyleBackColor = false;
            this.btnTrackOrder.Click += new System.EventHandler(this.btnTrackOrder_Click);

            // btnNewOrder
            this.btnNewOrder.BackColor = System.Drawing.Color.FromArgb(251, 228, 255);
            this.btnNewOrder.FlatAppearance.BorderSize = 0;
            this.btnNewOrder.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(227, 255, 233);
            this.btnNewOrder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(230, 227, 255);
            this.btnNewOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNewOrder.Location = new System.Drawing.Point(87, 138);
            this.btnNewOrder.Name = "btnNewOrder";
            this.btnNewOrder.Size = new System.Drawing.Size(222, 67);
            this.btnNewOrder.TabIndex = 0;
            this.btnNewOrder.Text = "Заказ такси";
            this.btnNewOrder.UseVisualStyleBackColor = false;
            this.btnNewOrder.Click += new System.EventHandler(this.btnNewOrder_Click);

            // panel2
            this.panel2.BackColor = System.Drawing.Color.FromArgb(218, 52, 247);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1072, 92);
            this.panel2.TabIndex = 0;

            // label1
            this.label1.Font = new System.Drawing.Font("Noto Sans Georgian Bold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(4, 0, 66);
            this.label1.Location = new System.Drawing.Point(335, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(427, 62);
            this.label1.TabIndex = 0;
            this.label1.Text = "Меню клиента";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // ClientMenu
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 608);
            this.Controls.Add(this.panel1);
            this.Name = "ClientMenu";
            this.Text = "Клиент";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnNewOrder;
        private System.Windows.Forms.Button btnTrackOrder;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.Button btnPromotions;
        private System.Windows.Forms.Button btnMyAddresses;
        private System.Windows.Forms.Button btnBack;
    }
}