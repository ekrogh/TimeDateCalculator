using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;


namespace TimeDateCalculator
{
    public partial class MainPage : ContentPage
    {

        private double width;
        private double height;


        private bool firstTime = true;
        private bool firstTimeWdthOrHeightChanged = true;

        private double nativeTotalStackWidthLandscape = 731.0;
        private double nativeTotalStackHeightPortrait = 732.0;

        private double StartDayNameWidthRequest = 0.0;
        private double StartDayNameFontSize = 0.0;

        private double StartDateTimeIntroLabelNameFontSizeOrig = 0.0;

        private double StartEndDayNameFontSizeOrig = 0.0;



        DateTime StartDateTimeIn = DateTime.MaxValue;
        // Total values for dateTime span
        int TotYearsIn = int.MinValue;
        int TotMonthsIn = int.MinValue;
        int TotWeeksIn = int.MinValue;
        int TotDaysIn = int.MinValue;
        int TotHoursIn = int.MinValue;
        int TotMinutesIn = int.MinValue;
        // Values for "Combnd" dateTime span
        int CombndYearsIn = int.MinValue;
        int CombndMonthsIn = int.MinValue;
        int CombndWeeksIn = int.MinValue;
        int CombndDaysIn = int.MinValue;
        int CombndHoursIn = int.MinValue;
        int CombndMinutesIn = int.MinValue;
        DateTime EndDateTimeIn = DateTime.MaxValue;
        // Output values
        DateTime SartDateTimeOut = DateTime.MaxValue;
        // Combnd
        int CombndYearsOut = int.MinValue;
        int CombndMonthsOut = int.MinValue;
        int CombndWeeksOut = int.MinValue;
        int CombndDaysOut = int.MinValue;
        int CombndHoursOut = int.MinValue;
        int CombndMinutesOut = int.MinValue;
        // Total values for dateTime span
        int TotYearsOut = int.MinValue;
        int TotMonthsOut = int.MinValue;
        int TotWeeksOut = int.MinValue;
        int TotDaysOut = int.MinValue;
        int TotHoursOut = int.MinValue;
        int TotMinutesOut = int.MinValue;
        DateTime EndDateTimeOut = DateTime.MaxValue;


        private void ClearAllIOVars()
        {
            StartDateTimeIn = DateTime.MaxValue;
            // Total values for dateTime span
            TotYearsIn = int.MinValue;
            TotMonthsIn = int.MinValue;
            TotWeeksIn = int.MinValue;
            TotDaysIn = int.MinValue;
            TotHoursIn = int.MinValue;
            TotMinutesIn = int.MinValue;
            // Values for "Combnd" dateTime span
            CombndYearsIn = int.MinValue;
            CombndMonthsIn = int.MinValue;
            CombndWeeksIn = int.MinValue;
            CombndDaysIn = int.MinValue;
            CombndHoursIn = int.MinValue;
            CombndMinutesIn = int.MinValue;
            EndDateTimeIn = DateTime.MaxValue;
            // Output values
            SartDateTimeOut = DateTime.MaxValue;
            // Combnd
            CombndYearsOut = int.MinValue;
            CombndMonthsOut = int.MinValue;
            CombndWeeksOut = int.MinValue;
            CombndDaysOut = int.MinValue;
            CombndHoursOut = int.MinValue;
            CombndMinutesOut = int.MinValue;
            // Total values for dateTime span
            TotYearsOut = int.MinValue;
            TotMonthsOut = int.MinValue;
            TotWeeksOut = int.MinValue;
            TotDaysOut = int.MinValue;
            TotHoursOut = int.MinValue;
            TotMinutesOut = int.MinValue;
            EndDateTimeOut = DateTime.MaxValue;
        }

        void DoClearAll()
        {
            StartDateTime.Text = "";
            StartDayName.Text = "ddd";

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
            EndDayName.Text = "ddd";

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

            ClearAllIOVars();
        }

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            if (firstTime)
            {
                DoClearAll();
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
                    StartDayNameWidthRequest = 2.0f * Math.Truncate(StartDayName.Width);
                    StartDayNameFontSize = StartDayName.FontSize;
                    StartDateTimeIntroLabelNameFontSizeOrig = StartDateTimeIntroLabelName.FontSize;
                    StartEndDayNameFontSizeOrig = StartDayName.FontSize;
                    firstTimeWdthOrHeightChanged = false;
                }


