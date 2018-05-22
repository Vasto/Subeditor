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
        private Regex timedEntryExpressionForward;
        private Regex timedEntryExpressionBackward;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="editedContent">Edytowana zawartość tekstowa napisów.</param>
        public AssEditStrategy(String editedContent) 
            : base(editedContent)
        {
            this.nextEntryIndex = 0;
            this.modifiedContentLengthDelta = 0;
            this.entryReadingDirection = ReadingDirection.None;
            this.timedEntryPattern = CreateTimedEntryPattern();
            this.timedEntryExpressionForward = new Regex(timedEntryPattern, RegexOptions.Compiled);
            this.timedEntryExpressionBackward = new Regex(timedEntryPattern, RegexOptions.Compiled | RegexOptions.RightToLeft);
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

            //(Optymalizacja) Kolejne wpisy są dopasowywane w orygianlnym content, aby zapobiec każdorazowemu wywoływaniu
            //ToString() zmiennej modifiedContent. Jedak aby wpisy zawierały poprawne informacje o pozycji w rzeczywistej zmodyfikowanej zawartości (modifiedContent),
            //konieczne jest dodanie delty długości (modifiedContentLengthDelta) do pozycji zwaracanego wpisu.
            AssEventEntry nextEntry = null;
            Match entryMatch = timedEntryExpressionForward.Match(content, nextEntryIndex);
            if (entryMatch.Success)
            {
                nextEntry = new AssEventEntry(entryMatch.Value, entryMatch.Index + modifiedContentLengthDelta);
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

            AssEventEntry previousEntry = null;
            Match entryMatch = timedEntryExpressionBackward.Match(content, nextEntryIndex);
            if (entryMatch.Success)
            {
                previousEntry = new AssEventEntry(entryMatch.Value, entryMatch.Index + modifiedContentLengthDelta);
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
        /// Tworzy wzorzec dla wyrażenia regularnego mającego dopasowąć wpis z timingiem w danej zawartości napisów .ass.
        /// </summary>
        private string CreateTimedEntryPattern()
        {
            return @"(?:Dialogue|Comment|Picture|Sound|Movie|Command):\s\d+,.*(?:\r\n?|\r?|\n?)";
            //(?>Dialogue|Comment|Picture|Sound|Movie|Command):\s\d+?,.*(?>\r\n|\r|\n)?
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
