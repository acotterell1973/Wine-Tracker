using System.Threading;
using System.Threading.Tasks;
using WineTracker.Models;

namespace WineTracker.Services
{
    public interface IUpcCodeService
    {
        Task<WineItemInfo> GetProductByUpcCode(CancellationToken cancellationToken, string code);
    }
}
