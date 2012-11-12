using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Subeditor.Model.Modifications;
using Subeditor.Model.StateManagement;
using Subeditor.Utilities;

namespace Subeditor.Model
{
    /// <summary>
    /// Edytor napisów. Pozwala na przeprowadzenie modyfikacji napisów.
    /// </summary>
    class SubtitlesEditor
    {

        private SubtitlesEditState editState;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="subtitlesManager">Menadżer zarządzający obiektami napisów.</param>
        public SubtitlesEditor(SubtitlesManager subtitlesManager) : this(subtitlesManager, null)
        {
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="subtitlesManager">Menadżer zarządzający obiektami napisów.</param>
        /// <param name="undoRedoManager">Obiekt implementujący zachowania menadżera wycofywania/przywracania zmian.</param>
        public SubtitlesEditor(SubtitlesManager subtitlesManager, IUndoRedoManager undoRedoManager)
        {
            this.SubtitlesManager = subtitlesManager;
            this.SubtitlesManager.CurrentSubtitlesChanged += new EventHandler<SubtitlesChangedEventArgs>(SubtitlesManagerSubtitlesChangedHandler);

            this.UndoRedoManager = undoRedoManager;

            this.Subtitles = subtitlesManager.CurrentSubtitles;
            this.Subtitles.ContentChanged += new EventHandler<SubtitlesContentChangedEventArgs>(SubtitlesContentChangedHandler);

            this.editState = new SubtitlesEditState();

            this.Clipboard = new SubtitlesClipboard(this);
        }

        /// <summary>
        /// Zdarzenie mające miejsce w momencie zmiany treści bieżącego pliku napisów.
        /// </summary>
        public event EventHandler SubtitlesContentChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy stan edytowania pliku napisów ulegnie zmianie.
        /// </summary>
        public event EventHandler<SubtitlesEditStateChangedEventArgs> EditStateChanged;

        /// <summary>
        /// Pobiera obecnie edytowany plik napisów.
        /// </summary>
        public SubtitlesFile Subtitles 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić treść obecnie dytowanego pliku napisów.
        /// </summary>
        public String SubtitlesContent
        {
            get 
            { 
                return Subtitles.Content; 
            }
            set 
            { 
                Subtitles.Content = value; 
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić obiekt opisujący obecny stan edytowania pliku napisów.
        /// </summary>
        public SubtitlesEditState EditState
        {
            get
            {
                return editState;
            }
            set
            {
                SubtitlesEditState oldState = editState;
                editState = value;
                OnEditStateChanged(oldState, value);
            }
        }

        /// <summary>
        /// Pozwala pobrać obiekt schowka, wykorzystywany przez edytor do opercji kopiowania/wklejania.
        /// </summary>
        public IClipboard Clipboard 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Pozwala pobrać SubtitlesManager powiązany z obecnym obiektem SubtitlesEdiotra.
        /// </summary>
        protected SubtitlesManager SubtitlesManager 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić menadżer cofania i przywracania zmian.
        /// </summary>
        protected IUndoRedoManager UndoRedoManager 
        { 
            get; 
            set;
        }

        /// <summary>
        /// Wykonuje wskazana modyfikację.
        /// </summary>
        /// <param name="modification">Obiekt implementujący interfejs modyfikacji.</param>
        /// <remarks>
        /// </remarks>
        public void PerformModification(IModification modification)
        {
            if (modification is CompositeModification)
            {
                foreach (var mod in (modification as CompositeModification))
                {
                    AssignModificationTarget(mod);
                }
            }
            else
            {
                AssignModificationTarget(modification);
            }

            modification.Perform();

            AddModificationToUndoRedoManager(modification);
        }

        /// <summary>
        /// Zanzancza określoną część edytowanego tekstu napisów.
        /// </summary>
        /// <param name="selectionStart">Indeks początku zaznaczenia.</param>
        /// <param name="selectionLength">Długość zaznaczenia.</param>
        public void SelectContent(int selectionStart, int selectionLength)
        {
            EditState = new SubtitlesEditState(selectionStart + selectionLength, new Selection(selectionStart, selectionLength));
        }

        /// <summary>
        /// Zanzancza określoną część edytowanego tekstu napisów.
        /// </summary>
        /// <param name="selection">Tekst do zaznaczenia.</param>
        public void SelectContent(String selection)
        {
            int selectionStart = SubtitlesContent.IndexOf(selection);
            int selectionLength = selection.Length;

            SelectContent(selectionStart, selectionLength);
        }

        /// <summary>
        /// Zaznacza cały edytowany tekst napisów.
        /// </summary>
        public void SelectAllContent()
        {
            SelectContent(0, SubtitlesContent.Length);
        }


        private void SubtitlesManagerSubtitlesChangedHandler(object sender, EventArgs e)
        {
            if (Subtitles != null)
            {
                Subtitles.ContentChanged -= new EventHandler<SubtitlesContentChangedEventArgs>(SubtitlesContentChangedHandler);
            }           

            Subtitles = SubtitlesManager.CurrentSubtitles;
            Subtitles.ContentChanged += new EventHandler<SubtitlesContentChangedEventArgs>(SubtitlesContentChangedHandler);
            //EditState = new SubtitlesEditState();    

            ResetUndoManager();
        }

        private void ResetUndoManager()
        {
            if (UndoRedoManager != null)
            {
                UndoRedoManager.DeleteUndoablesRedoeables();
            }
        }

        private void SubtitlesContentChangedHandler(object sender, SubtitlesContentChangedEventArgs e)
        {
            var temporaryEventHolder = SubtitlesContentChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, e);
            }
        }

        private void OnEditStateChanged(SubtitlesEditState oldValue, SubtitlesEditState newValue)
        {
            var temporaryEventHolder = EditStateChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new SubtitlesEditStateChangedEventArgs(oldValue, newValue));
            }
        }

        private void AssignModificationTarget(IModification modification)
        {
            if (modification is IModificationSource<SubtitlesEditor>)
            {
                var mod = modification as IModificationSource<SubtitlesEditor>;
                if (mod.ModificationTarget == null)
                {
                    mod.ModificationTarget = this;
                }
            }
        }

        private void AddModificationToUndoRedoManager(IModification modification)
        {
            if ((UndoRedoManager != null) && (modification is IUndoableRedoable))
            {
                //To do: Dodanie nie ustawialnej listy nie wspieranych przez undoRedoManager typów modyfikacji
                if (modification is SubtitlesEditStateModification)
                {
                    return;
                }
                //else if ((modification is CompositeModification) && (UndoRedoManager.LastUndoableRedoable is CompositeModification))
                //{
                    ////Co z sytuacją keidy mamy tylko SubtitlesContentModification, a nie composite?
                    //var currentCompositeMod = (CompositeModification)modification;
                    //var currentContentMods = from mod in currentCompositeMod where mod is SubtitlesContentModification select mod;

                    //var lastCompositeMod = (CompositeModification)UndoRedoManager.LastUndoableRedoable;
                    //var lastContentMods = from mod in lastCompositeMod where mod is SubtitlesContentModification select mod;

                    //if (new HashSet<IModification>(currentContentMods).SetEquals(lastContentMods))
                    //{
                    //    UndoRedoManager.DeleteLastUndoableRedoeable();
                    //}
                //}

                UndoRedoManager.AddUndoableRedoable(modification as IUndoableRedoable);
            }
        }

    }
}
