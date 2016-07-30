using System.Threading;
using System.Threading.Tasks;
using WineTracker.Models;


namespace WineTracker.Services.Components
{
    public interface IUpcCodeComponent
    {
        Task<ProductInfo> GetProductByUpcCode(CancellationToken cancellationToken, string upccode);
    }
}