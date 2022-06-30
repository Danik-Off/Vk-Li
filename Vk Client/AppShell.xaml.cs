using System;
using System.Collections.Generic;
using System.Diagnostics;
using Vk_Client.Services;
using Vk_Client.ViewModels;
using Vk_Client.Views;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;
using Xamarin.Forms;

namespace Vk_Client
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        private object user;

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
            Routing.RegisterRoute(nameof(OpenDialog), typeof(OpenDialog));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            loadMainInfo();



        }
        public static T GetParameter<T>(object obj)
        {
            return (T)obj;
        }
        private void loadMainInfo()
        {
           
                new LoadToCash();
           
            if (App.Current.Properties.TryGetValue("main_user", out user))
            {
                UserFullName_Label.Text  = GetParameter<User>(user).FirstName +" " + GetParameter<User>(user).LastName;
                UserStatus_Label.Text = GetParameter<User>(user).Status;
                
            }
        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
           
            await Shell.Current.GoToAsync("//LoginPage"); //await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
