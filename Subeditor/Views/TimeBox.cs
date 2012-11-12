using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Subeditor.Views
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TimeBox : UserControl
    {
        private TimeSpan time;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        public TimeBox()
        {
            InitializeComponent();

            int hours;
            TryGetValue(txtHours, out hours);

            int minutes;
            TryGetValue(txtMinutes, out minutes);

            int seconds;
            TryGetValue(txtSeconds, out seconds);

            int milliseconds;
            TryGetValue(txtMilliseconds, out milliseconds);

            time = new TimeSpan(0, hours, minutes, seconds, milliseconds);
        }

        public event EventHandler<EventArgs<TimeSpan>> TimeChanged;

        public TimeSpan Time
        {
            get 
            { 
                return time; 
            }
            set 
            { 
                time = value;

                SwitchTextChangedHandling(false);
                UpdateTimeBoxControl();
                SwitchTextChangedHandling(true);

                OnTimeChanged(value);
            }
        }

        public String DisplayTime
        {
            get { return time.ToString("hh\\:mm\\:ss\\.fff"); }
        }

        private void SwitchTextChangedHandling(bool isHandling)
        {
            if (isHandling)
            {
                txtHours.TextChanged += new EventHandler(txtHoursTextChangedHandler);
                txtMinutes.TextChanged += new EventHandler(txtMinutesTextChangedHandler);
                txtSeconds.TextChanged += new EventHandler(txtSecondsTextChangedHandler);
                txtMilliseconds.TextChanged += new EventHandler(txtMillisecondsTextChangedHandler);
            }
            else
            {
                txtHours.TextChanged -= new EventHandler(txtHoursTextChangedHandler);
                txtMinutes.TextChanged -= new EventHandler(txtMinutesTextChangedHandler);
                txtSeconds.TextChanged -= new EventHandler(txtSecondsTextChangedHandler);
                txtMilliseconds.TextChanged -= new EventHandler(txtMillisecondsTextChangedHandler);
            }
        }

        private void UpdateTimeBoxControl()
        {
            txtHours.Text = time.Hours.ToString("00");
            txtMinutes.Text = time.Minutes.ToString("00");
            txtSeconds.Text = time.Seconds.ToString("00");
            txtMilliseconds.Text = time.Milliseconds.ToString("000");
        }


        private bool TryGetValue(TextBox control, out int value)
        {
            if (!String.IsNullOrEmpty(control.Text))
            {
                return int.TryParse(control.Text, out value);
            }
            else
            {
                value = 0;
                return false;
            }
        }

        private void txtHoursTextChangedHandler(object sender, EventArgs e)
        {
            int hours;
            if (TryGetValue(txtHours, out hours))
            {
                time = new TimeSpan(0, hours, time.Minutes, time.Seconds, time.Milliseconds);

                OnTimeChanged(time);
            }
        }

        private void txtMinutesTextChangedHandler(object sender, EventArgs e)
        {
            int minutes;
            if (TryGetValue(txtMinutes, out minutes))
            {
                time = new TimeSpan(0, time.Hours, minutes, time.Seconds, time.Milliseconds);

                OnTimeChanged(time);
            }
        }

        private void txtSecondsTextChangedHandler(object sender, EventArgs e)
        {
            int seconds;
            if (TryGetValue(txtSeconds, out seconds))
            {
                time = new TimeSpan(0, time.Hours, time.Minutes, seconds, time.Milliseconds);

                OnTimeChanged(time);
            }
        }

        private void txtMillisecondsTextChangedHandler(object sender, EventArgs e)
        {
            int milliseconds;
            if (TryGetValue(txtMilliseconds, out milliseconds))
            {
                time = new TimeSpan(0, time.Hours, time.Minutes, time.Seconds, milliseconds);

                OnTimeChanged(time); 
            }
        }

        protected virtual void OnTimeChanged(TimeSpan newValue)
        {
            var temporaryEventHolder = TimeChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<TimeSpan>(newValue));
            }
        }


        private void txtHoursKeyPressHandler(object sender, KeyPressEventArgs e)
        {
            e.Handled = IsKeyPressHandled(e.KeyChar);
        }

        private void txtMinutesKeyPressHandler(object sender, KeyPressEventArgs e)
        {
            e.Handled = IsKeyPressHandled(e.KeyChar);
        }

        private void txtSecondsKeyPressHandler(object sender, KeyPressEventArgs e)
        {
            e.Handled = IsKeyPressHandled(e.KeyChar);
        }

        private void txtMillisecondsKeyPressHandler(object sender, KeyPressEventArgs e)
        {
            e.Handled = IsKeyPressHandled(e.KeyChar);
        }

        private bool IsKeyPressHandled(char key)
        {
            return !(char.IsControl(key)) && !(char.IsDigit(key));
        }


        private void txtHoursValidatingHandler(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtHours.Text))
            {
                txtHours.Undo();
                e.Cancel = false;
            }
        }

        private void txtMinutesValidatingHandler(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtMinutes.Text))
            {
                txtMinutes.Undo();
                e.Cancel = false;
            }
        }

        private void txtSecondsValidatingHandler(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtSeconds.Text))
            {
                txtSeconds.Undo();
                e.Cancel = false;
            }
        }

        private void txtMillisecondsValidatingHandler(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtMilliseconds.Text))
            {
                txtMilliseconds.Undo();
                e.Cancel = false;
            }
        }

        private void EnabledChangedHandler(object sender, EventArgs e)
        {
            if (this.Enabled)
            {
                pnlHost.BackColor = SystemColors.Window;
            }
            else
            {
                pnlHost.BackColor = SystemColors.Control;
            }
        }

    }
}
