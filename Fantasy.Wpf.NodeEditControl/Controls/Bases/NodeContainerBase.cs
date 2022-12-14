using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Fantasy.Wpf.NodeEditControl.Controls.Bases
{
    /// <summary>
    ///  start calculate delegate
    /// </summary>
    public delegate void CalculateDelegate();

    /// <summary>
    /// display node summary document
    /// </summary>
    public delegate void ShowSummaryDelegate();

    /// <summary>
    ///  set current node can calculate delegate,if true and no calculate record ,the node will calculate once .otherwise will read cache
    /// </summary>
    /// <param name="state"></param>
    public delegate void SetFreezeCalculateStateDelegate(bool state);

    /// <summary>
    /// open setting panel delegate
    /// </summary>
    public delegate void ShowSettingPanelDelegate();

    public delegate void ShowCalculateResultDelegate();

    public abstract class NodeContainerBase : UserControl
    {

        public abstract event CalculateDelegate CalculateEvent;


        public abstract event ShowSummaryDelegate ShowSummaryEvent;

        /// <summary>
        /// set freeze calculate state ,if set true ,the node state will be freeze otherwise not
        /// </summary>
        public abstract event SetFreezeCalculateStateDelegate SetFreezeCalculateStateEvent;

        public abstract event ShowSettingPanelDelegate SetSettingPanelEvent;

        public abstract event ShowCalculateResultDelegate ShowCalculateResultEvent;

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
