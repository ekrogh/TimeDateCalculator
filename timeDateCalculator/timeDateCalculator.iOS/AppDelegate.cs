using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using TimeDateCalculatorP.MessageThings;
using Xamarin.Forms;

namespace TimeDateCalculatorP.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.SetFlags("RadioButton_Experimental");

			global::Xamarin.Forms.Forms.Init();
			LoadApplication(new App());

			MessagingCenter.Subscribe<App>((App)Xamarin.Forms.Application.Current, MessengerKeys.LandscapeOrientationRequest, On_LandscapeOrientationRequest);
			MessagingCenter.Subscribe<App>((App)Xamarin.Forms.Application.Current, MessengerKeys.PortraitOrientationRequest, On_PortraitOrientationRequest);
			MessagingCenter.Subscribe<App>((App)Xamarin.Forms.Application.Current, MessengerKeys.AllButUpsideDowntOrientationRequest, On_AllButUpsideDowntOrientationRequest);

			return base.FinishedLaunching(app, options);
		}

		private void On_LandscapeOrientationRequest(App arg1)
		{
			UIDevice.CurrentDevice.SetValueForKey(NSNumber.FromNInt((int)UIInterfaceOrientation.LandscapeRight), new NSString("orientation"));
		}

		private void On_PortraitOrientationRequest(App arg1)
		{
			UIDevice.CurrentDevice.SetValueForKey(NSNumber.FromNInt((int)(UIInterfaceOrientation.Portrait)), new NSString("orientation"));
		}

		private void On_AllButUpsideDowntOrientationRequest(App arg1)
		{
			UIDevice.CurrentDevice.SetValueForKey(NSNumber.FromNInt((int)(UIInterfaceOrientation.Unknown)), new NSString("orientation"));
		}
	}
}
