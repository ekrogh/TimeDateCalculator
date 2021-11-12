using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

namespace TimeDateCalculator.GTK
{
	class MainClass
	{
		[STAThread]
		public static void Main(string[] args)
		{
			Gtk.Application.Init();
			Forms.Init();

			var app = new App();
			var window = new FormsWindow();
			window.SetDefaultSize(950, 500);
			window.SetSizeRequest(950, 500);

			window.LoadApplication(app);
			window.SetApplicationTitle("TimeDateCalculator");
			window.Show();

			Gtk.Application.Run();
		}
	}
}
