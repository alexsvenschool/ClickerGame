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
        public static float score = Properties.Settings.Default.Score;
        public static float scorePerSecond = Properties.Settings.Default.SPS;
        public static float scorePerClick = Properties.Settings.Default.SPC;
        private DateTime millis = DateTime.Now;
        PowerUp grandma, cafetria, bakery, autoclicker, factory;
        SpecialPowerUps easteregg;
        

        public MainWindow()
        {
            InitializeComponent();

            // Powerups initialisieren
            autoclicker = new PowerUp(btnAutoClick, lblAutoClickerCosts, lblAutoClickerCount, 15, 2000);
            grandma     = new PowerUp(btnGrandma, lblGrandmaCosts, lblGrandmaCount, 500, 50);
            cafetria    = new PowerUp(btnCafeteria, lblCafeteriaCosts, lblCafeteriaCount, 10000, 200);
            bakery      = new PowerUp(btnBakery, lblBakeryCosts, lblBakeryCount, 30000, 1000);
            factory     = new PowerUp(btnFactory, lblFactoryCosts, lblFactoryCount, 8000000, 10000);
            
            // SpecialPowerUps initialisieren
            easteregg   = new SpecialPowerUps(btnEasterEgg, lblEasteregg,5000);

            // Werte aus den Properties holen
            autoclicker.count   = Properties.Settings.Default.AutoCount;
            grandma.count       = Properties.Settings.Default.GrandmaCount;
            cafetria.count      = Properties.Settings.Default.CafeteriaCount;
            bakery.count        = Properties.Settings.Default.BakeryCount;
            factory.count       = Properties.Settings.Default.FactoryCount;
            easteregg.baseCost  = Properties.Settings.Default.EastereggBaseCost;

            // UI Update
            DispatcherTimer fps = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(17)
            };
            fps.Tick += UpdateFrame;              
            fps.Start();

            DispatcherTimer saveGameTicker = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            saveGameTicker.Tick += SaveGame;
            saveGameTicker.Start();
        }

        private void UpdateFrame(object sender, EventArgs e)
        {
            //Score Update            
            score += scorePerSecond * ((float)(DateTime.Now - millis).TotalMilliseconds / 1000);
            millis = DateTime.Now;

            lblPretzel.Content          = "pretzel: " + (int)score;
            lblAutoPretzel.Content      = "per second: " + scorePerSecond.ToString("0.00");
            lblPretzelPerClick.Content  = "per click: " + scorePerClick.ToString("0.00");
            
            // Update Powerups
            grandma.UpdateFrame();
            cafetria.UpdateFrame();
            bakery.UpdateFrame();
            autoclicker.UpdateFrame();
            factory.UpdateFrame();

            // Update SpecialPowerUps
            easteregg.UpdateFrame();

            //Autoclicker Upgrade einblenden
            if(!autoclicker.purchasedUpgrade && autoclicker.upgradeAvailable)
            {
                btnAutoClickerUpgrade.Visibility = Visibility.Visible;
            }
            else
            {
                btnAutoClickerUpgrade.Visibility = Visibility.Hidden;
            }
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

        private void BtnEasterEgg_Click(object sender, RoutedEventArgs e)
        {
            easteregg.EasterEgg();
        }

        private void BtnAutoClickerUpgrade_Click(object sender, RoutedEventArgs e)
        {
            autoclicker.purchasedUpgrade = true;
            autoclicker.upgradeAvailable = false;
            scorePerClick +=  (float)Math.Pow(autoclicker.count, 1.08);
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.R && Keyboard.Modifiers == ModifierKeys.Control)
            {
                ResetGame();
            }
        }

        private void ResetGame()
        {
            Properties.Settings.Default.Reset();
            Properties.Settings.Default.Save();
            score               = Properties.Settings.Default.Score;
            scorePerSecond      = Properties.Settings.Default.SPS;
            scorePerClick       = Properties.Settings.Default.SPC;
            autoclicker.count   = Properties.Settings.Default.AutoCount;
            grandma.count       = Properties.Settings.Default.GrandmaCount;
            cafetria.count      = Properties.Settings.Default.CafeteriaCount;
            bakery.count        = Properties.Settings.Default.BakeryCount;
            factory.count       = Properties.Settings.Default.FactoryCount;
            easteregg.baseCost  = Properties.Settings.Default.EastereggBaseCost;
        }

        private void SaveGame(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoCount           = autoclicker.count;
            Properties.Settings.Default.GrandmaCount        = grandma.count;
            Properties.Settings.Default.CafeteriaCount      = cafetria.count;
            Properties.Settings.Default.BakeryCount         = bakery.count;
            Properties.Settings.Default.FactoryCount        = factory.count;
            Properties.Settings.Default.Score               = score;
            Properties.Settings.Default.SPS                 = scorePerSecond;
            Properties.Settings.Default.SPC                 = scorePerClick;
            Properties.Settings.Default.EastereggBaseCost   = easteregg.NewCost();
        }
    }
}
