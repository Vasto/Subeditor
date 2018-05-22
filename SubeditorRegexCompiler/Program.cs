using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubeditorRegexCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Compiler compiler = new Compiler();
            compiler.AddRegexCompilationInfo(new AssEventEntryRegexInfo());
            compiler.AddRegexCompilationInfo(new SSAEventEntryRegexInfo());
            compiler.AddRegexCompilationInfo(new MicroDVDEntryRegexInfo());
            compiler.AddRegexCompilationInfo(new SubripTimingRegexInfo());
            compiler.AddRegexCompilationInfo(new TMPlayerEntryRegexInfo());

            compiler.Compile(
                "SubeditorRegexLib",
                "Version=1.0.0.0",
                "Culture=neutral");

            System.Console.ReadKey();
        }

    }
}
