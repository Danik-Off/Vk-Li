using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Vk_Client.ViewModels;

namespace Vk_Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenDialog : ContentPage
    {
        public OpenDialog()
        {
            InitializeComponent();
            BindingContext = new OpenDialogView();
        }
    }
}