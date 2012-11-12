using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Subeditor.Model.OrganizationalEntities;

namespace Subeditor.Model.Tools.Strategies
{
    /// <summary>
    /// Reprezentuje pustą strategię, nie wykonująca żadnej czynności,
    /// używaną w przypadku nieznanych typów plików.
    /// </summary>
    class EmptyEditStrategy : IEditStrategy
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="editedContent">Edytowana zawartość tekstowa napisów.</param>
        public EmptyEditStrategy(String editedContent)
        {

        }

        /// <summary>
        /// Pozwala pobrać lub ustawić przechowywaną i edytowaną zawartość tekstową napisów.
        /// </summary>
        /// <remarks>
        /// W przypadku Pustej strategi zawsze przyjmuje pusty ciąg tekstowy (String.Empty)
        /// </remarks>
        public String Content 
        {
            get 
            { 
                return String.Empty; 
            }
            set { }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić obiekt reprezentujący bieżąco edytowaną linie napisów.
        /// </summary>
        /// <remarks>
        ///  /// W przypadku Pustej strategi zawsze przyjmuje wartość null.
        /// </remarks>
        public Line CurrentLine
        {
            get 
            { 
                return null; 
            }
            set { }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić obiekt reprezentujący bieżąco edytowany wpis.
        /// </summary>
        /// <remarks>
        ///W przypadku Pustej strategi zawsze przyjmuje wartość null.
        /// </remarks>
        public Entry CurrentEntry
        {
            get 
            { 
                return null; 
            }
            set { }
        }

        /// <summary>
        /// Zapisuje modyfikacje bieżącej lini, przenosząc je na przechowywaną zawartość tekstową napisów.
        /// </summary>
        /// <remarks>
        /// W przypadku Pustej strategi nie wykonuje żadnej czynności.
        /// </remarks>
        public void SaveCurrentLine()
        {
            return;
        }

        /// <summary>
        /// Odczytuje i zwraca kolejną linie z edytowanej zawartości tekstowej napisów.
        /// </summary>
        /// <returns>Zwraca kolejną linię, a jeśli nie ma już więcej lini to null.</returns>
        /// <remarks>
        /// W przypadku Pustej strategi zwraca zawsze null.
        /// </remarks>
        public Line NextLine()
        {
            return null;
        }

        /// <summary>
        /// Odczytuje i zwraca poprzednią linie, w stosunku do bieżącej, z edytowanej zawartości tekstowej napisów.
        /// </summary>
        /// <returns>Zwraca poprzednią linię, a jeśli nie ma już więcej lini to null.</returns>
        /// <remarks>
        /// W przypadku Pustej strategi zwraca zawsze null.
        /// </remarks>
        public Line PreviousLine()
        {
            return null;
        }


        /// <summary>
        /// Zapisuje modyfikacje bieżącej wpisu, przenosząc je na przechowywaną zawartość tekstową napisów.
        /// </summary>
        /// <remarks>
        /// W przypadku Pustej strategi nie wykonuje żadnej czynności.
        /// </remarks>
        public void SaveCurrentEntry()
        {
            return;
        }

        /// <summary>
        /// Odczytuje i zwraca kolejny wpis z timingiem, z edytowanej zawartości tekstowej napisów.
        /// </summary>
        /// <returns>Zwraca kolejną wpis, a jeśli nie ma już więcej wpisów to null.</returns>
        /// <remarks>
        /// W przypadku Pustej strategi zwraca zawsze null.
        /// </remarks>
        public TimedEntry NextTimedEntry()
        {
            return null;
        }

        /// <summary>
        /// Odczytuje i zwraca poprzedni wpis z timingiem, w stosunku do bieżącego, z edytowanej zawartości tekstowej napisów.
        /// </summary>
        /// <returns>Zwraca poprzedni wpis, a jeśli nie ma już więcej wpisów to null.</returns>
        /// <remarks>
        /// W przypadku Pustej strategi zwraca zawsze null.
        /// </remarks>
        public TimedEntry PreviousTimedEntry()
        {
            return null;
        }

    }
}
