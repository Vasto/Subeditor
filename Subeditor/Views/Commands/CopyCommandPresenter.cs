using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Presenters.Commands;
using KWinFramework.Views;
using KWinFramework.Views.WinForms.Commands;
using Subeditor.Model;

namespace Subeditor.Views.Commands
{
    /// <summary>
    /// Reprezentuje prezentera polecenia kopiowania.
    /// </summary>
    class CopyCommandPresenter : CommandPresenter
    {
        private SubtitlesEditor subtitlesEditor;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="editor">Obiekt menadżera zajmującego się edycjią napisów.</param>
        public CopyCommandPresenter(IViewManager viewManager, ICommandView view, SubtitlesEditor subtitlesEditor)
            : base(viewManager, view)
        {
            this.subtitlesEditor = subtitlesEditor;
            this.subtitlesEditor.Clipboard.CanCopyChanged += new EventHandler<EventArgs<bool>>(ClipboardCanCopyChangedHandler);

            this.View.IsExecutable = this.subtitlesEditor.Clipboard.CanCopy;
        }

        /// <summary>
        /// Metoda wywoływana w momencie odpalenia polecenia.
        /// </summary>
        protected override void OnExecute()
        {
            subtitlesEditor.Clipboard.Copy();
        }

        private void ClipboardCanCopyChangedHandler(object sender, EventArgs<bool> e)
        {
            this.View.IsExecutable = e.Value;
        }

    }
}
