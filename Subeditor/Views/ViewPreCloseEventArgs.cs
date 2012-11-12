using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Views
{
    /// <summary>
    /// Argumenty zdarzenia o poprzedzającego zdarzenie zamknięcia widoku.
    /// </summary>
    public class ViewPreCloseEventArgs : EventArgs
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public ViewPreCloseEventArgs() : this(false)
        {

        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="cancelViewClose">Czy proces zamykania widoku ma być anulowany.</param>
        public ViewPreCloseEventArgs(bool cancelViewClose)
        {
            this.CancelViewClose = cancelViewClose;
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić informację czy proces zamykania widoku ma być anulowany.
        /// </summary>
        public bool CancelViewClose 
        { 
            get; set;
        }
    }
}
