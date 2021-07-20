using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeDateCalculatorDll
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FileICS : ContentPage
	{
		public FileICS()
		{
			InitializeComponent();
			
			if (Device.RuntimePlatform == Device.Android)
			{
				FileICSContentPageName.SetAppThemeColor(ContentPage.BackgroundColorProperty, Color.White, Color.Black);
			}

		}

		private async void OpenICSButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new OpenICS(), true);
		}

		private async void SaveToICSButton_ClickedAsync(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new SaveToICS(), true);
		}

	}
}