using System.ComponentModel;
using Vk_Client.ViewModels;
using Xamarin.Forms;

namespace Vk_Client.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}