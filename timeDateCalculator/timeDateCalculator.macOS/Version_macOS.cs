using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using TimeDateCalculatorP.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculatorP.macOS.Version_macOS))]
namespace TimeDateCalculatorP.macOS
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
