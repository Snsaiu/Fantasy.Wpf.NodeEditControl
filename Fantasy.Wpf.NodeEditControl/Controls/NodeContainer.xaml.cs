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
using System.Xml.Linq;
using Fantasy.Wpf.NodeEditControl.Controls.Bases;

namespace Fantasy.Wpf.NodeEditControl.Controls
{




    /// <summary>
    /// NodeContainer.xaml 的交互逻辑
    /// </summary>
    public partial class NodeContainer : NodeContainerBase
    {


        public NodeContainer()
        {
            InitializeComponent();

            this.resultBtn.Click += (s, e) =>
            {

                this.ShowCalculateResultEvent?.Invoke();
            };
           
            this.refreshBtn.Click += (s, e) =>
            {
                this.CalculateEvent?.Invoke();
            };
            this.infoBtn.Click += (s, e) =>
            {
                this.ShowSummaryEvent?.Invoke();
            };

            this.freezeCbox.Checked += (s, e) =>
            {
                this.SetFreezeCalculateStateEvent?.Invoke(true);
            };
            this.freezeCbox.Unchecked += (s, e) =>
            {
                this.SetFreezeCalculateStateEvent?.Invoke(false);
            };

            this.settingBtn.Click += (s, e) =>
            {
                this.SetSettingPanelEvent?.Invoke();


            };

        }

        public override event CalculateDelegate CalculateEvent;
        public override event ShowSummaryDelegate ShowSummaryEvent;
        public override event SetFreezeCalculateStateDelegate SetFreezeCalculateStateEvent;
        public override event ShowSettingPanelDelegate SetSettingPanelEvent;
        public override event ShowCalculateResultDelegate ShowCalculateResultEvent;

        public override void IsCalculateNode(bool isCalculateNode)
        {
            this.refreshBtn.Visibility = isCalculateNode ? Visibility.Visible : Visibility.Collapsed;
        
        }

        public override void DefaultStyle()
        {
            this.border.BorderBrush=new SolidColorBrush(Colors.BlanchedAlmond);
        }

        public override void SelectedStyle()
        {
            this.border.BorderBrush = new SolidColorBrush(Colors.OrangeRed);
        }

        public override void SetContent(FrameworkElement content)
        {
            this.container.Content=content;
        }

        public override void SetNodeName(string name)
        {
            this.title.Text = name;
        }

        public override void SetNodeSize(Size size)
        {
            this.rg.Rect = new Rect(size);
        }
    }
}
