using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TimeDateCalculator.Interfaces;
using Xamarin.Essentials;

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
			_ = await Navigation.PopAsync(true);
		}

		private async void UsersGuideButton_Clicked(object sender, EventArgs e)
		{
			if (await Launcher.CanOpenAsync(new Uri("http://eksit.dk/users-guide-2/")))
			{
				await Launcher.OpenAsync(new Uri("http://eksit.dk/users-guide-2/"));
			}
		}

		private async void MyUrlButton_Clicked(object sender, EventArgs e)
		{
			if (await Launcher.CanOpenAsync(new Uri("http://eksit.dk/")))
			{
				await Launcher.OpenAsync(new Uri("http://eksit.dk/"));
			}
		}

		private async void EmaiBtn_Clicked(object sender, EventArgs e)
		{
			if (await Launcher.CanOpenAsync(new Uri("mailto://timedatecalculator@eksit.dk")))
			{
				await Launcher.OpenAsync(new Uri("mailto://timedatecalculator@eksit.dk"));
			}
		}
	}
}