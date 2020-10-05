using PesquisaCep.Mobile.ViewModels;
using PesquisaCep.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace PesquisaCep.Mobile.Views
{
    public partial class MapPage : ContentPage
    {
        MapPageViewModel _viewModel;

        public MapPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MapPageViewModel();

            this.map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Constants.DEFAULT_LATITUDE, Constants.DEFAULT_LONGITUDE), Distance.FromKilometers(10000.0)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
