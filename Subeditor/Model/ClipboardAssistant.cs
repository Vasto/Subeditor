using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Subeditor.Model
{
    /// <summary>
    /// 
    /// </summary>
    class ClipboardAssistant
    {
        public event EventHandler<EventArgs<String>> ClipboardChanged;

        public void OnClipboardChanged(object sender, EventArgs<IDataObject> e)
        {
            var temporaryEventHolder = ClipboardChanged;
            if (temporaryEventHolder != null)
            {
                String data = (String)e.Value.GetData(typeof(String));

                temporaryEventHolder(this, new EventArgs<String>(data));
            }
        }
    }
}
