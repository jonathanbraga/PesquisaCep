using PesquisaCep.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PesquisaCep.Service
{
    public interface IDbActions
    {
        Task SaveDate(ZipCodeInfo data);
        Task<IList<ZipCodeInfo>> GetData();
    }
}
