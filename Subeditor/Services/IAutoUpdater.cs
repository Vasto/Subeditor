using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Services
{
    /// <summary>
    /// Interfejs klas zapewniających funkcjonalnośc automatycznej aktualizacji.
    /// </summary>
    interface IAutoUpdater
    {
        void Update();
        void UpdateAsync();
    }
}
