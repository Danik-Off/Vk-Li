using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
           
        }
     
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
           
            await Shell.Current.GoToAsync("//LoginPage"); //await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
