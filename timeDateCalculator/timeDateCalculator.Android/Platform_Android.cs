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
using TimeDateCalculatorP.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculatorP.Droid.Platform_Android))]
namespace TimeDateCalculatorP.Droid
{
    class Platform_Android : IPlatformInterface
    {
        public bool IsMobile() => true;
    }
}