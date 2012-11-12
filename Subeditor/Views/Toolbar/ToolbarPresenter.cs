using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework;
using KWinFramework.Presenters;
using KWinFramework.Views;
using Subeditor.Model;

namespace Subeditor.Views.Toolbar
{
    /// <summary>
    /// Prezenter paska narzędziowego.
    /// </summary>
    class ToolbarPresenter : PresenterBase<IToolbarView>
    {
        private SubtitlesManager subtitlesManager;
        private CommandFacade commandFacade;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt widoku zarządzanego przez prezentera.</param>
        /// <param name="subtitlesManager">Obiekt modelu menadżera plików.</param>
        /// <param name="commandFacade"></param>
        public ToolbarPresenter(IViewManager viewManager, IToolbarView view, SubtitlesManager subtitlesManager, CommandFacade commandFacade) 
            : base(viewManager, view)
        {
            this.subtitlesManager = subtitlesManager;
            this.commandFacade = commandFacade;
        }
    }
}
