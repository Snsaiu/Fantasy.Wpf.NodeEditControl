using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Fantasy.Wpf.NodeEditControl.Data;

namespace Fantasy.Wpf.NodeEditControl.Controls.Bases
{
    public abstract class NodeResultPanelBase:UserControl
    {
        public abstract void UpdateData(OutputData data);
    }
    

}
