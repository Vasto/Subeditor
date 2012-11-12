using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Presenters;
using KWinFramework.Views;
using Subeditor.Model;
using Subeditor.Model.Tools;

namespace Subeditor.Views.Tools.TimingAdjusts
{
    /// <summary>
    /// Prezenter narzędzia służącego do dostosowywania timingu napisów.
    /// </summary>
    class TimingAdjustmentToolPresenter : PresenterBase<ITimingAdjustmentToolView>
    {
        private SubtitlesManager manager;
        private SubtitlesEditor editor;
        private TimingAdjustmentTool tool;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="subtitlesManager">Obiekt modelu menadżera plików.</param>
        /// <param name="editor">Edytor napisów.</param>
        /// <param name="tool">Obiekt narzędzia do dostosowywania napisów.</param>
        public TimingAdjustmentToolPresenter(
            IViewManager viewManager, 
            ITimingAdjustmentToolView view, 
            SubtitlesManager subtitlesManager, 
            SubtitlesEditor editor, 
            TimingAdjustmentTool tool) 
            : base(viewManager, view)
        {
            this.manager = subtitlesManager;
            this.manager.CurrentSubtitlesFormatChanged += new EventHandler(CurrentSubtitlesFormatChangedHandler);

            this.editor = editor;

            this.tool = tool;

            this.View.Add += new EventHandler(AddHandler);
            this.View.Substract += new EventHandler(SubstractHandler);
            this.View.ApplyToRangeChanged += new EventHandler<EventArgs<bool>>(ApplyToRangeChangedHandler);
            this.View.UseFromRangeChanged += new EventHandler<EventArgs<bool>>(UseFromRangeChangedHandler);
            this.View.UseToRangeChanged += new EventHandler<EventArgs<bool>>(UseToRangeChangedHandler);
            this.SetViewRangeControlsEnabled(false);
            this.InitializeViewApplyTo();
            this.SetViewTimingMode();
        }

        /// <summary>
        /// Pozwala prezenterowi na wykonanie działań mających na celu zakończenie jego działania.
        /// </summary>
        public override void ClosePresenter()
        {
            base.ClosePresenter();

            manager.CurrentSubtitlesFormatChanged -= new EventHandler(CurrentSubtitlesFormatChangedHandler);
        }

        private void CurrentSubtitlesFormatChangedHandler(object sender, EventArgs e)
        {
            SetViewTimingMode();
        }

        private void SetViewTimingMode()
        {
            FormatDisplayResolver formatDisplayResolver = new FormatDisplayResolver(manager.CurrentSubtitlesFormat);
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

        private void InitializeViewApplyTo()
        {
            if (editor.EditState.Selection.Length > 0)
            {
                this.View.ApplyToSelection = true;
            }
            else
            {
                this.View.ApplyToAll = true;
            }
        }

        private void AddHandler(object sender, EventArgs e)
        {
            long timingChange = View.TimingChange;
            if (View.ApplyToAll)
            {
                tool.AddTiming(timingChange);
            }
            else if (View.ApplyToSelection)
            {
                Selection selection = editor.EditState.Selection;
                tool.AddTiming(timingChange, selection.Start, selection.Length); 
            }
            else if (View.ApplyToRange)
            {
                long timingFrom = View.UseRangeFrom ? View.TimingFrom : 0;
                long timingTo = View.UseRangeTo ? View.TimingTo : long.MaxValue;
                tool.AddTiming(timingChange, timingFrom, timingTo); 
            }
        }

        private void SubstractHandler(object sender, EventArgs e)
        {
            long timingChange = View.TimingChange;
            if (View.ApplyToAll)
            {
                tool.SubstractTiming(timingChange);
            }
            else if (View.ApplyToSelection)
            {
                Selection selection = editor.EditState.Selection;
                tool.SubstractTiming(timingChange, selection.Start, selection.Length);
            }
            else if (View.ApplyToRange)
            {
                long timingFrom = View.UseRangeFrom ? View.TimingFrom : 0 ;
                long timingTo = View.UseRangeTo ? View.TimingTo : long.MaxValue;
                tool.SubstractTiming(timingChange, timingFrom, timingTo);
            }
        }

        private void ApplyToRangeChangedHandler(object sender, EventArgs<bool> e)
        {
            SetViewRangeControlsEnabled(e.Value);
        }

        private void SetViewRangeControlsEnabled(bool enabled)
        {
            View.IsRangeFromEnabled = enabled;
            View.IsRangeToEnabled = enabled;

            View.IsTimingFromEnabled = (enabled && View.UseRangeFrom);
            View.IsTimingToEnabled = (enabled && View.UseRangeTo);
        }

        private void UseFromRangeChangedHandler(object sender, EventArgs<bool> e)
        {
            View.IsTimingFromEnabled = e.Value;
        }

        private void UseToRangeChangedHandler(object sender, EventArgs<bool> e)
        {
            View.IsTimingToEnabled = e.Value;
        }

    }
}
