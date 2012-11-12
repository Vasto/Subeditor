namespace Subeditor.Views.Commands
{
    partial class MenuView
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
            this.menuStrip = new KWinFramework.Views.WinForms.Commands.CommandMenuStripView();
            this.menuFile = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.menuFileOpen = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.menuFileSave = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.menuFileSaveAs = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileExit = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.viewsContainer.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewsContainer
            // 
            this.viewsContainer.Controls.Add(this.menuStrip);
            this.viewsContainer.Size = new System.Drawing.Size(638, 26);
            // 
            // menuStrip
            // 
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.ParentView = this;
            this.menuStrip.Size = new System.Drawing.Size(638, 26);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "commandMenuStripView1";
            this.menuStrip.ViewName = "MenuView";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileOpen,
            this.menuFileSave,
            this.menuFileSaveAs,
            this.toolStripSeparator1,
            this.menuFileExit});
            this.menuFile.IsExecutable = true;
            this.menuFile.Name = "menuFile";
            this.menuFile.ParentView = this.menuStrip;
            this.menuFile.Size = new System.Drawing.Size(37, 22);
            this.menuFile.Text = "File";
            this.menuFile.ViewName = "MenuFileView";
            // 
            // menuFileOpen
            // 
            this.menuFileOpen.IsExecutable = true;
            this.menuFileOpen.Name = "menuFileOpen";
            this.menuFileOpen.ParentView = this.menuFile;
            this.menuFileOpen.Size = new System.Drawing.Size(152, 22);
            this.menuFileOpen.Text = "Open";
            this.menuFileOpen.ViewName = "OpenView";
            // 
            // menuFileSave
            // 
            this.menuFileSave.IsExecutable = true;
            this.menuFileSave.Name = "menuFileSave";
            this.menuFileSave.ParentView = this.menuFile;
            this.menuFileSave.Size = new System.Drawing.Size(152, 22);
            this.menuFileSave.Text = "Save";
            this.menuFileSave.ViewName = "SaveView";
            // 
            // menuFileSaveAs
            // 
            this.menuFileSaveAs.IsExecutable = true;
            this.menuFileSaveAs.Name = "menuFileSaveAs";
            this.menuFileSaveAs.ParentView = this.menuFile;
            this.menuFileSaveAs.Size = new System.Drawing.Size(152, 22);
            this.menuFileSaveAs.Text = "Save As...";
            this.menuFileSaveAs.ViewName = "SaveAsView";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // menuFileExit
            // 
            this.menuFileExit.IsExecutable = true;
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.ParentView = this.menuFile;
            this.menuFileExit.Size = new System.Drawing.Size(152, 22);
            this.menuFileExit.Text = "Exit";
            this.menuFileExit.ViewName = "ExitView";
            // 
            // MenuView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "MenuView";
            this.Size = new System.Drawing.Size(638, 26);
            this.viewsContainer.ResumeLayout(false);
            this.viewsContainer.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KWinFramework.Views.WinForms.Commands.CommandMenuStripView menuStrip;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuFile;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuFileOpen;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuFileSave;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuFileExit;
    }
}
