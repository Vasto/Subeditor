using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Presenters.Commands;
using KWinFramework.Views;
using KWinFramework.Views.WinForms.Commands;
using Subeditor.Model;

namespace Subeditor.Views.Commands
{
    /// <summary>
    /// Reprezentuje prezentera polecenia zaznaczenia całego tekstu.
    /// </summary>
    class SelectAllCommandPresenter : CommandPresenter
    {
        private SubtitlesEditor editor;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="editor">Obiekt menadżera zajmującego się edycjią napisów.</param>
        public SelectAllCommandPresenter(IViewManager viewManager, ICommandView view, SubtitlesEditor editor)
            : base(viewManager, view)
        {
            this.editor = editor;
        }

        /// <summary>
        /// Metoda wywoływana w momencie odpalenia polecenia.
        /// </summary>
        protected override void OnExecute()
        {
            editor.SelectAllContent();
        }
    }
}
