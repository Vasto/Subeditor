using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Subeditor.Model.OrganizationalEntities;

namespace Subeditor.Model.Tools.Strategies
{
    /// <summary>
    /// Reprezentuje strategię edycji napisów i stanowi bazę dla wyspecjalizowanych strategi 
    /// koncentrujących się na poszczególnych formatach plików.
    /// </summary>
    abstract class  EditStrategyBase : IEditStrategy
    {
        private int currentLineNumber;
        private int lineStart;
        private int lineLength;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="editedContent"></param>
        public EditStrategyBase(String editedContent)
        {
            this.Content = editedContent;
        }

        #region IEditStrategy

        /// <summary>
        /// Pozwala pobrać lub ustawić przechowywaną i edytowaną zawartość tekstową.
        /// </summary>
        public virtual String Content
        {
            get;
            set;
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić obiekt reprezentujący bieżąco edytowaną linie napisów.
        /// </summary>
        public virtual Line CurrentLine
        {
            get;
            set;
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić obiekt reprezentujący bieżąco edytowany wpis.
        /// </summary>
        public virtual Entry CurrentEntry
        {
            get;
            set;
        }

        /// <summary>
        /// Zapisuje modyfikacje bieżącej lini, przenosząc je na przechowywaną zawartość tekstową napisów.
        /// </summary>
        public virtual void SaveCurrentLine()
        {
            if (CurrentLine == null)
            {
                return;
            }

            StringBuilder contentBuilder = new StringBuilder(Content);
            contentBuilder.Remove(lineStart, lineLength);
            contentBuilder.Insert(lineStart, CurrentLine.Content);

            lineStart = CurrentLine.Start;
            lineLength = CurrentLine.Length;

            Content = contentBuilder.ToString();
        }

        /// <summary>
        /// Odczytuje i zwraca kolejną linie z edytowanej zawartości tekstowej napisów.
        /// </summary>
        /// <returns>Zwraca kolejną linię, a jeśli nie ma już więcej lini to null.</returns>
        public Line NextLine()
        {
            Line nextLine = null;
            using (StringReader reader = new StringReader(Content))
            {
                int lineCounter = 0;
                int lineStartIndex = 0;
                int nextLineNumber = currentLineNumber + 1;
                String lineContent = String.Empty;

                while ((lineContent = reader.ReadLine()) != null)
                {
                    lineCounter++;
                    //Za każdym razem zaczynamy szukaniee lini od początku, stąd sprawdzanie
                    //czy numer bieżącej lini równa sie poszukiwanemu numerowi następnej lini.
                    if (lineCounter == nextLineNumber)
                    {
                        currentLineNumber = nextLineNumber;
                        nextLine = new Line(lineContent, currentLineNumber, lineStartIndex);
                        lineStart = lineStartIndex;
                        lineLength = lineContent.Length;

                        break;
                    }

                    lineStartIndex += lineContent.Length + Environment.NewLine.Length;
                }
            }

            CurrentLine = nextLine;

            return nextLine;
        }

        /// <summary>
        /// Odczytuje i zwraca poprzednią linie, w stosunku do bieżącej, z edytowanej zawartości tekstowej napisów.
        /// </summary>
        /// <returns>Zwraca poprzednią linię, a jeśli nie ma już więcej lini to null.</returns>
        public Line PreviousLine()
        {
            Line previousLine = null;
            if (currentLineNumber > 1)
            {
                using (StringReader reader = new StringReader(Content))
                {
                    int lineCounter = 0;
                    int lineStartIndex = 0;
                    int previousLineNumber = currentLineNumber - 1;
                    String lineContent = String.Empty;

                    while ((lineContent = reader.ReadLine()) != null)
                    {
                        lineCounter++;
                        if (lineCounter == previousLineNumber)
                        {
                            currentLineNumber = previousLineNumber;
                            previousLine = new Line(lineContent, currentLineNumber, lineStartIndex);
                            lineStart = lineStartIndex;
                            lineLength = lineContent.Length;

                            break;
                        }

                        lineStartIndex += lineContent.Length + Environment.NewLine.Length;
                    }
                }
            }
            else if (currentLineNumber <= 1)
            {
                currentLineNumber = 0;
            }

            CurrentLine = previousLine;

            return previousLine;
        }

        /// <summary>
        /// Odczytuje i zwraca kolejny wpis z timingiem, z edytowanej zawartości tekstowej napisów.
        /// </summary>
        /// <returns>Zwraca kolejną wpis, a jeśli nie ma już więcej wpisów to null.</returns>
        public abstract TimedEntry NextTimedEntry();

        /// <summary>
        /// Odczytuje i zwraca poprzedni wpis z timingiem, w stosunku do bieżącego, z edytowanej zawartości tekstowej napisów.
        /// </summary>
        /// <returns>Zwraca poprzedni wpis, a jeśli nie ma już więcej wpisów to null.</returns>
        public abstract TimedEntry PreviousTimedEntry();

        /// <summary>
        /// Zapisuje modyfikacje bieżącej wpisu, przenosząc je na przechowywaną zawartość tekstową napisów.
        /// </summary>
        public abstract void SaveCurrentEntry();

        #endregion //IEditStrategy

    }
}
