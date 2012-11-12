using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Subeditor.Model.OrganizationalEntities;

namespace Subeditor.Model.Tools.Strategies
{
    /// <summary>
    /// Reprezentuje strategię wykorzystywaną do edycji napisów typu MicroDVD.
    /// </summary>
    class MicroDVDEditStrategy : EditStrategyBase
    {
        private readonly String entryPattern;

        private Regex entryExpressionForward;
        private Regex entryExpressionBackward;
        private int currentEntryNumber;
        private int entryStart;
        private int entryLength;
        
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="editedContent">Edytowana zawartość tekstowa napisów.</param>
        public MicroDVDEditStrategy(String editedContent)
            : base(editedContent)
        {
            this.entryPattern = CreateTimedEntryPattern();
            this.entryExpressionForward = new Regex(entryPattern, RegexOptions.Compiled);
            this.entryExpressionBackward = new Regex(entryPattern, RegexOptions.Compiled | RegexOptions.RightToLeft);
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
            MicroDVDEntry nextEntry = null;
            int startIndex = CurrentEntry != null ? CurrentEntry.Start + CurrentEntry.Length : 0;
            Match entryMatch = entryExpressionForward.Match(Content, startIndex);
            if (entryMatch.Success)
            {
                nextEntry = new MicroDVDEntry(entryMatch.Value, entryMatch.Index);
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
            MicroDVDEntry previousEntry = null;
            int startIndex = (CurrentEntry != null) ? CurrentEntry.Start : 0;
            Match entryMatch = entryExpressionBackward.Match(Content, startIndex);
            if (entryMatch.Success)
            {
                previousEntry = new MicroDVDEntry(entryMatch.Value, entryMatch.Index);
                entryStart = entryMatch.Index;
                entryLength = entryMatch.Value.Length;
                currentEntryNumber--;
            }

            CurrentEntry = previousEntry;

            return previousEntry;
        }

        /// <summary>
        /// Tworzy wzorzec dla wyrażenia regularnego mającego dopasowąć wpis z timingiem w danej zawartości napisów .sub.
        /// </summary>
        private String CreateTimedEntryPattern()
        {
            return @"{\d*}{\d*}.*(\r\n|\r|\n)?";
        }

    }
}
