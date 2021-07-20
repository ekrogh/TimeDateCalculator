using AppKit;
using CustomRenderer;
using CustomRenderer.macOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

[assembly: ExportRenderer(typeof(myMacOSDatePicker), typeof(myMacOSDatePickerRenderer))]
namespace CustomRenderer.macOS
{
    public class myMacOSDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.DatePickerStyle = NSDatePickerStyle.ClockAndCalendar;
            }
        }
    }
}
