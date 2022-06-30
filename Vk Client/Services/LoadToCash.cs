using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Vk_Client.Views;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;
using Xamarin.Forms;

namespace Vk_Client.Services
{
    class LoadToCash
    {
        public LoadToCash()
        {
            InfoUser();
        }
        void InfoUser()
        {

           object token;
            if (App.Current.Properties.TryGetValue("token", out token))
            {
                try
                {
                    var api = new VkApi();

                    api.Authorize(new ApiAuthParams
                    {
                        AccessToken = token.ToString()
                    });

                    // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                    App.Current.Properties["main_user"] = api.Users.Get(new long[] { }).FirstOrDefault();
                 
                }
                catch
                {
                  // App.Current.Properties["token"] = "";
                   
                }
                }
            else {  }
        }
    }
}
