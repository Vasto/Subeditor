using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Model
{
    /// <summary>
    /// Opisuje stan edycji zawartości tekstowej napisów.
    /// </summary>
    class SubtitlesEditState : IEquatable<SubtitlesEditState>
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public SubtitlesEditState() : this(0, new Selection()) 
        {
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="caretPosition">Indeks określający pozycję karetki w tekście.</param>
        public SubtitlesEditState(int caretPosition) : this(caretPosition, new Selection(caretPosition, 0))
        {
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="caretPosition">Indeks określający pozycję karetki w tekście.</param>
        /// <param name="selection">Obiekt definiujący zaznaczenie tekstu.</param>
        public SubtitlesEditState(int caretPosition, Selection selection)
        {
            this.CaretPosition = caretPosition;
            this.Selection = selection;
        }

        /// <summary>
        /// Pozwala pobrać informację o pozycji karetki w tekście.
        /// </summary>
        public int CaretPosition 
        {
            get; 
            private set; 
        }

        /// <summary>
        /// Pozwala pobrać informację o zaznaczeniu tekstu.
        /// </summary>
        public Selection Selection 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Porównuje czy jeden obiekt SubtitlesEditState jest równy drugiemu
        /// </summary>
        /// <param name="other">Obiekt SubtitlesEditState do porównania.</param>
        /// <returns>Równy czy nie.</returns>
        public bool Equals(SubtitlesEditState other)
        {
            SubtitlesEditState otherEditState = other as SubtitlesEditState;
            if (otherEditState != null)
            {
                return (this.CaretPosition == otherEditState.CaretPosition) &&
                       (this.Selection.Equals(otherEditState.Selection));
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
            SubtitlesEditState other = obj as SubtitlesEditState;
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
            return CaretPosition.GetHashCode() + Selection.GetHashCode();
        }

    }
}
