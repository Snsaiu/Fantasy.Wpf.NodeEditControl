using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Fantasy.Wpf.NodeEditControl.Controls.Bases
{

    public delegate void CalculateDelegate();

    /// <summary>
    /// display node summary document
    /// </summary>
    public delegate void ShowSummaryDelegate();

    public abstract class NodeContainerBase : UserControl
    {

        public abstract event CalculateDelegate CalculateEvent;


        public abstract event ShowSummaryDelegate ShowSummaryEvent;

        public NodeContainerBase()
        {

        }

        /// <summary>
        /// default node style ,when mouse not touch the node will show this style
        /// </summary>
        public virtual void DefaultStyle()
        {

        }

        /// <summary>
        /// when mouse touched this node ,the node will show this style
        /// </summary>
        public virtual void SelectedStyle()
        {

        }


        public abstract void SetNodeName(string name);

        public abstract void IsCalculateNode(bool isCalculateNode);

        public abstract void SetNodeSize(Size size);


        public abstract void SetContent(FrameworkElement content);
    }
}
