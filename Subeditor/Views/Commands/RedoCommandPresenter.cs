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
    /// Reprezentuje prezentera polecenia przywrócenia zmiany.
    /// </summary>
    class RedoCommandPresenter : CommandPresenter
    {
        private UndoRedoManager undoRedoManager;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="undoRedoManager">Menadżer cofania/przywracania zmian.</param>
        public RedoCommandPresenter(IViewManager viewManager, ICommandView view, UndoRedoManager undoRedoManager)
            : base(viewManager, view)
        {
            this.undoRedoManager = undoRedoManager;
            this.undoRedoManager.CanRedoChanged += new EventHandler<EventArgs<bool>>(CanRedoChangedHandler);

            this.View.IsExecutable = this.undoRedoManager.CanRedo;
        }

        /// <summary>
        /// Metoda wywoływana w momencie odpalenia polecenia.
        /// </summary>
        protected override void OnExecute()
        {
            undoRedoManager.Redo();
        }

        private void CanRedoChangedHandler(object sender, EventArgs<bool> e)
        {
            this.View.IsExecutable = e.Value;
        }
    }
}
