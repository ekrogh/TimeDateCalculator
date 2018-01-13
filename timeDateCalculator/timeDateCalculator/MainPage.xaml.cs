using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;


namespace timeDateCalculator
{
    public partial class MainPage : ContentPage
    {

        private double width;
        private double height;
        private bool firstTime = true;
        private bool firstTimeWdthOrHeightChanged = true;

        private double nativeTotalStackWidthPortrait = 576;
        private double nativeTotalStackHeightPortrait = 562;

        private double nativeTotalStackWidthLandscape = 731;
        private double nativeTotalStacHeightLandscape = 311;

        private double startDayNameWidthRequest = 0.0;
        private double startDayNameFontSize = 0.0;

        private double startDateTimeIntroLabelNameFontSizeOrig = 0.0;

        private double startEndDayNameFontSizeOrig = 0.0;

        private double totalStackNameTranslationX = -140.0;

        private double UWP4InchLandscapeWidth = 489.33334350585938;

        void doClearAll()
        {
            startDateTime.Text = "";
            startDayName.Text = "ddd";

            combndYears.Text = "";
            combndMonths.Text = "";
            combndWeeks.Text = "";
            combndDays.Text = "";
            combndHours.Text = "";
            combndMinutes.Text = "";

            totYears.Text = "";
            totMonths.Text = "";
            totWeeks.Text = "";
            totDays.Text = "";
            totHours.Text = "";
            totMinutes.Text = "";

            endDateTime.Text = "";
            endDayName.Text = "ddd";

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    {
                        startDateTime.WidthRequest = 136;

                        combndYears.WidthRequest = 105;
                        combndMonths.WidthRequest = 105;
                        combndWeeks.WidthRequest = 105;
                        combndDays.WidthRequest = 105;
                        combndHours.WidthRequest = 105;
                        combndMinutes.WidthRequest = 105;

                        totYears.WidthRequest = 105;
                        totMonths.WidthRequest = 105;
                        totWeeks.WidthRequest = 105;
                        totDays.WidthRequest = 105;
                        totHours.WidthRequest = 105;
                        totMinutes.WidthRequest = 105;

                        endDateTime.WidthRequest = 136;

                        break;
                    }
                case Device.Android:
                    {
                        startDateTime.WidthRequest = 119;

                        combndYears.WidthRequest = 88;
                        combndMonths.WidthRequest = 88;
                        combndWeeks.WidthRequest = 88;
                        combndDays.WidthRequest = 88;
                        combndHours.WidthRequest = 88;
                        combndMinutes.WidthRequest = 88;

                        totYears.WidthRequest = 88;
                        totMonths.WidthRequest = 88;
                        totWeeks.WidthRequest = 88;
                        totDays.WidthRequest = 88;
                        totHours.WidthRequest = 88;
                        totMinutes.WidthRequest = 88;

                        endDateTime.WidthRequest = 119;

                        break;
                    }
                case Device.UWP:
                    {
                        startDateTime.WidthRequest = 165;

                        combndYears.WidthRequest = 121;
                        combndMonths.WidthRequest = 121;
                        combndWeeks.WidthRequest = 121;
                        combndDays.WidthRequest = 121;
                        combndHours.WidthRequest = 121;
                        combndMinutes.WidthRequest = 121;

                        totYears.WidthRequest = 121;
                        totMonths.WidthRequest = 121;
                        totWeeks.WidthRequest = 121;
                        totDays.WidthRequest = 121;
                        totHours.WidthRequest = 121;
                        totMinutes.WidthRequest = 121;

                        endDateTime.WidthRequest = 165;

                        break;
                    }
                default: //Set as UWP
                    {
                        startDateTime.WidthRequest = 165;

                        combndYears.WidthRequest = 121;
                        combndMonths.WidthRequest = 121;
                        combndWeeks.WidthRequest = 121;
                        combndDays.WidthRequest = 121;
                        combndHours.WidthRequest = 121;
                        combndMinutes.WidthRequest = 121;

                        totYears.WidthRequest = 121;
                        totMonths.WidthRequest = 121;
                        totWeeks.WidthRequest = 121;
                        totDays.WidthRequest = 121;
                        totHours.WidthRequest = 121;
                        totMinutes.WidthRequest = 121;

                        endDateTime.WidthRequest = 165;

                        break;
                    }
            }

        }

        public MainPage()
        {
            InitializeComponent();
        }



        //async void SetScrollToAsync(double x, double y)
        //{
        //    //eventWaitHandle.WaitOne();
        //    await scrollViewName.ScrollToAsync(x, y, true);
        //}

        //EventWaitHandle eventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);

        //class TimerExampleState
        //{
        //    public int counter = 0;
        //    public Timer tmr;
        //}

        //TimerExampleState s;
        //private TimerCallback timerDelegate;
        //private Timer timer;

