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

namespace TurretControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SerialCom com = new SerialCom("COM4", 9600);

            com.Start();

            for(int i = 0; i<=180; i+=5)
            {
                com.Write(new PriorityMessage($"m:{i},120",2));
            }

            this.Closed += (sender, e) => com.Stop();
        }

        
    }
}
