using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Models.Commands;

namespace Subeditor.Model.Commands
{
    class OpenFileCommand : ICommand
    {
        private SubtitlesManager subtitlesManager;

        public OpenFileCommand(SubtitlesManager subtitlesManager)
        {
            this.subtitlesManager = subtitlesManager;
        }

        public String Path { get; set; }

        #region ICommand

        public bool CanExecute()
        {
            return true;
        }

        public void Execute()
        {
            if (Path != null)
            {
                subtitlesManager.CurrentSubtitles.Path = Path;
            }
            subtitlesManager.Load();
        }

        #endregion //ICommand
    }
}
