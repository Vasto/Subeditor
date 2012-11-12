using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Subeditor.Utilities;

namespace Subeditor.Model.StateManagement
{
    /// <summary>
    /// Menadżer cofania, przywracania zmian.
    /// </summary>
    class UndoRedoManager : IUndoRedoManager
    {       
        private RollingStack<IUndoableRedoable> undoStack;
        private RollingStack<IUndoableRedoable> redoStack;
        private bool canUndo;
        private bool canRedo;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        public UndoRedoManager()
        {
            this.undoStack = new RollingStack<IUndoableRedoable>(50);
            this.redoStack = new RollingStack<IUndoableRedoable>(50);

            //this.UndoRedoDepth = 3;
            this.ClearRedoablesAfterAdd = true;

            this.canUndo = IsUndoAvailable();
            this.canRedo = IsRedoAvailable();    
        }

        /// <summary>
        /// Zdarzenie mające miejsce w momencie zmiany możliwości cofania. 
        /// </summary>
        public event EventHandler<EventArgs<bool>> CanUndoChanged;

        /// <summary>
        /// Zdarzenie mające miejsce w momencie zmiany możliwości przywracania. 
        /// </summary>
        public event EventHandler<EventArgs<bool>> CanRedoChanged;

        //To do: Zaimplementować
        //public int UndoRedoDepth
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// Pozwala pobrać lub ustawić informację czy aktualnie przechowywane przywracalne zmiany
        /// mają zostać usunięte po dodaniu nowej zmiany.
        /// </summary>
        public bool ClearRedoablesAfterAdd 
        { 
            get; 
            set; 
        }


        /// <summary>
        /// Pozwala pobrać informacje o możliwosci wykonania opercji cofania.
        /// </summary>
        public bool CanUndo
        {
            get
            {
                return canUndo;
            }
            private set
            {
                canUndo = value;
                OnCanUndoChanged(value);
            }
        }

        /// <summary>
        /// Pozwala pobrać informacje o możliwosci wykonania opercji przywracania.
        /// </summary>
        public bool CanRedo
        {
            get
            {
                return canRedo;
            }
            private set
            {
                canRedo = value;
                OnCanRedoChanged(value);
            }
        }

        /// <summary>
        /// Zwraca ostatnio dodany obiekt cofania/przywracania zmian.
        /// </summary>
        public IUndoableRedoable LastUndoableRedoable 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Wycofuje zmianę.
        /// </summary>
        public void Undo()
        {
            if (CanUndo)
            {
                IUndoableRedoable action = undoStack.Pop();
                action.Undo();
                redoStack.Push(action);
            }

            CanUndo = IsUndoAvailable();
            CanRedo = IsRedoAvailable();
        }

        /// <summary>
        /// Przywraca zmianę.
        /// </summary>
        public void Redo()
        {
            if (CanRedo)
            {
                IUndoableRedoable action = redoStack.Pop();
                action.Redo();
                undoStack.Push(action); 
            }

            CanUndo = IsUndoAvailable();
            CanRedo = IsRedoAvailable();
        }

        /// <summary>
        /// Dodaje obiekt obsługujące cofanie/przywracanie zmian do menadżera.
        /// </summary>
        /// <param name="undoableRedoable">Obiekt do dodania.</param>
        public void AddUndoableRedoable(IUndoableRedoable undoableRedoable)
        {
            undoStack.Push(undoableRedoable);

            LastUndoableRedoable = undoableRedoable;

            if (ClearRedoablesAfterAdd)
            {
                redoStack.Clear();
            }

            CanUndo = IsUndoAvailable();
            CanRedo = IsRedoAvailable();
        }

        /// <summary>
        /// Usuwa ostatnio dodany obiekt cofania/przywracania zmian.
        /// </summary>
        public void DeleteLastUndoableRedoeable()
        {
            if (CanUndo && undoStack.Peek().Equals(LastUndoableRedoable))
            {
                undoStack.Pop();
            }
            else if (CanRedo && redoStack.Peek().Equals(LastUndoableRedoable))
            {
                redoStack.Pop();
            }

            CanUndo = IsUndoAvailable();
            CanRedo = IsRedoAvailable();
        }

        /// <summary>
        /// Usuwa wszytkie obiekty cofania/przywracania zmian, zawierane przez menadżera.
        /// </summary>
        public void DeleteUndoablesRedoeables()
        {
            undoStack.Clear();
            redoStack.Clear();
            LastUndoableRedoable = null;

            CanUndo = IsUndoAvailable();
            CanRedo = IsRedoAvailable();
        }

        private bool IsUndoAvailable()
        {
            return undoStack.Count > 0;
        }

        private bool IsRedoAvailable()
        {
            return redoStack.Count > 0;
        }

        private void OnCanUndoChanged(bool newValue)
        {
            var temporaryEventHolder = CanUndoChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<bool>(newValue));
            }
        }

        private void OnCanRedoChanged(bool newValue)
        {
            var temporaryEventHolder = CanRedoChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<bool>(newValue));
            }
        }

    }
}
