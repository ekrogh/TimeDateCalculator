using CustomRenderer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TimeDateCalculator.Interfaces;
using TimeDateCalculatorDll;
using TimeDateCalculator.MessageThings;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace TimeDateCalculator
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(true)]
	public partial class MainPage : ContentPage
	{

		private double width;
		private double height;

		private double ScreenWidth = 0.0;
		private double ScreenHeight = 0.0;

		private bool firstTime = true;
		private bool firstTimeWdthOrHeightChanged = true;

		private readonly double nativeTotalStackWidthLandscape = 731.0;
		private readonly double nativeTotalStackHeightPortrait = 732.0;

		private double StartDateTimeIntroLabelNameFontSizeOrig = 0.0;

		private double StartEndDayNameFontSizeOrig = 0.0;


		private DateTime _startDateTimeIn;
		public DateTime StartDateTimeIn
		{
			get { return _startDateTimeIn; }
			set { _startDateTimeIn = value; }
		}

		private DateTime _startDateIn;
		public DateTime StartDateIn
		{
			get { return _startDateIn; }
			set { _startDateIn = value; }
		}
		public string StartDateInString
		{
			get { return _startDateIn.Date.ToString("u").Substring(0, 10); }
			set
			{
				if( DateTime.TryParse(value, out DateTime result) )
				{
					_startDateIn = result;
				}
			}
		}

		private TimeSpan _startTimeIn;
		public TimeSpan StartTimeIn
		{
			get { return _startTimeIn; }
			set { _startTimeIn = value; }
		}
		public string StartTimeInString
		{
			get { return _startTimeIn.ToString("c").Substring(0, 5); }
			set
			{
				if( TimeSpan.TryParse(value, out TimeSpan result) )
				{
					_startTimeIn = result;
				}
			}
		}

		private bool _calcStartDateSwitchIsOn = false;
		public bool CalcStartDateSwitchIsOn
		{
			get { return _calcStartDateSwitchIsOn; }
			set { _calcStartDateSwitchIsOn = value; }
		}

		private DateTime _startDateTimeOut;
		public DateTime StartDateTimeOut
		{
			get { return _startDateTimeOut; }
			set { _startDateTimeOut = value; }
		}

		private DateTime _endDateTimeIn;
		public DateTime EndDateTimeIn
		{
			get { return _endDateTimeIn; }
			set { _endDateTimeIn = value; }
		}

		private DateTime _endDateIn;
		public DateTime EndDateIn
		{
			get { return _endDateIn; }
			set { _endDateIn = value; }
		}
		public string EndDateInString
		{
			get { return _endDateIn.Date.ToString("u").Substring(0, 10); }
			set
			{
				if( DateTime.TryParse(value, out DateTime result) )
				{
					_endDateIn = result;
				}
			}
		}

		private TimeSpan _endTimeIn;
		public TimeSpan EndTimeIn
		{
			get { return _endTimeIn; }
			set { _endTimeIn = value; }
		}
		public string EndTimeInString
		{
			get { return _endTimeIn.ToString("c").Substring(0, 5); }
			set
			{
				if( TimeSpan.TryParse(value, out TimeSpan result) )
				{
					_endTimeIn = result;
				}
			}
		}

		private bool _calcEndDateSwitchIsOn = false;
		public bool CalcEndDateSwitchIsOn
		{
			get { return _calcEndDateSwitchIsOn; }
			set { _calcEndDateSwitchIsOn = value; }
		}

		private DateTime _endDateTimeOut;
		public DateTime EndDateTimeOut
		{
			get { return _endDateTimeOut; }
			set { _endDateTimeOut = value; }
		}


		private bool _calcYMWDHMIsOn = true;
		public bool CalcYMWDHMIsOn
		{
			get { return _calcYMWDHMIsOn; }
			set { _calcYMWDHMIsOn = value; }
		}

		private TimeSpan PrivEnteredYMWDHMTimeSpan { get; set; } = new TimeSpan(0);
		public TimeSpan EnteredYMWDHMTimeSpan
		{
			get { return PrivEnteredYMWDHMTimeSpan; }
			set { PrivEnteredYMWDHMTimeSpan = value; }
		}


		private readonly List<Entry> ListOfCmbndEntrys;
		private readonly List<Entry> ListOfTotEntrys;

		private readonly List<Switch> ListOfSwitches;


		// Total values for dateTime span
		private Int32 TotYearsIn = 0;
		private Int32 TotMonthsIn = 0;
		private Int32 TotWeeksIn = 0;
		private Int32 TotDaysIn = 0;
		private Int32 TotHoursIn = 0;
		private Int32 TotMinutesIn = 0;
		// Values for "Combnd" dateTime span
		private int CombndYearsIn = 0;
		private int CombndMonthsIn = 0;
		private int CombndWeeksIn = 0;
		private int CombndDaysIn = 0;
		private int CombndHoursIn = 0;
		private int CombndMinutesIn = 0;
		// Output values
		// Combnd
		private int CombndYearsOut = 0;
		private int CombndMonthsOut = 0;
		private int CombndWeeksOut = 0;
		private int CombndDaysOut = 0;
		private int CombndHoursOut = 0;
		private int CombndMinutesOut = 0;
		// Total values for dateTime span
		private Int64 TotYearsOut = 0;
		private Int64 TotMonthsOut = 0;
		private Int64 TotWeeksOut = 0;
		private Int64 TotDaysOut = 0;
		private Int64 TotHoursOut = 0;
		private Int64 TotMinutesOut = 0;


		private void SetStartDateTime()
		{
			StartDateEntry.SetBinding(Entry.TextProperty, "StartDateInString", BindingMode.TwoWay);

			StartDatePicker.Date = StartDateIn;

			StartTimePicker.Time = StartTimeIn;

			StartTimeEntry.SetBinding(Entry.TextProperty, "StartTimeInString", BindingMode.TwoWay);

			StartDayName.Text = StartDateIn.DayOfWeek.ToString().Remove(3);
		}

		private void SetEndDateTime()
		{
			EndDateEntry.SetBinding(Entry.TextProperty, "EndDateInString", BindingMode.TwoWay);

			EndDatePicker.Date = EndDateIn;

			EndTimePicker.Time = EndTimeIn;

			EndTimeEntry.SetBinding(Entry.TextProperty, "EndTimeInString", BindingMode.TwoWay);

			EndDayName.Text = EndDateIn.DayOfWeek.ToString().Remove(3);
		}

		private void ClearTotIOVars()
		{
			// Total values for dateTime span
			TotYearsIn = 0;
			TotMonthsIn = 0;
			TotWeeksIn = 0;
			TotDaysIn = 0;
			TotHoursIn = 0;
			TotMinutesIn = 0;
			// Total values for dateTime span
			TotYearsOut = 0;
			TotMonthsOut = 0;
			TotWeeksOut = 0;
			TotDaysOut = 0;
			TotHoursOut = 0;
			TotMinutesOut = 0;
		}
		private void ClearCmbndIOVars()
		{
			// Values for "Combnd" dateTime span
			CombndYearsIn = 0;
			CombndMonthsIn = 0;
			CombndWeeksIn = 0;
			CombndDaysIn = 0;
			CombndHoursIn = 0;
			CombndMinutesIn = 0;
			// Combnd
			CombndYearsOut = 0;
			CombndMonthsOut = 0;
			CombndWeeksOut = 0;
			CombndDaysOut = 0;
			CombndHoursOut = 0;
			CombndMinutesOut = 0;
		}

		private void EnableCmbndYMWDHM(Entry ImInFocus)
		{
			foreach( Entry CurEntry in ListOfCmbndEntrys )
			{
				if( CurEntry != ImInFocus )
				{
					CurEntry.IsEnabled = true;
				}
			}
		}

		private void EnableTotYMWDHM(Entry ImInFocus)
		{
			foreach( Entry CurEntry in ListOfTotEntrys )
			{
				if( CurEntry != ImInFocus )
				{
					CurEntry.IsEnabled = true;
				}
			}
		}

		private void EnableYMWDHM(Entry ImInFocus)
		{
			EnableCmbndYMWDHM(ImInFocus);
			EnableTotYMWDHM(ImInFocus);
		}

		private void DisableCmbndYMWDHM(Entry ImInFocus)
		{
			foreach( Entry CurEntry in ListOfCmbndEntrys )
			{
				if( CurEntry != ImInFocus )
				{
					CurEntry.IsEnabled = false;
				}
			}
		}

		private void DisableTotYMWDHM(Entry ImInFocus)
		{
			foreach( Entry CurEntry in ListOfTotEntrys )
			{
				if( CurEntry != ImInFocus )
				{
					CurEntry.IsEnabled = false;
				}
			}
		}

		private void DisableYMWDHM(Entry ImInFocus)
		{
			DisableCmbndYMWDHM(ImInFocus);
			DisableTotYMWDHM(ImInFocus);
		}

		private void EnableAndToggleOffAllSwitchedXceptMe(Switch SwitchToDisaable)
		{
			foreach( Switch CurSwitch in ListOfSwitches )
			{
				if( CurSwitch != SwitchToDisaable )
				{
					CurSwitch.IsEnabled = true;
					CurSwitch.IsToggled = false;
				}
				else
				{
					CurSwitch.IsEnabled = false;
					CurSwitch.IsToggled = true;
				}
			}
		}

		private void RWCmbndYMWDHM(Entry ImInFocus)
		{
			foreach( Entry CurEntry in ListOfCmbndEntrys )
			{
				if( CurEntry != ImInFocus )
				{
					CurEntry.IsReadOnly = false;
				}
			}
		}

		private void RWTotYMWDHM(Entry ImInFocus)
		{
			foreach( Entry CurEntry in ListOfTotEntrys )
			{
				if( CurEntry != ImInFocus )
				{
					CurEntry.IsReadOnly = false;
				}
			}
		}

		private void RWYMWDHM(Entry ImInFocus)
		{
			RWCmbndYMWDHM(ImInFocus);
			RWTotYMWDHM(ImInFocus);
		}

		private void ClearCmbndYMWDHM(Entry ImInFocus)
		{
			foreach( Entry CurEntry in ListOfCmbndEntrys )
			{
				if( CurEntry != ImInFocus )
				{
					CurEntry.Text = "";
				}
			}
			ClearCmbndIOVars();
		}

		private void ClearTotYMWDHM(Entry ImInFocus)
		{
			foreach( Entry CurEntry in ListOfTotEntrys )
			{
				if( CurEntry != ImInFocus )
				{
					CurEntry.Text = "";
				}
			}
			ClearTotIOVars();
		}

		private void ClearYMWDHM(Entry ImInFocus)
		{
			ClearCmbndYMWDHM(ImInFocus);
			ClearTotYMWDHM(ImInFocus);
		}

		private void ClearAllIOVars()
		{
			ClearTotIOVars();
			ClearCmbndIOVars();
		}

		private void DoClearAll()
		{

			SetStartDateTime();

			SetEndDateTime();

			ClearYMWDHM(null);

			switch( Device.RuntimePlatform )
			{
				case Device.iOS:
					{
						CombndYears.WidthRequest = 105;
						CombndMonths.WidthRequest = 105;
						CombndWeeks.WidthRequest = 105;
						CombndDays.WidthRequest = 105;
						CombndHours.WidthRequest = 105;
						CombndMinutes.WidthRequest = 105;

						TotYears.WidthRequest = 105;
						TotMonths.WidthRequest = 105;
						TotWeeks.WidthRequest = 105;
						TotDays.WidthRequest = 105;
						TotHours.WidthRequest = 105;
						TotMinutes.WidthRequest = 105;

						break;
					}
				case Device.Android:
					{
						CombndYears.WidthRequest = 88;
						CombndMonths.WidthRequest = 88;
						CombndWeeks.WidthRequest = 88;
						CombndDays.WidthRequest = 88;
						CombndHours.WidthRequest = 88;
						CombndMinutes.WidthRequest = 88;

						TotYears.WidthRequest = 88;
						TotMonths.WidthRequest = 88;
						TotWeeks.WidthRequest = 88;
						TotDays.WidthRequest = 88;
						TotHours.WidthRequest = 88;
						TotMinutes.WidthRequest = 88;

						break;
					}
				case Device.UWP:
					{
						CombndYears.WidthRequest = 121;
						CombndMonths.WidthRequest = 121;
						CombndWeeks.WidthRequest = 121;
						CombndDays.WidthRequest = 121;
						CombndHours.WidthRequest = 121;
						CombndMinutes.WidthRequest = 121;

						TotYears.WidthRequest = 121;
						TotMonths.WidthRequest = 121;
						TotWeeks.WidthRequest = 121;
						TotDays.WidthRequest = 121;
						TotHours.WidthRequest = 121;
						TotMinutes.WidthRequest = 121;

						break;
					}
				default: //Set as UWP
					{
						CombndYears.WidthRequest = 121;
						CombndMonths.WidthRequest = 121;
						CombndWeeks.WidthRequest = 121;
						CombndDays.WidthRequest = 121;
						CombndHours.WidthRequest = 121;
						CombndMinutes.WidthRequest = 121;

						TotYears.WidthRequest = 121;
						TotMonths.WidthRequest = 121;
						TotWeeks.WidthRequest = 121;
						TotDays.WidthRequest = 121;
						TotHours.WidthRequest = 121;
						TotMinutes.WidthRequest = 121;

						break;
					}
			}

			ClearAllIOVars();
		}

		private readonly Entry StartDateEntry;
		private readonly DatePicker StartDatePicker;
		private readonly Entry StartTimeEntry;
		private readonly TimePicker StartTimePicker;
		private readonly Label StartDayName;
		private readonly Button StartDateTimeNowButton;
		private readonly Entry EndDateEntry;
		private readonly DatePicker EndDatePicker;
		private readonly Entry EndTimeEntry;
		private readonly TimePicker EndTimePicker;
		private readonly Label EndDayName;
		private readonly Button EndDateTimeNowButton;

		public MainPage()
		{
			InitializeComponent();

			ListOfSwitches = new List<Switch>()
			{
				  SwitchCalcStartDateTime
				, SwitchCalcYMWDHM
				, SwitchCalcEndDateTime
			};

			ListOfCmbndEntrys = new List<Entry>()
			{
				  CombndYears
				, CombndMonths
				, CombndWeeks
				, CombndDays
				, CombndHours
				, CombndMinutes
			};
			ListOfTotEntrys = new List<Entry>()
			{
				  TotYears
				, TotMonths
				, TotWeeks
				, TotDays
				, TotHours
				, TotMinutes
			};

			DisableYMWDHM(null);
			EnableAndToggleOffAllSwitchedXceptMe(SwitchCalcYMWDHM);

			StartDateIn = DateTime.Today;
			StartTimeIn = DateTime.Now.TimeOfDay;

			EndDateIn = DateTime.Today;
			EndTimeIn = DateTime.Now.TimeOfDay;

			// Start Date/Time
			StartDateEntry = new Entry
			{
				Style = Resources[ "baseStartEndDateTimeEntryStyle" ] as Style,
				Text = DateTime.Today.ToString("d"),
				BindingContext = this
			};
			StartDateEntry.SetBinding(Entry.TextProperty, "StartDateInString", BindingMode.TwoWay);
			StartDateEntry.Completed += OnStartDateEntryCompleted;
			StartDateEntry.Unfocused += OnStartDateEntryCompleted;

			StartTimeEntry = new Entry
			{
				Style = Resources[ "baseStartEndDateTimeEntryStyle" ] as Style,
				Text = DateTime.Now.ToString("HH:mm"),
				BindingContext = this
			};
			StartTimeEntry.SetBinding(Entry.TextProperty, "StartTimeInString", BindingMode.TwoWay);
			StartTimeEntry.Completed += OnStartTimeEntryCompleted;
			StartTimeEntry.Unfocused += OnStartTimeEntryCompleted;

			StartDayName = new Label
			{
				Style = Resources[ "baseStartEndDateTimeEntryLabelStyle" ] as Style,
				Text = " MMM ",
				MinimumWidthRequest = 30,
				LineBreakMode = LineBreakMode.WordWrap
			};

			StartDateTimeNowButton = new Button
			{
				Style = Resources[ "baseButtonStyle" ] as Style
				,
				Text = "Now"
				,
				VerticalOptions = LayoutOptions.Center
			};
			StartDateTimeNowButton.Clicked += OnStartDateTimeNowButtonClicked;

			// End Date/Time
			EndDateEntry = new Entry
			{
				Style = Resources[ "baseStartEndDateTimeEntryStyle" ] as Style,
				Text = DateTime.Today.ToString("d"),
				BindingContext = this
			};
			EndDateEntry.SetBinding(Entry.TextProperty, "EndDateInString", BindingMode.TwoWay);
			EndDateEntry.Completed += OnEndDateEntryCompleted;
			EndDateEntry.Unfocused += OnEndDateEntryCompleted;

			EndTimeEntry = new Entry
			{
				Style = Resources[ "baseStartEndDateTimeEntryStyle" ] as Style,
				Text = DateTime.Now.ToString("HH:mm"),
				BindingContext = this
			};
			EndTimeEntry.SetBinding(Entry.TextProperty, "EndTimeInString", BindingMode.TwoWay);
			EndTimeEntry.Completed += OnEndTimeEntryCompleted;
			EndTimeEntry.Unfocused += OnEndTimeEntryCompleted;

			EndDayName = new Label
			{
				Style = Resources[ "baseStartEndDateTimeEntryLabelStyle" ] as Style,
				Text = " MMM ",
				MinimumWidthRequest = 30,
				LineBreakMode = LineBreakMode.WordWrap
			};

			EndDateTimeNowButton = new Button
			{
				Style = Resources[ "baseButtonStyle" ] as Style
				,
				Text = "Now"
				,
				VerticalOptions = LayoutOptions.Center
			};
			EndDateTimeNowButton.Clicked += OnEndDateTimeNowButtonClicked;

			switch( Device.RuntimePlatform )
			{
				case Device.macOS:
					{

						// Start Date/Time
						// Start Date/Time
						StartDatePicker = new myMacOSDatePicker();
						StartDatePicker.DateSelected += StartDatePicker_DateSelected;

						StartTimePicker = new myMacOSTimePicker();
						StartTimePicker.PropertyChanged += StartTimePicker_PropertyChanged;

						var localStartDateStack = new StackLayout();
						localStartDateStack.Children.Add(StartDateEntry);
						localStartDateStack.Children.Add(StartDatePicker);

						StartDateTimeStack.Children.Add(localStartDateStack);

						var localStartTimeStack = new StackLayout();
						localStartTimeStack.Children.Add(StartTimeEntry);
						localStartTimeStack.Children.Add(StartTimePicker);

						StartDateTimeStack.Children.Add(localStartTimeStack);

						StartDateTimeStack.Children.Add(StartDayName);
						StartDateTimeStack.Children.Add(StartDateTimeNowButton);


						// End Date/Time
						EndDatePicker = new myMacOSDatePicker();
						EndDatePicker.DateSelected += EndDatePicker_DateSelected;

						EndTimePicker = new myMacOSTimePicker();
						EndTimePicker.PropertyChanged += EndTimePicker_PropertyChanged;

						var localEndDateStack = new StackLayout();
						localEndDateStack.Children.Add(EndDateEntry);
						localEndDateStack.Children.Add(EndDatePicker);

						EndDateTimeStack.Children.Add(localEndDateStack);

						var localEndTimeStack = new StackLayout();
						localEndTimeStack.Children.Add(EndTimeEntry);
						localEndTimeStack.Children.Add(EndTimePicker);

						EndDateTimeStack.Children.Add(localEndTimeStack);

						EndDateTimeStack.Children.Add(EndDayName);
						EndDateTimeStack.Children.Add(EndDateTimeNowButton);

						break;
					}
				default:
					{
						// Start Date/Time
						StartDatePicker = new DatePicker();
						StartDatePicker.DateSelected += StartDatePicker_DateSelected;

						StartTimePicker = new TimePicker
						{
							Style = Resources[ "baseTimePickerStyle" ] as Style,
						};
						StartTimePicker.PropertyChanged += StartTimePicker_PropertyChanged;

						StartDateTimeStack.Children.Add(StartDatePicker);
						StartDateTimeStack.Children.Add(StartTimePicker);
						StartDateTimeStack.Children.Add(StartDayName);
						StartDateTimeStack.Children.Add(StartDateTimeNowButton);

						// End Date/Time
						EndDatePicker = new DatePicker();
						EndDatePicker.DateSelected += EndDatePicker_DateSelected;

						EndTimePicker = new TimePicker
						{
							Style = Resources[ "baseTimePickerStyle" ] as Style,
						};
						EndTimePicker.PropertyChanged += EndTimePicker_PropertyChanged;

						EndDateTimeStack.Children.Add(EndDatePicker);
						EndDateTimeStack.Children.Add(EndTimePicker);
						EndDateTimeStack.Children.Add(EndDayName);
						EndDateTimeStack.Children.Add(EndDateTimeNowButton);

						switch( Device.RuntimePlatform )
						{
							case Device.UWP:
								{
									StartDatePicker.Style = Resources[ "baseDatePickerStyle_WO_WidthRequest" ] as Style;
									EndDatePicker.Style = Resources[ "baseDatePickerStyle_WO_WidthRequest" ] as Style;
									break;
								}
							case Device.Android:
							case Device.iOS:
								{
									StartDatePicker.Style = Resources[ "baseDatePickerStyle_W_WidthRequest" ] as Style;
									EndDatePicker.Style = Resources[ "baseDatePickerStyle_W_WidthRequest" ] as Style;
									break;
								}
						}

						break;
					}
			}


			if( (Device.RuntimePlatform == Device.UWP) )
			{
				ScreenWidth = DependencyService.Get<IScreenSizeInterface>().GetScreenWidth();
				ScreenHeight = DependencyService.Get<IScreenSizeInterface>().GetScreenHeight();
			}



			StartDatePicker.MinimumDate = DateTime.MinValue;
			StartDatePicker.MaximumDate = DateTime.MaxValue;
			EndDatePicker.MinimumDate = DateTime.MinValue;
			EndDatePicker.MaximumDate = DateTime.MaxValue;
		}

		protected override void OnSizeAllocated(double width, double height)
		{
			if( firstTime )
			{
				DoClearAll();
				firstTime = false;
			}

			base.OnSizeAllocated(width, height);


			if( (Device.RuntimePlatform == Device.UWP) )
			{
				ScreenWidth = DependencyService.Get<IScreenSizeInterface>().GetScreenWidth();
				ScreenHeight = DependencyService.Get<IScreenSizeInterface>().GetScreenHeight();
			}
			else
			{
				ScreenWidth = width;
				ScreenHeight = height;
			}

			if( width != this.width || height != this.height )
			{

				this.width = width;
				this.height = height;

				//TotalStackName.Scale = 1.0f;
				TotalStackName.TranslationX = 0.0f;
				TotalStackName.TranslationY = 0.0f;

				double widthAndHightScale;


				if( firstTimeWdthOrHeightChanged )
				{
					StartDateTimeIntroLabelNameFontSizeOrig = StartDateTimeIntroLabelName.FontSize;
					StartEndDayNameFontSizeOrig = StartDayName.FontSize;
					firstTimeWdthOrHeightChanged = false;
				}

				// Get Metrics
				var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

				// Orientation (Landscape, Portrait, Square, Unknown)
				var orientation = mainDisplayInfo.Orientation;

				// Rotation (0, 90, 180, 270)
				var rotation = mainDisplayInfo.Rotation;

				// Width (in pixels)
				var mainWidth = mainDisplayInfo.Width;

				// Height (in pixels)
				var mainHeight = mainDisplayInfo.Height;

				bool portrait = (mainDisplayInfo.Orientation == DisplayOrientation.Portrait);
				
				if( portrait )
				{ // Portrait
					if
					(
						   (Device.RuntimePlatform == Device.macOS)
						|| (Device.RuntimePlatform == Device.UWP)
						|| ((Device.RuntimePlatform == Device.Android) && (mainHeight < 1920))
						//|| ((Device.RuntimePlatform == Device.Android) && (height < 659))
						//|| ((Device.RuntimePlatform == Device.iOS) && (mainWidth < 414))
						//|| ((Device.RuntimePlatform == Device.iOS) && (width < 414))
					)
					{ // Only Landscape allowed
						entriesOuterStack.Orientation = StackOrientation.Vertical;
						CombndTimeEntriesStack.Orientation = StackOrientation.Horizontal;
						TotalTimeEntriesStack.Orientation = StackOrientation.Horizontal;
						scrollViewName.Orientation = ScrollOrientation.Horizontal;
						MessagingCenter.Send((App)Xamarin.Forms.Application.Current, MessengerKeys.LandscapeOrientationRequest);
					}
					else
					{
						entriesOuterStack.Orientation = StackOrientation.Horizontal;
						CombndTimeEntriesStack.Orientation = StackOrientation.Vertical;
						TotalTimeEntriesStack.Orientation = StackOrientation.Vertical;
						scrollViewName.Orientation = ScrollOrientation.Vertical;
					}
				}
				else
				{ // Landscape
					entriesOuterStack.Orientation = StackOrientation.Vertical;
					CombndTimeEntriesStack.Orientation = StackOrientation.Horizontal;
					TotalTimeEntriesStack.Orientation = StackOrientation.Horizontal;
					scrollViewName.Orientation = ScrollOrientation.Horizontal;
				}

				switch( Device.RuntimePlatform )
				{
					//case Device.macOS:
					//	{
					//		TotalStackName.Scale = 1.0f;
					//		StartDateTimeIntroLabelName.FontSize = StartDateTimeIntroLabelNameFontSizeOrig;
					//		EndDateTimeIntroLabelName.FontSize = StartDateTimeIntroLabelNameFontSizeOrig;
					//		TotalStackName.TranslationX = 0;
					//		TotalStackName.TranslationY = 0;
					//		scrollViewName.WidthRequest = nativeTotalStackWidthLandscape + 50;
					//		scrollViewName.ScrollToAsync(TotalStackName, ScrollToPosition.Center, true);
					//		break;
					//	}
					case Device.Android:
						{
							if( portrait ) // Portrait ?
							{ // Portrait
								if( height > nativeTotalStackHeightPortrait )
								{
									ContentPageName.Scale = height / nativeTotalStackHeightPortrait;
								}
							}
							else
							{ // Landscape
								if( width > nativeTotalStackWidthLandscape )
								{
									ContentPageName.Scale = width / nativeTotalStackWidthLandscape;
								}
								else if( width < 659 )
								{
									TotalStackName.Scale = TotalStackName.Width / nativeTotalStackWidthLandscape;
									StartDateTimeStacAndPlus.Scale = 0.7f;
									EndDateTimeAndCalculateAndClearAllButtonsStackName.Scale = 0.65f;
								}
							}
							scrollViewName.ScrollToAsync(TotalStackName, ScrollToPosition.Center, false);

							StartDayName.WidthRequest = EndDayName.WidthRequest = 50;

							break;
						}
					case Device.iOS:
						{
							if( portrait ) // Portrait ?
							{ // Portrait
								if( height > nativeTotalStackHeightPortrait )
								{
									if( ((width >= 414) && (height <= 736)) || (height > 896) )
									{
										ContentPageName.Scale = height / nativeTotalStackHeightPortrait;
									}
									else
									{
										ContentPageName.Scale = height / (nativeTotalStackHeightPortrait * 1.15);
									}
								}
							}
							else
							{ // Landscape
								if( width > nativeTotalStackWidthLandscape )
								{
									ContentPageName.Scale = width / nativeTotalStackWidthLandscape;
								}
								else if( height < 370 )
								{
									TotalStackName.Scale = TotalStackName.Width / (nativeTotalStackWidthLandscape * 1.15f);
									StartDateTimeStacAndPlus.Scale = 0.85f;
									entriesOuterGrid.Scale = 0.82f;
									EndDateTimeAndCalculateAndClearAllButtonsStackName.Scale = 0.8f;
								}
								else if( height < 414 )
								{
									TotalStackName.Scale = TotalStackName.Width / (nativeTotalStackWidthLandscape * 1.18f);
								}
							}
							scrollViewName.ScrollToAsync(TotalStackName, ScrollToPosition.Center, true);

							//StartDayName.WidthRequest = EndDayName.WidthRequest = 50;

							break;
						}
					case Device.UWP:
						{
							if( DependencyService.Get<IPlatformInterface>().IsMobile() )
							{
								if( portrait ) // Portrait ?
								{ // Portrait
									if( height <= nativeTotalStackHeightPortrait ) // Need scaling ?
									{
										TotalStackName.Scale = widthAndHightScale =
											-(2.7410270192276622436e-009 * Math.Pow(ScreenHeight, 3))
											+ (4.7754782031987597521e-006 * Math.Pow(ScreenHeight, 2))
											- (0.0013991090738610563200 * ScreenHeight)
											+ 0.49946777681408938143;

										TotalStackName.TranslationX = 0;
										TotalStackName.TranslationY =
											(3.3707997844973771142e-005 * Math.Pow(ScreenHeight, 3))
											- (0.066636967320806955728 * Math.Pow(ScreenHeight, 2))
											+ (43.568112848719657393 * ScreenHeight)
											- 9425.4397956508601055;

										StartDateTimeIntroLabelName.FontSize = EndDateTimeIntroLabelName.FontSize
												= StartDateTimeIntroLabelNameFontSizeOrig * widthAndHightScale / 1.5;
										StartDayName.FontSize = EndDayName.FontSize = StartEndDayNameFontSizeOrig * widthAndHightScale /*/ 1.5*/;
									}
								}
								else
								{ // Landscape
									if( width <= nativeTotalStackWidthLandscape ) // Need scaling ?
									{
										TotalStackName.Scale =
											-(1.0433447427359796688e-007 * Math.Pow(ScreenWidth, 3))
											+ (0.00020154923472775974880 * Math.Pow(ScreenWidth, 2))
											- (0.12705258908531044670 * ScreenWidth)
											+ 26.859746894086349300;

										//TotalStackName.TranslationX = 0;
										TotalStackName.TranslationX =
											+(6.0103507005254339091e-005 * Math.Pow(ScreenWidth, 3))
											- (0.11838955202431701574 * Math.Pow(ScreenWidth, 2))
											+ (77.536187041332297554 * ScreenWidth)
											- 16935.307290964530694;
										TotalStackName.TranslationY = 0;
									}
								}
								scrollViewName.ScrollToAsync(TotalStackName, ScrollToPosition.Start, true);
							}
							//else
							//{ // NOT Mobile
							//	if (portrait) // Portrait ?
							//	{ // Portrait
							//		if (height <= nativeTotalStackHeightPortrait) // Need scaling ?
							//		{
							//			TotalStackName.Scale = widthAndHightScale = height / nativeTotalStackHeightPortrait;

							//			//StartDateTimeIntroLabelName.FontSize = EndDateTimeIntroLabelName.FontSize
							//			//		= StartDateTimeIntroLabelNameFontSizeOrig * widthAndHightScale / 1.5;
							//			//StartDayName.FontSize = EndDayName.FontSize = StartEndDayNameFontSizeOrig * widthAndHightScale /*/ 1.5*/;
							//		}
							//	}
							//	else
							//	{ // Landscape
							//		if (width <= nativeTotalStackWidthLandscape) // Need scaling ?
							//		{
							//			TotalStackName.Scale = widthAndHightScale = width / nativeTotalStackWidthLandscape;
							//		}
							//	}
							//	scrollViewName.ScrollToAsync(TotalStackName, ScrollToPosition.Center, true);

							//}

							StartDayName.WidthRequest = EndDayName.WidthRequest = 45;
							break;
						}
					default:
						{
							break;
						}
				}
			}
		}



		// Start date-time...

		private void CalcStartDateSwitch_Toggled(object sender, ToggledEventArgs e)
		{
			CalcStartDateSwitchIsOn = e.Value;

			if( CalcStartDateSwitchIsOn )
			{
				StartDateEntry.IsReadOnly = true;
				StartDatePicker.IsEnabled = false;
				StartTimeEntry.IsReadOnly = true;
				StartTimePicker.IsEnabled = false;
				StartDateTimeNowButton.IsEnabled = false;

				DoClearAll();
				EnableAndToggleOffAllSwitchedXceptMe((Switch)sender);

				LabelEqual.Text = "-";
				LabelPlus.Text = "=";
			}
			else
			{
				StartDateEntry.IsReadOnly = false;
				StartDatePicker.IsEnabled = true;
				StartTimeEntry.IsReadOnly = false;
				StartTimePicker.IsEnabled = true;
				StartDateTimeNowButton.IsEnabled = true;
			}
		}

		private void CheckSetEndDateTime()
		{
			if( EndDateIn < StartDateIn )
			{
				EndDateIn = StartDateIn;
				EndTimeIn = StartTimeIn;
				SetEndDateTime();
			}
			else
			{
				if( (EndDateIn == StartDateIn) && (EndTimeIn < StartTimeIn) )
				{
					EndTimeIn = StartTimeIn;
					SetEndDateTime();
				}
			}
		}

		private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
		{
			StartDateIn = e.NewDate;

			StartDateEntry.SetBinding(Entry.TextProperty, "StartDateInString", BindingMode.TwoWay);

			StartDayName.Text = StartDateIn.DayOfWeek.ToString().Remove(3);

			CheckSetEndDateTime();
		}

		private void OnStartDateEntryCompleted(object sEnder, EventArgs args)
		{
			StartDateInString = StartDateEntry.Text;

			SetStartDateTime();

			CheckSetEndDateTime();
		}

		private void OnStartTimeEntryCompleted(object sEnder, EventArgs args)
		{
			StartTimeInString = StartTimeEntry.Text;

			SetStartDateTime();

			CheckSetEndDateTime();
		}

		private void StartTimePicker_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if( e.PropertyName == "Time" )
			{
				StartTimeIn = StartTimePicker.Time;

				StartTimeEntry.SetBinding(Entry.TextProperty, "StartTimeInString", BindingMode.TwoWay);

				CheckSetEndDateTime();
			}
		}

		private void OnStartDateTimeNowButtonClicked(object sEnder, EventArgs args)
		{
			StartDateIn = DateTime.Today;
			StartTimeIn = DateTime.Now.TimeOfDay;

			SetStartDateTime();

			CheckSetEndDateTime();
		}


		//FROM HERE Combined
		//Combined Years...

		private void OnCombndYearsFocused(object sEnder, EventArgs args)
		{
			CombndYearsIn = 0;
		}

		private void OnCombndYearsUnfocused(object sEnder, EventArgs args)
		{
			OnCombndYearsCompleted(sEnder, args);
		}

		private async void OnCombndYearsCompleted(object sEnder, EventArgs args)
		{
			if( (CombndYears.Text.Length != 0) && !int.TryParse(CombndYears.Text, out CombndYearsIn) )
			{
				CombndYearsIn = 0;
				var TextHolder = CombndYears.Text;
				CombndYears.Text = "";
				await DisplayAlert("Invalid \"Combined Years\" ", TextHolder, "OK");
				CombndYears.Focus();
			}
		}


		//Combined Months...

		private void OnCombndMonthsFocused(object sEnder, EventArgs args)
		{
			CombndMonthsIn = 0;
		}

		private void OnCombndMonthsUnfocused(object sEnder, EventArgs args)
		{
			OnCombndMonthsCompleted(sEnder, args);
		}

		private async void OnCombndMonthsCompleted(object sEnder, EventArgs args)
		{
			if( (CombndMonths.Text.Length != 0) && !int.TryParse(CombndMonths.Text, out CombndMonthsIn) )
			{
				CombndMonthsIn = 0;
				var TextHolder = CombndMonths.Text;
				CombndMonths.Text = "";
				await DisplayAlert("Invalid \"Combined Months\" ", TextHolder, "OK");
				CombndMonths.Focus();
			}
		}


		//Combined Weeks...

		private void OnCombndWeeksFocused(object sEnder, EventArgs args)
		{
			CombndWeeksIn = 0;
		}

		private void OnCombndWeeksUnfocused(object sEnder, EventArgs args)
		{
			OnCombndWeeksCompleted(sEnder, args);
		}

		private async void OnCombndWeeksCompleted(object sEnder, EventArgs args)
		{
			if( (CombndWeeks.Text.Length != 0) && !int.TryParse(CombndWeeks.Text, out CombndWeeksIn) )
			{
				CombndWeeksIn = 0;
				var TextHolder = CombndWeeks.Text;
				CombndWeeks.Text = "";
				await DisplayAlert("Invalid \"Combined Weeks\" ", TextHolder, "OK");
				CombndWeeks.Focus();
			}
		}


		//Combined Days...

		private void OnCombndDaysFocused(object sEnder, EventArgs args)
		{
			CombndDaysIn = 0;
		}

		private void OnCombndDaysUnfocused(object sEnder, EventArgs args)
		{
			OnCombndDaysCompleted(sEnder, args);
		}

		private async void OnCombndDaysCompleted(object sEnder, EventArgs args)
		{
			if( (CombndDays.Text.Length != 0) && !int.TryParse(CombndDays.Text, out CombndDaysIn) )
			{
				CombndDaysIn = 0;
				var TextHolder = CombndDays.Text;
				CombndDays.Text = "";
				await DisplayAlert("Invalid \"Combined Days\" ", TextHolder, "OK");
				CombndDays.Focus();
			}
		}


		//Combined Hours...

		private void OnCombndHoursFocused(object sEnder, EventArgs args)
		{
			CombndHoursIn = 0;
		}

		private void OnCombndHoursUnfocused(object sEnder, EventArgs args)
		{
			OnCombndHoursCompleted(sEnder, args);
		}

		private async void OnCombndHoursCompleted(object sEnder, EventArgs args)
		{
			if( (CombndHours.Text.Length != 0) && !int.TryParse(CombndHours.Text, out CombndHoursIn) )
			{
				CombndHoursIn = 0;
				var TextHolder = CombndHours.Text;
				CombndHours.Text = "";
				await DisplayAlert("Invalid \"Combined Hours\" ", TextHolder, "OK");
				CombndHours.Focus();
			}
		}


		//Combined Minutes...

		private void OnCombndMinutesFocused(object sEnder, EventArgs args)
		{
			CombndMinutesIn = 0;
		}

		private void OnCombndMinutesUnfocused(object sEnder, EventArgs args)
		{
			OnCombndMinutesCompleted(sEnder, args);
		}

		private async void OnCombndMinutesCompleted(object sEnder, EventArgs args)
		{
			if( (CombndMinutes.Text.Length != 0) && !int.TryParse(CombndMinutes.Text, out CombndMinutesIn) )
			{
				CombndMinutesIn = 0;
				var TextHolder = CombndMinutes.Text;
				CombndMinutes.Text = "";
				await DisplayAlert("Invalid \"Combined Minutes\" ", TextHolder, "OK");
				CombndMinutes.Focus();
			}
		}
		//TO HERE Combined


		//FROM HERE Total
		//Total Years...

		private void OnTotYearsFocused(object sEnder, EventArgs args)
		{
			TotYearsIn = 0;
		}

		private void OnTotYearsUnfocused(object sEnder, EventArgs args)
		{
			OnTotYearsCompleted(sEnder, args);
		}

		private async void OnTotYearsCompleted(object sEnder, EventArgs args)
		{
			if( (TotYears.Text.Length != 0) && !Int32.TryParse(TotYears.Text, out TotYearsIn) )
			{
				TotYearsIn = 0;
				var TextHolder = TotYears.Text;
				TotYears.Text = "";
				await DisplayAlert("Invalid \"Total Years\" ", TextHolder, "OK");
				TotYears.Focus();
			}
		}


		//Total Months...

		private void OnTotMonthsFocused(object sEnder, EventArgs args)
		{
			TotMonthsIn = 0;
		}

		private void OnTotMonthsUnfocused(object sEnder, EventArgs args)
		{
			OnTotMonthsCompleted(sEnder, args);
		}

		private async void OnTotMonthsCompleted(object sEnder, EventArgs args)
		{
			if( (TotMonths.Text.Length != 0) && !int.TryParse(TotMonths.Text, out TotMonthsIn) )
			{
				TotMonthsIn = 0;
				var TextHolder = TotMonths.Text;
				TotMonths.Text = "";
				await DisplayAlert("Invalid \"Total Months\" ", TextHolder, "OK");
				TotMonths.Focus();
			}
		}


		//Total Weeks...

		private void OnTotWeeksFocused(object sEnder, EventArgs args)
		{
			TotWeeksIn = 0;
		}

		private void OnTotWeeksUnfocused(object sEnder, EventArgs args)
		{
			OnTotWeeksCompleted(sEnder, args);
		}

		private async void OnTotWeeksCompleted(object sEnder, EventArgs args)
		{
			if( (TotWeeks.Text.Length != 0) && !int.TryParse(TotWeeks.Text, out TotWeeksIn) )
			{
				TotWeeksIn = 0;
				var TextHolder = TotWeeks.Text;
				TotWeeks.Text = "";
				await DisplayAlert("Invalid \"Total Weeks\" ", TextHolder, "OK");
				TotWeeks.Focus();
			}
		}


		//Total Days...

		private void OnTotDaysFocused(object sEnder, EventArgs args)
		{
			TotDaysIn = 0;
		}

		private void OnTotDaysUnfocused(object sEnder, EventArgs args)
		{
			OnTotDaysCompleted(sEnder, args);
		}

		private async void OnTotDaysCompleted(object sEnder, EventArgs args)
		{
			if( (TotDays.Text.Length != 0) && !int.TryParse(TotDays.Text, out TotDaysIn) )
			{
				TotDaysIn = 0;
				var TextHolder = TotDays.Text;
				TotDays.Text = "";
				await DisplayAlert("Invalid \"Total Days\" ", TextHolder, "OK");
				TotDays.Focus();
			}
		}


		//Total Hours...

		private void OnTotHoursFocused(object sEnder, EventArgs args)
		{
			TotHoursIn = 0;
		}

		private void OnTotHoursUnfocused(object sEnder, EventArgs args)
		{
			OnTotHoursCompleted(sEnder, args);
		}

		private async void OnTotHoursCompleted(object sEnder, EventArgs args)
		{
			if( (TotHours.Text.Length != 0) && !int.TryParse(TotHours.Text, out TotHoursIn) )
			{
				TotHoursIn = 0;
				var TextHolder = TotHours.Text;
				TotHours.Text = "";
				await DisplayAlert("Invalid \"Total Hours\" ", TextHolder, "OK");
				TotHours.Focus();
			}
		}


		//Total Minutes...

		private void OnTotMinutesFocused(object sEnder, EventArgs args)
		{
			TotMinutesIn = 0;
		}

		private void OnTotMinutesUnfocused(object sEnder, EventArgs args)
		{
			OnTotMinutesCompleted(sEnder, args);
		}

		private async void OnTotMinutesCompleted(object sEnder, EventArgs args)
		{
			if( (TotMinutes.Text.Length != 0) && !int.TryParse(TotMinutes.Text, out TotMinutesIn) )
			{
				TotMinutesIn = 0;
				var TextHolder = TotMinutes.Text;
				TotMinutes.Text = "";
				await DisplayAlert("Invalid \"Total Minutes\" ", TextHolder, "OK");
				TotMinutes.Focus();
			}
		}
		//TO HERE Total


		// End date-time... 

		private void CalcEndDateSwitch_Toggled(object sender, ToggledEventArgs e)
		{
			CalcEndDateSwitchIsOn = e.Value;

			if( CalcEndDateSwitchIsOn )
			{
				EndDateEntry.IsReadOnly = true;
				EndDatePicker.IsEnabled = false;
				EndTimeEntry.IsReadOnly = true;
				EndTimePicker.IsEnabled = false;
				EndDateTimeNowButton.IsEnabled = false;

				DoClearAll();
				EnableAndToggleOffAllSwitchedXceptMe((Switch)sender);

				LabelEqual.Text = "=";
				LabelPlus.Text = "+";

			}
			else
			{
				EndDateEntry.IsReadOnly = false;
				EndDatePicker.IsEnabled = true;
				EndTimeEntry.IsReadOnly = false;
				EndTimePicker.IsEnabled = true;
				EndDateTimeNowButton.IsEnabled = true;
			}
		}


		private void CheckSetStartDateTime()
		{
			if( StartDateIn > EndDateIn )
			{
				StartDateIn = EndDateIn;
				StartTimeIn = EndTimeIn;
				SetStartDateTime();
			}
			else
			{
				if( (StartDateIn == EndDateIn) && (StartTimeIn > EndTimeIn) )
				{
					StartTimeIn = EndTimeIn;
					SetStartDateTime();
				}
			}
		}

		private void EndDatePicker_DateSelected(object sender, DateChangedEventArgs e)
		{
			EndDateIn = e.NewDate;

			EndDateEntry.SetBinding(Entry.TextProperty, "EndDateInString", BindingMode.TwoWay);

			EndDayName.Text = EndDateIn.DayOfWeek.ToString().Remove(3);

			CheckSetStartDateTime();
		}

		private void OnEndDateEntryCompleted(object sEnder, EventArgs args)
		{
			EndDateInString = EndDateEntry.Text;

			SetEndDateTime();

			CheckSetStartDateTime();
		}

		private void OnEndTimeEntryCompleted(object sEnder, EventArgs args)
		{
			EndTimeInString = EndTimeEntry.Text;

			SetEndDateTime();

			CheckSetStartDateTime();
		}

		private void EndTimePicker_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if( e.PropertyName == "Time" )
			{
				EndTimeIn = EndTimePicker.Time;

				EndTimeEntry.SetBinding(Entry.TextProperty, "EndTimeInString", BindingMode.TwoWay);

				CheckSetStartDateTime();
			}
		}

		private void OnEndDateTimeNowButtonClicked(object sEnder, EventArgs args)
		{
			EndDateIn = DateTime.Today;
			EndTimeIn = DateTime.Now.TimeOfDay;

			SetEndDateTime();

			CheckSetStartDateTime();
		}


		private void OnClearAllButtonClicked(object sEnder, EventArgs args)
		{
			DoClearAll();
		}



		// CALCULATION from here...

		private async void CalcAndShowTimeSpans()
		{
			CombndYearsOut = EndDateTimeIn.Year - StartDateTimeIn.Year;
			CombndMonthsOut = EndDateTimeIn.Month - StartDateTimeIn.Month;
			//if (EndDateTimeIn.Day < StartDateTimeIn.Day)
			//{
			//	CombndMonthsOut--;
			//}
			if( CombndMonthsOut < 0 )
			{
				CombndMonthsOut += 12;
				CombndYearsOut--;
			}
			DateTime dtCalc1 = StartDateTimeIn;
			dtCalc1 = dtCalc1.AddYears(CombndYearsOut);
			dtCalc1 = dtCalc1.AddMonths(CombndMonthsOut);
			TimeSpan ts1 = dtCalc1 - StartDateTimeIn; // Total Days in years + months
			TimeSpan ts2 = EndDateTimeIn - StartDateTimeIn; // Total Days in whole time span
			CombndDaysOut = ts2.Days - ts1.Days;
			if( CombndDaysOut < 0 )
			{
				CombndMonthsOut--;
				if( CombndMonthsOut < 0 )
				{
					CombndMonthsOut += 12;
					CombndYearsOut--;
				}
				DateTime dtCalc2 = StartDateTimeIn.AddYears(CombndYearsOut).AddMonths(CombndMonthsOut);
				ts1 = dtCalc2 - StartDateTimeIn; // Total Days in years + months
				CombndDaysOut = ts2.Days - ts1.Days;
			}
			CombndHoursOut = ts2.Hours; // Extra days besides Days in years + months
			CombndMinutesOut = ts2.Minutes; // Extra days besides Days in years + months

			CombndWeeksOut = (int)(CombndDaysOut / 7);
			CombndDaysOut %= 7; // Rest after div. w. 7

			TotDaysOut = (Int64)ts2.TotalDays;
			TotWeeksOut = (Int64)(TotDaysOut / 7);
			TotMonthsOut = CombndMonthsOut + 12 * CombndYearsOut;
			TotYearsOut = CombndYearsOut;
			TotHoursOut = (Int64)ts2.TotalHours;
			TotMinutesOut = (Int64)ts2.TotalMinutes;

			// Show Combnd in the text boxes
			CombndDays.Text = CombndDaysOut.ToString();
			CombndWeeks.Text = CombndWeeksOut.ToString();
			CombndMonths.Text = CombndMonthsOut.ToString();
			CombndYears.Text = CombndYearsOut.ToString();
			CombndHours.Text = CombndHoursOut.ToString();
			CombndMinutes.Text = CombndMinutesOut.ToString();

			// Show Tot. in the text boxes
			TotDays.Text = TotDaysOut.ToString();
			if( TotDaysOut > 9999999999 )
			{
				await DisplayAlert("Total \"Days\" > 9999999999", TotDays.ToString(), "OK");
			}
			TotWeeks.Text = TotWeeksOut.ToString();
			TotMonths.Text = TotMonthsOut.ToString();
			TotYears.Text = TotYearsOut.ToString();
			TotHours.Text = TotHoursOut.ToString();
			if( TotHoursOut > 9999999999 )
			{
				await DisplayAlert("Total \"Hours\" > 9999999999", TotHours.ToString(), "OK");
			}
			TotMinutes.Text = TotMinutesOut.ToString();
			if( TotMinutesOut > 9999999999 )
			{
				await DisplayAlert("Total \"Minutes\" > 9999999999", TotMinutes.ToString(), "OK");
			}
		}

		private async void OnCalculateButtonClicked(object sEnder, EventArgs e)
		{
			CalculateButton.Focus();

			// Input values
			EndDateTimeIn = EndDateIn + EndTimeIn;
			StartDateTimeIn = StartDateIn + StartTimeIn;

			// Output values
			StartDateTimeOut = DateTime.MaxValue;
			EndDateTimeOut = DateTime.MaxValue;

			if( CalcYMWDHMIsOn )
			{
				CalcAndShowTimeSpans();
			}
			else
			{ // !CalcYMWDHMIsOn
			  // Read all controls
			  // Combined
				if( (CombndYears.Text.Length != 0) && !int.TryParse(CombndYears.Text, out CombndYearsIn) )
				{
					CombndYearsIn = 0;
					var TextHolder = CombndYears.Text;
					CombndYears.Text = "";
					await DisplayAlert("Invalid \"Combined Years\" ", TextHolder, "OK");
					CombndYears.Focus();
				}
				if( (CombndMonths.Text.Length != 0) && !int.TryParse(CombndMonths.Text, out CombndMonthsIn) )
				{
					CombndMonthsIn = 0;
					var TextHolder = CombndMonths.Text;
					CombndMonths.Text = "";
					await DisplayAlert("Invalid \"Combined Months\" ", TextHolder, "OK");
					CombndMonths.Focus();
				}
				if( (CombndWeeks.Text.Length != 0) && !int.TryParse(CombndWeeks.Text, out CombndWeeksIn) )
				{
					CombndWeeksIn = 0;
					var TextHolder = CombndWeeks.Text;
					CombndWeeks.Text = "";
					await DisplayAlert("Invalid \"Combined Weeks\" ", TextHolder, "OK");
					CombndWeeks.Focus();
				}
				if( (CombndDays.Text.Length != 0) && !int.TryParse(CombndDays.Text, out CombndDaysIn) )
				{
					CombndDaysIn = 0;
					var TextHolder = CombndDays.Text;
					CombndDays.Text = "";
					await DisplayAlert("Invalid \"Combined Days\" ", TextHolder, "OK");
					CombndDays.Focus();
				}
				if( (CombndHours.Text.Length != 0) && !int.TryParse(CombndHours.Text, out CombndHoursIn) )
				{
					CombndHoursIn = 0;
					var TextHolder = CombndHours.Text;
					CombndHours.Text = "";
					await DisplayAlert("Invalid \"Combined Hours\" ", TextHolder, "OK");
					CombndHours.Focus();
				}
				if( (CombndMinutes.Text.Length != 0) && !int.TryParse(CombndMinutes.Text, out CombndMinutesIn) )
				{
					CombndMinutesIn = 0;
					var TextHolder = CombndMinutes.Text;
					CombndMinutes.Text = "";
					await DisplayAlert("Invalid \"Combined Minutes\" ", TextHolder, "OK");
					CombndMinutes.Focus();
				}
				// Total
				if( (TotYears.Text.Length != 0) && !Int32.TryParse(TotYears.Text, out TotYearsIn) )
				{
					TotYearsIn = 0;
					var TextHolder = TotYears.Text;
					TotYears.Text = "";
					await DisplayAlert("Invalid \"Total Years\" ", TextHolder, "OK");
					TotYears.Focus();
				}
				if( (TotMonths.Text.Length != 0) && !Int32.TryParse(TotMonths.Text, out TotMonthsIn) )
				{
					TotMonthsIn = 0;
					var TextHolder = TotMonths.Text;
					TotMonths.Text = "";
					await DisplayAlert("Invalid \"Total Months\" ", TextHolder, "OK");
					TotMonths.Focus();
				}
				if( (TotWeeks.Text.Length != 0) && !Int32.TryParse(TotWeeks.Text, out TotWeeksIn) )
				{
					TotWeeksIn = 0;
					var TextHolder = TotWeeks.Text;
					TotWeeks.Text = "";
					await DisplayAlert("Invalid \"Total Weeks\" ", TextHolder, "OK");
					TotWeeks.Focus();
				}
				if( (TotDays.Text.Length != 0) && !Int32.TryParse(TotDays.Text, out TotDaysIn) )
				{
					TotDaysIn = 0;
					var TextHolder = TotDays.Text;
					TotDays.Text = "";
					await DisplayAlert("Invalid \"Total Days\" ", TextHolder, "OK");
					TotDays.Focus();
				}
				if( (TotHours.Text.Length != 0) && !Int32.TryParse(TotHours.Text, out TotHoursIn) )
				{
					TotHoursIn = 0;
					var TextHolder = TotHours.Text;
					TotHours.Text = "";
					await DisplayAlert("Invalid \"Total Hours\" ", TextHolder, "OK");
					TotHours.Focus();
				}
				if( (TotMinutes.Text.Length != 0) && !Int32.TryParse(TotMinutes.Text, out TotMinutesIn) )
				{
					TotMinutesIn = 0;
					var TextHolder = TotMinutes.Text;
					TotMinutes.Text = "";
					await DisplayAlert("Invalid \"Total Minutes\" ", TextHolder, "OK");
					TotMinutes.Focus();
				}
			} // if (CalcYMWDHMIsOn) ..else


			if( CalcEndDateSwitchIsOn )
			{ // CalcEndDateSwitchIsOn = true
				bool TotChk = (TotYearsIn == 0) &&
								  (TotMonthsIn == 0) &&
								  (TotWeeksIn == 0) &&
								  (TotDaysIn == 0) &&
								  (TotHoursIn == 0) &&
								  (TotMinutesIn == 0);

				bool CombndChk = (CombndYearsIn == 0) &&
								 (CombndMonthsIn == 0) &&
								 (CombndWeeksIn == 0) &&
								 (CombndDaysIn == 0) &&
								 (CombndHoursIn == 0) &&
								 (CombndMinutesIn == 0);

				if( !TotChk || !CombndChk )
				{
					if( TotChk || CombndChk )
					{
						EndDateTimeOut = DateTime.MaxValue; // <=> no EndDateTimeOut found

						if( !TotChk )
						{
							if( TotYearsIn != 0 )
							{
								if( (TotMonthsIn == 0) &&
									(TotWeeksIn == 0) &&
									(TotDaysIn == 0) &&
									(TotHoursIn == 0) &&
									(TotMinutesIn == 0) )
								{
									try
									{
										EndDateTimeOut = StartDateTimeIn.AddYears(TotYearsIn);
									}
									catch( ArgumentOutOfRangeException outOfRange )
									{
										await DisplayAlert
										   (
											   "Argument Out Of Range"
											   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Total Years\" added = " + TotYearsIn.ToString()
											   + ".\r\nDate+Time Max. Value is " + DateTime.MaxValue.ToString("u").Remove(16)
											   , "OK"
										   );
										TotYearsIn = 0;
										TotYears.Text = "";
										TotYears.Focus();
										return;
									}
								}
								else
								{
									await DisplayAlert
									   (
										   "Type error"
										   , "Only one \"Total\" value allowed"
										   , "OK"
									   );
								}
							} // if (TotYearsIn != 0)
							else
							{
								if( TotMonthsIn != 0 )
								{
									if( (TotWeeksIn == 0) &&
										(TotDaysIn == 0) &&
										(TotHoursIn == 0) &&
										(TotMinutesIn == 0) )
									{
										try
										{
											EndDateTimeOut = StartDateTimeIn.AddMonths(TotMonthsIn);
										}
										catch( ArgumentOutOfRangeException outOfRange )
										{
											await DisplayAlert
											   (
												   "Argument Out Of Range"
												   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Total Months\" added = " + TotMonthsIn.ToString()
												   + ".\r\nDate+Time Max. Value is " + DateTime.MaxValue.ToString("u").Remove(16)
												   , "OK"
											   );
											TotMonthsIn = 0;
											TotMonths.Text = "";
											TotMonths.Focus();
											return;
										}
									}
									else
									{
										await DisplayAlert
										   (
											   "Type error"
											   , "Only one \"Total\" value allowed"
											   , "OK"
										   );
									}
								} // if (TotMonthsIn != 0)
								else
								{
									if( TotWeeksIn != 0 )
									{
										if( (TotDaysIn == 0) &&
										   (TotHoursIn == 0) &&
										   (TotMinutesIn == 0) )
										{
											try
											{
												EndDateTimeOut = StartDateTimeIn.AddDays(TotWeeksIn * 7);
											}
											catch( ArgumentOutOfRangeException outOfRange )
											{
												await DisplayAlert
												   (
													   "Argument Out Of Range"
													   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Total Weeks\" added = " + TotWeeksIn.ToString()
													   + ".\r\nDate+Time Max. Value is " + DateTime.MaxValue.ToString("u").Remove(16)
													   , "OK"
												   );
												TotWeeksIn = 0;
												TotWeeks.Text = "";
												TotWeeks.Focus();
												return;
											}
										}
										else
										{
											await DisplayAlert
											   (
												   "Type error"
												   , "Only one \"Total\" value allowed"
												   , "OK"
											   );
										}
									} // if (TotWeeksIn != 0)
									else
									{
										if( TotDaysIn != 0 )
										{
											if( (TotHoursIn == 0) &&
												(TotMinutesIn == 0) )
											{
												try
												{
													EndDateTimeOut = StartDateTimeIn.AddDays(TotDaysIn);
												}
												catch( ArgumentOutOfRangeException outOfRange )
												{
													await DisplayAlert
													   (
														   "Argument Out Of Range"
														   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Total Days\" added = " + TotDaysIn.ToString()
														   + ".\r\nDate+Time Max. Value is " + DateTime.MaxValue.ToString("u").Remove(16)
														   , "OK"
													   );
													TotDaysIn = 0;
													TotDays.Text = "";
													TotDays.Focus();
													return;
												}
											}
											else
											{
												await DisplayAlert
												   (
													   "Type error"
													   , "Only one \"Total\" value allowed"
													   , "OK"
												   );
											}
										} // if (TotDaysIn != 0)
										else
										{
											if( TotHoursIn != 0 )
											{
												if( TotMinutesIn == 0 )
												{
													try
													{
														EndDateTimeOut = StartDateTimeIn.AddHours(TotHoursIn);
													}
													catch( ArgumentOutOfRangeException outOfRange )
													{
														await DisplayAlert
														   (
															   "Argument Out Of Range"
															   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Total Hours\" added = " + TotHoursIn.ToString()
															   + ".\r\nDate+Time Max. Value is " + DateTime.MaxValue.ToString("u").Remove(16)
															   , "OK"
														   );
														TotHoursIn = 0;
														TotHours.Text = "";
														TotHours.Focus();
														return;
													}
												}
												else
												{
													await DisplayAlert
													   (
														   "Type error"
														   , "Only one \"Total\" value allowed"
														   , "OK"
													   );
												}
											} // if (TotHoursIn != 0)
											else
											{
												if( TotMinutesIn != 0 )
												{
													try
													{
														EndDateTimeOut = StartDateTimeIn.AddMinutes(TotMinutesIn);
													}
													catch( ArgumentOutOfRangeException outOfRange )
													{
														await DisplayAlert
														   (
															   "Argument Out Of Range"
															   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Total Minutes\" added = " + TotMinutesIn.ToString()
															   + ".\r\nDate+Time Max. Value is " + DateTime.MaxValue.ToString("u").Remove(16)
															   , "OK"
														   );
														TotMinutesIn = 0;
														TotMinutes.Text = "";
														TotMinutes.Focus();
														return;
													}
												} // if (TotMinutesIn != 0)
											} // if (TotHoursIn != 0) .. else ...
										} // if (TotDaysIn != 0) ... else ...
									} // if (TotWeeksIn != 0) ... else ...
								} // if (TotMonthsIn != 0) ... else ...
							} // if (TotYearsIn != 0) ... else ...
						} // if (!TotChk)
						else
						{ // Must be Combnd time span

							EndDateTimeOut = StartDateTimeIn;

							if( CombndYearsIn != 0 )
							{
								try
								{
									EndDateTimeOut = EndDateTimeOut.AddYears(CombndYearsIn);
								}
								catch( ArgumentOutOfRangeException outOfRange )
								{
									await DisplayAlert
									   (
										   "Argument Out Of Range"
										   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Combined Years\" added = " + CombndYearsIn.ToString()
										   + ".\r\nDate+Time Max. Value is " + DateTime.MaxValue.ToString("u").Remove(16)
										   , "OK"
									   );
									CombndYearsIn = 0;
									CombndYears.Text = "";
									CombndYears.Focus();
									return;
								}
							} // if (CombndYearsIn != 0)
							if( CombndMonthsIn != 0 )
							{
								try
								{
									EndDateTimeOut = EndDateTimeOut.AddMonths(CombndMonthsIn);
								}
								catch( ArgumentOutOfRangeException outOfRange )
								{
									await DisplayAlert
									   (
										   "Argument Out Of Range"
										   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Combined Months\" added = " + CombndMonthsIn.ToString()
										   + ".\r\nDate+Time Max. Value is " + DateTime.MaxValue.ToString("u").Remove(16)
										   , "OK"
									   );
									CombndMonthsIn = 0;
									CombndMonths.Text = "";
									CombndMonths.Focus();
									return;
								}
							} // if (CombndMonthsIn != 0)
							if( CombndWeeksIn != 0 )
							{
								try
								{
									EndDateTimeOut = EndDateTimeOut.AddDays(CombndWeeksIn * 7);
								}
								catch( ArgumentOutOfRangeException outOfRange )
								{
									await DisplayAlert
									   (
										   "Argument Out Of Range"
										   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Combined Weeks\" added = " + CombndWeeksIn.ToString()
										   + ".\r\nDate+Time Max. Value is " + DateTime.MaxValue.ToString("u").Remove(16)
										   , "OK"
									   );
									CombndWeeksIn = 0;
									CombndWeeks.Text = "";
									CombndWeeks.Focus();
									return;
								}
							} // if (CombndWeeksIn != 0)
							if( CombndDaysIn != 0 )
							{
								try
								{
									EndDateTimeOut = EndDateTimeOut.AddDays(CombndDaysIn);
								}
								catch( ArgumentOutOfRangeException outOfRange )
								{
									await DisplayAlert
									   (
										   "Argument Out Of Range"
										   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Combined Days\" added = " + CombndDaysIn.ToString()
										   + ".\r\nDate+Time Max. Value is " + DateTime.MaxValue.ToString("u").Remove(16)
										   , "OK"
									   );
									CombndDaysIn = 0;
									CombndDays.Text = "";
									CombndDays.Focus();
									return;
								}
							} // if (CombndDaysIn != 0)
							if( CombndHoursIn != 0 )
							{
								try
								{
									EndDateTimeOut = EndDateTimeOut.AddHours(CombndHoursIn);
								}
								catch( ArgumentOutOfRangeException outOfRange )
								{
									await DisplayAlert
									   (
										   "Argument Out Of Range"
										   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Combined Hours\" added = " + CombndHoursIn.ToString()
										   + ".\r\nDate+Time Max. Value is " + DateTime.MaxValue.ToString("u").Remove(16)
										   , "OK"
									   );
									CombndHoursIn = 0;
									CombndHours.Text = "";
									CombndHours.Focus();
									return;
								}
							} // if (CombndHoursIn != 0)
							if( CombndMinutesIn != 0 )
							{
								try
								{
									EndDateTimeOut = EndDateTimeOut.AddMinutes(CombndMinutesIn);
								}
								catch( ArgumentOutOfRangeException outOfRange )
								{
									await DisplayAlert
									   (
										   "Argument Out Of Range"
										   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Combined Minutes\" added = " + CombndMinutesIn.ToString()
										   + ".\r\nDate+Time Max. Value is " + DateTime.MaxValue.ToString("u").Remove(16)
										   , "OK"
									   );
									CombndMinutesIn = 0;
									CombndMinutes.Text = "";
									CombndMinutes.Focus();
									return;
								}
							} // if (CombndMinutesIn != 0)

						}  // if (!TotChk) ... else ...

						if( EndDateTimeOut != DateTime.MaxValue )
						{
							// Save tmp SartDateTime and EndDateTime
							var tmpCalcEndDateSwitchIsOn = CalcEndDateSwitchIsOn;

							// Clear and reseteverything
							DoClearAll();

							// Show Start- and End Date Time
							CalcEndDateSwitchIsOn = tmpCalcEndDateSwitchIsOn;

							EndDateTimeIn = EndDateTimeOut;
							EndDateIn = EndDateTimeOut.Date;
							EndTimeIn = EndDateTimeOut.TimeOfDay;

							SetEndDateTime();

							// Show Time Spans.
							CalcAndShowTimeSpans();
						}

					} // if ( !(!TotChk && !CombndChk) )
					else
					{
						await DisplayAlert
						   (
							   "Type error"
							   , "Not both \"Total\" and \"Combined\" time spans can be used"
							   , "OK"
						   );
					} // if ( !(!TotChk && !CombndChk) ) ... else ...
				} // if ( !(TotChk && CombndChk) )
				else
				{
					await DisplayAlert
					   (
						   "Type error"
						   , "When \"Start Date + Time\" entered and no \"End Date + Time\" either a \"Total\" or \"Combined\" time span must be entered"
						   , "OK"
					   );
				} //  // if ( !(TotChk && CombndChk) ) ... else ...
			} // if (!CalcEndDateSwitchIsOn) ... else ...

			if( CalcStartDateSwitchIsOn )
			{ // CalcStartDateSwitchIsOn = true
				if( !CalcEndDateSwitchIsOn )
				{
					bool TotChk = (TotYearsIn == 0) &&
								  (TotMonthsIn == 0) &&
								  (TotWeeksIn == 0) &&
								  (TotDaysIn == 0) &&
								  (TotHoursIn == 0) &&
								  (TotMinutesIn == 0);

					bool CombndChk = (CombndYearsIn == 0) &&
									 (CombndMonthsIn == 0) &&
									 (CombndWeeksIn == 0) &&
									 (CombndDaysIn == 0) &&
									 (CombndHoursIn == 0) &&
									 (CombndMinutesIn == 0);

					if( !(TotChk && CombndChk) )
					{
						if( !(!TotChk && !CombndChk) )
						{
							StartDateTimeOut = DateTime.MaxValue; // <=> no StartDateTimeOut found

							if( !TotChk )
							{
								if( TotYearsIn != 0 )
								{
									if( (TotMonthsIn == 0) &&
										(TotWeeksIn == 0) &&
										(TotDaysIn == 0) &&
										(TotHoursIn == 0) &&
										(TotMinutesIn == 0) )
									{
										try
										{
											StartDateTimeOut = EndDateTimeIn.AddYears(-TotYearsIn);
										}
										catch( ArgumentOutOfRangeException outOfRange )
										{
											await DisplayAlert
											   (
												   "Argument Out Of Range"
												   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Total Years\" subtracted = " + TotYearsIn.ToString()
												   + ".\r\nDate+Time Min. Value is " + DateTime.MinValue.ToString("u").Remove(16)
												   , "OK"
											   );
											TotYearsIn = 0;
											TotYears.Text = "";
											TotYears.Focus();
											return;
										}
									}
									else
									{
										await DisplayAlert
										   (
											   "Type error"
											   , "Only one \"Total\" value allowed"
											   , "OK"
										   );
									}
								} // if (TotYearsIn != 0)
								else
								{
									if( TotMonthsIn != 0 )
									{
										if( (TotWeeksIn == 0) &&
											(TotDaysIn == 0) &&
											(TotHoursIn == 0) &&
											(TotMinutesIn == 0) )
										{
											try
											{
												StartDateTimeOut = EndDateTimeIn.AddMonths(-TotMonthsIn);
											}
											catch( ArgumentOutOfRangeException outOfRange )
											{
												await DisplayAlert
												   (
													   "Argument Out Of Range"
													   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Total Months\" subtracted = " + TotMonthsIn.ToString()
													   + ".\r\nDate+Time Min. Value is " + DateTime.MinValue.ToString("u").Remove(16)
													   , "OK"
												   );
												TotMonthsIn = 0;
												TotMonths.Text = "";
												TotMonths.Focus();
												return;
											}
										}
										else
										{
											await DisplayAlert
											   (
												   "Type error"
												   , "Only one \"Total\" value allowed"
												   , "OK"
											   );
										}
									} // if (TotMonthsIn != 0)
									else
									{
										if( TotWeeksIn != 0 )
										{
											if( (TotDaysIn == 0) &&
											   (TotHoursIn == 0) &&
											   (TotMinutesIn == 0) )
											{
												try
												{
													StartDateTimeOut = EndDateTimeIn.AddDays(-(TotWeeksIn * 7));
												}
												catch( ArgumentOutOfRangeException outOfRange )
												{
													await DisplayAlert
													   (
														   "Argument Out Of Range"
														   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Total Weeks\" subtracted = " + TotWeeksIn.ToString()
														   + ".\r\nDate+Time Min. Value is " + DateTime.MinValue.ToString("u").Remove(16)
														   , "OK"
													   );
													TotWeeksIn = 0;
													TotWeeks.Text = "";
													TotWeeks.Focus();
													return;
												}
											}
											else
											{
												await DisplayAlert
												   (
													   "Type error"
													   , "Only one \"Total\" value allowed"
													   , "OK"
												   );
											}
										} // if (TotWeeksIn != 0)
										else
										{
											if( TotDaysIn != 0 )
											{
												if( (TotHoursIn == 0) &&
													(TotMinutesIn == 0) )
												{
													try
													{
														StartDateTimeOut = EndDateTimeIn.AddDays(-TotDaysIn);
													}
													catch( ArgumentOutOfRangeException outOfRange )
													{
														await DisplayAlert
														   (
															   "Argument Out Of Range"
															   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Total Days\" subtracted = " + TotDaysIn.ToString()
															   + ".\r\nDate+Time Min. Value is " + DateTime.MinValue.ToString("u").Remove(16)
															   , "OK"
														   );
														TotDaysIn = 0;
														TotDays.Text = "";
														TotDays.Focus();
														return;
													}
												}
												else
												{
													await DisplayAlert
													   (
														   "Type error"
														   , "Only one \"Total\" value allowed"
														   , "OK"
													   );
												}
											} // if (TotDaysIn != 0)
											else
											{
												if( TotHoursIn != 0 )
												{
													if( TotMinutesIn == 0 )
													{
														try
														{
															StartDateTimeOut = EndDateTimeIn.AddHours(-TotHoursIn);
														}
														catch( ArgumentOutOfRangeException outOfRange )
														{
															await DisplayAlert
															   (
																   "Argument Out Of Range"
																   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Total Hours\" subtracted = " + TotHoursIn.ToString()
																   + ".\r\nDate+Time Min. Value is " + DateTime.MinValue.ToString("u").Remove(16)
																   , "OK"
															   );
															TotHoursIn = 0;
															TotHours.Text = "";
															TotHours.Focus();
															return;
														}
													}
													else
													{
														await DisplayAlert
														   (
															   "Type error"
															   , "Only one \"Total\" value allowed"
															   , "OK"
														   );
													}
												} // if (TotHoursIn != 0)
												else
												{
													if( TotMinutesIn != 0 )
													{
														try
														{
															StartDateTimeOut = EndDateTimeIn.AddMinutes(-TotMinutesIn);
														}
														catch( ArgumentOutOfRangeException outOfRange )
														{
															await DisplayAlert
															   (
																   "Argument Out Of Range"
																   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Total Minutes\" subtracted = " + TotMinutesIn.ToString()
																   + ".\r\nDate+Time Min. Value is " + DateTime.MinValue.ToString("u").Remove(16)
																   , "OK"
															   );
															TotMinutesIn = 0;
															TotMinutes.Text = "";
															TotMinutes.Focus();
															return;
														}
													} // if (TotMinutesIn != 0)
													  //else
													  //{
													  //    MessageBox.Show("At least one Total value must be entered", "Type error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
													  //} // if (TotMinutesIn != 0) ... else ...
												} // if (TotHoursIn != 0) .. else ...
											} // if (TotDaysIn != 0) ... else ...
										} // if (TotWeeksIn != 0) ... else ...
									} // if (TotMonthsIn != 0) ... else ...
								} // if (TotYearsIn != 0) ... else ...
							} // if (!TotChk)
							else
							{ // Must be Combnd time span

								StartDateTimeOut = EndDateTimeIn;

								if( CombndYearsIn != 0 )
								{
									try
									{
										StartDateTimeOut = StartDateTimeOut.AddYears(-CombndYearsIn);
									}
									catch( ArgumentOutOfRangeException outOfRange )
									{
										await DisplayAlert
										   (
											   "Argument Out Of Range"
											   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Combined Years\" subtracted = " + CombndYearsIn.ToString()
											   + ".\r\nDate+Time Min. Value is " + DateTime.MinValue.ToString("u").Remove(16)
											   , "OK"
										   );
										CombndYearsIn = 0;
										CombndYears.Text = "";
										CombndYears.Focus();
										return;
									}
								} // if (CombndYearsIn != 0)
								if( CombndMonthsIn != 0 )
								{
									try
									{
										StartDateTimeOut = StartDateTimeOut.AddMonths(-CombndMonthsIn);
									}
									catch( ArgumentOutOfRangeException outOfRange )
									{
										await DisplayAlert
										   (
											   "Argument Out Of Range"
											   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Combined Months\" subtracted = " + CombndMonthsIn.ToString()
											   + ".\r\nDate+Time Min. Value is " + DateTime.MinValue.ToString("u").Remove(16)
											   , "OK"
										   );
										CombndMonthsIn = 0;
										CombndMonths.Text = "";
										CombndMonths.Focus();
										return;
									}
								} // if (CombndMonthsIn != 0)
								if( CombndWeeksIn != 0 )
								{
									try
									{
										StartDateTimeOut = StartDateTimeOut.AddDays(-(CombndWeeksIn * 7));
									}
									catch( ArgumentOutOfRangeException outOfRange )
									{
										await DisplayAlert
										   (
											   "Argument Out Of Range"
											   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Combined Weeks\" subtracted = " + CombndWeeksIn.ToString()
											   + ".\r\nDate+Time Min. Value is " + DateTime.MinValue.ToString("u").Remove(16)
											   , "OK"
										   );
										CombndWeeksIn = 0;
										CombndWeeks.Text = "";
										CombndWeeks.Focus();
										return;
									}
								} // if (CombndWeeksIn != 0)
								if( CombndDaysIn != 0 )
								{
									try
									{
										StartDateTimeOut = StartDateTimeOut.AddDays(-CombndDaysIn);
									}
									catch( ArgumentOutOfRangeException outOfRange )
									{
										await DisplayAlert
										   (
											   "Argument Out Of Range"
											   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Combined Days\" subtracted = " + CombndDaysIn.ToString()
											   + ".\r\nDate+Time Min. Value is " + DateTime.MinValue.ToString("u").Remove(16)
											   , "OK"
										   );
										CombndDaysIn = 0;
										CombndDays.Text = "";
										CombndDays.Focus();
										return;
									}
								} // if (CombndDaysIn != 0)
								if( CombndHoursIn != 0 )
								{
									try
									{
										StartDateTimeOut = StartDateTimeOut.AddHours(-CombndHoursIn);
									}
									catch( ArgumentOutOfRangeException outOfRange )
									{
										await DisplayAlert
										   (
											   "Argument Out Of Range"
											   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Combined Hours\" subtracted = " + CombndHoursIn.ToString()
											   + ".\r\nDate+Time Min. Value is " + DateTime.MinValue.ToString("u").Remove(16)
											   , "OK"
										   );
										CombndHoursIn = 0;
										CombndHours.Text = "";
										CombndHours.Focus();
										return;
									}
								} // if (CombndHoursIn != 0)
								if( CombndMinutesIn != 0 )
								{
									try
									{
										StartDateTimeOut = StartDateTimeOut.AddMinutes(-CombndMinutesIn);
									}
									catch( ArgumentOutOfRangeException outOfRange )
									{
										await DisplayAlert
										   (
											   "Argument Out Of Range"
											   , outOfRange.Message.Remove(outOfRange.Message.IndexOf(" name:")) + ": \"Combined Minutes\" subtracted = " + CombndMinutesIn.ToString()
											   + ".\r\nDate+Time Min. Value is " + DateTime.MinValue.ToString("u").Remove(16)
											   , "OK"
										   );
										CombndMinutesIn = 0;
										CombndMinutes.Text = "";
										CombndMinutes.Focus();
										return;
									}
								} // if (CombndMinutesIn != 0)

							}  // if (!TotChk) ... else ...

							if( StartDateTimeOut != DateTime.MaxValue )
							{
								// Save tmp SartDateTime and EndDateTime
								var tmpCalcStartDateSwitchIsOn = CalcStartDateSwitchIsOn;

								// Clear and reseteverything
								DoClearAll();

								//// Show Start- and End Date Time
								CalcStartDateSwitchIsOn = tmpCalcStartDateSwitchIsOn;
								StartDateTimeIn = StartDateTimeOut;

								StartDateIn = StartDateTimeOut.Date;
								StartTimeIn = StartDateTimeOut.TimeOfDay;

								SetStartDateTime();

								// Show Time Spans.
								CalcAndShowTimeSpans();
							}

						} // if ( !(!TotChk && !CombndChk) )
						else
						{
							await DisplayAlert
							   (
								   "Type error"
								   , "Not both \"Total\" and \"Combined\" time spans can be used"
								   , "OK"
							   );
						} // if ( !(!TotChk && !CombndChk) ) ... else ...
					} // if ( !(TotChk && CombndChk) )
					else
					{
						await DisplayAlert
						   (
							   "Error"
							   , "When \"Calculate\" \"End Date + Time\" selected then either a \"Total\" or \"Combined\" time span must be entered"
							   , "OK"
						   );
					} //  // if ( !(TotChk && CombndChk) ) ... else ...
				} // if (!CalcEndDateSwitchIsOn)
				else
				{ // CalcEndDateSwitchIsOn = true
					await DisplayAlert
					   (
						   "Error"
						   , "Can't calculate both \"Start Date + Time\" and \"End Date + Time\""
						   , "OK"
					   );
				} // if (!CalcEndDateSwitchIsOn) ... else ...
			} // if (!CalcStartDateSwitchIsOn) ... else...

		} // private async void OnCalculateButtonClicked(object sEnder, EventArgs e)

		// CALCULATION Ends here...

		private async void OnHelpButtonClicked(object sEnder, EventArgs e)
		{
			await Navigation.PushAsync(new AboutHelp());

			//	+ Environment.NewLine + Environment.NewLine
			//	+ "Test"
			//	;
			//await DisplayAlert("Application", AppTitleAndVersion, "OK");

		}

		private void CalcYMWDHM_toggeled(object sender, ToggledEventArgs e)
		{
			CalcYMWDHMIsOn = e.Value;
			if( CalcYMWDHMIsOn )
			{
				DisableYMWDHM(null);
				DoClearAll();
				EnableAndToggleOffAllSwitchedXceptMe((Switch)sender);
				LabelEqual.Text = "=";
				LabelPlus.Text = "+";
			}
			else
			{
				EnableYMWDHM(null);
				RWYMWDHM(null);
			}
		}

		private void OnTotYMWDHMFocused(object sender, FocusEventArgs e)
		{
			ClearYMWDHM((Entry)sender);
		}

		private void OnCombndYMWDHMFocused(object sender, FocusEventArgs e)
		{
			ClearTotYMWDHM((Entry)sender);
		}

	}

}
