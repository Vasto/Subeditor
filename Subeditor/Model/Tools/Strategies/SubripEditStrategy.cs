using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Subeditor.Model.OrganizationalEntities;
using System.Text.RegularExpressions;

namespace Subeditor.Model.Tools.Strategies
{
    /// <summary>
    /// Reprezentuje strategię wykorzystywaną do edycji napisów typu Subrip.
    /// </summary>
    class SubripEditStrategy : EditStrategyBase
    {
        //Przechowuje oryginalną zawartośc tekstową.
        private String content;
        //Przechowuje zmodyfikowaną zawartosć tekstową.
        private StringBuilder modifiedContent;
        private bool isContentModified;
        private int modifiedContentLengthDelta;

        private int currentEntryInitialStart;
        private int currentEntryInitialLength;
        private int currentEntryNumber;
        //private int nextEntryIndex;
        private ReadingDirection entryReadingDirection;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="editedContent">Edytowana zawartość tekstowa napisów.</param>
        public SubripEditStrategy(String editedContent) 
            : base(editedContent)
        {
            //this.nextEntryIndex = 0;
            this.modifiedContentLengthDelta = 0;
            this.entryReadingDirection = ReadingDirection.None;
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
                //nextEntryIndex = 0;
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

            SubripEntry entry = null;
            using (StringReader reader = new StringReader(content))
            {
                int entryStartIndex = 0;
                int nextEntryNumber = currentEntryNumber + 1;
                bool entryFound = false;
                StringBuilder entryContentBuilder = new StringBuilder();
                String lineContent = String.Empty;

                while ((lineContent = reader.ReadLine()) != null)
                {    
                    //if ((!entryFound) && (int.TryParse(lineContent, out entryCounter)) && (entryCounter == nextEntryNumber))
                    if ((!entryFound) && (IsLineNumber(lineContent)) && ((int.Parse(lineContent) == nextEntryNumber)))
                    {
                        entryFound = true;
                    }

                    if (!entryFound)
                    {
                        entryStartIndex += lineContent.Length + Environment.NewLine.Length;
                    }

                    if (entryFound)
                    {
                        bool isLastLine = reader.Peek() == -1;
                        if (isLastLine)
                        {
                            //Jeśli jest to ostatnia linia to dodajemy zawartość bez symbola nowej lini na końcu, który dodaje AppendLine.
                            entryContentBuilder.Append(lineContent);
                        }
                        else
                        {
                            entryContentBuilder.AppendLine(lineContent);
                        }

                        //Jeśli doszliśmy do lini pustej oznaczającej koniec wpisu, lub ostatniej lini tekstu, 
                        //to oznacza że właściwy wpis został znaleziony, w związku z czym można stworzyć obiekt go reprezentujący.
                        if ((lineContent == String.Empty) || isLastLine)
                        {
                            entry = new SubripEntry(entryContentBuilder.ToString(), entryStartIndex + modifiedContentLengthDelta);
                            currentEntryNumber = nextEntryNumber;
                            currentEntryInitialLength = entryContentBuilder.Length;
                            currentEntryInitialStart = entryStartIndex;
                            break;
                        }
                    }
                }
            }

            CurrentEntry = entry;

            return entry;
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

            SubripEntry entry = null;
            if (currentEntryNumber > 1)
            {
                using (StringReader reader = new StringReader(content))
                {
                    int entryStartIndex = 0;
                    int previousEntryNumber = currentEntryNumber - 1;
                    bool entryFound = false;
                    StringBuilder entryContentBuilder = new StringBuilder();
                    String lineContent = String.Empty;

                    while ((lineContent = reader.ReadLine()) != null)
                    {
                        if ((!entryFound) && (IsLineNumber(lineContent)) && ((int.Parse(lineContent) == previousEntryNumber)))
                        {
                            entryFound = true;
                        }

                        if (!entryFound)
                        {
                            entryStartIndex += lineContent.Length + Environment.NewLine.Length;
                        }

                        if (entryFound)
                        {
                            entryContentBuilder.AppendLine(lineContent);
                            //Jeśli doszliśmy do lini pustej oznaczającej ostatnią lienie
                            //wykonujemy czynności kończące przeskok do następnego wpisu.
                            if (lineContent == String.Empty)
                            {
                                entry = new SubripEntry(entryContentBuilder.ToString(), entryStartIndex + modifiedContentLengthDelta);

                                currentEntryNumber = previousEntryNumber;
                                currentEntryInitialLength = entryContentBuilder.Length;
                                currentEntryInitialStart = entryStartIndex;
                                break;
                            }
                        }
                    }
                }
            }
            else if (currentEntryNumber <= 1)
            {
                currentEntryNumber = 0;
            }

            CurrentEntry = entry;

            return entry;
        }

        /// <summary>
        /// Określa czy wskazany teks może być numerem lini.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool IsLineNumber(String text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return false;
            }
            else
            {
                text = text.TrimEnd(' ', '\t');
                for (int i = 0; i < text.Length; ++i)
                {
                    if (!char.IsDigit(text, i))
                    {
                        return false;
                    }
                }

                return true;
            }
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
                //nextEntryIndex += modifiedContentLengthDelta;
                modifiedContentLengthDelta = 0;

                isContentModified = false;
            }
        }

    }
}
