using System.Threading;
using Tesseract;
using WineTracker.Models;
using WineTracker.PageModels;
using WineTracker.RepositoryServices;
using WineTracker.RepositoryServices.Components;
using Xamarin.Forms;

namespace WineTracker.ViewModels
{
    public class DashboardViewModel : BaseViewModel<ProductInfo>
    {

        private readonly IUpcCodeService _upcCodeSerivce;
        private readonly ITesseractApi _tesseractApi;
        private readonly IWineHunterComponent _wineHunterComponent;
        CancellationTokenSource _lastCancelSource;
        
        public DashboardViewModel(IUpcCodeService upcCodeSerivce, ITesseractApi tesseractApi, IWineHunterComponent wineHunterComponent)
        {

            _upcCodeSerivce = upcCodeSerivce;
            _tesseractApi = tesseractApi;
            _wineHunterComponent = wineHunterComponent;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Model = new ProductInfo();
        }


        #region Commands
        public Command AddWineCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<WineCaptureViewModel>();
                });
            }
        }
        #endregion
    }
}
