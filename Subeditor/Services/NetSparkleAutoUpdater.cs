using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using AppLimit.NetSparkle;

namespace Subeditor.Services
{
    /// <summary>
    /// Klasa automatycznych aktualizacji wykorzystująca bibliotekę NetSparkle.
    /// </summary>
    class NetSparkleAutoUpdater : IAutoUpdater, IDisposable
    {
        private Sparkle autoUpdater;

        public NetSparkleAutoUpdater(String versionInfoURL)
        {
            this.autoUpdater = new Sparkle(versionInfoURL);
        }

        public NetSparkleAutoUpdater(String versionInfoURL, Icon appWindowIcon)
        {
            this.autoUpdater = new Sparkle(versionInfoURL);
            this.autoUpdater.ApplicationWindowIcon = appWindowIcon;
        }

        public NetSparkleAutoUpdater(String versionInfoURL, Icon appWindowIcon, Icon updaterIcon)
        {
            this.autoUpdater = new Sparkle(versionInfoURL);
            this.autoUpdater.ApplicationWindowIcon = appWindowIcon;
            this.autoUpdater.ApplicationIcon = updaterIcon.ToBitmap();
        }

        public void Update()
        {
            var autoUpdaterConfig = autoUpdater.GetApplicationConfig();
            NetSparkleAppCastItem latestVersion;
            if (autoUpdater.IsUpdateRequired(autoUpdaterConfig, out latestVersion))
            {
                autoUpdater.ShowUpdateNeededUI(latestVersion);
            }
        } 

        public void UpdateAsync()
        {
            autoUpdater.checkLoopFinished += (sender, args) =>
            {
                autoUpdater.StopLoop();
            };

            autoUpdater.StartLoop(true, true);
        }

        public void Dispose()
        {
            if (autoUpdater != null)
            {
                autoUpdater.Dispose();
            }
        }

    }
}
