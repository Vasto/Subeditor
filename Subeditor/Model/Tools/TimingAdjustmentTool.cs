using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Subeditor.Model.Tools;
using Subeditor.Model.Modifications;
using Subeditor.Model.OrganizationalEntities;
using Subeditor.Model.Tools.Strategies;

namespace Subeditor.Model.Tools
{
    /// <summary>
    /// Narzędzie służące do dostosowywania timingu napisów.
    /// </summary>
    class TimingAdjustmentTool : EditToolBase
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="manager">Menadżer napisów.</param>
        /// <param name="editor">Edytor napisów</param>
        public TimingAdjustmentTool(SubtitlesManager manager, SubtitlesEditor editor)
            : base(manager, editor)
        {

        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="manager">Menadżer napisów.</param>
        /// <param name="editor">Edytor napisów</param>
        /// <param name="strategyRepository">Obiekt repozytorium strategi.</param>
        public TimingAdjustmentTool(SubtitlesManager manager, SubtitlesEditor editor, StrategyForFormatRepository strategyRepository)
            : base(manager, editor, strategyRepository)
        {

        }

        /// <summary>
        /// Dodaje do timingu napisów podany czas.
        /// </summary>
        /// <param name="timing">Czas o jaki ma zostać zwiększony timing.</param>
        public void AddTiming(TimeSpan timing)
        {
            AddTiming(timing.Ticks);
        }

        /// <summary>
        /// Dodaje do timingu napisów podaną wartość.
        /// </summary>
        /// <param name="timing">Wartość o jaką ma zostać zwiększony timing.</param>
        public void AddTiming(long timing)
        {
            IEditStrategy editStrategy = this.CreateStrategyForCurrentFormat();

            TimedEntry entry = null;
            while ((entry = editStrategy.NextTimedEntry()) != null)
            {
                entry.TimingStart += timing;
                if (entry.UsesTimingEnd)
                {
                    entry.TimingEnd += timing;
                }

                editStrategy.SaveCurrentEntry();
            }

            SetEditorContent(editStrategy.Content);
        }

        /// <summary>
        /// Dodaje do timingu napisów podaną wartość.
        /// </summary>
        /// <param name="timing">Wartość o jaką ma zostać zwiększony timing.</param>
        /// <param name="timingFrom">Wartość od której ma się rozpocząć zmiana.</param>
        /// <param name="timingTo">Wrtość do której ma zostać przeprowadzona zmiana.</param>
        public void AddTiming(long timing, long timingFrom, long timingTo)
        {
            IEditStrategy editStrategy = this.CreateStrategyForCurrentFormat();

            TimedEntry entry = null;
            while ((entry = editStrategy.NextTimedEntry()) != null)
            {
                bool needSave = false;
                if ((entry.TimingStart >= timingFrom) && (entry.TimingStart <= timingTo))
                {
                    entry.TimingStart += timing;
                    needSave = true;
                }

                if (entry.UsesTimingEnd && (entry.TimingEnd >= timingFrom) && (entry.TimingEnd <= timingTo))
                {
                    entry.TimingEnd += timing;
                    needSave = true;
                }

                if (needSave)
                {
                    editStrategy.SaveCurrentEntry();
                }
            }

            SetEditorContent(editStrategy.Content);
        }


        /// <summary>
        ///  Dodaje do timingu napisów podaną wartość.
        /// </summary>
        /// <param name="timing">Wartość o jaką ma zostać zwiększony timing.</param>
        /// <param name="selectionStart">Indeks określający początek zaznaczenia.</param>
        /// <param name="selectionLength">Długość zazanaczenia wyrażona poprzez liczbę znaków.</param>
        /// <param name="selectionIncludesLastNewLine">Określa czy zaznaczenie sięgające do końca lini, będzie zawierać znak nowej lini.</param>
        public void AddTiming(long timing, int selectionStart, int selectionLength, bool selectionIncludesLastNewLine = false)
        {
            IEditStrategy editStrategy = this.CreateStrategyForCurrentFormat();

            TimedEntry entry = null;
            while ((entry = editStrategy.NextTimedEntry()) != null)
            {
                int entryStart = entry.Start;
                int entryEnd = entry.Start + entry.Length;
                int selectionEnd = selectionStart + selectionLength;
                
                //Zaznaczenie nie obejmuje znaku nowej lini dla ostatniego wpisu, więc w sytuacji kiedy sięga ono końca wpisu
                //istnieje konieczność zwiększenia jego długości o znak nowej lini, tak aby jego indeks końca nie mniejszy
                //niż indeks końca wpisu.
                if ((!selectionIncludesLastNewLine) && 
                    (entryEnd == selectionEnd + 2) && 
                    entry.Content.EndsWith(Environment.NewLine))
                {
                    selectionEnd += 2;
                }


                //Wpis znajduje się przed zaznaczeniem;
                if (entryEnd < selectionStart)
                {
                    continue;
                }
                //Wpis znajduje sie w całości w zaznaczeniu.
                else if ((entryStart >= selectionStart) && (entryEnd <= selectionEnd))
                {
                    entry.TimingStart += timing;
                    if (entry.UsesTimingEnd)
                    {
                        entry.TimingEnd += timing;
                    }

                    editStrategy.SaveCurrentEntry();
                }
                //Wpis przecina się z zaznaczeniem.
                else if (((entryStart < selectionStart) && (entryEnd <= selectionEnd)) ||
                        ((entryStart < selectionStart) && (entryEnd > selectionEnd)) ||
                        ((entryStart >= selectionStart) && (entryEnd < selectionEnd)))
                {
                    if (IsTimingStartInSelection(entry, selectionStart, selectionEnd))
                    {
                        entry.TimingStart += timing;
                    }

                    if (entry.UsesTimingEnd && IsTimingEndInSelection(entry, selectionStart, selectionEnd))
                    {
                        entry.TimingEnd += timing;
                    }
                    
                    editStrategy.SaveCurrentEntry();

                }
                //Wpis znajduje się za zaznaczeniem
                else if (entryStart > selectionEnd)
                {
                    break;
                }
            }

            SetEditorContent(editStrategy.Content);
        }

        /// <summary>
        /// Odejmuje od timingu napisów podany czas.
        /// </summary>
        /// <param name="timing">Czas o jaki zostanie zmniejszony timing.</param>
        public void SubstractTiming(TimeSpan timing)
        {
            SubstractTiming(timing.Ticks);
        }

        /// <summary>
        ///  Odejmuje od timingu napisów podaną wartość.
        /// </summary>
        /// <param name="timing">Wartość o jaką zostanie zmniejszony timing.</param>
        public void SubstractTiming(long timing)
        {
            IEditStrategy editStrategy = this.CreateStrategyForCurrentFormat();

            TimedEntry entry = null;
            while ((entry = editStrategy.NextTimedEntry()) != null)
            {
                entry.TimingStart = entry.TimingStart - timing < 0 ? 0 : entry.TimingStart - timing;
                if (entry.UsesTimingEnd)
                {
                    entry.TimingEnd = entry.TimingEnd - timing < 0 ? 0 : entry.TimingEnd - timing; 
                }

                editStrategy.SaveCurrentEntry();
            }

            SetEditorContent(editStrategy.Content);
        }

        /// <summary>
        ///  Odejmuje od timingu napisów podaną wartość.
        /// </summary>
        /// <param name="timing">Wartość o jaką zostanie zmniejszony timing.</param>
        /// <param name="timingFrom">Wartość od której ma się rozpocząć zmiana.</param>
        /// <param name="timingTo">Wrtość do której ma zostać przeprowadzona zmiana.</param>
        public void SubstractTiming(long timing, long timingFrom, long timingTo)
        {
            IEditStrategy editStrategy = this.CreateStrategyForCurrentFormat();

            TimedEntry entry = null;
            while ((entry = editStrategy.NextTimedEntry()) != null)
            {
                bool needSave = false;
                if ((entry.TimingStart >= timingFrom) && (entry.TimingStart <= timingTo))
                {
                    entry.TimingStart = entry.TimingStart - timing < 0 ? 0 : entry.TimingStart - timing;
                    needSave = true;
                }

                if (entry.UsesTimingEnd && (entry.TimingEnd >= timingFrom) && (entry.TimingEnd <= timingTo))
                {
                    entry.TimingEnd = entry.TimingEnd - timing < 0 ? 0 : entry.TimingEnd - timing;
                    needSave = true;
                }

                if (needSave)
                {
                    editStrategy.SaveCurrentEntry();
                }
            }

            SetEditorContent(editStrategy.Content);
        }

        /// <summary>
        ///  Odejmuje od timingu napisów podaną wartość.
        /// </summary>
        /// <param name="timing">Wartość o jaką ma zostać zmniejszony timing.</param>
        /// <param name="selectionStart">Indeks określający początek zaznaczenia.</param>
        /// <param name="selectionLength">Długość zazanaczenia wyrażona poprzez liczbę znaków.</param>
        /// <param name="selectionIncludesLastNewLine">Określa czy zaznaczenie sięgające do końca lini, będzie zawierać znak nowej lini.</param>
        public void SubstractTiming(long timing, int selectionStart, int selectionLength, bool selectionIncludesLastNewLine = false)
        {
            IEditStrategy editStrategy = this.CreateStrategyForCurrentFormat();

            TimedEntry entry = null;
            while ((entry = editStrategy.NextTimedEntry()) != null)
            {
                int entryStart = entry.Start;
                int entryEnd = entry.Start + entry.Length;
                int selectionEnd = selectionStart + selectionLength;

                //Zaznaczenie nie obejmuje znaku nowej lini dla ostatniego wpisu, więc w sytuacji kiedy sięga ono końca wpisu
                //istnieje konieczność zwiększenia jego długości o znak nowej lini, tak aby jego indeks końca nie mniejszy
                //niż indeks końca wpisu.
                if ((!selectionIncludesLastNewLine) &&
                    (entryEnd == selectionEnd + 2) &&
                    entry.Content.EndsWith(Environment.NewLine))
                {
                    selectionEnd += 2;
                }


                //Wpis znajduje się przed zaznaczeniem;
                if (entryEnd < selectionStart)
                {
                    continue;
                }
                //Wpis znajduje sie w całości w zaznaczeniu.
                else if ((entryStart >= selectionStart) && (entryEnd <= selectionEnd))
                {
                    entry.TimingStart = entry.TimingStart - timing < 0 ? 0 : entry.TimingStart - timing;
                    if (entry.UsesTimingEnd)
                    {
                        entry.TimingEnd = entry.TimingEnd - timing < 0 ? 0 : entry.TimingEnd - timing;
                    }

                    editStrategy.SaveCurrentEntry();
                }
                //Wpis przecina się z zaznaczeniem.
                else if (((entryStart < selectionStart) && (entryEnd <= selectionEnd)) ||
                        ((entryStart < selectionStart) && (entryEnd > selectionEnd)) ||
                        ((entryStart >= selectionStart) && (entryEnd < selectionEnd)))
                {
                    if (IsTimingStartInSelection(entry, selectionStart, selectionEnd))
                    {
                        entry.TimingStart = entry.TimingStart - timing < 0 ? 0 : entry.TimingStart - timing;
                    }

                    if ((entry.UsesTimingEnd) && (IsTimingEndInSelection(entry, selectionStart, selectionEnd)))
                    {
                        entry.TimingEnd = entry.TimingEnd - timing < 0 ? 0 : entry.TimingEnd - timing;
                    }

                    editStrategy.SaveCurrentEntry();

                }
                //Wpis znajduje się za zaznaczeniem
                else if (entryStart > selectionEnd)
                {
                    break;
                }
            }

            SetEditorContent(editStrategy.Content);
        }

        /// <summary>
        /// Określa czy wartość właściwości TimingStart, wpisu typu TimedEntry mieści się w określonym zaznaczeniu.
        /// </summary>
        private bool IsTimingStartInSelection(TimedEntry entry, int selectionStart, int selectionEnd)
        {
            //Znajdujemy indeks początku timingu w zawartosci wpisu.
            int timingEntryContentIndex = entry.Content.IndexOf(entry.FormattedTimingStart);
            if (timingEntryContentIndex < 0)
            {
                return false;
            }

            int timingStartIndex = entry.Start + timingEntryContentIndex;
            int timingEndIndex = timingStartIndex + entry.FormattedTimingStart.Length;

            return (timingStartIndex >= selectionStart) && (timingEndIndex <= selectionEnd);
        }

        /// <summary>
        /// Określa czy wartość właściwości TimingEnd, wpisu typu TimedEntry mieści się w określonym zaznaczeniu.
        /// </summary>
        private bool IsTimingEndInSelection(TimedEntry entry, int selectionStart, int selectionEnd)
        {
            //Znajdujemy indeks początku timingu w zawartosci wpisu.
            int timingEntryContentIndex = entry.Content.IndexOf(entry.FormattedTimingEnd);
            if (timingEntryContentIndex < 0)
            {
                return false;
            }

            int timingStartIndex = entry.Start + timingEntryContentIndex;
            int timingEndIndex = timingStartIndex + entry.FormattedTimingEnd.Length;

            return (timingStartIndex >= selectionStart) && (timingEndIndex <= selectionEnd);
        }

        private void SetEditorContent(String content)
        {
            SubtitlesContentModificationArea modificationArea = 
                (Editor.EditState.Selection.Length > 0) ? SubtitlesContentModificationArea.Selection : SubtitlesContentModificationArea.Entire;

            //Najpierw modyfikacja nowej zawrtości, a później stanu edycji 
            //aby po zmodyfikowaniu zawrtości stan edycji był taki sam jak przed.
            SubtitlesContentModification contentModification = new SubtitlesContentModification(content, modificationArea);
            SubtitlesEditStateModification editStateModification = new SubtitlesEditStateModification(Editor.EditState);

            ModificationComposer composer = new ModificationComposer();
            composer.Begin();
            composer.Add(contentModification);
            composer.Add(editStateModification);
            var modification = composer.End();

            this.Editor.PerformModification(modification);
        }

    }
}
