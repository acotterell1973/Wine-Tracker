using System.Threading;
using System.Threading.Tasks;
using WineTracker.Models;

namespace WineTracker.RepositoryServices
{
    public interface IUpcCodeService
    {
        Task<WineItemInfo> GetProductByUpcCode(CancellationToken cancellationToken, string code);
    }
}
