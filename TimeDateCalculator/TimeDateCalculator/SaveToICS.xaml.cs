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
		}

		private async void SaveICSButton_Clicked(object sender, EventArgs e)
		{

			IcsDescriptionMessageArgs IcsDescription = new IcsDescriptionMessageArgs
			{
				EventName_Summary = Summary.Text,
				TheDescription = Description.Text,
				Location =  LocationEntry.Text
			};

			if ((Summary.Text == null) || (Summary.Text == ""))
			{
				IcsDescription.EventName_Summary = "TimeDateCalculator Event";
			}

			// Fire the message
			MessagingCenter.Send
				(
					(App)Application.Current,
					MessengerKeys.IcsDescriptionEntered,
					IcsDescription
				);

			_ = await Navigation.PopAsync(true);

		}

	}
}