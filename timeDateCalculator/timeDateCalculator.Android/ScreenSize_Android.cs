using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Hardware;
using TimeDateCalculator.Interfaces;
using Android.Util;
using Android.Content.Res;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculator.Droid.ScreenSize_Android))]
namespace TimeDateCalculator.Droid
{
    class ScreenSize_Android : IScreenSizeInterface
    {
        public double GetScreenWidth()
        {
            return 0.0f;
        }

        public double GetScreenHeight()
        {
            return 0.0f;
        }
    }
}