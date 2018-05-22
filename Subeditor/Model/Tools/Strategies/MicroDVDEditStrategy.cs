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

        //Przechowuje oryginalną zawartośc tekstową.
        private String content;
        //Przechowuje zmodyfikowaną zawartosć tekstową.
        private StringBuilder modifiedContent;
        private bool isContentModified;
        private int modifiedContentLengthDelta;

        private int currentEntryInitialStart;
        private int currentEntryInitialLength;
        private int currentEntryNumber;
        private int nextEntryIndex;
        private ReadingDirection entryReadingDirection;
        private Regex entryExpressionForward;
        private Regex entryExpressionBackward;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="editedContent">Edytowana zawartość tekstowa napisów.</param>
        public MicroDVDEditStrategy(String editedContent)
            : base(editedContent)
        {
            this.nextEntryIndex = 0;
            this.modifiedContentLengthDelta = 0;
            this.entryReadingDirection = ReadingDirection.None;
            this.entryPattern = CreateTimedEntryPattern();
            this.entryExpressionForward = new Regex(entryPattern, RegexOptions.Compiled);
            this.entryExpressionBackward = new Regex(entryPattern, RegexOptions.Compiled | RegexOptions.RightToLeft);
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić przechowywaną i edytowaną zawartość tekstową.
        /// </summary>
        public override String Content
        {
            get
            {
                UpdateContent();
                return content;
            }
            set
            {
                content = value;
                modifiedContent = new StringBuilder(value);
                nextEntryIndex = 0;
                modifiedContentLengthDelta = 0;
                entryReadingDirection = ReadingDirection.None;
            }
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

            int entryStartInModifiedContent = currentEntryInitialStart + modifiedContentLengthDelta;
            modifiedContent.Remove(entryStartInModifiedContent, currentEntryInitialLength);
            //Zastanowić się czy insert nie powinno wstawić w pozycję określoną przez Entry...
            modifiedContent.Insert(entryStartInModifiedContent, CurrentEntry.Content);

            //Oblicza zmiane długości wpisu po modyfikacji i dodaje ją do całkowitej zmiany długości zmodyfikowanego tekstu
            //w stosunku do oryginału.
            int entryLengthDelta = CurrentEntry.Length - currentEntryInitialLength;
            modifiedContentLengthDelta += entryLengthDelta;

            isContentModified = true;
        }

        /// <summary>
        /// Odczytuje i zwraca kolejny wpis z timingiem, z edytowanej zawartości tekstowej napisów.
        /// </summary>
        /// <returns>Zwraca kolejną wpis, a jeśli nie ma już więcej wpisów to null.</returns>
        public override TimedEntry NextTimedEntry()
        {
            if (entryReadingDirection == ReadingDirection.Backward)
            {
                UpdateContent();
            }
            entryReadingDirection = ReadingDirection.Forward;

            MicroDVDEntry nextEntry = null;
            Match entryMatch = entryExpressionForward.Match(content, nextEntryIndex);
            if (entryMatch.Success)
            {
                nextEntry = new MicroDVDEntry(entryMatch.Value, entryMatch.Index + modifiedContentLengthDelta);
                currentEntryInitialStart = entryMatch.Index;
                currentEntryInitialLength = entryMatch.Value.Length;
                currentEntryNumber++;
            }

            CurrentEntry = nextEntry;
            if (CurrentEntry != null)
            {
                nextEntryIndex = currentEntryInitialStart + currentEntryInitialLength;
            }

            return nextEntry;
        }

        /// <summary>
        /// Odczytuje i zwraca poprzedni wpis z timingiem, w stosunku do bieżącego, z edytowanej zawartości tekstowej napisów.
        /// </summary>
        /// <returns>Zwraca poprzedni wpis, a jeśli nie ma już więcej wpisów to null.</returns>
        public override TimedEntry PreviousTimedEntry()
        {
            if (entryReadingDirection == ReadingDirection.Forward)
            {
                UpdateContent();
            }
            entryReadingDirection = ReadingDirection.Backward;

            MicroDVDEntry previousEntry = null;
            Match entryMatch = entryExpressionBackward.Match(content, nextEntryIndex);
            if (entryMatch.Success)
            {
                previousEntry = new MicroDVDEntry(entryMatch.Value, entryMatch.Index + modifiedContentLengthDelta);
                currentEntryInitialStart = entryMatch.Index;
                currentEntryInitialLength = entryMatch.Value.Length;
                currentEntryNumber--;
            }

            CurrentEntry = previousEntry;
            if (CurrentEntry != null)
            {
                nextEntryIndex = currentEntryInitialStart;
            }

            return previousEntry;
        }

        /// <summary>
        /// Tworzy wzorzec dla wyrażenia regularnego mającego dopasowąć wpis z timingiem w danej zawartości napisów .sub.
        /// </summary>
        private String CreateTimedEntryPattern()
        {
            //return @"{\d*}{\d*}.*(\r\n|\r|\n)?";
            return @"{\d+}{\d+}.*(?:\r\n?|\r?|\n?)";
        }

        /// <summary>
        /// Dokonuje przeniesienia zawartości zmodyfikowanej (modifiedContent) do zmiennej przechowującej zawartość (content).
        /// Przypisuje zmiennym pomocniczym zawartości zmodyfikowanej, odpowiednie wartości. 
        /// </summary>
        private void UpdateContent()
        {
            if (isContentModified)
            {
                content = modifiedContent.ToString();
                nextEntryIndex += modifiedContentLengthDelta;
                modifiedContentLengthDelta = 0;

                isContentModified = false;
            }
        }

    }
}
