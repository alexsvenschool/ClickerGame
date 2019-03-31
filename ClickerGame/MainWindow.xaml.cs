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
        public static float score = 0;
        public static float scorePerSecond = 0;
        public static float scorePerClick = 1;
        PowerUp grandma, cafetria, bakery, autoclicker;       

        public MainWindow()
        {
            InitializeComponent();

            // Powerups initialisieren
            autoclicker = new PowerUp(lblAutoClickerCosts, lblBakeryCosts, 20, 1f);
            btnAutoClick.Tag = autoclicker;

            grandma = new PowerUp(lblGrandmaCosts, lblCafeteriaCosts, 5000, 5);
            btnGrandma.Tag = grandma;

            // UI Update
            DispatcherTimer fps = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000)
            };
            fps.Tick += UpdateFrame;              
            fps.Start();
        }

        private void UpdateFrame(object sender, EventArgs e)
        {
            //Score Update
            score += scorePerSecond;
            lblPretzel.Content = "pretzel: " + (int)score;
            lblAutoPretzel.Content = "pretzel per second: " + scorePerSecond;           
            // Update Powerups
            grandma.UpdateFrame();
            autoclicker.UpdateFrame();
        }

        private void Upgrade_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            PowerUp pu = (PowerUp)button.Tag;
            pu.OnClick();
        }

        private void BtnClickField_Click(object sender, RoutedEventArgs e)
        {
            score += scorePerClick;
        }
    }
}
