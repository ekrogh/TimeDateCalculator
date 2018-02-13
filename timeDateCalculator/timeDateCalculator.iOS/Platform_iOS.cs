using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using TimeDateCalculator.Interfaces;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculator.iOS.Platform_iOS))]
namespace TimeDateCalculator.iOS
{
    class Platform_iOS : IPlatformInterface
    {
        public bool IsMobile()
        {
            return true;
        }
    }
}