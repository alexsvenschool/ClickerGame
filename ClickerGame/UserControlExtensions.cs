using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ClickerGame
{
    public static class UserControlExtensions
    {
        public static Brush background = (Brush)new BrushConverter().ConvertFrom("#80A9A9A9");
        public static void Disable (this Button btn){
            btn.Foreground = Brushes.DarkGray;
            btn.Background = Brushes.Transparent;
        }

        public static void Disable(this Label btn)
        {
            btn.Foreground = Brushes.DarkGray;
            btn.Background = Brushes.Transparent;
        }

        public static void Enable(this Button btn)
        {
            btn.Foreground = Brushes.White;
            btn.Background = background;
        }

        public static void Enable(this Label btn)
        {
            btn.Foreground = Brushes.White;
            btn.Background = background;
        }
    }
}
