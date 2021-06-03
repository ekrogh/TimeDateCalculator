using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using TimeDateCalculatorP.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculatorP.iOS.Version_iOS))]
namespace TimeDateCalculatorP.iOS
{
    public class Version_iOS : IAppVersion
    {
        public string GetAppTitle()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleDisplayName").ToString();
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
