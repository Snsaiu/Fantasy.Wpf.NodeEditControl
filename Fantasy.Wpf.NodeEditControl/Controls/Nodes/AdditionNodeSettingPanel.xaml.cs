using Fantasy.Wpf.NodeEditControl.Controls.Bases;

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
    /// AdditionNodeSettingPanel.xaml 的交互逻辑
    /// </summary>
    public partial class AdditionNodeSettingPanel : SettingPanelBase
    {
        public AdditionNodeSettingPanel()
        {
            InitializeComponent();
            this.inputTxt.TextChanged += (s, e) =>
            {
                this.UpdateEvent?.Invoke(this.inputTxt.Text);

            };
        }

        public override event UpdateDelegate UpdateEvent;
    }
}
