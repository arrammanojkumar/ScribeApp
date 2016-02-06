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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Scribes_APP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignIn_Page : Page
    {
        public ApplicationDataContainer localSettings;

        public SignIn_Page()
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
            localSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtUNAME.Text = string.Empty;
            txtPWD.Password = string.Empty;
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //If Sign in success
            List<MAIN_TABLE> table = await App.MobileService.GetTable<MAIN_TABLE>().ToListAsync();
           
            string email = txtUNAME.Text;
            string password = txtPWD.Password;
            string cat = "";
            for (int i = 0; i < table.Count; i++)
            {
                MAIN_TABLE temp = table[i];
                if (email == temp.email && password==temp.password)
                {
                    cat = temp.category;
                    if (cat == "scribe")
                    {
                        Frame.Navigate(typeof(mytest), email);
                    }
                    else
                    {
                        Frame.Navigate(typeof(PHPSearchPage));
                    }
                }
            }

            txtMSG.Visibility = Visibility.Visible ;
           // txtMSG.IsEnabled = true;
            txtMSG.Text = "Invalid Login";
            txtUNAME.Text = string.Empty;
            txtPWD.Password = string.Empty;
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));

        }
    }
}
