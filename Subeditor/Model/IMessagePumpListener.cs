using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Model
{
    /// <summary>
    /// Interfejs obiektów chcących otrzymywać wiadomości z pętli głównej aplikcji.
    /// </summary>
    public interface IMessagePumpListener
    {
        /// <summary>
        /// Metoda wywoływana gdy pojawi się wiadomość.
        /// </summary>
        /// <param name="messageID">Identyfikator wiadomości.</param>
        /// <param name="wParam">Parametr widomośći.</param>
        /// <param name="lParam">Parametr widomośći.</param>
        void OnMessage(int messageID, IntPtr wParam, IntPtr lParam);
    }
}
