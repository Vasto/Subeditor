using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KWinFramework.Views.WinForms;

namespace Subeditor.Views.Dialogs.SaveBeforeClose
{
    /// <summary>
    /// Widok dialogu pytającego zapis zmian przed wyjściem.
    /// </summary>
    public partial class SaveBeforeCloseView : ModalFormView, ISaveBeforeCloseView
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public SaveBeforeCloseView()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        /// <summary>
        /// Zdarzenie wciśnięcia przycisku zapisz.
        /// </summary>
        public event EventHandler Save;

        /// <summary>
        /// Zdarzenie wciśnięcia przycisku nie zapisuj.
        /// </summary>
        public event EventHandler DoNotSave;

        /// <summary>
        /// Zdarzenie wciśnięcia przycisku anuluj.
        /// </summary>
        public event EventHandler Cancel;

        private void btnSaveClickHandler(object sender, EventArgs e)
        {
            var temporaryEventHolder = Save;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs());
            }
        }

        private void btnDontSaveClickHandler(object sender, EventArgs e)
        {
            var temporaryEventHolder = DoNotSave;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs());
            }
        }

        private void btnCancelClickHandler(object sender, EventArgs e)
        {
            var temporaryEventHolder = Cancel;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs());
            }
        }
    }
}
