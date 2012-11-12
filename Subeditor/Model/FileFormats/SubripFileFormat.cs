using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Subeditor.Properties;

namespace Subeditor.Model.FileFormats
{
    /// <summary>
    /// Reprezentuje format napisów SubRip.
    /// </summary>
    class SubripFileFormat : SubtitlesFileFormat
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public SubripFileFormat() : base(Resources.DscSubRip, new[] { ".srt" })
        {
        }

    }
}
