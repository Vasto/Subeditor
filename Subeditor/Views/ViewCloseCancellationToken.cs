using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Views
{
    /// <summary>
    /// Reprezentuje informacje o tym czy zamykanie widoku powinna być kontynuowane czy anulowane.
    /// </summary>
    class ViewCloseCancellationToken
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public ViewCloseCancellationToken() : this(false)
        {
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="cancelViewClose">Prawda jeśli zamykanie widoku ma być anulowane.</param>
        public ViewCloseCancellationToken(bool cancelViewClose)
        {
            this.Cancel = cancelViewClose;
        }

        /// <summary>
        /// Pozwala pobrac lub ustawić informację o tym czy zamykanie widoku ma zostać anulowane.
        /// </summary>
        public bool Cancel 
        {
            get; set; 
        }
    }
}
