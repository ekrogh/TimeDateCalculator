using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using TimeDateCalculatorP.MessageThings;

namespace TimeDateCalculatorP.Droid
{
    [Activity(Label = "TimeDateCalculatorP", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

			Xamarin.Essentials.Platform.Init(this, bundle); // add this line to your code, it may also be called: bundle

            global::Xamarin.Forms.Forms.SetFlags("RadioButton_Experimental");

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
 
           MessagingCenter.Subscribe<App>((App)Xamarin.Forms.Application.Current, MessengerKeys.LandscapeOrientationRequest, On_LandscapeOrientationRequest);
            MessagingCenter.Subscribe<App>((App)Xamarin.Forms.Application.Current, MessengerKeys.PortraitOrientationRequest, On_PortraitOrientationRequest);
        }

        private void On_LandscapeOrientationRequest(App arg1)
		{
            RequestedOrientation = ScreenOrientation.Landscape;
        }

        private void On_PortraitOrientationRequest(App arg1)
        {
            RequestedOrientation = ScreenOrientation.Portrait;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[ ] permissions, Android.Content.PM.Permission[ ] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

