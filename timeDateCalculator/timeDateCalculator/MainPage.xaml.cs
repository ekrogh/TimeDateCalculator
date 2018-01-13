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

        private double nativeTotalStackWidthLandscape = 731.0;
        private double nativeTotalStackHeightPortrait = 732.0;

        private double startDayNameWidthRequest = 0.0;
        private double startDayNameFontSize = 0.0;

        private double StartDateTimeIntroLabelNameFontSizeOrig = 0.0;

        private double startEndDayNameFontSizeOrig = 0.0;


        void doClearAll()
        {
            StartDateTime.Text = "";
            startDayName.Text = "ddd";

            CombndYears.Text = "";
            CombndMonths.Text = "";
            CombndWeeks.Text = "";
            CombndDays.Text = "";
            CombndHours.Text = "";
            CombndMinutes.Text = "";

            TotYears.Text = "";
            TotMonths.Text = "";
            TotWeeks.Text = "";
            TotDays.Text = "";
            TotHours.Text = "";
            TotMinutes.Text = "";

            EndDateTime.Text = "";
            endDayName.Text = "ddd";

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    {
                        StartDateTime.WidthRequest = 136;

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

                        EndDateTime.WidthRequest = 136;

                        break;
                    }
                case Device.Android:
                    {
                        StartDateTime.WidthRequest = 119;

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

                        EndDateTime.WidthRequest = 119;

                        break;
                    }
                case Device.UWP:
                    {
                        StartDateTime.WidthRequest = 165;

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

                        EndDateTime.WidthRequest = 165;

                        break;
                    }
                default: //Set as UWP
                    {
                        StartDateTime.WidthRequest = 165;

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

                        EndDateTime.WidthRequest = 165;

                        break;
                    }
            }

        }

        public MainPage()
        {
            InitializeComponent();
        }



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
                    StartDateTimeIntroLabelNameFontSizeOrig = StartDateTimeIntroLabelName.FontSize;
                    startEndDayNameFontSizeOrig = startDayName.FontSize;
                    firstTimeWdthOrHeightChanged = false;
                }


                if (height > width) // Portrait ?
                { // Portrait
                    TotalStackName.Scale = 1;

                    entriesOuterStack.Orientation = StackOrientation.Horizontal;
                    combinedTimeEntriesStack.Orientation = StackOrientation.Vertical;
                    TotalTimeEntriesStack.Orientation = StackOrientation.Vertical;
                    scrollViewName.Orientation = ScrollOrientation.Vertical;

                    widthAndHightScale = height / nativeTotalStackHeightPortrait;
                }
                else
                { // Landscape
                    TotalStackName.Scale = 1;
                    landscape = true;

                    entriesOuterStack.Orientation = StackOrientation.Vertical;
                    combinedTimeEntriesStack.Orientation = StackOrientation.Horizontal;
                    TotalTimeEntriesStack.Orientation = StackOrientation.Horizontal;
                    scrollViewName.Orientation = ScrollOrientation.Horizontal;

                    widthAndHightScale = width / nativeTotalStackWidthLandscape;
                }

                if (widthAndHightScale > 0)
                {
                    if (widthAndHightScale < 1)
                    {
                        if (Device.RuntimePlatform == Device.UWP)
                        {
                            TotalStackName.Scale = Math.Truncate(widthAndHightScale * 10.0) / 10.0;

                            if (height > width) // Portrait ?
                            { // Portrait
                                TotalStackName.TranslationX = 0; TotalStackName.TranslationY = -50;

                                StartDateTimeIntroLabelName.FontSize = EndDateTimeIntroLabelName.FontSize
                                    = StartDateTimeIntroLabelNameFontSizeOrig * widthAndHightScale / 1.5;
                                startDayName.FontSize = endDayName.FontSize = startEndDayNameFontSizeOrig * widthAndHightScale /*/ 1.5*/;
                            }
                            else
                            { // Landscape

                                TotalStackName.TranslationX
                                    = (-4.8888669389253778094e-007 * Math.Pow(width, 3)) - (0.00064574454304721696542 * Math.Pow(width, 2)) + (1.8862989043510793863 * width) - 851.12468784747602513;
                                TotalStackName.TranslationY = 0;
                            }
                        }
                        else
                        {
                            TotalStackName.Scale = widthAndHightScale;
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


        // Start date-time...

        bool StartDateTimeJustFocused = false;
        bool StartDateTimeChanged = false;
        string StartDateTimeContentOnFocused = "";

        void FormatStartDateTime()
        {
            if (StartDateTime.Text.Length != 0)
            {
                var dayName = "";
                StartDateTime.Text = FormatDateTime(StartDateTime.Text, ref dayName);
                startDayName.Text = dayName;
            }
        }

        void OnStartDateTimeNowButtonClicked(object sender, EventArgs args)
        { // yyyy-MM-dd HH:mm
            StartDateTimeJustFocused = false;
            StartDateTimeChanged = false;
            StartDateTime.Text = DateTime.Now.ToString("u").Remove(16);
            startDayName.Text = DateTime.Now.ToString("R").Remove(3);
        }

        void OnStartDateTimeFocused(object sender, EventArgs args)
        {
            StartDateTimeContentOnFocused = StartDateTime.Text;
            StartDateTime.Text = "";
            startDayName.Text = "ddd";
            StartDateTimeJustFocused = true;
            StartDateTimeChanged = false;
        }

        void OnStartDateTimeUnfocused(object sender, EventArgs args)
        { // yyyyMMddHHmm -> yyy-MM-dd HH:mm
            StartDateTimeJustFocused = false;

            if (StartDateTimeChanged)
            {
                StartDateTimeChanged = false;
                FormatStartDateTime();
            }
            else
            {
                StartDateTime.Text = StartDateTimeContentOnFocused;
                FormatStartDateTime();
            }
        }

        void OnStartDateTimeTextChanged(object sender, EventArgs args)
        {
            if (StartDateTimeJustFocused)
            {
                StartDateTimeJustFocused = false;
                StartDateTimeChanged = true;
            }
        }

        void OnStartDateTimeCompleted(object sender, EventArgs args)
        { // yyyyMMddHHmm -> yyy-MM-dd HH:mm
            FormatStartDateTime();
        }


        //HERFRA Combined
        //Combined Years...

        bool CombndYearsJustFocused = false;
        bool CombndYearsChanged = false;
        string CombndYearsContentOnFocused = "";

        void OnCombndYearsFocused(object sender, EventArgs args)
        {
            CombndYearsContentOnFocused = CombndYears.Text;
            CombndYears.Text = "";
            CombndYearsJustFocused = true;
            CombndYearsChanged = false;
        }

        void OnCombndYearsUnfocused(object sender, EventArgs args)
        {
            CombndYearsJustFocused = false;

            if (CombndYearsChanged)
            {
                CombndYearsChanged = false;
            }
            else
            {
                CombndYears.Text = CombndYearsContentOnFocused;
            }
        }

        void OnCombndYearsTextChanged(object sender, EventArgs args)
        {
            if (CombndYearsJustFocused)
            {
                CombndYearsJustFocused = false;
                CombndYearsChanged = true;
            }
        }

        void OnCombndYearsCompleted(object sender, EventArgs args)
        { 
        }


        //Combined Months...

        bool CombndMonthsJustFocused = false;
        bool CombndMonthsChanged = false;
        string CombndMonthsContentOnFocused = "";

        void OnCombndMonthsFocused(object sender, EventArgs args)
        {
            CombndMonthsContentOnFocused = CombndMonths.Text;
            CombndMonths.Text = "";
            CombndMonthsJustFocused = true;
            CombndMonthsChanged = false;
        }

        void OnCombndMonthsUnfocused(object sender, EventArgs args)
        {
            CombndMonthsJustFocused = false;

            if (CombndMonthsChanged)
            {
                CombndMonthsChanged = false;
            }
            else
            {
                CombndMonths.Text = CombndMonthsContentOnFocused;
            }
        }

        void OnCombndMonthsTextChanged(object sender, EventArgs args)
        {
            if (CombndMonthsJustFocused)
            {
                CombndMonthsJustFocused = false;
                CombndMonthsChanged = true;
            }
        }

        void OnCombndMonthsCompleted(object sender, EventArgs args)
        {
        }


        //Combined Weeks...

        bool CombndWeeksJustFocused = false;
        bool CombndWeeksChanged = false;
        string CombndWeeksContentOnFocused = "";

        void OnCombndWeeksFocused(object sender, EventArgs args)
        {
            CombndWeeksContentOnFocused = CombndWeeks.Text;
            CombndWeeks.Text = "";
            CombndWeeksJustFocused = true;
            CombndWeeksChanged = false;
        }

        void OnCombndWeeksUnfocused(object sender, EventArgs args)
        {
            CombndWeeksJustFocused = false;

            if (CombndWeeksChanged)
            {
                CombndWeeksChanged = false;
            }
            else
            {
                CombndWeeks.Text = CombndWeeksContentOnFocused;
            }
        }

        void OnCombndWeeksTextChanged(object sender, EventArgs args)
        {
            if (CombndWeeksJustFocused)
            {
                CombndWeeksJustFocused = false;
                CombndWeeksChanged = true;
            }
        }

        void OnCombndWeeksCompleted(object sender, EventArgs args)
        {
        }


        //Combined Days...

        bool CombndDaysJustFocused = false;
        bool CombndDaysChanged = false;
        string CombndDaysContentOnFocused = "";

        void OnCombndDaysFocused(object sender, EventArgs args)
        {
            CombndDaysContentOnFocused = CombndDays.Text;
            CombndDays.Text = "";
            CombndDaysJustFocused = true;
            CombndDaysChanged = false;
        }

        void OnCombndDaysUnfocused(object sender, EventArgs args)
        {
            CombndDaysJustFocused = false;

            if (CombndDaysChanged)
            {
                CombndDaysChanged = false;
            }
            else
            {
                CombndDays.Text = CombndDaysContentOnFocused;
            }
        }

        void OnCombndDaysTextChanged(object sender, EventArgs args)
        {
            if (CombndDaysJustFocused)
            {
                CombndDaysJustFocused = false;
                CombndDaysChanged = true;
            }
        }

        void OnCombndDaysCompleted(object sender, EventArgs args)
        {
        }


        //Combined Hours...

        bool CombndHoursJustFocused = false;
        bool CombndHoursChanged = false;
        string CombndHoursContentOnFocused = "";

        void OnCombndHoursFocused(object sender, EventArgs args)
        {
            CombndHoursContentOnFocused = CombndHours.Text;
            CombndHours.Text = "";
            CombndHoursJustFocused = true;
            CombndHoursChanged = false;
        }

        void OnCombndHoursUnfocused(object sender, EventArgs args)
        {
            CombndHoursJustFocused = false;

            if (CombndHoursChanged)
            {
                CombndHoursChanged = false;
            }
            else
            {
                CombndHours.Text = CombndHoursContentOnFocused;
            }
        }

        void OnCombndHoursTextChanged(object sender, EventArgs args)
        {
            if (CombndHoursJustFocused)
            {
                CombndHoursJustFocused = false;
                CombndHoursChanged = true;
            }
        }

        void OnCombndHoursCompleted(object sender, EventArgs args)
        {
        }


        //Combined Minutes...

        bool CombndMinutesJustFocused = false;
        bool CombndMinutesChanged = false;
        string CombndMinutesContentOnFocused = "";

        void OnCombndMinutesFocused(object sender, EventArgs args)
        {
            CombndMinutesContentOnFocused = CombndMinutes.Text;
            CombndMinutes.Text = "";
            CombndMinutesJustFocused = true;
            CombndMinutesChanged = false;
        }

        void OnCombndMinutesUnfocused(object sender, EventArgs args)
        {
            CombndMinutesJustFocused = false;

            if (CombndMinutesChanged)
            {
                CombndMinutesChanged = false;
            }
            else
            {
                CombndMinutes.Text = CombndMinutesContentOnFocused;
            }
        }

        void OnCombndMinutesTextChanged(object sender, EventArgs args)
        {
            if (CombndMinutesJustFocused)
            {
                CombndMinutesJustFocused = false;
                CombndMinutesChanged = true;
            }
        }

        void OnCombndMinutesCompleted(object sender, EventArgs args)
        {
        }
//HERTIL Combined


        //HERFRA Total
        //Total Years...

        bool TotYearsJustFocused = false;
        bool TotYearsChanged = false;
        string TotYearsContentOnFocused = "";

        void OnTotYearsFocused(object sender, EventArgs args)
        {
            TotYearsContentOnFocused = TotYears.Text;
            TotYears.Text = "";
            TotYearsJustFocused = true;
            TotYearsChanged = false;
        }

        void OnTotYearsUnfocused(object sender, EventArgs args)
        {
            TotYearsJustFocused = false;

            if (TotYearsChanged)
            {
                TotYearsChanged = false;
            }
            else
            {
                TotYears.Text = TotYearsContentOnFocused;
            }
        }

        void OnTotYearsTextChanged(object sender, EventArgs args)
        {
            if (TotYearsJustFocused)
            {
                TotYearsJustFocused = false;
                TotYearsChanged = true;
            }
        }

        void OnTotYearsCompleted(object sender, EventArgs args)
        {
        }


        //Total Months...

        bool TotMonthsJustFocused = false;
        bool TotMonthsChanged = false;
        string TotMonthsContentOnFocused = "";

        void OnTotMonthsFocused(object sender, EventArgs args)
        {
            TotMonthsContentOnFocused = TotMonths.Text;
            TotMonths.Text = "";
            TotMonthsJustFocused = true;
            TotMonthsChanged = false;
        }

        void OnTotMonthsUnfocused(object sender, EventArgs args)
        {
            TotMonthsJustFocused = false;

            if (TotMonthsChanged)
            {
                TotMonthsChanged = false;
            }
            else
            {
                TotMonths.Text = TotMonthsContentOnFocused;
            }
        }

        void OnTotMonthsTextChanged(object sender, EventArgs args)
        {
            if (TotMonthsJustFocused)
            {
                TotMonthsJustFocused = false;
                TotMonthsChanged = true;
            }
        }

        void OnTotMonthsCompleted(object sender, EventArgs args)
        {
        }


        //Total Weeks...

        bool TotWeeksJustFocused = false;
        bool TotWeeksChanged = false;
        string TotWeeksContentOnFocused = "";

        void OnTotWeeksFocused(object sender, EventArgs args)
        {
            TotWeeksContentOnFocused = TotWeeks.Text;
            TotWeeks.Text = "";
            TotWeeksJustFocused = true;
            TotWeeksChanged = false;
        }

        void OnTotWeeksUnfocused(object sender, EventArgs args)
        {
            TotWeeksJustFocused = false;

            if (TotWeeksChanged)
            {
                TotWeeksChanged = false;
            }
            else
            {
                TotWeeks.Text = TotWeeksContentOnFocused;
            }
        }

        void OnTotWeeksTextChanged(object sender, EventArgs args)
        {
            if (TotWeeksJustFocused)
            {
                TotWeeksJustFocused = false;
                TotWeeksChanged = true;
            }
        }

        void OnTotWeeksCompleted(object sender, EventArgs args)
        {
        }


        //Total Days...

        bool TotDaysJustFocused = false;
        bool TotDaysChanged = false;
        string TotDaysContentOnFocused = "";

        void OnTotDaysFocused(object sender, EventArgs args)
        {
            TotDaysContentOnFocused = TotDays.Text;
            TotDays.Text = "";
            TotDaysJustFocused = true;
            TotDaysChanged = false;
        }

        void OnTotDaysUnfocused(object sender, EventArgs args)
        {
            TotDaysJustFocused = false;

            if (TotDaysChanged)
            {
                TotDaysChanged = false;
            }
            else
            {
                TotDays.Text = TotDaysContentOnFocused;
            }
        }

        void OnTotDaysTextChanged(object sender, EventArgs args)
        {
            if (TotDaysJustFocused)
            {
                TotDaysJustFocused = false;
                TotDaysChanged = true;
            }
        }

        void OnTotDaysCompleted(object sender, EventArgs args)
        {
        }


        //Total Hours...

        bool TotHoursJustFocused = false;
        bool TotHoursChanged = false;
        string TotHoursContentOnFocused = "";

        void OnTotHoursFocused(object sender, EventArgs args)
        {
            TotHoursContentOnFocused = TotHours.Text;
            TotHours.Text = "";
            TotHoursJustFocused = true;
            TotHoursChanged = false;
        }

        void OnTotHoursUnfocused(object sender, EventArgs args)
        {
            TotHoursJustFocused = false;

            if (TotHoursChanged)
            {
                TotHoursChanged = false;
            }
            else
            {
                TotHours.Text = TotHoursContentOnFocused;
            }
        }

        void OnTotHoursTextChanged(object sender, EventArgs args)
        {
            if (TotHoursJustFocused)
            {
                TotHoursJustFocused = false;
                TotHoursChanged = true;
            }
        }

        void OnTotHoursCompleted(object sender, EventArgs args)
        {
        }


        //Total Minutes...

        bool TotMinutesJustFocused = false;
        bool TotMinutesChanged = false;
        string TotMinutesContentOnFocused = "";

        void OnTotMinutesFocused(object sender, EventArgs args)
        {
            TotMinutesContentOnFocused = TotMinutes.Text;
            TotMinutes.Text = "";
            TotMinutesJustFocused = true;
            TotMinutesChanged = false;
        }

        void OnTotMinutesUnfocused(object sender, EventArgs args)
        {
            TotMinutesJustFocused = false;

            if (TotMinutesChanged)
            {
                TotMinutesChanged = false;
            }
            else
            {
                TotMinutes.Text = TotMinutesContentOnFocused;
            }
        }

        void OnTotMinutesTextChanged(object sender, EventArgs args)
        {
            if (TotMinutesJustFocused)
            {
                TotMinutesJustFocused = false;
                TotMinutesChanged = true;
            }
        }

        void OnTotMinutesCompleted(object sender, EventArgs args)
        {
        }
//HERTIL Total

        // End date-time... 

        bool EndDateTimeJustFocused = false;
        bool EndDateTimeChanged = false;
        string EndDateTimeContentOnFocused = "";

        void FormatEndDateTime()
        {
            if (EndDateTime.Text.Length != 0)
            {
                var dayName = "";
                EndDateTime.Text = FormatDateTime(EndDateTime.Text, ref dayName);
                endDayName.Text = dayName;
            }
        }

        void OnEndDateTimeNowButtonClicked(object sender, EventArgs args)
        { // yyyy-MM-dd HH:mm
            EndDateTimeJustFocused = false;
            EndDateTimeChanged = false;
            EndDateTime.Text = DateTime.Now.ToString("u").Remove(16);
            endDayName.Text = DateTime.Now.ToString("R").Remove(3);
        }

        void OnEndDateTimeFocused(object sender, EventArgs args)
        {
            EndDateTimeContentOnFocused = EndDateTime.Text;
            EndDateTime.Text = "";
            endDayName.Text = "ddd";
            EndDateTimeJustFocused = true;
            EndDateTimeChanged = false;
        }

        void OnEndDateTimeUnfocused(object sender, EventArgs args)
        { // yyyyMMddHHmm -> yyyy-MM-dd HH:mm
            EndDateTimeJustFocused = false;

            if (EndDateTimeChanged)
            {
                EndDateTimeChanged = false;
                FormatEndDateTime();
            }
            else
            {
                EndDateTime.Text = EndDateTimeContentOnFocused;
                FormatEndDateTime();
            }
        }

        void OnEndDateTimeTextChanged(object sender, EventArgs args)
        {
            if (EndDateTimeJustFocused)
            {
                EndDateTimeJustFocused = false;
                EndDateTimeChanged = true;
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
