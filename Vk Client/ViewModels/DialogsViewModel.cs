using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vk_Client.Models;
using Vk_Client.Views;
using Vk_Client.Services;
using VkNet;
using VkNet.Model;
using VkNet.Model.RequestParams;
using Xamarin.Forms;

namespace Vk_Client.ViewModels
{
    class DialogsViewModel:BaseViewModel
    {
        private Dialog _selectedItem;
       

        public ObservableCollection<Dialog> Items { get; }
        GetConversationsResult getDialogs;
        public Command LoadDialogsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Dialog> DialogTapped { get; }

        public DialogsViewModel()
        {
            Title = "Диалоги";
            FirstLoad();
            Items = new ObservableCollection<Dialog>();
            LoadDialogsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            DialogTapped = new Command<Dialog>(OnItemSelected);
            
            AddItemCommand = new Command(OnAddItem);
        }
        public async void FirstLoad()
        {
           await Task.Run(() =>ExecuteLoadItemsCommand());


        }
        public async Task ExecuteLoadItemsCommand()
        {
            
            
            try
            {
                if (IsBusy)
                {
                    throw new Exception();
                }
                IsBusy = true;
                Items.Clear();
                object token = "";
                if (App.Current.Properties.TryGetValue("token", out token))// App.Current.Properties["id_user_Auth"]
                {
                    var api = new VkApi();

                    api.Authorize(new ApiAuthParams
                    {
                        AccessToken = token.ToString()
                    });
                   
                    new Task(() => {
                    getDialogs = api.Messages.GetConversations(new GetConversationsParams
                    {
                        Count = 10
                    });
                    var s = getDialogs.Items;
                    Debug.WriteLine(s.Count);
                    foreach (var a in s)
                    {
                        Debug.WriteLine(a.Conversation.Peer.Id);

                            string text = "";
                            object us_id;

                            if (App.Current.Properties.TryGetValue("id_user_Auth", out us_id))
                            {
                                if (Convert.ToInt32(us_id) == a.LastMessage.FromId)
                                {
                                    text += "Вы: ";
                                }
                            }
                            text += a.LastMessage.Text;
                            try
                            {
                                switch (a.LastMessage.Attachments[0].Type.Name)
                                {
                                    case "Photo":
                                        text += "(Фото)";
                                        break;
                                    default:
                                        text += "(Неизвестное вложение)";
                                        break;
                                }
                               
                            }
                            catch
                            {

                            }
                         
                            //  var L_user = L_.FirstOrDefault();

                            switch (a.Conversation.Peer.Type.ToString())
                        {
                            case "user":
                                var user = new DataCash().GetUserByCash(a.Conversation.Peer.LocalId);
                               
                                   
                                    Items.Add(new Dialog()
                                {
                                        id = a.Conversation.Peer.LocalId,
                                    Title = user.FirstName+" "+user.LastName,
                                    isconversation = true,
                                    Text = text
                                });;

                                break;
                            case "chat":
                             var chat =api.Messages.GetChat(a.Conversation.Peer.LocalId);  
                                    Items.Add(new Dialog()
                                {
                                        id = a.Conversation.Peer.LocalId,
                                        Title = chat.Title,
                                    isconversation = true,
                                    Text =  text
                                }) ;
                                break;
                            case "group":
                                break;
                            case "email":
                                break;
                        }


                        }
                    }).Start();

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
        public void AddDialogToList(string title,string text)
        {

        }
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Dialog SelectedItem
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

        async void OnItemSelected(Dialog item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
           
            await Shell.Current.GoToAsync($"{nameof(OpenDialog)}?{nameof(OpenDialogView.Peer_id)}={item.id}");
        }
    }

}
