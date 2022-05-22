using Xamarin.Forms;

namespace TimeDateCalculator.tizen.Tizen.TV
{
	internal class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
	{
		protected override void OnCreate()
		{
			base.OnCreate();
			LoadApplication(new App());
		}

		static void Main(string[] args)
		{
			var app = new Program();
			//global::Xamarin.Forms.Platform.Tizen.In
			Forms.Init(app);
			//global::Xamarin.Forms.Platform.Tizen.Forms.Init(app);
			app.Run(args);
		}
	}
}