                if (height > width) // Portrait ?
                { // Portrait
                    TotalStackName.Scale = 1;

                    entriesOuterStack.Orientation = StackOrientation.Horizontal;
                    CombndTimeEntriesStack.Orientation = StackOrientation.Vertical;
                    TotalTimeEntriesStack.Orientation = StackOrientation.Vertical;
                    scrollViewName.Orientation = ScrollOrientation.Vertical;

                    widthAndHightScale = height / nativeTotalStackHeightPortrait;
                }
                else
                { // Landscape
                    TotalStackName.Scale = 1;
                    landscape = true;

                    entriesOuterStack.Orientation = StackOrientation.Vertical;
                    CombndTimeEntriesStack.Orientation = StackOrientation.Horizontal;
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
                                StartDayName.FontSize = EndDayName.FontSize = StartEndDayNameFontSizeOrig * widthAndHightScale /*/ 1.5*/;
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

                    StartDayName.WidthRequest = EndDayName.WidthRequest = 45;
                }
            }
        }



        string FormatDateTime(string theDateTimeStringToFormat, out string dayName, out DateTime TheDateTime)
        { // yyyyMMddHHmm -> yyyy-MM-dd HH:mm
            var theFormattedDateTime = theDateTimeStringToFormat;

            DateTime dateTimeHolder = DateTime.Now;
            try
            {
                dateTimeHolder = DateTime.Parse(theFormattedDateTime);
                dayName = dateTimeHolder.ToString("R").Remove(3);
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
                    dayName = dateTimeHolder.ToString("R").Remove(3);
                }
                catch (FormatException)
                {
                    Task task = DisplayAlert("Type error", "Illegal Date+Time !", "OK");
                    theFormattedDateTime = "";
                    dayName = "ddd";
                    dateTimeHolder = DateTime.MaxValue;
                }
            }
            TheDateTime = dateTimeHolder;
            return theFormattedDateTime;

        }



        // Start date-time...

        bool StartDateTimeJustFocused = false;
        bool StartDateTimeChanged = false;
        string StartDateTimeContentOnFocused = "";

        DateTime FormatStartDateTime()
        {
            DateTime TheDateTime = DateTime.MaxValue;

            if (StartDateTime.Text.Length != 0)
            {
                var dayName = "";
                StartDateTime.Text = FormatDateTime(StartDateTime.Text, out dayName, out TheDateTime);
                StartDayName.Text = dayName;
            }

            return TheDateTime;
        }

        void OnStartDateTimeNowButtonClicked(object sEnder, EventArgs args)
        { // yyyy-MM-dd HH:mm
            StartDateTimeJustFocused = false;
            StartDateTimeChanged = false;
            StartDateTime.Text = DateTime.Now.ToString("u").Remove(16);
            StartDayName.Text = "ddd";
            StartDateTimeIn = FormatStartDateTime();
        }

        void OnStartDateTimeFocused(object sEnder, EventArgs args)
        {
            StartDateTimeContentOnFocused = StartDateTime.Text;
            StartDateTime.Text = "";
            StartDayName.Text = "ddd";
            StartDateTimeJustFocused = true;
            StartDateTimeChanged = false;
            StartDateTimeIn = DateTime.MaxValue;
        }

        void OnStartDateTimeUnfocused(object sEnder, EventArgs args)
        { // yyyyMMddHHmm -> yyy-MM-dd HH:mm
            OnStartDateTimeCompleted(sEnder, args);
        }

        void OnStartDateTimeTextChanged(object sEnder, EventArgs args)
        {
            if (StartDateTimeJustFocused)
            {
                StartDateTimeJustFocused = false;
                StartDateTimeChanged = true;
            }
        }

        void OnStartDateTimeCompleted(object sEnder, EventArgs args)
        { // yyyyMMddHHmm -> yyy-MM-dd HH:mm
            StartDateTimeJustFocused = false;

            if (StartDateTimeChanged)
            {
                StartDateTimeChanged = false;
                StartDateTimeIn = FormatStartDateTime();
            }
            else
            {
                StartDateTime.Text = StartDateTimeContentOnFocused;
                StartDateTimeIn = FormatStartDateTime();
            }
        }


        //FROM HERE Combined
        //Combined Years...

        bool CombndYearsJustFocused = false;
        bool CombndYearsChanged = false;
        string CombndYearsContentOnFocused = "";

        void OnCombndYearsFocused(object sEnder, EventArgs args)
        {
            CombndYearsContentOnFocused = CombndYears.Text;
            CombndYears.Text = "";
            CombndYearsJustFocused = true;
            CombndYearsChanged = false;
            CombndYearsIn = Int32.MinValue;
        }

        void OnCombndYearsUnfocused(object sEnder, EventArgs args)
        {
            OnCombndYearsCompleted(sEnder, args);
        }

        void OnCombndYearsTextChanged(object sEnder, EventArgs args)
        {
            if (CombndYearsJustFocused)
            {
                CombndYearsJustFocused = false;
                CombndYearsChanged = true;
            }
        }

        void OnCombndYearsCompleted(object sEnder, EventArgs args)
        {
            CombndYearsJustFocused = false;

            if (CombndYearsChanged)
            {
                CombndYearsChanged = false;
                if (!int.TryParse(CombndYears.Text, out CombndYearsIn))
                {
                    CombndYearsIn = Int32.MinValue;
                    Task task = DisplayAlert("Invalid \"Combined Years\" ", CombndYears.Text, "OK");
                    CombndYears.Text = "";
                }
            }
            else
            {
                CombndYears.Text = CombndYearsContentOnFocused;
            }
        }


        //Combined Months...

        bool CombndMonthsJustFocused = false;
        bool CombndMonthsChanged = false;
        string CombndMonthsContentOnFocused = "";

        void OnCombndMonthsFocused(object sEnder, EventArgs args)
        {
            CombndMonthsContentOnFocused = CombndMonths.Text;
            CombndMonths.Text = "";
            CombndMonthsJustFocused = true;
            CombndMonthsChanged = false;
            CombndMonthsIn = Int32.MinValue;
        }

        void OnCombndMonthsUnfocused(object sEnder, EventArgs args)
        {
            OnCombndMonthsCompleted(sEnder, args);
        }

        void OnCombndMonthsTextChanged(object sEnder, EventArgs args)
        {
            if (CombndMonthsJustFocused)
            {
                CombndMonthsJustFocused = false;
                CombndMonthsChanged = true;
            }
        }

        void OnCombndMonthsCompleted(object sEnder, EventArgs args)
        {
            CombndMonthsJustFocused = false;

            if (CombndMonthsChanged)
            {
                CombndMonthsChanged = false;
                if (!int.TryParse(CombndMonths.Text, out CombndMonthsIn))
                {
                    CombndMonthsIn = Int32.MinValue;
                    Task task = DisplayAlert("Invalid \"Combined Months\" ", CombndMonths.Text, "OK");
                    CombndMonths.Text = "";
                }
            }
            else
            {
                CombndMonths.Text = CombndMonthsContentOnFocused;
            }
        }


        //Combined Weeks...

        bool CombndWeeksJustFocused = false;
        bool CombndWeeksChanged = false;
        string CombndWeeksContentOnFocused = "";

        void OnCombndWeeksFocused(object sEnder, EventArgs args)
        {
            CombndWeeksContentOnFocused = CombndWeeks.Text;
            CombndWeeks.Text = "";
            CombndWeeksJustFocused = true;
            CombndWeeksChanged = false;
            CombndWeeksIn = Int32.MinValue;
        }

        void OnCombndWeeksUnfocused(object sEnder, EventArgs args)
        {
            OnCombndWeeksCompleted(sEnder, args);
        }

        void OnCombndWeeksTextChanged(object sEnder, EventArgs args)
        {
            if (CombndWeeksJustFocused)
            {
                CombndWeeksJustFocused = false;
                CombndWeeksChanged = true;
            }
        }

        void OnCombndWeeksCompleted(object sEnder, EventArgs args)
        {
            CombndWeeksJustFocused = false;

            if (CombndWeeksChanged)
            {
                CombndWeeksChanged = false;
                if (!int.TryParse(CombndWeeks.Text, out CombndWeeksIn))
                {
                    CombndWeeksIn = Int32.MinValue;
                    Task task = DisplayAlert("Invalid \"Combined Weeks\" ", CombndWeeks.Text, "OK");
                    CombndWeeks.Text = "";
                }
            }
            else
            {
                CombndWeeks.Text = CombndWeeksContentOnFocused;
            }
        }


        //Combined Days...

        bool CombndDaysJustFocused = false;
        bool CombndDaysChanged = false;
        string CombndDaysContentOnFocused = "";

        void OnCombndDaysFocused(object sEnder, EventArgs args)
        {
            CombndDaysContentOnFocused = CombndDays.Text;
            CombndDays.Text = "";
            CombndDaysJustFocused = true;
            CombndDaysChanged = false;
            CombndDaysIn = Int32.MinValue;
        }

        void OnCombndDaysUnfocused(object sEnder, EventArgs args)
        {
            OnCombndDaysCompleted(sEnder, args);
        }

        void OnCombndDaysTextChanged(object sEnder, EventArgs args)
        {
            if (CombndDaysJustFocused)
            {
                CombndDaysJustFocused = false;
                CombndDaysChanged = true;
            }
        }

        void OnCombndDaysCompleted(object sEnder, EventArgs args)
        {
            CombndDaysJustFocused = false;

            if (CombndDaysChanged)
            {
                CombndDaysChanged = false;
                if (!int.TryParse(CombndDays.Text, out CombndDaysIn))
                {
                    CombndDaysIn = Int32.MinValue;
                    Task task = DisplayAlert("Invalid \"Combined Days\" ", CombndDays.Text, "OK");
                    CombndDays.Text = "";
                }
            }
            else
            {
                CombndDays.Text = CombndDaysContentOnFocused;
            }
        }


        //Combined Hours...

        bool CombndHoursJustFocused = false;
        bool CombndHoursChanged = false;
        string CombndHoursContentOnFocused = "";

        void OnCombndHoursFocused(object sEnder, EventArgs args)
        {
            CombndHoursContentOnFocused = CombndHours.Text;
            CombndHours.Text = "";
            CombndHoursJustFocused = true;
            CombndHoursChanged = false;
            CombndHoursIn = Int32.MinValue;
        }

        void OnCombndHoursUnfocused(object sEnder, EventArgs args)
        {
            OnCombndHoursCompleted(sEnder, args);
        }

        void OnCombndHoursTextChanged(object sEnder, EventArgs args)
        {
            if (CombndHoursJustFocused)
            {
                CombndHoursJustFocused = false;
                CombndHoursChanged = true;
            }
        }

        void OnCombndHoursCompleted(object sEnder, EventArgs args)
        {
            CombndHoursJustFocused = false;

            if (CombndHoursChanged)
            {
                CombndHoursChanged = false;
                if (!int.TryParse(CombndHours.Text, out CombndHoursIn))
                {
                    CombndHoursIn = Int32.MinValue;
                    Task task = DisplayAlert("Invalid \"Combined Hours\" ", CombndHours.Text, "OK");
                    CombndHours.Text = "";
                }
            }
            else
            {
                CombndHours.Text = CombndHoursContentOnFocused;
            }
        }


        //Combined Minutes...

        bool CombndMinutesJustFocused = false;
        bool CombndMinutesChanged = false;
        string CombndMinutesContentOnFocused = "";

        void OnCombndMinutesFocused(object sEnder, EventArgs args)
        {
            CombndMinutesContentOnFocused = CombndMinutes.Text;
            CombndMinutes.Text = "";
            CombndMinutesJustFocused = true;
            CombndMinutesChanged = false;
            CombndMinutesIn = Int32.MinValue;
        }

        void OnCombndMinutesUnfocused(object sEnder, EventArgs args)
        {
            OnCombndMinutesCompleted(sEnder, args);
        }

        void OnCombndMinutesTextChanged(object sEnder, EventArgs args)
        {
            if (CombndMinutesJustFocused)
            {
                CombndMinutesJustFocused = false;
                CombndMinutesChanged = true;
            }
        }

        void OnCombndMinutesCompleted(object sEnder, EventArgs args)
        {
            CombndMinutesJustFocused = false;

            if (CombndMinutesChanged)
            {
                CombndMinutesChanged = false;
                if (!int.TryParse(CombndMinutes.Text, out CombndMinutesIn))
                {
                    CombndMinutesIn = Int32.MinValue;
                    Task task = DisplayAlert("Invalid \"Combined Minutes\" ", CombndMinutes.Text, "OK");
                    CombndMinutes.Text = "";
                }
            }
            else
            {
                CombndMinutes.Text = CombndMinutesContentOnFocused;
            }
        }
