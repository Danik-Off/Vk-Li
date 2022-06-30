using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            try
            {
                string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), $"text.txt");
     var its  =  JsonConvert.DeserializeObject<IDictionary<string ,object>>(  File.ReadAllText(filename));
                foreach(var i in its)
                {
                    App.Current.Properties.Add(i);
                }
                
            }
            catch
            {

            }
            try
            {
                var user = new DataCash().GetUserByCash(0);
                UserFullName_Label.Text = user.FirstName + " " + user.LastName;
                UserStatus_Label.Text = user.Status;
            }
            catch
            {

            }
            
        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
           
            await Shell.Current.GoToAsync("//LoginPage"); //await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
        }
    }
}
