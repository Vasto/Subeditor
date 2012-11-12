using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Presenters.Commands;
using KWinFramework.Views;
using KWinFramework.Views.WinForms.Commands;
using Subeditor.Model;
using Subeditor.Views.Dialogs.SaveFile;

namespace Subeditor.Views.Commands
{
    /// <summary>
    /// Reprezentuje prezentera polecenia otwarcia widoku zapisu pliku.
    /// </summary>
    class SaveAsFileCommandPresenter : CommandPresenter
    {
        private SubtitlesManager subtitlesManager;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="subtitlesManager">Obiekt menadżera zarządzającego obiektami reprezentującymi napisy.</param>
        public SaveAsFileCommandPresenter(IViewManager viewManager, ICommandView view, SubtitlesManager subtitlesManager)
            : base(viewManager, view)
        {
            this.subtitlesManager = subtitlesManager;
        }

        /// <summary>
        /// Metoda wywoływana w momencie odpalenia polecenia.
        /// </summary>
        protected override void OnExecute()
        {
            ISaveFileView saveFileView = new SaveFileView();
            SaveFilePresenter saveFilePresenter = new SaveFilePresenter(ViewManager, saveFileView, subtitlesManager);

            ViewManager.AddView(saveFileView);
            ViewManager.ShowView(saveFileView);
            ViewManager.CloseView(saveFileView);
            ViewManager.RemoveView(saveFileView);
        }

    }
}
