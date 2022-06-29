using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vk_Client.Models;
using Vk_Client.ViewModels;
using Vk_Client.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vk_Client.Views
{
    public partial class ItemsPage : ContentPage
    {
        //ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();
            BindingContext = new ItemsViewModel();
        }
    }
}