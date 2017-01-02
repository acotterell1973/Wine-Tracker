using System;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using PropertyChanged;
using Tesseract;
using WineTracker.Interface;
using WineTracker.Models;
using WineTracker.Services;
using WineTracker.Services.Components;
using Xamarin.Forms;
using ZXing.Mobile;
using Result = WineTracker.Models.Result;

namespace WineTracker.ViewModels
{
    [ImplementPropertyChanged]
    public class WineCaptureViewModel : BaseViewModel<WineItemInfo>
    {
        private readonly IUpcCodeService _upcCodeSerivce;
        //    private readonly ITesseractApi _tesseractApi;
        private readonly IWineHunterComponent _wineHunterComponent;
        private readonly IGeoLocationComponent _geoLocationComponent;
        private CancellationTokenSource _cancellationToken;
        //   private Image _takenImage;
        private Item _selectedLocation;
        private readonly ICognitiveService _visionServiceClient;
        public WineCaptureViewModel(IUpcCodeService upcCodeSerivce, ITesseractApi tesseractApi,
            IWineHunterComponent wineHunterComponent, IGeoLocationComponent geoLocationComponent,
            ICognitiveService cognitiveService)
        {
            _upcCodeSerivce = upcCodeSerivce;
            _visionServiceClient = cognitiveService;
            //    _tesseractApi = tesseractApi;
            _wineHunterComponent = wineHunterComponent;
            _geoLocationComponent = geoLocationComponent;
            _cancellationToken = new CancellationTokenSource();

        }
        public override void Init(object initData)
        {
            base.Init(initData);
            Model = new WineItemInfo();

            Task.Run(async () =>
            {
                IsBusy = true;
                var position = await _geoLocationComponent.GetCurentLocation(_cancellationToken.Token);


                if (position == null) return;

                //Get Address Info from GeCode
                Locations = await _geoLocationComponent.GetNearByPlacesTask(_cancellationToken.Token, position.Latitude.ToString(), position.Longitude.ToString());
                IsBusy = false;
                //Device.BeginInvokeOnMainThread(() =>
                //{
                //    ScanbarCode.Execute(null);
                //});
                // Model = await QueryUpc("0010986007634");
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
        public Command SendMessageCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<AddWineViewModel>();
                });
            }
        }
        public Command ScanbarCode
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<AddWineViewModel>();
                });
            }
        }

        public Command ScanImage
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<AddWineViewModel>();
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
        private async Task<WineItemInfo> QueryUpc(string upc)
        {
            Model.Upc = upc;
            _cancellationToken?.Cancel();

            // Perform the _search
            _cancellationToken = new CancellationTokenSource();
            var cancellationToken = _cancellationToken.Token;
            var bottleInfo = await _upcCodeSerivce.GetProductByUpcCode(cancellationToken, upc);

            return bottleInfo;
        }

        public async Task<bool> TakePhotoAsync()
        {
            MediaFile photo;
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "WineHunter",
                    PhotoSize = PhotoSize.Medium
                });
            }
            else
            {
                photo = await CrossMedia.Current.PickPhotoAsync();
            }

            using (var stream = photo.GetStream())
            {
                var extractedText = await _visionServiceClient.ExtractImageTextStringAsync("en", stream);
                Device.BeginInvokeOnMainThread(() =>
                {
                    try
                    {
                        Model.Producer = extractedText;
                    }
                    catch (Exception ex)
                    {

                    }
                });

            }
            return true;
        }
        #endregion
    }
}
