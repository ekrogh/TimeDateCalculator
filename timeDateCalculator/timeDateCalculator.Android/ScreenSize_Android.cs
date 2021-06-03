using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Hardware;
using TimeDateCalculatorP.Interfaces;
using Android.Util;
using Android.Content.Res;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculatorP.Droid.ScreenSize_Android))]
namespace TimeDateCalculatorP.Droid
{
    class ScreenSize_Android : IScreenSizeInterface
    {
        public double GetScreenWidth()
        {
            return 0;
        }

        public double GetScreenHeight()
        {
            return 0;
        }
    }
}