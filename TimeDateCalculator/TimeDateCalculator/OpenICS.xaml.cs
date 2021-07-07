using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TimeDateCalculator.MessageThings;
using TimeDateCalculator;
using TimeDateCalculator.FileHandlers;

namespace TimeDateCalculatorDll
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OpenICS : ContentPage
	{

		public OpenICS()
		{
			InitializeComponent();

			OpenTheFile();
		}

		private readonly string[] filetypesToReadFrom = new string[] { "ics" };
		private async void OpenTheFile()
		{
			await DependencyService.Get<IHandleFiles>().SelectFilesToReadFrom(filetypesToReadFrom);

			_ = await Navigation.PopAsync(true);

		}

	}
}