using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Subeditor.Model.FileFormats;
using Subeditor.Model.Tools.Strategies;

namespace Subeditor.Model.Tools
{
    /// <summary>
    /// Klasa bazowa dla klas narzędzi operujących na napisach.
    /// </summary>
    abstract class EditToolBase
    {
        private SubtitlesManager manager;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="manager">Menadżer napisów.</param>
        /// <param name="editor">Edytor napisów</param>
        public EditToolBase(SubtitlesManager manager, SubtitlesEditor editor) 
            : this(manager, editor, new StrategyForFormatRepository())
        {
            this.InitialzieStrategyRepository();
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="manager">Menadżer napisów.</param>
        /// <param name="editor">Edytor napisów</param>
        /// <param name="strategyRepository"></param>
        public EditToolBase(SubtitlesManager manager, SubtitlesEditor editor, StrategyForFormatRepository strategyRepository)
        {
            this.StrategyRepository = new StrategyForFormatRepository();

            this.Manager = manager;
            this.Manager.CurrentSubtitlesChanged += new EventHandler<SubtitlesChangedEventArgs>(CurrentSubtitlesChangedHandler);

            this.Editor = editor;
        }

        /// <summary>
        /// Pozwala klasą potomnym na pobranie lub ustawienie
        /// obiektu menadżera napisów.
        /// </summary>
        protected SubtitlesManager Manager 
        {
            get 
            { 
                return manager; 
            }
            set
            {
                if (manager != null)
                {
                    manager.CurrentSubtitlesChanged -= new EventHandler<SubtitlesChangedEventArgs>(CurrentSubtitlesChangedHandler);
                }
                manager = value;
                manager.CurrentSubtitlesChanged += new EventHandler<SubtitlesChangedEventArgs>(CurrentSubtitlesChangedHandler);
            }
        }

        /// <summary>
        /// Pozwala klasą potomnym na pobranie lub ustawienie
        /// obiektu edytora napisów powiązanego z bierzącym narzędziem.
        /// </summary>
        protected SubtitlesEditor Editor 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Pozwala klasą potomnym na pobranie lub ustawienie
        /// obiektu repozytorium przechowującego strategie edycji dla danych formatów plików.
        /// </summary>
        protected StrategyForFormatRepository StrategyRepository 
        {
            get; 
            set; 
        }

        /// <summary>
        /// Pozwala stworzyć obiekt strategi edycji adekwateny do aktulnego formatu pliku napisów.
        /// </summary>
        protected virtual IEditStrategy CreateStrategyForCurrentFormat()
        {
            SubtitlesFileFormat currentFormat = Manager.CurrentSubtitlesFormat;
            Type strategy = StrategyRepository.GetStrategyForFormat(currentFormat);
            String subsContent = Editor.SubtitlesContent;

            return (IEditStrategy)Activator.CreateInstance(strategy, subsContent);
        }

        /// <summary>
        /// Inicjalizuje obiekt repozytorim strategi odpowiednimi typami strategi i powiązanymi z nimi typami plików napisów.
        /// </summary>
        protected virtual void InitialzieStrategyRepository()
        {
            StrategyRepository.RegisterStrategy(typeof(AssEditStrategy), new AssFileFormat());
            StrategyRepository.RegisterStrategy(typeof(SSAEditStrategy), new SSAFileFormat());
            StrategyRepository.RegisterStrategy(typeof(SubripEditStrategy), new SubripFileFormat());
            StrategyRepository.RegisterStrategy(typeof(MicroDVDEditStrategy), new MicroDVDFileFormat());
            StrategyRepository.RegisterStrategy(typeof(TMPlayerEditStrategy), new TMPlayerFileFormat());
            StrategyRepository.RegisterStrategy(typeof(EmptyEditStrategy), new UnresolvedFileFormat());
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnCurrentSubtitlesChanged()
        {

        }

        private void CurrentSubtitlesChangedHandler(object sender, SubtitlesChangedEventArgs e)
        {
            OnCurrentSubtitlesChanged();
        }

    }
}
