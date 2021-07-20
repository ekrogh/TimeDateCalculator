using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TimeDateCalculator.MessageThings;
using TimeDateCalculator;

namespace TimeDateCalculatorDll
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SaveToICS : ContentPage
	{
		public SaveToICS()
		{
			InitializeComponent();

			if (Device.RuntimePlatform == Device.Android)
			{
				SaveToICSContentPageName.SetAppThemeColor(ContentPage.BackgroundColorProperty, Color.White, Color.Black);
				Resources["DynamicBaseButtonStyle"] = Resources["AndroidBaseButtonStyle"];
			}
			else
			{
				Resources["DynamicBaseButtonStyle"] = Resources["baseButtonStyle"];
			}

			Summary.Focus();
		}

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
		private async void SaveICSButton_Clicked(object sender, EventArgs e)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
		{

			SaveToIcsMessageArgs IcsDescription = new SaveToIcsMessageArgs
			{
				EventName_Summary = Summary.Text,
				TheDescription = Description.Text,
				Location = LocationEntry.Text
			};

			if ((Summary.Text == null) || (Summary.Text == ""))
			{
				IcsDescription.EventName_Summary = "Summary";
			}
			if ((Description.Text == null) || (Description.Text == ""))
			{
				IcsDescription.TheDescription = "Description";
			}
			if ((LocationEntry.Text == null) || (LocationEntry.Text == ""))
			{
				IcsDescription.Location = "Location";
			}

			// Fire the message
			MessagingCenter.Send
				(
					(App)Application.Current,
					MessengerKeys.SaveToIcsMessageKey,
					IcsDescription
				);


		}

	}
}