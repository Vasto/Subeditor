using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KWinFramework.Views.WinForms;

namespace Subeditor.Views.Toolbar
{
    /// <summary>
    /// Widok paska narzędziowego.
    /// </summary>
    public partial class ToolbarView : UserControlView, IToolbarView
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        public ToolbarView()
        {
            InitializeComponent();
        }
    }
}
