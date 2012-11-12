using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KWinFramework.Views;
using Subeditor.Views.Main;

namespace Subeditor.Model
{
    /// <summary>
    /// Menadżer aplikacji.
    /// </summary>
    class ApplicationManager
    {
        private IViewManager viewManager;

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="viewManager">Menadżer widoków (musi zawierać główny widok aplikacji).</param>
        public ApplicationManager(IViewManager viewManager)
        {
            this.viewManager = viewManager;
            this.ApplicationMainView = (MainFormView)viewManager.GetView(Subeditor.Properties.Resources.NameMainView);
        }

        /// <summary>
        /// Widok główny apikacji.
        /// </summary>
        public MainFormView ApplicationMainView 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Uruchamia aplikację.
        /// </summary>
        public void StartApplication()
        {
            Application.Run(ApplicationMainView);
        }

        /// <summary>
        /// Kończy działanie aplikacji.
        /// </summary>
        public void StopApplication()
        {
           //Application.Exit();
           //ApplicationMainView.CloseView();
            viewManager.CloseView(ApplicationMainView);
        }

    }
}
