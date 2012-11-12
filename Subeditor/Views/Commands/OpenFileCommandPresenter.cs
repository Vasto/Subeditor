using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Presenters.Commands;
using KWinFramework.Views;
using KWinFramework.Views.WinForms.Commands;
using Subeditor.Model;
using Subeditor.Views.Dialogs.OpenFile;

namespace Subeditor.Views.Commands
{
    /// <summary>
    /// Reprezentuje prezentera polecenia otwarcia widoku wyboru pliku do wczytania.
    /// </summary>
    class OpenFileCommandPresenter : CommandPresenter
    {
        private SubtitlesManager subtitlesManager;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="subtitlesManager">Obiekt menadżera zarządzającego obiektami reprezentującymi napisy.</param>
        public OpenFileCommandPresenter(IViewManager viewManager, ICommandView view, SubtitlesManager subtitlesManager)
            : base(viewManager, view)
        {
            this.subtitlesManager = subtitlesManager;
        }

        /// <summary>
        /// Metoda wywoływana w momencie odpalenia polecenia.
        /// </summary>
        protected override void OnExecute()
        {
            IOpenFileView openFileView = new OpenFileView();
            OpenFilePresenter openFilePresenter = new OpenFilePresenter(ViewManager, openFileView, subtitlesManager);

            ViewManager.AddView(openFileView);
            ViewManager.ShowView(openFileView);
            ViewManager.CloseView(openFileView);
            ViewManager.RemoveView(openFileView);
        }

    }
}
