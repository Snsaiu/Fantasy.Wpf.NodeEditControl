

using Fantasy.Wpf.NodeEditControl.Controls.Bases;
using Fantasy.Wpf.NodeEditControl.Data;

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

namespace Fantasy.Wpf.NodeEditControl.Controls
{

    /// <summary>
    /// Node.xaml 的交互逻辑
    /// </summary>
    public partial class Node : NodeBase
    {
        public Node()
        {
            InitializeComponent();
          
        }

        protected override string GetNodeName()
        {
            return "TEST";
        }

        protected override Size GetNodeSize()
        {
            return  new Size(400, 300);
        }

        public override List<PortBase> GetPorts()
        {
            List<PortBase> res = new List<PortBase>() { 
            this.input1,
            this.input2,
            this.output
            };
            return res;
        }

        public override List<Type> SupportInputTypes()
        {
            throw new NotImplementedException();
        }

        public override List<Type> SupportOutputTypes()
        {
            throw new NotImplementedException();
        }

        public override OutputData Calculate()
        {
            throw new NotImplementedException();
        }
    }
}
