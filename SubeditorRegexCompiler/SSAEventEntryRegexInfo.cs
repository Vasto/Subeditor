using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SubeditorRegexCompiler
{
    /// <summary>
    /// Informacje o obiekcie Regex reprezentującym wzorzec zdarzenia w napisach SSA.
    /// </summary>
    class SSAEventEntryRegexInfo : RegexInfo
    {
        public override RegexCompilationInfo CreateRegexCompilationInfo()
        {
            StringBuilder patternBuilder = new StringBuilder();
            patternBuilder.Append(@"(?<format>(Dialogue|Comment|Picture|Sound|Movie|Command)):\s");
            patternBuilder.Append(@"Marked=(?<marked>\d+),");
            patternBuilder.Append(@"(?<start>\d{1,2}:\d{2}:\d{2}\.\d{2}),");
            patternBuilder.Append(@"(?<end>\d{1,2}:\d{2}:\d{2}\.\d{2}),");
            patternBuilder.Append(@"(?<style>.*),");
            patternBuilder.Append(@"(?<name>.*),");
            patternBuilder.Append(@"(?<marginL>\d{4}),");
            patternBuilder.Append(@"(?<marginR>\d{4}),");
            patternBuilder.Append(@"(?<marginV>\d{4}),");
            patternBuilder.Append(@"(?<effect>.*),");
            patternBuilder.Append(@"(?<text>.*(\r\n|\r|\n)?)");

            String regexName = "SSAEventEntryRegex";

            String regexNamespace = "SubeditorRegexLib";

            RegexCompilationInfo info = new RegexCompilationInfo(
                patternBuilder.ToString(),
                RegexOptions.None,
                regexName,
                regexNamespace,
                true);

            return info;
        }

    }
}
