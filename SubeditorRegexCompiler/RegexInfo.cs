using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SubeditorRegexCompiler
{
    /// <summary>
    /// Informacje o obiekcie Regex dla kompilaotra.
    /// W oparciu o te inforamcje kompilator utworzy konkretny obiekt Regex z prekompilowanym wyrażeniem.
    /// Baza dla konkretnych klas zawierających sprecyzowane informacje o obiektach Regex.
    /// </summary>
    abstract class RegexInfo
    {
        private RegexCompilationInfo info;

        public RegexCompilationInfo Info
        {
            get
            {
                if (info == null)
                {
                    info = CreateRegexCompilationInfo();
                }

                return info;
            }
        }

        public abstract RegexCompilationInfo CreateRegexCompilationInfo();

    }
}
