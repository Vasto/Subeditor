using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Views.WinForms.Dialogs;

namespace Subeditor.Views.Dialogs.SaveFile
{
    /// <summary>
    /// Interfejs widoku zapisu pliku.
    /// </summary>
    interface ISaveFileView : IDialogView
    {
        String FileName { get; }
        String Filter { get; set; }
    }
}
