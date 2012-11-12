using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Subeditor.Model.FileFormats;

namespace Subeditor.Model
{
    /// <summary>
    /// Menadżer zarządzający obiektami napisów.
    /// </summary>
    class SubtitlesManager
    {
        private SubtitlesFile currentSubtitles;
        private SubtitlesFileFormat currentSubtitlesFileFormat;
        private HashSet<SubtitlesFileFormat> supportedSubtitlesFormats;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        public SubtitlesManager()
        {
            this.supportedSubtitlesFormats = new HashSet<SubtitlesFileFormat>();

            if (this.CurrentSubtitles == null)
            {
                this.CurrentSubtitles = new SubtitlesFile();
                this.CurrentSubtitlesFormat = new UnresolvedFileFormat();
            }
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="currentSubtitles">Obiekt napisów który zostanie ustawiony jako bieżący.</param>
        public SubtitlesManager(SubtitlesFile currentSubtitles)
        {
            this.supportedSubtitlesFormats = new HashSet<SubtitlesFileFormat>();

            this.CurrentSubtitles = currentSubtitles;
        }

        /// <summary>
        /// Zdarzenie mające miejsce gdy bieżacy obiekt napsiów zostanie zmieniony na inny.
        /// </summary>
        public event EventHandler<SubtitlesChangedEventArgs> CurrentSubtitlesChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy właściwość określająca format bieżących napsiów ulegnie zmianie.
        /// </summary>
        public event EventHandler CurrentSubtitlesFormatChanged;

        /// <summary>
        /// Zdarzenie mające miejsce zawartość bieżącego obiektu napisów zostanie załadowana z dsyku.
        /// </summary>
        public event EventHandler CurrentSubtitlesLoaded;

        /// <summary>
        /// Zdarzenie mające miejsce zawartość bieżącego obiektu napisów zostanie załadowana z dsyku.
        /// </summary>
        public event EventHandler CurrentSubtitlesSaved;

        /// <summary>
        /// Pozwala pobrać lub ustawić bieżacy plik napisów.
        /// </summary>
        public SubtitlesFile CurrentSubtitles
        {
            get
            {
                return currentSubtitles;
            }
            set
            {
                var oldSubtitles = currentSubtitles;
                currentSubtitles = value;

                CurrentSubtitlesFormat = GetCurrentSubtitlesFormat();

                OnCurrentSubtitlesChanged(oldSubtitles, value);
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić format bieżących napisów.
        /// </summary>
        public SubtitlesFileFormat CurrentSubtitlesFormat 
        {
            get
            {
                return currentSubtitlesFileFormat;
            }
            set
            {
                currentSubtitlesFileFormat = value;
                OnCurrentSubtitlesFileFormatChanged();
            }
        }

        /// <summary>
        /// Pozwala pobrać kolekcje obsługiwanych typów plików napisów.
        /// </summary>
        public IEnumerable<SubtitlesFileFormat> SupportedSubtitlesFormats
        {
            get 
            { 
                return supportedSubtitlesFormats; 
            }
        }

        /// <summary>
        /// Pozwala pobrać informację czy bieżacy plik napisów został zapisany.
        /// </summary>
        public bool AreCurrentSubtitlesSaved
        {
            get 
            {
                return currentSubtitles.IsSaved; 
            }
        }

        /// <summary>
        /// Wczytuje zawartość bieżącego pliku napisów z dysku.
        /// </summary>
        /// <exception cref="System.Exception">Not supported subtitles format.</exception>
        public void Load()
        {
            if (!IsCurrentSubtitlesExtensionSupported())
            {
                throw new Exception("Not supported subtitles format.");
            }

            CurrentSubtitles.Load();

            CurrentSubtitlesFormat = GetCurrentSubtitlesFormat();

            OnLoad();
        }

        /// <summary>
        /// Wczytuje zawartość pliku napisów o podanej ścieżce.
        /// </summary>
        /// <param name="filePath">Ścieżka pliku napisów.</param>
        /// <exception cref="System.Exception">Not supported subtitles format.</exception>
        public void Load(String filePath)
        {
            CurrentSubtitles = new SubtitlesFile(filePath);

            if (!IsCurrentSubtitlesExtensionSupported())
            {
                throw new Exception("Not supported subtitles format.");
            }

            CurrentSubtitles.Load();

            CurrentSubtitlesFormat = GetCurrentSubtitlesFormat();

            OnLoad();
        }

        /// <summary>
        /// Próbuje wczytać zawartość pliku ze ścieżki podanej poprzez argumenty wiersza poleceń.
        /// </summary>
        /// <returns>Jeśli uda sie wczytać plik zwraca prawdę.</returns>
        public bool TryLoadFromLineArgs()
        {
            var arguments = Environment.GetCommandLineArgs();
            if (arguments.Length > 1)
            {
                if ((new FileInfo(arguments[1])).Exists)
                {
                    this.Load(arguments[1]);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Próbuje wczytać zawartość pliku z danych przekazywancyh od środowiska uruchomieniowego ClickOnce.
        /// </summary>
        /// <returns>Jeśli uda sie wczytać plik zwraca prawdę.</returns>
        public bool TryLoadFromClickOnceDeploymentSystem()
        {
            var arguments = AppDomain.CurrentDomain.SetupInformation.ActivationArguments;
            if (arguments == null)
            {
                //File.WriteAllLines("C:\\log.txt", new[] {"In arguments null"});
                return false;
            }

            var data = arguments.ActivationData;
            if (data == null)
            {
                //File.WriteAllLines("C:\\log.txt", new[] { "In data null" });
                return false;
            }

            //File.WriteAllLines("C:\\log.txt", data);

            if (data.Length > 1)
            {
                if ((new FileInfo(data[1])).Exists)
                {
                    this.Load(data[1]);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Zapisuje bieżący plik napsiów na dysk.
        /// </summary>
        public void Save()
        {
            //String extension = Path.GetExtension(CurrentSubtitles.Path);
            //var supportedSubsList = (from subs in supportedSubtitlesFormats where subs.Extension == extension select subs).ToList();
            //if (supportedSubsList.Count > 0)
            //{
            //    CurrentSubtitles.Save();
            //    CurrentSubtitlesFormat = supportedSubsList[0];
            //}
            //else
            //{
            //    throw new Exception("Not supported subtitles format.");
            //}   

            CurrentSubtitles.Save();

            OnSave();
        }

        /// <summary>
        /// Ustawia zestaw obsługiwanych napisów.
        /// </summary>
        /// <param name="fileFormats">Kolekcja z typami obsługiwanych napisów.</param>
        public void SetSupportedSubtitles(IEnumerable<SubtitlesFileFormat> subtitlesFileFormats)
        {
            supportedSubtitlesFormats = new HashSet<SubtitlesFileFormat>(subtitlesFileFormats);
        }

        /// <summary>
        /// Dodaje format obsługiwanych napisów do bieżącego zestawu menadżera.
        /// </summary>
        /// <param name="subtitlesFileFormat">Format do dodania.</param>
        /// <returns>Prawda jeśli format został dodany, w przciwnym razie fałsz.</returns>
        public bool AddSupportedSubtitles(SubtitlesFileFormat subtitlesFileFormat)
        {
            return supportedSubtitlesFormats.Add(subtitlesFileFormat);
        }

        /// <summary>
        /// usuwa format obsługiwanych napisów z bieżącego zestawu menadżera.
        /// </summary>
        /// <param name="subtitlesFileFormat">Format do usunięcia.</param>
        /// <returns>Prawda jeśli format został dodany, w przciwnym razie fałsz.</returns>
        public bool RemoveSupportedSubtitles(SubtitlesFileFormat subtitlesFileFormat)
        {
            return supportedSubtitlesFormats.Remove(subtitlesFileFormat);
        }

        /// <summary>
        /// Określa czy dany format napisów jest dodany do obsługiwanych formatów menadżera.
        /// </summary>
        /// <param name="subtitlesFileFormat">Format do określenia.</param>
        /// <returns>Prawda jeśli obsługiwany, w przeciwnym razie fałsz.</returns>
        public bool AreSubtitlesSupported(MicroDVDFileFormat subtitlesFileFormat)
        {
            return supportedSubtitlesFormats.Contains(subtitlesFileFormat);
        }

        /// <summary>
        /// Czyści zestaw aktualnie obsługiwanych napisów.
        /// </summary>
        public void ClearSupportedSubtitles()
        {
            supportedSubtitlesFormats.Clear();
        }

        private bool IsCurrentSubtitlesExtensionSupported()
        {
            String extension = CurrentSubtitles.Extension;
            return supportedSubtitlesFormats.FirstOrDefault(format => format.IsExtensionCorrect(extension)) != null;
        }

        private SubtitlesFileFormat GetCurrentSubtitlesFormat()
        {
            SubtitlesFormatResolver formatResolver = new SubtitlesFormatResolver(CurrentSubtitles);
            return formatResolver.Resolve();
        }

        private void OnCurrentSubtitlesChanged(SubtitlesFile oldSubtitles, SubtitlesFile newSubtitles)
        {
            var temporaryEventHolder = CurrentSubtitlesChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new SubtitlesChangedEventArgs(oldSubtitles, newSubtitles));
            }
        }

        private void OnCurrentSubtitlesFileFormatChanged()
        {
            var temporaryEventHolder = CurrentSubtitlesFormatChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs());
            }
        }

        private void OnLoad()
        {
            var temporaryEventHolder = CurrentSubtitlesLoaded;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs());
            }
        }

        private void OnSave()
        {
            var temporaryEventHolder = CurrentSubtitlesSaved;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs());
            }
        }

    }
}
