using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KWinFramework.Views.WinForms;

namespace Subeditor.Views.Subtitles
{
    public partial class ScintillaSubtitlesView : UserControlView, ISubtitlesView
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public ScintillaSubtitlesView()
        {
            InitializeComponent();

            this.firstVisibleLineBeforeMouseWheelScroll = 0;

            this.textEditor.NativeInterface.UsePopUp(false);
            this.textEditor.SelectionChanged += new EventHandler(SelectionChangedHandler);
            this.textEditor.MouseWheel += new MouseEventHandler(MouseWheelHandler);
        }

        #region ISubtitlesView

        private int firstVisibleLineBeforeMouseWheelScroll;

        /// <summary>
        /// Zdarzenie mające miejsce w momencie zmiany edytowanego tekstu napisów.
        /// </summary>
        public event EventHandler ContentChanged;

        /// <summary>
        /// Zdarzenie mające miejsce w moemencie zmiany obaszaru zaznaczenia.
        /// </summary>
        public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

        /// <summary>
        /// Zdarzenie mające miejsce przy przewijaniu.
        /// </summary>
        public event EventHandler<ScrolledEventArgs> Scrolled;

        /// <summary>
        /// Tekstowa zawartość edytowanego pliku napisów.
        /// </summary>
        public String Content
        {
            get 
            { 
                return this.textEditor.Text; 
            }
            set 
            { 
                this.textEditor.Text = value; 
            }
        }

        /// <summary>
        /// Zawartość edytowanego pliku w formie bajtów.
        /// </summary>
        public byte[] RawContent
        {
            get 
            {
                return this.textEditor.RawText; 
            }
        }

        /// <summary>
        /// Pozwala pobrac lub ustawić pozycję karetki.
        /// </summary>
        public int CaretPosition
        {
            get
            {
                return this.textEditor.Caret.Position; 
            }
            set 
            { 
                this.textEditor.Caret.Position = value;
            }
        }

        /// <summary>
        /// Pozwala pobrać numer lini w której znajduję się karetka.
        /// </summary>
        public int CaretLine
        {
            get 
            { 
                return textEditor.Caret.LineNumber; 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int SelectionStart
        {
            get 
            { 
                return textEditor.Selection.Start; 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int SelectionLength
        {
            get 
            { 
                return textEditor.Selection.Length; 
            }
        }

        /// <summary>
        /// Pozwala pobrać numer pierwszej widocznej lini.
        /// </summary>
        public int FirstVisibleLine
        {
            get 
            { 
                return textEditor.Lines.FirstVisible.Number; 
            }
        }

        /// <summary>
        /// Pozwala pobrać liczbę wszystkich widocznych lini.
        /// </summary>
        public int VisibleLinesCount
        {
            get
            { 
                return textEditor.Lines.VisibleCount; 
            }
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić informację o szerokości kolumny z numeracją lini.
        /// </summary>
        public int LineNumberColumnWidth
        {
            get 
            { 
                return textEditor.Margins[0].Width; 
            }
            set 
            {
                textEditor.Margins[0].Width = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ScrollToCaret()
        {
            textEditor.Scrolling.ScrollToCaret();
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void ScrollToBegin()
        {
            textEditor.Scrolling.ScrollBy(0, -FirstVisibleLine);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        public void ScrollByLine(int line)
        {
            textEditor.Scrolling.ScrollBy(0, line);
        }

        /// <summary>
        /// Zaznacza okreslony obszar.
        /// </summary>
        /// <param name="selectionStart">Indeks początku zaznaczenia.</param>
        /// <param name="selectionLength">Liczba znaków do zaznaczenia.</param>
        public void Select(int selectionStart, int selectionLength)
        {
            textEditor.Selection.Start = selectionStart;
            textEditor.Selection.Length = selectionLength;
        }

        /// <summary>
        /// Zaznacza całość.
        /// </summary>
        public void SelectAll()
        {
            textEditor.Selection.SelectAll();
        }

        #endregion //ISubtitlesView

        private void TextEditorTextChangedHandler(object sender, EventArgs e)
        {
            var temporaryEventHolder = ContentChanged;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, e);
            }
        }

        private void SelectionChangedHandler(object sender, EventArgs e)
        {
            var temporaryEventHolder = SelectionChanged;
            if (temporaryEventHolder != null)
            {
                ScintillaNET.Selection selection = textEditor.Selection;
                temporaryEventHolder(this, new SelectionChangedEventArgs(selection.Start, selection.Length));
            }
        }

        private void ScrollHandler(object sender, ScrollEventArgs e)
        {
            var temporaryEventHolder = Scrolled;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new ScrolledEventArgs(e.NewValue, e.OldValue));
            }
        }

        private void MouseWheelHandler(object sender, MouseEventArgs e)
        {
            var temporaryEventHolder = Scrolled;
            if (temporaryEventHolder != null)
            {
                int wheelDelta = FirstVisibleLine - firstVisibleLineBeforeMouseWheelScroll;
                temporaryEventHolder(this, new ScrolledEventArgs(FirstVisibleLine + wheelDelta, firstVisibleLineBeforeMouseWheelScroll + wheelDelta));
                firstVisibleLineBeforeMouseWheelScroll = FirstVisibleLine;
            }
        }

    }
}
