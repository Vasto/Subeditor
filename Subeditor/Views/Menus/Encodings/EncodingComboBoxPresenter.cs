using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using KWinFramework.Presenters;
using KWinFramework.Views;
using KWinFramework.Views.WinForms.Menus;
using Subeditor.Model;

namespace Subeditor.Views.Menus.Encodings
{
    /// <summary>
    /// Prezenter ComboBoxa pozwalającego na wybór sposobu kodowania.
    /// </summary>
    class EncodingComboBoxPresenter : PresenterBase<IToolStripComboBoxView>
    {
        private SubtitlesManager subtitlesManager;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Obiekt menadżera widoków.</param>
        /// <param name="view">Obiekt zarządzanego przez prezentera widoku.</param>
        /// <param name="subtitlesManager">Menadżer napisów.</param>
        public EncodingComboBoxPresenter(IViewManager viewManager, IToolStripComboBoxView view, SubtitlesManager subtitlesManager)
            : base(viewManager, view)
        {
            this.subtitlesManager = subtitlesManager;

            this.View.SelectedValueChanged += new EventHandler(ViewSelectedValueChangedHandler);
            var encodingList = EncodingDisplayNameMap.GetAllNames();
            this.View.Values = encodingList;
            this.View.SelectedValue = encodingList.First();      
        }

        private void ViewSelectedValueChangedHandler(object sender, EventArgs e)
        {
            int encodingCodepage = EncodingDisplayNameMap.GetCodepageForName(View.SelectedValue);
            Encoding selectedEncoding = Encoding.GetEncoding(encodingCodepage);
            subtitlesManager.CurrentSubtitles.OutputEncoding = selectedEncoding;
        }

    }
}
