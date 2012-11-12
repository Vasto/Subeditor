using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Subeditor.Properties;

namespace Subeditor.Model.FileFormats
{
    /// <summary>
    /// Reprezentuje format napisów Advanced SubStation Alpha.
    /// </summary>
    class AssFileFormat : SubtitlesFileFormat
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public AssFileFormat() : base(Resources.DscASS, new[] {".ass"})
        {
        }

    }
}
