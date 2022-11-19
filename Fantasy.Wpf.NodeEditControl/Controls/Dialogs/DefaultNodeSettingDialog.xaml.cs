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

namespace Fantasy.Wpf.NodeEditControl.Controls.Dialogs
{
    /// <summary>
    /// DefaultNodeSettingDialog.xaml 的交互逻辑
    /// </summary>
    public partial class DefaultNodeSettingDialog : SettingDialogBase
    {
        public DefaultNodeSettingDialog()
        {
            InitializeComponent();
            var p= Mouse.GetPosition(null);
            this.Left = p.X;
            this.Top = p.Y;
        }

     
        public override void SetSettingBaseInfo(string nodeName, ImageSource logo)
        {
            this.Title = nodeName;
            this.Icon = logo;
        }

        public override void SetContent(SettingPanelBase content)
        {
            this.Content = content;
        }
        
    }
}
