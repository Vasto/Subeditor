using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Presenters;
using KWinFramework.Views;
using Subeditor.Model;
using Subeditor.Model.Tools;
using Subeditor.Properties;
using Subeditor.Views.Dialogs.InfoBox;

namespace Subeditor.Views.Tools.Synchronization
{
    /// <summary>
    /// Prezenter narzędzia sycnchroznizacji timingu napsiów.
    /// </summary>
    class SynchronizationToolPresenter : PresenterBase<ISynchronizationToolView>
    {
        private SubtitlesManager subtitlesManager;
        private SynchronizationTool tool;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="subtitlesManager">Obiekt modelu menadżera plików.</param>
        /// <param name="tool">Obiekt narzędzia do synchronizacji napisów.</param>
        public SynchronizationToolPresenter(IViewManager viewManager, ISynchronizationToolView view, SubtitlesManager subtitlesManager, SynchronizationTool tool)
            : base(viewManager, view)
        {
            this.subtitlesManager = subtitlesManager;
            this.subtitlesManager.CurrentSubtitlesFormatChanged += new EventHandler(CurrentSubtitlesFormatChangedHandler);

            this.tool = tool;

            this.View.CanSynchronize = false;
            this.View.CanAdd = true;
            this.View.CanDelete = false;
            this.View.CanChange = false;
            this.View.Synchronize += new EventHandler(SynchronizeHandler);
            this.View.Add += new EventHandler(AddHandler);
            this.View.Delete += new EventHandler(DeleteHandler);
            this.View.Change += new EventHandler(ChangeHandler);
            this.View.SelectedSynchronizationPointChanged += new EventHandler<EventArgs<int>>(SelectedTimingChangedHandler);
            this.View.SwitchTimingToTime();
            this.SetViewTimingMode();
        }

        /// <summary>
        /// Pozwala prezenterowi na wykonanie działań mających na celu zakończenie jego działania.
        /// </summary>
        public override void ClosePresenter()
        {
            base.ClosePresenter();

            subtitlesManager.CurrentSubtitlesFormatChanged -= new EventHandler(CurrentSubtitlesFormatChangedHandler);
        }

        private void CurrentSubtitlesFormatChangedHandler(object sender, EventArgs e)
        {
            View.ClearSynchronizationPoints();
            UnselectAllPoints();
            SetViewTimingMode();
        }

        private void SetViewTimingMode()
        {
            FormatDisplayResolver formatDisplayResolver = new FormatDisplayResolver(subtitlesManager.CurrentSubtitlesFormat);
            FormatDisplayResolver.DisplayMode displayMode = formatDisplayResolver.Resolve();

            if (displayMode == FormatDisplayResolver.DisplayMode.Time)
            {
                View.SwitchTimingToTime();
            }
            else if (displayMode == FormatDisplayResolver.DisplayMode.Frames)
            {
                View.SwitchTimingToFrames();
            }
        }

        private void SynchronizeHandler(object sender, EventArgs e)
        {
            tool.Synchronize();
        }

        private void AddHandler(object sender, EventArgs e)
        {
            SynchronizationPoint syncPoint = new SynchronizationPoint(View.OrginalTiming, View.CorrectTiming);
            SynchronizationPointsValidator syncPointsValidatior = new SynchronizationPointsValidator();
            if (syncPointsValidatior.CanAddPoint(syncPoint, tool.SynchronizationPoints))
            {
                tool.SynchronizationPoints.Add(syncPoint);

                View.AddSynchronizationPoint();

                SelectLastPoint();

                UpdateViewCanSynchronize();
            }
            else
            {
                IInfoBoxView infoBoxView = new InfoBoxView();
                InfoBoxPresenter infoBoxPresenter = new InfoBoxPresenter(
                    ViewManager, 
                    infoBoxView, 
                    Resources.MsgSyncPointOrginalTimingWrong,
                    System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, Resources.PathAlertImage));

                //Dodajemy widok do głównego widoku, żeby został wyświetlony jako widok podrzędny w stosutnku do niego.
                var mainView = (Subeditor.Views.Main.MainFormView)ViewManager.GetView(Resources.NameMainView);
                mainView.AddChildView((IHierarchicalView)infoBoxView);

                ViewManager.AddView(infoBoxView);
                ViewManager.ShowView(infoBoxView);
                ViewManager.RemoveView(infoBoxView);
            }
        }

        private void DeleteHandler(object sender, EventArgs e)
        {
            int selectedIndex = View.SelectedSynchronizationPointIndex;
            tool.SynchronizationPoints.RemoveAt(selectedIndex);

            View.DeleteSynchronizationPoint();

            SelectLastPoint();

            UpdateViewCanSynchronize();   
        }

        private void ChangeHandler(object sender, EventArgs e)
        {
            int selectedIndex = View.SelectedSynchronizationPointIndex;
            tool.SynchronizationPoints.RemoveAt(selectedIndex);
            SynchronizationPoint syncPoint = new SynchronizationPoint(View.OrginalTiming, View.CorrectTiming);
            tool.SynchronizationPoints.Insert(selectedIndex, syncPoint);

            View.ChangeSynchronizationPoint();
            View.SelectedSynchronizationPointIndex = selectedIndex;
        }

        private void SelectLastPoint()
        {
            int selectedIndex = View.SynchronizationPointsCount - 1;
            View.SelectedSynchronizationPointIndex = selectedIndex;
        }

        private void UnselectAllPoints()
        {
            View.SelectedSynchronizationPointIndex = -1;
        }

        private void UpdateViewCanSynchronize()
        {
            if (View.SynchronizationPointsCount >= SynchronizationTool.MinimumPointsToSynchronize)
            {
                View.CanSynchronize = true;
            }
            else
            {
                View.CanSynchronize = false;
            }
        }

        private void SelectedTimingChangedHandler(object sender, EventArgs<int> e)
        {
            if (e.Value >= 0)
            {
                View.CanDelete = true;
                View.CanChange = true;
            }
            else
            {
                View.CanDelete = false;
                View.CanChange = false;
            }
        }

    }
}
