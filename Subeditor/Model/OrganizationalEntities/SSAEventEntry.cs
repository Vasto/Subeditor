﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Subeditor.Model.OrganizationalEntities
{
    /// <summary>
    /// Reprezentuje zdarzenie w napisach typu SSA.
    /// </summary>
    class SSAEventEntry : TimedEntry
    {
        private static readonly String timeFormat = "h\\:mm\\:ss\\.ff";
        private static readonly String marginFormat = "0000";
        private static readonly String eventPattern = CreateEventPattern();
        private static readonly Regex eventRegex = new Regex(eventPattern, RegexOptions.Compiled);

        /// <summary>
        /// Tworzy wzór zdarzenia dla wyrażenia regularengo, mającego dopasować bieżący wpis.
        /// </summary>
        /// <returns></returns>
        private static String CreateEventPattern()
        {
            StringBuilder patternBuilder = new StringBuilder();

            patternBuilder.Append(@"(?<format>(?:Dialogue|Comment|Picture|Sound|Movie|Command)):\s");
            patternBuilder.Append(@"Marked=(?<marked>\d+),");
            patternBuilder.Append(@"(?<start>\d{1,2}:\d{2}:\d{2}\.\d{2}),");
            patternBuilder.Append(@"(?<end>\d{1,2}:\d{2}:\d{2}\.\d{2}),");
            patternBuilder.Append(@"(?<style>.*?),");
            patternBuilder.Append(@"(?<name>.*?),");
            patternBuilder.Append(@"(?<marginL>\d{1,4}),");
            patternBuilder.Append(@"(?<marginR>\d{1,4}),");
            patternBuilder.Append(@"(?<marginV>\d{1,4}),");
            patternBuilder.Append(@"(?<effect>.*?),");
            patternBuilder.Append(@"(?<text>.*(?:\r\n?|\r?|\n?))");

            return patternBuilder.ToString();
        }

        private FormatType format;
        private int marked;
        private String style;
        private String name;
        private int marginL;
        private int marginR;
        private int marginV;
        private String effect;
        private String text;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="content">Tekst zawierany przez wpis.</param>
        /// <param name="start">Indeks początkowy wpisu.</param>
        public SSAEventEntry(String content, int start)
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
        public SSAEventEntry(String content, int start, long timingStart, long timingEnd)
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
        public SSAEventEntry(String content, int start, TimeSpan timingStart, TimeSpan timingEnd)
            : base(content, start, timingStart, timingEnd)
        {
            this.ParseContent();
        }

        /// <summary>
        /// Reperezentuje formaty zdarzenia w napisach SSA.
        /// </summary>
        public enum FormatType
        {
            Dialogue,
            Comment,
            Picture,
            Sound,
            Movie,
            Command,
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
        /// Pozwala pobrać lub ustawić format zdarzenia.
        /// </summary>
        public FormatType Format
        {
            get
            {
                return format;
            }
            set
            {
                format = value;
                ContentNeedUpdate = true;
                OnContentChanged();
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić warstwę.
        /// </summary>
        public int Marked
        {
            get
            {
                return marked;
            }
            set
            {
                marked = value;
                ContentNeedUpdate = true;
                OnContentChanged();
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić informację o stylu.
        /// </summary>
        public String Style
        {
            get
            {
                return style;
            }
            set
            {
                style = value;
                ContentNeedUpdate = true;
                OnContentChanged();
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić informację o nazwie.
        /// </summary>
        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                ContentNeedUpdate = true;
                OnContentChanged();
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić informację o marginesie z lewej strony.
        /// </summary>
        public int MarginL
        {
            get
            {
                return marginL;
            }
            set
            {
                marginL = value;
                ContentNeedUpdate = true;
                OnContentChanged();
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić informację o marginesie z prwej strony.
        /// </summary>
        public int MarginR
        {
            get
            {
                return marginR;
            }
            set
            {
                marginR = value;
                ContentNeedUpdate = true;
                OnContentChanged();
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić informację o marginesie z prwej strony.
        /// </summary>
        public int MarginV
        {
            get
            {
                return marginV;
            }
            set
            {
                marginV = value;
                ContentNeedUpdate = true;
                OnContentChanged();
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić informację o marginesie wertykalnym.
        /// </summary>
        public String Effect
        {
            get
            {
                return effect;
            }
            set
            {
                effect = value;
                ContentNeedUpdate = true;
                OnContentChanged();
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić przechowywany tekst zdarzenia.
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
            SSAEventEntry otherSSAEventEntry = other as SSAEventEntry;
            if (otherSSAEventEntry != null)
            {
                return (this.Format == otherSSAEventEntry.Format) &&
                       (this.Marked == otherSSAEventEntry.Marked) &&
                       (this.Style == otherSSAEventEntry.Style) &&
                       (this.Name == otherSSAEventEntry.Name) &&
                       (this.MarginL == otherSSAEventEntry.MarginL) &&
                       (this.MarginR == otherSSAEventEntry.MarginR) &&
                       (this.MarginV == otherSSAEventEntry.MarginV) &&
                       (this.Effect == otherSSAEventEntry.Effect) &&
                       (this.Text == otherSSAEventEntry.Text) &&
                       base.Equals(otherSSAEventEntry);
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
            SSAEventEntry other = obj as SSAEventEntry;
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
            return Format.GetHashCode() +
                   Marked.GetHashCode() +
                   Style.GetHashCode() +
                   Name.GetHashCode() +
                   MarginL.GetHashCode() +
                   MarginR.GetHashCode() +
                   MarginV.GetHashCode() +
                   Effect.GetHashCode() +
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

            content.Append(Format.ToString());
            content.Append(": Marked=");
            content.Append(Marked.ToString());
            content.Append(",");
            content.Append(GetFormattedTiming(TimingStart));
            content.Append(",");
            content.Append(GetFormattedTiming(TimingEnd));
            content.Append(",");
            content.Append(Style);
            content.Append(",");
            content.Append(Name);
            content.Append(",");
            content.Append(MarginL.ToString(marginFormat));
            content.Append(",");
            content.Append(MarginR.ToString(marginFormat));
            content.Append(",");
            content.Append(MarginV.ToString(marginFormat));
            content.Append(",");
            content.Append(Effect);
            content.Append(",");
            content.Append(Text);

            Content = content.ToString();
        }

        /// <summary>
        /// Parsuje zawartość tekstową, w celu pozyskania informacji o poszczególnych wartościach wpisu.
        /// </summary>
        private void ParseContent()
        {
            Match eventMatch = eventRegex.Match(Content);
            if (eventMatch.Success)
            {
                String formatValue = eventMatch.Groups["format"].Value;
                Format = (FormatType)Enum.Parse(typeof(FormatType), formatValue);

                String layerValue = eventMatch.Groups["marked"].Value;
                Marked = int.Parse(layerValue);

                String timingStartValue = eventMatch.Groups["start"].Value;
                TimingStart = TimeSpan.Parse(timingStartValue).Ticks;

                String timingEndValue = eventMatch.Groups["end"].Value;
                TimingEnd = TimeSpan.Parse(timingEndValue).Ticks;

                String styleValue = eventMatch.Groups["style"].Value;
                Style = styleValue;

                String nameValue = eventMatch.Groups["name"].Value;
                Name = nameValue;

                String marginLValue = eventMatch.Groups["marginL"].Value;
                MarginL = int.Parse(marginLValue);

                String marginRValue = eventMatch.Groups["marginR"].Value;
                MarginR = int.Parse(marginRValue);

                String marginVValue = eventMatch.Groups["marginV"].Value;
                MarginV = int.Parse(marginVValue);

                String effectValue = eventMatch.Groups["effect"].Value;
                Effect = effectValue;

                String textValue = eventMatch.Groups["text"].Value;
                Text = textValue;

                ContentNeedUpdate = false;
            }
            else
            {
                throw new Exception("Cannot parse entry.");
            }
        }


    }
}
