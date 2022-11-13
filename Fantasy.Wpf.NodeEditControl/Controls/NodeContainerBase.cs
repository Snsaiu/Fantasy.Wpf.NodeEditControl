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

        public NodeContainerBase()
        {

        }

        public abstract void SetNodeName(string name);

        public abstract void IsCalculateNode(bool  isCalculateNode);

        public abstract void SetNodeSize(Size size);
        

        public abstract void SetContent(FrameworkElement content);
    }
}
