using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KWinFramework.Views.WinForms;

namespace Subeditor.Views.Tools.TimingAdjusts
{
    /// <summary>
    /// Widok narzędzia służącego do dostosowywania timingu napisów.
    /// </summary>
    public partial class TimingAdjustmentToolView : FormView, ITimingAdjustmentToolView
    {
        private long timingChange;
        private long timingFrom;
        private long timingTo;
        private bool applyToSelection;
        private bool applyToAll;
        private bool applyToRange;
        private bool useRangeFrom;
        private bool useRangeTo;
        private TimingType timingDisplayType;
        private FramesBox framesBoxTimingChange;
        private FramesBox framesBoxTimingFrom;
        private FramesBox framesBoxTimingTo;
        private TimeBox timeBoxTimingChange;
        private TimeBox timeBoxTimingFrom;
        private TimeBox timeBoxTimingTo;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        public TimingAdjustmentToolView()
        {
            InitializeComponent();

            this.framesBoxTimingChange = new FramesBox();
            this.framesBoxTimingChange.FramesChanged += new EventHandler<EventArgs<int>>(TimingControlValueChangedHandler);
            this.framesBoxTimingChange.Dock = DockStyle.Fill;

            this.framesBoxTimingFrom = new FramesBox();
            this.framesBoxTimingFrom.FramesChanged += new EventHandler<EventArgs<int>>(TimingControlValueChangedHandler);
            this.framesBoxTimingFrom.Dock = DockStyle.Fill;

            this.framesBoxTimingTo = new FramesBox();
            this.framesBoxTimingTo.FramesChanged += new EventHandler<EventArgs<int>>(TimingControlValueChangedHandler);
            this.framesBoxTimingTo.Dock = DockStyle.Fill;

            this.timeBoxTimingChange = new TimeBox();
            this.timeBoxTimingChange.TimeChanged += new EventHandler<EventArgs<TimeSpan>>(TimingControlValueChangedHandler);
            this.timeBoxTimingChange.Dock = DockStyle.Fill;

            this.timeBoxTimingFrom = new TimeBox();
            this.timeBoxTimingFrom.TimeChanged += new EventHandler<EventArgs<TimeSpan>>(TimingControlValueChangedHandler);
            this.timeBoxTimingFrom.Dock = DockStyle.Fill;

            this.timeBoxTimingTo = new TimeBox();
            this.timeBoxTimingTo.TimeChanged += new EventHandler<EventArgs<TimeSpan>>(TimingControlValueChangedHandler);
            this.timeBoxTimingTo.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// Zdarzenie zmiany wartości właściwości TimingChange.
        /// </summary>
        public event EventHandler<EventArgs<long>> TimingChangeChanged;

        /// <summary>
        /// Zdarzenie zmiany wartości właściwości TimingFrom.
        /// </summary>
        public event EventHandler<EventArgs<long>> TimingFromChanged;

        /// <summary>
        /// Zdarzenie zmiany wartości właściwości TimingTo.
        /// </summary>
        public event EventHandler<EventArgs<long>> TimingToChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy wartość właściwości ApplyToSelection ulegnie zmianie.
        /// </summary>
        public event EventHandler<EventArgs<bool>> ApplyToSelectionChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy wartość właściwości ApplyToAll ulegnie zmianie.
        /// </summary>
        public event EventHandler<EventArgs<bool>> ApplyToAllChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy wartość właściwości ApplyToRange ulegnie zmianie.
        /// </summary>
        public event EventHandler<EventArgs<bool>> ApplyToRangeChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy wartość właściwości UseFromRange ulegnie zmianie.
        /// </summary>
        public event EventHandler<EventArgs<bool>> UseFromRangeChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy wartość właściwości UseToRange ulegnie zmianie.
        /// </summary>
        public event EventHandler<EventArgs<bool>> UseToRangeChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy zostanie wciśnięty przycisk dodawania timingu.
        /// </summary>
        public event EventHandler Add;

        /// <summary>
        /// Zdarzenie mające miejsce gdy zostanie wciśnięty przycisk odejmowania timingu.
        /// </summary>
        public event EventHandler Substract;

        /// <summary>
        /// Typ jednostek w jakich wyrażony jest timing.
        /// </summary>
        private enum TimingType
        {
            Frames,
            Time,
        }
        
        /// <summary>
        /// Pozwala pobrać lub ustawić wartość zmiany timingu.
        /// </summary>
        public long TimingChange
        {
            get
            {
                return timingChange;
            }
            set
            {
                timingChange = value;
                UpdateTimingControls(framesBoxTimingChange, timeBoxTimingChange, value);
                OnTimingChangeChanged(value);
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić wartość od jakiej ma zostać zmieniony timing.
        /// </summary>
        public long TimingFrom
        {
            get
            {
                return timingFrom;
            }
            set
            {
                if (useRangeFrom)
                {
                    return;
                }

                timingFrom = value;
                UpdateTimingControls(framesBoxTimingFrom, timeBoxTimingFrom, value);
                OnTimingFromChanged(value);
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić wartość, do której ma zostać zmieniony timing.
        /// </summary>
        public long TimingTo
        {
            get
            {
                return timingTo;
            }
            set
            {
                if (useRangeTo)
                {
                    return;
                }

                timingTo = value;
                UpdateTimingControls(framesBoxTimingTo, timeBoxTimingTo, value);
                OnTimingToChanged(value);
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić informacje czy zmiana timingu ma zostać zastosowana do całości.
        /// </summary>
        public bool ApplyToAll
        {
            get
            {
                return applyToAll;
            }
            set
            {
                applyToAll = value;
                UpdateApplyToAllControl(value);
                OnApplyToAllChanged(value);
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić informacje czy zmiana timingu ma zostać zastosowana do zaznaczenia.
        /// </summary>
        public bool ApplyToSelection
        {
            get
            {
                return applyToSelection;
            }
            set
            {
                applyToSelection = value;
                UpdateApplyToSelectionControl(value);
                OnApplyToSelectionChanged(value);
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić informacje czy zmiana timingu ma zostać zastosowana do wskazanego zakresu.
        /// </summary>
        public bool ApplyToRange
        {
            get
            {
                return applyToRange;
            }
            set
            {
                applyToRange = value;
                UpdateApplyToRangeControl(value);
                OnApplyToRangeChanged(value);
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić informacje czy zakres ma określoną dolną wartość.
        /// </summary>
        public bool UseRangeFrom
        {
            get
            {
                return useRangeFrom;
            }
            set
            {
                if (!cbFrom.Enabled)
                {
                    return;
                }

                useRangeFrom = value;
                UpdateUseRangeFromControl(value);
                OnUseRangeFromChanged(value);    
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić informacje czy zakres ma określoną górną wartość. 
        /// </summary>
        public bool UseRangeTo
        {
            get
            {
                return useRangeTo;
            }
            set
            {
                if (!cbTo.Enabled)
                {
                    return;
                }

                useRangeTo = value;
                UpdateUseRangeToControl(value);
                OnUseRangeToChanged(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRangeFromEnabled
        {
            get 
            {
                return cbFrom.Enabled;
            }
            set
            {
                cbFrom.Enabled = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRangeToEnabled
        {
            get
            {
                return cbTo.Enabled;
            }
            set
            {
                cbTo.Enabled = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsTimingFromEnabled
        {
            get
            {
                return pnlTimingFromHost.Enabled;
            }
            set
            {
                pnlTimingFromHost.Enabled = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsTimingToEnabled
        {
            get
            {
                return pnlTimingToHost.Enabled;
            }
            set
            {
                pnlTimingToHost.Enabled = value;
            }
        }

        /// <summary>
        /// Zmienia reprezentacje timingu na klatkową.
        /// </summary>
        public void SwitchTimingToFrames()
        {
            if (pnlTimingChangeHost.Controls.Contains(timeBoxTimingChange))
            {
                pnlTimingChangeHost.Controls.Remove(timeBoxTimingChange);
            }

            if (pnlTimingFromHost.Controls.Contains(timeBoxTimingFrom))
            {
                pnlTimingFromHost.Controls.Remove(timeBoxTimingFrom);
            }

            if (pnlTimingToHost.Controls.Contains(timeBoxTimingTo))
            {
                pnlTimingToHost.Controls.Remove(timeBoxTimingTo);
            }


            if (!pnlTimingChangeHost.Controls.Contains(framesBoxTimingChange))
            {
                pnlTimingChangeHost.Controls.Add(framesBoxTimingChange);
            }

            if (!pnlTimingFromHost.Controls.Contains(framesBoxTimingFrom))
            {
                pnlTimingFromHost.Controls.Add(framesBoxTimingFrom);
            }

            if (!pnlTimingToHost.Controls.Contains(framesBoxTimingTo))
            {
                pnlTimingToHost.Controls.Add(framesBoxTimingTo);
            }

            timingDisplayType = TimingType.Frames;
        }

        /// <summary>
        /// Zmienia reprezentację timingu na czasową.
        /// </summary>
        public void SwitchTimingToTime()
        {
            if (pnlTimingChangeHost.Controls.Contains(framesBoxTimingChange))
            {
                pnlTimingChangeHost.Controls.Remove(framesBoxTimingChange);
            }

            if (pnlTimingFromHost.Controls.Contains(framesBoxTimingFrom))
            {
                pnlTimingFromHost.Controls.Remove(framesBoxTimingFrom);
            }

            if (pnlTimingToHost.Controls.Contains(framesBoxTimingTo))
            {
                pnlTimingToHost.Controls.Remove(framesBoxTimingTo);
            }


            if (!pnlTimingChangeHost.Controls.Contains(timeBoxTimingChange))
            {
                pnlTimingChangeHost.Controls.Add(timeBoxTimingChange);
            }

            if (!pnlTimingFromHost.Controls.Contains(timeBoxTimingFrom))
            {
                pnlTimingFromHost.Controls.Add(timeBoxTimingFrom);
            }

            if (!pnlTimingToHost.Controls.Contains(timeBoxTimingTo))
            {
                pnlTimingToHost.Controls.Add(timeBoxTimingTo);
            }

            timingDisplayType = TimingType.Time;
        }

        private void OnTimingChangeChanged(long newValue)
        {
            var temporaryEventHolder = TimingChangeChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<long>(newValue));
            }
        }

        private void OnTimingFromChanged(long newValue)
        {
            var temporaryEventHolder = TimingFromChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<long>(newValue));
            }
        }

        private void OnTimingToChanged(long newValue)
        {
            var temporaryEventHolder = TimingToChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<long>(newValue));
            }
        }

        private void UpdateTimingControls(FramesBox framesBoxControl, TimeBox timeBoxControl, long newValue)
        {
            if (timingDisplayType == TimingType.Frames)
            {
                framesBoxControl.FramesChanged -= new EventHandler<EventArgs<int>>(TimingControlValueChangedHandler);
                framesBoxControl.Frames = (int)newValue;
                framesBoxControl.FramesChanged += new EventHandler<EventArgs<int>>(TimingControlValueChangedHandler);
            }
            else if (timingDisplayType == TimingType.Time)
            {
                timeBoxControl.TimeChanged -= new EventHandler<EventArgs<TimeSpan>>(TimingControlValueChangedHandler);
                timeBoxControl.Time = TimeSpan.FromTicks(newValue);
                timeBoxControl.TimeChanged += new EventHandler<EventArgs<TimeSpan>>(TimingControlValueChangedHandler);
            }
        }

        private void TimingControlValueChangedHandler(object sender, EventArgs e)
        {
            long newValue = 0;
            if (e is EventArgs<int>)
            {
                newValue = (e as EventArgs<int>).Value;
            }
            else if (e is EventArgs<TimeSpan>)
            {
                newValue = (e as EventArgs<TimeSpan>).Value.Ticks;
            }


            if ((sender == framesBoxTimingChange) || (sender == timeBoxTimingChange))
            {
                timingChange = newValue;
                OnTimingChangeChanged(newValue);
            }
            else if ((sender == framesBoxTimingFrom) || (sender == timeBoxTimingFrom))
            {
                timingFrom = newValue;
                OnTimingFromChanged(newValue);
            }
            else if ((sender == framesBoxTimingTo) || (sender == timeBoxTimingTo))
            {
                timingTo = newValue;
                OnTimingToChanged(newValue);
            }
        }

        private void OnApplyToAllChanged(bool newValue)
        {
            var temporaryEventHolder = ApplyToAllChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<bool>(newValue));
            }
        }

        private void UpdateApplyToAllControl(bool newValue)
        {
            rbAll.CheckedChanged -= new EventHandler(rbAllCheckedChangedHandler);
            rbAll.Checked = newValue;
            rbAll.CheckedChanged += new EventHandler(rbAllCheckedChangedHandler);
        }

        private void OnApplyToSelectionChanged(bool newValue)
        {
            var temporaryEventHolder = ApplyToSelectionChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<bool>(newValue));
            }
        }

        private void UpdateApplyToSelectionControl(bool newValue)
        {
            rbSelection.CheckedChanged -= new EventHandler(rbSelectionCheckedChangedHandler);
            rbSelection.Checked = newValue;
            rbSelection.CheckedChanged += new EventHandler(rbSelectionCheckedChangedHandler);
        }

        private void OnApplyToRangeChanged(bool newValue)
        {
            var temporaryEventHolder = ApplyToRangeChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<bool>(newValue));
            }
        }

        private void UpdateApplyToRangeControl(bool newValue)
        {
            rbRange.CheckedChanged -= new EventHandler(rbRangeCheckedChangedHandler);
            rbRange.Checked = newValue;
            rbRange.CheckedChanged += new EventHandler(rbRangeCheckedChangedHandler);
        }

        private void OnUseRangeFromChanged(bool newValue)
        {
            var temporaryEventHolder = UseFromRangeChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<bool>(newValue));
            }
        }

        private void UpdateUseRangeFromControl(bool newValue)
        {
            cbFrom.CheckedChanged -= new EventHandler(cbFromCheckedChangedHandler);
            cbFrom.Checked = newValue;
            cbFrom.CheckedChanged += new EventHandler(cbFromCheckedChangedHandler);
        }

        private void OnUseRangeToChanged(bool newValue)
        {
            var temporaryEventHolder = UseToRangeChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<bool>(newValue));
            }
        }

        private void UpdateUseRangeToControl(bool newValue)
        {
            cbTo.CheckedChanged -= new EventHandler(cbToCheckedChangedHandler);
            cbTo.Checked = newValue;
            cbTo.CheckedChanged += new EventHandler(cbToCheckedChangedHandler);
        }

        private void rbAllCheckedChangedHandler(object sender, EventArgs e)
        {
            applyToAll = rbAll.Checked;
            OnApplyToAllChanged(rbAll.Checked);
        }

        private void rbSelectionCheckedChangedHandler(object sender, EventArgs e)
        {
            applyToSelection = rbSelection.Checked;
            OnApplyToSelectionChanged(rbSelection.Checked);
        }

        private void rbRangeCheckedChangedHandler(object sender, EventArgs e)
        {
            applyToRange = rbRange.Checked;
            OnApplyToRangeChanged(rbRange.Checked);
        }

        private void btnAddClickHandler(object sender, EventArgs e)
        {
            var temporaryEventHolder = Add;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs());
            }
        }

        private void btnSubstractClickHandler(object sender, EventArgs e)
        {
            var temporaryEventHolder = Substract;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs());
            }
        }

        private void cbFromCheckedChangedHandler(object sender, EventArgs e)
        {
            bool newValue = cbFrom.Checked;
            useRangeFrom = newValue;
            OnUseRangeFromChanged(newValue);
        }

        private void cbToCheckedChangedHandler(object sender, EventArgs e)
        {
            bool newValue = cbTo.Checked;
            useRangeTo = newValue;
            OnUseRangeToChanged(newValue);
        }

    }
}
