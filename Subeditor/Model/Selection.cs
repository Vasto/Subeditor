using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Model
{
    /// <summary>
    /// Reprezentuje zaznaczenie w tekście.
    /// </summary>
    class Selection : IEquatable<Selection>
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public Selection() : this(0, 0)
        {
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="selectionStart">Indeks początku zaznaczenia.</param>
        /// <param name="selectionLength">Liczba znaków do zaznaczenia.</param>
        public Selection(int selectionStart, int selectionLength)
        {
            this.Start = selectionStart;
            this.Length = selectionLength;
        }

        /// <summary>
        /// Pozwala pobrać indeks początku zaznaczenia.
        /// </summary>
        public int Start 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Pozwala pobra liczbe określająca długość zaznaczenie w znakach.
        /// </summary>
        public int Length 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Porównuje czy jeden obiekt Selection jest równy drugiemu
        /// </summary>
        /// <param name="other">Obiekt Selection do porównania.</param>
        /// <returns>Równy czy nie.</returns>
        public bool Equals(Selection other)
        {
            Selection otherSelection = other as Selection;
            if (otherSelection != null)
            {
                return (this.Start == other.Start) &&
                       (this.Length == other.Length);
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
            Selection other = obj as Selection;
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
            return Start.GetHashCode() + Length.GetHashCode();
        }

    }
}
