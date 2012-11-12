using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Utilities
{
    /// <summary>
    /// Klasa do służąca do budwy filtrów rozszerzeń plików.
    /// </summary>
    class FileFilterBuilder
    {
        private readonly String connectingElement; 

        private StringBuilder builder;
        private bool atLeastOneFormat;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        public FileFilterBuilder()
        {
            this.connectingElement = "|";
            this.builder = new StringBuilder();
        }

        /// <summary>
        /// Dodaje kolejny format pliku do tworzonego filtra.
        /// </summary>
        /// <param name="formatDescription">Opis formatu pliku.</param>
        /// <param name="formatExtension">Rozszerzenie formatu pliku.</param>
        public void AppendFileFormat(String formatDescription, String formatExtension)
        {
            this.AppendFileFormat(formatDescription, new String[] { formatExtension });
        }

        /// <summary>
        /// Dodaje kolejny format pliku do tworzonego filtra.
        /// </summary>
        /// <param name="formatDescription">Opis formatu pliku.</param>
        /// <param name="formatExtensions">Zbiór rozszerzeń jakie może przyjąć format pliku.</param>
        public void AppendFileFormat(String formatDescription, IEnumerable<String> formatExtensions)
        {
            if (atLeastOneFormat)
            {
                builder.Append(connectingElement);
            }

            builder.Append(formatDescription);
            builder.Append(" (");
            AppendExtensions(formatExtensions);
            builder.Append(")");
            builder.Append(connectingElement);
            AppendExtensions(formatExtensions);

            atLeastOneFormat = true;
        }

        /// <summary>
        /// Powoduje wyczyszczenie tworzonego filtra.
        /// </summary>
        public void Clear()
        {
            builder.Clear();

            atLeastOneFormat = false;
        }

        /// <summary>
        /// Zwraca tekstową reprezentację stworzonego filtra.
        /// </summary>
        /// <returns></returns>
        public override String ToString()
        {
            return builder.ToString();
        }

        private void AppendExtensions(IEnumerable<String> extensions)
        {
            foreach (var extension in extensions)
            {
                if(!extension.StartsWith("*."))
                {
                    if (extension.StartsWith("."))
                    {
                        builder.Append("*");
                    }
                    else
                    {
                        builder.Append("*.");
                    }
                }

                builder.Append(extension);
                builder.Append(";");
            }

            //Usuwamy ostatni średnik.
            if (builder.Length > 0)
            {
                builder.Remove(builder.Length - 1, 1);
            }
        }

    }
}
