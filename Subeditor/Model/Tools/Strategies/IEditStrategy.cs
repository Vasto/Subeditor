using System;
using Subeditor.Model.OrganizationalEntities;

namespace Subeditor.Model.Tools.Strategies
{
    /// <summary>
    /// Definiuje wspólne zachowania dla wszystkich strategi edytowania zawartości napisów.
    /// </summary>
    interface IEditStrategy
    {
        /// <summary>
        /// Pozwala pobrać lub ustawić przechowywaną i edytowaną zawartość tekstową napisów.
        /// </summary>
        String Content { get; set; }

        /// <summary>
        /// Pozwala pobrać lub ustawić obiekt reprezentujący bieżąco edytowaną linie napisów.
        /// </summary>
        Line CurrentLine { get; set; }

        /// <summary>
        /// Pozwala pobrać lub ustawić obiekt reprezentujący bieżąco edytowany wpis.
        /// </summary>
        Entry CurrentEntry { get; set; }

        /// <summary>
        /// Zapisuje modyfikacje bieżącej lini, przenosząc je na przechowywaną zawartość tekstową napisów.
        /// </summary>
        void SaveCurrentLine();

        /// <summary>
        /// Odczytuje i zwraca kolejną linie z edytowanej zawartości tekstowej napisów.
        /// </summary>
        /// <returns>Zwraca kolejną linię, a jeśli nie ma już więcej lini to null.</returns>
        Line NextLine();

        /// <summary>
        /// Odczytuje i zwraca poprzednią linie, w stosunku do bieżącej, z edytowanej zawartości tekstowej napisów.
        /// </summary>
        /// <returns>Zwraca poprzednią linię, a jeśli nie ma już więcej lini to null.</returns>
        Line PreviousLine();

        /// <summary>
        /// Zapisuje modyfikacje bieżącej wpisu, przenosząc je na przechowywaną zawartość tekstową napisów.
        /// </summary>
        void SaveCurrentEntry();

        /// <summary>
        /// Odczytuje i zwraca kolejny wpis z timingiem, z edytowanej zawartości tekstowej napisów.
        /// </summary>
        /// <returns>Zwraca kolejną wpis, a jeśli nie ma już więcej wpisów to null.</returns>
        TimedEntry NextTimedEntry();

        /// <summary>
        /// Odczytuje i zwraca poprzedni wpis z timingiem, w stosunku do bieżącego, z edytowanej zawartości tekstowej napisów.
        /// </summary>
        /// <returns>Zwraca poprzedni wpis, a jeśli nie ma już więcej wpisów to null.</returns>
        TimedEntry PreviousTimedEntry();

    }
}
