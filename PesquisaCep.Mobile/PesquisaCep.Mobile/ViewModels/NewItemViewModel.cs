using System;
using PesquisaCep.Mobile.Models;
using PesquisaCep.Model;
using Xamarin.Forms;

namespace PesquisaCep.Mobile.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string _zipConde;
        private string description;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command SearchZipCodeCommand { get; set; }
        public ZipCodeInfo ZipCodeInfoData { get; set; }

        public string ZipCode
        {
            get => _zipConde;
            set => SetProperty(ref _zipConde, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public NewItemViewModel()
        {
            SearchZipCodeCommand = new Command(OnSearchZipCodeCommand);
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSearch()
        {
            return !String.IsNullOrWhiteSpace(_zipConde);
        }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Text = ZipCode,
                Description = Description
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSearchZipCodeCommand()
        {
            ZipCodeInfoData = await ZipCodeService.GetZipCodeInfo(ZipCode);
        }
    }
}
