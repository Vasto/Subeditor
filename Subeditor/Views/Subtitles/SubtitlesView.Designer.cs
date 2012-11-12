namespace Subeditor.Views.SubtitlesFiles
{
    partial class SubtitlesView
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
            this.textEditor = new ICSharpCode.TextEditor.TextEditorControl();
            this.viewsContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewsContainer
            // 
            this.viewsContainer.BackColor = System.Drawing.SystemColors.Window;
            this.viewsContainer.Controls.Add(this.textEditor);
            this.viewsContainer.Size = new System.Drawing.Size(840, 537);
            // 
            // textEditor
            // 
            this.textEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditor.IsReadOnly = false;
            this.textEditor.Location = new System.Drawing.Point(0, 0);
            this.textEditor.Name = "textEditor";
            this.textEditor.ShowVRuler = false;
            this.textEditor.Size = new System.Drawing.Size(840, 537);
            this.textEditor.TabIndex = 0;
            this.textEditor.Text = "textEditorControl1";
            this.textEditor.TextChanged += new System.EventHandler(this.OnTextEditorTextChanged);
            // 
            // SubtitlesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SubtitlesView";
            this.Size = new System.Drawing.Size(840, 537);
            this.ViewName = "SubtitlesFileView";
            this.viewsContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ICSharpCode.TextEditor.TextEditorControl textEditor;

    }
}
