using System.Windows.Controls;
using Fantasy.Wpf.NodeEditControl.Controls.Bases;
using Fantasy.Wpf.NodeEditControl.Data;

namespace Fantasy.Wpf.NodeEditControl.Controls.Nodes;

public partial class ConstNodeResultPanel : NodeResultPanelBase
{
    public ConstNodeResultPanel()
    {
        InitializeComponent();
    }

    public override void UpdateData(OutputData data)
    {
        if (data != null)
        {
            if (data.Data != null)
                this.text.Text = data.Data.ToString();
        }
    }
}