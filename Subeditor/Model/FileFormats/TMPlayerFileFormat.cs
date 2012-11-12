using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Subeditor.Properties;

namespace Subeditor.Model.FileFormats
{
    /// <summary>
    /// Reprezentuje format napsiów TMPlayer.
    /// </summary>
    class TMPlayerFileFormat : SubtitlesFileFormat
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public TMPlayerFileFormat() : base(Resources.DscTMPlayer, new[] { ".txt" })
        {
        }

    }
}
