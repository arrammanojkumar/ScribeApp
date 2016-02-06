using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public class PHP_REG
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "name")]
        public string name { get; set; }
        [DataMember(Name = "gender")]
        public string gender { get; set; }
        [DataMember(Name = "age")]
        public string age { get; set; }
        [DataMember(Name = "addr")]
        public string addr { get; set; }
        [DataMember(Name = "phone")]
        public string phone { get; set; }
        [DataMember(Name = "email")]
        public string email { get; set; }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PHP_Registration : Page
    {
        public PHP_Registration()
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = Php_Name.Text;
            string gender = ((ComboBoxItem)Php_Gender.SelectedItem).Content.ToString(); 
            string age = Php_Age.Text;
            string password = txtPassword.Password;
            string addr = Php_Addr.Text;
            string phone = Php_Phone.Text;
            string email = Php_Email.Text;

            PHP_REG item = new PHP_REG {name = name, gender = gender, age = age, addr = addr, phone = phone, email = email };
            await App.MobileService.GetTable<PHP_REG>().InsertAsync(item);

            MAIN_TABLE item1 = new MAIN_TABLE { email = email, password = password, category = "php" };
            await App.MobileService.GetTable<MAIN_TABLE>().InsertAsync(item1);

        }

        private void imgBack_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignIn_Page));
        }

    }
}
