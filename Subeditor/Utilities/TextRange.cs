using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Utilities
{
    class TextRange : IEquatable<TextRange>
    {
        public TextRange(int start, int length)
        {
            this.Start = start;
            this.Length = length;
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

        public bool Equals(TextRange other)
        {
            TextRange otherTextRange = other as TextRange;
            if (otherTextRange != null)
            {
                return (this.Start == otherTextRange.Start) &&
                       (this.Length == otherTextRange.Length);
            }
            else
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            TextRange other = obj as TextRange;
            if (other != null)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Start.GetHashCode() + Length.GetHashCode();
        }
    }
}
