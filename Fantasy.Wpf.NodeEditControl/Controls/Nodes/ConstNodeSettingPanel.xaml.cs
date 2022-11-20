using System.Windows.Controls;
using Fantasy.Wpf.NodeEditControl.Controls.Bases;

namespace Fantasy.Wpf.NodeEditControl.Controls.Nodes;

public partial class ConstNodeSettingPanel : SettingPanelBase
{
    public ConstNodeSettingPanel()
    {
        InitializeComponent();
        this.input.TextChanged += (s, e) =>
        {
            this.UpdateEvent?.Invoke(this.input.Text);

        };
    }

    public override event UpdateDelegate UpdateEvent;
}