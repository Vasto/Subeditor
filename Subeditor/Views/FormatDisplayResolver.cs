using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Subeditor.Model.FileFormats;

namespace Subeditor.Views
{
    /// <summary>
    /// Klasa pomocnicza pozwalająca ustalić format wyświetlania timingu dla danego formatu pliku.
    /// </summary>
    class FormatDisplayResolver
    {
        private SubtitlesFileFormat fileFormat;
        private IDictionary<SubtitlesFileFormat, DisplayMode> formatToDisplayMap;
        
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="fileFormat">
        /// Obiekt reprezentujący format pliku, dla którego ma zostać rozstrzygnięty format wyświetlania.
        /// </param>
        public FormatDisplayResolver(SubtitlesFileFormat fileFormat)
        {
            this.fileFormat = fileFormat;
            this.formatToDisplayMap = CreateFormatToDisplayMap();
        }

        /// <summary>
        /// Reprezentuje tryby wyświetlania timingów.
        /// </summary>
        public enum DisplayMode
        {
            Time,
            Frames,
        }

        /// <summary>
        /// Umozliwia rozstrzygnięcie trybu wyświetlania, dla podanego dla tego obiektu, formatu plików.
        /// </summary>
        /// <returns>Tryb wyświetlania.</returns>
        public virtual DisplayMode Resolve()
        {
            if (formatToDisplayMap.ContainsKey(fileFormat))
            {
                return formatToDisplayMap[fileFormat];
            }
            else
            {
                throw new Exception("Cannot obtain a display mode for specified format.");
            }
        }

        /// <summary>
        /// Tworzy słownik mapujący typy formatów plików do trybów wyświetlania.
        /// Słownik ten jest wykorzystywany przez klasę do rozstrzygnia trybów wyświetlania dla podanych formatów plików.
        /// </summary>
        protected virtual IDictionary<SubtitlesFileFormat, DisplayMode> CreateFormatToDisplayMap()
        {
            Dictionary<SubtitlesFileFormat, DisplayMode> map = new Dictionary<SubtitlesFileFormat, DisplayMode>();

            map.Add(new AssFileFormat(), DisplayMode.Time);
            map.Add(new SSAFileFormat(), DisplayMode.Time);
            map.Add(new MicroDVDFileFormat(), DisplayMode.Frames);
            map.Add(new SubripFileFormat(), DisplayMode.Time);
            map.Add(new TMPlayerFileFormat(), DisplayMode.Time);
            map.Add(new UnresolvedFileFormat(), DisplayMode.Time);

            return map;
        }
    }
}
