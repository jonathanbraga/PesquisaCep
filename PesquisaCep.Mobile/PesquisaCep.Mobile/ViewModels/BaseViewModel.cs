using Xamarin.Forms;
using PesquisaCep.Mobile.Models;
using PesquisaCep.Mobile.Services;
using PesquisaCep.Service;
using Prism.Mvvm;

namespace PesquisaCep.Mobile.ViewModels
{
    public class BaseViewModel : BindableBase
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        public IZipCode ZipCodeService => DependencyService.Get<IZipCode>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
    }
}
