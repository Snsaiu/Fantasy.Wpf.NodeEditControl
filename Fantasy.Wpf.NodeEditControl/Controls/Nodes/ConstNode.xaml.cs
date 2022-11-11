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

namespace Fantasy.Wpf.NodeEditControl.Controls.Nodes
{
    /// <summary>
    /// ConstNode.xaml 的交互逻辑
    /// </summary>
    public partial class ConstNode : NodeBase
    {
        private int index = 1;
        public ConstNode()
        {

            InitializeComponent();
            this.inputTxt.Text = "1";
            this.MouseDoubleClick += (s, e) =>
            {

                this.index++;
                this.inputTxt.Text = this.index.ToString();

            };
        }
        public override OutputData Calculate()
        {
            return new OutputData { Data=this.index,DataType=typeof(int)};
        }

        protected override string GetNodeName()
        {
            return "常量";
        }

        protected override Size GetNodeSize()
        {
           return new Size(300,200);
        }

        protected override List<PortBase> GetPorts()
        {
            return new List<PortBase> { this.output };

        }

        public override List<Type> SupportInputTypes()
        {
            return new List<Type> { typeof(string), typeof(int), typeof(double), typeof(float), typeof(decimal) };
        }

        public override List<Type> SupportOutputTypes()
        {
            return new List<Type> { typeof(string), typeof(int), typeof(double), typeof(float), typeof(decimal) };
        }
    }
}
