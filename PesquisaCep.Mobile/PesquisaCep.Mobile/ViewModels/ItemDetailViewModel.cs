using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PesquisaCep.Mobile.Models;
using PesquisaCep.Model;
using Xamarin.Forms;

namespace PesquisaCep.Mobile.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private ZipCodeInfo _item;
        public ZipCodeInfo Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }
        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public void LoadItemId(string itemId)
        {
            using (var data = new Store.LocalStore())
            {
                try
                {
                    Item = data.DataConnection.Table<ZipCodeInfo>().FirstOrDefault(x => x.CEP == itemId);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
