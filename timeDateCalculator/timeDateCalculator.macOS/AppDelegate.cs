using AppKit;
using CoreGraphics;
using Foundation;

namespace TimeDateCalculator.macOS
{
	[Register("AppDelegate")]
    public class AppDelegate : global::Xamarin.Forms.Platform.MacOS.FormsApplicationDelegate
    {
        NSWindow window;
        public AppDelegate()
        {
            var style = NSWindowStyle.Closable | NSWindowStyle.Resizable | NSWindowStyle.Titled;

            var rect = new CoreGraphics.CGRect(200, 600, 950, 550);
            window = new NSWindow(rect, style, NSBackingStore.Buffered, false);
            window.Title = "TimeDateCalculator"; // choose your own Title here
            window.TitleVisibility = NSWindowTitleVisibility.Hidden;
        }

        public override NSWindow MainWindow
        {
            get { return window; }
        }

        public override void DidFinishLaunching(NSNotification notification)
        {
            window.MinSize = new CGSize(950, 550); // Set min window size

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
