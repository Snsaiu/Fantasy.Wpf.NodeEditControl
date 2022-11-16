using Fantasy.Wpf.NodeEditControl.Controls.Nodes;

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

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            //this.canvas.AddNode(new AdditionNode() { Position = new Point(200, 300),Canvas=this.canvas });
            //this.canvas.AddNode(new AdditionNode() { Position = new Point(300, 400),Canvas=this.canvas });
            //this.canvas.AddNode(new ConstNode() { Position = new Point(300, 400),Canvas=this.canvas });
            //this.canvas.AddNode(new ConstNode() { Position = new Point(300, 400),Canvas=this.canvas });
        }
    }
}
