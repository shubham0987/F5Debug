using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace F5Debug
{
    public partial class Selection : PhoneApplicationPage
    {
        public Selection()
        {
            InitializeComponent();
        }

        private void Image_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/MainPage.xaml?id={0}", 1), UriKind.Relative));
        }

        private void Image_Tap_2(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/MainPage.xaml?id={0}", 4), UriKind.Relative));
        }

        private void Image_Tap_3(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/MainPage.xaml?id={0}", 0), UriKind.Relative));
        }

        private void Image_Tap_4(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/MainPage.xaml?id={0}", 2), UriKind.Relative));
        }

        private void Image_Tap_5(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/MainPage.xaml?id={0}", 3), UriKind.Relative));
        }

        private void Image_Tap_6(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/MainPage.xaml?id={0}", 5), UriKind.Relative));
        }
        private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri(string.Format("/MainPage.xaml?id={0}", 6), UriKind.Relative));
        }

        private void ApplicationBarMenuItem_Click_1(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/about.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}