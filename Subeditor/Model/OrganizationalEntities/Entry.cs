using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Model.OrganizationalEntities
{
    /// <summary>
    /// Reprezentuje pojedynczy wpis w napisach.
    /// Poprzez pojęcie wpisu rozumiemy fragment napisów będący przykładowo,
    /// jedną linią dialogu o ustalonym czasie, linie definiująca styl itp.
    /// </summary>
    abstract class Entry : IEquatable<Entry>
    {
        private String content;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="content">Tekst zawierany przez linię.</param>
        /// <param name="start">Indeks początkowy tekstu lini.</param>
        public Entry(String content, int start)
        {
            this.Content = content;
            this.Start = start;
        }

        /// <summary>
        /// Zdarzenie mające miejsce gdy zawartość tekstowa wpisu ulegnie zmianie.
        /// </summary>
        public event EventHandler ContentChanged;

        /// <summary>
        /// Pozwala pobrać i ustawić zawartość tekstową wpisu.
        /// </summary>
        public String Content 
        {
            get
            {
                if (ContentNeedUpdate)
                {
                    UpdateContent();
                    ContentNeedUpdate = false;
                }

                return content;
            }
            set
            {
                content = value;
            }
        }

        /// <summary>
        /// Pozwala pobrać i ustawić indeks początkowy tekstu zawieranego przez linie.
        /// </summary>
        public int Start 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Pozwala pobrać długość tekstu zawieranego przez linie.
        /// </summary>
        public int Length 
        { 
            get 
            { 
                return this.Content.Length; 
            } 
        }

        /// <summary>
        /// Pozwala klasą potomnym pobrać lub ustawić informację o tym,
        /// czy zawartość tekstowa wymaga aktualizcacji przed udostępnienim jej klientowi.
        /// </summary>
        protected bool ContentNeedUpdate 
        {
            get; 
            set; 
        }

        /// <summary>
        /// Porównuje czy jeden obiekt Entry jest równy drugiemu
        /// </summary>
        /// <param name="other">Obiekt Selection do porównania.</param>
        /// <returns>Równy czy nie.</returns>
        public virtual bool Equals(Entry other)
        {
            if (other != null)
            {
                return (this.Content == other.Content) && 
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
            Entry other = obj as Entry;
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
            return Content.GetHashCode() + Start.GetHashCode();
        }

        /// <summary>
        /// Metoda wywoływana w celu zaktualizowaniu zawartości tekstowej wpisu (Content).
        /// </summary>
        protected abstract void UpdateContent();

        /// <summary>
        /// Metoda wywoływana gdy wartość właściwości Content ulegnie zmianie.
        /// </summary>
        protected virtual void OnContentChanged()
        {
            var temporaryEventHolder = ContentChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs());
            }
        }
    }
}
