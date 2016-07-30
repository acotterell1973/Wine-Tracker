using System.Threading;
using System.Threading.Tasks;
using PropertyChanged;
using WineTracker.Models;
using WineTracker.PageModels;
using WineTracker.Services;
using Xamarin.Forms;

namespace WineTracker.ViewModels
{
    [ImplementPropertyChanged]
    public class ScanProductViewModel : BaseViewModel<ProductInfo>
    {
        private readonly IUpcCodeService _upcCodeSerivce;
        CancellationTokenSource _lastCancelSource;

        public ScanProductViewModel(IUpcCodeService upcCodeSerivce)
        {
            _upcCodeSerivce = upcCodeSerivce;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            Model = new ProductInfo { number = "049331000372" };
        }

        #region Helpers

        private async Task<ProductInfo> QueryUpc(string upc)
        {
            _lastCancelSource?.Cancel();

            // Perform the _search
            _lastCancelSource = new CancellationTokenSource();
            var cancellationToken = _lastCancelSource.Token;
            var product = await _upcCodeSerivce.GetProductByUpcCode(cancellationToken, upc);
            return product;
        }
        #endregion
        #region Commands
        public Command SiginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;
                    await QueryUpc(Model.number);
                    IsBusy = false;
                });
            }
        }

        public Command ScanbarCode
        {
            get
            {
                return new Command(async () =>
                {
                    var scanner = new ZXing.Mobile.MobileBarcodeScanner();
                    var upc = await scanner.Scan();
                    await QueryUpc(upc.Text);
                });
            }
        }


        #endregion
    }
}
