using System;

namespace Subeditor.Model.Modifications
{
    /// <summary>
    /// Określa obszar zawartości tekstowej napisów na jakim zaszła modyfikacja.
    /// </summary>
    enum SubtitlesContentModificationArea
    {
        /// <summary>
        /// Przed pozycją karetki w tekście.
        /// </summary>
        PreCaret,

        /// <summary>
        /// Po pozycji karetki w tekście.
        /// </summary>
        PostCaret,

        /// <summary>
        /// Na obszarze zaznaczenia w tekście.
        /// </summary>
        Selection,

        /// <summary>
        /// Obszar do pozycji karetki lub do końca zaznaczenia w zależności,
        /// który obszar jest dłuższy.
        /// </summary>
        CaretOrSelectionEnd,

        /// <summary>
        /// Na całym obszarze tekstu.
        /// </summary>
        Entire,
    }
}
