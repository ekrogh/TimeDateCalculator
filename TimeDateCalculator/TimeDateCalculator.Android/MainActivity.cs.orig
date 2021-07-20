using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;

using Plugin.CurrentActivity;
using Acr.UserDialogs;

using TimeDateCalculator.MessageThings;


namespace TimeDateCalculator.Droid
{
	[Activity(Label = "TimeDateCalculator", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]

	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		Context LoclContext => CrossCurrentActivity.Current.Activity;
		internal static MainActivity Instance { get; private set; }

		public const int MYOPENFILECODE = 100;
		public const int MYSAVEILECODE = 200;

		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			Xamarin.Essentials.Platform.Init(this, bundle); // add this line to your code, it may also be called: bundle

			Forms.Init(this, bundle);

			Instance = this;

			UserDialogs.Init(this);

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

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			try
			{
				SelectFileResultMessageArgs args = new SelectFileResultMessageArgs();

				switch (requestCode)
				{
					case MYOPENFILECODE:
						{
							if (resultCode == Result.Ok)
							{
								args.DidPick = true;

								if (data != null)
								{

									args.TheSelectedFileInfo = new SelectedFileInfo();

									SelectedFileInfo urlHere = new SelectedFileInfo();

									ClipData clipData = data.ClipData;
									if (clipData != null)
									{
										for (int i = 0; i < clipData.ItemCount; i++)
										{
											ClipData.Item item = clipData.GetItemAt(i);
											Android.Net.Uri uri = item.Uri;

											if (uri != null)
											{
												urlHere.TheStream = LoclContext.ContentResolver.OpenInputStream(uri);
											}

											args.TheSelectedFileInfo = urlHere;
										}
									}
									else
									{
										Android.Net.Uri uri = data.Data;

										if (uri != null)
										{
											urlHere.TheStream = LoclContext.ContentResolver.OpenInputStream(uri);
										}

										args.TheSelectedFileInfo = urlHere;
									}
								}
							}
							else
							{
								args.DidPick = false;
							}

							// Fire the message
							MessagingCenter.Send(
								(App)Xamarin.Forms.Application.Current,
								MessengerKeys.FileToReadFromSelected,
								args);

							break;
						}
					case MYSAVEILECODE:
						{
							if (resultCode == Result.Ok)
							{
								args.DidPick = true;

								if (data != null)
								{

									args.TheSelectedFileInfo = new SelectedFileInfo();

									SelectedFileInfo urlHere = new SelectedFileInfo();

									ClipData clipData = data.ClipData;
									if (clipData != null)
									{
										for (int i = 0; i < clipData.ItemCount; i++)
										{
											ClipData.Item item = clipData.GetItemAt(i);
											Android.Net.Uri uri = item.Uri;

											if (uri != null)
											{
												urlHere.TheStream = LoclContext.ContentResolver.OpenOutputStream(uri);
											}

											args.TheSelectedFileInfo = urlHere;
										}
									}
									else
									{
										Android.Net.Uri uri = data.Data;

										if (uri != null)
										{
											urlHere.TheStream = LoclContext.ContentResolver.OpenOutputStream(uri);
										}

										args.TheSelectedFileInfo = urlHere;
									}
								}
							}
							else
							{
								args.DidPick = false;
							}

							// Fire the message
							MessagingCenter.Send(
								(App)Xamarin.Forms.Application.Current,
								MessengerKeys.FileToSaveToSelected,
								args);

							break;
						}
				}
			}
			catch (Exception ex)
			{
				var excptn = ex.ToString();
			}

		}
	}
}

