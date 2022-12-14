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


        }

        protected override void UpdateNodeDisplayData(object data)
        {
            if (data != null)
                this.inputTxt.Text = data.ToString();
        }

        protected override OutputData CalculateImpl(object data)
        {
            var od = new OutputData();
            if (data!=null)
            {
                string content = data.ToString();

        
        
                if (int.TryParse(content, out int x))
                {
                    od.DataType = typeof(int);
                }
                else if (double.TryParse(content, out double y))
                {
                    od.DataType = typeof(double);
                }
                else
                {
                    od.DataType = typeof(string);
                }
                od.Data = content;
              
            }
            else
            {
           
                od.DataType = typeof(string);
                od.Data = "";
            }
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

        public override NodeResultPanelBase SetNodeResultPanel()
        {
            return new ConstNodeResultPanel();
        }

        public override SettingPanelBase SetSettingContent()
        {
            return new ConstNodeSettingPanel();
        }

        public override string GetNodeSummary()
        {
            string content = "一个固定不变的值，可以是文本和数字";
            return content;
        }
    }
}
