using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Subeditor.Model.OrganizationalEntities
{
    /// <summary>
    /// 
    /// </summary>
    class VPlayerEntry : TimedEntry
    {
        private readonly String timeFormat;
        private readonly String framesEntryPattern;
        private readonly String timeEntryPattern;

        private String text;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="start"></param>
        public VPlayerEntry(String content, int start)
            : base(content, start)
        {
            this.timeFormat = CreateTimeFormat();
            this.framesEntryPattern = CreateFramesEntryPattern();
            this.timeEntryPattern = CreateTimeEntryPattern();

            ParseContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="start"></param>
        /// <param name="timingStart"></param>
        /// <param name="timingEnd"></param>
        public VPlayerEntry(String content, int start, long timingStart, long timingEnd) 
            : base(content, start, timingStart, timingEnd)
        {
            this.timeFormat = CreateTimeFormat();
            this.framesEntryPattern = CreateFramesEntryPattern();
            this.timeEntryPattern = CreateTimeEntryPattern();

            ParseContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <param name="start"></param>
        /// <param name="timingStart"></param>
        /// <param name="timingEnd"></param>
        public VPlayerEntry(String content, int start, TimeSpan timingStart, TimeSpan timingEnd)
            : base(content, start, timingStart, timingEnd)
        {
            this.timeFormat = CreateTimeFormat();
            framesEntryPattern = CreateFramesEntryPattern();
            timeEntryPattern = CreateTimeEntryPattern();

            ParseContent();
        }

        /// <summary>
        /// 
        /// </summary>
        public enum TimingFormatType
        {
            Time,
            Frames,
        }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        public TimingFormatType TimingFormat { get; set; }

        /// <summary>
        /// Porównuje czy jeden obiekt Entry jest równy drugiemu
        /// </summary>
        /// <param name="other">Obiekt Selection do porównania.</param>
        /// <returns>Równy czy nie.</returns>
        public override bool Equals(Entry other)
        {
            if (other is VPlayerEntry)
            {
                VPlayerEntry otherVPlayerEntry = other as VPlayerEntry;

                return (this.Text == otherVPlayerEntry.Text) &&
                       base.Equals(otherVPlayerEntry);
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
            if (obj is VPlayerEntry)
            {
                return Equals((VPlayerEntry)obj);
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
            return String.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void UpdateContent()
        {
            StringBuilder contentBuilder = new StringBuilder();
            if (TimingFormat == TimingFormatType.Frames)
            {
                contentBuilder.Append("{");
                contentBuilder.Append(TimingStart);
                contentBuilder.Append("}");
                contentBuilder.Append("{");
                contentBuilder.Append(TimingEnd);
                contentBuilder.Append("}");
            }
            else
            {
                contentBuilder.Append(FormatTiming(TimingStart));
                contentBuilder.Append(":");
            }
            contentBuilder.Append(Text);

            this.Content = contentBuilder.ToString();
        }

        private void ParseContent()
        {
            Match entryMatch = Regex.Match(Content, framesEntryPattern);
            if (entryMatch.Success)
            {
                TimingFormat = TimingFormatType.Frames;
            }
            else
            {
                entryMatch = Regex.Match(Content, timeEntryPattern);
                TimingFormat = TimingFormatType.Time;
            }

            if (entryMatch.Success)
            {
                String timingStartValue = entryMatch.Groups["start"].Value;
                TimingStart = (TimingFormat == TimingFormatType.Frames) ? (long.Parse(timingStartValue)) : (TimeSpan.Parse(timingStartValue).Ticks);

                String timingEndValue = entryMatch.Groups["end"].Value;
                TimingEnd = (TimingFormat == TimingFormatType.Frames) ? (long.Parse(timingEndValue)) : 0;

                String textValue = entryMatch.Groups["text"].Value;
                Text = textValue;
            }
        }

        private String CreateTimeFormat()
        {
            return "hh\\:mm\\:ss";
        }

        private String CreateFramesEntryPattern()
        {
            return @"{(?<start>\d+)}{(?<end>\d+)}(?<text>.*(\r\n|\r|\n)?)";
        }

        private String CreateTimeEntryPattern()
        {
            return @"(?<start>\d{2}:\d{2}:\d{2}):(?<text>.*(\r\n|\r|\n)?)";
        }

        private String FormatTiming(long timing)
        {
            return TimeSpan.FromTicks(timing).ToString(timeFormat);
        }
    }
}
