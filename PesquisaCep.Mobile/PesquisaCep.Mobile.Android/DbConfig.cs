using System.IO;
using PesquisaCep.Service;
using Xamarin.Forms;

[assembly: Dependency(typeof(PesquisaCep.Mobile.Droid.DbConfig))]
namespace PesquisaCep.Mobile.Droid
{
    public class DbConfig : IDbConfig
    {
        public string DirectoryDB => Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "pesquisaCepDB");
    }
}