using Fantasy.Wpf.NodeEditControl.Controls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.Wpf.NodeEditControl.NotifyModel
{

    /// <summary>
    /// single class  when port position changed ,it will call all conneced lines to update position
    /// </summary>
    public sealed class PortPositionChangedNotificator
    {
        private static PortPositionChangedNotificator instance;

        public static PortPositionChangedNotificator GetNotificator()
        {
            if (instance == null)
                instance = new PortPositionChangedNotificator();
            return instance;
        }

        private PortPositionChangedNotificator()
        {

        }

        private List<LineBase> Lines = new List<LineBase>();

        public void Regist(LineBase line)
        {
            if(!Lines.Contains(line))
                Lines.Add(line);
        }

        public void UnRegist(LineBase line) {
            Lines.Remove(line);
        }

        public void Publish(PortBase port)
        {

        }
    }
}
