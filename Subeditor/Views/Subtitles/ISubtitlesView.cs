using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework;
using KWinFramework.Views;
using KWinFramework.Views.WinForms.Commands;

namespace Subeditor.Views.Subtitles
{
    /// <summary>
    /// Interfejs widoku napisów.
    /// </summary>
    interface ISubtitlesView : IView
    {
        /// <summary>
        /// Zdarzenie mające miejsce w momencie zmiany edytowanego tekstu napisów.
        /// </summary>
        event EventHandler ContentChanged;

        /// <summary>
        /// Zdarzenie mające miejsce w moemencie zmiany obaszaru zaznaczenia.
        /// </summary>
        event EventHandler<SelectionChangedEventArgs> SelectionChanged;

        /// <summary>
        /// Zdarzenie mające miejsce przy przewijaniu.
        /// </summary>
        event EventHandler<ScrolledEventArgs> Scrolled;

        /// <summary>
        /// Pozwala pobrać lub ustawić tekstową zawartość edytowanego pliku napisów.
        /// </summary>
        String Content { get; set; }

        /// <summary>
        /// Pozwala pobrać zawartość edytowanego pliku w formie bajtów.
        /// </summary>
        byte[] RawContent { get; }

        /// <summary>
        /// Pozwala pobrac lub ustawić pozycję karetki.
        /// </summary>
        int CaretPosition { get; set; }

        /// <summary>
        /// Pozwala pobrać numer lini w której znajduję się karetka.
        /// </summary>
        int CaretLine { get; }

        /// <summary>
        /// 
        /// </summary>
        int SelectionStart { get; }

        /// <summary>
        /// 
        /// </summary>
        int SelectionLength { get; }

        /// <summary>
        /// Pozwala pobrać numer pierwszej widocznej lini.
        /// </summary>
        int FirstVisibleLine { get; }

        /// <summary>
        /// Pozwala pobrać liczbę wszystkich widocznych liń.
        /// </summary>
        int VisibleLinesCount { get; }

        /// <summary>
        /// Pozwala pobrać lub ustawić informację o szerokości kolumny z numeracją lini.
        /// </summary>
        int LineNumberColumnWidth { get; set; }

        ///// <summary>
        ///// Pozwala pobrać lub ustawić widok menu kontekstowego.
        ///// </summary
        //CommandContextMenuStripView ContextMenuStripView { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        void ScrollByLine(int line);

        /// <summary>
        /// 
        /// </summary>
        void ScrollToBegin();

        /// <summary>
        /// 
        /// </summary>
        void ScrollToCaret();

        /// <summary>
        /// Zaznacza okreslony obszar.
        /// </summary>
        /// <param name="selectionStart">Indeks początku zaznaczenia.</param>
        /// <param name="selectionLength">Liczba znaków do zaznaczenia.</param>
        void Select(int selectionStart, int selectionLength);

        /// <summary>
        /// Zaznacza całość.
        /// </summary>
        void SelectAll();
    }
}
