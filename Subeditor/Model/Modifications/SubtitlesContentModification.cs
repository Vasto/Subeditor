using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiffMatchPatch;
using Subeditor.Model.StateManagement;

namespace Subeditor.Model.Modifications
{
    /// <summary>
    /// Modyfikacja zawratości tekstowej napisów.
    /// </summary>
    class SubtitlesContentModification 
        : IModification, IModificationSource<SubtitlesEditor>, IGroupableModification, IUndoableRedoable, IEquatable<SubtitlesContentModification>
    {
        private String newContent;
        private SubtitlesEditState newState;
        //private bool preCaretModification;
        private ICollection<Tuple<Diff, int>> contentChanges;
        private bool modificationPerformed;
        private SubtitlesContentModificationArea modificationArea;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="newContent">Nowy tekst napisów.</param>
        /// <param name="area">Określa obszar zawartości tekstowej napisów na jakim zaszła modyfikacja.</param>
        public SubtitlesContentModification(String newContent, SubtitlesContentModificationArea area)
        {
            this.newContent = newContent;
            this.modificationArea = area;
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="newContent">Nowy tekst napisów.</param>
        /// <param name="newState">Nowy stan edytowania napisów.</param>
        /// <param name="area">Określa obszar zawartości tekstowej napisów na jakim zaszła modyfikacja.</param>
        public SubtitlesContentModification(String newContent, SubtitlesEditState newState, SubtitlesContentModificationArea area)
        {
            this.newContent = newContent;
            this.newState = newState;
            this.modificationArea = area;
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="editor">Obiekt edytora napisów.</param>
        /// <param name="newContent">Nowy tekst napisów.</param>
        /// <param name="area">Określa obszar zawartości tekstowej napisów na jakim zaszła modyfikacja.</param>
        public SubtitlesContentModification(SubtitlesEditor editor, String newContent, SubtitlesContentModificationArea area)
        {
            this.ModificationTarget = editor;
            this.newContent = newContent;
            this.modificationArea = area;
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="editor">Obiekt edytora napisów.</param>
        /// <param name="newContent">Nowy tekst napisów.</param>
        /// <param name="newState">Nowy stan edytowania napisów.</param>
        /// <param name="area">Określa obszar zawartości tekstowej napisów na jakim zaszła modyfikacja.</param>
        public SubtitlesContentModification(SubtitlesEditor editor, String newContent, SubtitlesEditState newState, SubtitlesContentModificationArea area)
        {
            this.ModificationTarget = editor;
            this.newContent = newContent;
            this.newState = newState;
            this.modificationArea = area;
        }

        /// <summary>
        /// Porównuje czy jeden obiekt SubtitlesContentModification jest równy drugiemu.
        /// </summary>
        /// <param name="other">Obiekt Selection do porównania.</param>
        /// <returns>Równy czy nie.</returns>
        public bool Equals(SubtitlesContentModification other)
        {
            if ((other != null) && 
                (this.contentChanges.Count == other.contentChanges.Count))
            {
                //Modyfikcacje zawartości powinny cechować tylko dokonanae zmiany, cel jest nie istotny...
                //Reszta zmiennych jest potrzebna zeby uzyskać liste zmian.
                var thisChangesSet = new HashSet<Tuple<Diff, int>>(this.contentChanges);
                bool areEquals = thisChangesSet.SetEquals(other.contentChanges);

                return areEquals;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Porównuje czy jeden System.Object jest równy drugiemu.
        /// </summary>
        /// <param name="o">System.Object do porównania.</param>
        /// <returns>Równy czy nie.</returns>
        public override bool Equals(object obj)
        {
            SubtitlesContentModification other = obj as SubtitlesContentModification;
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
            int hashcode = 141;
            if (contentChanges != null)
            {
                foreach (var change in contentChanges)
                {
                    hashcode += change.GetHashCode();
                }
            }

            return hashcode;
        }

        #region IModification

        /// <summary>
        /// Wykonuje modyfikację.
        /// </summary>
        public void Perform()
        {
            if (ModificationTarget == null)
            {
                throw new Exception();
            }

            if (modificationPerformed)
            {
                return;
            }

            if (newState == null)
            {
                newState = TryGetModifiedEditState();
            }

            String oldContent = ModificationTarget.SubtitlesContent;
            String oldChangedContent = oldContent;
            String newChangedContent = newContent;

            //W przypadku ucięcia części zawartości przed obszarem zmian, ma za zadania przechowywać długość uciętego tekstu,
            //tak aby indeksy pozycji poszczególnych zmian były prawidłowe dla pełnej, nieskróconej zawartości.
            int initialOffset = 0;

            if (modificationArea == SubtitlesContentModificationArea.PreCaret)
            {
                //Potrzebuje nowy stan edycji
                if (newState != null)
                {
                    SubtitlesEditState oldState = ModificationTarget.EditState;

                    oldChangedContent = GetContentChangeArea(oldContent, 0, oldState.CaretPosition);
                    newChangedContent = GetContentChangeArea(newContent, 0, newState.CaretPosition);
                }

            }
            else if (modificationArea == SubtitlesContentModificationArea.PostCaret)
            {
                //Nie potrzebuje nowego stanu edycji, zakładamy że część przed karetką jest jednakowa.
                SubtitlesEditState oldState = ModificationTarget.EditState;
                int selectionStart = oldState.Selection.Start;

                oldChangedContent = GetContentChangeArea(oldContent, selectionStart, oldContent.Length - selectionStart);
                newChangedContent = GetContentChangeArea(newContent, selectionStart, newContent.Length - selectionStart);

                initialOffset = oldState.Selection.Start;
            }
            else if (modificationArea == SubtitlesContentModificationArea.Selection)
            {
                //W przypadku zaznaczenia na pewno można odpuścic zawartość przed początkiem zaznaczenia
                //co z reszta? 
                //- modyfikacja mogła wprowadzić zmianą dłuższą niż pierwotna długość zaznaczenia.
                //- a także krótszą

                //By wyodrębnić obszar zmian w przypadku modyfikacji zaznaczenia konieczne jest znanie staniu edycji po modyfikacji.
                if (newState != null)
                {
                    SubtitlesEditState oldState = ModificationTarget.EditState;

                    //Długość obszaru zmian dla nowej zawartości, zależy od tego czy po modyfikacji będzie istniało zaznaczenie.
                    //Jeśli nie będzie istnieć, długość jest obliczana na podstawie zmian długości zawartości z przed modyfikacji i po modyfikacji.
                    int newChangedContentLength;
                    if (newState.Selection.Length > 0)
                    {
                        newChangedContentLength = newState.Selection.Length;
                    }
                    else
                    {
                        int contentLengthDelta = newContent.Length - oldContent.Length;
                        newChangedContentLength = oldState.Selection.Length + contentLengthDelta;
                    }

                    oldChangedContent = GetContentChangeArea(oldContent, oldState.Selection.Start, oldState.Selection.Length);
                    newChangedContent = GetContentChangeArea(newContent, oldState.Selection.Start, newChangedContentLength);

                    initialOffset = oldState.Selection.Start;
                }
            }
            else if (modificationArea == SubtitlesContentModificationArea.CaretOrSelectionEnd)
            {
                //By wyodrębnić obszar zmian w przypadku modyfikacji zaznaczenia konieczne jest znanie staniu edycji po modyfikacji.
                if (newState != null)
                {
                    SubtitlesEditState oldState = ModificationTarget.EditState;

                    int oldChangedContentLength = 0;
                    //Sprawdzamy czy pozycja karetki jest bardziej odległa, czy też końca zaznaczenia.
                    if ((oldState.CaretPosition) >= (oldState.Selection.Start + oldState.Selection.Length))
                    {
                        oldChangedContentLength = oldState.CaretPosition;
                    }
                    else
                    {
                        oldChangedContentLength = oldState.Selection.Start + oldState.Selection.Length;
                    }

                    int contentLengthDelta = newContent.Length - oldContent.Length;
                    int newChangedContentLength = oldChangedContentLength + contentLengthDelta;

                    oldChangedContent = GetContentChangeArea(oldContent, 0, oldChangedContentLength);
                    newChangedContent = GetContentChangeArea(newContent, 0, newChangedContentLength);
                }
            }

            contentChanges = GetContentChanges(oldChangedContent, newChangedContent, initialOffset);

            ModificationTarget.SubtitlesContent = newContent;

            //Nie przechowujemy zawartości, gdyż po zaplikowaniu innych modyfikacji zawartości, nie bedzie aktualna,
            //w celu oszczędności pamięci wystarczy przechowywać róznice.
            newContent = String.Empty;

            modificationPerformed = true;
        }

        #endregion //IModification

        #region IModificationTarget

        /// <summary>
        /// Pozwala ustawić lub pobrać informację o docelowo modyfikowanym obiekcie.
        /// </summary>
        public SubtitlesEditor ModificationTarget { get; set; }

        #endregion //IModificationTarget

        #region IGroupableModification

        /// <summary>
        /// Pozwala ustawić lub pobrać informację o grupie do jakiej należy modyfiacja.
        /// </summary>
        public ModificationGroup Group { get; set; }

        /// <summary>
        /// Pozwala ustawić lub pobrać informację o indeksie jaki ma bieżąca modyfikacja w grupie, do której przynależy.
        /// </summary>
        public int GroupIndex { get; set; }

        #endregion //IGroupableModification

        #region IUndoableRedoable

        /// <summary>
        /// Cofa modyfikację.
        /// </summary>
        public void Undo()
        {
            if (ModificationTarget == null)
            {
                throw new Exception();
            }

            StringBuilder contentBuilder = new StringBuilder(ModificationTarget.SubtitlesContent);

            foreach (var change in contentChanges.Reverse())
            {
                Diff differenceItem = change.Item1;
                int differneceStartIndex = change.Item2;

                if (differenceItem.operation == Operation.INSERT)
                {
                    DeleteDifference(contentBuilder, differenceItem, differneceStartIndex);
                }
                else if (differenceItem.operation == Operation.DELETE)
                {
                    InsertDifference(contentBuilder, differenceItem, differneceStartIndex);
                }
            }

            ModificationTarget.SubtitlesContent = contentBuilder.ToString();
        }

        /// <summary>
        /// Przywraca modyfikację.
        /// </summary>
        public void Redo()
        {
            if (ModificationTarget == null)
            {
                throw new Exception();
            }

            StringBuilder contentBuilder = new StringBuilder(ModificationTarget.SubtitlesContent);

            foreach (var change in contentChanges)
            {
                Diff differenceItem = change.Item1;
                int differneceStartIndex = change.Item2;

                if (differenceItem.operation == Operation.INSERT)
                {
                    InsertDifference(contentBuilder, differenceItem, differneceStartIndex);
                }
                else if (differenceItem.operation == Operation.DELETE)
                {
                    DeleteDifference(contentBuilder, differenceItem, differneceStartIndex);
                }
            }

            ModificationTarget.SubtitlesContent = contentBuilder.ToString();
        }

        #endregion //IUndoableRedoable

        private String GetContentChangeArea(String content, int changeStartIndex, int changeLength)
        {
            return content.Substring(changeStartIndex, changeLength);
        }

        private String TrimOldContent(String oldContent)
        {
            return oldContent.Substring(0, ModificationTarget.EditState.CaretPosition);
        }

        private String TrimNewContent(String newContent)
        {
            return newContent.Substring(0, newState.CaretPosition);
        }

        private ICollection<Tuple<Diff, int>> GetContentChanges(String oldContent, String newContent, int changesIndexOffset = 0)
        {
            List<Tuple<Diff, int>> result = new List<Tuple<Diff, int>>();
            List<Diff> differences = (new diff_match_patch()).diff_main(oldContent, newContent);
            int currentIndex = changesIndexOffset;

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

        private void InsertDifference(StringBuilder contentBuilder, Diff differenceItem, int differneceStartIndex)
        {
            contentBuilder.Insert(differneceStartIndex, differenceItem.text);
        }

        private void DeleteDifference(StringBuilder contentBuilder, Diff differenceItem, int differneceStartIndex)
        {
            contentBuilder.Remove(differneceStartIndex, differenceItem.text.Length);
        }

        private SubtitlesEditState TryGetModifiedEditState()
        {
            //Jeśli modyfikacja należy do grupy modyfikacji 
            //to sprawdzamy czy następna modyfikacja w grupie jest modyfikacją stanu edytowania.
            if ((Group != null) && (GroupIndex + 1 < Group.Count))
            {
                IModification mod = Group[GroupIndex + 1];
                if (mod is SubtitlesEditStateModification)
                {
                    return (mod as SubtitlesEditStateModification).ModifiedState;
                }
            }

            return null;
        }

    }
}
