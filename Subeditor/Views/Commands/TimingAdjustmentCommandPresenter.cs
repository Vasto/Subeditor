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
using Subeditor.Views.Tools.TimingAdjusts;

namespace Subeditor.Views.Commands
{
    /// <summary>
    /// Prezenter polecenia uruchamiającego narzędzie dostosowywania timingu.
    /// </summary>
    class TimingAdjustmentCommandPresenter : CommandPresenter
    {
        private SubtitlesManager manager;
        private SubtitlesEditor editor;
        private TimingAdjustmentTool tool;
        private ITimingAdjustmentToolView toolView;
        private TimingAdjustmentToolPresenter presenter;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="subtitlesManager">Obiekt menadżera zarządzającego obiektami reprezentującymi napisy.</param>
        /// <param name="editor">Obiekt menadżera zajmującego się edycjią napisów.</param>
        /// <param name="tool">OBiekt narzędzia służącego do dostosowywania timingów.</param>
        public TimingAdjustmentCommandPresenter(
            IViewManager viewManager, 
            ICommandView view, 
            SubtitlesManager subtitlesManager, 
            SubtitlesEditor editor, 
            TimingAdjustmentTool tool) 
            : base(viewManager, view)
        {
            this.manager = subtitlesManager;

            this.editor = editor;

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


                toolView = new TimingAdjustmentToolView();
                presenter = new TimingAdjustmentToolPresenter(ViewManager, toolView, manager, editor, tool);

                mainView.AddChildView((IHierarchicalView)toolView);

                ViewManager.AddView(toolView);
                ViewManager.ShowView(toolView);
            }
        }

    }
}
