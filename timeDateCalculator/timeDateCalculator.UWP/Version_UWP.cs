using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeDateCalculatorP.Interfaces;
using Windows.ApplicationModel;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculatorP.UWP.Version_UWP))]
namespace TimeDateCalculatorP.UWP
{
    public class Version_UWP : IAppVersion
    {
        public string GetAppTitle()
        {
            return Package.Current.DisplayName;
        }

        public string GetVersion()
        {
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;
            return $"{version.Major}.{version.Minor}";
        }

        public string GetBuild()
        {
            var package = Package.Current;
            var packageId = package.Id;
            var version = packageId.Version;
            return '.' + $"{version.Build}";
        }

		public string GetRevision()
		{
			var package = Package.Current;
			var packageId = package.Id;
			var version = packageId.Version;
			return '.' + $"{version.Revision}";
		}
	}
}
