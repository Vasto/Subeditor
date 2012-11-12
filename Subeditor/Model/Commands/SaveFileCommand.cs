using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KWinFramework.Models.Commands;

namespace Subeditor.Model.Commands
{
    class SaveFileCommand : ICommand
    {
        private SubtitlesManager subtitlesManager;

        public SaveFileCommand(SubtitlesManager subtitlesManager)
        {
            this.subtitlesManager = subtitlesManager;
        }

        public String Path { get; set; }

        #region ICommand

        public bool CanExecute()
        {
            return subtitlesManager.CurrentSubtitles.Path != null;
        }

        public void Execute()
        {
            if (Path != null)
            {
                subtitlesManager.CurrentSubtitles.Path = Path;
            }
            subtitlesManager.Save();
        }

        #endregion //ICommand
    }
}
