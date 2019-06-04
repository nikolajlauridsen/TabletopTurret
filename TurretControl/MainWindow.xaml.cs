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
using PrioritySerialCom;
using TurretLib;

namespace TurretControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Turret turret;
        public MainWindow()
        {
            InitializeComponent();

            turret = new Turret("COM4", 9600);
            turret.Activate();

            for(int i = 0; i<=180; i+=5)
            {
                turret.Move(i, 120);
            }


            this.Closed += (sender, e) => turret.Deactivate();
        }

        
    }
}
