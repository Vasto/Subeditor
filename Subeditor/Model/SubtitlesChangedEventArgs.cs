using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Model
{
    /// <summary>
    /// Klasa argumentów zdarzenia o zmianie obiektu pliku napisów.
    /// </summary>
    class SubtitlesChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="oldSubtitles">Obiekt przed zdarzeniem.</param>
        /// <param name="newSubtitles">Obiekt po zdarzeniem.</param>
        public SubtitlesChangedEventArgs(SubtitlesFile oldSubtitles, SubtitlesFile newSubtitles)
        {
            this.OldSubtitles = oldSubtitles;
            this.NewSubtitles = newSubtitles;
        }

        /// <summary>
        /// Pobiera obiekt napisów z przed wystąpienia zdarzenia.
        /// </summary>
        public SubtitlesFile OldSubtitles 
        {
            get; 
            private set; 
        }

        /// <summary>
        /// Pobiera nowy obiekt napisów.
        /// </summary>
        public SubtitlesFile NewSubtitles 
        { 
            get;
            private set; 
        }

    }
}
