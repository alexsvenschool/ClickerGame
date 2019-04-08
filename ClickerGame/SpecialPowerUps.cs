using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClickerGame
{
    class SpecialPowerUps
    {
        private readonly Button button;
        private readonly Label lblCost;
        public int baseCost;
        public int count = 0;

        // Konstruktor
        public SpecialPowerUps(Button btn, Label cost, int bCost)
        {
            lblCost     = cost;
            button      = btn;
            baseCost    = bCost;
            btn.Tag     = this;
        }

        public void EasterEgg()
        {
            if (MainWindow.score >= NewCost())
            {
                int zufallszahl = generate_random_number();
                count++;
                switch (zufallszahl)
                {
                    case 1:
                        MainWindow.scorePerSecond *= 1.10F;
                        break;
                    case 2:
                        MainWindow.scorePerSecond *= 1.20F;
                        break;
                    case 3:
                        MainWindow.scorePerSecond *= 1.30F;
                        break;
                    case 4:
                        MainWindow.scorePerSecond *= 1.40F;
                        break;
                    case 5:
                        MainWindow.scorePerSecond *= 1.50F;
                        break;
                    case 6:
                        MainWindow.scorePerSecond /= 1.40F;
                        break;
                    case 7:
                        MainWindow.scorePerSecond /= 1.30F;
                        break;
                    case 8:
                        MainWindow.scorePerSecond /= 1.20F;
                        break;
                    case 9:
                        MainWindow.scorePerSecond /= 1.10F;
                        break;
                    case 10:
                        MainWindow.scorePerSecond /= 1.50F;
                        break;
                }
                MainWindow.score -= NewCost();
            }
        }
        public void UpdateFrame()
        {
            lblCost.Content = NewCost();
            if (NewCost() > MainWindow.score)
            {
                button.Disable();
                lblCost.Disable();
            }
            else
            {
                button.Enable();
                lblCost.Enable();
            }
        }

        public int NewCost()
        {
            return (int)(baseCost * Math.Pow(1.15, count));
        }
        public int generate_random_number()
        {
            Random rnd = new Random();
            int zufallszahl = rnd.Next(1, 10);
            return zufallszahl;
        }
    }
}
