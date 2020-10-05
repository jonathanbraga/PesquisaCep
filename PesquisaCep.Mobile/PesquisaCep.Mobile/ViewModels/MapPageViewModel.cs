using PesquisaCep.Model;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;

namespace PesquisaCep.Mobile.ViewModels
{
    public class MapPageViewModel : BindableBase
    {
        private bool _loading;
        private ObservableCollection<LocationZipCode> _locations;

        public ObservableCollection<LocationZipCode> Locations
        {
            get { return _locations; }
            set { SetProperty(ref _locations, value); }
        }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public bool Loading
        {
            get { return _loading; }
            set { SetProperty(ref _loading, value); }
        }

        private Position _centeredReference;
        public Position CenteredReference
        {
            get { return _centeredReference; }
            set { SetProperty(ref _centeredReference, value); }
        }

        public MapPageViewModel()
        {
            Latitude = -5.8889889;
            Longitude = -35.2082431;
        }

        public async void OnAppearing()
        {
            await LoadMapPointsAsync();
        }

        Task LoadMapPointsAsync()
        {
            return Task.Run(() =>
            {
                LoadLocalData();
            });
        }

        private void LoadLocalData()
        {
            using (var data = new Store.LocalStore())
            {
                try
                {
                    Loading = true;
                    var result = new ObservableCollection<ZipCodeInfo>(data.DataConnection.Table<ZipCodeInfo>().OrderByDescending(x => x.CEP).ToList());
                    Locations = new ObservableCollection<LocationZipCode>();

                    if(result != null)
                    {
                        CenteredReference = new Position(result.FirstOrDefault().Latitude, result.FirstOrDefault().Longitude);
                        foreach (var item in result)
                        {
                            Locations.Add(new LocationZipCode
                            {
                                Position = new Xamarin.Forms.Maps.Position(item.Latitude, item.Longitude),
                                Address = item.CEP
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    Loading = false;
                }
            }
        }
    }
}
