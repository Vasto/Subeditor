using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Subeditor.Model.StateManagement;

namespace Subeditor.Model.Modifications
{
    /// <summary>
    /// Reprezentuje złożoną modyfikację, mogącą sie składać z dowolnej liczby innych modyfikacji.
    /// </summary>
    class CompositeModification : IModification, IUndoableRedoable, IQueryable<IModification>, IEquatable<CompositeModification>
    {
        private IEnumerable<IModification> modifications;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="modifications">Modyfikacje mające wchodzić w skałd bieżącej modyfikacji złożonej.</param>
        public CompositeModification(params IModification[] modifications)
        {
            this.modifications = new List<IModification>(modifications);
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="modifications">Kolekcja modyfikacji mających wchodzić w skałd bieżącej modyfikacji złożonej.</param>
        public CompositeModification(IEnumerable<IModification> modifications)
        {
            this.modifications = new List<IModification>(modifications);
        }

        /// <summary>
        /// Porównuje czy jeden obiekt CompositeModification jest równy drugiemu.
        /// </summary>
        /// <param name="other">Obiekt Selection do porównania.</param>
        /// <returns>Równy czy nie.</returns>
        public bool Equals(CompositeModification other)
        {
            if (other != null)
            {
                var thisMods = new HashSet<IModification>(this.modifications);
                bool areEquals = thisMods.SetEquals(other.modifications);

                return areEquals;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Porównuje czy jeden System.Object jest równy drugiemu.
        /// </summary>
        /// <param name="o">System.Object do porównania.</param>
        /// <returns>Równy czy nie.</returns>
        public override bool Equals(object obj)
        {
            CompositeModification other = obj as CompositeModification;
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
            int hashcode = 0;
            if (modifications != null)
            {
                foreach (var mod in modifications)
                {
                    hashcode += mod.GetHashCode();
                }
            }

            return hashcode;
        }

        #region IModification

        /// <summary>
        /// Dokonuje modyfikacji.
        /// </summary>
        public void Perform()
        {
            foreach (var mod in modifications)
            {
                mod.Perform();
            }
        }

        #endregion //IModification

        #region IUndoableRedoable

        /// <summary>
        /// Cofa modyfikację.
        /// </summary>
        public void Undo()
        {
            foreach (var mod in modifications)
            {
                if (mod is IUndoableRedoable)
                {
                    (mod as IUndoableRedoable).Undo();
                }
            }
        }

        /// <summary>
        /// Przywraca modyfikację.
        /// </summary>
        public void Redo()
        {
            foreach (var mod in modifications)
            {
                if (mod is IUndoableRedoable)
                {
                    (mod as IUndoableRedoable).Redo();
                }
            }
        }

        #endregion //IUndoableRedoable

        #region IQueryable

        public IEnumerator<IModification> GetEnumerator()
        {
            return modifications.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType
        {
            get { return modifications.AsQueryable().ElementType; }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return modifications.AsQueryable().Expression; }
        }

        public IQueryProvider Provider
        {
            get { return modifications.AsQueryable().Provider; }
        }

        #endregion //IQueryable
    }
}
