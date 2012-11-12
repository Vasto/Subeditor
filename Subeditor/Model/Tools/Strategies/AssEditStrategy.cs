using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Subeditor.Model.OrganizationalEntities;

namespace Subeditor.Model.Tools.Strategies
{
    /// <summary>
    /// Reprezentuje strategię wykorzystywaną do edycji napisów typu Advanced SubStation Alpha.
    /// </summary>
    class AssEditStrategy : EditStrategyBase
    {
        private readonly String timedEntryPattern;

        private Regex timedEntryExpressionForward;
        private Regex timedEntryExpressionBackward;
        private int currentEntryNumber;
        private int entryStart;
        private int entryLength;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="editedContent">Edytowana zawartość tekstowa napisów.</param>
        public AssEditStrategy(String editedContent) 
            : base(editedContent)
        {
            this.timedEntryPattern = CreateTimedEntryPattern();
            this.timedEntryExpressionForward = new Regex(timedEntryPattern, RegexOptions.Compiled);
            this.timedEntryExpressionBackward = new Regex(timedEntryPattern, RegexOptions.Compiled | RegexOptions.RightToLeft);
        }

        /// <summary>
        /// Zapisuje modyfikacje bieżącej wpisu, przenosząc je na przechowywaną zawartość tekstową napisów.
        /// </summary>
        public override void SaveCurrentEntry()
        {
            if (CurrentEntry == null)
            {
                return;
            }

            StringBuilder contentBuilder = new StringBuilder(Content);
            contentBuilder.Remove(entryStart, entryLength);
            contentBuilder.Insert(entryStart, CurrentEntry.Content);

            entryStart = CurrentEntry.Start;
            entryLength = CurrentEntry.Length;

            Content = contentBuilder.ToString();
        }

        /// <summary>
        /// Odczytuje i zwraca kolejny wpis z timingiem, z edytowanej zawartości tekstowej napisów.
        /// </summary>
        /// <returns>Zwraca kolejną wpis, a jeśli nie ma już więcej wpisów to null.</returns>
        public override TimedEntry NextTimedEntry()
        {
            AssEventEntry nextEntry = null;
            int startIndex = CurrentEntry != null ? CurrentEntry.Start + CurrentEntry.Length : 0;
            Match entryMatch = timedEntryExpressionForward.Match(Content, startIndex);
            if (entryMatch.Success)
            {
                nextEntry = new AssEventEntry(entryMatch.Value, entryMatch.Index);
                entryStart = entryMatch.Index;
                entryLength = entryMatch.Value.Length;
                currentEntryNumber++;
            }

            CurrentEntry = nextEntry;

            return nextEntry;
        }

        /// <summary>
        /// Odczytuje i zwraca poprzedni wpis z timingiem, w stosunku do bieżącego, z edytowanej zawartości tekstowej napisów.
        /// </summary>
        /// <returns>Zwraca poprzedni wpis, a jeśli nie ma już więcej wpisów to null.</returns>
        public override TimedEntry PreviousTimedEntry()
        {
            AssEventEntry previousEntry = null;
            int startIndex = (CurrentEntry != null) ? CurrentEntry.Start : 0;
            Match entryMatch = timedEntryExpressionBackward.Match(Content, startIndex);
            if (entryMatch.Success)
            {    
                previousEntry = new AssEventEntry(entryMatch.Value, entryMatch.Index);
                entryStart = entryMatch.Index;
                entryLength = entryMatch.Value.Length;
                currentEntryNumber--;
            }

            CurrentEntry = previousEntry;

            return previousEntry;
        }

        /// <summary>
        /// Tworzy wzorzec dla wyrażenia regularnego mającego dopasowąć wpis z timingiem w danej zawartości napisów .ass.
        /// </summary>
        private string CreateTimedEntryPattern()
        {
            //StringBuilder patternBuilder = new StringBuilder();

            //patternBuilder.Append(@"(Dialogue|Comment|Picture|Sound|Movie|Command):\s");
            ////patternBuilder.Append(@"\d,");
            ////patternBuilder.Append(@"\d{1,2}:\d{2}:\d{2}\.\d{2},");
            ////patternBuilder.Append(@"\d{1,2}:\d{2}:\d{2}\.\d{2},");
            ////patternBuilder.Append(@".*,");
            ////patternBuilder.Append(@".*,");
            ////patternBuilder.Append(@"\d{4},");
            ////patternBuilder.Append(@"\d{4},");
            ////patternBuilder.Append(@"\d{4},");
            ////patternBuilder.Append(@".*,");
            //patternBuilder.Append(@".*(\r\n|\r|\n)?");

            //return patternBuilder.ToString();

            return @"(Dialogue|Comment|Picture|Sound|Movie|Command):\s\d,.*(\r\n|\r|\n)?";
        }

    }
}
