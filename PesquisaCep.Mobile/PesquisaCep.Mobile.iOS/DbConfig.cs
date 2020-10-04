using System.IO;
using PesquisaCep.Service;
using Xamarin.Forms;

[assembly: Dependency(typeof(PesquisaCep.Mobile.iOS.DbConfig))]
namespace PesquisaCep.Mobile.iOS
{
    public class DbConfig : IDbConfig
    {
        public string DirectoryDB => Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "pesquisaCepDB");
    }
}