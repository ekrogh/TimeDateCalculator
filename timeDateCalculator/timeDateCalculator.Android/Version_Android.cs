using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content.PM;
using TimeDateCalculatorP.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculatorP.Droid.Version_Android))]
namespace TimeDateCalculatorP.Droid
{
    public class Version_Android : IAppVersion
    {
        private PackageInfo packageInfo;

        public string GetAppTitle()
        {
            var context = Application.Context;

            PackageManager manager = context.PackageManager;

            return manager.GetApplicationLabel(context.ApplicationInfo);
        }

        public string GetVersion()
        {
            var context = Application.Context;
            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionName;
        }

		public string GetBuild()
        {
            var context = Application.Context;
            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return '.' + PackageInfoCompat.GetLongVersionCode(info).ToString();
        }

        public string GetRevision()
		{
			return "";
		}
	}
}