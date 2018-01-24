using Foundation;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculator.iOS.Version_iOS))]
namespace TimeDateCalculator.iOS
{
    public class Version_iOS : Interfaces.IAppVersion
    {
        public string GetVersion()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        }
        public int GetBuild()
        {
            return int.Parse(NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString());
        }
    }
}
