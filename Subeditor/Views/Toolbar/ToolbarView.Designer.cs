namespace Subeditor.Views.Toolbar
{
    partial class ToolbarView
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
            this.commandToolStripView1 = new KWinFramework.Views.WinForms.Commands.CommandToolStripView();
            this.viewsContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewsContainer
            // 
            this.viewsContainer.Controls.Add(this.commandToolStripView1);
            this.viewsContainer.Size = new System.Drawing.Size(691, 25);
            // 
            // commandToolStripView1
            // 
            this.commandToolStripView1.Location = new System.Drawing.Point(0, 0);
            this.commandToolStripView1.Name = "commandToolStripView1";
            this.commandToolStripView1.ParentView = this;
            this.commandToolStripView1.Size = new System.Drawing.Size(691, 25);
            this.commandToolStripView1.TabIndex = 0;
            this.commandToolStripView1.Text = "commandToolStripView1";
            this.commandToolStripView1.ViewName = null;
            // 
            // ToolbarView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ToolbarView";
            this.Size = new System.Drawing.Size(691, 25);
            this.ViewName = "ToolbarView";
            this.viewsContainer.ResumeLayout(false);
            this.viewsContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private KWinFramework.Views.WinForms.Commands.CommandToolStripView commandToolStripView1;

    }
}
