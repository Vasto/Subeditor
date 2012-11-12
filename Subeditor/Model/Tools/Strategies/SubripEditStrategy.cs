using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Subeditor.Model.OrganizationalEntities;

namespace Subeditor.Model.Tools.Strategies
{
    /// <summary>
    /// Reprezentuje strategię wykorzystywaną do edycji napisów typu Subrip.
    /// </summary>
    class SubripEditStrategy : EditStrategyBase
    {
        private int currentEntryNumber;
        private int entryStart;
        private int entryLength;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="editedContent">Edytowana zawartość tekstowa napisów.</param>
        public SubripEditStrategy(String editedContent) 
            : base(editedContent)
        {
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
            SubripEntry entry = null;
            using (StringReader reader = new StringReader(Content))
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
                            entry = new SubripEntry(entryContentBuilder.ToString(), entryStartIndex);
                            currentEntryNumber = nextEntryNumber;
                            entryLength = entryContentBuilder.Length;
                            entryStart = entryStartIndex;
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
            SubripEntry entry = null;
            if (currentEntryNumber > 1)
            {
                using (StringReader reader = new StringReader(Content))
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
                                entry = new SubripEntry(entryContentBuilder.ToString(), entryStartIndex);

                                currentEntryNumber = previousEntryNumber;
                                entryLength = entryContentBuilder.Length;
                                entryStart = entryStartIndex;
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

    }
}
