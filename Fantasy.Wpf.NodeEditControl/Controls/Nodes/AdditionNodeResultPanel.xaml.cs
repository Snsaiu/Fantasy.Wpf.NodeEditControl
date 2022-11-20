using System.Windows.Controls;
using Fantasy.Wpf.NodeEditControl.Controls.Bases;
using Fantasy.Wpf.NodeEditControl.Data;

namespace Fantasy.Wpf.NodeEditControl.Controls.Nodes;

public partial class AdditionNodeResultPanel : NodeResultPanelBase
{
    public AdditionNodeResultPanel()
    {
        InitializeComponent();
    }

    public override void UpdateData(OutputData data)
    {
        if (data != null)
        {
            if (data.Data != null)
                this.resultTb.Text = data.Data.ToString();
        }
    }
}