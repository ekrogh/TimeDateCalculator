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
		}

		private async void Open_Button_Clicked(object sender, System.EventArgs e)
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

//#if __MACOS__
//			await Navigation.PopAsync(true);
//#else
//			await Navigation.PopToRootAsync(true);
//#endif
		}
	}
}