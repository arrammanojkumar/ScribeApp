using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Scribes_APP
{

    [DataContract]
    public class SCRIBE_AVAIL
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "email")]
        public string email { get; set; }
        [DataMember(Name = "day")]
        public string day { get; set; }
        [DataMember(Name = "month")]
        public string month { get; set; }
        [DataMember(Name = "year")]
        public string year { get; set; }
        [DataMember(Name = "pincode")]
        public string pincode { get; set; }
       
    }
    
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class mytest : Page
    {
        string email;
        string recent_tap;

        DateTime calendarDate;

        public mytest()
        {
            this.InitializeComponent();
            calendarDate = DateTime.Today;
            Initialize_Calendar(calendarDate);
        }
        private async void btnSave_click(object sender, RoutedEventArgs e)
        {
            //TODO SAVE DATE

            string pincod = pincode.Text;

            string month = CalendarHeader.Text.Split(' ')[0];
            string year = CalendarHeader.Text.Split(' ')[1];
           

            // date present in recent_tap


            // email available in email variable.

            // UPDATE IF NOT APPEND IN Table.

            SCRIBE_AVAIL item = new SCRIBE_AVAIL{email=email,day = recent_tap,month = month,year=year,pincode=pincod};

           

            List<SCRIBE_AVAIL> table = await App.MobileService.GetTable<SCRIBE_AVAIL>().ToListAsync();

            bool updat = false;
            for(int i=0;i<table.Count;i++)
            {
                SCRIBE_AVAIL temp = table[i];
                if(temp.email == email && temp.month == month &&temp.year==year &&temp.day == recent_tap)
                {
                    temp.pincode = pincod;
                    updat = true;
                    await App.MobileService.GetTable<SCRIBE_AVAIL>().UpdateAsync(temp);
                    return;
                }
            }
            if (updat == false)
            {
                await App.MobileService.GetTable<SCRIBE_AVAIL>().InsertAsync(item);
            }


            




        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            email = e.Parameter as string;
        }
        TextBlock prev;
        private async void calenderTap(object sender, RoutedEventArgs e)
        {
            if (prev != null)
            {
                prev.Foreground = new SolidColorBrush(Colors.White);
            }

            TextBlock text1 = (TextBlock)e.OriginalSource;

            text1.Foreground = new SolidColorBrush(Colors.Red);

            prev = text1;
            //TODO GET DATE MONTH YEAR.
            String month = CalendarHeader.Text.Split(' ')[0];
            String year = CalendarHeader.Text.Split(' ')[1];
            TextBlock text = (TextBlock)e.OriginalSource;
            recent_tap = text.Text;

            List<SCRIBE_AVAIL> table = await App.MobileService.GetTable<SCRIBE_AVAIL>().ToListAsync();

            for (int i = 0; i < table.Count; i++)
            {
                SCRIBE_AVAIL temp = table[i];
                if (temp.email == email && temp.month == month && temp.year == year && temp.day == recent_tap)
                {
                    pincode.Text = temp.pincode;
                }
            }


            
            // retrive the text from DATABASE
           
        }
        void Initialize_Calendar(DateTime date)
        {
            CalendarHeader.Text = date.ToString("MMMM yyyy");
            date = new DateTime(date.Year, date.Month, 1);
            int dayOfWeek = (int)date.DayOfWeek + 1;
            int daysOfMonth = DateTime.DaysInMonth(date.Year, date.Month);
            int i = 1;
            foreach (var o1 in Calendar.Children)
            {
                foreach (var o2 in (o1 as StackPanel).Children)
                {
                    var o3 = (o2 as Grid).Children[0] as TextBlock;
                    if (i >= dayOfWeek && i < (daysOfMonth + dayOfWeek))
                        o3.Text = (i - dayOfWeek + 1).ToString();
                    else
                        o3.Text = "";
                    i++;
                }
            }
        }

        private void previousMonth(object sender, RoutedEventArgs e)
        {
            calendarDate = calendarDate.AddMonths(-1);
            Initialize_Calendar(calendarDate);
        }

        private void nextMonth(object sender, RoutedEventArgs e)
        {
            calendarDate = calendarDate.AddMonths(1);
            Initialize_Calendar(calendarDate);
        }

        //private void imgBack_Tapped(object sender, TappedRoutedEventArgs e)
        //{
        //    Frame.Navigate(typeof(MainPage));
        //}

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>

    }
}
