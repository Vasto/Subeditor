using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Model.OrganizationalEntities
{
    /// <summary>
    /// Reprezentuje jedną linię tekstowej zawartości napisów.
    /// </summary>
    class Line : IEquatable<Line>
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="content">Tekst zawierany przez linię.</param>
        /// <param name="number">Numer lini/</param>
        /// <param name="start">Indeks początkowy tekstu lini.</param>
        public Line(String content, int number, int start)
        {
            this.Content = content;
            this.Number = number;
            this.Start = start;
        }

        /// <summary>
        /// Pozwala pobrać i ustawić zawartość tekstową lini.
        /// </summary>
        public String Content { get; set; }

        /// <summary>
        /// Pozwala pobrać i ustawić numer lini wśród wszystkich lini składających się na tekst.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Pozwala pobrać i ustawić indeks początkowy tekstu zawieranego przez linie.
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// Pozwala pobrać długość tekstu zawieranego przez linie.
        /// </summary>
        public int Length { get { return this.Content.Length; } }

        /// <summary>
        /// Porównuje czy jeden obiekt Line jest równy drugiemu
        /// </summary>
        /// <param name="other">Obiekt Selection do porównania.</param>
        /// <returns>Równy czy nie.</returns>
        public virtual bool Equals(Line other)
        {
            if (other != null)
            {
                return (this.Content == other.Content) &&
                       (this.Number == other.Number) &&
                       (this.Start == other.Start);
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
            Line other = obj as Line;
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
            return Content.GetHashCode() + Number.GetHashCode() + Start.GetHashCode();
        }

    }
}
