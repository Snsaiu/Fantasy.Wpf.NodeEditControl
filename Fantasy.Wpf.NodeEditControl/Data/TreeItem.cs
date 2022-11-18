using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Fantasy.Wpf.NodeEditControl.Data
{
    public class TreeItem
    {
        public string GroupName { get; init; }

        public List<NodeDisplayItem> Nodes { get; init; }=new List<NodeDisplayItem>();
    }

    public class NodeDisplayItem
    {
        public string GroupName { get; init; }

        public ImageSource Logo { get; init; }
        public string NodeName { get; init; }
    }
}
