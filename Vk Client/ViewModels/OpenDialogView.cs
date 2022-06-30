using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Vk_Client.Services;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;
using Xamarin.Forms;

namespace Vk_Client.ViewModels
{
    [QueryProperty(nameof(Peer_id), nameof(Peer_id))]
    class OpenDialogView : BaseViewModel
    {
        public ObservableCollection<Models.Message> Items { get; }
        private long itemId;
        private object token;
       public OpenDialogView()
        {
            Items = new ObservableCollection<Models.Message>();

            var user = new DataCash().GetUserByCash(Convert.ToInt64(Peer_id));
            Title = user.FirstName + " " +user.LastName;

        }
        public long Peer_id
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadDialog(value);
            }
        }
        void LoadDialog(long id)
        {
        if (App.Current.Properties.TryGetValue("token", out token))// App.Current.Properties["id_user_Auth"]
        {
            var api = new VkApi();

            api.Authorize(new ApiAuthParams
            {
                AccessToken = token.ToString()
            });

            new Task(() => { }).Start();

                try
                {
                  
                    var history = api.Messages.GetHistory(new VkNet.Model.RequestParams.MessagesGetHistoryParams()
                    {
                        Count = 10,
                        UserId = itemId
                    });
                    foreach (var s in history.Messages)
                    {
                        var user = new DataCash().GetUserByCash(Convert.ToInt64( s.FromId));
                        Items.Add(new Models.Message() {Title =user.FirstName+" "+user.LastName, Text = s.Text });
                    }
                    Debug.WriteLine(history.TotalCount);
                }
                catch
                {
                }
            }
        }
    }
}
