using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vk_Client.Models;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;
using Xamarin.Forms;

namespace Vk_Client.ViewModels
{
    class DialogsViewModel:BaseViewModel
    {
        private Message _selectedItem;
       

        public ObservableCollection<Dialog> Items { get; }
        GetConversationsResult getDialogs;
        public Command LoadDialogsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Message> ItemTapped { get; }

        public DialogsViewModel()
        {
            Title = "Диалоги";
            Items = new ObservableCollection<Dialog>();
            LoadDialogsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Message>(OnItemSelected);
            
            AddItemCommand = new Command(OnAddItem);
        }
        public async void FirstLoad()
        {
           await ExecuteLoadItemsCommand();
        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            Items.Clear();
            try
            {
                object token = "";
                if (App.Current.Properties.TryGetValue("token", out token))
                {
                    var api = new VkApi();

                    api.Authorize(new ApiAuthParams
                    {
                        AccessToken = token.ToString()
                    });
                    getDialogs =await api.Messages.GetConversationsAsync(new GetConversationsParams
                    {
                        Count = 20
                    });
                    var s = getDialogs.Items;
                    Debug.WriteLine(s.Count);
                    foreach (var a in s)
                    {
                        Debug.WriteLine(a.Conversation.Peer.Id);
                        var L_ = await api.Users.GetAsync(new long[] { Convert.ToInt64(a.LastMessage.FromId) });
                        var L_user = L_.FirstOrDefault();
                        switch (a.Conversation.Peer.Type.ToString())
                        {
                            case "user":
                                var u = await api.Users.GetAsync(new long[] { a.Conversation.Peer.Id });
                                var user = u.FirstOrDefault();
                                
                                Items.Add(new Dialog()
                                {
                                    Title = user.FirstName+" "+user.LastName,
                                    isconversation = true,
                                    Text = L_user.FirstName+": "+ a.LastMessage.Text
                                });

                                break;
                            case "chat":
                             var chat =await api.Messages.GetChatAsync(a.Conversation.Peer.LocalId);
                              
                                Items.Add(new Dialog()
                                {
                                    Title = chat.Title,
                                    isconversation = true,
                                    Text = L_user.FirstName + ": " + a.LastMessage.Text
                                }) ;
                                break;
                            case "group":
                                break;
                            case "email":
                                break;
                        }
                          
                     
                    }

                    // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one

                    //   Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                    Debug.WriteLine(token.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Message SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
          //  await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Message item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
           // await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }

}
