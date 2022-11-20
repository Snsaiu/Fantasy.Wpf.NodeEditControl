using Fantasy.Wpf.NodeEditControl.Data;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Fantasy.Wpf.NodeEditControl.Controls.Bases
{
    public abstract class NodeResultDialogBase:Window
    {
        protected override void OnClosing(CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        /// <summary>
        /// set node window title and icon
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="logo"></param>
        public abstract void SetNodeWindowBaseInfo(string nodeName, ImageSource logo);


        /// <summary>
        /// set current result window content
        /// </summary>
        /// <param name="panel"></param>
        public abstract void SetShowControl(NodeResultPanelBase panel);

    }
}
