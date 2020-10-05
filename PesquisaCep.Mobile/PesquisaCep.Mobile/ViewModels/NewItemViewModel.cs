using System;
using System.Linq;
using PesquisaCep.Model;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PesquisaCep.Mobile.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string _zipCode;
        private string description;
        private bool _loading;
        private ZipCodeInfo _zipCodeInfoData;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command SearchZipCodeCommand { get; set; }

        public string ZipCode
        {
            get => _zipCode;
            set => SetProperty(ref _zipCode, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public ZipCodeInfo ZipCodeInfoData
        {
            get => _zipCodeInfoData;
            set 
            {
                CanDisplay = value?.CEP != null;
                SetProperty(ref _zipCodeInfoData, value);
            }
        }

        public bool Loading
        {
            get { return _loading; }
            set { SetProperty(ref _loading, value); }
        }

        private bool _canDisplay;
        public bool CanDisplay
        {
            get { return _canDisplay; }
            set { SetProperty(ref _canDisplay, value);}
        }

        public NewItemViewModel()
        {
            SearchZipCodeCommand = new Command(OnSearchZipCodeCommand);
            SaveCommand = new Command(OnSave);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSearch()
        {
            return !String.IsNullOrWhiteSpace(_zipCode);
        }

        private async void OnSave()
        {
            if(ZipCodeInfoData == null || ZipCodeInfoData?.CEP == null)
            {
                await App.Current.MainPage.DisplayAlert(Constants.MESSAGE_TYPE_WARNNING, Constants.MESSAGE_NODATAFOUND, Constants.MESSAGE_BUTTONTITLE);
                return;
            }
            using(var data = new Store.LocalStore())
            {
                try
                {
                    var checkObj = data.DataConnection.Table<ZipCodeInfo>().FirstOrDefault(p => p.CEP == ZipCodeInfoData.CEP);
                    if(checkObj == null)
                    {
                        var locations = await Geocoding.GetLocationsAsync(ZipCodeInfoData?.Logradouro);
                        var place = locations.FirstOrDefault();

                        if(place != null)
                        {
                            ZipCodeInfoData.Latitude = place.Latitude;
                            ZipCodeInfoData.Longitude = place.Longitude;

                            data.DataConnection.Insert(ZipCodeInfoData);
                            ZipCodeInfoData = new ZipCodeInfo();
                            ZipCode = string.Empty;
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert(Constants.MESSAGE_TYPE_WARNNING, Constants.MESSAGE_DATAEXIST, Constants.MESSAGE_BUTTONTITLE);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private async void OnSearchZipCodeCommand()
        {
            Loading = true;
            try
            {
                ZipCodeInfoData = await ZipCodeService.GetZipCodeInfo(ZipCode);
                if(ZipCodeInfoData?.CEP == null)
                {
                    await App.Current.MainPage.DisplayAlert(Constants.MESSAGE_TYPE_WARNNING, Constants.MESSAGE_NODATAFOUND, Constants.MESSAGE_BUTTONTITLE);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            } 
            finally
            {
                Loading = false;
            }
        }
    }
}
