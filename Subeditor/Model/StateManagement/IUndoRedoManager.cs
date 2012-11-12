using System;

namespace Subeditor.Model.StateManagement
{
    /// <summary>
    /// Interfejs mendadżerów cofania/przywracania zmian.
    /// </summary>
    interface IUndoRedoManager
    {
        /// <summary>
        /// Zdarzenie mające miejsce w momencie zmiany możliwości cofania. 
        /// </summary>
        event EventHandler<Subeditor.EventArgs<bool>> CanUndoChanged;
        /// <summary>
        /// Zdarzenie mające miejsce w momencie zmiany możliwości przywracania. 
        /// </summary>
        event EventHandler<Subeditor.EventArgs<bool>> CanRedoChanged;

        /// <summary>
        /// Pozwala pobrać informacje o możliwosci wykonania opercji cofania.
        /// </summary>
        bool CanUndo { get; }

        /// <summary>
        /// Pozwala pobrać informacje o możliwosci wykonania opercji przywracania.
        /// </summary>
        bool CanRedo { get; }

        /// <summary>
        /// Zwraca ostatnio dodany obiekt cofania/przywracania zmian.
        /// </summary>
        IUndoableRedoable LastUndoableRedoable { get; }

        /// <summary>
        /// Wycofuje zmianę.
        /// </summary>
        void Undo();

        /// <summary>
        /// Przywraca zmianę.
        /// </summary>
        void Redo();

        /// <summary>
        /// Dodaje obiekt obsługujące cofanie/przywracanie zmian do menadżera.
        /// </summary>
        /// <param name="undoableRedoable">Obiekt do dodania.</param>
        void AddUndoableRedoable(IUndoableRedoable undoableRedoable);

        /// <summary>
        /// Usuwa ostatnio dodany obiekt cofania/przywracania zmian.
        /// </summary>
        void DeleteLastUndoableRedoeable();

        /// <summary>
        /// Usuwa wszytkie obiekty cofania/przywracania zmian, zawierane przez menadżera.
        /// </summary>
        void DeleteUndoablesRedoeables();

    }
}
