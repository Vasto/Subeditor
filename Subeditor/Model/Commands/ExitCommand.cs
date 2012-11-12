using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Models.Commands;

namespace Subeditor.Model.Commands
{
    class ExitCommand : ICommand
    {
        private ApplicationManager appManager;

        public ExitCommand(ApplicationManager appManager)
        {
            this.appManager = appManager;
        }

        #region ICommand

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            appManager.StopApplication();
        }

        #endregion //ICommand
    }
}
