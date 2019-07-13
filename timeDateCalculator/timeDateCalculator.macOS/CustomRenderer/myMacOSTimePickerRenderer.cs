﻿using AppKit;
using CustomRenderer;
using CustomRenderer.macOS;
using Foundation;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

[assembly: ExportRenderer(typeof(myMacOSTimePicker), typeof(myMacOSTimePickerRenderer))]
{
	// New from here
	public class myMacOSTimePickerRenderer : ViewRenderer<myMacOSTimePicker, NSDatePicker>
	{
		NSColor _defaultTextColor;
		NSColor _defaultBackgroundColor;
		bool _disposed;

		IElementController ElementController => Element;

		protected override void OnElementChanged(ElementChangedEventArgs<myMacOSTimePicker> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				if (Control == null)
				{
					SetNativeControl(new FormsNSDatePicker
					{
						DatePickerMode = NSDatePickerMode.Single,
						TimeZone = new NSTimeZone("UTC"),
						DatePickerStyle = NSDatePickerStyle.TextFieldAndStepper,
						DatePickerElements = NSDatePickerElementFlags.HourMinuteSecond
					});

					(Control as FormsNSDatePicker).FocusChanged += ControlFocusChanged;
					Control.ValidateProposedDateValue += HandleValueChanged;
					_defaultTextColor = Control.TextColor;
					_defaultBackgroundColor = Control.BackgroundColor;
				}

				UpdateTime();
				UpdateFont();
				UpdateTextColor();
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == TimePicker.TimeProperty.PropertyName ||
				e.PropertyName == TimePicker.FormatProperty.PropertyName)
				UpdateTime();

			if (e.PropertyName == TimePicker.TextColorProperty.PropertyName ||
				e.PropertyName == VisualElement.IsEnabledProperty.PropertyName)
				UpdateTextColor();

			if (e.PropertyName == Picker.FontSizeProperty.PropertyName ||
				e.PropertyName == Picker.FontFamilyProperty.PropertyName ||
				e.PropertyName == Picker.FontAttributesProperty.PropertyName)
				UpdateFont();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && !_disposed)
			{
				if (Control != null)
				{
					Control.ValidateProposedDateValue -= HandleValueChanged;
					(Control as FormsNSDatePicker).FocusChanged -= ControlFocusChanged;
				}

				_disposed = true;
			}
			base.Dispose(disposing);
		}

		protected override void SetBackgroundColor(Color color)
		{
			base.SetBackgroundColor(color);

			if (Control == null)
				return;
			Control.BackgroundColor = color == Color.Default ? _defaultBackgroundColor : color.ToNSColor();
		}

		void ControlFocusChanged(object sender, BoolEventArgs e)
		{
			ElementController?.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, e.Value);
		}

		void HandleValueChanged(object sender, NSDatePickerValidatorEventArgs e)
		{
			ElementController?.SetValueFromRenderer(TimePicker.TimeProperty, e.ProposedDateValue.ToDateTime() - new DateTime(2001, 1, 1));
		}

		void UpdateFont()
		{
			if (Control == null || Element == null)
				return;

			Control.Font = Element.ToNSFont();
		}

		void UpdateTime()
		{
			if (Control == null || Element == null)
				return;
			var time = new DateTime(2001, 1, 1).Add(Element.Time);
			var newDate = time.ToNSDate();
			if (!Equals(Control.DateValue, newDate))
				Control.DateValue = newDate;
		}

		void UpdateTextColor()
		{
			if (Control == null || Element == null)
				return;
			var textColor = Element.TextColor;

			if (textColor.IsDefault || !Element.IsEnabled)
				Control.TextColor = _defaultTextColor;
			else
				Control.TextColor = textColor.ToNSColor();
		}
	}
	//END new
	// Old from here
	//   public class myMacOSTimePickerRenderer : TimePickerRenderer
	//   {
	//       protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
	//       {
	//           base.OnElementChanged(e);

	//           if (Control != null)
	//           {
	//               Control.DatePickerStyle = NSDatePickerStyle.ClockAndCalendar;
	//               Control.DatePickerElements = NSDatePickerElementFlags.HourMinute;
	//			Control.ValidateProposedDateValue += HandleValueChanged;
	//		}
	//       }

	//	int noTimeErrors = 0;
	//	void HandleValueChanged(object sender, NSDatePickerValidatorEventArgs e)
	//	{

	//		var tstTim = e.ProposedTimeInterval;
	//		//if (e.ProposedDateValue.ToDateTime() >= new DateTime(2001, 1, 1))
	//		//{
	//		//	base.ElementController?.SetValueFromRenderer(TimePicker.TimeProperty, e.ProposedDateValue.ToDateTime() - new DateTime(2001, 1, 1));
	//		//}
	//		//else
	//		//{
	//		//	noTimeErrors++;
	//		//}
	//	}

	//}
}
