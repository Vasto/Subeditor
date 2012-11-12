using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Presenters;
using KWinFramework.Views;
using Subeditor.Model;
using Subeditor.Model.FileFormats;
using Subeditor.Utilities;

namespace Subeditor.Views.Dialogs.SaveFile
{
    /// <summary>
    /// Prezenter zapisu pliku.
    /// </summary>
    class SaveFilePresenter : PresenterBase<ISaveFileView>
    {
        private SubtitlesManager subtitlesManager;
        private ViewCloseCancellationToken cancellationToken;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="subtitlesManager">Obiekt menadżera zarządzającego obiektami reprezentującymi napisy.</param>
        public SaveFilePresenter(IViewManager viewManager, ISaveFileView view, SubtitlesManager subtitlesManager)
            : base(viewManager, view)
        {
            this.subtitlesManager = subtitlesManager;

            this.View.Filter = GetFileFilters();
            this.View.DialogClosed += new EventHandler<KWinFramework.Views.WinForms.Dialogs.DialogClosedEventArgs>(DialogClosedHandler);
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="subtitlesManager">Obiekt menadżera zarządzającego obiektami reprezentującymi napisy.</param>
        /// <param name="cancellationToken">
        /// Obiekt przechowujący informację o anulowaniu zamykania widoku. 
        /// Wykorzystywany jeśli prezenter zapisu jest używany przy pytaniu o zapisanie zmian przed zamknięciem aplikacji, 
        /// by móc anulować zmykanie w przypadku anuloawania zapisu
        /// </param>
        public SaveFilePresenter(IViewManager viewManager, ISaveFileView view, SubtitlesManager subtitlesManager, ViewCloseCancellationToken cancellationToken)
            : base(viewManager, view)
        {
            this.subtitlesManager = subtitlesManager;

            this.View.Filter = GetFileFilters();
            this.View.DialogClosed += new EventHandler<KWinFramework.Views.WinForms.Dialogs.DialogClosedEventArgs>(DialogClosedHandler);

            this.cancellationToken = cancellationToken;
        }

        private String GetFileFilters()
        {
            FileFilterBuilder builder = new FileFilterBuilder();

            IEnumerable<SubtitlesFileFormat> supportedFormats = CreateCurrentFormatFirstSubtitlesFormatsCollection();
            foreach (var supportedformat in supportedFormats)
            {
                builder.AppendFileFormat(supportedformat.Description, supportedformat.CorrectExtensions);
            }

            return builder.ToString();
        }

        private IEnumerable<SubtitlesFileFormat> CreateCurrentFormatFirstSubtitlesFormatsCollection()
        {
            IList<SubtitlesFileFormat> supportedFormats = new List<SubtitlesFileFormat>(subtitlesManager.SupportedSubtitlesFormats);
            bool isRemoveSuccessful = supportedFormats.Remove(subtitlesManager.CurrentSubtitlesFormat);
            if (isRemoveSuccessful)
            {
                supportedFormats.Insert(0, subtitlesManager.CurrentSubtitlesFormat);
            }

            return supportedFormats;
        }

        private void DialogClosedHandler(object sender, KWinFramework.Views.WinForms.Dialogs.DialogClosedEventArgs e)
        {
            if (e.DialogViewResult == KWinFramework.Views.WinForms.Dialogs.DialogViewResult.Ok)
            {
                subtitlesManager.CurrentSubtitles.Path = View.FileName;
                subtitlesManager.Save();
            }
            else if (e.DialogViewResult == KWinFramework.Views.WinForms.Dialogs.DialogViewResult.Cancel)
            {
                if (cancellationToken != null)
                {
                    cancellationToken.Cancel = true;
                }
            }
        }

    }
}
