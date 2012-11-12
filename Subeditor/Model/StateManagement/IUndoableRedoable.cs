using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Model.StateManagement
{
    /// <summary>
    /// Definiuje obiekty potrafiące cofnąć/przywrócić swój stan do poprzedzającego zmianę. 
    /// </summary>
    interface IUndoableRedoable
    {
        /// <summary>
        /// Wycofuje ostatnią zmianę.
        /// </summary>
        void Undo();

        /// <summary>
        /// Przywraca ostatnio wycofaną zmianę.
        /// </summary>
        void Redo();

    }
}
