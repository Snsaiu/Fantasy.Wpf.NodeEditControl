using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Fantasy.Wpf.NodeEditControl.Data
{
    /// <summary>
    ///  regist node model
    /// </summary>
    public class RegistNodeItem
    {
        public Type NodeType { get; init; }

        public string NodeName { get; init; }

        public string GroupName { get; init; }

        public ImageSource NodeLogo { get; init; }
    }
}
