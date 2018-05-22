using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;

namespace SubeditorRegexCompiler
{
    /// <summary>
    /// Klasa kompilatora, który w oparciu o podane informacje utworzy bibliotekę zawierającą prekompilowane obiekty Regex.
    /// </summary>
    class Compiler
    {
        private IList<RegexInfo> batch;

        public Compiler()
        {
            this.batch = new List<RegexInfo>();
        }

        public void AddRegexCompilationInfo(RegexInfo info)
        {
            batch.Add(info);
        }

        public void RemoveRegexCompilationInfo(RegexInfo info)
        {
            batch.Remove(info);
        }

        public void Compile(
            String destinationAssemblyName,
            String destinationAssemblyVersion,
            String destinationAssemblyCulture)
        {
            AssemblyName destinationAssembly = new AssemblyName(destinationAssemblyName);
            RegexCompilationInfo[] regegCompiliaitonInfoArray = batch.Select(i => i.Info).ToArray();
            Regex.CompileToAssembly(regegCompiliaitonInfoArray, destinationAssembly);
        }

    }
}
