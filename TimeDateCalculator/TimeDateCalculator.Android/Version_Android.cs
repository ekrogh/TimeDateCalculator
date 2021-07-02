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
using TimeDateCalculator.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculator.Droid.Version_Android))]
namespace TimeDateCalculator.Droid
{
    public class Version_Android : IAppVersion
    {
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