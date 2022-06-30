using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VkNet;
using VkNet.Model;

namespace Vk_Client.Services
{
    class DataCash
    {
        private object token;

        public void CashUser(long user_id)
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
                    App.Current.Properties["user_id"+user_id.ToString()] = api.Users.Get(new long[] { user_id}).FirstOrDefault();
                  
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
