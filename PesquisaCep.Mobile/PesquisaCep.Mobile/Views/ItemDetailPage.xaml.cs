using System.ComponentModel;
using Xamarin.Forms;
using PesquisaCep.Mobile.ViewModels;

namespace PesquisaCep.Mobile.Views
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