using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vk_Client.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vk_Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DialogsPage : ContentPage
    {
        public DialogsPage()
        {
            InitializeComponent();
            BindingContext = new DialogsViewModel();
        }
    }
}