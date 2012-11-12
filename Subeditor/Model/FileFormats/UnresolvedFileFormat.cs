using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Subeditor.Properties;

namespace Subeditor.Model.FileFormats
{
    /// <summary>
    /// Reprezentuje nieznany format napisów.
    /// </summary>
    class UnresolvedFileFormat : SubtitlesFileFormat
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public UnresolvedFileFormat() : base(Resources.DscUnresolved, new String[] { })
        {

        }
    }
}
