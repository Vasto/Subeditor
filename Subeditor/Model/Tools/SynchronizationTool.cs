using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using MathNet.Numerics.Interpolation;
using Subeditor.Model.Modifications;
using Subeditor.Model.OrganizationalEntities;
using Subeditor.Model.Tools.Strategies;

namespace Subeditor.Model.Tools
{
    /// <summary>
    /// Narzędzie służące do synchronizacji timingu napsiów.
    /// </summary>
    class SynchronizationTool : EditToolBase
    {
        /// <summary>
        /// Określa minimalną wymaganą licznę punktów, aby przeprowadzenie synchronizacji było możliwe.
        /// </summary>
        public const int MinimumPointsToSynchronize = 2;

        private Collection<SynchronizationPoint> synchronizationPoints;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="manager">Menadżer napisów.</param>
        /// <param name="editor">Edytor napisów</param>
        public SynchronizationTool(SubtitlesManager manager, SubtitlesEditor editor)
            : base(manager, editor)
        {
            this.synchronizationPoints = new Collection<SynchronizationPoint>();
        }

        /// <summary>
        /// Pozwala pobrać kolekcje punktów synchronizacji.
        /// </summary>
        public Collection<SynchronizationPoint> SynchronizationPoints
        {
            get 
            { 
                return synchronizationPoints; 
            }
        }

        /// <summary>
        /// Wykonuje operacje synchronizacji
        /// w oparciu o zawierane w kolekcji SynchronizationPoints, punkty synchronizacji.
        /// </summary>
        public void Synchronize()
        {
            var orderedSyncPoints = synchronizationPoints.OrderBy(point => point.OrginalTiming);
            var points = (from p in orderedSyncPoints select (double)p.OrginalTiming).ToArray();
            var values = (from p in orderedSyncPoints select (double)p.CorrectTiming).ToArray();
            var method = Interpolate.LinearBetweenPoints(points, values);

            IEditStrategy editStrategy = this.CreateStrategyForCurrentFormat();

            TimedEntry entry = null;
            while ((entry = editStrategy.NextTimedEntry()) != null)
            {
                entry.TimingStart = (long)method.Interpolate(entry.TimingStart);
                if (entry.UsesTimingEnd)
                {
                    entry.TimingEnd = (long)method.Interpolate(entry.TimingEnd);
                }

                editStrategy.SaveCurrentEntry();
            }

            SetEditorContent(editStrategy.Content);
        }

        private void SetEditorContent(String content)
        {
            SubtitlesContentModificationArea modificationArea =
                Editor.EditState.Selection.Length > 0 ? 
                SubtitlesContentModificationArea.Selection : 
                SubtitlesContentModificationArea.Entire;

            //Najpierw modyfikacja nowej zawrtości, a później stanu edycji 
            //aby po zmodyfikowaniu zawrtości stan edycji był taki sam jak przed.
            SubtitlesContentModification contentModification = new SubtitlesContentModification(content, SubtitlesContentModificationArea.Entire);
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
