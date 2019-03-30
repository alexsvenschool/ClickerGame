using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ClickerGame
{    
    class PowerUp
    {
        private readonly Label lblCost;
        private readonly Label lblCount;
        public int count;
        public int baseCost;

        public PowerUp(Label cost, Label count)
        {
            lblCost = cost;
            lblCount = count;
        }


    }
}
