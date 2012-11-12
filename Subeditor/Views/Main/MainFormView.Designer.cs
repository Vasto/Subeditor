namespace Subeditor.Views.Main
{
    partial class MainFormView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFormView));
            this.toolbarStrip = new KWinFramework.Views.WinForms.Commands.ToolStripView();
            this.toolbarOpenFile = new KWinFramework.Views.WinForms.Commands.ToolStripButtonView();
            this.toolbarSaveFile = new KWinFramework.Views.WinForms.Commands.ToolStripButtonView();
            this.toolbarSaveAsFile = new KWinFramework.Views.WinForms.Commands.ToolStripButtonView();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbarCut = new KWinFramework.Views.WinForms.Commands.ToolStripButtonView();
            this.toolbarCopy = new KWinFramework.Views.WinForms.Commands.ToolStripButtonView();
            this.toolbarPaste = new KWinFramework.Views.WinForms.Commands.ToolStripButtonView();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolbarUndo = new KWinFramework.Views.WinForms.Commands.ToolStripButtonView();
            this.toolbarRedo = new KWinFramework.Views.WinForms.Commands.ToolStripButtonView();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxView1 = new KWinFramework.Views.WinForms.Menus.ToolStripComboBoxView();
            this.menuStrip = new KWinFramework.Views.WinForms.Commands.MenuStripView();
            this.menuFile = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.menuFileOpen = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.menuFileSave = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.menuFileSaveAs = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuFileExit = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.menuEdit = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.menuEditUndo = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.menuEditRedo = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuEditCut = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.menuEditCopy = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.menuEditPaste = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuEditSelectAll = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.menuEditSelectFrom = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.menuTools = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.menuToolsTimingAdjustment = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.menuToolsSynchronization = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.subtitlesView = new Subeditor.Views.Subtitles.ScintillaSubtitlesView();
            this.viewsContainer.SuspendLayout();
            this.toolbarStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.subtitlesView.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewsContainer
            // 
            this.viewsContainer.Controls.Add(this.menuStrip);
            this.viewsContainer.Controls.Add(this.toolbarStrip);
            this.viewsContainer.Controls.Add(this.subtitlesView);
            this.viewsContainer.Size = new System.Drawing.Size(944, 562);
            // 
            // toolbarStrip
            // 
            this.toolbarStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolbarStrip.AutoSize = false;
            this.toolbarStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolbarStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolbarOpenFile,
            this.toolbarSaveFile,
            this.toolbarSaveAsFile,
            this.toolStripSeparator2,
            this.toolbarCut,
            this.toolbarCopy,
            this.toolbarPaste,
            this.toolStripSeparator5,
            this.toolbarUndo,
            this.toolbarRedo,
            this.toolStripSeparator6,
            this.toolStripLabel1,
            this.toolStripComboBoxView1});
            this.toolbarStrip.Location = new System.Drawing.Point(0, 24);
            this.toolbarStrip.Name = "toolbarStrip";
            this.toolbarStrip.ParentView = this;
            this.toolbarStrip.Size = new System.Drawing.Size(952, 28);
            this.toolbarStrip.TabIndex = 6;
            this.toolbarStrip.Text = "commandToolStripView1";
            this.toolbarStrip.ViewName = "ToolbarView";
            // 
            // toolbarOpenFile
            // 
            this.toolbarOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbarOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("toolbarOpenFile.Image")));
            this.toolbarOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbarOpenFile.IsExecutable = true;
            this.toolbarOpenFile.Name = "toolbarOpenFile";
            this.toolbarOpenFile.ParentView = this.toolbarStrip;
            this.toolbarOpenFile.Size = new System.Drawing.Size(23, 25);
            this.toolbarOpenFile.Text = "Open";
            this.toolbarOpenFile.ViewName = "ToolbarOpenFileView";
            // 
            // toolbarSaveFile
            // 
            this.toolbarSaveFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbarSaveFile.Image = ((System.Drawing.Image)(resources.GetObject("toolbarSaveFile.Image")));
            this.toolbarSaveFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbarSaveFile.IsExecutable = true;
            this.toolbarSaveFile.Name = "toolbarSaveFile";
            this.toolbarSaveFile.ParentView = this.toolbarStrip;
            this.toolbarSaveFile.Size = new System.Drawing.Size(23, 25);
            this.toolbarSaveFile.Text = "Save";
            this.toolbarSaveFile.ViewName = "ToolbarSaveFileView";
            // 
            // toolbarSaveAsFile
            // 
            this.toolbarSaveAsFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbarSaveAsFile.Image = ((System.Drawing.Image)(resources.GetObject("toolbarSaveAsFile.Image")));
            this.toolbarSaveAsFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbarSaveAsFile.IsExecutable = true;
            this.toolbarSaveAsFile.Name = "toolbarSaveAsFile";
            this.toolbarSaveAsFile.ParentView = this.toolbarStrip;
            this.toolbarSaveAsFile.Size = new System.Drawing.Size(23, 25);
            this.toolbarSaveAsFile.Text = "Save As...";
            this.toolbarSaveAsFile.ViewName = "ToolbarSaveAsFileView";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // toolbarCut
            // 
            this.toolbarCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbarCut.Image = ((System.Drawing.Image)(resources.GetObject("toolbarCut.Image")));
            this.toolbarCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbarCut.IsExecutable = true;
            this.toolbarCut.Name = "toolbarCut";
            this.toolbarCut.ParentView = this.toolbarStrip;
            this.toolbarCut.Size = new System.Drawing.Size(23, 25);
            this.toolbarCut.Text = "Cut";
            this.toolbarCut.ViewName = "ToolbarCutView";
            // 
            // toolbarCopy
            // 
            this.toolbarCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbarCopy.Image = ((System.Drawing.Image)(resources.GetObject("toolbarCopy.Image")));
            this.toolbarCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbarCopy.IsExecutable = true;
            this.toolbarCopy.Name = "toolbarCopy";
            this.toolbarCopy.ParentView = this.toolbarStrip;
            this.toolbarCopy.Size = new System.Drawing.Size(23, 25);
            this.toolbarCopy.Text = "Copy";
            this.toolbarCopy.ViewName = "ToolbarCopyView";
            // 
            // toolbarPaste
            // 
            this.toolbarPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbarPaste.Image = ((System.Drawing.Image)(resources.GetObject("toolbarPaste.Image")));
            this.toolbarPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbarPaste.IsExecutable = true;
            this.toolbarPaste.Name = "toolbarPaste";
            this.toolbarPaste.ParentView = this.toolbarStrip;
            this.toolbarPaste.Size = new System.Drawing.Size(23, 25);
            this.toolbarPaste.Text = "Paste";
            this.toolbarPaste.ViewName = "ToolbarPasteView";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 28);
            // 
            // toolbarUndo
            // 
            this.toolbarUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbarUndo.Image = ((System.Drawing.Image)(resources.GetObject("toolbarUndo.Image")));
            this.toolbarUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbarUndo.IsExecutable = true;
            this.toolbarUndo.Name = "toolbarUndo";
            this.toolbarUndo.ParentView = this.toolbarStrip;
            this.toolbarUndo.Size = new System.Drawing.Size(23, 25);
            this.toolbarUndo.Text = "Undo";
            this.toolbarUndo.ViewName = "ToolbarUndoView";
            // 
            // toolbarRedo
            // 
            this.toolbarRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolbarRedo.Image = ((System.Drawing.Image)(resources.GetObject("toolbarRedo.Image")));
            this.toolbarRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolbarRedo.IsExecutable = true;
            this.toolbarRedo.Name = "toolbarRedo";
            this.toolbarRedo.ParentView = this.toolbarStrip;
            this.toolbarRedo.Size = new System.Drawing.Size(23, 25);
            this.toolbarRedo.Text = "Redo";
            this.toolbarRedo.ViewName = "ToolbarRedoView";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(60, 25);
            this.toolStripLabel1.Text = "Encoding:";
            // 
            // toolStripComboBoxView1
            // 
            this.toolStripComboBoxView1.CausesValidation = false;
            this.toolStripComboBoxView1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxView1.Margin = new System.Windows.Forms.Padding(1, 3, 1, 2);
            this.toolStripComboBoxView1.Name = "toolStripComboBoxView1";
            this.toolStripComboBoxView1.ParentView = this.toolbarStrip;
            this.toolStripComboBoxView1.SelectedValue = null;
            this.toolStripComboBoxView1.Size = new System.Drawing.Size(150, 23);
            this.toolStripComboBoxView1.ViewName = "ToolbarEncodingView";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEdit,
            this.menuTools});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.ParentView = this;
            this.menuStrip.Size = new System.Drawing.Size(944, 24);
            this.menuStrip.TabIndex = 7;
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
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "File";
            this.menuFile.ViewName = "MenuFileView";
            // 
            // menuFileOpen
            // 
            this.menuFileOpen.IsExecutable = true;
            this.menuFileOpen.Name = "menuFileOpen";
            this.menuFileOpen.ParentView = this.menuFile;
            this.menuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.menuFileOpen.Size = new System.Drawing.Size(146, 22);
            this.menuFileOpen.Text = "Open";
            this.menuFileOpen.ViewName = "MenuOpenView";
            // 
            // menuFileSave
            // 
            this.menuFileSave.IsExecutable = true;
            this.menuFileSave.Name = "menuFileSave";
            this.menuFileSave.ParentView = this.menuFile;
            this.menuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuFileSave.Size = new System.Drawing.Size(146, 22);
            this.menuFileSave.Text = "Save";
            this.menuFileSave.ViewName = "MenuSaveView";
            // 
            // menuFileSaveAs
            // 
            this.menuFileSaveAs.IsExecutable = true;
            this.menuFileSaveAs.Name = "menuFileSaveAs";
            this.menuFileSaveAs.ParentView = this.menuFile;
            this.menuFileSaveAs.Size = new System.Drawing.Size(146, 22);
            this.menuFileSaveAs.Text = "Save As...";
            this.menuFileSaveAs.ViewName = "MenuSaveAsView";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // menuFileExit
            // 
            this.menuFileExit.IsExecutable = true;
            this.menuFileExit.Name = "menuFileExit";
            this.menuFileExit.ParentView = this.menuFile;
            this.menuFileExit.Size = new System.Drawing.Size(146, 22);
            this.menuFileExit.Text = "Exit";
            this.menuFileExit.ViewName = "MenuExitView";
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEditUndo,
            this.menuEditRedo,
            this.toolStripSeparator3,
            this.menuEditCut,
            this.menuEditCopy,
            this.menuEditPaste,
            this.toolStripSeparator4,
            this.menuEditSelectAll,
            this.menuEditSelectFrom});
            this.menuEdit.IsExecutable = true;
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.ParentView = this.menuStrip;
            this.menuEdit.Size = new System.Drawing.Size(39, 20);
            this.menuEdit.Text = "Edit";
            this.menuEdit.ViewName = "MenuEditView";
            // 
            // menuEditUndo
            // 
            this.menuEditUndo.IsExecutable = true;
            this.menuEditUndo.Name = "menuEditUndo";
            this.menuEditUndo.ParentView = this.menuEdit;
            this.menuEditUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.menuEditUndo.Size = new System.Drawing.Size(210, 22);
            this.menuEditUndo.Text = "Undo";
            this.menuEditUndo.ViewName = "MenuUndoView";
            // 
            // menuEditRedo
            // 
            this.menuEditRedo.IsExecutable = true;
            this.menuEditRedo.Name = "menuEditRedo";
            this.menuEditRedo.ParentView = this.menuEdit;
            this.menuEditRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.menuEditRedo.Size = new System.Drawing.Size(210, 22);
            this.menuEditRedo.Text = "Redo";
            this.menuEditRedo.ViewName = "MenuRedoView";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(207, 6);
            // 
            // menuEditCut
            // 
            this.menuEditCut.IsExecutable = true;
            this.menuEditCut.Name = "menuEditCut";
            this.menuEditCut.ParentView = this.menuEdit;
            this.menuEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.menuEditCut.Size = new System.Drawing.Size(210, 22);
            this.menuEditCut.Text = "Cut";
            this.menuEditCut.ViewName = "MenuCutView";
            // 
            // menuEditCopy
            // 
            this.menuEditCopy.IsExecutable = true;
            this.menuEditCopy.Name = "menuEditCopy";
            this.menuEditCopy.ParentView = this.menuEdit;
            this.menuEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.menuEditCopy.Size = new System.Drawing.Size(210, 22);
            this.menuEditCopy.Text = "Copy";
            this.menuEditCopy.ViewName = "MenuCopyView";
            // 
            // menuEditPaste
            // 
            this.menuEditPaste.IsExecutable = true;
            this.menuEditPaste.Name = "menuEditPaste";
            this.menuEditPaste.ParentView = this.menuEdit;
            this.menuEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.menuEditPaste.Size = new System.Drawing.Size(210, 22);
            this.menuEditPaste.Text = "Paste";
            this.menuEditPaste.ViewName = "MenuPasteView";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(207, 6);
            // 
            // menuEditSelectAll
            // 
            this.menuEditSelectAll.IsExecutable = true;
            this.menuEditSelectAll.Name = "menuEditSelectAll";
            this.menuEditSelectAll.ParentView = this.menuEdit;
            this.menuEditSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.menuEditSelectAll.Size = new System.Drawing.Size(210, 22);
            this.menuEditSelectAll.Text = "Select All";
            this.menuEditSelectAll.ViewName = "MenuSelectAllView";
            // 
            // menuEditSelectFrom
            // 
            this.menuEditSelectFrom.IsExecutable = true;
            this.menuEditSelectFrom.Name = "menuEditSelectFrom";
            this.menuEditSelectFrom.ParentView = this.menuEdit;
            this.menuEditSelectFrom.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
            this.menuEditSelectFrom.Size = new System.Drawing.Size(210, 22);
            this.menuEditSelectFrom.Text = "Select From";
            this.menuEditSelectFrom.ViewName = "MenuSelectFromView";
            // 
            // menuTools
            // 
            this.menuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolsTimingAdjustment,
            this.menuToolsSynchronization});
            this.menuTools.IsExecutable = true;
            this.menuTools.Name = "menuTools";
            this.menuTools.ParentView = this.menuStrip;
            this.menuTools.Size = new System.Drawing.Size(48, 20);
            this.menuTools.Text = "Tools";
            this.menuTools.ViewName = "MenuToolsView";
            // 
            // menuToolsTimingAdjustment
            // 
            this.menuToolsTimingAdjustment.IsExecutable = true;
            this.menuToolsTimingAdjustment.Name = "menuToolsTimingAdjustment";
            this.menuToolsTimingAdjustment.ParentView = this.menuTools;
            this.menuToolsTimingAdjustment.Size = new System.Drawing.Size(177, 22);
            this.menuToolsTimingAdjustment.Text = "Timing Adjustment";
            this.menuToolsTimingAdjustment.ViewName = "MenuTimingAdjustmentView";
            // 
            // menuToolsSynchronization
            // 
            this.menuToolsSynchronization.IsExecutable = true;
            this.menuToolsSynchronization.Name = "menuToolsSynchronization";
            this.menuToolsSynchronization.ParentView = this.menuTools;
            this.menuToolsSynchronization.Size = new System.Drawing.Size(177, 22);
            this.menuToolsSynchronization.Text = "Synchronization";
            this.menuToolsSynchronization.ViewName = "MenuSynchronizationView";
            // 
            // subtitlesView
            // 
            this.subtitlesView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subtitlesView.CaretPosition = 0;
            this.subtitlesView.Content = "";
            this.subtitlesView.LineNumberColumnWidth = 25;
            this.subtitlesView.Location = new System.Drawing.Point(3, 55);
            this.subtitlesView.Name = "subtitlesView";
            this.subtitlesView.ParentView = this;
            this.subtitlesView.Size = new System.Drawing.Size(941, 507);
            this.subtitlesView.TabIndex = 4;
            this.subtitlesView.ViewName = "SubtitlesView";
            // 
            // subtitlesView.ViewsContainer
            // 
            this.subtitlesView.ViewsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subtitlesView.ViewsContainer.Location = new System.Drawing.Point(0, 0);
            this.subtitlesView.ViewsContainer.Name = "ViewsContainer";
            this.subtitlesView.ViewsContainer.Size = new System.Drawing.Size(941, 507);
            this.subtitlesView.ViewsContainer.TabIndex = 0;
            // 
            // MainFormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 562);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainFormView";
            this.Text = "Subeditor";
            this.viewsContainer.ResumeLayout(false);
            this.viewsContainer.PerformLayout();
            this.toolbarStrip.ResumeLayout(false);
            this.toolbarStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.subtitlesView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Subtitles.ScintillaSubtitlesView subtitlesView;
        private KWinFramework.Views.WinForms.Commands.ToolStripView toolbarStrip;
        private KWinFramework.Views.WinForms.Commands.MenuStripView menuStrip;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuFile;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuFileOpen;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuFileSave;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuFileExit;
        private KWinFramework.Views.WinForms.Commands.ToolStripButtonView toolbarOpenFile;
        private KWinFramework.Views.WinForms.Commands.ToolStripButtonView toolbarSaveFile;
        private KWinFramework.Views.WinForms.Commands.ToolStripButtonView toolbarSaveAsFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuEdit;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuTools;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuEditUndo;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuEditRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuEditCut;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuEditCopy;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuEditPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuEditSelectAll;
        private KWinFramework.Views.WinForms.Commands.ToolStripButtonView toolbarCut;
        private KWinFramework.Views.WinForms.Commands.ToolStripButtonView toolbarCopy;
        private KWinFramework.Views.WinForms.Commands.ToolStripButtonView toolbarPaste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private KWinFramework.Views.WinForms.Commands.ToolStripButtonView toolbarUndo;
        private KWinFramework.Views.WinForms.Commands.ToolStripButtonView toolbarRedo;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuToolsTimingAdjustment;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuEditSelectFrom;
        private KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView menuToolsSynchronization;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private KWinFramework.Views.WinForms.Menus.ToolStripComboBoxView toolStripComboBoxView1;
    }
}