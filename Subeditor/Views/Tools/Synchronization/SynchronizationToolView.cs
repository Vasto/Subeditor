using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KWinFramework.Views.WinForms;

namespace Subeditor.Views.Tools.Synchronization
{
    /// <summary>
    /// Widok narzędzia sycnchroznizacji timingu napsiów.
    /// </summary>
    public partial class SynchronizationToolView : FormView, ISynchronizationToolView
    {
        private bool canSynchronize;
        private bool canAdd;
        private bool canDelete;
        private bool canChange;
        private long orginalTiming;
        private long correctTiming;
        private int selectedSynchronizationPointIndex;
        private TimingType timingDisplayType;
        private FramesBox framesBoxOrginalTiming;
        private FramesBox framesBoxCorrectTiming;
        private TimeBox timeBoxOrginalTiming;
        private TimeBox timeBoxCorrectTiming;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        public SynchronizationToolView()
        {
            InitializeComponent();

            this.framesBoxOrginalTiming = new FramesBox();
            this.framesBoxOrginalTiming.FramesChanged += new EventHandler<EventArgs<int>>(TimingControlValueChangedHandler);
            this.framesBoxOrginalTiming.Dock = DockStyle.Fill;

            this.framesBoxCorrectTiming = new FramesBox();
            this.framesBoxCorrectTiming.FramesChanged += new EventHandler<EventArgs<int>>(TimingControlValueChangedHandler);
            this.framesBoxCorrectTiming.Dock = DockStyle.Fill;

            this.timeBoxOrginalTiming = new TimeBox();
            this.timeBoxOrginalTiming.TimeChanged += new EventHandler<EventArgs<TimeSpan>>(TimingControlValueChangedHandler);
            this.timeBoxOrginalTiming.Dock = DockStyle.Fill;

            this.timeBoxCorrectTiming = new TimeBox();
            this.timeBoxCorrectTiming.TimeChanged += new EventHandler<EventArgs<TimeSpan>>(TimingControlValueChangedHandler);
            this.timeBoxCorrectTiming.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// Zdarzenie mające miejsce gdy zmianie ulegnie aktualnie wybrany wpis na liście punktów synchronizacji.
        /// </summary>
        public event EventHandler<EventArgs<int>> SelectedSynchronizationPointChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy zmianie ulegnie wartość właściwości OrginalTiming.
        /// </summary>
        public event EventHandler<EventArgs<long>> OrginalTimingChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy zmianie zmianie ulegnie wartość właściwości CorrectTiming.
        /// </summary>
        public event EventHandler<EventArgs<long>> CorrectTimingChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy zostanie wciśnięty przycisk dodania wpsiu na liste timingów.
        /// </summary>
        public event EventHandler Add;

        /// <summary>
        /// Zdarzenie mające miejsce gdy zostanie wciśnięty przycisk usunięcia wpsiu z listy timingów.
        /// </summary>
        public event EventHandler Delete;

        /// <summary>
        /// Zdarzenie mające miejsce gdy zostanie wciśnięty przycisk modyfikacji wpisu z lisy timingów.
        /// </summary>
        public event EventHandler Change;

        /// <summary>
        /// Zdarzenie mające miejsce gdy zostanie wciśnięty przycisk synchronizacji.
        /// </summary>
        public event EventHandler Synchronize;

        /// <summary>
        /// Typ jednostek w jakich wyrażony jest timing.
        /// </summary>
        private enum TimingType
        {
            Frames,
            Time,
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić informację czy przycisk synchronizacji ma być włączony.
        /// </summary>
        public bool CanSynchronize
        {
            get
            {
                return canSynchronize;
            }
            set
            {
                canSynchronize = value;
                EnableControl(btnSynchronize, value);
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić informację czy przycisk Add ma być włączony.
        /// </summary>
        public bool CanAdd
        {
            get
            {
                return canAdd;
            }
            set
            {
                canAdd = value;
                EnableControl(btnAdd, value);              
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić informację czy przycisk Delete ma być włączony.
        /// </summary>
        public bool CanDelete
        {
            get
            {
                return canDelete;
            }
            set
            {
                canDelete = value;
                EnableControl(btnDelete, value);  
            }
        }


        /// <summary>
        /// Pozwala pobrać lub ustawić informację czy przycisk Change ma być włączony.
        /// </summary>
        public bool CanChange
        {
            get
            {
                return canChange;
            }
            set
            {
                canChange = value;
                EnableControl(btnChange, value);  
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić wartość pierwotnego timingu, dla punktu synchronizacji.
        /// </summary>
        public long OrginalTiming
        {
            get 
            { 
                return orginalTiming; 
            }
            set 
            { 
                orginalTiming = value;
                UpdateTimingControls(framesBoxOrginalTiming, timeBoxOrginalTiming, value);
                OnOrginalTimingChanged(value);
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić wartość poprawionego timingu, dla punktu synchronizacji.
        /// </summary>
        public long CorrectTiming
        {
            get
            {
                return correctTiming;
            }
            set
            {
                correctTiming = value;
                UpdateTimingControls(framesBoxOrginalTiming, timeBoxOrginalTiming, value);
                OnCorrectTimingChanged(value);
            }
        }

        /// <summary>
        /// Zwraca lub ustawia indeks aktualnie wybranego punktu synchronizacji z listy.
        /// </summary>
        /// <remarks>Przyjmuje wartość ujemną jeżeli żaden element nie jest zaznaczony.</remarks>
        public int SelectedSynchronizationPointIndex
        {
            get 
            {
                return selectedSynchronizationPointIndex; 
            }
            set 
            {
                selectedSynchronizationPointIndex = value;
                UpdateTimingControlSelection(value);
                OnSelectedSynchronizationPointIndexChanged(value);
            }
        }

        /// <summary>
        /// Zwraca liczbę punktów synchrozizacji na liście.
        /// </summary>
        public int SynchronizationPointsCount
        {
            get
            { 
                return lvTimings.Items.Count;
            }
        }

        /// <summary>
        /// Dodaje bieżącą wartość OrginalTiming i CorrectTiming jako wpis do listy punktów synchronizacji.
        /// </summary>
        public void AddSynchronizationPoint()
        {
            var item = new ListViewItem();
            if (timingDisplayType == TimingType.Frames)
            {
                item.Text = framesBoxOrginalTiming.Frames.ToString();
                item.SubItems.Add(framesBoxCorrectTiming.Frames.ToString());
            }
            else if (timingDisplayType == TimingType.Time)
            {
                item.Text = timeBoxOrginalTiming.DisplayTime;
                item.SubItems.Add(timeBoxCorrectTiming.DisplayTime);
            }

            lvTimings.Items.Add(item);
        }

        /// <summary>
        /// Usuwa aktualnie zaznaczony wpis z listy punktów synchronizacji.
        /// </summary>
        public void DeleteSynchronizationPoint()
        {
            lvTimings.Items.RemoveAt(selectedSynchronizationPointIndex);
        }

        /// <summary>
        /// Czyści listę punktów synchronizacji ze wszystkich wpisów.
        /// </summary>
        public void ClearSynchronizationPoints()
        {
            lvTimings.Items.Clear();
        }

        /// <summary>
        /// Zmienia wartość aktualnie zaznaczonego wpsiu na liście punktów synchronizacji,
        /// na zgodną z wartościami właściwości OrginalTiming i CorrectTiming.
        /// </summary>
        public void ChangeSynchronizationPoint()
        {
            int insertionIndex = selectedSynchronizationPointIndex;

            DeleteSynchronizationPoint();

            var item = new ListViewItem();
            if (timingDisplayType == TimingType.Frames)
            {
                item.Text = framesBoxOrginalTiming.Frames.ToString();
                item.SubItems.Add(framesBoxCorrectTiming.Frames.ToString());
            }
            else if (timingDisplayType == TimingType.Time)
            {
                item.Text = timeBoxOrginalTiming.DisplayTime;
                item.SubItems.Add(timeBoxCorrectTiming.DisplayTime);
            }

            lvTimings.Items.Insert(insertionIndex, item);    
        }

        /// <summary>
        /// Zmienia reprezentacje timingu wykorzystywane przez widok na klatkowe.
        /// </summary>
        public void SwitchTimingToFrames()
        {
            if (pnlOrginalTimingHost.Controls.Contains(timeBoxOrginalTiming))
            {
                pnlOrginalTimingHost.Controls.Remove(timeBoxOrginalTiming);
            }

            if (pnlCorrectTimingHost.Controls.Contains(timeBoxCorrectTiming))
            {
                pnlCorrectTimingHost.Controls.Remove(timeBoxCorrectTiming);
            }


            if (!pnlOrginalTimingHost.Controls.Contains(framesBoxOrginalTiming))
            {
                pnlOrginalTimingHost.Controls.Add(framesBoxOrginalTiming);
            }

            if (!pnlCorrectTimingHost.Controls.Contains(framesBoxCorrectTiming))
            {
                pnlCorrectTimingHost.Controls.Add(framesBoxCorrectTiming);
            }

            timingDisplayType = TimingType.Frames;
        }

        /// <summary>
        /// Zmienia reprezentację timingu wykorzystywane przez widok na czasowe.
        /// </summary>
        public void SwitchTimingToTime()
        {
            if (pnlOrginalTimingHost.Controls.Contains(framesBoxOrginalTiming))
            {
                pnlOrginalTimingHost.Controls.Remove(framesBoxOrginalTiming);
            }

            if (pnlCorrectTimingHost.Controls.Contains(framesBoxCorrectTiming))
            {
                pnlCorrectTimingHost.Controls.Remove(framesBoxCorrectTiming);
            }


            if (!pnlOrginalTimingHost.Controls.Contains(timeBoxOrginalTiming))
            {
                pnlOrginalTimingHost.Controls.Add(timeBoxOrginalTiming);
            }

            if (!pnlCorrectTimingHost.Controls.Contains(timeBoxCorrectTiming))
            {
                pnlCorrectTimingHost.Controls.Add(timeBoxCorrectTiming);
            }

            timingDisplayType = TimingType.Time;
        }

        private void EnableControl(Control control, bool enabled)
        {
            control.Enabled = enabled;
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

            if ((sender == framesBoxOrginalTiming) || (sender == timeBoxOrginalTiming))
            {
                orginalTiming = newValue;
                OnOrginalTimingChanged(newValue);
            }
            else if ((sender == framesBoxCorrectTiming) || (sender == timeBoxCorrectTiming))
            {
                correctTiming = newValue;
                OnCorrectTimingChanged(newValue);
            }
        }

        private void OnOrginalTimingChanged(long newValue)
        {
            var temporaryEventHolder = OrginalTimingChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<long>(newValue));
            }
        }

        private void OnCorrectTimingChanged(long newValue)
        {
            var temporaryEventHolder = CorrectTimingChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<long>(newValue));
            }
        }

        private void UpdateTimingControlSelection(int newValue)
        {
            lvTimings.SelectedIndices.Clear();
            if (newValue >= 0)
            {
                lvTimings.Items[newValue].Selected = true;
            }
        }

        private void OnSelectedSynchronizationPointIndexChanged(int newValue)
        {
            var temporaryEventHolder = SelectedSynchronizationPointChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<int>(newValue));
            }
        }

        private void btnSynchronizeClickHandler(object sender, EventArgs e)
        {
            var temporaryEventHolder = Synchronize;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs());
            }
        }

        private void btnAddClickHandler(object sender, EventArgs e)
        {
            var temporaryEventHolder = Add;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs());
            }
        }

        private void btnDeleteClickHandler(object sender, EventArgs e)
        {
            var temporaryEventHolder = Delete;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs());
            }
        }

        private void btnChangeClickHandler(object sender, EventArgs e)
        {
            var temporaryEventHolder = Change;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs());
            }
        }

        private void lvTimingsSelectedIndexChangedHandler(object sender, EventArgs e)
        {
            int newValue = -1;
            if (lvTimings.SelectedIndices.Count > 0)
            {
                newValue = lvTimings.SelectedIndices[0];
            }

            selectedSynchronizationPointIndex = newValue;
            OnSelectedSynchronizationPointIndexChanged(newValue);
        }

    }
}
