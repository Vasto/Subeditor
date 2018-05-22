using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SubeditorRegexCompiler
{
    /// <summary>
    /// Informacje o obiekcie Regex reprezentującym wzorzec zdarzenia w napisach Subrip.
    /// </summary>
    class SubripTimingRegexInfo : RegexInfo
    {
        public override RegexCompilationInfo CreateRegexCompilationInfo()
        {
            String pattern = @"(?<start>\d{2}:\d{2}:\d{2},\d{3})\s-->\s(?<end>\d{2}:\d{2}:\d{2},\d{3})";

            String regexName = "SubripTimingRegex";

            String regexNamespace = "SubeditorRegexLib";

            RegexCompilationInfo info = new RegexCompilationInfo(
                pattern,
                RegexOptions.None, 
                regexName, 
                regexNamespace, 
                true);

            return info;
        }

    }
}
