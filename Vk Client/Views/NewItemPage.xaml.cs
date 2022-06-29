using System;
using System.Collections.Generic;
using System.ComponentModel;
using Vk_Client.Models;
using Vk_Client.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vk_Client.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}