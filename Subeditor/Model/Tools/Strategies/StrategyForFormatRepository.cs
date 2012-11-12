using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Subeditor.Model.FileFormats;

namespace Subeditor.Model.Tools.Strategies
{
    /// <summary>
    /// Repozytorium strategi edycji dla danych formatów plikó.
    /// Przechowuje obiekty IEditStrategy, oraz umożliwia powiązanie ich z odpowiednimi obiektami formatu plików - SubtitlesFileFormat.
    /// </summary>
    class StrategyForFormatRepository
    {
        private IDictionary<Type, SubtitlesFileFormat> strategyToFormatMap;
        private IDictionary<SubtitlesFileFormat, Type> formatToStrategyMap;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        public StrategyForFormatRepository()
        {
            this.strategyToFormatMap = new Dictionary<Type, SubtitlesFileFormat>();
            this.formatToStrategyMap = new Dictionary<SubtitlesFileFormat, Type>();
        }

        /// <summary>
        /// Pozwala pobrać zbiór formatów plików zarejestrowanych w repozytorium.
        /// </summary>
        public IEnumerable<SubtitlesFileFormat> RegisteredFormats
        {
            get 
            { 
                return strategyToFormatMap.Values; 
            }
        }

        /// <summary>
        /// Pozwala pobrać zbiór strategi zarejestrowanych w repozytorium.
        /// </summary>
        public IEnumerable<Type> RegisteredStrategies
        {
            get 
            { 
                return formatToStrategyMap.Values; 
            }
        }

        /// <summary>
        /// Rejestruje podany typ strategi oraz powiązany z nią typ pliku napisów.
        /// </summary>
        /// <param name="strategyType">Typ strategi do zarejestrowania.</param>
        /// <param name="associatedFormat">Powiązany format plików.</param>
        public void RegisterStrategy(Type strategyType, SubtitlesFileFormat associatedFormat)
        {
            bool canAddStrategy = false;
            if (!strategyToFormatMap.ContainsKey(strategyType))
            {
                canAddStrategy = true;
            }

            bool canAddSubsFormat = false;
            if (!formatToStrategyMap.ContainsKey(associatedFormat))
            {
                canAddSubsFormat = true;
            }

            if (canAddStrategy && canAddSubsFormat)
            {
                strategyToFormatMap.Add(strategyType, associatedFormat);
                formatToStrategyMap.Add(associatedFormat, strategyType);
            }
        }

        /// <summary>
        /// Wyrejestrowywuje podany typ strategi z repozytorium.
        /// </summary>
        /// <param name="strategyType">Typ strategi do wyrejestrowania.</param>
        public void UnregisterStrategy(Type strategyType)
        {
            if (strategyToFormatMap.ContainsKey(strategyType))
            {
                SubtitlesFileFormat format = strategyToFormatMap[strategyType];

                strategyToFormatMap.Remove(strategyType);
                formatToStrategyMap.Remove(format);
            }
        }

        /// <summary>
        /// Pobiera typ strategi powiązany z podanym formatem pliku.
        /// </summary>
        /// <param name="format">Format pliku.</param>
        /// <exception cref="System.ArgumentException">
        /// Wyjątek mający miejsce, jeśli nie znaleziono typu strategi dla podanego formatu.
        /// </exception>
        /// <returns>Typ strategi.</returns>
        public Type GetStrategyForFormat(SubtitlesFileFormat format)
        {
            if (formatToStrategyMap.ContainsKey(format))
            {
                return formatToStrategyMap[format];
            }
            else
            {
                throw new ArgumentException("Invalid format argument. Format not associated with any strategy.");
            }
        }

        /// <summary>
        /// Pobiera typ format pliku powiązany z podanym typem strategi.
        /// </summary>
        /// <param name="strategyType">Typ strategi.</param>
        /// <exception cref="System.ArgumentException">
        /// Wyjątek mający miejsce, jeśli nie znaleziono formatu pliku dla podanego typu strategi.
        /// </exception>
        /// <returns>Format pliku.</returns>
        public SubtitlesFileFormat GetFormatForStrategy(Type strategyType)
        {
            if (strategyToFormatMap.ContainsKey(strategyType))
            {
                return strategyToFormatMap[strategyType];
            }
            else
            {
                throw new ArgumentException("Invalid strategyType argument. Type not registered.");
            }
        }

        /// <summary>
        /// Określa czy podany typ strategi został zarejestrowany w repozytorium.
        /// </summary>
        /// <param name="strategy">Typ strategi do sprawdzenia.</param>
        /// <returns>Prawda jeśli podany typ został zarejestrowany.</returns>
        public bool IsStrategyRegistered(Type strategy)
        {
            return strategyToFormatMap.ContainsKey(strategy);
        }

        /// <summary>
        /// Pozwala wyczyścic informacje przechowywane w repozytorium.
        /// </summary>
        public void Clear()
        {
            strategyToFormatMap.Clear();
            formatToStrategyMap.Clear();
        }

    }
}
