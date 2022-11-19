using Fantasy.Wpf.NodeEditControl.Controls;
using Fantasy.Wpf.NodeEditControl.Controls.Bases;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fantasy.Wpf.NodeEditControl.Controls.Dialogs;

namespace Fantasy.Wpf.NodeEditControl.Factory
{
    public abstract class FantasyNodeFactoryBase
    {
        public virtual LineBase SetLineStyle() {
            return new ArrowLine();
        }

        public virtual NodeContainerBase SetNodeContainerStyle()
        {
            return new NodeContainer();
        }

        public virtual NodeInfoDialogBase SetNodeInfoDialogStyle()
        {
            return new DefaultNodeInfoDialog();
        }
       
    }
}
