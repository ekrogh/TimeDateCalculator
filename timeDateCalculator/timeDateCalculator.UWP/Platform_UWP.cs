using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeDateCalculator.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculator.UWP.Platform_UWP))]
namespace TimeDateCalculator.UWP
{
    class Platform_UWP : IPlatformInterface
    {
        public bool IsMobile()
        {
            var qualifiers = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().QualifierValues;
            return (qualifiers.ContainsKey("DeviceFamily") && qualifiers["DeviceFamily"] == "Mobile");
        }
    }
}
