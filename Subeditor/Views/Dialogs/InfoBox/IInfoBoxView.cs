using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Views;

namespace Subeditor.Views.Dialogs.InfoBox
{
    /// <summary>
    /// Interfejs widoku z komunikatem.
    /// </summary>
    interface IInfoBoxView : IView
    {
        /// <summary>
        /// Zdarzenie wciśnięcia przycisku zapisz.
        /// </summary>
        event EventHandler Ok;

        /// <summary>
        /// 
        /// </summary>
        String Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        String MessageImage { get; set; }
    }
}
