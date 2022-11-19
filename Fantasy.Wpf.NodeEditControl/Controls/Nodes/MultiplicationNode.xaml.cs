using Fantasy.Wpf.NodeEditControl.Controls.Bases;
using Fantasy.Wpf.NodeEditControl.Data;
using Fantasy.Wpf.NodeEditControl.Helpers;

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
    /// MultiplicationNode.xaml 的交互逻辑
    /// </summary>
    public partial class MultiplicationNode:NodeBase
    {
        public MultiplicationNode()
        {
            InitializeComponent();
        }
        protected override string GetNodeName()
        {
            return "乘法";
        }

        protected override Size GetNodeSize()
        {
            return new Size(200, 100);
        }

        protected override OutputData CalculateImpl(object data)
        {
            OutputData d = new OutputData();
            if (this.input1.ConnectedLines.Count == 0 || this.input2.ConnectedLines.Count == 0)
            {
                Tools.ShowWarning("警告", "输入端口未连接");
                return d;
            }

            var p1 = this.input1.ConnectedLines[0].HeaderNode.Calculate();
            var p2 = this.input2.ConnectedLines[0].HeaderNode.Calculate();

            if (p1.DataType == p2.DataType)
            {
                if (p1.DataType == typeof(double))
                {
                    d.Data = double.Parse(p1.Data.ToString()) * double.Parse(p2.Data.ToString());
                    d.DataType = typeof(double);
                }
                else if (p1.DataType == typeof(int))
                {

                    d.Data = int.Parse(p1.Data.ToString()) * int.Parse(p2.Data.ToString());
                    d.DataType = typeof(int);
                }

            }
            else
            {
                Tools.ShowWarning("警告", "类型不匹配，无法计算!");

            }
            this.richTxt.Text = d.Data.ToString();
            return d;
        }

        public override List<PortBase> GetPorts()
        {
            return new List<PortBase>()
            {
                this.input1,
                this.input2,
                this.output
            };
        }

        public override List<Type> SupportInputTypes()
        {
            return new List<Type> { typeof(int), typeof(double), typeof(float), typeof(decimal) };
        }

        public override List<Type> SupportOutputTypes()
        {
            return new List<Type> { typeof(int), typeof(double), typeof(float), typeof(decimal) };

        }

        public override SettingPanelBase SetSettingContent()
        {
            throw new NotImplementedException();
        }

        public override string GetNodeSummary()
        {
            string content = "两个数相乘";
            return content;
        }
    }
}
