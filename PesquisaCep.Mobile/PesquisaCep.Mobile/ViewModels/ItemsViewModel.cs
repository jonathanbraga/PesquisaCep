using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using PesquisaCep.Mobile.Models;
using PesquisaCep.Model;

namespace PesquisaCep.Mobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private bool _loading;
        private ObservableCollection<ZipCodeInfo> _items;

        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

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
                    Items = new ObservableCollection<ZipCodeInfo>(data.DataConnection.Table<ZipCodeInfo>().ToList());

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