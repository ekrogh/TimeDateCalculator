using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using TimeDateCalculator.Interfaces;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculator.iOS.ScreenSize_iOS))]
namespace TimeDateCalculator.iOS
{
    class ScreenSize_iOS : IScreenSizeInterface
    {
        public double GetScreenWidth()
        {
            return UIScreen.MainScreen.Bounds.Width;
        }

        public double GetScreenHeight()
        {
            return UIScreen.MainScreen.Bounds.Height;
        }
    }
}