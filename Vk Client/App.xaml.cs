using System;
using Vk_Client.Services;
using Vk_Client.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vk_Client
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
