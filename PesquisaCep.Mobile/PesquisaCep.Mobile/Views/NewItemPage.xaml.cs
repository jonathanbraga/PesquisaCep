using PesquisaCep.Mobile.ViewModels;
using Xamarin.Forms;

namespace PesquisaCep.Mobile.Views
{
    public partial class NewItemPage : ContentPage
    {
        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}