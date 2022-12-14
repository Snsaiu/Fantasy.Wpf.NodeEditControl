using Fantasy.Wpf.NodeEditControl.Controls.Bases;
using Fantasy.Wpf.NodeEditControl.Data;
using Fantasy.Wpf.NodeEditControl.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
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
    /// AdditionNode.xaml 的交互逻辑
    /// </summary>
    public partial class AdditionNode : NodeBase
    {
        public AdditionNode()
        {
            InitializeComponent();
        }

        public override string GetNodeSummary()
        {
            string content = "两个值相加，如果输入节点是string类型，那么将会把所有的输入节点全部转换为string，最终结果是字符串" +
                             "的拼接";
            return content;
        }

        protected override string GetNodeName()
        {
            return "加法";
        }

        protected override Size GetNodeSize()
        {
            return new Size(250, 100);
        }


        protected override void UpdateNodeDisplayData(object data)
        {
           if(data!=null)
                this.inputtxt.Text = data.ToString();
        }
        protected override OutputData CalculateImpl(object data)
        {
            

            OutputData d = new OutputData();
           if (this.input1.ConnectedLines.Count==0||this.input2.ConnectedLines.Count==0)
            {
                Tools.ShowWarning("警告", "输入端口未连接");
                return d;
            }

          var p1=  this.input1.ConnectedLines[0].HeaderNode.Calculate();
           var p2= this.input2.ConnectedLines[0].HeaderNode.Calculate();

           if (p1.Data == null || p2.Data == null)
           {
               Tools.ShowWarning("警告", "输入端口未连接");
               return d;
            }

           if (data == null || data == "")
           {
               if (p1.DataType == p2.DataType)
               {
                   if (p1.DataType == typeof(string))
                   {
                       d.Data = p1.Data.ToString() + p2.Data.ToString();
                       d.DataType = typeof(string);
                   }
                   else if (p1.DataType == typeof(int))
                   {

                       d.Data = int.Parse(p1.Data.ToString()) + int.Parse(p2.Data.ToString());
                       d.DataType = typeof(int);
                   }
                   else if (p1.DataType == typeof(double))
                   {
                       d.Data = double.Parse(p1.Data.ToString()) + double.Parse(p2.Data.ToString());
                       d.DataType = typeof(double);
                   }
               }
               else
               {
                   d.Data = p1.Data.ToString() + p2.Data.ToString();
                   d.DataType = typeof(string);

               }

           }
           else
           {
               string dataStr = data.ToString();
               if (int.TryParse(dataStr, out int x)||double.TryParse(dataStr,out double y))
               {
                 if(
                     (p1.DataType==typeof(int)||p1.DataType==typeof(double))
                         && (p2.DataType==typeof(int)||p2.DataType==typeof(double))
                         )
                 {
                     d.Data = double.Parse(dataStr) + double.Parse(p1.Data.ToString()) + double.Parse(p2.Data.ToString());

                 }
                 else
                 {
                     d.Data = dataStr.ToString() + p1.Data.ToString() + p2.Data.ToString();
                 }
                   
               }
               else
               {
                   d.Data =  p1.Data.ToString() + p2.Data.ToString()+dataStr.ToString() ;
               }
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
           return new List<Type> { typeof(string) ,typeof(int),typeof(double),typeof(float),typeof(decimal)};
        }

        public override List<Type> SupportOutputTypes()
        {
            return new List<Type> { typeof(string), typeof(int), typeof(double), typeof(float), typeof(decimal) };
        }

        public override NodeResultPanelBase SetNodeResultPanel()
        {
            return new AdditionNodeResultPanel();
        }

        public override SettingPanelBase SetSettingContent()
        {
            return new AdditionNodeSettingPanel();
        }
    }
}
