using System.Threading;
using System.Threading.Tasks;
using PropertyChanged;
using Tesseract;
using WineTracker.Models;
using WineTracker.PageModels;
using WineTracker.RepositoryServices;
using WineTracker.RepositoryServices.Components;
using Xamarin.Forms;
using ZXing.Mobile;
using Result = WineTracker.Models.Result;

namespace WineTracker.ViewModels
{
    [ImplementPropertyChanged]
    public class WineCaptureViewModel : BaseViewModel<BottleInfo>
    {
        private readonly IUpcCodeService _upcCodeSerivce;
    //    private readonly ITesseractApi _tesseractApi;
        private readonly IWineHunterComponent _wineHunterComponent;
        private readonly IGeoLocationComponent _geoLocationComponent;
        private  CancellationTokenSource _cancellationToken;
     //   private Image _takenImage;
        private Item _selectedLocation;

        public WineCaptureViewModel(IUpcCodeService upcCodeSerivce, ITesseractApi tesseractApi, IWineHunterComponent wineHunterComponent, IGeoLocationComponent geoLocationComponent)
        {
            _upcCodeSerivce = upcCodeSerivce;
        //    _tesseractApi = tesseractApi;
            _wineHunterComponent = wineHunterComponent;
            _geoLocationComponent = geoLocationComponent;
            _cancellationToken = new CancellationTokenSource();

        }
        public override void Init(object initData)
        {
            base.Init(initData);
            Model = new BottleInfo();
            Task.Run(async () =>
            {
                var position = await _geoLocationComponent.GetCurentLocation(_cancellationToken.Token);


                if (position == null) return ;

                //Get Address Info from GeCode
                Locations = await _geoLocationComponent.GetNearByPlacesTask(_cancellationToken.Token, position.Latitude.ToString(), position.Longitude.ToString());

            }, _cancellationToken.Token);
            
        }

        #region ViewModel Properties

        public GeoPlaces Locations { get; set; }
        public Result SelectedLocation
        {
            get { return GetValue<Result>(); }
            set
            {
                SetValue(value);
                //  SomeCommand.Execute(_selectedAddressItem);
             //   SelectedLocation = null;
            }



        }
        #endregion
        #region Commands
        public Command ScanbarCode
        {
            get
            {
                return new Command(async () =>
                {
                    var counts = await _wineHunterComponent.GetWineCount();
                    var scanner = new MobileBarcodeScanner();
                    var upc = await scanner.Scan();
                    Model = await QueryUpc(upc?.Text);
                });
            }
        }

        public Command CancelCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PopPageModel();
                });
            }
        }
        #endregion

        #region Helpers
        private async Task<BottleInfo> QueryUpc(string upc)
        {
            Model.Bottle.Upc = upc;
            _cancellationToken?.Cancel();

            // Perform the _search
            _cancellationToken = new CancellationTokenSource();
            var cancellationToken = _cancellationToken.Token;
            var product = await _upcCodeSerivce.GetProductByUpcCode(cancellationToken, upc);
            var bottleInfo = new BottleInfo();
            return bottleInfo;
        }
        #endregion
    }
}
