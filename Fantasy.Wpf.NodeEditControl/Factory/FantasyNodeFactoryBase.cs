using Fantasy.Wpf.NodeEditControl.Controls;
using Fantasy.Wpf.NodeEditControl.Controls.Bases;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Wpf.NodeEditControl.Factory
{
    public abstract class FantasyNodeFactoryBase
    {
        public virtual LineBase SetLineStyle() {
            return new ArrowLine();
        }

       
    }
}
