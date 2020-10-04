using PesquisaCep.Model;
using System.Threading.Tasks;

namespace PesquisaCep.Service
{
    public interface IZipCode
    {
        Task<ZipCodeInfo> GetZipCodeInfo(string zipCode);
    }
}
