using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SubeditorRegexCompiler
{
    /// <summary>
    /// Informacje o obiekcie Regex reprezentującym wzorzec zdarzenia w napisach ASS.
    /// </summary>
    class AssEventEntryRegexInfo : RegexInfo
    {
        public override RegexCompilationInfo CreateRegexCompilationInfo()
        {
            StringBuilder patternBuilder = new StringBuilder();
            patternBuilder.Append(@"(?<format>(Dialogue|Comment|Picture|Sound|Movie|Command)):\s"); 
            patternBuilder.Append(@"(?<layer>\d+),");//(?<layer>\d+?),
            patternBuilder.Append(@"(?<start>\d{1,2}:\d{2}:\d{2}\.\d{2}),");
            patternBuilder.Append(@"(?<end>\d{1,2}:\d{2}:\d{2}\.\d{2}),");
            patternBuilder.Append(@"(?<style>.*),"); //(?<style>.*?),
            patternBuilder.Append(@"(?<name>.*),"); //(?<name>.*?),
            patternBuilder.Append(@"(?<marginL>\d{1,4}),");
            patternBuilder.Append(@"(?<marginR>\d{1,4}),");
            patternBuilder.Append(@"(?<marginV>\d{1,4}),");
            patternBuilder.Append(@"(?<effect>.*),"); //(?<effect>.*?),
            patternBuilder.Append(@"(?<text>.*(\r\n|\r|\n)?)"); //(?<text>.*(?>\r\n?|\r|\n))     ----------> ? czy to działać będzie?

            String regexName = "AssEventEntryRegex";

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