        ///*static*/async  void CheckTimerStatus(Object state)
        //{
        //    TimerExampleState s = (TimerExampleState)state;
        //    s.counter++;
        //    Console.WriteLine("{0} Checking Status {1}.", DateTime.Now.TimeOfDay, s.counter);
        //    if (s.counter == 5)
        //    {
        //        // Shorten the period. Wait 10 seconds to restart the timer.
        //        (s.tmr).Change(10000, 100);
        //        Console.WriteLine("changed...");
        //    }
        //    if (s.counter == 10)
        //    {
        //        Console.WriteLine("disposing of timer...");
        //        s.tmr.Dispose();
        //        s.tmr = null;
        //        eventWaitHandle.Set();
        //        //await scrollViewName.ScrollToAsync(0, 147, true);
        //        //SetScrollToAsync(0, 146);
        //    }
        //}

        //int noEqualTimesCalled = 0;

        protected override void OnSizeAllocated(double width, double height)
        {
            if (firstTime)
            {
                doClearAll();
                firstTime = false;
            }

            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {

                this.width = width;
                this.height = height;
                double widthAndHightScale;
                var landscape = false;

                if (firstTimeWdthOrHeightChanged)
                {
                    startDayNameWidthRequest = 2.0f * Math.Truncate(startDayName.Width);
                    startDayNameFontSize = startDayName.FontSize;
                    startDateTimeIntroLabelNameFontSizeOrig = startDateTimeIntroLabelName.FontSize;
                    startEndDayNameFontSizeOrig = startDayName.FontSize;
                    firstTimeWdthOrHeightChanged = false;
                }


                if (height > width) // Portrait ?
                { // Portrait
                    totalStackName.Scale = 1;

                    entriesOuterStack.Orientation = StackOrientation.Horizontal;
                    combinedTimeEntriesStack.Orientation = StackOrientation.Vertical;
                    totalTimeEntriesStack.Orientation = StackOrientation.Vertical;
                    scrollViewName.Orientation = ScrollOrientation.Vertical;

                    var stackSizeEndDateTimeAndCalculateAndClearAllButtonsStackName
                        = endDateTimeAndCalculateAndClearAllButtonsStackName.Bounds.Y + endDateTimeAndCalculateAndClearAllButtonsStackName.Bounds.Height;
                    widthAndHightScale = Math.Truncate((height * 10) / stackSizeEndDateTimeAndCalculateAndClearAllButtonsStackName) / 10;
                    //widthAndHightScale = height / stackSizeEndDateTimeAndCalculateAndClearAllButtonsStackName;
                    //widthAndHightScale = 0.7;
                }
                else
                { // Landscape
                    totalStackName.Scale = 1;
                    landscape = true;

                    entriesOuterStack.Orientation = StackOrientation.Vertical;
                    combinedTimeEntriesStack.Orientation = StackOrientation.Horizontal;
                    totalTimeEntriesStack.Orientation = StackOrientation.Horizontal;
                    scrollViewName.Orientation = ScrollOrientation.Horizontal;


                    var combndYearsWidth = combndYears.Width;
                    var totEntriesWidth = 6 * combndYearsWidth;
                    var totSpacing = ((double)(baseCmbndAndTotStackLayoutStyleName.Setters[0].Value) * 6f);
                    widthAndHightScale = (width - totSpacing)/ totEntriesWidth;
                }

                if (widthAndHightScale > 0)
                {
                    if (widthAndHightScale < 1)
                    {
                        if (Device.RuntimePlatform == Device.UWP)
                        {
                            totalStackName.Scale = Math.Truncate((widthAndHightScale * 10f)) / 10f;
                            if (height > width) // Portrait ?
                            { // Portrait
                                totalStackName.TranslationX = 0; totalStackName.TranslationY = -50;

                                startDateTimeIntroLabelName.FontSize = endDateTimeIntroLabelName.FontSize
                                    = startDateTimeIntroLabelNameFontSizeOrig * widthAndHightScale/* / 1.5*/;
                                startDayName.FontSize = endDayName.FontSize = startEndDayNameFontSizeOrig * widthAndHightScale/* / 1.5*/;
                            }
                            else
                            { // Landscape
                                totalStackName.TranslationX
                                    = (-4.8888669389253778094e-007 * Math.Pow(width, 3)) - (0.00064574454304721696542 * Math.Pow(width, 2)) + (1.8862989043510793863 * width) - 851.12468784747602513;
                                totalStackName.TranslationY = 0;
                            }
                        }
                        else
                        {
                            totalStackName.Scale = widthAndHightScale;
                        }
                    }

                    startDayName.WidthRequest = endDayName.WidthRequest = 45;
                }
            }
        }

        string FormatDateTime(string theDateTimeStringToFormat, ref string dayName)
        { // yyyyMMddHHmm -> yyyy-MM-dd HH:mm
            var theFormattedDateTime = theDateTimeStringToFormat;

            DateTime dateTimeHolder = DateTime.Now;
            try
            {
                dateTimeHolder = DateTime.Parse(theFormattedDateTime);
            }
            catch (FormatException)
            {
                if (theFormattedDateTime.Length < 12)
                {
                    for (int i = theFormattedDateTime.Length + 1; i <= 12; i++)
                    {
                        theFormattedDateTime += "0";
                    }
                }
                if (theFormattedDateTime.Length > 12)
                {
                    theFormattedDateTime = theFormattedDateTime.Remove(12);
                }

                theFormattedDateTime = theFormattedDateTime.Insert(10, ":");
                theFormattedDateTime = theFormattedDateTime.Insert(8, " ");
                theFormattedDateTime = theFormattedDateTime.Insert(6, "-");
                theFormattedDateTime = theFormattedDateTime.Insert(4, "-");

                try
                {
                    dateTimeHolder = DateTime.Parse(theFormattedDateTime);
                }
                catch (FormatException)
                {
                    DisplayAlert("Illegal Date+Time !", "", "OK");
                    theFormattedDateTime = "";
                }
            }
            dayName = dateTimeHolder.ToString("R").Remove(3);
            return theFormattedDateTime;

        }


