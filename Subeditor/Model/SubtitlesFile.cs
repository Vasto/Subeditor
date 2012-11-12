using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Subeditor.Model
{
    /// <summary>
    /// Reprezentuje plik napsiów.
    /// </summary>
    class SubtitlesFile
    {
        private String path;
        private String content;
        private int lineCount;
        private bool needUpdateLineCount;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        public SubtitlesFile()
        {
            this.InpuEncoding = Encoding.Default;
            this.OutputEncoding = Encoding.Unicode;
            this.content = String.Empty;
            this.needUpdateLineCount = true;
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="filePath">Ścieżka pliku z napisami.</param>
        public SubtitlesFile(String filePath)
        {
            this.Path = filePath;
            this.InpuEncoding = Encoding.Default;
            this.OutputEncoding = Encoding.Unicode;
            this.content = String.Empty;
            this.needUpdateLineCount = true;
        }

        /// <summary>
        /// Zdarzenie mające miejsce w momencie zmiany zawartości tekstowej napisów.
        /// </summary>
        public event EventHandler<SubtitlesContentChangedEventArgs> ContentChanged;

        /// <summary>
        /// Zdarzenie mające miejsce w momencie zmiany nazwy napisów.
        /// </summary>
        public event EventHandler<EventArgs<String>> NameChanged;

        /// <summary>
        /// Pozwa;a pobrać nazwę plików z napisami.
        /// </summary>
        public String Name {  get; private set; }

        /// <summary>
        /// Pozwala pobrać lub ustawić pełną ścieżkę bieżącego pliku napisów.
        /// </summary>
        public String Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
                OnPathChanged();
            }
        }

        /// <summary>
        /// Pozwala pobrać rozszerzenie pliku.
        /// </summary>
        public String Extension
        {
            get;
            private set;
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić kodowanie jakie ma zostać użyte podczas wczytywaina napisów.
        /// </summary>
        public Encoding InpuEncoding
        {
            get;
            set;
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić kodowanie jakie ma zostać użyte podczas zapisywania napisów.
        /// </summary>
        public Encoding OutputEncoding
        {
            get;
            set;
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić zawartość tekstowa pliku napisów.
        /// </summary>
        public String Content
        {
            get
            {
                return content;
            }
            set
            {
                String oldContent = content;
                content = value;
                OnContentChanged(oldContent, value);
            }
        }

        /// <summary>
        /// Pozwala pobrać liczbę lini tekstu pliku napisów
        /// </summary>
        public int LineCount
        {
            get
            {
                if (needUpdateLineCount)
                {
                    lineCount = GetLineCount();
                    needUpdateLineCount = false;
                }

                return lineCount;
            }
        }

        /// <summary>
        /// Pozwala pobrać informację czy plik został zapisany.
        /// </summary>
        public bool IsSaved 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Wczytuje plik napisów z dysku.
        /// </summary>
        public virtual void Load()
        {
            Content = File.ReadAllText(Path, InpuEncoding);
            IsSaved = true;
        }

        /// <summary>
        /// Zapisuje plik napisów na dysku.
        /// </summary>
        public virtual void Save()
        {
            File.WriteAllText(Path, Content, OutputEncoding);
            IsSaved = true;
        }

        private int GetLineCount()
        {
            int count = 0;
            using(StringReader reader = new StringReader(Content))
            {
                while (reader.ReadLine() != null)
                {
                    count++;
                }
            }

            return count;
        }

        private void OnPathChanged()
        {
            Name = System.IO.Path.GetFileNameWithoutExtension(Path);
            Extension = System.IO.Path.GetExtension(Path);

            var temporaryEventHolder = NameChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<String>(Name));
            }
        }

        private void OnContentChanged(String oldContent, String newContent)
        {
            IsSaved = false;

            var temporaryEventHolder = ContentChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new SubtitlesContentChangedEventArgs(oldContent, newContent));
            }
        }

    }
}
