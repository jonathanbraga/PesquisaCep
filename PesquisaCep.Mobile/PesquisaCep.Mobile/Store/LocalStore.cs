using PesquisaCep.Model;
using PesquisaCep.Service;
using SQLite;
using System;
using Xamarin.Forms;

namespace PesquisaCep.Mobile.Store
{
    public class LocalStore : IDisposable
    {
        public SQLiteConnection DataConnection { get; set; }

        public LocalStore()
        {
            var config = DependencyService.Get<IDbConfig>();
            DataConnection = new SQLiteConnection(config.DirectoryDB);
            DataConnection.CreateTable<ZipCodeInfo>();
        }

        public void Dispose()
        {
            DataConnection.Dispose();
        }
    }
}
