using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Presenters;
using KWinFramework.Views;
using KWinFramework.Views.WinForms.Commands;
using Subeditor.Model;
using Subeditor.Views.Commands;

namespace Subeditor.Views.ContextMenu
{
    /// <summary>
    /// Prezenter głównego menu kontekstowego aplikacji.
    /// </summary>
    class MainContextMenuStripPresenter : PresenterBase<IMainContextMenuStripView>
    {
        private SubtitlesEditor editor;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="editor">Obiekt edytora napsiów.</param>
        public MainContextMenuStripPresenter(IViewManager viewManager, IMainContextMenuStripView view, SubtitlesEditor editor)
            : base(viewManager, view)
        {
            this.editor = editor;

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            ICommandView cutCmdView = (ICommandView)ViewManager.GetView("ContextMenuCutView");
            CutCommandPresenter cutCmdPresenter = new CutCommandPresenter(ViewManager, cutCmdView, editor);

            ICommandView copyCmdView = (ICommandView)ViewManager.GetView("ContextMenuCopyView");
            CopyCommandPresenter copyCmdPresenter = new CopyCommandPresenter(ViewManager, copyCmdView, editor);

            ICommandView pasteCmdView = (ICommandView)ViewManager.GetView("ContextMenuPasteView");
            PasteCommandPresenter pasteCmdPresenter = new PasteCommandPresenter(ViewManager, pasteCmdView, editor);

            ICommandView selectAllCmdView = (ICommandView)ViewManager.GetView("ContextMenuSelectAllView");
            SelectAllCommandPresenter selectAllCmdPresenter = new SelectAllCommandPresenter(ViewManager, selectAllCmdView, editor);

            ICommandView selectFromCmdView = (ICommandView)ViewManager.GetView("ContextMenuSelectFromView");
            SelectFromCommandPresenter selectFromCmdPresenter = new SelectFromCommandPresenter(ViewManager, selectFromCmdView, editor);
        }

    }
}
