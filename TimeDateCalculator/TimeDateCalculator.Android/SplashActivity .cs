using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;

namespace TimeDateCalculator.Droid
{
	[Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        private SemaphoreSlim signal = new SemaphoreSlim(0, 1);

        Task startupWork;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            //StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            startupWork = new Task(() => { StartActivity(new Intent(Android.App.Application.Context, typeof(MainActivity))); });
			startupWork.Start();

            startupWork.Wait();
        }
        // Prevent the back button from canceling the startup process
        public override void OnBackPressed() { }
	}
}