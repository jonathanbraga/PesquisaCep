using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using PesquisaCep.Mobile.Models;
using PesquisaCep.Mobile.Views;
using PesquisaCep.Model;

namespace PesquisaCep.Mobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        private ObservableCollection<ZipCodeInfo> _items;
        public ObservableCollection<ZipCodeInfo> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        public ItemsViewModel()
        {
            LoadItemsCommand = new Command(ExecuteLoadItemsCommand);

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        private async void ExecuteLoadItemsCommand()
        {
            await ExecuteLoadItemsCommandAsync();
        }

        Task ExecuteLoadItemsCommandAsync()
        {
            return Task.Run(() =>
            {
                LoadLocalData();
            });
        }

        public void OnAppearing()
        {
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }

        public void LoadLocalData()
        {
            using (var data = new Store.LocalStore())
            {
                try
                {
                    IsBusy = true;
                    Items = new ObservableCollection<ZipCodeInfo>(data.DataConnection.Table<ZipCodeInfo>().ToList());

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
    }
}