namespace Subeditor.Views.Tools.TimingAdjusts
{
    partial class TimingAdjustmentToolView
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
            this.pnlTimingChangeHost = new System.Windows.Forms.Panel();
            this.lblTimingChange = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSubstract = new System.Windows.Forms.Button();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbSelection = new System.Windows.Forms.RadioButton();
            this.gbApplyTo = new System.Windows.Forms.GroupBox();
            this.cbTo = new System.Windows.Forms.CheckBox();
            this.cbFrom = new System.Windows.Forms.CheckBox();
            this.pnlTimingToHost = new System.Windows.Forms.Panel();
            this.pnlTimingFromHost = new System.Windows.Forms.Panel();
            this.rbRange = new System.Windows.Forms.RadioButton();
            this.viewsContainer.SuspendLayout();
            this.gbApplyTo.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewsContainer
            // 
            this.viewsContainer.Controls.Add(this.gbApplyTo);
            this.viewsContainer.Controls.Add(this.btnSubstract);
            this.viewsContainer.Controls.Add(this.btnAdd);
            this.viewsContainer.Controls.Add(this.lblTimingChange);
            this.viewsContainer.Controls.Add(this.pnlTimingChangeHost);
            this.viewsContainer.Size = new System.Drawing.Size(321, 218);
            // 
            // pnlTimingChangeHost
            // 
            this.pnlTimingChangeHost.BackColor = System.Drawing.SystemColors.Control;
            this.pnlTimingChangeHost.Location = new System.Drawing.Point(98, 12);
            this.pnlTimingChangeHost.Name = "pnlTimingChangeHost";
            this.pnlTimingChangeHost.Size = new System.Drawing.Size(211, 20);
            this.pnlTimingChangeHost.TabIndex = 1;
            // 
            // lblTimingChange
            // 
            this.lblTimingChange.AutoSize = true;
            this.lblTimingChange.Location = new System.Drawing.Point(12, 17);
            this.lblTimingChange.Name = "lblTimingChange";
            this.lblTimingChange.Size = new System.Drawing.Size(80, 13);
            this.lblTimingChange.TabIndex = 0;
            this.lblTimingChange.Text = "Timing change:";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(153, 184);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAddClickHandler);
            // 
            // btnSubstract
            // 
            this.btnSubstract.Location = new System.Drawing.Point(234, 184);
            this.btnSubstract.Name = "btnSubstract";
            this.btnSubstract.Size = new System.Drawing.Size(75, 23);
            this.btnSubstract.TabIndex = 4;
            this.btnSubstract.Text = "Substract";
            this.btnSubstract.UseVisualStyleBackColor = true;
            this.btnSubstract.Click += new System.EventHandler(this.btnSubstractClickHandler);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Location = new System.Drawing.Point(20, 19);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(60, 17);
            this.rbAll.TabIndex = 0;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "All lines";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAllCheckedChangedHandler);
            // 
            // rbSelection
            // 
            this.rbSelection.AutoSize = true;
            this.rbSelection.Location = new System.Drawing.Point(141, 19);
            this.rbSelection.Name = "rbSelection";
            this.rbSelection.Size = new System.Drawing.Size(85, 17);
            this.rbSelection.TabIndex = 1;
            this.rbSelection.TabStop = true;
            this.rbSelection.Text = "Slected lines";
            this.rbSelection.UseVisualStyleBackColor = true;
            this.rbSelection.CheckedChanged += new System.EventHandler(this.rbSelectionCheckedChangedHandler);
            // 
            // gbApplyTo
            // 
            this.gbApplyTo.Controls.Add(this.cbTo);
            this.gbApplyTo.Controls.Add(this.cbFrom);
            this.gbApplyTo.Controls.Add(this.pnlTimingToHost);
            this.gbApplyTo.Controls.Add(this.pnlTimingFromHost);
            this.gbApplyTo.Controls.Add(this.rbRange);
            this.gbApplyTo.Controls.Add(this.rbAll);
            this.gbApplyTo.Controls.Add(this.rbSelection);
            this.gbApplyTo.Location = new System.Drawing.Point(12, 45);
            this.gbApplyTo.Name = "gbApplyTo";
            this.gbApplyTo.Size = new System.Drawing.Size(297, 133);
            this.gbApplyTo.TabIndex = 2;
            this.gbApplyTo.TabStop = false;
            this.gbApplyTo.Text = "Apply to";
            // 
            // cbTo
            // 
            this.cbTo.AutoSize = true;
            this.cbTo.Location = new System.Drawing.Point(33, 98);
            this.cbTo.Name = "cbTo";
            this.cbTo.Size = new System.Drawing.Size(42, 17);
            this.cbTo.TabIndex = 5;
            this.cbTo.Text = "To:";
            this.cbTo.UseVisualStyleBackColor = true;
            this.cbTo.CheckedChanged += new System.EventHandler(this.cbToCheckedChangedHandler);
            // 
            // cbFrom
            // 
            this.cbFrom.AutoSize = true;
            this.cbFrom.Location = new System.Drawing.Point(33, 72);
            this.cbFrom.Name = "cbFrom";
            this.cbFrom.Size = new System.Drawing.Size(52, 17);
            this.cbFrom.TabIndex = 3;
            this.cbFrom.Text = "From:";
            this.cbFrom.UseVisualStyleBackColor = true;
            this.cbFrom.CheckedChanged += new System.EventHandler(this.cbFromCheckedChangedHandler);
            // 
            // pnlTimingToHost
            // 
            this.pnlTimingToHost.BackColor = System.Drawing.SystemColors.Control;
            this.pnlTimingToHost.Location = new System.Drawing.Point(86, 95);
            this.pnlTimingToHost.Name = "pnlTimingToHost";
            this.pnlTimingToHost.Size = new System.Drawing.Size(205, 20);
            this.pnlTimingToHost.TabIndex = 6;
            // 
            // pnlTimingFromHost
            // 
            this.pnlTimingFromHost.BackColor = System.Drawing.SystemColors.Control;
            this.pnlTimingFromHost.Location = new System.Drawing.Point(86, 69);
            this.pnlTimingFromHost.Name = "pnlTimingFromHost";
            this.pnlTimingFromHost.Size = new System.Drawing.Size(205, 20);
            this.pnlTimingFromHost.TabIndex = 4;
            // 
            // rbRange
            // 
            this.rbRange.AutoSize = true;
            this.rbRange.Location = new System.Drawing.Point(20, 42);
            this.rbRange.Name = "rbRange";
            this.rbRange.Size = new System.Drawing.Size(57, 17);
            this.rbRange.TabIndex = 2;
            this.rbRange.TabStop = true;
            this.rbRange.Text = "Range";
            this.rbRange.UseVisualStyleBackColor = true;
            this.rbRange.CheckedChanged += new System.EventHandler(this.rbRangeCheckedChangedHandler);
            // 
            // TimingAdjustmentToolView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 218);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TimingAdjustmentToolView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Timing Adjustment";
            this.ViewName = "TimingAdjustmentToolView";
            this.viewsContainer.ResumeLayout(false);
            this.viewsContainer.PerformLayout();
            this.gbApplyTo.ResumeLayout(false);
            this.gbApplyTo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTimingChange;
        private System.Windows.Forms.Panel pnlTimingChangeHost;
        private System.Windows.Forms.RadioButton rbSelection;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.Button btnSubstract;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox gbApplyTo;
        private System.Windows.Forms.RadioButton rbRange;
        private System.Windows.Forms.Panel pnlTimingToHost;
        private System.Windows.Forms.Panel pnlTimingFromHost;
        private System.Windows.Forms.CheckBox cbTo;
        private System.Windows.Forms.CheckBox cbFrom;

    }
}