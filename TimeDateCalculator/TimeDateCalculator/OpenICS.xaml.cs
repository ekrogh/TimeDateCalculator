using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TimeDateCalculator.FileHandlers;
using System.Threading.Tasks;
using TimeDateCalculator.MessageThings;
using TimeDateCalculator;

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
	}
}