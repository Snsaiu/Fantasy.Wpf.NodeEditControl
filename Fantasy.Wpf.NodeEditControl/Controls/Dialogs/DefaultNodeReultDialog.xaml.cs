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
using System.Windows.Shapes;
using Fantasy.Wpf.NodeEditControl.Controls.Bases;
using Fantasy.Wpf.NodeEditControl.Data;

namespace Fantasy.Wpf.NodeEditControl.Controls.Dialogs
{
    /// <summary>
    /// Interaction logic for DefaultNodeReultDialog.xaml
    /// </summary>
    public partial class DefaultNodeReultDialog : NodeResultDialogBase
    {
        public DefaultNodeReultDialog()
        {
            InitializeComponent();
            var p = Mouse.GetPosition(null);
            this.Left = p.X;
            this.Top = p.Y;
        }

        public override void SetNodeWindowBaseInfo(string nodeName, ImageSource logo)
        {
            this.Title = nodeName+" 计算结果";
            this.Icon=logo;
            
        }

        public override void SetShowControl(NodeResultPanelBase panel)
        {
            this.Content=panel;
        }
    }
}
