using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Subeditor.Model.Modifications;

namespace Subeditor.Model
{
    /// <summary>
    /// Klasa schowka aplikacji Subeditor.
    /// Wykorzystywana przy operacjiach kopiowania, wycinania, wklejania.
    /// </summary>
    class SubtitlesClipboard : IClipboard
    {
        private SubtitlesEditor subtitlesEditor;
        private ClipboardAssistant clipboardAssistant;
        private bool canCut;
        private bool canCopy;
        private bool canPaste;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="subtitlesEditor">Edytor napisów.</param>
        public SubtitlesClipboard(SubtitlesEditor subtitlesEditor)
        {
            this.subtitlesEditor = subtitlesEditor;
            this.subtitlesEditor.EditStateChanged += new EventHandler<SubtitlesEditStateChangedEventArgs>(EditStateChangedHandler);

            this.InitializeOperationsReadiness();
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="subtitlesEditor">Edytor napisów.</param>
        /// <param name="clipboardAssistant">Asystent schowka.</param>
        public SubtitlesClipboard(SubtitlesEditor subtitlesEditor, ClipboardAssistant clipboardAssistant)
        {
            this.subtitlesEditor = subtitlesEditor;
            this.subtitlesEditor.EditStateChanged += new EventHandler<SubtitlesEditStateChangedEventArgs>(EditStateChangedHandler);

            this.ClipboardAssistant = clipboardAssistant;

            this.InitializeOperationsReadiness();
        }

        /// <summary>
        /// Zdarzenie mające miejsce gdy możliwość wykonania opercja wycinania ulegnie zmianie.
        /// </summary>
        public event EventHandler<EventArgs<bool>> CanCutChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy możliwość wykonania opercja kopiowania ulegnie zmianie.
        /// </summary>
        public event EventHandler<EventArgs<bool>> CanCopyChanged;

        /// <summary>
        /// Zdarzenie mające miejsce gdy możliwość wykonania opercja wklejania ulegnie zmianie.
        /// </summary>
        public event EventHandler<EventArgs<bool>> CanPasteChanged;

        /// <summary>
        /// Pozwala pobrać lub ustawić asystenta schowka.
        /// Ustawnie tego obiektu umożliwia regowanie na zmiany w schowku systemowym dokonane przez inne aplikacje.
        /// </summary>
        public ClipboardAssistant ClipboardAssistant 
        {
            get 
            { 
                return clipboardAssistant; 
            }
            set
            {
                if (clipboardAssistant != null)
                {
                    clipboardAssistant.ClipboardChanged -= new EventHandler<EventArgs<String>>(ClipboardChangedHandler);
                }

                clipboardAssistant = value;
                clipboardAssistant.ClipboardChanged += new EventHandler<EventArgs<String>>(ClipboardChangedHandler);
            }
        }

        /// <summary>
        /// Pobiera informacje o tym czy opercja wycinania jest wykonalna.
        /// </summary>
        public bool CanCut
        {
            get
            {
                return canCut;
            }
            set
            {
                canCut = value;
                OnCanCutChanged(value);
            }
        }

        /// <summary>
        /// Pobiera informacje o tym czy opercja kopiowania jest wykonalna.
        /// </summary>
        public bool CanCopy
        {
            get
            {
                return canCopy;
            }
            set
            {
                canCopy = value;
                OnCanCopyChanged(value);
            }
        }

        /// <summary>
        /// Pobiera informacje o tym czy opercja wklejania jest wykonalna.
        /// </summary>
        public bool CanPaste
        {
            get
            {
                return canPaste;
            }
            set
            {
                canPaste = value;
                OnCanPasteChanged(value);
            }
        }

        /// <summary>
        /// Wytnij.
        /// </summary>
        public void Cut()
        {
            SubtitlesEditState editState = subtitlesEditor.EditState;
            
            String content = subtitlesEditor.SubtitlesContent;
            Selection selection = editState.Selection;
            String contentToCut = content.Substring(selection.Start, selection.Length);

            Clipboard.SetText(contentToCut);

            String cuttedContent = content.Remove(selection.Start, selection.Length);

            SubtitlesContentModification contentMod = new SubtitlesContentModification(cuttedContent, SubtitlesContentModificationArea.Selection);

            SubtitlesEditState newEditState = new SubtitlesEditState(selection.Start, new Selection(selection.Start, 0));
            SubtitlesEditStateModification editMod = new SubtitlesEditStateModification(newEditState);

            subtitlesEditor.PerformModification(new CompositeModification(contentMod, editMod));
        }

        /// <summary>
        /// Kopiuj.
        /// </summary>
        public void Copy()
        {
            SubtitlesEditState editState = subtitlesEditor.EditState;

            String content = subtitlesEditor.SubtitlesContent;
            Selection selection = editState.Selection;
            String contentToCopy = content.Substring(selection.Start, selection.Length);

            Clipboard.SetText(contentToCopy);
        }

        /// <summary>
        /// Wklej.
        /// </summary>
        public void Paste()
        {
            StringBuilder modifiedContentBuilder = new StringBuilder(subtitlesEditor.SubtitlesContent);

            SubtitlesContentModificationArea modificationArea = SubtitlesContentModificationArea.PostCaret;
            
            SubtitlesEditState editState = subtitlesEditor.EditState;
            int insertPosition = editState.CaretPosition;

            Selection selection = editState.Selection;
            if (selection.Length > 0)
            {
                modifiedContentBuilder.Remove(selection.Start, selection.Length);

                modificationArea = SubtitlesContentModificationArea.Selection;
                insertPosition = selection.Start;
            }

            String contentToInsert = Clipboard.GetText();
            modifiedContentBuilder.Insert(insertPosition, contentToInsert);           

            SubtitlesContentModification contentMod = new SubtitlesContentModification(modifiedContentBuilder.ToString(), modificationArea);

            int postInsertCaret = insertPosition + contentToInsert.Length;
            SubtitlesEditState newEditState = new SubtitlesEditState(postInsertCaret, new Selection(postInsertCaret, 0));
            SubtitlesEditStateModification editMod = new SubtitlesEditStateModification(newEditState);
            
            subtitlesEditor.PerformModification(new CompositeModification(contentMod, editMod));
        }

        private void InitializeOperationsReadiness()
        {
            var selection = subtitlesEditor.EditState.Selection;

            CanCut = (selection != null) ? (selection.Length > 0) : false;
            CanCopy = (selection != null) ? (selection.Length > 0) : false;
            CanPaste = Clipboard.ContainsText();
        }

        private void EditStateChangedHandler(object sender, SubtitlesEditStateChangedEventArgs e)
        {
            if (e.NewState.Selection.Length > 0)
            {
                CanCut = true;
                CanCopy = true;
            }
            else
            {
                CanCut = false;
                CanCopy = false;
            }
        }

        private void ClipboardChangedHandler(object sender, EventArgs<String> e)
        {
            if (String.IsNullOrEmpty(e.Value))
            {
                CanPaste = false;
            }
            else
            {
                CanPaste = true;
            }
        }

        private void OnCanCutChanged(bool newValue)
        {
            var temporaryEventHolder = CanCutChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<bool>(newValue));
            }
        }

        private void OnCanCopyChanged(bool newValue)
        {
            var temporaryEventHolder = CanCopyChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<bool>(newValue));
            }
        }

        private void OnCanPasteChanged(bool newValue)
        {
            var temporaryEventHolder = CanPasteChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<bool>(newValue));
            }
        }

    }
}
