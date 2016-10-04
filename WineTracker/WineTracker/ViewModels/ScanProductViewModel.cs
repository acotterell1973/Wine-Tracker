using System;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using PropertyChanged;
using Tesseract;
using WineTracker.Models;
using WineTracker.PageModels;
using WineTracker.RepositoryServices;
using WineTracker.RepositoryServices.Components;
using Xamarin.Forms;
using ZXing.Mobile;


namespace WineTracker.ViewModels
{
    [ImplementPropertyChanged]
    public class ScanProductViewModel : BaseViewModel<ProductInfo>
    {
        private readonly IUpcCodeService _upcCodeSerivce;
        private readonly ITesseractApi _tesseractApi;
        private readonly IWineHunterComponent _wineHunterComponent;
        CancellationTokenSource _lastCancelSource;
        private Image _takenImage;

        public ScanProductViewModel(IUpcCodeService upcCodeSerivce, ITesseractApi tesseractApi, IWineHunterComponent wineHunterComponent)
        {
            _upcCodeSerivce = upcCodeSerivce;
            _tesseractApi = tesseractApi;
            _wineHunterComponent = wineHunterComponent;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Model = new ProductInfo { number = "7572000081" };
        }

        #region Helpers
        private async Task<MediaFile> TakePic()
        {
            var mediaPicker = DependencyService.Get<IMedia>() ?? CrossMedia.Current;
            await mediaPicker.Initialize();

            if (!mediaPicker.IsCameraAvailable || !mediaPicker.IsTakePhotoSupported) return null;
            var mediaStorageOptions = new StoreCameraMediaOptions
            {
                DefaultCamera = CameraDevice.Rear,
                Directory = "Sample",
                Name = "test.jpg"
            };
            var mediaFile = await mediaPicker.TakePhotoAsync(mediaStorageOptions);

            return mediaFile;
        }
        private async Task<ProductInfo> QueryUpc(string upc)
        {
            Model.number = upc;
            _lastCancelSource?.Cancel();

            // Perform the _search
            _lastCancelSource = new CancellationTokenSource();
            var cancellationToken = _lastCancelSource.Token;
            var product = await _upcCodeSerivce.GetProductByUpcCode(cancellationToken, upc);
            return product;
        }
        #endregion
        #region Commands
        public Command UpcEntryCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;
                    if (Model != null) Model = await QueryUpc(Model.number);
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
                    var counts = await _wineHunterComponent.GetWineCount();
                    var scanner = new MobileBarcodeScanner();
                    var upc = await scanner.Scan();
                    Model = await QueryUpc(upc?.Text);
                });
            }
        }

        public Command TakePicture
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (!_tesseractApi.Initialized)
                            await _tesseractApi.Init("eng");

                        var photo = await TakePic();
                        if (photo != null)
                        {
                            var photoStream = photo.GetStream();
                            var imageBytes = new byte[photoStream.Length];
                            photoStream.Position = 0;
                            photoStream.Read(imageBytes, 0, (int)photoStream.Length);
                            photoStream.Position = 0;

                            _takenImage = new Image { Source = ImageSource.FromStream(() => photoStream) };
                            var tessResult = await _tesseractApi.SetImage(imageBytes);
                            if (tessResult)
                            {
                                Model.ScannedText = _tesseractApi.Text;
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        AsyncErrorHandler.HandleException(exception);
                    }

                });
            }
        }
        #endregion
    }
}
