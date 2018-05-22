using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SubeditorRegexCompiler
{
    /// <summary>
    /// Informacje o obiekcie Regex reprezentującym wzorzec zdarzenia w napisach TMPlayer.
    /// </summary>
    class TMPlayerEntryRegexInfo : RegexInfo
    {
        public override RegexCompilationInfo CreateRegexCompilationInfo()
        {
            String pattern = @"(?<start>\d{2}:\d{2}:\d{2}):(?<text>.*(\r\n|\r|\n)?)";

            String regexName = "TMPlayerEntryRegex";

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
