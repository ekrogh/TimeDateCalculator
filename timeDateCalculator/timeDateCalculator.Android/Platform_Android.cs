using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TimeDateCalculator.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculator.Droid.Platform_Android))]
namespace TimeDateCalculator.Droid
{
    class Platform_Android : IPlatformInterface
    {
        public bool IsMobile() => true;
    }
}