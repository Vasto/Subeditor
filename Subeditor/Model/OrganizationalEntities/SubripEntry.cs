using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Subeditor.Model.OrganizationalEntities
{
    /// <summary>
    /// Reprezentuje wpis w napisach typu SubRip (.srt).
    /// </summary>
    class SubripEntry : TimedEntry
    {
        private static readonly String timeFormat = CreateTimeFormat();
        private static readonly String timingPattern = CreateTimingPattern();
        private static readonly Regex timingRegex = new Regex(timingPattern, RegexOptions.Compiled);

        /// <summary>
        /// Tworzy tekst będący formatem czasu używanego przez wpis.
        /// </summary>
        /// <returns></returns>
        private static String CreateTimeFormat()
        {
            return "hh\\:mm\\:ss\\,fff";
        }

        /// <summary>
        /// Tworzy wzór zdarzenia dla wyrażenia regularengo, mającego dopasować czas bieżącego wpsiu.
        /// </summary>
        /// <returns></returns>
        private static String CreateTimingPattern()
        {
            return @"(?<start>\d{2}:\d{2}:\d{2},\d{3})\s-->\s(?<end>\d{2}:\d{2}:\d{2},\d{3})";
        }

        private int number;
        private String text;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="content">Tekst zawierany przez wpis.</param>
        /// <param name="start">Indeks początkowy wpisu.</param>
        public SubripEntry(String content, int start)
            : base(content, start)
        {
            this.ParseContent();
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="content">Tekst zawierany przez wpis.</param>
        /// <param name="start">Indeks początkowy wpisu.</param>
        /// <param name="timingStart">Czas początku.</param>
        /// <param name="timingEnd">Czas końca.</param>
        public SubripEntry(String content, int start, long timingStart, long timingEnd) 
            : base(content, start, timingStart, timingEnd)
        {
            this.ParseContent();
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="content">Tekst zawierany przez wpis.</param>
        /// <param name="start">Indeks początkowy wpisu.</param>
        /// <param name="timingStart">Czas początku.</param>
        /// <param name="timingEnd">Czas końca.</param>
        public SubripEntry(String content, int start, TimeSpan timingStart, TimeSpan timingEnd)
            : base(content, start, timingStart, timingEnd)
        {
            this.ParseContent();
        }


        /// <summary>
        /// Pozwala pobrać informację czy typ wykorzystuje timing końcowy.
        /// </summary>
        public override bool UsesTimingEnd
        {
            get 
            { 
                return true; 
            }
        }


        /// <summary>
        /// Pozwala pobrać lub ustawić numer wpisu.
        /// </summary>
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                ContentNeedUpdate = true;
                OnContentChanged();
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić przechowywany tekst wpisu.
        /// </summary>
        public String Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                ContentNeedUpdate = true;
                OnContentChanged();
            }
        }

        /// <summary>
        /// Porównuje czy jeden obiekt Entry jest równy drugiemu
        /// </summary>
        /// <param name="other">Obiekt Selection do porównania.</param>
        /// <returns>Równy czy nie.</returns>
        public override bool Equals(Entry other)
        {
            SubripEntry otherSrtEntry = other as SubripEntry;
            if (otherSrtEntry != null)
            {
                return (this.Number == otherSrtEntry.Number) &&
                       (this.Text == otherSrtEntry.Text) &&
                       base.Equals(otherSrtEntry);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Porównuje czy jeden System.Object jest równy drugiemu
        /// </summary>
        /// <param name="o">System.Object do porównania.</param>
        /// <returns>Równy czy nie.</returns>
        public override bool Equals(object obj)
        {
            SubripEntry other = obj as SubripEntry;
            if (other != null)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Funkcja haszująca.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Number.GetHashCode() +
                   Text.GetHashCode() +
                   base.GetHashCode();
        }

        /// <summary>
        /// Zwraca timing sformatowany w sposób właściwy dla danego typy napisów.
        /// </summary>
        /// <param name="timing">Timing podany w niezależnych jednostkach.</param>
        /// <returns>Sformatowany timing.</returns>
        protected override String GetFormattedTiming(long timing)
        {
            return TimeSpan.FromTicks(timing).ToString(timeFormat);
        }

        /// <summary>
        /// Metoda wywoływana w celu zaktualizowaniu zawartości tekstowej wpisu (Content),
        /// w oparciu o wartości posczególnych właściowości bieżącego obiektu.
        /// </summary>
        protected override void UpdateContent()
        {
            StringBuilder content = new StringBuilder();

            content.Append(Number.ToString());
            content.AppendLine();
            content.Append(GetFormattedTiming(TimingStart));
            content.Append(" --> ");
            content.Append(GetFormattedTiming(TimingEnd));
            content.AppendLine();
            content.Append(Text);

            Content = content.ToString();
        }

        /// <summary>
        /// Parsuje zawartość tekstową, w celu pozyskania informacji o poszczególnych wartościach wpisu.
        /// </summary>
        private void ParseContent()
        {
            using (StringReader reader = new StringReader(Content))
            {
                //Pierwsza linia oznacza numer lini.
                String firstLine = reader.ReadLine();
                if (firstLine == null)
                {
                    throw new Exception("Missing entry part.");
                }

                int numberValue;
                if (int.TryParse(firstLine, out numberValue))
                {
                    Number = numberValue;
                }
                else
                {
                    throw new Exception("Invalid format.");
                }

                //Druga linia zawiera timingi.
                String secondLine = reader.ReadLine();
                if (secondLine == null)
                {
                    throw new Exception("Missing entry part.");
                }

                //Regex timingRegex = new Regex(timingPattern);
                Match timingMatch = timingRegex.Match(secondLine);
                if (timingMatch.Success)
                {
                    String timingStartValue = timingMatch.Groups["start"].Value;
                    TimingStart = TimeSpan.Parse(timingStartValue).Ticks;

                    String timingEndValue = timingMatch.Groups["end"].Value;
                    TimingEnd = TimeSpan.Parse(timingEndValue).Ticks;
                }
                else
                {
                    throw new Exception("Invalid format.");
                }

                //Pozostałe linie to zawartość tekstowa wpisu.
                Text = reader.ReadToEnd();

                ContentNeedUpdate = false;
            }
        }


    }
}
