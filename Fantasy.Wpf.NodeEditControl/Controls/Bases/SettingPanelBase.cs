using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Fantasy.Wpf.NodeEditControl.Controls.Bases
{
    /// <summary>
    ///  update data can make node start calculate
    /// </summary>
    public delegate void UpdateDelegate(object data);

    public abstract class SettingPanelBase:UserControl
    {
        public abstract event UpdateDelegate UpdateEvent;
    }
}
