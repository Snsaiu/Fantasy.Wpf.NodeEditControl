using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Fantasy.Wpf.NodeEditControl.Controls.Bases
{


    public abstract class SettingDialogBase:Window
    {

      

        public abstract void SetSettingBaseInfo(string nodeName, ImageSource logo);
        /// <summary>
        /// set setting content
        /// </summary>
        /// <param name="content"></param>
        public abstract void SetContent(SettingPanelBase content);


    }
}
