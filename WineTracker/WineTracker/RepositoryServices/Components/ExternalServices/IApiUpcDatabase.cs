using System.Threading.Tasks;
using WineTracker.Models;

namespace WineTracker.RepositoryServices.Components.ExternalServices
{
    public interface IApiUpcDatabase
    {
        Task<ProductInfo> GetInfoByUpc(string upc);
    }
}