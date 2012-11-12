using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Presenters.Commands;
using KWinFramework.Views;
using KWinFramework.Views.WinForms.Commands;
using Subeditor.Model;
using Subeditor.Views.Dialogs.SaveBeforeClose;

namespace Subeditor.Views.Commands
{
    /// <summary>
    /// Reprezentuje prezentera polecenia zamknięcia aplikacji.
    /// </summary>
    class ExitCommandPresenter : CommandPresenter
    {
        private ApplicationManager applicationManager;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="appManager">Menadżer aplikacji.</param>
        public ExitCommandPresenter(IViewManager viewManager, ICommandView view, ApplicationManager appManager)
            : base(viewManager, view)
        {
            this.applicationManager = appManager;
        }

        /// <summary>
        /// Metoda wywoływana w momencie odpalenia polecenia.
        /// </summary>
        protected override void OnExecute()
        {
            applicationManager.StopApplication();
        }

    }
}
