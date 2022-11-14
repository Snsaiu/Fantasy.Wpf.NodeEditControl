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
    /// OutputPort.xaml 的交互逻辑
    /// </summary>
    public partial class OutputPort : PortBase
    {
        public OutputPort()
        {
            InitializeComponent();
            this.output.MouseDown += (s, e) =>
            {
                this.CreateLine();
            };
        }

        protected override FrameworkElement GetPortMark()
        {
            return this.output;
        }
    }
}
