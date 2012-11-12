namespace Subeditor.Views.Subtitles
{
    partial class ScintillaSubtitlesView
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
            this.textEditor = new ScintillaNET.Scintilla();
            this.viewsContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditor)).BeginInit();
            this.SuspendLayout();
            // 
            // viewsContainer
            // 
            this.viewsContainer.Controls.Add(this.textEditor);
            this.viewsContainer.Size = new System.Drawing.Size(730, 446);
            // 
            // textEditor
            // 
            this.textEditor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditor.Font = new System.Drawing.Font("Courier New", 13.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(238)));
            this.textEditor.Location = new System.Drawing.Point(0, 0);
            this.textEditor.Margins.Left = 5;
            this.textEditor.Margins.Margin0.Width = 25;
            this.textEditor.Name = "textEditor";
            this.textEditor.Size = new System.Drawing.Size(730, 446);
            this.textEditor.Styles.BraceBad.Size = 9F;
            this.textEditor.Styles.BraceLight.Size = 9F;
            this.textEditor.Styles.ControlChar.Size = 9F;
            this.textEditor.Styles.Default.BackColor = System.Drawing.SystemColors.Window;
            this.textEditor.Styles.Default.Size = 9F;
            this.textEditor.Styles.IndentGuide.Size = 9F;
            this.textEditor.Styles.LastPredefined.Size = 9F;
            this.textEditor.Styles.LineNumber.BackColor = System.Drawing.SystemColors.Menu;
            this.textEditor.Styles.LineNumber.CharacterSet = ScintillaNET.CharacterSet.EastEurope;
            this.textEditor.Styles.LineNumber.FontName = "Consolas";
            this.textEditor.Styles.LineNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.textEditor.Styles.LineNumber.Size = 9F;
            this.textEditor.Styles.Max.Size = 9F;
            this.textEditor.TabIndex = 0;
            this.textEditor.UndoRedo.IsUndoEnabled = false;
            this.textEditor.Scroll += new System.EventHandler<System.Windows.Forms.ScrollEventArgs>(this.ScrollHandler);
            this.textEditor.TextChanged += new System.EventHandler(this.TextEditorTextChangedHandler);
            // 
            // ScintillaSubtitlesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ScintillaSubtitlesView";
            this.Size = new System.Drawing.Size(730, 446);
            this.viewsContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ScintillaNET.Scintilla textEditor;
        //private ScintillaNET.Scintilla scintilla1;
    }
}
