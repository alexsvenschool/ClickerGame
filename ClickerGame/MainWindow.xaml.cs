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
using System.Windows.Threading;

namespace ClickerGame
{    
    public partial class MainWindow : Window
    {
        public float score = 0;
        public float scorePerSecond = 0;

        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer fps = new DispatcherTimer();
            fps.Interval = TimeSpan.FromSeconds(1 / Properties.Settings.Default.Hertz);
            fps.Tick += UpdateFrame;
            fps.Start();
        }

        private void UpdateFrame(object sender, EventArgs e)
        {            
        }
    }
}
