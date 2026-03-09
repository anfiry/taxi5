using System;
using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    partial class ClientPromotionForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridViewPromotions;
        private Button buttonBack;
        private Button buttonRefresh;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dataGridViewPromotions = new System.Windows.Forms.DataGridView();
            this.buttonBack = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPromotions)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewPromotions
            // 
            this.dataGridViewPromotions.AllowUserToAddRows = false;
            this.dataGridViewPromotions.AllowUserToDeleteRows = false;
            this.dataGridViewPromotions.AllowUserToResizeRows = false;
            this.dataGridViewPromotions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewPromotions.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewPromotions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewPromotions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPromotions.Location = new System.Drawing.Point(12, 70);
            this.dataGridViewPromotions.Name = "dataGridViewPromotions";
            this.dataGridViewPromotions.ReadOnly = true;
            this.dataGridViewPromotions.RowHeadersVisible = false;
            this.dataGridViewPromotions.RowHeadersWidth = 51;
            this.dataGridViewPromotions.Size = new System.Drawing.Size(1060, 500);
            this.dataGridViewPromotions.TabIndex = 0;
            this.dataGridViewPromotions.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewPromotions_CellDoubleClick);
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.buttonBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonBack.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonBack.Location = new System.Drawing.Point(980, 20);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(100, 40);
            this.buttonBack.TabIndex = 2;
            this.buttonBack.Text = "Назад";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(228)))), ((int)(((byte)(255)))));
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.buttonRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(0)))), ((int)(((byte)(66)))));
            this.buttonRefresh.Location = new System.Drawing.Point(836, 20);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(124, 40);
            this.buttonRefresh.TabIndex = 1;
            this.buttonRefresh.Text = "Обновить";
            this.buttonRefresh.UseVisualStyleBackColor = false;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // ClientPromotionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1084, 600);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.dataGridViewPromotions);
            this.MinimumSize = new System.Drawing.Size(1100, 640);
            this.Name = "ClientPromotionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Акции и скидки";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPromotions)).EndInit();
            this.ResumeLayout(false);

        }
    }
}