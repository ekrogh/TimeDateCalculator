using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeDateCalculatorP.Interfaces;
using Windows.UI.Xaml;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculatorP.UWP.ScreenSize_UWP))]
namespace TimeDateCalculatorP.UWP
{
    class ScreenSize_UWP : IScreenSizeInterface
    {
        public double GetScreenWidth()
        {
            return Window.Current.Bounds.Width;
        }

        public double GetScreenHeight()
        {
            return Window.Current.Bounds.Height;
        }
    }
}
