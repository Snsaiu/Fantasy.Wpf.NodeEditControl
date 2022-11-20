using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
        
        public abstract void InitShow(string NodeName, ImageSource NodeLogo, string Summary);
    
    }
}
