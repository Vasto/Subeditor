using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Views.Subtitles
{
    public class ScrolledEventArgs : EventArgs
    {
        public ScrolledEventArgs(int newLineNumber, int oldLineNumber)
        {
            this.NewLineNumber = newLineNumber;
            this.OldLineNumber = oldLineNumber;
        }

        public int NewLineNumber 
        {
            get; 
            set; 
        }

        public int OldLineNumber 
        { 
            get; 
            set; 
        }
    }
}
