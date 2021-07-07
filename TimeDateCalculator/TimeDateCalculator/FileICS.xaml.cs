using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TimeDateCalculator.MessageThings;
using TimeDateCalculator;
using System.Threading.Tasks;

namespace TimeDateCalculatorDll
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FileICS : ContentPage
	{
		public FileICS()
		{
			InitializeComponent();
		}

		private async void OpenICSButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new OpenICS(), true);
		}

		private async Task SaveToICSButton_ClickedAsync(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new SaveToICS(), true);
		}
	}
}