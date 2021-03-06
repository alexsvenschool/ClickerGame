﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClickerGame
{    
    class PowerUp
    {
        private readonly Button button;
        private readonly Label lblCost;
        private readonly Label lblCount;        
        private readonly int baseCost;
        private readonly float baseScore;
        public int count = 0;
        public bool purchasedUpgrade = false;
        public bool upgradeAvailable = false;

        // Konstruktor
        public PowerUp(Button btn, Label cost, Label count, int bCost, float bScore)
        {
            lblCost     = cost;
            lblCount    = count;
            baseCost    = bCost;
            baseScore   = bScore;
            button      = btn;
            btn.Tag     = this;
        }

        public void OnClick()
        {
            if(MainWindow.score >= NewCost())
            {
                MainWindow.score -= NewCost();
                MainWindow.scorePerSecond -= ScorePerSecond();
                count++;
                MainWindow.scorePerSecond += ScorePerSecond();
            }
            if(count % 25 == 0 && count > 0)
            {
                upgradeAvailable = true;
                purchasedUpgrade = false;
            }
        }

        public float ScorePerSecond()
        {
            return baseScore * count;
        }

        public void UpdateFrame()
        {
            lblCost.Content = NewCost();
            lblCount.Content = count;
            if(NewCost() > MainWindow.score)
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
    }
}
