using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Fantasy.Wpf.NodeEditControl.Controls
{

    public delegate void CalculateDelegate();
    public abstract class NodeContainerBase:UserControl
    {

        public abstract event CalculateDelegate CalculateEvent;

        public NodeContainerBase(Size size, bool isCalculateNode, string name)
        {

        }

        public abstract void SetContent(FrameworkElement content);
    }
}
