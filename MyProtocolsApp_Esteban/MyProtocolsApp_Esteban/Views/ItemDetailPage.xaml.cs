using MyProtocolsApp_Esteban.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MyProtocolsApp_Esteban.Views
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