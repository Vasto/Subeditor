using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Presenters.Commands;
using KWinFramework.Views;
using KWinFramework.Views.WinForms.Commands;
using Subeditor.Model.StateManagement;

namespace Subeditor.Views.Commands
{
    /// <summary>
    /// Reprezentuje prezentera polecenia wycofania zmiany.
    /// </summary>
    class UndoCommandPresenter : CommandPresenter
    {
        private IUndoRedoManager undoRedoManager;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="undoRedoManager">Menadżer cofania/przywracania zmian.</param>
        public UndoCommandPresenter(IViewManager viewManager, ICommandView view, IUndoRedoManager undoRedoManager)
            : base(viewManager, view)
        {
            this.undoRedoManager = undoRedoManager;
            this.undoRedoManager.CanUndoChanged += new EventHandler<EventArgs<bool>>(CanUndoChangedHandler);

            this.View.IsExecutable = this.undoRedoManager.CanUndo;
        }

        /// <summary>
        /// Metoda wywoływana w momencie odpalenia polecenia.
        /// </summary>
        protected override void OnExecute()
        {
            undoRedoManager.Undo();
        }

        private void CanUndoChangedHandler(object sender, EventArgs<bool> e)
        {
            this.View.IsExecutable = e.Value;
        }
    }
}
