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
        public static float scorePerSecond = 3000;
        public static float scorePerClick = 1;
        private DateTime millis = DateTime.Now;
        PowerUp grandma, cafetria, bakery, autoclicker, factory;       

        public MainWindow()
        {
            InitializeComponent();

            // Powerups initialisieren
            autoclicker = new PowerUp(btnAutoClick, lblAutoClickerCosts, lblAutoClickerCount, 15, 0.1f);
            grandma = new PowerUp(btnGrandma, lblGrandmaCosts, lblGrandmaCount, 500, 5);
            cafetria = new PowerUp(btnCafeteria, lblCafeteriaCosts, lblCafeteriaCount, 10000, 50);
            bakery = new PowerUp(btnBakery, lblBakeryCosts, lblBakeryCount, 30000, 500);
            factory = new PowerUp(btnFactory, lblFactoryCosts, lblFactoryCount, 8000000, 5000);

            // UI Update
            DispatcherTimer fps = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(17)
            };
            fps.Tick += UpdateFrame;              
            fps.Start();
        }

        private void UpdateFrame(object sender, EventArgs e)
        {
            //Score Update            
            score += scorePerSecond * ((float)(DateTime.Now - millis).TotalMilliseconds / 1000);
            millis = DateTime.Now;
            lblPretzel.Content = "pretzel: " + (int)score;
            lblAutoPretzel.Content = "pretzel per second: " + scorePerSecond;      
            
            // Update Powerups
            grandma.UpdateFrame();
            cafetria.UpdateFrame();
            bakery.UpdateFrame();
            autoclicker.UpdateFrame();
            factory.UpdateFrame();            
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
