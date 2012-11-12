using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using KWinFramework.Views.WinForms;
using Subeditor.Model;
using Subeditor.Views.Subtitles;

namespace Subeditor.Views.SubtitlesFiles
{
    /// <summary>
    /// Klasa widoku napisów.
    /// </summary>
    public partial class SubtitlesView : UserControlView, ISubtitlesView
    {
        #region ISubtitlesView

        /// <summary>
        /// Zdarzenie mające miejsce w momencie zmiany edytowanego tekstu napisów.
        /// </summary>
        public event EventHandler ContentChanged;

        /// <summary>
        /// Zdarzenie mające miejsce w moemencie zmiany obaszaru zaznaczenia.
        /// </summary>
        public event EventHandler<EventArgs<String>> SelectionChanged;

        /// <summary>
        /// Tekstowa zawartość edytowanego pliku napisów.
        /// </summary>
        public String Content
        {
            get { return this.textEditor.Text; }
            set { this.textEditor.Text = value; }
        }

        private void OnTextEditorTextChanged(object sender, EventArgs e)
        {
            var temporaryEventHolder = ContentChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, e);
            }
        }

        #endregion //ISubtitlesView

        private SelectionManager selectionManager;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        public SubtitlesView()
        {
            InitializeComponent();

            this.selectionManager = textEditor.ActiveTextAreaControl.SelectionManager;
            this.selectionManager.SelectionChanged += new EventHandler(SelectionChangedHandler);
        }

        private void SelectionChangedHandler(object sender, EventArgs e)
        {
            var temporaryEventHolder = SelectionChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs<String>(selectionManager.SelectedText));
            }
        }
    }
}
