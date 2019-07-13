using AppKit;
using CustomRenderer;
using CustomRenderer.macOS;
using System;
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
				Control.ValidateProposedDateValue += HandleValueChanged;
			}
        }

		int noTimeErrors = 0;
		void HandleValueChanged(object sender, NSDatePickerValidatorEventArgs e)
		{
			
			var tstTim = e.ProposedTimeInterval;
			//if (e.ProposedDateValue.ToDateTime() >= new DateTime(2001, 1, 1))
			//{
			//	base.ElementController?.SetValueFromRenderer(TimePicker.TimeProperty, e.ProposedDateValue.ToDateTime() - new DateTime(2001, 1, 1));
			//}
			//else
			//{
			//	noTimeErrors++;
			//}
		}

	}
}

