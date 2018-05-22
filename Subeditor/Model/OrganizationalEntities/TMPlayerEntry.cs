using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Subeditor.Model.OrganizationalEntities
{
    /// <summary>
    /// Reprezentuje wpis w napisach typu TMPlayer (.txt).
    /// </summary>
    class TMPlayerEntry: TimedEntry
    {
        private static readonly String timeFormat = CreateTimeFormat();
        private static readonly String timeEntryPattern = CreateTimeEntryPattern();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static String CreateTimeFormat()
        {
            return "hh\\:mm\\:ss";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static String CreateTimeEntryPattern()
        {
            return @"(?<start>\d{2}:\d{2}:\d{2})(?::|\s)(?<text>.*(?:\r\n?|\r?|\n?))"; //lepsza wydajność
            //return @"(?<start>\d{2}:\d{2}:\d{2}):(?<text>.*(\r\n|\r|\n)?)";
        }

        private String text;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="content">Tekst zawierany przez wpis.</param>
        /// <param name="start">Indeks początkowy wpisu.</param>
        public TMPlayerEntry(String content, int start)
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
        public TMPlayerEntry(String content, int start, long timingStart, long timingEnd) 
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
        public TMPlayerEntry(String content, int start, TimeSpan timingStart, TimeSpan timingEnd)
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
                return false; 
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
            TMPlayerEntry otherTMPlayerEntry = other as TMPlayerEntry;
            if (otherTMPlayerEntry != null)
            {
                return (this.Text == otherTMPlayerEntry.Text) &&
                       base.Equals(otherTMPlayerEntry);
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
            TMPlayerEntry other = obj as TMPlayerEntry;
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
            return Text.GetHashCode() +
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
            StringBuilder contentBuilder = new StringBuilder();
            contentBuilder.Append(GetFormattedTiming(TimingStart));
            contentBuilder.Append(":");
            contentBuilder.Append(Text);

            this.Content = contentBuilder.ToString();
        }

        /// <summary>
        /// Parsuje zawartość tekstową, w celu pozyskania informacji o poszczególnych wartościach wpisu.
        /// </summary>
        private void ParseContent()
        {
            //Regex entryRegex = new Regex(timeEntryPattern);
            Match entryMatch = Regex.Match(Content, timeEntryPattern);
            if (entryMatch.Success)
            {
                String timingStartValue = entryMatch.Groups["start"].Value;
                TimingStart = TimeSpan.Parse(timingStartValue).Ticks;

                String textValue = entryMatch.Groups["text"].Value;
                Text = textValue;
            }
        }

    }
}
