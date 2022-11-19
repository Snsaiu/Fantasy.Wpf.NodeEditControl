using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Fantasy.Wpf.NodeEditControl.Controls.Bases
{
    public abstract class NodeInfoDialogBase:Window
    {
        public NodeInfoDialogBase()
        {
          
        }

        public abstract void InitShow(string NodeName, ImageSource NodeLogo, string Summary);
    
    }
}
