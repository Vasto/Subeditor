using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Subeditor.Properties;

namespace Subeditor.Model.FileFormats
{
    /// <summary>
    /// Reprezentuje format napisów SubStation Alpha.
    /// </summary>
    class SSAFileFormat : SubtitlesFileFormat
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public SSAFileFormat() : base(Resources.DscSSA, new[] { ".ssa" })
        {
        }

    }
}
