using CustomRenderer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using TimeDateCalculator.FileHandlers;
using TimeDateCalculator.Interfaces;
using TimeDateCalculator.MessageThings;
using TimeDateCalculatorDll;
using Xamarin.Essentials;
using Xamarin.Forms;
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

		double nativeTotalStackWidthLandscape = 731.0;
		double nativeTotalStackHeightPortrait = 732.0;


		DatePicker MacStartDatePicker = new DatePicker();
		TimePicker MacStartTimePicker = new myMacOSNormalTimePicker();
		DatePicker MacEndDatePicker = new DatePicker();
		TimePicker MacEndTimePicker = new myMacOSNormalTimePicker();

		DatePicker StartDatePicker = new DatePicker();
		TimePicker StartTimePicker = new TimePicker();
		Picker GtkStartHourPicker = new Picker();
		Picker GtkStartMinutsPicker = new Picker();
		Label StartDayName = new Label();
		Button StartDateTimeNowButton = new Button();
		DatePicker EndDatePicker = new DatePicker();
		TimePicker EndTimePicker = new TimePicker();
		Picker GtkEndHourPicker = new Picker();
		Picker GtkEndMinutsPicker = new Picker();
		Label EndDayName = new Label();
		Button EndDateTimeNowButton = new Button();

		private double StartDateTimeIntroLabelNameFontSizeOrig = 0.0;

		private double StartEndDayNameFontSizeOrig = 0.0;

		public DateTime StartDateTimeIn { get; set; }
		public DateTime StartDateIn { get; set; }



		public TimeSpan StartTimeIn { get; set; }


		public bool CalcStartDateSwitchIsOn { get; set; } = false;
		public DateTime StartDateTimeOut { get; set; }

		public DateTime EndDateTimeIn { get; set; }
		public DateTime EndDateIn { get; set; }

		public TimeSpan EndTimeIn { get; set; }

		public bool CalcEndDateSwitchIsOn { get; set; } = false;
		public DateTime EndDateTimeOut { get; set; }
		public bool CalcYMWDHMIsOn { get; set; } = true;

		private TimeSpan PrivEnteredYMWDHMTimeSpan { get; set; } = new TimeSpan(0);
		public TimeSpan EnteredYMWDHMTimeSpan
		{
			get { return PrivEnteredYMWDHMTimeSpan; }
			set => PrivEnteredYMWDHMTimeSpan = value;
		}


		List<Entry> ListOfCmbndEntrys;
		List<Entry> ListOfTotEntrys;

		List<Switch> ListOfSwitches;


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
			try
			{
				if (Device.RuntimePlatform == Device.GTK)
				{
					// Remove event
					GtkStartHourPicker.SelectedIndexChanged -= GtkStartTime_SelectedIndexChanged;
					GtkStartMinutsPicker.SelectedIndexChanged -= GtkStartTime_SelectedIndexChanged;
					// Show time
					GtkStartHourPicker.SelectedIndex = StartTimeIn.Hours;
					GtkStartMinutsPicker.SelectedIndex = StartTimeIn.Minutes;
					// Restore event
					GtkStartHourPicker.SelectedIndexChanged += GtkStartTime_SelectedIndexChanged;
					GtkStartMinutsPicker.SelectedIndexChanged += GtkStartTime_SelectedIndexChanged;
				}
				else
				{
					MacStartDatePicker.Date = StartDateIn;
					MacStartTimePicker.Time = new TimeSpan(StartTimeIn.Hours, StartTimeIn.Minutes, 0);

					StartTimePicker.Time = new TimeSpan(StartTimeIn.Hours, StartTimeIn.Minutes, 0);
				}

				StartDatePicker.Date = StartDateIn;

				StartDayName.Text = StartDateIn.DayOfWeek.ToString().Remove(3);

			}
			catch (Exception)
			{
			}
		}

		private void SetEndDateTime()
		{
			try
			{
				if (Device.RuntimePlatform == Device.GTK)
				{
					// Remove events
					GtkEndHourPicker.SelectedIndexChanged -= GtkEndTime_SelectedIndexChanged;
					GtkEndMinutsPicker.SelectedIndexChanged -= GtkEndTime_SelectedIndexChanged;
					// Show time
					GtkEndHourPicker.SelectedIndex = EndTimeIn.Hours;
					GtkEndMinutsPicker.SelectedIndex = EndTimeIn.Minutes;
					// Restore events
					GtkEndHourPicker.SelectedIndexChanged += GtkEndTime_SelectedIndexChanged;
					GtkEndMinutsPicker.SelectedIndexChanged += GtkEndTime_SelectedIndexChanged;
				}
				else
				{
					MacEndDatePicker.Date = EndDateIn;
					MacEndTimePicker.Time = new TimeSpan(EndTimeIn.Hours, EndTimeIn.Minutes, 0);

					EndTimePicker.Time = new TimeSpan(EndTimeIn.Hours, EndTimeIn.Minutes, 0);
				}

				EndDatePicker.Date = EndDateIn;

				EndDayName.Text = EndDateIn.DayOfWeek.ToString().Remove(3);

			}
			catch (Exception)
			{
			}
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
			foreach (Entry CurEntry in ListOfCmbndEntrys)
			{
				if (CurEntry != ImInFocus)
				{
					CurEntry.IsReadOnly = false;
					//CurEntry.IsEnabled = true;
				}
			}
		}

		private void EnableTotYMWDHM(Entry ImInFocus)
		{
			foreach (Entry CurEntry in ListOfTotEntrys)
			{
				if (CurEntry != ImInFocus)
				{
					CurEntry.IsReadOnly = false;
					//CurEntry.IsEnabled = true;
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
			foreach (Entry CurEntry in ListOfCmbndEntrys)
			{
				if (CurEntry != ImInFocus)
				{
					CurEntry.IsReadOnly = true;
					//CurEntry.IsEnabled = false;
				}
			}
		}

		private void DisableTotYMWDHM(Entry ImInFocus)
		{
			foreach (Entry CurEntry in ListOfTotEntrys)
			{
				if (CurEntry != ImInFocus)
				{
					CurEntry.IsReadOnly = true;
					//CurEntry.IsEnabled = false;
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
			foreach (Switch CurSwitch in ListOfSwitches)
			{
				if (CurSwitch != SwitchToDisaable)
				{
					CurSwitch.IsEnabled = true;
					CurSwitch.IsToggled = false;
				}
				else
				{
					CurSwitch.IsEnabled = false;
					CurSwitch.IsToggled = true;
					CurSwitch.OnColor = Color.Green;
				}
			}
		}

		private void RWCmbndYMWDHM(Entry ImInFocus)
		{
			foreach (Entry CurEntry in ListOfCmbndEntrys)
			{
				if (CurEntry != ImInFocus)
				{
					CurEntry.IsReadOnly = false;
				}
			}
		}

		private void RWTotYMWDHM(Entry ImInFocus)
		{
			foreach (Entry CurEntry in ListOfTotEntrys)
			{
				if (CurEntry != ImInFocus)
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
			foreach (Entry CurEntry in ListOfCmbndEntrys)
			{
				if (CurEntry != ImInFocus)
				{
					CurEntry.Text = "";
				}
			}
			ClearCmbndIOVars();
		}

		private void ClearTotYMWDHM(Entry ImInFocus)
		{
			foreach (Entry CurEntry in ListOfTotEntrys)
			{
				if (CurEntry != ImInFocus)
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

			switch (Device.RuntimePlatform)
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

		public MainPage()
		{

			InitializeComponent();

			MessagingCenter.Subscribe<App, SelectFileResultMessageArgs>((App)Application.Current, MessengerKeys.FileToReadFromSelected, On_FileToReadFromSelectedAsync);
			MessagingCenter.Subscribe<App, SelectFileResultMessageArgs>((App)Application.Current, MessengerKeys.FileToSaveToSelected, On_FileToSaveToSelected);
			MessagingCenter.Subscribe<App, SelectFileResultMessageArgs>((App)Application.Current, MessengerKeys.FileToSaveRawTextToSelected, On_FileToSaveRawTextToSelected);
			MessagingCenter.Subscribe<App, SaveToIcsMessageArgs>((App)Application.Current, MessengerKeys.SaveToIcsMessageKey, On_SaveToIcsMessageReceived);
			MessagingCenter.Subscribe<App, OpenIcsMessageArgs>((App)Application.Current, MessengerKeys.OpenIcsMessageKey, On_OpenIcsMessageReceived);


			switch (Device.RuntimePlatform)
			{
				case Device.Android:
					{
						ContentPageName.SetAppThemeColor(BackgroundColorProperty, Color.White, Color.Black);
						Resources["DynamicBaseButtonStyle"] = Resources["AndroidBaseButtonStyle"];
						break;
					}
				case Device.GTK:
					{
						//ContentPageName.SetAppThemeColor(BackgroundColorProperty, Color.White, Color.Black);
						Resources["DynamicBaseButtonStyle"] = Resources["baseButtonStyle"];
						break;
					}
				default:
					{
						Resources["DynamicBaseButtonStyle"] = Resources["baseButtonStyle"];
						break;
					}
			}

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

			StartDateIn = DateTime.Now.Date;
			StartTimeIn = DateTime.Now.TimeOfDay;

			EndDateIn = DateTime.Now.Date;
			EndTimeIn = DateTime.Now.TimeOfDay;

			// Start Date/Time
			MacStartDatePicker = new DatePicker
			{
				Style = Resources["baseDatePickerStyle_WO_WidthRequest"] as Style
				,
				Format = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern
				,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				,
				Date = DateTime.Today.Date
			};
			MacStartDatePicker.DateSelected += OnMacStartDatePickerDateSelected;

			MacStartTimePicker = new myMacOSNormalTimePicker
			{
				Style = Resources["baseTimePickerStyle"] as Style
					,
				Time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0)
					,
				Format =
					(
						CultureInfo.CurrentUICulture.DateTimeFormat.ShortTimePattern
					).Substring
						(
							0
							,
							CultureInfo.CurrentUICulture.DateTimeFormat.ShortTimePattern.Length - 3
						)
			};
			MacStartTimePicker.PropertyChanged += OnMacStartTimePickerPropertyChanged;

			StartDayName = new Label
			{
				Style = Resources["baseStartEndDateTimeEntryLabelStyle"] as Style,
				Text = " MMM ",
				MinimumWidthRequest = 30,
				LineBreakMode = LineBreakMode.WordWrap
			};

			StartDateTimeNowButton = new Button
			{
				Text = "Now"
							,
				VerticalOptions = LayoutOptions.Center
			};
			if (Device.RuntimePlatform == Device.Android)
			{
				StartDateTimeNowButton.Style = Resources["AndroidBaseButtonStyle"] as Style;
			}
			else
			{
				StartDateTimeNowButton.Style = Resources["baseButtonStyle"] as Style;
			}
			StartDateTimeNowButton.Clicked += OnStartDateTimeNowButtonClicked;

			// End Date/Time
			MacEndDatePicker = new DatePicker
			{
				Style = Resources["baseDatePickerStyle_WO_WidthRequest"] as Style
				,
				Format = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern
				,
				HorizontalOptions = LayoutOptions.CenterAndExpand
				,
				Date = DateTime.Today.Date
			};
			MacEndDatePicker.DateSelected += OnMacEndDatePickerDateSelected;

			MacEndTimePicker = new myMacOSNormalTimePicker
			{
				Style = Resources["baseTimePickerStyle"] as Style
					,
				Time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0)
					,
				Format =
					(
						CultureInfo.CurrentUICulture.DateTimeFormat.ShortTimePattern
					).Substring
						(
							0
							,
							CultureInfo.CurrentUICulture.DateTimeFormat.ShortTimePattern.Length - 3
						)
			};
			MacEndTimePicker.PropertyChanged += OnMacEndTimePickerPropertyChanged;

			EndDayName = new Label
			{
				Style = Resources["baseStartEndDateTimeEntryLabelStyle"] as Style,
				Text = " MMM ",
				MinimumWidthRequest = 30,
				LineBreakMode = LineBreakMode.WordWrap
			};

			EndDateTimeNowButton = new Button
			{
				Text = "Now"
				,
				VerticalOptions = LayoutOptions.Center

			};
			if (Device.RuntimePlatform == Device.Android)
			{
				EndDateTimeNowButton.Style = Resources["AndroidBaseButtonStyle"] as Style;
			}
			else
			{
				EndDateTimeNowButton.Style = Resources["baseButtonStyle"] as Style;
			}
			EndDateTimeNowButton.Clicked += OnEndDateTimeNowButtonClicked;

			switch (Device.RuntimePlatform)
			{
				case Device.macOS:
					{
						// Start Date/Time
						StartDatePicker = new myMacOSDatePicker();
						StartDatePicker.BackgroundColor = Color.Gray;
						StartDatePicker.DateSelected += StartDatePicker_DateSelected;

						StartTimePicker = new myMacOSTimePicker();
						StartTimePicker.PropertyChanged += StartTimePicker_PropertyChanged;

						var localStartDateStack = new StackLayout();
						localStartDateStack.Children.Add(MacStartDatePicker);
						localStartDateStack.Children.Add(StartDatePicker);

						StartDateTimeStack.Children.Add(localStartDateStack);

						var localStartTimeStack = new StackLayout();
						localStartTimeStack.Children.Add(MacStartTimePicker);
						localStartTimeStack.Children.Add(StartTimePicker);

						StartDateTimeStack.Children.Add(localStartTimeStack);

						StartDateTimeStack.Children.Add(StartDayName);
						StartDateTimeStack.Children.Add(StartDateTimeNowButton);


						// End Date/Time
						EndDatePicker = new myMacOSDatePicker();
						EndDatePicker.BackgroundColor = Color.Gray;
						EndDatePicker.DateSelected += EndDatePicker_DateSelected;

						EndTimePicker = new myMacOSTimePicker();
						EndTimePicker.PropertyChanged += EndTimePicker_PropertyChanged;

						var localEndDateStack = new StackLayout();
						localEndDateStack.Children.Add(MacEndDatePicker);
						localEndDateStack.Children.Add(EndDatePicker);

						EndDateTimeStack.Children.Add(localEndDateStack);

						var localEndTimeStack = new StackLayout();
						localEndTimeStack.Children.Add(MacEndTimePicker);
						localEndTimeStack.Children.Add(EndTimePicker);

						EndDateTimeStack.Children.Add(localEndTimeStack);

						EndDateTimeStack.Children.Add(EndDayName);
						EndDateTimeStack.Children.Add(EndDateTimeNowButton);

						break;
					}
				case Device.GTK:
					{
						// Start Date/Time
						StartDatePicker = new DatePicker
						{
							Style = Resources["baseGTKDatePickerStyle_WO_WidthRequest"] as Style
						};
						StartDatePicker.DateSelected += StartDatePicker_DateSelected;
						//StartDatePicker.BackgroundColor = Color.White;
						//StartDatePicker.BackgroundColor = Color.DarkSlateBlue;

						StartDatePicker.Format = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;
						StartDatePicker.HorizontalOptions = LayoutOptions.CenterAndExpand;

						StartDateTimeStack.Children.Add(StartDatePicker);


						GtkStartHourPicker = new Picker
						{
							Style = Resources["baseGTKTimePickerStyle"] as Style
						};
						for (int i = 0; i <= 23; i++)
						{
							GtkStartHourPicker.Items.Add(i.ToString());
						}
						StartDateTimeStack.Children.Add(GtkStartHourPicker);

						Label StartHMColon = new Label
						{
							Text = ":"
	,
							Style = Resources["baseStartEndDateTimeEntryLabelStyle"] as Style
						};
						StartDateTimeStack.Children.Add(StartHMColon);

						GtkStartMinutsPicker = new Picker
						{
							Style = Resources["baseGTKTimePickerStyle"] as Style
						};
						for (int i = 0; i <= 59; i++)
						{
							GtkStartMinutsPicker.Items.Add(i.ToString());
						}
						StartDateTimeStack.Children.Add(GtkStartMinutsPicker);

						StartDateTimeStack.Children.Add(StartDayName);
						StartDateTimeStack.Children.Add(StartDateTimeNowButton);


						// End Date/Time
						EndDatePicker = new DatePicker
						{
							Style = Resources["baseGTKDatePickerStyle_WO_WidthRequest"] as Style
						};
						EndDatePicker.DateSelected += EndDatePicker_DateSelected;

						EndDatePicker.Format = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;
						EndDatePicker.HorizontalOptions = LayoutOptions.CenterAndExpand;

						EndDateTimeStack.Children.Add(EndDatePicker);


						GtkEndHourPicker = new Picker
						{
							Style = Resources["baseGTKTimePickerStyle"] as Style
						};
						for (int i = 0; i <= 23; i++)
						{
							GtkEndHourPicker.Items.Add(i.ToString());
						}
						EndDateTimeStack.Children.Add(GtkEndHourPicker);

						Label EndMColon = new Label
						{
							Text = ":"
,
							Style = Resources["baseStartEndDateTimeEntryLabelStyle"] as Style
						};
						EndDateTimeStack.Children.Add(EndMColon);

						GtkEndMinutsPicker = new Picker
						{
							Style = Resources["baseGTKTimePickerStyle"] as Style
						};
						for (int i = 0; i <= 59; i++)
						{
							GtkEndMinutsPicker.Items.Add(i.ToString());
						}
						EndDateTimeStack.Children.Add(GtkEndMinutsPicker);

						EndDateTimeStack.Children.Add(EndDayName);
						EndDateTimeStack.Children.Add(EndDateTimeNowButton);


						break;
					}
				default:
					{
						// Start Date/Time
						StartDatePicker = new DatePicker
						{
							Style = Resources["baseDatePickerStyle_WO_WidthRequest"] as Style
						};
						StartDatePicker.DateSelected += StartDatePicker_DateSelected;
						//StartDatePicker.BackgroundColor = Color.DarkSlateBlue;

						StartTimePicker = new TimePicker
						{
							Style = Resources["baseTimePickerStyle"] as Style
						};
						StartTimePicker.PropertyChanged += StartTimePicker_PropertyChanged;

						StartDateTimeStack.Children.Add(StartDatePicker);
						StartDateTimeStack.Children.Add(StartTimePicker);
						StartDateTimeStack.Children.Add(StartDayName);
						StartDateTimeStack.Children.Add(StartDateTimeNowButton);

						// End Date/Time
						EndDatePicker = new DatePicker
						{
							Style = Resources["baseDatePickerStyle_WO_WidthRequest"] as Style
						};
						EndDatePicker.DateSelected += EndDatePicker_DateSelected;

						EndTimePicker = new TimePicker
						{
							Style = Resources["baseTimePickerStyle"] as Style
						};
						EndTimePicker.PropertyChanged += EndTimePicker_PropertyChanged;

						EndDateTimeStack.Children.Add(EndDatePicker);
						EndDateTimeStack.Children.Add(EndTimePicker);
						EndDateTimeStack.Children.Add(EndDayName);
						EndDateTimeStack.Children.Add(EndDateTimeNowButton);

						StartDatePicker.Format = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;
						StartDatePicker.HorizontalOptions = LayoutOptions.CenterAndExpand;
						StartTimePicker.Format = CultureInfo.CurrentUICulture.DateTimeFormat.ShortTimePattern;

						EndDatePicker.Format = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;
						EndDatePicker.HorizontalOptions = LayoutOptions.CenterAndExpand;
						EndTimePicker.Format = CultureInfo.CurrentUICulture.DateTimeFormat.ShortTimePattern;

						break;
					}
			}

			if (Device.RuntimePlatform == Device.GTK)
			{
				GtkStartHourPicker.SelectedIndex = DateTime.Now.TimeOfDay.Hours;
				GtkStartMinutsPicker.SelectedIndex = DateTime.Now.TimeOfDay.Minutes;
				GtkEndHourPicker.SelectedIndex = DateTime.Now.TimeOfDay.Hours;
				GtkEndMinutsPicker.SelectedIndex = DateTime.Now.TimeOfDay.Minutes;


				GtkStartHourPicker.SelectedIndexChanged += GtkStartTime_SelectedIndexChanged;
				GtkStartMinutsPicker.SelectedIndexChanged += GtkStartTime_SelectedIndexChanged;

				GtkEndHourPicker.SelectedIndexChanged += GtkEndTime_SelectedIndexChanged;
				GtkEndMinutsPicker.SelectedIndexChanged += GtkEndTime_SelectedIndexChanged;

			}
			else
			{
				StartTimePicker.Time = DateTime.Now.TimeOfDay;
				EndTimePicker.Time = DateTime.Now.TimeOfDay;
			}

			StartDatePicker.Date = DateTime.Now.Date;

			EndDatePicker.Date = DateTime.Now.Date;



			if (Device.RuntimePlatform == Device.UWP)
			{
				ScreenWidth = DependencyService.Get<IScreenSizeInterface>().GetScreenWidth();
				ScreenHeight = DependencyService.Get<IScreenSizeInterface>().GetScreenHeight();
			}



			StartDatePicker.MinimumDate = DateTime.MinValue;
			StartDatePicker.MaximumDate = DateTime.MaxValue;
			EndDatePicker.MinimumDate = DateTime.MinValue;
			EndDatePicker.MaximumDate = DateTime.MaxValue;

		}

		private double TotalStackNameScaleLast = 1.0f;
		private double scrollViewNameScaleLast = 1.0f;
		private double ContentPageNameScaleLast = 1.0f;
		private double StartDateTimeStacAndPlusScaleLast = 1.0f;
		private double entriesOuterGridScaleLast = 1.0f;
		private double EndDateTimeAndCalculateAndClearAllButtonsStackNameScaleLast = 1.0f;

		protected override void OnSizeAllocated(double width, double height)
		{
			if (firstTime)
			{
				DoClearAll();
				firstTime = false;
			}

			base.OnSizeAllocated(width, height);


			if (Device.RuntimePlatform == Device.UWP)
			{
				ScreenWidth = DependencyService.Get<IScreenSizeInterface>().GetScreenWidth();
				ScreenHeight = DependencyService.Get<IScreenSizeInterface>().GetScreenHeight();
			}
			else
			{
				ScreenWidth = width;
				ScreenHeight = height;
			}

			if (width != this.width || height != this.height)
			{

				this.width = width;
				this.height = height;

				TotalStackName.TranslationX = 0.0f;
				TotalStackName.TranslationY = 0.0f;

				double widthAndHightScale;


				if (firstTimeWdthOrHeightChanged)
				{
					StartDateTimeIntroLabelNameFontSizeOrig = StartDateTimeIntroLabelName.FontSize;
					StartEndDayNameFontSizeOrig = StartDayName.FontSize;
					firstTimeWdthOrHeightChanged = false;
				}

				// Reset scaling
				ContentPageName.Scale = 1.0f / ContentPageNameScaleLast;
				scrollViewName.Scale = 1.0f / scrollViewNameScaleLast;
				TotalStackName.Scale = 1.0f / TotalStackNameScaleLast;
				StartDateTimeStacAndPlus.Scale = 1.0f / StartDateTimeStacAndPlusScaleLast;
				entriesOuterGrid.Scale = 1.0f / entriesOuterGridScaleLast;
				EndDateTimeAndCalculateAndClearAllButtonsStackName.Scale = 1.0f / EndDateTimeAndCalculateAndClearAllButtonsStackNameScaleLast;

				ContentPageNameScaleLast = 1.0f;
				scrollViewNameScaleLast = 1.0f;
				TotalStackNameScaleLast = 1.0f;
				StartDateTimeStacAndPlusScaleLast = 1.0f;
				entriesOuterGridScaleLast = 1.0f;
				EndDateTimeAndCalculateAndClearAllButtonsStackNameScaleLast = 1.0f;

				DisplayInfo mainDisplayInfo;
				DisplayOrientation orientation = DisplayOrientation.Landscape;
				double mainWidth = 1080;
				double mainHeight = 1920f;
				bool portrait = false;

				if (Device.RuntimePlatform != Device.GTK)
				{
					// Get Metrics
					mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

					// Orientation (Landscape, Portrait, Square, Unknown)
					orientation = mainDisplayInfo.Orientation;

					// Width (in pixels)
					mainWidth = mainDisplayInfo.Width;

					// Height (in pixels)
					mainHeight = mainDisplayInfo.Height;

					portrait = (orientation == DisplayOrientation.Portrait);
				}

				if (portrait)
				{ // Portrait
					if
					(
						   (Device.RuntimePlatform == Device.macOS)
						|| (Device.RuntimePlatform == Device.UWP)
						|| ((Device.RuntimePlatform == Device.Android) && (mainHeight < 1920))
					//|| ((Device.RuntimePlatform == Device.iOS) && (mainWidth <= 828))
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

				switch (Device.RuntimePlatform)
				{
					case Device.GTK:
						{
							StartLabelNDateTimeStack.Orientation = StackOrientation.Horizontal;
							EndLabelNDateTimeStack.Orientation = StackOrientation.Horizontal;
							StartDayName.WidthRequest = EndDayName.WidthRequest = 45;

							break;
						}
					case Device.macOS:
						{
							StartLabelNDateTimeStack.Orientation = StackOrientation.Horizontal;
							EndLabelNDateTimeStack.Orientation = StackOrientation.Horizontal;

							break;
						}
					case Device.Android:
						{
							if (portrait) // Portrait ?
							{ // Portrait
								StartLabelNDateTimeStack.Orientation = StackOrientation.Vertical;
								EndLabelNDateTimeStack.Orientation = StackOrientation.Vertical;

								if (height > nativeTotalStackHeightPortrait)
								{
									ContentPageNameScaleLast = height / nativeTotalStackHeightPortrait;
									ContentPageName.Scale = ContentPageNameScaleLast;
								}
							}
							else
							{ // Landscape
								StartLabelNDateTimeStack.Orientation = StackOrientation.Horizontal;
								EndLabelNDateTimeStack.Orientation = StackOrientation.Horizontal;

								if (width > nativeTotalStackWidthLandscape)
								{
									ContentPageNameScaleLast = width / nativeTotalStackWidthLandscape;
									ContentPageName.Scale = ContentPageNameScaleLast;
								}
								else if (width < 659.0f)
								{
									scrollViewNameScaleLast = width / scrollViewName.Width;
									scrollViewName.Scale = scrollViewNameScaleLast;
									TotalStackName.Scale = width / TotalStackName.Width;
									double entriesOuterGridScale = width / entriesOuterGrid.Width;
									double entriesOuterGridScaleY = 0.9f;
									entriesOuterGrid.Scale = entriesOuterGridScale;
									entriesOuterGrid.ScaleY = entriesOuterGridScaleY;
									StartDateTimeStacAndPlus.Scale = entriesOuterGridScale;
									StartDateTimeStacAndPlus.ScaleY = entriesOuterGridScaleY;
									EndDateTimeAndCalculateAndClearAllButtonsStackName.Scale = entriesOuterGridScale;
									EndDateTimeAndCalculateAndClearAllButtonsStackName.ScaleY = entriesOuterGridScaleY;
								}
							}
							scrollViewName.ScrollToAsync(TotalStackName, ScrollToPosition.Center, false);

							StartDayName.WidthRequest = EndDayName.WidthRequest = 50;

							break;
						}
					case Device.iOS:
						{
							if (portrait) // Portrait ?
							{ // Portrait
								StartLabelNDateTimeStack.Orientation = StackOrientation.Vertical;
								EndLabelNDateTimeStack.Orientation = StackOrientation.Vertical;

								if (DeviceInfo.Idiom == DeviceIdiom.Tablet)
								{
									scrollViewNameScaleLast = width * 0.7f / scrollViewName.Width;
									scrollViewName.Scale = scrollViewNameScaleLast;
								}
								else if ((mainHeight / mainWidth) >= 2.16f)
								{
									scrollViewNameScaleLast = width / scrollViewName.Width;
									scrollViewName.Scale = scrollViewNameScaleLast;
								}
								else if (width <= 320f)
								{
									scrollViewNameScaleLast = width / scrollViewName.Width;
									scrollViewName.Scale = scrollViewNameScaleLast;
									TotalStackName.Scale = width * 0.5f / TotalStackName.Width;
								}
								else
								{
									scrollViewNameScaleLast = width / scrollViewName.Width;
									scrollViewName.Scale = scrollViewNameScaleLast;
									TotalStackName.Scale = width * 0.7f / TotalStackName.Width;
								}
							}
							else
							{ // Landscape
								StartLabelNDateTimeStack.Orientation = StackOrientation.Horizontal;
								EndLabelNDateTimeStack.Orientation = StackOrientation.Horizontal;

								if (DeviceInfo.Idiom == DeviceIdiom.Tablet)
								{
									scrollViewNameScaleLast = width / scrollViewName.Width;
									scrollViewName.Scale = scrollViewNameScaleLast;
								}
								else if ((mainWidth / mainHeight) >= 2.16f)
								{
									scrollViewNameScaleLast = width * 0.9f / scrollViewName.Width;
									scrollViewName.Scale = scrollViewNameScaleLast;
								}
								else if (height <= 320f)
								{
									scrollViewNameScaleLast = width / scrollViewName.Width;
									scrollViewName.Scale = scrollViewNameScaleLast;
									TotalStackName.Scale = width / TotalStackName.Width;
									double entriesOuterGridScale = width * 0.55f / entriesOuterGrid.Width;
									double entriesOuterGridScaleY = 1.05f;
									entriesOuterGrid.Scale = entriesOuterGridScale;
									entriesOuterGrid.ScaleY = entriesOuterGridScaleY;
									StartDateTimeStacAndPlus.Scale = entriesOuterGridScale;
									StartDateTimeStacAndPlus.ScaleY = entriesOuterGridScaleY;
									EndDateTimeAndCalculateAndClearAllButtonsStackName.Scale = entriesOuterGridScale;
									EndDateTimeAndCalculateAndClearAllButtonsStackName.ScaleY = entriesOuterGridScaleY;
								}
								else
								{
									scrollViewNameScaleLast = width / scrollViewName.Width;
									scrollViewName.Scale = scrollViewNameScaleLast;
									TotalStackName.Scale = width * 0.9f / TotalStackName.Width;
								}
							}

							scrollViewName.ScrollToAsync(TotalStackName, ScrollToPosition.Start, true);
							scrollViewName.ScrollToAsync(TotalStackName, ScrollToPosition.End, true);
							scrollViewName.ScrollToAsync(TotalStackName, ScrollToPosition.Center, true);

							break;
						}
					case Device.UWP:
						{
							StartLabelNDateTimeStack.Orientation = StackOrientation.Horizontal;
							EndLabelNDateTimeStack.Orientation = StackOrientation.Horizontal;

							if (DependencyService.Get<IPlatformInterface>().IsMobile())
							{
								if (portrait) // Portrait ?
								{ // Portrait
									StartLabelNDateTimeStack.Orientation = StackOrientation.Vertical;
									EndLabelNDateTimeStack.Orientation = StackOrientation.Vertical;

									if (height <= nativeTotalStackHeightPortrait) // Need scaling ?
									{
										TotalStackNameScaleLast = widthAndHightScale =
											-(2.7410270192276622436e-009 * Math.Pow(ScreenHeight, 3))
											+ (4.7754782031987597521e-006 * Math.Pow(ScreenHeight, 2))
											- (0.0013991090738610563200 * ScreenHeight)
											+ 0.49946777681408938143;
										TotalStackName.Scale = TotalStackNameScaleLast;

										TotalStackName.TranslationX = 0;
										TotalStackName.TranslationY =
											(3.3707997844973771142e-005 * Math.Pow(ScreenHeight, 3))
											- (0.066636967320806955728 * Math.Pow(ScreenHeight, 2))
											+ (43.568112848719657393 * ScreenHeight)
											- 9425.4397956508601055;

										StartDateTimeIntroLabelName.FontSize = EndDateTimeIntroLabelName.FontSize
												= StartDateTimeIntroLabelNameFontSizeOrig * widthAndHightScale / 1.5;
										StartDayName.FontSize = EndDayName.FontSize = StartEndDayNameFontSizeOrig * widthAndHightScale / 1.5;
									}
								}
								else
								{ // Landscape
									StartLabelNDateTimeStack.Orientation = StackOrientation.Horizontal;
									EndLabelNDateTimeStack.Orientation = StackOrientation.Horizontal;

									if (width <= nativeTotalStackWidthLandscape) // Need scaling ?
									{
										TotalStackNameScaleLast =
											-(1.0433447427359796688e-007 * Math.Pow(ScreenWidth, 3))
											+ (0.00020154923472775974880 * Math.Pow(ScreenWidth, 2))
											- (0.12705258908531044670 * ScreenWidth)
											+ 26.859746894086349300;
										TotalStackName.Scale = TotalStackNameScaleLast;

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
							else
							{ // NOT Mobile
								StartLabelNDateTimeStack.Orientation = StackOrientation.Horizontal;
								EndLabelNDateTimeStack.Orientation = StackOrientation.Horizontal;
							}

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

			if (CalcStartDateSwitchIsOn)
			{
				//MacStartDatePicker.IsEnabled = false;
				//MacStartTimePicker.IsEnabled = false;
				//StartDatePicker.IsEnabled = false;
				//StartTimePicker.IsEnabled = false;

				StartDateTimeNowButton.IsEnabled = false;

				DoClearAll();
				EnableAndToggleOffAllSwitchedXceptMe((Switch)sender);

				LabelEqual.Text = "-";
				LabelPlus.Text = "=";
			}
			else
			{
				MacStartDatePicker.IsEnabled = true;
				MacStartTimePicker.IsEnabled = true;
				StartDatePicker.IsEnabled = true;
				StartTimePicker.IsEnabled = true;
				StartDateTimeNowButton.IsEnabled = true;
			}
		}

		private void CheckSetEndDateTime()
		{
			if (EndDateIn < StartDateIn)
			{
				EndDateIn = StartDateIn;
				EndTimeIn = StartTimeIn;
				SetEndDateTime();
			}
			else
			{
				if ((EndDateIn == StartDateIn) && (EndTimeIn < StartTimeIn))
				{
					EndTimeIn = StartTimeIn;
					SetEndDateTime();
				}
			}
		}

		private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
		{
			StartDateIn = e.NewDate;

			MacStartDatePicker.Date = StartDateIn;

			StartDayName.Text = StartDateIn.DayOfWeek.ToString().Remove(3);

			CheckSetEndDateTime();
		}

		private void OnMacStartDatePickerDateSelected(object sEnder, DateChangedEventArgs e)
		{
			StartDateIn = e.NewDate;

			StartDatePicker.Date = StartDateIn;

			StartDayName.Text = StartDateIn.DayOfWeek.ToString().Remove(3);

			CheckSetEndDateTime();
		}

		private void OnMacStartTimePickerPropertyChanged(object sEnder, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Time")
			{
				StartTimeIn = MacStartTimePicker.Time;

				StartTimePicker.Time = StartTimeIn;
				//StartTimePicker.Time = new TimeSpan(StartTimeIn.Hours, StartTimeIn.Minutes, 0);

				CheckSetEndDateTime();
			}
		}
		private void GtkStartTime_SelectedIndexChanged(object sender, EventArgs e)
		{
			StartTimeIn = new TimeSpan(GtkStartHourPicker.SelectedIndex, GtkStartMinutsPicker.SelectedIndex, 0);

			CheckSetEndDateTime();
		}

		private void StartTimePicker_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Time")
			{
				StartTimeIn = StartTimePicker.Time;

				if
				(
					(MacStartTimePicker.Time.Hours != StartTimeIn.Hours)
					||
					(MacStartTimePicker.Time.Minutes != StartTimeIn.Minutes)
				)
				{
					MacStartTimePicker.Time = StartTimeIn;
				}

				CheckSetEndDateTime();
			}
		}

		private void OnStartDateTimeNowButtonClicked(object sEnder, EventArgs args)
		{
			StartDateIn = DateTime.Now.Date;
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
			if ((CombndYears.Text.Length != 0) && !int.TryParse(CombndYears.Text, out CombndYearsIn))
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
			if ((CombndMonths.Text.Length != 0) && !int.TryParse(CombndMonths.Text, out CombndMonthsIn))
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
			if ((CombndWeeks.Text.Length != 0) && !int.TryParse(CombndWeeks.Text, out CombndWeeksIn))
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
			if ((CombndDays.Text.Length != 0) && !int.TryParse(CombndDays.Text, out CombndDaysIn))
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
			if ((CombndHours.Text.Length != 0) && !int.TryParse(CombndHours.Text, out CombndHoursIn))
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
			if ((CombndMinutes.Text.Length != 0) && !int.TryParse(CombndMinutes.Text, out CombndMinutesIn))
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
			if ((TotYears.Text.Length != 0) && !Int32.TryParse(TotYears.Text, out TotYearsIn))
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
			if ((TotMonths.Text.Length != 0) && !int.TryParse(TotMonths.Text, out TotMonthsIn))
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
			if ((TotWeeks.Text.Length != 0) && !int.TryParse(TotWeeks.Text, out TotWeeksIn))
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
			if ((TotDays.Text.Length != 0) && !int.TryParse(TotDays.Text, out TotDaysIn))
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
			if ((TotHours.Text.Length != 0) && !int.TryParse(TotHours.Text, out TotHoursIn))
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
			if ((TotMinutes.Text.Length != 0) && !int.TryParse(TotMinutes.Text, out TotMinutesIn))
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

			if (CalcEndDateSwitchIsOn)
			{
				//MacEndDatePicker.IsEnabled = false;
				//MacEndTimePicker.IsEnabled = false;
				//EndDatePicker.IsEnabled = false;
				//EndTimePicker.IsEnabled = false;

				EndDateTimeNowButton.IsEnabled = false;

				DoClearAll();
				EnableAndToggleOffAllSwitchedXceptMe((Switch)sender);

				LabelEqual.Text = "=";
				LabelPlus.Text = "+";

			}
			else
			{
				MacEndDatePicker.IsEnabled = true;
				MacEndTimePicker.IsEnabled = true;
				EndDatePicker.IsEnabled = true;
				EndTimePicker.IsEnabled = true;
				EndDateTimeNowButton.IsEnabled = true;
			}

		}


		private void CheckSetStartDateTime()
		{
			if (StartDateIn > EndDateIn)
			{
				StartDateIn = EndDateIn;
				StartTimeIn = EndTimeIn;
				SetStartDateTime();
			}
			else
			{
				if ((StartDateIn == EndDateIn) && (StartTimeIn > EndTimeIn))
				{
					StartTimeIn = EndTimeIn;
					SetStartDateTime();
				}
			}
		}

		private void EndDatePicker_DateSelected(object sender, DateChangedEventArgs e)
		{
			EndDateIn = e.NewDate;

			MacEndDatePicker.Date = EndDateIn;

			EndDayName.Text = EndDateIn.DayOfWeek.ToString().Remove(3);

			CheckSetStartDateTime();
		}

		private void OnMacEndDatePickerDateSelected(object sEnder, DateChangedEventArgs e)
		{
			EndDateIn = e.NewDate;

			EndDatePicker.Date = EndDateIn;

			EndDayName.Text = EndDateIn.DayOfWeek.ToString().Remove(3);

			CheckSetStartDateTime();
		}

		private void OnMacEndTimePickerPropertyChanged(object sEnder, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Time")
			{
				EndTimeIn = MacEndTimePicker.Time;

				EndTimePicker.Time = EndTimeIn;
				//EndTimePicker.Time = new TimeSpan(EndTimeIn.Hours, EndTimeIn.Minutes, 0);

				CheckSetStartDateTime();
			}
		}

		private void GtkEndTime_SelectedIndexChanged(object sender, EventArgs e)
		{
			EndTimeIn = new TimeSpan(GtkEndHourPicker.SelectedIndex, GtkEndMinutsPicker.SelectedIndex, 0);

			CheckSetStartDateTime();
		}

		private void EndTimePicker_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Time")
			{
				EndTimeIn = EndTimePicker.Time;

				if
				(
					(MacEndTimePicker.Time.Hours != EndTimeIn.Hours)
					||
					(MacEndTimePicker.Time.Minutes != EndTimeIn.Minutes)
				)
				{
					MacEndTimePicker.Time = EndTimeIn;
				}

				CheckSetStartDateTime();
			}
		}

		private void OnEndDateTimeNowButtonClicked(object sEnder, EventArgs args)
		{
			EndDateIn = DateTime.Now.Date;
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
			if (CombndMonthsOut < 0)
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
			if (CombndDaysOut < 0)
			{
				CombndMonthsOut--;
				if (CombndMonthsOut < 0)
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
			if (TotDaysOut > 9999999999)
			{
				await DisplayAlert("Total \"Days\" > 9999999999", TotDays.ToString(), "OK");
			}
			TotWeeks.Text = TotWeeksOut.ToString();
			TotMonths.Text = TotMonthsOut.ToString();
			TotYears.Text = TotYearsOut.ToString();
			TotHours.Text = TotHoursOut.ToString();
			if (TotHoursOut > 9999999999)
			{
				await DisplayAlert("Total \"Hours\" > 9999999999", TotHours.ToString(), "OK");
			}
			TotMinutes.Text = TotMinutesOut.ToString();
			if (TotMinutesOut > 9999999999)
			{
				await DisplayAlert("Total \"Minutes\" > 9999999999", TotMinutes.ToString(), "OK");
			}
		}

		private void OnCalculateButtonClicked(object sEnder, EventArgs e)
		{
			CalculateButton.Focus();
			DoCalculate();
		}

		private async void DoCalculate()
		{
			if (Device.RuntimePlatform == Device.GTK)
			{
				StartTimeIn = new TimeSpan(GtkStartHourPicker.SelectedIndex, GtkStartMinutsPicker.SelectedIndex, 0);
				EndTimeIn = new TimeSpan(GtkEndHourPicker.SelectedIndex, GtkEndMinutsPicker.SelectedIndex, 0);
			}
			else
			{
				StartTimeIn = StartTimePicker.Time;
				EndTimeIn = EndTimePicker.Time;
			}

			StartDateIn = StartDatePicker.Date;
			EndDateIn = EndDatePicker.Date;

			// Input values
			EndDateTimeIn = EndDateIn + EndTimeIn;
			StartDateTimeIn = StartDateIn + StartTimeIn;

			// Output values
			StartDateTimeOut = DateTime.MaxValue;
			EndDateTimeOut = DateTime.MaxValue;

			if (CalcYMWDHMIsOn)
			{
				CalcAndShowTimeSpans();
			}
			else
			{ // !CalcYMWDHMIsOn
			  // Read all controls
			  // Combined
				if ((CombndYears.Text.Length != 0) && !int.TryParse(CombndYears.Text, out CombndYearsIn))
				{
					CombndYearsIn = 0;
					string TextHolder = CombndYears.Text;
					CombndYears.Text = "";
					await DisplayAlert("Invalid \"Combined Years\" ", TextHolder, "OK");
					CombndYears.Focus();
				}
				if ((CombndMonths.Text.Length != 0) && !int.TryParse(CombndMonths.Text, out CombndMonthsIn))
				{
					CombndMonthsIn = 0;
					var TextHolder = CombndMonths.Text;
					CombndMonths.Text = "";
					await DisplayAlert("Invalid \"Combined Months\" ", TextHolder, "OK");
					CombndMonths.Focus();
				}
				if ((CombndWeeks.Text.Length != 0) && !int.TryParse(CombndWeeks.Text, out CombndWeeksIn))
				{
					CombndWeeksIn = 0;
					var TextHolder = CombndWeeks.Text;
					CombndWeeks.Text = "";
					await DisplayAlert("Invalid \"Combined Weeks\" ", TextHolder, "OK");
					CombndWeeks.Focus();
				}
				if ((CombndDays.Text.Length != 0) && !int.TryParse(CombndDays.Text, out CombndDaysIn))
				{
					CombndDaysIn = 0;
					var TextHolder = CombndDays.Text;
					CombndDays.Text = "";
					await DisplayAlert("Invalid \"Combined Days\" ", TextHolder, "OK");
					CombndDays.Focus();
				}
				if ((CombndHours.Text.Length != 0) && !int.TryParse(CombndHours.Text, out CombndHoursIn))
				{
					CombndHoursIn = 0;
					var TextHolder = CombndHours.Text;
					CombndHours.Text = "";
					await DisplayAlert("Invalid \"Combined Hours\" ", TextHolder, "OK");
					CombndHours.Focus();
				}
				if ((CombndMinutes.Text.Length != 0) && !int.TryParse(CombndMinutes.Text, out CombndMinutesIn))
				{
					CombndMinutesIn = 0;
					var TextHolder = CombndMinutes.Text;
					CombndMinutes.Text = "";
					await DisplayAlert("Invalid \"Combined Minutes\" ", TextHolder, "OK");
					CombndMinutes.Focus();
				}
				// Total
				if ((TotYears.Text.Length != 0) && !Int32.TryParse(TotYears.Text, out TotYearsIn))
				{
					TotYearsIn = 0;
					var TextHolder = TotYears.Text;
					TotYears.Text = "";
					await DisplayAlert("Invalid \"Total Years\" ", TextHolder, "OK");
					TotYears.Focus();
				}
				if ((TotMonths.Text.Length != 0) && !Int32.TryParse(TotMonths.Text, out TotMonthsIn))
				{
					TotMonthsIn = 0;
					var TextHolder = TotMonths.Text;
					TotMonths.Text = "";
					await DisplayAlert("Invalid \"Total Months\" ", TextHolder, "OK");
					TotMonths.Focus();
				}
				if ((TotWeeks.Text.Length != 0) && !Int32.TryParse(TotWeeks.Text, out TotWeeksIn))
				{
					TotWeeksIn = 0;
					var TextHolder = TotWeeks.Text;
					TotWeeks.Text = "";
					await DisplayAlert("Invalid \"Total Weeks\" ", TextHolder, "OK");
					TotWeeks.Focus();
				}
				if ((TotDays.Text.Length != 0) && !Int32.TryParse(TotDays.Text, out TotDaysIn))
				{
					TotDaysIn = 0;
					var TextHolder = TotDays.Text;
					TotDays.Text = "";
					await DisplayAlert("Invalid \"Total Days\" ", TextHolder, "OK");
					TotDays.Focus();
				}
				if ((TotHours.Text.Length != 0) && !Int32.TryParse(TotHours.Text, out TotHoursIn))
				{
					TotHoursIn = 0;
					var TextHolder = TotHours.Text;
					TotHours.Text = "";
					await DisplayAlert("Invalid \"Total Hours\" ", TextHolder, "OK");
					TotHours.Focus();
				}
				if ((TotMinutes.Text.Length != 0) && !Int32.TryParse(TotMinutes.Text, out TotMinutesIn))
				{
					TotMinutesIn = 0;
					var TextHolder = TotMinutes.Text;
					TotMinutes.Text = "";
					await DisplayAlert("Invalid \"Total Minutes\" ", TextHolder, "OK");
					TotMinutes.Focus();
				}
			} // if (CalcYMWDHMIsOn) ..else


			if (CalcEndDateSwitchIsOn)
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

				if (!TotChk || !CombndChk)
				{
					if (TotChk || CombndChk)
					{
						EndDateTimeOut = DateTime.MaxValue; // <=> no EndDateTimeOut found

						if (!TotChk)
						{
							if (TotYearsIn != 0)
							{
								if ((TotMonthsIn == 0) &&
									(TotWeeksIn == 0) &&
									(TotDaysIn == 0) &&
									(TotHoursIn == 0) &&
									(TotMinutesIn == 0))
								{
									try
									{
										EndDateTimeOut = StartDateTimeIn.AddYears(TotYearsIn);
									}
									catch (ArgumentOutOfRangeException outOfRange)
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
								if (TotMonthsIn != 0)
								{
									if ((TotWeeksIn == 0) &&
										(TotDaysIn == 0) &&
										(TotHoursIn == 0) &&
										(TotMinutesIn == 0))
									{
										try
										{
											EndDateTimeOut = StartDateTimeIn.AddMonths(TotMonthsIn);
										}
										catch (ArgumentOutOfRangeException outOfRange)
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
									if (TotWeeksIn != 0)
									{
										if ((TotDaysIn == 0) &&
										   (TotHoursIn == 0) &&
										   (TotMinutesIn == 0))
										{
											try
											{
												EndDateTimeOut = StartDateTimeIn.AddDays(TotWeeksIn * 7);
											}
											catch (ArgumentOutOfRangeException outOfRange)
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
										if (TotDaysIn != 0)
										{
											if ((TotHoursIn == 0) &&
												(TotMinutesIn == 0))
											{
												try
												{
													EndDateTimeOut = StartDateTimeIn.AddDays(TotDaysIn);
												}
												catch (ArgumentOutOfRangeException outOfRange)
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
											if (TotHoursIn != 0)
											{
												if (TotMinutesIn == 0)
												{
													try
													{
														EndDateTimeOut = StartDateTimeIn.AddHours(TotHoursIn);
													}
													catch (ArgumentOutOfRangeException outOfRange)
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
												if (TotMinutesIn != 0)
												{
													try
													{
														EndDateTimeOut = StartDateTimeIn.AddMinutes(TotMinutesIn);
													}
													catch (ArgumentOutOfRangeException outOfRange)
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

							if (CombndYearsIn != 0)
							{
								try
								{
									EndDateTimeOut = EndDateTimeOut.AddYears(CombndYearsIn);
								}
								catch (ArgumentOutOfRangeException outOfRange)
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
							if (CombndMonthsIn != 0)
							{
								try
								{
									EndDateTimeOut = EndDateTimeOut.AddMonths(CombndMonthsIn);
								}
								catch (ArgumentOutOfRangeException outOfRange)
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
							if (CombndWeeksIn != 0)
							{
								try
								{
									EndDateTimeOut = EndDateTimeOut.AddDays(CombndWeeksIn * 7);
								}
								catch (ArgumentOutOfRangeException outOfRange)
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
							if (CombndDaysIn != 0)
							{
								try
								{
									EndDateTimeOut = EndDateTimeOut.AddDays(CombndDaysIn);
								}
								catch (ArgumentOutOfRangeException outOfRange)
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
							if (CombndHoursIn != 0)
							{
								try
								{
									EndDateTimeOut = EndDateTimeOut.AddHours(CombndHoursIn);
								}
								catch (ArgumentOutOfRangeException outOfRange)
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
							if (CombndMinutesIn != 0)
							{
								try
								{
									EndDateTimeOut = EndDateTimeOut.AddMinutes(CombndMinutesIn);
								}
								catch (ArgumentOutOfRangeException outOfRange)
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

						if (EndDateTimeOut != DateTime.MaxValue)
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

			if (CalcStartDateSwitchIsOn)
			{ // CalcStartDateSwitchIsOn = true
				if (!CalcEndDateSwitchIsOn)
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

					if (!(TotChk && CombndChk))
					{
						if (!(!TotChk && !CombndChk))
						{
							StartDateTimeOut = DateTime.MaxValue; // <=> no StartDateTimeOut found

							if (!TotChk)
							{
								if (TotYearsIn != 0)
								{
									if ((TotMonthsIn == 0) &&
										(TotWeeksIn == 0) &&
										(TotDaysIn == 0) &&
										(TotHoursIn == 0) &&
										(TotMinutesIn == 0))
									{
										try
										{
											StartDateTimeOut = EndDateTimeIn.AddYears(-TotYearsIn);
										}
										catch (ArgumentOutOfRangeException outOfRange)
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
									if (TotMonthsIn != 0)
									{
										if ((TotWeeksIn == 0) &&
											(TotDaysIn == 0) &&
											(TotHoursIn == 0) &&
											(TotMinutesIn == 0))
										{
											try
											{
												StartDateTimeOut = EndDateTimeIn.AddMonths(-TotMonthsIn);
											}
											catch (ArgumentOutOfRangeException outOfRange)
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
										if (TotWeeksIn != 0)
										{
											if ((TotDaysIn == 0) &&
											   (TotHoursIn == 0) &&
											   (TotMinutesIn == 0))
											{
												try
												{
													StartDateTimeOut = EndDateTimeIn.AddDays(-(TotWeeksIn * 7));
												}
												catch (ArgumentOutOfRangeException outOfRange)
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
											if (TotDaysIn != 0)
											{
												if ((TotHoursIn == 0) &&
													(TotMinutesIn == 0))
												{
													try
													{
														StartDateTimeOut = EndDateTimeIn.AddDays(-TotDaysIn);
													}
													catch (ArgumentOutOfRangeException outOfRange)
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
												if (TotHoursIn != 0)
												{
													if (TotMinutesIn == 0)
													{
														try
														{
															StartDateTimeOut = EndDateTimeIn.AddHours(-TotHoursIn);
														}
														catch (ArgumentOutOfRangeException outOfRange)
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
													if (TotMinutesIn != 0)
													{
														try
														{
															StartDateTimeOut = EndDateTimeIn.AddMinutes(-TotMinutesIn);
														}
														catch (ArgumentOutOfRangeException outOfRange)
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

								if (CombndYearsIn != 0)
								{
									try
									{
										StartDateTimeOut = StartDateTimeOut.AddYears(-CombndYearsIn);
									}
									catch (ArgumentOutOfRangeException outOfRange)
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
								if (CombndMonthsIn != 0)
								{
									try
									{
										StartDateTimeOut = StartDateTimeOut.AddMonths(-CombndMonthsIn);
									}
									catch (ArgumentOutOfRangeException outOfRange)
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
								if (CombndWeeksIn != 0)
								{
									try
									{
										StartDateTimeOut = StartDateTimeOut.AddDays(-(CombndWeeksIn * 7));
									}
									catch (ArgumentOutOfRangeException outOfRange)
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
								if (CombndDaysIn != 0)
								{
									try
									{
										StartDateTimeOut = StartDateTimeOut.AddDays(-CombndDaysIn);
									}
									catch (ArgumentOutOfRangeException outOfRange)
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
								if (CombndHoursIn != 0)
								{
									try
									{
										StartDateTimeOut = StartDateTimeOut.AddHours(-CombndHoursIn);
									}
									catch (ArgumentOutOfRangeException outOfRange)
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
								if (CombndMinutesIn != 0)
								{
									try
									{
										StartDateTimeOut = StartDateTimeOut.AddMinutes(-CombndMinutesIn);
									}
									catch (ArgumentOutOfRangeException outOfRange)
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

							if (StartDateTimeOut != DateTime.MaxValue)
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
			await Navigation.PushAsync(new AboutHelp(), true);

			//	+ Environment.NewLine + Environment.NewLine
			//	+ "Test"
			//	;
			//await DisplayAlert("Application", AppTitleAndVersion, "OK");

		}

		private void CalcYMWDHM_toggeled(object sender, ToggledEventArgs e)
		{
			CalcYMWDHMIsOn = e.Value;
			if (CalcYMWDHMIsOn)
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

		// Calendar
		private string CalendarItem = "";
		private bool CorrectForIcsTimeZone = false;

		private async void On_OpenIcsMessageReceived(App arg1, OpenIcsMessageArgs arg2)
		{
			CorrectForIcsTimeZone = arg2.CorrectForTimeZone;

			await DependencyService.Get<IHandleFiles>().SelectFilesToReadFrom(new string[] { "ics" });

			await Navigation.PopAsync(true);
		}

		private async void On_FileToReadFromSelectedAsync(App arg1, SelectFileResultMessageArgs arg2)
		{
			if (arg2.DidPick)
			{

				List<string> TheIcsTxt = new List<string>();
				try
				{
					// Create an instance of StreamReader to read from a file.
					// The using statement also closes the StreamReader.
					using (StreamReader sr = new StreamReader(arg2.TheSelectedFileInfo.TheStream))
					{
						string line;
						// Read and display lines from the file until the end of
						// the file is reached.
						while ((line = sr.ReadLine()) != null)
						{
							TheIcsTxt.Add(line);
						}
					}
				}
				catch (Exception e)
				{
					// Let the user know what went wrong.
					await DisplayAlert
							   (
								   "The file could not be read:"
								   , e.Message
								   , "OK"
							   );
				}

				SwitchCalcYMWDHM.IsToggled = true;

				try
				{
					// Time Zone
					var IdxBEGIN_STANDARD = TheIcsTxt.FindIndex(s => s.Contains(@"BEGIN:STANDARD"));
					var IdxEND_STANDARD = TheIcsTxt.FindIndex(s => s.Contains(@"END:STANDARD"));
					var LgthSTANDARD = IdxEND_STANDARD - IdxBEGIN_STANDARD;
					var TimeIDX = TheIcsTxt.FindIndex(IdxBEGIN_STANDARD, LgthSTANDARD, s => s.Contains(@"TZOFFSETTO:"));
					int SignIdx = TheIcsTxt[TimeIDX].IndexOfAny("+-".ToCharArray(), TheIcsTxt[TimeIDX].LastIndexOf(':'));
					var TheSign = TheIcsTxt[TimeIDX][SignIdx];
					var StartOfTimeStringIDX = ++SignIdx;
					var LgthOfTimestring = TheIcsTxt[TimeIDX].Length - StartOfTimeStringIDX;
					var TimeString = TheIcsTxt[TimeIDX].Substring(StartOfTimeStringIDX, LgthOfTimestring);

					var TheTZOFFSETTO = TimeSpan.ParseExact(TimeString, "hhmm", null);
					if (TheSign == '-')
					{
						TheTZOFFSETTO = TimeSpan.Zero - TheTZOFFSETTO;
					}
					var BaseUtcOff = TimeZoneInfo.Local.BaseUtcOffset;

					// Start Time
					TimeIDX = TheIcsTxt.FindIndex(s => s.Contains(@"DTSTART;TZID="));
					StartOfTimeStringIDX = TheIcsTxt[TimeIDX].LastIndexOf(':') + 1;
					LgthOfTimestring = TheIcsTxt[TimeIDX].Length - StartOfTimeStringIDX;
					TimeString = TheIcsTxt[TimeIDX].Substring(StartOfTimeStringIDX, LgthOfTimestring);
					StartDateTimeOut = DateTime.ParseExact(TimeString, @"yyyyMMddTHHmm00", null);

					if (CorrectForIcsTimeZone)
					{
						StartDateTimeOut -= TheTZOFFSETTO; // Calender start time in utc time
						StartDateTimeOut += BaseUtcOff; // In local time zone time
					}

					StartDateTimeIn = StartDateTimeOut;
					StartDateIn = StartDateTimeOut.Date;
					StartTimeIn = StartDateTimeOut.TimeOfDay;

					SetStartDateTime();

					// End Date Time
					TimeIDX = TheIcsTxt.FindIndex(s => s.Contains(@"DTEND;TZID="));
					StartOfTimeStringIDX = TheIcsTxt[TimeIDX].LastIndexOf(':') + 1;
					LgthOfTimestring = TheIcsTxt[TimeIDX].Length - StartOfTimeStringIDX;
					TimeString = TheIcsTxt[TimeIDX].Substring(StartOfTimeStringIDX, LgthOfTimestring);
					EndDateTimeOut = DateTime.ParseExact(TimeString, @"yyyyMMddTHHmm00", null);

					if (CorrectForIcsTimeZone)
					{
						EndDateTimeOut -= TheTZOFFSETTO; // Calender End time in utc time
						EndDateTimeOut += BaseUtcOff; // In local time zone time
					}

					EndDateTimeIn = EndDateTimeOut;
					EndDateIn = EndDateTimeOut.Date;
					EndTimeIn = EndDateTimeOut.TimeOfDay;

					SetEndDateTime();

					// Show Time Spans.
					CalcAndShowTimeSpans();

				}
				catch (Exception e)
				{
					await DisplayAlert
							   (
								   "Bad .ics file"
								   , e.Message
								   , "OK"
							   );
				}
			}

			await Navigation.PopAsync(true);

		}

		string SuggestedNameOfFileToSaveTo = "";
		private async void On_SaveToIcsMessageReceived(App arg1, SaveToIcsMessageArgs arg2)
		{
			DateTime DateStart = StartDateIn + StartTimeIn;
			DateTime DateEnd = EndDateIn + EndTimeIn;
			string Summary = arg2.EventName_Summary;
			string Location = arg2.Location;
			string Description = arg2.TheDescription;
			//string FileName = "CalendarItem";

			//create a new stringbuilder instance
			StringBuilder sb = new StringBuilder();

			//start the calendar item
			sb.AppendLine("BEGIN:VCALENDAR");
			sb.AppendLine("VERSION:2.0");
			sb.AppendLine("PRODID:eksit.dk");
			//sb.AppendLine("CALSCALE:GREGORIAN");
			sb.AppendLine("METHOD:PUBLISH");

#if true // USE_LOCAL_TIME
			var TimeZoneName = TimeZoneInfo.Local.StandardName;
			var systemTimeZoneName = TimeZoneInfo.GetSystemTimeZones();
			var IsDaylightsavingtimeOn = TimeZoneInfo.Local.IsDaylightSavingTime(DateTime.Now);
			var UtcOffset = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now);
			var UtcOffsetStr = UtcOffset.ToString("hhmm");
			if (UtcOffset.Hours >= 0)
			{
				UtcOffsetStr = "+" + UtcOffsetStr;
			}
			else
			{
				UtcOffsetStr = "-" + UtcOffsetStr;
			}

			var BaseUtcOff = TimeZoneInfo.Local.BaseUtcOffset;
			var BaseUtcOffStr = BaseUtcOff.ToString("hhmm");
			if (BaseUtcOff.Hours >= 0)
			{
				BaseUtcOffStr = "+" + BaseUtcOffStr;
			}
			else
			{
				BaseUtcOffStr = "-" + BaseUtcOffStr;
			}
#else // USE_LOCAL_TIME (USE "Central Standard Time")
			TimeZoneInfo cst = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
			var TimeZoneName = cst.StandardName;
			var UtcOffset = cst.GetUtcOffset(DateTime.Now);
			var UtcOffsetStr = UtcOffset.ToString("hhmm");
			if (UtcOffset.Hours >= 0)
			{
				UtcOffsetStr = "+" + UtcOffsetStr;
			}
			else
			{
				UtcOffsetStr = "-" + UtcOffsetStr;
			}

			var BaseUtcOff = cst.BaseUtcOffset;
			var BaseUtcOffStr = BaseUtcOff.ToString("hhmm");
			if (BaseUtcOff.Hours >= 0)
			{
				BaseUtcOffStr = "+" + BaseUtcOffStr;
			}
			else
			{
				BaseUtcOffStr = "-" + BaseUtcOffStr;
			}
#endif // USE_LOCAL_TIME

			sb.AppendLine("BEGIN:VTIMEZONE");
			sb.AppendLine("TZID:" + TimeZoneName);

			sb.AppendLine("BEGIN:STANDARD");
			sb.AppendLine("TZOFFSETFROM:" + UtcOffsetStr);
			sb.AppendLine("TZOFFSETTO:" + BaseUtcOffStr);
			sb.AppendLine("END:STANDARD");

			sb.AppendLine("BEGIN:DAYLIGHT");
			sb.AppendLine("TZOFFSETFROM:" + BaseUtcOffStr);
			sb.AppendLine("TZOFFSETTO:" + UtcOffsetStr);
			sb.AppendLine("END:DAYLIGHT");

			sb.AppendLine("END:VTIMEZONE");

			//add the event
			sb.AppendLine("BEGIN:VEVENT");

			//with time zone specified
			sb.AppendLine("DTSTART;TZID=" + "\"" + TimeZoneName + "\":" + DateStart.ToString("yyyyMMddTHHmm00"));
			sb.AppendLine("DTEND;TZID=" + "\"" + TimeZoneName + "\":" + DateEnd.ToString("yyyyMMddTHHmm00"));
			//or without
			//sb.AppendLine("DTSTART:" + DateStart.ToString("yyyyMMddTHHmm00"));
			//sb.AppendLine("DTEND:" + DateEnd.ToString("yyyyMMddTHHmm00"));

			sb.AppendLine("SUMMARY:" + Summary + "");
			sb.AppendLine("LOCATION:" + Location + "");
			sb.AppendLine("DESCRIPTION:" + Description + "");
			sb.AppendLine("PRIORITY:5");

			sb.AppendLine("END:VEVENT");

			//end calendar item
			sb.AppendLine("END:VCALENDAR");

			CalendarItem = sb.ToString().Replace("\r", "");
			//send the calendar item to the browser
			//Response.ClearHeaders();
			//Response.Clear();
			//Response.Buffer = true;
			//Response.ContentType = "text/calendar";
			//Response.AddHeader("content-length", CalendarItem.Length.ToString());
			//Response.AddHeader("content-disposition", "attachment; filename=\"" + FileName + ".ics\"");
			//Response.Write(CalendarItem);
			//Response.Flush();
			//HttpContext.Current.ApplicationInstance.CompleteRequest();


			string[] filetypesToSaveTo = new string[] { "ics" };
			SuggestedNameOfFileToSaveTo = Summary;
			await DependencyService.Get<IHandleFiles>().SelectFilesToSaveTo(SuggestedNameOfFileToSaveTo, filetypesToSaveTo, MessengerKeys.FileToSaveToSelected);

			await Navigation.PopAsync(true);
		}

		private async void FileButton_Clicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new FileICS(), true);
		}

		private async void On_FileToSaveToSelected(App arg1, SelectFileResultMessageArgs arg2)
		{
			if (arg2.DidPick)
			{
				_ = await DependencyService.Get<IHandleFiles>().SaveToTextFile(arg2.TheSelectedFileInfo.TheStream, CalendarItem);

				// Close file
				arg2.TheSelectedFileInfo.TheStream.Dispose();
			}

			await Navigation.PopAsync(true);

		}

		private void On_FileToSaveRawTextToSelected(App arg1, SelectFileResultMessageArgs arg2)
		{
		}

	}

}
