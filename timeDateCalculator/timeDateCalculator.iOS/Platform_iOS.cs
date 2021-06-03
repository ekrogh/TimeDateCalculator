using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using TimeDateCalculatorP.Interfaces;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculatorP.iOS.Platform_iOS))]
namespace TimeDateCalculatorP.iOS
{
    class Platform_iOS : IPlatformInterface
    {
        public bool IsMobile()
        {
            return true;
        }
    }
}