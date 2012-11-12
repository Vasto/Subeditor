using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Model
{
    /// <summary>
    /// Klasa argumentów zdarzenia o zmianie stanu edycji napisów..
    /// </summary>
    class SubtitlesEditStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="oldState">Stan przed zdarzeniem.</param>
        /// <param name="newState">Stan po zdarzeniu.</param>
        public SubtitlesEditStateChangedEventArgs(SubtitlesEditState oldState, SubtitlesEditState newState)
        {
            this.OldState = oldState;
            this.NewState = newState;
        }

        /// <summary>
        /// Pozwala pobrać stan z przed wystąpienia zdrzenia.
        /// </summary>
        public SubtitlesEditState OldState 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Pozwala pobrać stan po wystąpieniu zdarzenia.
        /// </summary>
        public SubtitlesEditState NewState
        { 
            get; 
            private set;
        }

    }
}
