using TimeDateCalculator;
using TimeDateCalculator.MessageThings;
using Xamarin.Forms;

namespace TimeDateCalculatorDll
{
	public partial class OpenICS : ContentPage
	{

		public OpenICS()
		{
			InitializeComponent();

			if (Device.RuntimePlatform == Device.Android)
			{
				OpenICSContentPageName.SetAppThemeColor(ContentPage.BackgroundColorProperty, Color.White, Color.Black);
				Resources["DynamicBaseButtonStyle"] = Resources["AndroidBaseButtonStyle"];
			}
			else
			{
				Resources["DynamicBaseButtonStyle"] = Resources["baseButtonStyle"];

				if (Device.RuntimePlatform == Device.macOS)
				{
					FileOpenCancelButton.IsVisible = true;
				}
			}
		}

		private void Open_Button_Clicked(object sender, System.EventArgs e)
		{
			OpenIcsMessageArgs TheAgr = new OpenIcsMessageArgs
			{
				CorrectForTimeZone = SwitchTimeZone.IsToggled
			};

			// Fire the message
			MessagingCenter.Send
					(
						(App)Application.Current,
						MessengerKeys.OpenIcsMessageKey,
						TheAgr
					);

		}

		private async void FileOpenCancelButtn_Clicked(object sender, System.EventArgs e)
		{
			await Navigation.PopToRootAsync(true);
		}
	}
}