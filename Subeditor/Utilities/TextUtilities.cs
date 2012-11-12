using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Utilities
{
    static class TextUtilities
    {
        private static Dictionary<TextRange, int> charCountCache = new Dictionary<TextRange, int>();
        private static Dictionary<TextRange, int> byteCountCache = new Dictionary<TextRange, int>();

        public static int GetCharCount(byte[] rawText, int start, int length)
        {
            Encoding enc = Encoding.UTF8;
            int count = enc.GetCharCount(rawText, start, length);

            return count;
        }

        public static int GetByteCount(String text, int start, int length)
        {
            Encoding enc = Encoding.UTF8;
            int count = enc.GetByteCount(text.ToCharArray(), start, length);

            return count;
        }

    }
}
