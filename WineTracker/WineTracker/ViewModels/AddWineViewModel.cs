using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.Media;
using Plugin.Media.Abstractions;
using PropertyChanged;
using WineTracker.Extensions;
using WineTracker.Interface;
using WineTracker.Models;
using WineTracker.Services;
using WineTracker.Services.Components;
using Xamarin.Forms;
using ZXing.Mobile;

namespace WineTracker.ViewModels
{
    [ImplementPropertyChanged]
    public class AddWineViewModel : BaseViewModel<WineItemInfo>
    {
        private readonly IUpcCodeService _upcCodeSerivce;
        private readonly IWineHunterComponent _wineHunterComponent;
        private EventViewModelFactory _eventViewModelFactory;
        private IChatServices _chatServices;
        private readonly ICognitiveService _cognitiveService;
        private CancellationTokenSource _cancellationToken;
        private const float DefaultChatWindowHeight = 500;

        public AddWineViewModel(IUpcCodeService upcCodeSerivce, IWineHunterComponent wineHunterComponent, ICognitiveService cognitiveService, IChatServices chatServices)
        {
            _upcCodeSerivce = upcCodeSerivce;
            _wineHunterComponent = wineHunterComponent;
            _cognitiveService = cognitiveService;
            _cancellationToken = new CancellationTokenSource();
            Events = new ObservableCollection<EventViewModel>();
            _eventViewModelFactory = new EventViewModelFactory();
            _chatServices = chatServices;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Model = new WineItemInfo();

            //Adjust the chat windows when the keyboad is open or close.
            KeyboardHelper.KeyboardChanged += (sender, e) =>
            {
                //Default Height is 500
                ChatLayoutHeight = e.Visible ? ChatLayoutHeight - (e.Height + 20) : DefaultChatWindowHeight;
            };
        }

        #region property
        public ObservableCollection<EventViewModel> Events { get; set; }
        public string ChatInputText { get; set; }
        public string AuthorName { get; set; }
        public string MessageLabel { get; set; }
        public string Status { get; set; }
        public ImageSource ImageSource { get; set; }

        public float ChatLayoutHeight { get; set; } = DefaultChatWindowHeight;
        #endregion


        #region Commands
        public Command ScanbarCode
        {
            get
            {
                return new Command(async () =>
                {
                    //    var counts = await _wineHunterComponent.GetWineCount();
                    var scanner = new MobileBarcodeScanner();
                    var upc = await scanner.Scan();
                    Model = await QueryUpc(upc?.Text);
                });
            }
        }

        public Command ScanImage
        {
            get
            {
                return new Command(async () =>
                {
                    var success = await TakePhotoAsync();
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

        public ICommand SendMessageCommand => new Command(OnSendMessage);

        #endregion


        #region Helpers

        private async void OnSendMessage()
        {
            if (string.IsNullOrEmpty(ChatInputText))
                return;
            var text = ChatInputText;
            ChatInputText = string.Empty;
            await _chatServices.SendMessage(text);

            _chatServices.Messages.SynchronizeWith(Events, i => _eventViewModelFactory.Get(i, App.BotSender.DisplayName));
        }
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
        /// <summary>
        /// The _scheduler.
        /// </summary>
        private readonly TaskScheduler _scheduler = TaskScheduler.FromCurrentSynchronizationContext();



        public async Task<bool> TakePhotoAsync()
        {
            MediaFile photo;
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "WineHunter",
                    PhotoSize = PhotoSize.Medium,
                    DefaultCamera = CameraDevice.Front
                }).ContinueWith(t =>
                {
                    if (t.IsFaulted)
                    {
                        Status = t.Exception.InnerException.ToString();
                    }
                    else if (t.IsCanceled)
                    {
                        Status = "Canceled";
                    }
                    else
                    {
                        var mediaFile = t.Result;

                        ImageSource = ImageSource.FromStream(() => mediaFile.GetStream());

                        return mediaFile;
                    }

                    return null;
                }, _scheduler);
            }
            else
            {
                photo = await CrossMedia.Current.PickPhotoAsync();
            }

            using (var stream = photo.GetStream())
            {
                var extractedText = await _cognitiveService.ExtractImageTextStringAsync("en", stream);
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
