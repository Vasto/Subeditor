using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Views;

namespace Subeditor.Views.Dialogs.SaveBeforeClose
{
    /// <summary>
    /// Interfejs widoku dialogu pytającego zapis zmian przed wyjściem.
    /// </summary>
    interface ISaveBeforeCloseView : IView
    {
        /// <summary>
        /// Zdarzenie wciśnięcia przycisku zapisz.
        /// </summary>
        event EventHandler Save;

        /// <summary>
        /// Zdarzenie wciśnięcia przycisku nie zapisuj.
        /// </summary>
        event EventHandler DoNotSave;

        /// <summary>
        /// Zdarzenie wciśnięcia przycisku anuluj.
        /// </summary>
        event EventHandler Cancel;
    }
}
