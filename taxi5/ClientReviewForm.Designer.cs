using System.Drawing;
using System.Windows.Forms;

namespace taxi4
{
    partial class ClientReviewForm
    {
        private System.ComponentModel.IContainer components = null;
        private NumericUpDown numericRating;
        private TextBox txtComment;
        private Button btnSave;
        private Button btnCancel;
        private Label lblRating;
        private Label lblComment;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.numericRating = new NumericUpDown();
            this.txtComment = new TextBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.lblRating = new Label();
            this.lblComment = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericRating)).BeginInit();
            this.SuspendLayout();

            // numericRating
            this.numericRating.Location = new Point(120, 20);
            this.numericRating.Minimum = 1;
            this.numericRating.Maximum = 5;
            this.numericRating.Value = 5;
            this.numericRating.Size = new Size(60, 22);

            // lblRating
            this.lblRating.AutoSize = true;
            this.lblRating.Location = new Point(20, 22);
            this.lblRating.Text = "Оценка:";

            // txtComment
            this.txtComment.Location = new Point(120, 60);
            this.txtComment.Multiline = true;
            this.txtComment.Size = new Size(300, 100);
            this.txtComment.MaxLength = 500;

            // lblComment
            this.lblComment.AutoSize = true;
            this.lblComment.Location = new Point(20, 63);
            this.lblComment.Text = "Комментарий:";

            // btnSave
            this.btnSave.Location = new Point(180, 180);
            this.btnSave.Size = new Size(100, 30);
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.Location = new Point(300, 180);
            this.btnCancel.Size = new Size(100, 30);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // ReviewForm
            this.ClientSize = new Size(450, 230);
            this.Controls.Add(this.numericRating);
            this.Controls.Add(this.lblRating);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Оставить отзыв";
            this.StartPosition = FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.numericRating)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}