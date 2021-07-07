using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TimeDateCalculator.FileHandlers;
using System.Threading.Tasks;

namespace TimeDateCalculatorDll
{
	public partial class OpenICS : ContentPage
	{

		public OpenICS()
		{
			InitializeComponent();

			OpenTheFileAsync();
		}

		private readonly string[] filetypesToReadFrom = new string[] { "ics" };

		private async void OpenTheFileAsync()
		{
			await DependencyService.Get<IHandleFiles>().SelectFilesToReadFrom(filetypesToReadFrom);

			await Navigation.PopToRootAsync(true);
		}

	}
}