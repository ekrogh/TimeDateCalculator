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
				TheDescription = Description.Text
			};

			if ((Description.Text == null) || (Description.Text == ""))
			{
				IcsDescription.TheDescription = "TimeDateCalcolator";
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