        bool startDateTimeJustFocused = false;
        bool startDateTimeChanged = false;
        string startDateTimeContentOnFocused = "";

        void FormatStartDateTime()
        {
            if (startDateTime.Text.Length != 0)
            {
                var dayName = "";
                startDateTime.Text = FormatDateTime(startDateTime.Text, ref dayName);
                startDayName.Text = dayName;
            }
        }

        void OnStartDateTimeNowButtonClicked(object sender, EventArgs args)
        { // yyyy-MM-dd HH:mm
            startDateTimeJustFocused = false;
            startDateTimeChanged = false;
            startDateTime.Text = DateTime.Now.ToString("u").Remove(16);
            startDayName.Text = DateTime.Now.ToString("R").Remove(3);

            //SetScrollPos(0, 146);

            //DEBUG
            //var scrollViewNameScrolledX = scrollViewName.ScrollX;
            //var scrollViewNameScrolledY = scrollViewName.ScrollY;
            //DEBUG End
        }

        void OnStartDateTimeFocused(object sender, EventArgs args)
        {
            startDateTimeContentOnFocused = startDateTime.Text;
            startDateTime.Text = "";
            startDayName.Text = "ddd";
            startDateTimeJustFocused = true;
            startDateTimeChanged = false;
        }

        void OnStartDateTimeUnfocused(object sender, EventArgs args)
        { // yyyyMMddHHmm -> yyy-MM-dd HH:mm
            startDateTimeJustFocused = false;

            if (startDateTimeChanged)
            {
                startDateTimeChanged = false;
                FormatStartDateTime();
            }
            else
            {
                startDateTime.Text = startDateTimeContentOnFocused;
                FormatStartDateTime();
            }
        }

        void OnStartDateTimeTextChanged(object sender, EventArgs args)
        {
            if (startDateTimeJustFocused)
            {
                startDateTimeJustFocused = false;
                startDateTimeChanged = true;
            }
        }

        void OnStartDateTimeCompleted(object sender, EventArgs args)
        { // yyyyMMddHHmm -> yyy-MM-dd HH:mm
            FormatStartDateTime();
        }


        bool endDateTimeJustFocused = false;
        bool endDateTimeChanged = false;
        string endDateTimeContentOnFocused = "";

        void FormatEndDateTime()
        {
            if (endDateTime.Text.Length != 0)
            {
                var dayName = "";
                endDateTime.Text = FormatDateTime(endDateTime.Text, ref dayName);
                endDayName.Text = dayName;
            }
        }

        void OnEndDateTimeNowButtonClicked(object sender, EventArgs args)
        { // yyyy-MM-dd HH:mm
            endDateTimeJustFocused = false;
            endDateTimeChanged = false;
            endDateTime.Text = DateTime.Now.ToString("u").Remove(16);
            endDayName.Text = DateTime.Now.ToString("R").Remove(3);
        }

        void OnEndDateTimeFocused(object sender, EventArgs args)
        {
            endDateTimeContentOnFocused = endDateTime.Text;
            endDateTime.Text = "";
            endDayName.Text = "ddd";
            endDateTimeJustFocused = true;
            endDateTimeChanged = false;
        }

        void OnEndDateTimeUnfocused(object sender, EventArgs args)
        { // yyyyMMddHHmm -> yyyy-MM-dd HH:mm
            endDateTimeJustFocused = false;

            if (endDateTimeChanged)
            {
                endDateTimeChanged = false;
                FormatEndDateTime();
            }
            else
            {
                endDateTime.Text = endDateTimeContentOnFocused;
                FormatEndDateTime();
            }
        }

        void OnEndDateTimeTextChanged(object sender, EventArgs args)
        {
            if (endDateTimeJustFocused)
            {
                endDateTimeJustFocused = false;
                endDateTimeChanged = true;
            }
        }

        void OnEndDateTimeCompleted(object sender, EventArgs args)
        { // yyyyMMddHHmm -> yyyy-MM-dd HH:mm
            FormatEndDateTime();
        }

        void OnCalculateButtonClicked(object sender, EventArgs args)
        {
            Task<bool> task = DisplayAlert("CalculateButtonClicked", "Decide on an option",
                                           "yes or ok", "no or cancel");
        }

        void OnClearAllButtonClicked(object sender, EventArgs args)
        {
            doClearAll();
            //Task<bool> task = DisplayAlert("ClearAllButtonClicked", "Decide on an option",
            //                               "yes or ok", "no or cancel");
        }
    }
}
