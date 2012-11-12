using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Subeditor.Model.OrganizationalEntities
{
    /// <summary>
    /// Reprezentuje wpis w napisach typu MicroDVD (.sub).
    /// </summary>
    class MicroDVDEntry : TimedEntry
    {
        private readonly String entryPattern;

        private String text;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="content">Tekst zawierany przez wpis.</param>
        /// <param name="start">Indeks początkowy wpisu.</param>
        public MicroDVDEntry(String content, int start)
            : base(content, start)
        {
            this.entryPattern = CreateEntryPattern();

            this.ParseContent();
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="content">Tekst zawierany przez wpis.</param>
        /// <param name="start">Indeks początkowy wpisu.</param>
        /// <param name="timingStart">Czas początku.</param>
        /// <param name="timingEnd">Czas końca.</param>
        public MicroDVDEntry(String content, int start, long timingStart, long timingEnd) 
            : base(content, start, timingStart, timingEnd)
        {
            this.entryPattern = CreateEntryPattern();

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
            MicroDVDEntry otherMicroDVDEntry = other as MicroDVDEntry;
            if (otherMicroDVDEntry != null)
            {
                return (this.Text == otherMicroDVDEntry.Text) &&
                       base.Equals(otherMicroDVDEntry);
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
            MicroDVDEntry other = obj as MicroDVDEntry;
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
            return timing.ToString();
        }

        /// <summary>
        /// Metoda wywoływana w celu zaktualizowaniu zawartości tekstowej wpisu (Content),
        /// w oparciu o wartości posczególnych właściowości bieżącego obiektu.
        /// </summary>
        protected override void UpdateContent()
        {
            StringBuilder contentBuilder = new StringBuilder();
            contentBuilder.Append("{");
            contentBuilder.Append(GetFormattedTiming(TimingStart));
            contentBuilder.Append("}");
            contentBuilder.Append("{");
            contentBuilder.Append(GetFormattedTiming(TimingEnd));
            contentBuilder.Append("}");
            contentBuilder.Append(Text);

            this.Content = contentBuilder.ToString();
        }

        /// <summary>
        /// Parsuje zawartość tekstową, w celu pozyskania informacji o poszczególnych wartościach wpisu.
        /// </summary>
        private void ParseContent()
        {
            Match entryMatch = Regex.Match(Content, entryPattern);
            if (entryMatch.Success)
            {
                String timingStartValue = entryMatch.Groups["start"].Value;
                TimingStart = long.Parse(timingStartValue);

                String timingEndValue = entryMatch.Groups["end"].Value;
                TimingEnd = long.Parse(timingEndValue);

                String textValue = entryMatch.Groups["text"].Value;
                Text = textValue;
            }
        }

        private String CreateEntryPattern()
        {
            return @"{(?<start>\d+)}{(?<end>\d+)}(?<text>.*(\r\n|\r|\n)?)";
        }
    }
}
