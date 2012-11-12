using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KWinFramework.Views;
using KWinFramework.Views.WinForms.Commands;

namespace Subeditor.Views.ContextMenu
{
    /// <summary>
    /// Widok głównego menu kontekstowego aplikacji.
    /// </summary>
    class MainContextMenuStripView : ContextMenuStripView, IMainContextMenuStripView
    {
        private ToolStripMenuItemView contextMenuCut;
        private ToolStripMenuItemView contextMenuCopy;
        private ToolStripMenuItemView contextMenuPaste;
        private ToolStripMenuItemView contextMenuSelectAll;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItemView contextMenuSelectFrom;
    
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public MainContextMenuStripView()
        {
            InitializeComponent();

            
        }

        public IView GetMenuItemView(String viewName)
        {
            foreach (var childView in ChildViews)
            {
                if (childView.ViewName == viewName)
                {
                    return childView;
                }
            }

            throw new Exception();
        }

        private void InitializeComponent()
        {
            this.contextMenuCut = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.contextMenuCopy = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.contextMenuPaste = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.contextMenuSelectAll = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.contextMenuSelectFrom = new KWinFramework.Views.WinForms.Commands.ToolStripMenuItemView();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.SuspendLayout();
            // 
            // contextMenuCut
            // 
            this.contextMenuCut.IsExecutable = true;
            this.contextMenuCut.Name = "contextMenuCut";
            this.contextMenuCut.ParentView = this;
            this.contextMenuCut.ShortcutKeyDisplayString = "Ctrl+X";
            this.contextMenuCut.Size = new System.Drawing.Size(210, 22);
            this.contextMenuCut.Text = "Cut";
            this.contextMenuCut.ViewName = "ContextMenuCutView";
            // 
            // contextMenuCopy
            // 
            this.contextMenuCopy.IsExecutable = true;
            this.contextMenuCopy.Name = "contextMenuCopy";
            this.contextMenuCopy.ParentView = this;
            this.contextMenuCopy.ShortcutKeyDisplayString = "Ctrl+C";
            this.contextMenuCopy.Size = new System.Drawing.Size(210, 22);
            this.contextMenuCopy.Text = "Copy";
            this.contextMenuCopy.ViewName = "ContextMenuCopyView";
            // 
            // contextMenuPaste
            // 
            this.contextMenuPaste.IsExecutable = true;
            this.contextMenuPaste.Name = "contextMenuPaste";
            this.contextMenuPaste.ParentView = this;
            this.contextMenuPaste.ShortcutKeyDisplayString = "Ctrl+V";
            this.contextMenuPaste.Size = new System.Drawing.Size(210, 22);
            this.contextMenuPaste.Text = "Paste";
            this.contextMenuPaste.ViewName = "ContextMenuPasteView";
            // 
            // contextMenuSelectAll
            // 
            this.contextMenuSelectAll.IsExecutable = true;
            this.contextMenuSelectAll.Name = "contextMenuSelectAll";
            this.contextMenuSelectAll.ParentView = this;
            this.contextMenuSelectAll.ShortcutKeyDisplayString = "Ctrl+A";
            this.contextMenuSelectAll.Size = new System.Drawing.Size(210, 22);
            this.contextMenuSelectAll.Text = "Select All";
            this.contextMenuSelectAll.ViewName = "ContextMenuSelectAllView";
            // 
            // contextMenuSelectFrom
            // 
            this.contextMenuSelectFrom.IsExecutable = true;
            this.contextMenuSelectFrom.Name = "contextMenuSelectFrom";
            this.contextMenuSelectFrom.ParentView = this;
            this.contextMenuSelectFrom.ShortcutKeyDisplayString = "Ctrl+Shift+A";
            this.contextMenuSelectFrom.Size = new System.Drawing.Size(210, 22);
            this.contextMenuSelectFrom.Text = "Select From";
            this.contextMenuSelectFrom.ViewName = "ContextMenuSelectFromView";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 6);
            // 
            // MainContextMenuStripView
            // 
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuCut,
            this.contextMenuCopy,
            this.contextMenuPaste,
            this.toolStripSeparator1,
            this.contextMenuSelectAll,
            this.contextMenuSelectFrom});
            this.Size = new System.Drawing.Size(211, 114);
            this.ViewName = "MainContextMenuStripView";
            this.ResumeLayout(false);

        }
    }
}
