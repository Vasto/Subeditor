using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Presenters;
using KWinFramework.Views;
using Subeditor.Model;
using Subeditor.Views.Dialogs.SaveFile;

namespace Subeditor.Views.Dialogs.SaveBeforeClose
{
    /// <summary>
    /// Prezenter widoku dialogu pytającego zapis zmian przed wyjściem.
    /// </summary>
    class SaveBeforeClosePresenter : PresenterBase<ISaveBeforeCloseView>
    {
        private SubtitlesManager subtitlesManager;
        private ViewCloseCancellationToken cancellationToken;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="subtitlesManager">Obiekt menadżera napsiów.</param>
        /// <param name="cancelatlionToken">Obiekt informacji o anulowaniu zamykania widoku.</param>
        public SaveBeforeClosePresenter(
            IViewManager viewManager, 
            ISaveBeforeCloseView view, 
            SubtitlesManager subtitlesManager,
            ViewCloseCancellationToken cancelatlionToken)
            : base(viewManager, view)
        {
            this.subtitlesManager = subtitlesManager;

            this.View.Save += new EventHandler(SaveHandler);
            this.View.DoNotSave += new EventHandler(DoNotSaveHandler);
            this.View.Cancel += new EventHandler(CancelHandler);

            this.cancellationToken = cancelatlionToken;
        }

        private void SaveHandler(object sender, EventArgs e)
        {
            cancellationToken.Cancel = false;

            //Istnieje konieczność ukrycia widoku inaczej nie zniknie on gdy ma miejsce sytuacja taka jak tu,
            //gdy zamykamy jeden blokujący widok okna a otwieramy zaraz następny.
            ViewManager.HideView(View);
   
            var subtitlesPath = subtitlesManager.CurrentSubtitles.Path;
            if (subtitlesPath != null)
            {
                subtitlesManager.Save();
            }
            else
            {
                ISaveFileView saveFileView = new SaveFileView();
                SaveFilePresenter saveFilePresenter = new SaveFilePresenter(ViewManager, saveFileView, subtitlesManager, cancellationToken);

                //Dodajemy widok zapisu do głównego widoku, żeby został wyświetlony jako widok podrzędny w stosutnku do niego.
                var mainView = (Subeditor.Views.Main.MainFormView)ViewManager.GetView(Subeditor.Properties.Resources.NameMainView);
                mainView.AddChildView(saveFileView);

                ViewManager.AddView(saveFileView);
                ViewManager.ShowView(saveFileView);
                ViewManager.CloseView(saveFileView);
                ViewManager.RemoveView(saveFileView);
            }

            ViewManager.CloseView(View);
        }

        private void DoNotSaveHandler(object sender, EventArgs e)
        {
            cancellationToken.Cancel = false;
            ViewManager.CloseView(View);
        }

        private void CancelHandler(object sender, EventArgs e)
        {
            cancellationToken.Cancel = true;
            ViewManager.CloseView(View);
        }

    }
}
