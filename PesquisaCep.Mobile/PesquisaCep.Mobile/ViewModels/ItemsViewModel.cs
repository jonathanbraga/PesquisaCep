using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using PesquisaCep.Model;
using PesquisaCep.Mobile.Views;

namespace PesquisaCep.Mobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private bool _loading;
        private ObservableCollection<ZipCodeInfo> _items;

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<ZipCodeInfo> ItemTappedCommand { get; }

        public ObservableCollection<ZipCodeInfo> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        public bool Loading
        {
            get { return _loading; }
            set { SetProperty(ref _loading, value); }
        }

        public ItemsViewModel()
        {
            LoadItemsCommand = new Command(ExecuteLoadItemsCommand);
            ItemTappedCommand = new Command<ZipCodeInfo>(ExecuteItemTappedCommand);
        }

        private async void ExecuteItemTappedCommand(ZipCodeInfo obj)
        {
            if (obj == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={obj.CEP}");
        }

        private async void ExecuteLoadItemsCommand()
        {
            await RefreshItemsAsync();
        }

        Task RefreshItemsAsync()
        {
            return Task.Run(() =>
            {
                LoadLocalData();
            });
        }

        public async void OnAppearing()
        {
            await RefreshItemsAsync();
        }

        private void LoadLocalData()
        {
            using (var data = new Store.LocalStore())
            {
                try
                {
                    Loading = true;
                    Items = new ObservableCollection<ZipCodeInfo>(data.DataConnection.Table<ZipCodeInfo>().OrderByDescending(x => x.CEP).ToList());

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