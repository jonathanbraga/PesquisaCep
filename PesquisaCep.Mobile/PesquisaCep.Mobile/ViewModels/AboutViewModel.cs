using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PesquisaCep.Mobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";
            OpenWebCommand = new Command(async () => {
                var teste = await ZipCodeService.GetZipCodeInfo("59152600");
                var novo = teste;
            });
        }

        public ICommand OpenWebCommand { get; }
    }
}