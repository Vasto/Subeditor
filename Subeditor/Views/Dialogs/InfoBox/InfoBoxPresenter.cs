using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Presenters;
using KWinFramework.Views;
using Subeditor.Model;

namespace Subeditor.Views.Dialogs.InfoBox
{
    /// <summary>
    /// Prezenter widoku z komunikatem.
    /// </summary>
    class InfoBoxPresenter : PresenterBase<IInfoBoxView>
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="message">Treść komunikatu jaka ma zostać przedstawiona na widoku.</param>
        /// <param name="messageImage"></param>
        public InfoBoxPresenter(IViewManager viewManager, IInfoBoxView view, String message, String messageImage)
            : base(viewManager, view)
        {
            this.View.Ok += new EventHandler(OkHandler);
            this.View.Message = message;
            this.View.MessageImage = messageImage;
        }

        private void OkHandler(object sender, EventArgs e)
        {
            ViewManager.CloseView(View);
        }

    }
}
