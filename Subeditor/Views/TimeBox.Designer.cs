namespace Subeditor.Views
{
    partial class TimeBox
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
            this.pnlHost = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHours = new System.Windows.Forms.TextBox();
            this.txtMinutes = new System.Windows.Forms.TextBox();
            this.txtSeconds = new System.Windows.Forms.TextBox();
            this.txtMilliseconds = new System.Windows.Forms.TextBox();
            this.pnlHost.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHost
            // 
            this.pnlHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHost.BackColor = System.Drawing.SystemColors.Window;
            this.pnlHost.ColumnCount = 7;
            this.pnlHost.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlHost.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.pnlHost.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlHost.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.pnlHost.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlHost.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.pnlHost.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlHost.Controls.Add(this.label1, 1, 0);
            this.pnlHost.Controls.Add(this.label2, 3, 0);
            this.pnlHost.Controls.Add(this.label3, 5, 0);
            this.pnlHost.Controls.Add(this.txtHours, 0, 0);
            this.pnlHost.Controls.Add(this.txtMinutes, 2, 0);
            this.pnlHost.Controls.Add(this.txtSeconds, 4, 0);
            this.pnlHost.Controls.Add(this.txtMilliseconds, 6, 0);
            this.pnlHost.Location = new System.Drawing.Point(1, 1);
            this.pnlHost.Name = "pnlHost";
            this.pnlHost.RowCount = 1;
            this.pnlHost.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlHost.Size = new System.Drawing.Size(178, 21);
            this.pnlHost.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Impact", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(37, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = ":";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Impact", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(84, 1);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 1, 0, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = ":";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Impact", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(131, 1);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 1, 0, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = ".";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHours
            // 
            this.txtHours.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHours.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHours.Location = new System.Drawing.Point(3, 3);
            this.txtHours.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.txtHours.MaxLength = 2;
            this.txtHours.Name = "txtHours";
            this.txtHours.Size = new System.Drawing.Size(34, 13);
            this.txtHours.TabIndex = 3;
            this.txtHours.Text = "00";
            this.txtHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHours.TextChanged += new System.EventHandler(this.txtHoursTextChangedHandler);
            this.txtHours.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHoursKeyPressHandler);
            this.txtHours.Validating += new System.ComponentModel.CancelEventHandler(this.txtHoursValidatingHandler);
            // 
            // txtMinutes
            // 
            this.txtMinutes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMinutes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMinutes.Location = new System.Drawing.Point(47, 3);
            this.txtMinutes.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.txtMinutes.MaxLength = 2;
            this.txtMinutes.Name = "txtMinutes";
            this.txtMinutes.Size = new System.Drawing.Size(37, 13);
            this.txtMinutes.TabIndex = 4;
            this.txtMinutes.Text = "00";
            this.txtMinutes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMinutes.TextChanged += new System.EventHandler(this.txtMinutesTextChangedHandler);
            this.txtMinutes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMinutesKeyPressHandler);
            this.txtMinutes.Validating += new System.ComponentModel.CancelEventHandler(this.txtMinutesValidatingHandler);
            // 
            // txtSeconds
            // 
            this.txtSeconds.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSeconds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSeconds.Location = new System.Drawing.Point(94, 3);
            this.txtSeconds.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.txtSeconds.MaxLength = 2;
            this.txtSeconds.Name = "txtSeconds";
            this.txtSeconds.Size = new System.Drawing.Size(37, 13);
            this.txtSeconds.TabIndex = 5;
            this.txtSeconds.Text = "00";
            this.txtSeconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSeconds.TextChanged += new System.EventHandler(this.txtSecondsTextChangedHandler);
            this.txtSeconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSecondsKeyPressHandler);
            this.txtSeconds.Validating += new System.ComponentModel.CancelEventHandler(this.txtSecondsValidatingHandler);
            // 
            // txtMilliseconds
            // 
            this.txtMilliseconds.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMilliseconds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMilliseconds.Location = new System.Drawing.Point(141, 3);
            this.txtMilliseconds.Margin = new System.Windows.Forms.Padding(0, 3, 3, 0);
            this.txtMilliseconds.MaxLength = 3;
            this.txtMilliseconds.Name = "txtMilliseconds";
            this.txtMilliseconds.Size = new System.Drawing.Size(34, 13);
            this.txtMilliseconds.TabIndex = 6;
            this.txtMilliseconds.Text = "000";
            this.txtMilliseconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMilliseconds.TextChanged += new System.EventHandler(this.txtMillisecondsTextChangedHandler);
            this.txtMilliseconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMillisecondsKeyPressHandler);
            this.txtMilliseconds.Validating += new System.ComponentModel.CancelEventHandler(this.txtMillisecondsValidatingHandler);
            // 
            // TimeBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.pnlHost);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "TimeBox";
            this.Size = new System.Drawing.Size(180, 23);
            this.EnabledChanged += new System.EventHandler(this.EnabledChangedHandler);
            this.pnlHost.ResumeLayout(false);
            this.pnlHost.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnlHost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHours;
        private System.Windows.Forms.TextBox txtMinutes;
        private System.Windows.Forms.TextBox txtSeconds;
        private System.Windows.Forms.TextBox txtMilliseconds;
    }
}
