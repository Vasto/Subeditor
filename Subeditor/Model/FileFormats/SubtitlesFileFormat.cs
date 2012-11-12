using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Model.FileFormats
{
    /// <summary>
    /// Reprezentuje format pliku napisów.
    /// </summary>
    class SubtitlesFileFormat : IEquatable<SubtitlesFileFormat>
    {
        private HashSet<String> correctExtensions;
        private int hashcode;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="currentExtension">Rozszerzenie pliku.</param>
        /// <param name="fileDescription">Opis pliku.</param>
        public SubtitlesFileFormat(String fileDescription, IEnumerable<String> fileExtensions)
        {
            this.Description = fileDescription;
            this.correctExtensions = new HashSet<String>(fileExtensions);

            this.InitializeHashcode();

        }

        /// <summary>
        /// Pozwala pobrać opis formatu pliku.
        /// </summary>
        public String Description 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Pozwala pobrać zbiór poprawnych rozszerzeń dla bieżącego formatu pliku.
        /// </summary>
        public IEnumerable<String> CorrectExtensions 
        {
            get {  return correctExtensions; } 
        }

        /// <summary>
        /// Określa czy podane rozszerzenie jest poprawne dla tego formatu pliku.
        /// </summary>
        /// <param name="extension">Rozszerzenie.</param>
        /// <returns>Prawda jeśli jest poprawne.</returns>
        public bool IsExtensionCorrect(String extension)
        {
            return correctExtensions.Contains(extension);
        }

        /// <summary>
        /// Porównuje czy jeden obiekt SubtitlesFileFormat jest równy drugiemu
        /// </summary>
        /// <param name="other">Obiekt SubtitlesFileFormat do porównania.</param>
        /// <returns>Równy czy nie.</returns>
        public bool Equals(SubtitlesFileFormat other)
        {
            SubtitlesFileFormat otherFormat = other as SubtitlesFileFormat;
            if (otherFormat != null)
            {
                return (this.Description == otherFormat.Description) &&
                       (this.correctExtensions.SetEquals(otherFormat.correctExtensions));
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
            SubtitlesFileFormat other = obj as SubtitlesFileFormat;
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
            return hashcode;
        }

        private void InitializeHashcode()
        {
            hashcode = Description.GetHashCode();
            foreach (var e in CorrectExtensions)
            {
                hashcode += e.GetHashCode();
            }
        }

    }
}
