using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Subeditor.Properties;

namespace Subeditor.Model.FileFormats
{
    /// <summary>
    /// Reprezentuje format napisów MicroDVD.
    /// </summary>
    class MicroDVDFileFormat : SubtitlesFileFormat
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public MicroDVDFileFormat() : base(Resources.DscMicroDVD, new[] { ".sub", ".txt" })
        {
        }

    }
}
