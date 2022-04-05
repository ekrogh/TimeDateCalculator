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
				Resources["DynamicBaseButtonStyle"] = Resources["AndroidBaseButtonStyle"];
			}
			else
			{
				Resources["DynamicBaseButtonStyle"] = Resources["baseButtonStyle"];

				if (Device.RuntimePlatform == Device.macOS)
				{
					FileCancelButton.IsVisible = true;
				}
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

		private async void FileCancelButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopToRootAsync(true);
		}
	}
}