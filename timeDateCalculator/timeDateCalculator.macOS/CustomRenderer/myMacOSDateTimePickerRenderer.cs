using AppKit;
using CustomRenderer;
using CustomRenderer.macOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

[assembly: ExportRenderer(typeof(myMacOSDateTimePicker), typeof(myMacOSDateTimePickerRenderer))]namespace CustomRenderer.macOS
{
	public class myMacOSDateTimePickerRenderer : DatePickerRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
		{
			base.OnElementChanged(e);

			if (Control != null)
			{
				Control.DatePickerStyle = NSDatePickerStyle.ClockAndCalendar;
				Control.DatePickerElements = NSDatePickerElementFlags.YearMonthDateDay | NSDatePickerElementFlags.HourMinuteSecond;
			}
		}
	}
}
