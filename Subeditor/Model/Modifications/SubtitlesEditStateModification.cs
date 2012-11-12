using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Subeditor.Model.StateManagement;

namespace Subeditor.Model.Modifications
{
    /// <summary>
    /// Modyfikacja stanu edytowania napisów.
    /// </summary>
    class SubtitlesEditStateModification
        : IModification, IModificationSource<SubtitlesEditor>, IGroupableModification, IUndoableRedoable, IEquatable<SubtitlesEditStateModification> 
    {
        private SubtitlesEditState oldState;
        private SubtitlesEditState newState;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="newState">Nowy stan edytowania napisów.</param>
        public SubtitlesEditStateModification(SubtitlesEditState newState)
        {
            this.newState = newState;
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="editor">Obiekt edytora napisów.</param>
        /// <param name="newState">Nowy stan edytowania napisów.</param>
        public SubtitlesEditStateModification(SubtitlesEditor editor, SubtitlesEditState newState)
        {
            this.ModificationTarget = editor;
            this.newState = newState;
        }

        public SubtitlesEditState ModifiedState 
        {
            get 
            { 
                return newState; 
            } 
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

            oldState = ModificationTarget.EditState;
            ModificationTarget.EditState = newState;
        }

        /// <summary>
        /// Porównuje czy jeden obiekt SubtitlesEditStateModification jest równy drugiemu.
        /// </summary>
        /// <param name="other">Obiekt Selection do porównania.</param>
        /// <returns>Równy czy nie.</returns>
        public bool Equals(SubtitlesEditStateModification other)
        {
            if (other != null)
            {
                return (this.oldState == other.oldState) && 
                    (this.newState == other.newState);
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
            SubtitlesEditStateModification other = obj as SubtitlesEditStateModification;
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
            int hashCode = 176;
            if (oldState != null)
            {
                hashCode += oldState.GetHashCode();
            }
            hashCode += newState.GetHashCode();

            return hashCode;
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

            ModificationTarget.EditState = oldState;
        }

        /// <summary>
        /// Pprzywraca modyfikację.
        /// </summary>
        public void Redo()
        {
            if (ModificationTarget == null)
            {
                throw new Exception();
            }

            ModificationTarget.EditState = newState;
        }

        #endregion //IUndoableRedoable

    }
}
