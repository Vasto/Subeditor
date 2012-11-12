using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KWinFramework.Views.WinForms;

namespace Subeditor.Views.Dialogs.InfoBox
{
    /// <summary>
    /// Widok z komunikatem.
    /// </summary>
    public partial class InfoBoxView : ModalFormView, IInfoBoxView
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public InfoBoxView()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        /// <summary>
        /// Zdarzenie wciśnięcia przycisku zapisz.
        /// </summary>
        public event EventHandler Ok;

        /// <summary>
        /// 
        /// </summary>
        public String Message 
        {
            get 
            { 
                return lblMessage.Text; 
            }
            set 
            { 
                lblMessage.Text = value;
                CenterMessage();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String MessageImage
        {
            get
            {
                return pbMessageImage.ImageLocation;
            }
            set
            {
                pbMessageImage.ImageLocation = value;
                UpdateImage();
            }
        }

        private void CenterMessage()
        {
            int lblHeight = lblMessage.Height;
            int lblPanelHeight = panel1.Height;

            lblMessage.Location = new Point(lblMessage.Location.X, (lblPanelHeight / 2) - (lblHeight / 2));
        }


        private void UpdateImage()
        {
            pbMessageImage.Load();
        }


        private void btnOkClickHandler(object sender, EventArgs e)
        {
            var temporaryEventHolder = Ok;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs());
            }
        }
    }
}
