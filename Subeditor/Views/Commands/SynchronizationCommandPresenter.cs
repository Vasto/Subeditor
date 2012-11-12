using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Presenters.Commands;
using KWinFramework.Views;
using KWinFramework.Views.WinForms.Commands;
using Subeditor.Model;
using Subeditor.Model.Tools;
using Subeditor.Views.Main;
using Subeditor.Views.Tools.Synchronization;

namespace Subeditor.Views.Commands
{
    /// <summary>
    /// Prezenter polecenia uruchamiającego narzędzie synchronizacji.
    /// </summary>
    class SynchronizationCommandPresenter : CommandPresenter
    {
        private SubtitlesManager subtitlesManager;
        private SynchronizationTool tool;
        private ISynchronizationToolView toolView;
        private SynchronizationToolPresenter presenter;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="subtitlesManager">Obiekt menadżera zarządzającego obiektami reprezentującymi napisy.</param>
        /// <param name="editor">Obiekt menadżera zajmującego się edycjią napisów.</param>
        /// <param name="tool">Obiekt narzędzia odpowiadającego za synchronizację napisów.</param>
        public SynchronizationCommandPresenter(IViewManager viewManager, ICommandView view, SubtitlesManager subtitlesManager, SynchronizationTool tool)
            : base(viewManager, view)
        {
            this.subtitlesManager = subtitlesManager;

            this.tool = tool;

            this.View.IsExecutable = true;
        }

        /// <summary>
        /// Metoda wywoływana w momencie odpalenia polecenia.
        /// </summary>
        protected override void OnExecute()
        {
            bool isToolShown = (toolView != null) ? ViewManager.IsViewShown(toolView) : false;
            if (!isToolShown)
            {
                var mainView = (MainFormView)ViewManager.GetView(Subeditor.Properties.Resources.NameMainView);

                if (toolView != null)
                {
                    ViewManager.CloseView(toolView);
                    ViewManager.RemoveView(toolView);

                    mainView.RemoveChildView((IHierarchicalView)toolView);
                }

                if (presenter != null)
                {
                    presenter.ClosePresenter();
                }

                toolView = new SynchronizationToolView();
                presenter = new SynchronizationToolPresenter(ViewManager, toolView, subtitlesManager, tool);

                mainView.AddChildView((IHierarchicalView)toolView);

                ViewManager.AddView(toolView);
                ViewManager.ShowView(toolView); 
            }
        }

    }
}
