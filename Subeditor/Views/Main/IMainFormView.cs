using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework;
using KWinFramework.Views;

namespace Subeditor.Views.Main
{
    /// <summary>
    /// Widok głównej formy aplikacji.
    /// </summary>
    interface IMainFormView : IView
    {
        /// <summary>
        /// Zdarzenie żądania wyświetlenia widoku.
        /// </summary>
        event EventHandler ShowRequest;

        /// <summary>
        /// Zdarzenie żądania zamknięcia widoku.
        /// </summary>
        event EventHandler CloseRequest;

        /// <summary>
        /// Zdarzenie poprzedzające żądanie zamknięcia widoku. 
        /// </summary>
        event EventHandler<ViewPreCloseEventArgs> PreCloseRequest;

        /// <summary>
        /// Pozwala pobrać lub ustawić nagłówek widoku.
        /// </summary>
        String Caption { get; set; }
    }
}
