using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Fantasy.Wpf.NodeEditControl.Controls
{




    /// <summary>
    /// NodeContainer.xaml 的交互逻辑
    /// </summary>
    public partial class NodeContainer : NodeContainerBase
    {


        public NodeContainer()
        {
            InitializeComponent();
          
           
            this.refreshBtn.Click += (s, e) =>
            {
                this.CalculateEvent?.Invoke();
            };
          
        }

        public override event CalculateDelegate CalculateEvent;

        public override void IsCalculateNode(bool isCalculateNode)
        {
            this.refreshBtn.Visibility = isCalculateNode ? Visibility.Visible : Visibility.Collapsed;
        
        }

        public override void SetContent(FrameworkElement content)
        {
            this.container.Content=content;
        }

        public override void SetNodeName(string name)
        {
            this.title.Text = name;
        }

        public override void SetNodeSize(Size size)
        {
            this.rg.Rect = new Rect(size);
        }
    }
}
