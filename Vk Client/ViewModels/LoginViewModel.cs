using Leaf.xNet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Dynamic;
using Vk_Client.Views;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using Xamarin.Forms;

namespace Vk_Client.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        string login_edit = string.Empty;
        public string Login_edit
        {
            get { return login_edit; }
            set { SetProperty(ref login_edit, value); }
        }
        string password_edit = string.Empty;
        public string Password_edit
        {
            get { return password_edit; }
            set { SetProperty(ref password_edit, value); }
        }
        public LoginViewModel()
        {
            object token = "";
            if (App.Current.Properties.TryGetValue("token", out token))
            {
                var api = new VkApi();

                api.Authorize(new ApiAuthParams
                {
                    AccessToken = token.ToString()
                });
                Debug.WriteLine(api.Messages.GetDialogs(new MessagesDialogsGetParams()).TotalCount);
                // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one

                 Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
            }
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            Debug.WriteLine(Login_edit);
            Debug.WriteLine(Password_edit);
            string token;
            using (var avtoreg = new HttpRequest())
            {
                string danni = avtoreg.Get("https://oauth.vk.com/token?grant_type=password&client_id=2274003&client_secret=hHbZxrka2uZ6jB1inYsH&username=" + Login_edit + "&password=" + Password_edit).ToString(); //отправляем Get запрос 
                dynamic dynObj = JsonConvert.DeserializeObject(danni);
                token = dynObj.access_token;
                Debug.WriteLine(token);
            }
 
           

           
           
            var api = new VkApi();

            api.Authorize(new ApiAuthParams
            {
                AccessToken = token
            });
            Debug.WriteLine(api.Messages.GetDialogs(new MessagesDialogsGetParams()).TotalCount);
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            App.Current.Properties["token"] = api.Token;
            await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
           
        }
    }
}
