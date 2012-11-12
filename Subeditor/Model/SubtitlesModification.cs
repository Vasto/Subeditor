using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiffMatchPatch;
using Subeditor.Model.UndoRedo;

namespace Subeditor.Model
{
    class SubtitlesModification : IUndoableRedoable
    {
        #region IUndoableRedoable

        /// <summary>
        /// Wycofuje modyfikacje.
        /// </summary>
        public void Undo()
        {
            foreach (var change in contentChanges.Reverse())
            {
                Diff differenceItem = change.Item1;
                int differneceStartIndex = change.Item2;

                if (differenceItem.operation == Operation.INSERT)
                {
                    DeleteDifference(differenceItem, differneceStartIndex);
                }
                else if (differenceItem.operation == Operation.DELETE)
                {
                    InsertDifference(differenceItem, differneceStartIndex);
                }
            }

            RestoreSelection();
        }

        /// <summary>
        /// Przywraca modyfikacje.
        /// </summary>
        public void Redo()
        {
            foreach (var change in contentChanges)
            {
                Diff differenceItem = change.Item1;
                int differneceStartIndex = change.Item2;

                if (differenceItem.operation == Operation.INSERT)
                {
                    InsertDifference(differenceItem, differneceStartIndex);
                }
                else if (differenceItem.operation == Operation.DELETE)
                {
                    DeleteDifference(differenceItem, differneceStartIndex);
                }
            }

            RestoreSelection();
        }

        #endregion //IUndoableRedoable

        private SubtitlesEditor subtitlesEditor;
        private SubtitlesEditState newEditState;
        private SubtitlesEditState oldEditState;
        private Selection currentSelection;
        private IEnumerable<Tuple<Diff, int>> contentChanges;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="subtitlesEditor"></param>
        /// <param name="oldContent"></param>
        /// <param name="newContent"></param>
        /// <param name="oldEditState"></param>
        /// <param name="newEditState"></param>
        public SubtitlesModification(SubtitlesEditor subtitlesEditor, String oldContent, String newContent, 
            SubtitlesEditState oldEditState, SubtitlesEditState newEditState)
        {
            this.subtitlesEditor = subtitlesEditor;
            this.newEditState = newEditState;
            this.oldEditState = oldEditState;
            this.currentSelection = newEditState.Selection;

            String oldContentTrimed = TrimOldContent(oldContent);
            String newContentTrimed = TrimNewContent(newContent);

            this.contentChanges = GetContentChanges(oldContentTrimed, newContentTrimed);
        }

        private String TrimOldContent(String oldContent)
        {
            return oldContent.Substring(0, oldEditState.CaretPosition);
        }

        private String TrimNewContent(String newContent)
        {
            return newContent.Substring(0, newEditState.CaretPosition);
        }

        private IEnumerable<Tuple<Diff, int>> GetContentChanges(String oldContent, String newContent)
        {
            List<Tuple<Diff, int>> result = new List<Tuple<Diff, int>>();
            List<Diff> differences = (new diff_match_patch()).diff_main(oldContent, newContent);
            int currentIndex = 0;

            for (int i = 0; i < differences.Count; ++i)
            {
                Diff currentDifferenceItem = differences[i];
                if (currentDifferenceItem.operation == Operation.EQUAL)
                {
                    currentIndex += currentDifferenceItem.text.Length;
                }
                else if (currentDifferenceItem.operation == Operation.INSERT)
                {
                    result.Add(Tuple.Create<Diff, int>(currentDifferenceItem, currentIndex));

                    currentIndex += currentDifferenceItem.text.Length;
                }
                else if (currentDifferenceItem.operation == Operation.DELETE)
                {
                    result.Add(Tuple.Create<Diff, int>(currentDifferenceItem, currentIndex));
                }
            }

            return result;
        }

        private void InsertDifference(Diff differenceItem, int differneceStartIndex)
        {
            String content = subtitlesEditor.SubtitlesContent;
            content = content.Insert(differneceStartIndex, differenceItem.text);

            subtitlesEditor.SubtitlesContent = content;

            SubtitlesEditState currentEditState = subtitlesEditor.EditState;
            SubtitlesEditState newEditState = new SubtitlesEditState(differneceStartIndex + differenceItem.text.Length, currentEditState.Selection);
        }

        private void DeleteDifference(Diff differenceItem, int differneceStartIndex)
        {
            String content = subtitlesEditor.SubtitlesContent;
            content = content.Remove(differneceStartIndex, differenceItem.text.Length);

            subtitlesEditor.SubtitlesContent = content;

            SubtitlesEditState currentEditState = subtitlesEditor.EditState;
            SubtitlesEditState newEditState = new SubtitlesEditState(differneceStartIndex, currentEditState.Selection);
        }

        private void RestoreSelection()
        {
            if (currentSelection == newEditState.Selection)
            {
                currentSelection = oldEditState.Selection;
                subtitlesEditor.SelectContent(currentSelection.Start , currentSelection.Length);
            }
            else if (currentSelection == oldEditState.Selection)
            {
                currentSelection = newEditState.Selection;
                subtitlesEditor.SelectContent(currentSelection.Start, currentSelection.Length);
            }
        }
    }
}
