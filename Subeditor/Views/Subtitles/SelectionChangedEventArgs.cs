using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Views.Subtitles
{
    /// <summary>
    /// Argumenty zdarzenia o zmianie zaznaczenia.
    /// </summary>
    public class SelectionChangedEventArgs : EventArgs
    {
        public SelectionChangedEventArgs(int selectionStart, int selectionLength)
        {
            this.Start = selectionStart;
            this.Length = selectionLength;
        }

        public int Start 
        {
            get; 
            private set; 
        }

        public int Length 
        { 
            get; 
            private set; 
        }
    }
}
