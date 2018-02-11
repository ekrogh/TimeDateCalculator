using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeDateCalculator.Interfaces;
using Windows.UI.Xaml;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculator.UWP.ScreenSize_UWP))]
namespace TimeDateCalculator.UWP
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
