using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Akavache;
using WineTracker.Models;
using WineTracker.Services.Components;

namespace WineTracker.Services
{
    public class UpcCodeSerivce : IUpcCodeService
    {
        private readonly UpcCodeComponent _upcCodeComponent;
        public UpcCodeSerivce(UpcCodeComponent upcCodeComponent1)
        {
            _upcCodeComponent = upcCodeComponent1;
        }

        public async Task<ProductInfo> GetProductByUpcCode(CancellationToken cancellationToken, string code)
        {
            if (_upcCodeComponent == null) return null;

            string key = $"GetProductByUpcCode::{code}";

            var productInfo = await BlobCache.LocalMachine.GetAndFetchLatest(
                key,
                async () =>
                {
                    var result = await _upcCodeComponent.GetProductByUpcCode(cancellationToken, code);
                    return result;
                }, null, null);

            return productInfo;
        }
    }
}
