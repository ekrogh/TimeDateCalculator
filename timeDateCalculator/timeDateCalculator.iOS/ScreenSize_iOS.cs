using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using TimeDateCalculatorP.Interfaces;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculatorP.iOS.ScreenSize_iOS))]
namespace TimeDateCalculatorP.iOS
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