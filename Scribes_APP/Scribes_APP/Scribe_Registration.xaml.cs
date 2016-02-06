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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    [DataContract]
    public class SCRIBE_REG
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "name")]
        public string name { get; set; }
        [DataMember(Name = "age")]
        public string age { get; set; }
        [DataMember(Name = "gender")]
        public string gender { get; set; }
        [DataMember(Name = "highest_edu")]
        public string highest_edu { get; set; }
        [DataMember(Name = "percent")]
        public string percent { get; set; }
        [DataMember(Name = "addr")]
        public string addr { get; set; }
        [DataMember(Name = "phone")]
        public string phone { get; set; }
        [DataMember(Name = "email")]
        public string email { get; set; }
    }

    [DataContract]
    public class MAIN_TABLE
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "email")]
        public string email { get; set; }
        [DataMember(Name = "password")]
        public string password { get; set; }
        [DataMember(Name = "category")]
        public string category { get; set; }
    }


    public sealed partial class Scribe_Registration : Page
    {
        public Scribe_Registration()
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
            string name = txtName.Text;
            string age = txtAge.Text;
            string gender = ((ComboBoxItem)cbGender.SelectedItem).Content.ToString();

            string highest_edu = txtHEQ.Text;
            string percent = txtHEG.Text;
            string password = txtPassword.Password;
            string addr = txtADDR.Text;
            string phone = txtPHN.Text;
            string email = txtEMAIL.Text;

            SCRIBE_REG item = new SCRIBE_REG { name = name, gender = gender, age = age, addr = addr, phone = phone, email = email, highest_edu = highest_edu,percent=percent };
            await App.MobileService.GetTable<SCRIBE_REG>().InsertAsync(item);

            MAIN_TABLE item1 = new MAIN_TABLE { email = email, password = password, category = "scribe" };
            await App.MobileService.GetTable<MAIN_TABLE>().InsertAsync(item1);

        }

        private void imgBack_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignIn_Page));
        }
    }
}
