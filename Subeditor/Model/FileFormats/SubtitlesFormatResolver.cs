using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Subeditor.Model.Tools.Strategies;

namespace Subeditor.Model.FileFormats
{
    /// <summary>
    /// Odpowiada za określenie formatu podanych napisów w oparciu o ich treść.
    /// </summary>
    class SubtitlesFormatResolver
    {
        private StrategyForFormatRepository strategyRepository;
        private SubtitlesFile file;
        
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="file">Obiekt pliku napisów, którego format ma być rozsztrzygnięty.</param>
        public SubtitlesFormatResolver(SubtitlesFile file) 
            : this(file, new StrategyForFormatRepository())
        {
            InitialzieStrategyRepository();
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="file">Obiekt pliku napisów, którego format ma być rozsztrzygnięty.</param>
        /// <param name="strategyRepository">Repozytorium strategii, wykorzystywanych do czytania zawartości pliku.</param>
        public SubtitlesFormatResolver(SubtitlesFile file, StrategyForFormatRepository strategyRepository)
        {
            this.strategyRepository = strategyRepository;
            this.file = file;
        }

        /// <summary>
        /// Rozstrzyga format pliku, w oparciu o jego zawartość i znane metody jej odczytwania.
        /// </summary>
        /// <exception cref=""></exception>
        /// <returns>Obiekt reprezentujący format rozsztrzyganego pliku.</returns>
        public virtual SubtitlesFileFormat Resolve()
        {
            var formatsWithMatchingExtension = from format in strategyRepository.RegisteredFormats 
                                               where format.IsExtensionCorrect(file.Extension) 
                                               select format;

            foreach (var format in formatsWithMatchingExtension)
            {
                Type strategyType = strategyRepository.GetStrategyForFormat(format);
                IEditStrategy stategy = (IEditStrategy)Activator.CreateInstance(strategyType, file.Content);

                //To do: zastanowić się czy to jest dobry sposób sprawdzania poprawności formatu.
                //To do: może lepiej stowrzyc jakies ogolne NextEntry bo nie koniecznie musi być zawsze czas.
                if (stategy.NextTimedEntry() != null)
                {
                    return format;
                }
            }

            return new UnresolvedFileFormat();
        }

        /// <summary>
        /// Inicjalizuje repozytorium strategii odczytywania, wykorzystywanych dla podanych formatów plików.
        /// </summary>
        protected virtual void InitialzieStrategyRepository()
        {
            strategyRepository.RegisterStrategy(typeof(AssEditStrategy), new AssFileFormat());
            strategyRepository.RegisterStrategy(typeof(SSAEditStrategy), new SSAFileFormat());
            strategyRepository.RegisterStrategy(typeof(SubripEditStrategy), new SubripFileFormat());
            strategyRepository.RegisterStrategy(typeof(MicroDVDEditStrategy), new MicroDVDFileFormat());
            strategyRepository.RegisterStrategy(typeof(TMPlayerEditStrategy), new TMPlayerFileFormat());
        }

    }
}
