using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Presenters;
using KWinFramework.Views;
using Subeditor.Model;
using Subeditor.Properties;
using Subeditor.Views.Dialogs.SaveBeforeClose;

namespace Subeditor.Views.Main
{
    /// <summary>
    /// Prezenter formy głównej aplikacji.
    /// </summary>
    class MainFormPresenter : PresenterBase<IMainFormView>
    {
        private ApplicationManager applicationManager;
        private SubtitlesManager subtitlesManager;
        private SubtitlesFile currentSubtitles;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="appManager">Obiekt menadżera aplikacji.</param>
        public MainFormPresenter(IViewManager viewManager, IMainFormView view, ApplicationManager appManager) 
            : base(viewManager, view)
        {
            this.applicationManager = appManager;

            this.View.ShowRequest += new EventHandler(ViewShowRequestHandler);
            this.View.CloseRequest += new EventHandler(ViewCloseRequestHandler);
            this.View.PreCloseRequest += new EventHandler<ViewPreCloseEventArgs>(ViewPreCloseRequestHandler);
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="appManager">Obiekt menadżera aplikacji.</param>
        /// <param name="subtitlesManager">Menadżer obiektów reprezentujących napisy.</param>
        public MainFormPresenter(IViewManager viewManager, IMainFormView view, ApplicationManager appManager, SubtitlesManager subtitlesManager)
            : base(viewManager, view)
        {
            this.applicationManager = appManager;

            this.subtitlesManager = subtitlesManager;
            this.subtitlesManager.CurrentSubtitlesChanged += new EventHandler<SubtitlesChangedEventArgs>(CurrentSubtitlesChangedHandler);

            this.currentSubtitles = subtitlesManager.CurrentSubtitles;
            this.currentSubtitles.NameChanged += new EventHandler<EventArgs<String>>(CurrentSubtitlesNameChangedHandler);

            this.View.ShowRequest += new EventHandler(ViewShowRequestHandler);
            this.View.CloseRequest += new EventHandler(ViewCloseRequestHandler);
            this.View.PreCloseRequest += new EventHandler<ViewPreCloseEventArgs>(ViewPreCloseRequestHandler);
        }

        private void CurrentSubtitlesChangedHandler(object sender, SubtitlesChangedEventArgs e)
        {
            e.OldSubtitles.NameChanged -= new EventHandler<EventArgs<String>>(CurrentSubtitlesNameChangedHandler);
            e.NewSubtitles.NameChanged += new EventHandler<EventArgs<String>>(CurrentSubtitlesNameChangedHandler);

            currentSubtitles = e.NewSubtitles;

            View.Caption = CreateCaption(currentSubtitles.Name);
        }

        private void CurrentSubtitlesNameChangedHandler(object sender, EventArgs<String> e)
        {
            View.Caption = CreateCaption(currentSubtitles.Name);
        }

        private void ViewShowRequestHandler(object sender, EventArgs e)
        {
            applicationManager.StartApplication();
        }

        private void ViewCloseRequestHandler(object sender, EventArgs e)
        {

        }

        private void ViewPreCloseRequestHandler(object sender, ViewPreCloseEventArgs e)
        {
            var currentSubtitles = subtitlesManager.CurrentSubtitles;
            if ((!currentSubtitles.IsSaved) && (!String.IsNullOrEmpty(currentSubtitles.Content)))
            {
                ViewCloseCancellationToken mainViewCloseCancellationToken = new ViewCloseCancellationToken(e.CancelViewClose);

                ISaveBeforeCloseView view = new SaveBeforeCloseView();
                SaveBeforeClosePresenter presenter = new SaveBeforeClosePresenter(ViewManager, view, subtitlesManager, mainViewCloseCancellationToken);

                ViewManager.AddView(view);
                ViewManager.ShowView(view);
                //Zamyka się sam.
                ViewManager.RemoveView(view);

                e.CancelViewClose = mainViewCloseCancellationToken.Cancel;
            }
        }

        private String CreateCaption(String fileName)
        {
            return String.Format("{0} - {1}", fileName, Resources.NameApplication);
        }
    }
}
