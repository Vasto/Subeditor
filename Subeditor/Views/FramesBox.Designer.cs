namespace Subeditor.Views
{
    partial class FramesBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHost = new System.Windows.Forms.Panel();
            this.txtNumeric = new System.Windows.Forms.TextBox();
            this.pnlHost.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHost
            // 
            this.pnlHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHost.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlHost.BackColor = System.Drawing.SystemColors.Window;
            this.pnlHost.Controls.Add(this.txtNumeric);
            this.pnlHost.Location = new System.Drawing.Point(1, 1);
            this.pnlHost.Name = "pnlHost";
            this.pnlHost.Size = new System.Drawing.Size(394, 15);
            this.pnlHost.TabIndex = 1;
            // 
            // txtNumeric
            // 
            this.txtNumeric.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumeric.BackColor = System.Drawing.SystemColors.Window;
            this.txtNumeric.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNumeric.Location = new System.Drawing.Point(3, 1);
            this.txtNumeric.MaxLength = 8;
            this.txtNumeric.Name = "txtNumeric";
            this.txtNumeric.Size = new System.Drawing.Size(388, 13);
            this.txtNumeric.TabIndex = 0;
            this.txtNumeric.Text = "0";
            this.txtNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNumeric.TextChanged += new System.EventHandler(this.txtNumericTextChangedHandler);
            this.txtNumeric.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumericKeyPressHandler);
            this.txtNumeric.Validating += new System.ComponentModel.CancelEventHandler(this.txtNumericValidatingHandler);
            // 
            // FramesBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.pnlHost);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "FramesBox";
            this.Size = new System.Drawing.Size(396, 17);
            this.EnabledChanged += new System.EventHandler(this.EnabledChangedHandler);
            this.pnlHost.ResumeLayout(false);
            this.pnlHost.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHost;
        private System.Windows.Forms.TextBox txtNumeric;
    }
}
