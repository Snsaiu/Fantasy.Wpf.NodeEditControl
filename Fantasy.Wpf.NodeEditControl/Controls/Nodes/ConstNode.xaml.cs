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

namespace Fantasy.Wpf.NodeEditControl.Controls.Nodes
{
    /// <summary>
    /// ConstNode.xaml 的交互逻辑
    /// </summary>
    public partial class ConstNode : NodeBase
    {
    
        public ConstNode()
        {

            InitializeComponent();
            this.inputTxt.Text = "1";
            this.inputTxt.TextChanged += (s, e) =>
            {
                this.NotifyCalculate();
            };

        }
        protected override OutputData CalculateImpl()
        {
            string content = this.inputTxt.Text;
            var od = new OutputData();
            if (int.TryParse(content,out int x))
            {
                od.DataType = typeof(int);
            }
            else if(double.TryParse(content,out double y))
            {
                od.DataType = typeof(double);
            }
            else
            {
                od.DataType = typeof(string);
            }
            od.Data = content;
            return od;
        }

        protected override string GetNodeName()
        {
            return "常量";
        }

        protected override Size GetNodeSize()
        {
           return new Size(200,100);
        }

        public override List<PortBase> GetPorts()
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

        public override string GetNodeSummary()
        {
            string content = "一个固定不变的值，该至可以是文本和数字";
            return content;
        }
    }
}
