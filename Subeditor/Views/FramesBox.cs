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
    public partial class FramesBox : UserControl
    {
        private int frames;

        public FramesBox()
        {
            InitializeComponent();
        }

        public event EventHandler<EventArgs<int>> FramesChanged;

        public int Frames 
        {
            get
            {
                return frames;
            }
            set
            {
                frames = value;

                txtNumeric.TextChanged -= new EventHandler(txtNumericTextChangedHandler);
                UpdateFramesBoxControl();
                txtNumeric.TextChanged += new EventHandler(txtNumericTextChangedHandler);

                OnFramesChanged(value);
            }
        }

        private void UpdateFramesBoxControl()
        {
            txtNumeric.Text = frames.ToString();
        }

        private void OnFramesChanged(int newValue)
        {
            var temporaryEventHolder = FramesChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<int>(newValue));
            }
        }

        private void txtNumericTextChangedHandler(object sender, EventArgs e)
        {
            if (int.TryParse(txtNumeric.Text, out frames))
            {
                OnFramesChanged(frames);
            }
        }

        private void txtNumericKeyPressHandler(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsControl(e.KeyChar)) && !(char.IsDigit(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void txtNumericValidatingHandler(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtNumeric.Text))
            {
                txtNumeric.Undo();
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
