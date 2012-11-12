using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Model.Modifications
{
    /// <summary>
    /// Interfejs który musi zaimplementować każda modyfikacja.
    /// </summary>
    interface IModification
    {  
        /// <summary>
        /// Wykonuje modyfikację.
        /// </summary>
        void Perform();

    }
}
