using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VkNet;
using VkNet.Model;

namespace Vk_Client.Services
{
    class DataCash
    {
        private object token;

        public void CashUser(long user_id = 0)
        {
           
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
                    if (user_id != 0)
                    {
                        App.Current.Properties["user_id" + user_id.ToString()] = api.Users.Get(new long[] { user_id }).FirstOrDefault();
                    }
                    else
                    {
                        App.Current.Properties["user_id" + user_id.ToString()] = api.Users.Get(new long[] {  }).FirstOrDefault();
                    }
                    var json = JsonConvert.SerializeObject(App.Current.Properties);
                    string filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), $"text.txt");
                    File.WriteAllText(filename, json);

                }
                catch
                {
                    App.Current.Properties["user_id" + user_id.ToString()] = "";
                    
                }
            }
        }
        public User GetUserByCash(long user_id)
        {

            
            object user;
            if (App.Current.Properties.TryGetValue("user_id" + user_id.ToString(), out user))
            {
                return user as User;
            }
            else
            {
                CashUser(user_id);
                App.Current.Properties.TryGetValue("user_id" + user_id.ToString(), out user);
                return user as User;
            }

        }
    }
}
