using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SubeditorRegexCompiler
{
    /// <summary>
    /// Informacje o obiekcie Regex reprezentującym wzorzec zdarzenia w napisach MicroDVD.
    /// </summary>
    class MicroDVDEntryRegexInfo : RegexInfo
    {
        public override RegexCompilationInfo CreateRegexCompilationInfo()
        {
            String pattern = @"{(?<start>\d+)}{(?<end>\d+)}(?<text>.*(\r\n|\r|\n)?)";

            String regexName = "MicroDVDEntryRegex";

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
