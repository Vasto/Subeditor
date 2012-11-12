using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using KWinFramework.Views.WinForms;
using KWinFramework.Views.WinForms.Menus;
using Subeditor.Model;

namespace Subeditor.Views.Main
{
    /// <summary>
    /// Widok głównej formy aplikacji.
    /// </summary>
    public partial class MainFormView : FormView, IMainFormView
    {
        #region IMainFormView

        /// <summary>
        /// Zdarzenie żądania wyświetlenia widoku.
        /// </summary>
        public event EventHandler ShowRequest;

        /// <summary>
        /// Zdarzenie żądania zamknięcia widoku.
        /// </summary>
        public event EventHandler CloseRequest;

        /// <summary>
        /// Zdarzenie poprzedzające żądanie zamknięcia widoku. 
        /// </summary>
        public event EventHandler<ViewPreCloseEventArgs> PreCloseRequest;

        /// <summary>
        /// Pozwala pobrać lub ustawić nagłówek widoku.
        /// </summary>
        public String Caption
        {
            get 
            { 
                return this.Text; 
            }
            set 
            { 
                this.Text = value; 
            }
        }

        /// <summary>
        /// Wyświetla widok.
        /// </summary>
        public override void ShowView()
        {
            base.ShowView();

            OnShowRequest();
        }

        /// <summary>
        /// Zamyka widok.
        /// </summary>
        public override void CloseView()
        {
            base.CloseView();

            OnCloseRequest();
        }

        private void OnShowRequest()
        {
            var temporaryEventHolder = ShowRequest;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs());
            }
        }

        private void OnCloseRequest()
        {
            var temporaryEventHolder = CloseRequest;
            if (temporaryEventHolder != null)
            {
                temporaryEventHolder(this, new EventArgs());
            }
        }

        //private void FormClosingHandler(object sender, FormClosingEventArgs e)
        //{
        //    var temporaryEventHolder = PreCloseRequest;
        //    if (temporaryEventHolder != null)
        //    {
        //        var args = new ViewPreCloseEventArgs(e.Cancel);
        //        temporaryEventHolder(this, args);

        //        e.Cancel = args.CancelViewClose;
        //    }      
        //}

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            var temporaryEventHolder = PreCloseRequest;
            if (temporaryEventHolder != null)
            {
                var args = new ViewPreCloseEventArgs(e.Cancel);
                temporaryEventHolder(this, args);

                e.Cancel = args.CancelViewClose;
            }  
        }

        #endregion //IMainFormView

        private IntPtr nextClipboardViewer;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        public MainFormView()
        {
            InitializeComponent();

            this.nextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewName">Unikalna nazwa instancji widoku.</param>
        public MainFormView(String viewName)
        {
            InitializeComponent();

            this.ViewName = viewName;

            this.nextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
        }

        /// <summary>
        /// Zdarzenie informujące o zmianie zawartości schowka.
        /// </summary>
        public event EventHandler<EventArgs<IDataObject>> ClipboardChanged;

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNext);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        protected static extern int SetClipboardViewer(int hWndNewViewer);

        /// <summary>
        /// Metoda przetwarzająca zdarzenia.
        /// </summary>
        /// <param name="m">Parametry zdarzenia.</param>
        protected override void WndProc(ref Message m)
        {
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x030D;

            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    OnClipboardChanged();
                    SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;

                case WM_CHANGECBCHAIN:
                    if (m.WParam == nextClipboardViewer)
                    {
                        nextClipboardViewer = m.LParam;
                    }
                    else
                    {
                        SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    }
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        /// <summary>
        /// Zwalnia używane zasoby.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            ChangeClipboardChain(this.Handle, nextClipboardViewer);

            base.Dispose(disposing);
        }

        private void OnClipboardChanged()
        {
            var temporaryEventHolder = ClipboardChanged;
            if (temporaryEventHolder != null)
            {
                var data = Clipboard.GetDataObject();
                temporaryEventHolder(this, new EventArgs<IDataObject>(data));
            }
        }

    }
}
