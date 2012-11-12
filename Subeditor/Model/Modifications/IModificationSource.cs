using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Model.Modifications
{
    /// <summary>
    /// Interfejs obiektów modyfikujących innych obiekt.
    /// To do: zastanowić się nad nazwą.
    /// </summary>
    /// <typeparam name="T">Typ docelowo modyfikowanego obiektu.</typeparam>
    interface IModificationSource<T>
    {
        /// <summary>
        /// Docelowy obiekt podlegające modtfikacji.
        /// </summary>
        T ModificationTarget { get; set; }

    }
}
