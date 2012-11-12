using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Presenters;
using KWinFramework.Views;
using Subeditor.Model;
using Subeditor.Properties;
using Subeditor.Utilities;

namespace Subeditor.Views.Dialogs.OpenFile
{
    /// <summary>
    /// Prezenter wyboru i otwarcia pliku.
    /// </summary>
    class OpenFilePresenter : PresenterBase<IOpenFileView>
    {
        private SubtitlesManager subtitlesManager;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="fileFilter">Filter obsługiwanych plików.</param>
        public OpenFilePresenter(IViewManager viewManager, IOpenFileView view, String fileFilter)
            : base(viewManager, view)
        {
            this.View.Filter = fileFilter;
            this.View.DialogClosed += new EventHandler<KWinFramework.Views.WinForms.Dialogs.DialogClosedEventArgs>(DialogClosedHandler);
        }


        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="subtitlesManager"></param>
        public OpenFilePresenter(IViewManager viewManager, IOpenFileView view, SubtitlesManager subtitlesManager)
            : base(viewManager, view)
        {
            this.subtitlesManager = subtitlesManager;

            this.View.Filter = GetFileFilters();
            this.View.DialogClosed += new EventHandler<KWinFramework.Views.WinForms.Dialogs.DialogClosedEventArgs>(DialogClosedHandler);
        }

        private String GetFileFilters()
        {
            if (subtitlesManager.SupportedSubtitlesFormats.Count() == 0)
            {
                return String.Empty;
            }

            var supportedSubtitlesFormats = subtitlesManager.SupportedSubtitlesFormats;
            var extensions = supportedSubtitlesFormats.SelectMany(format => format.CorrectExtensions).Distinct();

            FileFilterBuilder builder = new FileFilterBuilder();
            builder.AppendFileFormat(Resources.DscAllSupported, extensions);
            foreach (var supportedSubtitle in supportedSubtitlesFormats)
            {
                builder.AppendFileFormat(supportedSubtitle.Description, supportedSubtitle.CorrectExtensions);
            }

            return builder.ToString();
        }

        private void DialogClosedHandler(object sender, KWinFramework.Views.WinForms.Dialogs.DialogClosedEventArgs e)
        {
            if (e.DialogViewResult == KWinFramework.Views.WinForms.Dialogs.DialogViewResult.Ok)
            {
                subtitlesManager.Load(View.FileName);
            }
        }

    }
}
