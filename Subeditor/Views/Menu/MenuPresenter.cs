using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Presenters;
using KWinFramework.Views;

namespace Subeditor.Views.Commands
{
    class MenuPresenter : PresenterBase<IMenuView>
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt widoku zarządzanego przez prezentera.</param>
        /// <param name="subtitlesManager">Obiekt modelu menadżera plików.</param>
        /// <param name="commandFacade"></param>
        public MenuPresenter(IViewManager viewManager, IMenuView view)
            : base(viewManager, view)
        {
        }
    }
}
