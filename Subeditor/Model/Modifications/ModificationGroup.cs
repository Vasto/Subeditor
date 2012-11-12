using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Model.Modifications
{
    /// <summary>
    /// Reprezentuje grupę modyfikacji.
    /// </summary>
    class ModificationGroup : IEnumerable<IModification>
    {
        private IList<IModification> modifications;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="modifications">Kolekcja modyfikacji mających wchodzić w skłąd grupy.</param>
        public ModificationGroup(IEnumerable<IModification> modifications)
        {
            this.modifications = new List<IModification>(modifications);
        }

        /// <summary>
        /// Liczba modyfikacji przynależących do grupy.
        /// </summary>
        public int Count 
        { 
            get 
            { 
                return modifications.Count; 
            } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IModification this[int index]
        {
            get 
            { 
                return modifications[index]; 
            }
        }

        #region IEnumerable

        public IEnumerator<IModification> GetEnumerator()
        {
            return modifications.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion //IEnumerable
    }
}
