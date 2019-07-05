using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TimeDateCalculator.Interfaces;

namespace TimeDateCalculatorDll
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutHelp : ContentPage
	{
		public AboutHelp()
		{
			InitializeComponent();

			AppNameAndVer.Text =
								'"'
								+ DependencyService.Get<IAppVersion>().GetAppTitle()
								+ '"'
								+ "  Version: "
								+ DependencyService.Get<IAppVersion>().GetVersion()
								+ DependencyService.Get<IAppVersion>().GetBuild()
								+ DependencyService.Get<IAppVersion>().GetRevision();

		}

		private async void OKButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}

		private void UsersGuideButton_Clicked(object sender, EventArgs e)
		{
			Device.OpenUri(new Uri("http://eksit.dk/users-guide-2/"));
		}

		private void MyUrlButton_Clicked(object sender, EventArgs e)
		{
			Device.OpenUri(new Uri("http://eksit.dk/"));
		}
	}
}