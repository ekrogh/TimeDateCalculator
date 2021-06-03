using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeDateCalculatorP.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(TimeDateCalculatorP.UWP.Platform_UWP))]
namespace TimeDateCalculatorP.UWP
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
