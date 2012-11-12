using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Model.OrganizationalEntities
{
    /// <summary>
    /// Reprezentuje wpis w napisach, usytuowany w czasie (posiada timing).
    /// </summary>
    abstract class TimedEntry : Entry
    {
        private long timingStart;
        private long timingEnd;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="content">Tekst zawierany przez wpis.</param>
        /// <param name="start">Indeks początkowy wpisu.</param>
        public TimedEntry(String content, int start)
            : base(content, start)
        {
            this.TimingStart = 0;
            this.TimingEnd = 0;
            //Content jest podawany jako parametr więc zakładamy że nie budujemy nowego,
            //gdyż tworzące go parametry będą z nim początkowo zgodne.
            this.ContentNeedUpdate = false;
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="content">Tekst zawierany przez wpis.</param>
        /// <param name="start">Indeks początkowy wpisu.</param>
        /// <param name="timingStart">Czas początku.</param>
        /// <param name="timingEnd">Czas końca.</param>
        public TimedEntry(String content, int start, long timingStart, long timingEnd) 
            : base(content, start)
        {
            this.TimingStart = timingStart;
            this.TimingEnd = timingEnd;
            this.ContentNeedUpdate = false;
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="content">Tekst zawierany przez wpis.</param>
        /// <param name="start">Indeks początkowy wpisu.</param>
        /// <param name="timingStart">Czas początku.</param>
        /// <param name="timingEnd">Czas końca.</param>
        public TimedEntry(String content, int start, TimeSpan timingStart, TimeSpan timingEnd)
            : base(content, start)
        {
            this.TimingStart = timingStart.Ticks;
            this.TimingEnd = timingEnd.Ticks;
            this.ContentNeedUpdate = false;
        }

        /// <summary>
        /// Czas początku.
        /// Określa czas, w którym wpis pojawi się na ekranie w trakcie wyświetlanie wideo.
        /// </summary>
        /// <remarks>
        /// Wartość w zależności od reprezentowanego wpsiu może być 
        /// liczbą ticks'ów czasu lub liczbą klatek.
        /// </remarks>
        public long TimingStart
        {
            get
            {
                return timingStart;
            }
            set
            {
                timingStart = value;
                ContentNeedUpdate = true;
                OnContentChanged();
            }
        }

        /// <summary>
        /// Czas końca.
        /// Określa czas, w którym wpis zniknie z ekranu w trakcie wyświetlanie wideo.
        /// </summary>
        /// <remarks>
        /// Wartość w zależności od reprezentowanego wpsiu może być 
        /// liczbą ticks'ów czasu lub liczbą klatek.
        /// </remarks>
        public long TimingEnd
        {
            get
            {
                return timingEnd;
            }
            set
            {
                timingEnd = value;
                ContentNeedUpdate = true;
                OnContentChanged();
            }
        }

        /// <summary>
        /// Zwraca czas początku sformatowany do postaci wykorzysytwanej przez dany typ napsiów,
        /// który jest powiązany z typem bieżącego wpsiu.
        /// </summary>
        public String FormattedTimingStart
        {
            get 
            {
                return GetFormattedTiming(timingStart); 
            }
        }

        /// <summary>
        /// Zwraca czas końca sformatowany do postaci wykorzysytwanej przez dany typ napsiów,
        /// który jest powiązany z typem bieżącego wpsiu.
        /// </summary>
        public String FormattedTimingEnd
        {
            get 
            { 
                return GetFormattedTiming(timingEnd); 
            }
        }

        /// <summary>
        /// Pozwala pobrać informację czy typ wykorzystuje timing końcowy.
        /// </summary>
        public abstract bool UsesTimingEnd 
        { 
            get; 
        }

        /// <summary>
        /// Pozwala ustawić timing wpisu.
        /// </summary>
        /// <param name="start">Czas początku.</param>
        /// <param name="end">Czas końca.</param>
        public void SetTiming(long start, long end)
        {
            TimingStart = start;
            TimingEnd = end;
        }

        /// <summary>
        /// Porównuje czy jeden obiekt Entry jest równy drugiemu
        /// </summary>
        /// <param name="other">Obiekt Selection do porównania.</param>
        /// <returns>Równy czy nie.</returns>
        public override bool Equals(Entry other)
        {
            TimedEntry otherTimedEntry = other as TimedEntry;
            if (otherTimedEntry != null)
            {
                return (this.TimingStart == otherTimedEntry.TimingStart) &&
                       (this.TimingEnd == otherTimedEntry.TimingEnd) &&
                       base.Equals(otherTimedEntry);
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
            TimedEntry other = obj as TimedEntry;
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
            return TimingStart.GetHashCode() + TimingEnd.GetHashCode() + base.GetHashCode();
        }

        /// <summary>
        /// Zwraca timing sformatowany w sposób właściwy dla danego typy napisów.
        /// </summary>
        /// <param name="timing">Timing podany w niezależnych jednostkach.</param>
        /// <returns>Sformatowany timing.</returns>
        protected abstract String GetFormattedTiming(long timing);

    }
}
