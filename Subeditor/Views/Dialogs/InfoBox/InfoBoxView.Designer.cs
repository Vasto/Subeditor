namespace Subeditor.Views.Dialogs.InfoBox
{
    partial class InfoBoxView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pbMessageImage = new System.Windows.Forms.PictureBox();
            this.viewsContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMessageImage)).BeginInit();
            this.SuspendLayout();
            // 
            // viewsContainer
            // 
            this.viewsContainer.Controls.Add(this.panel1);
            this.viewsContainer.Controls.Add(this.btnOk);
            this.viewsContainer.Size = new System.Drawing.Size(359, 117);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(142, 82);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOkClickHandler);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.pbMessageImage);
            this.panel1.Controls.Add(this.lblMessage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(359, 71);
            this.panel1.TabIndex = 3;
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMessage.AutoEllipsis = true;
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblMessage.Location = new System.Drawing.Point(66, 26);
            this.lblMessage.MaximumSize = new System.Drawing.Size(280, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(71, 21);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "Message";
            // 
            // pbMessageImage
            // 
            this.pbMessageImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbMessageImage.InitialImage = null;
            this.pbMessageImage.Location = new System.Drawing.Point(12, 11);
            this.pbMessageImage.Name = "pbMessageImage";
            this.pbMessageImage.Size = new System.Drawing.Size(48, 48);
            this.pbMessageImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMessageImage.TabIndex = 1;
            this.pbMessageImage.TabStop = false;
            // 
            // InfoBoxView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 117);
            this.Name = "InfoBoxView";
            this.Text = "Subeditor";
            this.viewsContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMessageImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.PictureBox pbMessageImage;
    }
}