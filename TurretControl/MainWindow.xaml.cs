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
using System.Diagnostics;
using System.Runtime.Remoting.Channels;
using TurretLib;

namespace TurretControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Turret turret;
        private Stopwatch watch;
        public MainWindow()
        {
            InitializeComponent();

            turret = new Turret("COM4", 115200);
            turret.Activate();

            for (int i = 0; i <= 180; i += 2) {
                {
                    turret.Move(i, 120); turret.Move(i, 120, 0);
                }
            }

            ControlArea.MouseMove += RecieveMouseMove;

            this.Closed += (sender, e) => turret.Deactivate();
            watch = Stopwatch.StartNew();

            xBox.PreviewTextInput += validateCoordInput;
            xBox.PreviewKeyDown += filterSpace;

            yBox.PreviewTextInput += validateCoordInput;
            yBox.PreviewKeyDown += filterSpace;
            MoveBtn.Click += (sender, args) => MoveToCoordinates();

            this.KeyUp += HandleKeyUp;
        }

        public void HandleKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    MoveToCoordinates();
                    break;
                case Key.Escape:
                    if (TrackpadCheck.IsChecked == true) TrackpadCheck.IsChecked = false;
                    else TrackpadCheck.IsChecked = true;
                    break;
            }
        }

        public void MoveToCoordinates()
        {
            if (xBox.Text != "" && yBox.Text != "") turret.Move(int.Parse(xBox.Text), int.Parse(yBox.Text));
        }

        public void RecieveMouseMove(object sender, MouseEventArgs e)
        {
            if (watch.ElapsedMilliseconds > 100 && TrackpadCheck.IsChecked == true)
            {
                Point cursor = e.GetPosition(ControlArea);
                int x = (int)(180 * (cursor.X / ControlArea.Width));
                int y = 180 - (int)(180 * (cursor.Y / ControlArea.Height));

                xBox.Text = x.ToString();
                yBox.Text = y.ToString();
                turret.Move(x,y);
                watch = Stopwatch.StartNew();
            }
        }

        private void validateCoordInput(object sender, TextCompositionEventArgs e)
        {
            TextBox selectedBox = sender as TextBox;
            if (selectedBox.SelectedText == selectedBox.Text) selectedBox.Text = ""; ;
            e.Handled = !isValidCoords((selectedBox.Text + e.Text));
        }


        private void filterSpace(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) {
                e.Handled = true;
            }
        }

        private static bool isValidCoords(string str)
        {
            int input;
            return int.TryParse(str, out input) && input >= 0 && input <= 180;
        }

    }
}
