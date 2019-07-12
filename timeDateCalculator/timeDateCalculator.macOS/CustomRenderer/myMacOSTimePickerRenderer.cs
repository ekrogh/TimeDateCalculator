using AppKit;
using CustomRenderer;
using CustomRenderer.macOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

[assembly: ExportRenderer(typeof(myMacOSTimePicker), typeof(myMacOSTimePickerRenderer))]namespace CustomRenderer.macOS
{
    public class myMacOSTimePickerRenderer : TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.DatePickerStyle = NSDatePickerStyle.ClockAndCalendar;
                Control.DatePickerElements = NSDatePickerElementFlags.HourMinute;

            }
        }
    }
}

