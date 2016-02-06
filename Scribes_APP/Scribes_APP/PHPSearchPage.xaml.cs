using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using System.Globalization;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Scribes_APP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PHPSearchPage : Page
    {


        public PHPSearchPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void search_Click(object sender, RoutedEventArgs e)
        {

            

            string day = date.Date.Day.ToString();

            
            
            string month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(date.Date.Month);
            string year = date.Date.Year.ToString();
            string education = qualification.Text;
            string phGrade = grade.Text;
            string pin = pinCode.Text;

            if (pin.Length < 6 || Convert.ToInt32(phGrade) < 0)
            {
                txtArea.Visibility = Visibility.Visible;
                txtArea.Text = "Check input";
            }
            else
            {
                List<string> a = new List<string>();

                List<SCRIBE_AVAIL> table = await App.MobileService.GetTable<SCRIBE_AVAIL>().ToListAsync();


                for (int i = 0; i < table.Count; i++)
                {
                    SCRIBE_AVAIL temp = table[i];
                    if (temp.month == month && temp.year == year && temp.day == day && temp.pincode == pin)
                    {
                        a.Add(temp.email);
                    }
                }

                List<string> to_Show = new List<string>();

                List<SCRIBE_REG> table1 = await App.MobileService.GetTable<SCRIBE_REG>().ToListAsync();

                for (int i = 0; i < a.Count; i++)
                {
                    string temp_email = a[i];
                    for (int j = 0; j < table1.Count; j++)
                    {
                        SCRIBE_REG temp = table1[j];
                        if (temp_email == temp.email && education == temp.highest_edu && Convert.ToInt32(phGrade) > Convert.ToInt32(temp.percent))
                        {
                            to_Show.Add(temp_email + "_" + temp.phone);
                        }
                    }
                }

                if (to_Show.Count < 1)
                {
                    txtArea.Visibility = Visibility.Visible;
                    txtArea.Text = "No Scribes Available";
                }
                else
                {
                    txtArea.Visibility = Visibility.Visible;
                    for (int k = 0; k < to_Show.Count; k++)
                    {
                        txtArea.Text = txtArea.Text + "\n" + to_Show[k];
                    }
                }


            }

        }

        private void reset_Click(object sender, RoutedEventArgs e)
        {
            date.Date = DateTime.Today;
            qualification.Text = "";
            grade.Text = "";
            pinCode.Text = "";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
