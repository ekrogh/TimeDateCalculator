using AppKit;
using CoreGraphics;
using Foundation;

using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

namespace TimeDateCalculator.macOS
{
    [Register("AppDelegate")]
    public class AppDelegate : global::Xamarin.Forms.Platform.MacOS.FormsApplicationDelegate
    {
        NSWindow window;
        public AppDelegate()
        {
            var style = NSWindowStyle.Closable | NSWindowStyle.Resizable | NSWindowStyle.Titled;

            var rect = new CoreGraphics.CGRect(200, 1000, 1024, 768);
            window = new NSWindow(rect, style, NSBackingStore.Buffered, false);
            window.Title = "Xamarin.Forms on Mac!"; // choose your own Title here
            window.TitleVisibility = NSWindowTitleVisibility.Hidden;
        }

        public override NSWindow MainWindow
        {
            get { return window; }
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            /*HERFRA

        private double nativeTotalStackWidthLandscape = 731.0;
        private double nativeTotalStackWidthPortrait = 562.0;
        private double nativeTotalStackHeightLandscape = 311.0;
        private double nativeTotalStackHeightPortrait = 732.0;
* HERTIL*/
            window.MinSize = new CGSize(790, 311); // Set min window size

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            base.DidFinishLaunching(notification);
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }


        public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender)
        {
            return true;
        }
    }
}
