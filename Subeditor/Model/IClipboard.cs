using System;

namespace Subeditor.Model
{
    /// <summary>
    /// Interfejs definiujący zestaw zachowań jakie ma implementować klasa chcąca pełnić rolę schowka.
    /// </summary>
    interface IClipboard
    {
        /// <summary>
        /// Zdarzenie mające miejsce gdy możliwość wykonania opercja wycinania ulegnie zmianie.
        /// </summary>
        event EventHandler<EventArgs<bool>> CanCutChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy możliwość wykonania opercja kopiowania ulegnie zmianie.
        /// </summary>
        event EventHandler<EventArgs<bool>> CanCopyChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy możliwość wykonania opercja wklejania ulegnie zmianie.
        /// </summary>
        event EventHandler<EventArgs<bool>> CanPasteChanged;

        /// <summary>
        /// Pobiera informacje o tym czy opercja wycinania jest wykonalna.
        /// </summary>
        bool CanCut { get; }

        /// <summary>
        /// Pobiera informacje o tym czy opercja kopiowania jest wykonalna.
        /// </summary>
        bool CanCopy { get; }

        /// <summary>
        /// Pobiera informacje o tym czy opercja wklejania jest wykonalna.
        /// </summary>
        bool CanPaste { get; }

        /// <summary>
        /// Wytnij.
        /// </summary>
        void Cut();

        /// <summary>
        /// Kopiuj.
        /// </summary>
        void Copy();

        /// <summary>
        /// Wklej.
        /// </summary>
        void Paste();

    }
}
