using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Model
{
    /// <summary>
    /// Klasa argumentów zdarzenia o zawartości tekstu napisów.
    /// </summary>
    class SubtitlesContentChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="oldContent">Zawartość przed zdarzeniem.</param>
        /// <param name="newContent">Zawartość po zdarzeniu.</param>
        public SubtitlesContentChangedEventArgs(String oldContent, String newContent)
        {
            this.OldContent = oldContent;
            this.NewContent = newContent;
        }

        /// <summary>
        /// Pozwala pobrać zawartość z przed wystąpienia zdrzenia.
        /// </summary>
        public String OldContent 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Pozwala pobrać zawartość po wystąpieniu zdarzenia.
        /// </summary>
        public String NewContent 
        { 
            get; 
            private set; 
        }

    }

}
