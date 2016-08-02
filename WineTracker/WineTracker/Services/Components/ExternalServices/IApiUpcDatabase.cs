using System.Threading.Tasks;
using WineTracker.Models;

namespace WineTracker.Services.Components.ExternalServices
{
    public interface IApiUpcDatabase
    {
        Task<ProductInfo> GetInfoByUpc(string upc);
    }
}