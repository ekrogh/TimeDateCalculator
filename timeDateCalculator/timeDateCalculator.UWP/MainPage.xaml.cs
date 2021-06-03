using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace TimeDateCalculatorP.UWP
{
	public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();


			ApplicationView.PreferredLaunchViewSize = new Size(950, 500);
			ApplicationView.PreferredLaunchWindowingMode =
				ApplicationViewWindowingMode.PreferredLaunchViewSize;

			LoadApplication(new TimeDateCalculatorP.App());
        }
    }
}
