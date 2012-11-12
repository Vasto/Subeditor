using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Model.Modifications
{
    /// <summary>
    /// Interfejs modyfikacji, mogącej przynależeć do grupy modyfikacji.
    /// </summary>
    interface IGroupableModification
    {
        /// <summary>
        /// Pozwala ustawić lub pobrać informację o grupie do jakiej należy modyfiacja.
        /// </summary>
        ModificationGroup Group { get; set; }

        /// <summary>
        /// Pozwala ustawić lub pobrać informację o indeksie jaki ma bieżąca modyfikacja w grupie, do której przynależy.
        /// </summary>
        int GroupIndex { get; set; }

    }
}
