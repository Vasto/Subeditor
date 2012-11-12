using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Views;
using KWinFramework.Views.WinForms.Dialogs;

namespace Subeditor.Views.Dialogs.OpenFile
{
    /// <summary>
    /// Interfejs widoku wyboru i otwarcia pliku.
    /// </summary>
    interface IOpenFileView : IDialogView
    {
        String FileName { get; }
        String Filter { get; set; }
    }
}