//TO HERE Combined


        //FROM HERE Total
        //Total Years...

        bool TotYearsJustFocused = false;
        bool TotYearsChanged = false;
        string TotYearsContentOnFocused = "";

        void OnTotYearsFocused(object sEnder, EventArgs args)
        {
            TotYearsContentOnFocused = TotYears.Text;
            TotYears.Text = "";
            TotYearsJustFocused = true;
            TotYearsChanged = false;
            TotYearsIn = Int32.MinValue;
        }

        void OnTotYearsUnfocused(object sEnder, EventArgs args)
        {
            OnTotYearsCompleted(sEnder, args);
        }

        void OnTotYearsTextChanged(object sEnder, EventArgs args)
        {
            if (TotYearsJustFocused)
            {
                TotYearsJustFocused = false;
                TotYearsChanged = true;
            }
        }

        void OnTotYearsCompleted(object sEnder, EventArgs args)
        {
            TotYearsJustFocused = false;

            if (TotYearsChanged)
            {
                TotYearsChanged = false;
                if (!int.TryParse(TotYears.Text, out TotYearsIn))
                {
                    TotYearsIn = Int32.MinValue;
                    Task task = DisplayAlert("Invalid \"Total Years\" ", TotYears.Text, "OK");
                    TotYears.Text = "";
                }
            }
            else
            {
                TotYears.Text = TotYearsContentOnFocused;
            }
        }


        //Total Months...

        bool TotMonthsJustFocused = false;
        bool TotMonthsChanged = false;
        string TotMonthsContentOnFocused = "";

        void OnTotMonthsFocused(object sEnder, EventArgs args)
        {
            TotMonthsContentOnFocused = TotMonths.Text;
            TotMonths.Text = "";
            TotMonthsJustFocused = true;
            TotMonthsChanged = false;
            TotMonthsIn = Int32.MinValue;
        }

        void OnTotMonthsUnfocused(object sEnder, EventArgs args)
        {
            OnTotMonthsCompleted(sEnder, args);
        }

        void OnTotMonthsTextChanged(object sEnder, EventArgs args)
        {
            if (TotMonthsJustFocused)
            {
                TotMonthsJustFocused = false;
                TotMonthsChanged = true;
            }
        }

        void OnTotMonthsCompleted(object sEnder, EventArgs args)
        {
            TotMonthsJustFocused = false;

            if (TotMonthsChanged)
            {
                TotMonthsChanged = false;
                if (!int.TryParse(TotMonths.Text, out TotMonthsIn))
                {
                    TotMonthsIn = Int32.MinValue;
                    Task task = DisplayAlert("Invalid \"Total Months\" ", TotMonths.Text, "OK");
                    TotMonths.Text = "";
                }
            }
            else
            {
                TotMonths.Text = TotMonthsContentOnFocused;
            }
        }


        //Total Weeks...

        bool TotWeeksJustFocused = false;
        bool TotWeeksChanged = false;
        string TotWeeksContentOnFocused = "";

        void OnTotWeeksFocused(object sEnder, EventArgs args)
        {
            TotWeeksContentOnFocused = TotWeeks.Text;
            TotWeeks.Text = "";
            TotWeeksJustFocused = true;
            TotWeeksChanged = false;
            TotWeeksIn = Int32.MinValue;
        }

        void OnTotWeeksUnfocused(object sEnder, EventArgs args)
        {
            OnTotWeeksCompleted(sEnder, args);
        }

        void OnTotWeeksTextChanged(object sEnder, EventArgs args)
        {
            if (TotWeeksJustFocused)
            {
                TotWeeksJustFocused = false;
                TotWeeksChanged = true;
            }
        }

        void OnTotWeeksCompleted(object sEnder, EventArgs args)
        {
            TotWeeksJustFocused = false;

            if (TotWeeksChanged)
            {
                TotWeeksChanged = false;
                if (!int.TryParse(TotWeeks.Text, out TotWeeksIn))
                {
                    TotWeeksIn = Int32.MinValue;
                    Task task = DisplayAlert("Invalid \"Total Weeks\" ", TotWeeks.Text, "OK");
                    TotWeeks.Text = "";
                }
            }
            else
            {
                TotWeeks.Text = TotWeeksContentOnFocused;
            }
        }


        //Total Days...

        bool TotDaysJustFocused = false;
        bool TotDaysChanged = false;
        string TotDaysContentOnFocused = "";

        void OnTotDaysFocused(object sEnder, EventArgs args)
        {
            TotDaysContentOnFocused = TotDays.Text;
            TotDays.Text = "";
            TotDaysJustFocused = true;
            TotDaysChanged = false;
            TotDaysIn = Int32.MinValue;
        }

        void OnTotDaysUnfocused(object sEnder, EventArgs args)
        {
            OnTotDaysCompleted(sEnder, args);
        }

        void OnTotDaysTextChanged(object sEnder, EventArgs args)
        {
            if (TotDaysJustFocused)
            {
                TotDaysJustFocused = false;
                TotDaysChanged = true;
            }
        }

        void OnTotDaysCompleted(object sEnder, EventArgs args)
        {
            TotDaysJustFocused = false;

            if (TotDaysChanged)
            {
                TotDaysChanged = false;
                if (!int.TryParse(TotDays.Text, out TotDaysIn))
                {
                    TotDaysIn = Int32.MinValue;
                    Task task = DisplayAlert("Invalid \"Total Days\" ", TotDays.Text, "OK");
                    TotDays.Text = "";
                }
            }
            else
            {
                TotDays.Text = TotDaysContentOnFocused;
            }
        }


        //Total Hours...

        bool TotHoursJustFocused = false;
        bool TotHoursChanged = false;
        string TotHoursContentOnFocused = "";

        void OnTotHoursFocused(object sEnder, EventArgs args)
        {
            TotHoursContentOnFocused = TotHours.Text;
            TotHours.Text = "";
            TotHoursJustFocused = true;
            TotHoursChanged = false;
            TotHoursIn = Int32.MinValue;
        }

        void OnTotHoursUnfocused(object sEnder, EventArgs args)
        {
            OnTotHoursCompleted(sEnder, args);
        }

        void OnTotHoursTextChanged(object sEnder, EventArgs args)
        {
            if (TotHoursJustFocused)
            {
                TotHoursJustFocused = false;
                TotHoursChanged = true;
            }
        }

        void OnTotHoursCompleted(object sEnder, EventArgs args)
        {
            TotHoursJustFocused = false;

            if (TotHoursChanged)
            {
                TotHoursChanged = false;
                if (!int.TryParse(TotHours.Text, out TotHoursIn))
                {
                    TotHoursIn = Int32.MinValue;
                    Task task = DisplayAlert("Invalid \"Total Hours\" ", TotHours.Text, "OK");
                    TotHours.Text = "";
                }
            }
            else
            {
                TotHours.Text = TotHoursContentOnFocused;
            }
        }


        //Total Minutes...

        bool TotMinutesJustFocused = false;
        bool TotMinutesChanged = false;
        string TotMinutesContentOnFocused = "";

        void OnTotMinutesFocused(object sEnder, EventArgs args)
        {
            TotMinutesContentOnFocused = TotMinutes.Text;
            TotMinutes.Text = "";
            TotMinutesJustFocused = true;
            TotMinutesChanged = false;
            TotMinutesIn = Int32.MinValue;
        }

        void OnTotMinutesUnfocused(object sEnder, EventArgs args)
        {
            OnTotMinutesCompleted(sEnder, args);
        }

        void OnTotMinutesTextChanged(object sEnder, EventArgs args)
        {
            if (TotMinutesJustFocused)
            {
                TotMinutesJustFocused = false;
                TotMinutesChanged = true;
            }
        }

        void OnTotMinutesCompleted(object sEnder, EventArgs args)
        {
            TotMinutesJustFocused = false;

            if (TotMinutesChanged)
            {
                TotMinutesChanged = false;
                if (!int.TryParse(TotMinutes.Text, out TotMinutesIn))
                {
                    TotMinutesIn = Int32.MinValue;
                    Task task = DisplayAlert("Invalid \"Total Minutes\" ", TotMinutes.Text, "OK");
                    TotMinutes.Text = "";
                }
            }
            else
            {
                TotMinutes.Text = TotMinutesContentOnFocused;
            }
        }
        //TO HERE Total


        // End date-time... 

        bool EndDateTimeJustFocused = false;
        bool EndDateTimeChanged = false;
        string EndDateTimeContentOnFocused = "";

        DateTime FormatEndDateTime()
        {
            DateTime TheDateTime = DateTime.MaxValue;

            if (EndDateTime.Text.Length != 0)
            {
                var dayName = "";
                EndDateTime.Text = FormatDateTime(EndDateTime.Text, out dayName, out TheDateTime);
                EndDayName.Text = dayName;
            }

            return TheDateTime;
        }

        void OnEndDateTimeNowButtonClicked(object sEnder, EventArgs args)
        { // yyyy-MM-dd HH:mm
            EndDateTimeJustFocused = false;
            EndDateTimeChanged = false;
            EndDateTime.Text = DateTime.Now.ToString("u").Remove(16);
            EndDayName.Text = "ddd";
            EndDateTimeIn = FormatEndDateTime();
        }

        void OnEndDateTimeFocused(object sEnder, EventArgs args)
        {
            EndDateTimeContentOnFocused = EndDateTime.Text;
            EndDateTime.Text = "";
            EndDayName.Text = "ddd";
            EndDateTimeJustFocused = true;
            EndDateTimeChanged = false;
            StartDateTimeIn = DateTime.MaxValue;
        }

        void OnEndDateTimeUnfocused(object sEnder, EventArgs args)
        { // yyyyMMddHHmm -> yyyy-MM-dd HH:mm
            OnEndDateTimeCompleted(sEnder, args);
        }

        void OnEndDateTimeTextChanged(object sEnder, EventArgs args)
        {
            if (EndDateTimeJustFocused)
            {
                EndDateTimeJustFocused = false;
                EndDateTimeChanged = true;
            }
        }


        void OnEndDateTimeCompleted(object sEnder, EventArgs args)
        { // yyyyMMddHHmm -> yyy-MM-dd HH:mm
            EndDateTimeJustFocused = false;

            if (EndDateTimeChanged)
            {
                EndDateTimeChanged = false;
                EndDateTimeIn = FormatEndDateTime();
            }
            else
            {
                EndDateTime.Text = EndDateTimeContentOnFocused;
                EndDateTimeIn = FormatEndDateTime();
            }
        }

        void OnClearAllButtonClicked(object sEnder, EventArgs args)
        {
            DoClearAll();
        }



        // CALCULATION from here...

        private void CalcAndShowTimeSpans()
        {
            CombndYearsOut = EndDateTimeIn.Year - StartDateTimeIn.Year;
            CombndMonthsOut = EndDateTimeIn.Month - StartDateTimeIn.Month;
            if (EndDateTimeIn.Day < StartDateTimeIn.Day)
            {
                CombndMonthsOut--;
            }
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
            CombndDaysOut = CombndDaysOut % 7; // Rest after div. w. 7

            TotDaysOut = (int)ts2.TotalDays;
            TotWeeksOut = (int)(TotDaysOut / 7);
            TotMonthsOut = CombndMonthsOut + 12 * CombndYearsOut;
            TotYearsOut = CombndYearsOut;
            TotHoursOut = (int)ts2.TotalHours;
            TotMinutesOut = (int)ts2.TotalMinutes;

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
                Task task = DisplayAlert("Total \"Days\" > 9999999999", TotDays.ToString(), "OK");
            }
            TotWeeks.Text = TotWeeksOut.ToString();
            TotMonths.Text = TotMonthsOut.ToString();
            TotYears.Text = TotYearsOut.ToString();
            TotHours.Text = TotHoursOut.ToString();
            if (TotHoursOut > 9999999999)
            {
                Task task = DisplayAlert("Total \"Hours\" > 9999999999", TotHours.ToString(), "OK");
            }
            TotMinutes.Text = TotMinutesOut.ToString();
            if (TotMinutesOut > 9999999999)
            {
                Task task = DisplayAlert("Total \"Minutes\" > 9999999999", TotMinutes.ToString(), "OK");
            }
        }

        private void OnCalculateButtonClicked(object sEnder, EventArgs e)
        {
            CalculateButton.Focus();

            if (StartDateTimeIn != DateTime.MaxValue)
            {
                if (EndDateTimeIn != DateTime.MaxValue)
                {
                    if (EndDateTimeIn >= StartDateTimeIn)
                    {
                        if (!((TotYearsIn == int.MinValue) &&
                               (TotMonthsIn == int.MinValue) &&
                               (TotDaysIn == int.MinValue) &&
                               (TotHoursIn == int.MinValue) &&
                               (TotMinutesIn == int.MinValue) &&
                               (CombndYearsIn == int.MinValue) &&
                               (CombndMonthsIn == int.MinValue) &&
                               (CombndDaysIn == int.MinValue) &&
                               (CombndHoursIn == int.MinValue) &&
                               (CombndMinutesIn == int.MinValue))
                           )
                        {
                            Task task = DisplayAlert
                                (
                                    "Type error"
                                    , "Tot. and Combined must be empty when Start- and End DateTime are both set"
                                    , "OK"
                                );
                        }
                        else
                        {
                            CalcAndShowTimeSpans();
                        }
                    }
                    else
                    {
                        Task task = DisplayAlert
                           (
                               "Date time error"
                               , "End date time must be >= Start date time"
                               , "OK"
                           );
                    }
                } // if (EndDateTimeIn != DateTime.MaxValue)
                else
                {
                    bool TotChk = (TotYearsIn == int.MinValue) &&
                                  (TotMonthsIn == int.MinValue) &&
                                  (TotWeeksIn == int.MinValue) &&
                                  (TotDaysIn == int.MinValue) &&
                                  (TotHoursIn == int.MinValue) &&
                                  (TotMinutesIn == int.MinValue);

                    bool combndChk = (CombndYearsIn == int.MinValue) &&
                                     (CombndMonthsIn == int.MinValue) &&
                                     (CombndWeeksIn == int.MinValue) &&
                                     (CombndDaysIn == int.MinValue) &&
                                     (CombndHoursIn == int.MinValue) &&
                                     (CombndMinutesIn == int.MinValue);

                    if (!(TotChk && combndChk))
                    {
                        if (!(!TotChk && !combndChk))
                        {
                            EndDateTimeOut = DateTime.MaxValue; // <=> no EndDateTimeOut found

                            if (!TotChk)
                            {
                                if (TotYearsIn != int.MinValue)
                                {
                                    if ((TotMonthsIn == int.MinValue) &&
                                        (TotWeeksIn == int.MinValue) &&
                                        (TotDaysIn == int.MinValue) &&
                                        (TotHoursIn == int.MinValue) &&
                                        (TotMinutesIn == int.MinValue))
                                    {
                                        EndDateTimeOut = StartDateTimeIn.AddYears(TotYearsIn);
                                    }
                                    else
                                    {
                                        Task task = DisplayAlert
                                           (
                                               "Type error"
                                               , "Only one \"Total\" value allowed"
                                               , "OK"
                                           );
                                    }
                                } // if (TotYearsIn != int.MinValue)
                                else
                                {
                                    if (TotMonthsIn != int.MinValue)
                                    {
                                        if ((TotWeeksIn == int.MinValue) &&
                                            (TotDaysIn == int.MinValue) &&
                                            (TotHoursIn == int.MinValue) &&
                                            (TotMinutesIn == int.MinValue))
                                        {
                                            EndDateTimeOut = StartDateTimeIn.AddMonths(TotMonthsIn);
                                        }
                                        else
                                        {
                                            Task task = DisplayAlert
                                               (
                                                   "Type error"
                                                   , "Only one \"Total\" value allowed"
                                                   , "OK"
                                               );
                                        }
                                    } // if (TotMonthsIn != int.MinValue)
                                    else
                                    {
                                        if (TotWeeksIn != int.MinValue)
                                        {
                                            if ((TotDaysIn == int.MinValue) &&
                                               (TotHoursIn == int.MinValue) &&
                                               (TotMinutesIn == int.MinValue))
                                            {
                                                EndDateTimeOut = StartDateTimeIn.AddDays(TotWeeksIn * 7);
                                            }
                                            else
                                            {
                                                Task task = DisplayAlert
                                                   (
                                                       "Type error"
                                                       , "Only one \"Total\" value allowed"
                                                       , "OK"
                                                   );
                                            }
                                        } // if (TotWeeksIn != int.MinValue)
                                        else
                                        {
                                            if (TotDaysIn != int.MinValue)
                                            {
                                                if ((TotHoursIn == int.MinValue) &&
                                                    (TotMinutesIn == int.MinValue))
                                                {
                                                    EndDateTimeOut = StartDateTimeIn.AddDays(TotDaysIn);
                                                }
                                                else
                                                {
                                                    Task task = DisplayAlert
                                                       (
                                                           "Type error"
                                                           , "Only one \"Total\" value allowed"
                                                           , "OK"
                                                       );
                                                }
                                            } // if (TotDaysIn != int.MinValue)
                                            else
                                            {
                                                if (TotHoursIn != int.MinValue)
                                                {
                                                    if (TotMinutesIn == int.MinValue)
                                                    {
                                                        EndDateTimeOut = StartDateTimeIn.AddHours(TotHoursIn);
                                                    }
                                                    else
                                                    {
                                                        Task task = DisplayAlert
                                                           (
                                                               "Type error"
                                                               , "Only one \"Total\" value allowed"
                                                               , "OK"
                                                           );
                                                    }
                                                } // if (TotHoursIn != int.MinValue)
                                                else
                                                {
                                                    if (TotMinutesIn != int.MinValue)
                                                    {
                                                        EndDateTimeOut = StartDateTimeIn.AddMinutes(TotMinutesIn);
                                                    } // if (TotMinutesIn != int.MinValue)
                                                    //else
                                                    //{
                                                    //    Task task = DisplayAlert
                                                    //       (
                                                    //           "Type error"
                                                    //           , "Only one \"Total\" value allowed"
                                                    //           , "OK"
                                                    //       );
                                                    //    MessageBox.Show("At least one Total value must be entered", "Type error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                    //} // if (TotMinutesIn != int.MinValue) ... else ...
                                                } // if (TotHoursIn != int.MinValue) .. else ...
                                            } // if (TotDaysIn != int.MinValue) ... else ...
                                        } // if (TotWeeksIn != int.MinValue) ... else ...
                                    } // if (TotMonthsIn != int.MinValue) ... else ...
                                } // if (TotYearsIn != int.MinValue) ... else ...
                            } // if (!TotChk)
                            else
                            { // Must be Combnd time span

                                EndDateTimeOut = StartDateTimeIn;

                                if (CombndYearsIn != int.MinValue)
                                {
                                    EndDateTimeOut = EndDateTimeOut.AddYears(CombndYearsIn);
                                } // if (CombndYearsIn != int.MinValue)
                                if (CombndMonthsIn != int.MinValue)
                                {
                                    EndDateTimeOut = EndDateTimeOut.AddMonths(CombndMonthsIn);
                                } // if (CombndMonthsIn != int.MinValue)
                                if (CombndWeeksIn != int.MinValue)
                                {
                                    EndDateTimeOut = EndDateTimeOut.AddDays(CombndWeeksIn * 7);
                                } // if (CombndWeeksIn != int.MinValue)
                                if (CombndDaysIn != int.MinValue)
                                {
                                    EndDateTimeOut = EndDateTimeOut.AddDays(CombndDaysIn);
                                } // if (CombndDaysIn != int.MinValue)
                                if (CombndHoursIn != int.MinValue)
                                {
                                    EndDateTimeOut = EndDateTimeOut.AddHours(CombndHoursIn);
                                } // if (CombndHoursIn != int.MinValue)
                                if (CombndMinutesIn != int.MinValue)
                                {
                                    EndDateTimeOut = EndDateTimeOut.AddMinutes(CombndMinutesIn);
                                } // if (CombndMinutesIn != int.MinValue)

                            }  // if (!TotChk) ... else ...

                            if (EndDateTimeOut != DateTime.MaxValue)
                            {
                                // Save tmp SartDateTime and EndDateTime
                                DateTime tmpStartDateTimeIn = StartDateTimeIn;
                                DateTime tmpEndDateTimeIn = EndDateTimeOut;

                                // Clear and reseteverything
                                DoClearAll();
                                ClearAllIOVars();

                                // Show Start- and End Date Time
                                StartDateTimeIn = tmpStartDateTimeIn;
                                EndDateTimeIn = tmpEndDateTimeIn;
                                StartDateTime.Text = StartDateTimeIn.ToString("u").Remove(16);
                                StartDayName.Text = StartDateTimeIn.ToString("R").Remove(3);
                                EndDateTime.Text = EndDateTimeIn.ToString("u").Remove(16);
                                EndDayName.Text = EndDateTimeIn.ToString("R").Remove(3);

                                // Show Time Spans.
                                CalcAndShowTimeSpans();
                            }

                        } // if ( !(!TotChk && !combndChk) )
                        else
                        {
                            Task task = DisplayAlert
                               (
                                   "Type error"
                                   , "Not both \"Total\" and \"Combined\" time spans can be used"
                                   , "OK"
                               );
                        } // if ( !(!TotChk && !combndChk) ) ... else ...
                    } // if ( !(TotChk && combndChk) )
                    else
                    {
                        Task task = DisplayAlert
                           (
                               "Type error"
                               , "When \"Start Date + Time\" entered and no \"End Date + Time\" either a \"Total\" or \"Combined\" time span must be entered"
                               , "OK"
                           );
                    } //  // if ( !(TotChk && combndChk) ) ... else ...
                } // if (EndDateTimeIn != DateTime.MaxValue) ... else ...
            } // if (StartDateTimeIn != DateTime.MaxValue)
            else
            { // StartDateTimeIn == DateTime.MaxValue
                if (EndDateTimeIn != DateTime.MaxValue)
                {
                    bool TotChk = (TotYearsIn == int.MinValue) &&
                                  (TotMonthsIn == int.MinValue) &&
                                  (TotWeeksIn == int.MinValue) &&
                                  (TotDaysIn == int.MinValue) &&
                                  (TotHoursIn == int.MinValue) &&
                                  (TotMinutesIn == int.MinValue);

                    bool combndChk = (CombndYearsIn == int.MinValue) &&
                                     (CombndMonthsIn == int.MinValue) &&
                                     (CombndWeeksIn == int.MinValue) &&
                                     (CombndDaysIn == int.MinValue) &&
                                     (CombndHoursIn == int.MinValue) &&
                                     (CombndMinutesIn == int.MinValue);

                    if (!(TotChk && combndChk))
                    {
                        if (!(!TotChk && !combndChk))
                        {
                            SartDateTimeOut = DateTime.MaxValue; // <=> no SartDateTimeOut found

                            if (!TotChk)
                            {
                                if (TotYearsIn != int.MinValue)
                                {
                                    if ((TotMonthsIn == int.MinValue) &&
                                        (TotWeeksIn == int.MinValue) &&
                                        (TotDaysIn == int.MinValue) &&
                                        (TotHoursIn == int.MinValue) &&
                                        (TotMinutesIn == int.MinValue))
                                    {
                                        SartDateTimeOut = EndDateTimeIn.AddYears(-TotYearsIn);
                                    }
                                    else
                                    {
                                        Task task = DisplayAlert
                                           (
                                               "Type error"
                                               , "Only one \"Total\" value allowed"
                                               , "OK"
                                           );
                                    }
                                } // if (TotYearsIn != int.MinValue)
                                else
                                {
                                    if (TotMonthsIn != int.MinValue)
                                    {
                                        if ((TotWeeksIn == int.MinValue) &&
                                            (TotDaysIn == int.MinValue) &&
                                            (TotHoursIn == int.MinValue) &&
                                            (TotMinutesIn == int.MinValue))
                                        {
                                            SartDateTimeOut = EndDateTimeIn.AddMonths(-TotMonthsIn);
                                        }
                                        else
                                        {
                                            Task task = DisplayAlert
                                               (
                                                   "Type error"
                                                   , "Only one \"Total\" value allowed"
                                                   , "OK"
                                               );
                                        }
                                    } // if (TotMonthsIn != int.MinValue)
                                    else
                                    {
                                        if (TotWeeksIn != int.MinValue)
                                        {
                                            if ((TotDaysIn == int.MinValue) &&
                                               (TotHoursIn == int.MinValue) &&
                                               (TotMinutesIn == int.MinValue))
                                            {
                                                SartDateTimeOut = EndDateTimeIn.AddDays(-(TotWeeksIn * 7));
                                            }
                                            else
                                            {
                                                Task task = DisplayAlert
                                                   (
                                                       "Type error"
                                                       , "Only one \"Total\" value allowed"
                                                       , "OK"
                                                   );
                                            }
                                        } // if (TotWeeksIn != int.MinValue)
                                        else
                                        {
                                            if (TotDaysIn != int.MinValue)
                                            {
                                                if ((TotHoursIn == int.MinValue) &&
                                                    (TotMinutesIn == int.MinValue))
                                                {
                                                    SartDateTimeOut = EndDateTimeIn.AddDays(-TotDaysIn);
                                                }
                                                else
                                                {
                                                    Task task = DisplayAlert
                                                       (
                                                           "Type error"
                                                           , "Only one \"Total\" value allowed"
                                                           , "OK"
                                                       );
                                                }
                                            } // if (TotDaysIn != int.MinValue)
                                            else
                                            {
                                                if (TotHoursIn != int.MinValue)
                                                {
                                                    if (TotMinutesIn == int.MinValue)
                                                    {
                                                        SartDateTimeOut = EndDateTimeIn.AddHours(-TotHoursIn);
                                                    }
                                                    else
                                                    {
                                                        Task task = DisplayAlert
                                                           (
                                                               "Type error"
                                                               , "Only one \"Total\" value allowed"
                                                               , "OK"
                                                           );
                                                    }
                                                } // if (TotHoursIn != int.MinValue)
                                                else
                                                {
                                                    if (TotMinutesIn != int.MinValue)
                                                    {
                                                        SartDateTimeOut = EndDateTimeIn.AddMinutes(-TotMinutesIn);
                                                    } // if (TotMinutesIn != int.MinValue)
                                                    //else
                                                    //{
                                                    //    MessageBox.Show("At least one Total value must be entered", "Type error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                    //} // if (TotMinutesIn != int.MinValue) ... else ...
                                                } // if (TotHoursIn != int.MinValue) .. else ...
                                            } // if (TotDaysIn != int.MinValue) ... else ...
                                        } // if (TotWeeksIn != int.MinValue) ... else ...
                                    } // if (TotMonthsIn != int.MinValue) ... else ...
                                } // if (TotYearsIn != int.MinValue) ... else ...
                            } // if (!TotChk)
                            else
                            { // Must be Combnd time span

                                SartDateTimeOut = EndDateTimeIn;

                                if (CombndYearsIn != int.MinValue)
                                {
                                    SartDateTimeOut = SartDateTimeOut.AddYears(-CombndYearsIn);
                                } // if (CombndYearsIn != int.MinValue)
                                if (CombndMonthsIn != int.MinValue)
                                {
                                    SartDateTimeOut = SartDateTimeOut.AddMonths(-CombndMonthsIn);
                                } // if (CombndMonthsIn != int.MinValue)
                                if (CombndWeeksIn != int.MinValue)
                                {
                                    SartDateTimeOut = SartDateTimeOut.AddDays(-(CombndWeeksIn * 7));
                                } // if (CombndWeeksIn != int.MinValue)
                                if (CombndDaysIn != int.MinValue)
                                {
                                    SartDateTimeOut = SartDateTimeOut.AddDays(-CombndDaysIn);
                                } // if (CombndDaysIn != int.MinValue)
                                if (CombndHoursIn != int.MinValue)
                                {
                                    SartDateTimeOut = SartDateTimeOut.AddHours(-CombndHoursIn);
                                } // if (CombndHoursIn != int.MinValue)
                                if (CombndMinutesIn != int.MinValue)
                                {
                                    SartDateTimeOut = SartDateTimeOut.AddMinutes(-CombndMinutesIn);
                                } // if (CombndMinutesIn != int.MinValue)

                            }  // if (!TotChk) ... else ...

                            if (SartDateTimeOut != DateTime.MaxValue)
                            {
                                // Save tmp SartDateTime and EndDateTime
                                DateTime tmpEndDateTimeIn = EndDateTimeIn;
                                DateTime tmpStartDateTimeIn = SartDateTimeOut;

                                // Clear and reseteverything
                                DoClearAll();
                                ClearAllIOVars();

                                // Show Start- and End Date Time
                                StartDateTimeIn = tmpStartDateTimeIn;
                                EndDateTimeIn = tmpEndDateTimeIn;
                                StartDateTime.Text = StartDateTimeIn.ToString("u").Remove(16);
                                StartDayName.Text = StartDateTimeIn.ToString("R").Remove(3);
                                EndDateTime.Text = EndDateTimeIn.ToString("u").Remove(16);
                                EndDayName.Text = EndDateTimeIn.ToString("R").Remove(3);

                                // Show Time Spans.
                                CalcAndShowTimeSpans();
                            }

                        } // if ( !(!TotChk && !combndChk) )
                        else
                        {
                            Task task = DisplayAlert
                               (
                                   "Type error"
                                   , "Not both \"Total\" and \"Combined\" time spans can be used"
                                   , "OK"
                               );
                        } // if ( !(!TotChk && !combndChk) ) ... else ...
                    } // if ( !(TotChk && combndChk) )
                    else
                    {
                        Task task = DisplayAlert
                           (
                               "Type error"
                               , "When \"Start Date + Time\" entered and no \"End Date + Time\" either a \"Total\" or \"Combined\" time span must be entered"
                               , "OK"
                           );
                    } //  // if ( !(TotChk && combndChk) ) ... else ...
                } // if (EndDateTimeIn != DateTime.MaxValue)
                else
                {
                    Task task = DisplayAlert
                       (
                           "Type error"
                           , "\"Start Date + Time\" and/or \"End Date + Time\" must be entered."
                           , "OK"
                       );
                } // if (EndDateTimeIn != DateTime.MaxValue) ... else ...
            } // if (StartDateTimeIn != DateTime.MaxValue) ... else...
        }

        // CALCULATION Ends here...

    }
}
