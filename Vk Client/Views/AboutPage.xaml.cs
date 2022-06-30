using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vk_Client.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            lblVersionNumber.Text = "ver: "+AppInfo.VersionString; 
            lblBuildNumber.Text =  "build: "+AppInfo.BuildString;
        }
    }
}