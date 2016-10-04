using System.Threading;
using System.Threading.Tasks;
using WineTracker.Models;

namespace WineTracker.RepositoryServices
{
    public interface IUpcCodeService
    {
        Task<ProductInfo> GetProductByUpcCode(CancellationToken cancellationToken, string code);
    }
}
