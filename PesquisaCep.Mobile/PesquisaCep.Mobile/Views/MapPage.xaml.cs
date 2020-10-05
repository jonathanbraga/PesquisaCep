using PesquisaCep.Mobile.ViewModels;
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

            this.map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(-15.807889, -47.873588), Distance.FromKilometers(10000.0)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
