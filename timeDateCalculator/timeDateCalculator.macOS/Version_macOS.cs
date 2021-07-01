using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using TimeDateCalculator.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculator.macOS.Version_macOS))]
namespace TimeDateCalculator.macOS
{
    public class Version_macOS : IAppVersion
    {
        public string GetAppTitle()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleName").ToString();
        }

        public string GetVersion()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();

        }

        public string GetBuild()
        {
            return '.' + NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString();

        }

		public string GetRevision()
		{
			return "".ToString();
		}
	}
}
