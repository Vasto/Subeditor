namespace Subeditor.Views.Tools.Synchronization
{
    partial class SynchronizationToolView
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
            this.lvTimings = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSynchronize = new System.Windows.Forms.Button();
            this.pnlCorrectTimingHost = new System.Windows.Forms.Panel();
            this.pnlOrginalTimingHost = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.viewsContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewsContainer
            // 
            this.viewsContainer.Controls.Add(this.label1);
            this.viewsContainer.Controls.Add(this.btnChange);
            this.viewsContainer.Controls.Add(this.btnAdd);
            this.viewsContainer.Controls.Add(this.btnDelete);
            this.viewsContainer.Controls.Add(this.pnlOrginalTimingHost);
            this.viewsContainer.Controls.Add(this.pnlCorrectTimingHost);
            this.viewsContainer.Controls.Add(this.btnSynchronize);
            this.viewsContainer.Controls.Add(this.panel1);
            this.viewsContainer.Controls.Add(this.lvTimings);
            this.viewsContainer.Size = new System.Drawing.Size(343, 253);
            // 
            // lvTimings
            // 
            this.lvTimings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvTimings.BackColor = System.Drawing.SystemColors.Window;
            this.lvTimings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvTimings.CausesValidation = false;
            this.lvTimings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvTimings.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lvTimings.FullRowSelect = true;
            this.lvTimings.GridLines = true;
            this.lvTimings.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvTimings.HideSelection = false;
            this.lvTimings.Location = new System.Drawing.Point(12, 28);
            this.lvTimings.MultiSelect = false;
            this.lvTimings.Name = "lvTimings";
            this.lvTimings.Size = new System.Drawing.Size(318, 113);
            this.lvTimings.TabIndex = 0;
            this.lvTimings.UseCompatibleStateImageBehavior = false;
            this.lvTimings.View = System.Windows.Forms.View.Details;
            this.lvTimings.SelectedIndexChanged += new System.EventHandler(this.lvTimingsSelectedIndexChangedHandler);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Orginal timing";
            this.columnHeader1.Width = 159;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Correct timing";
            this.columnHeader2.Width = 159;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Location = new System.Drawing.Point(12, 214);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(318, 1);
            this.panel1.TabIndex = 7;
            // 
            // btnSynchronize
            // 
            this.btnSynchronize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSynchronize.Location = new System.Drawing.Point(256, 221);
            this.btnSynchronize.Name = "btnSynchronize";
            this.btnSynchronize.Size = new System.Drawing.Size(75, 23);
            this.btnSynchronize.TabIndex = 6;
            this.btnSynchronize.Text = "Synchroznie";
            this.btnSynchronize.UseVisualStyleBackColor = true;
            this.btnSynchronize.Click += new System.EventHandler(this.btnSynchronizeClickHandler);
            // 
            // pnlCorrectTimingHost
            // 
            this.pnlCorrectTimingHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCorrectTimingHost.BackColor = System.Drawing.SystemColors.Control;
            this.pnlCorrectTimingHost.Location = new System.Drawing.Point(174, 150);
            this.pnlCorrectTimingHost.Name = "pnlCorrectTimingHost";
            this.pnlCorrectTimingHost.Size = new System.Drawing.Size(156, 20);
            this.pnlCorrectTimingHost.TabIndex = 2;
            // 
            // pnlOrginalTimingHost
            // 
            this.pnlOrginalTimingHost.BackColor = System.Drawing.SystemColors.Control;
            this.pnlOrginalTimingHost.Location = new System.Drawing.Point(12, 150);
            this.pnlOrginalTimingHost.Name = "pnlOrginalTimingHost";
            this.pnlOrginalTimingHost.Size = new System.Drawing.Size(156, 20);
            this.pnlOrginalTimingHost.TabIndex = 1;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(93, 179);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDeleteClickHandler);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 179);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAddClickHandler);
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(174, 179);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(75, 23);
            this.btnChange.TabIndex = 5;
            this.btnChange.Text = "Change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChangeClickHandler);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Synchronization control points (at least 2):";
            // 
            // SynchronizationToolView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 253);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SynchronizationToolView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Synchronization";
            this.ViewName = "SynchronizationToolView";
            this.viewsContainer.ResumeLayout(false);
            this.viewsContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvTimings;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnSynchronize;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Panel pnlOrginalTimingHost;
        private System.Windows.Forms.Panel pnlCorrectTimingHost;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Label label1;
    }
}