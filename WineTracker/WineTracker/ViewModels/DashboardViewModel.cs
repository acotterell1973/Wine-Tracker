using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tesseract;
using WineTracker.Models;
using WineTracker.PageModels;
using WineTracker.Services;
using Xamarin.Forms;

namespace WineTracker.ViewModels
{
    public class DashboardViewModel : BaseViewModel<ProductInfo>
    {

        private readonly IUpcCodeService _upcCodeSerivce;
        private readonly ITesseractApi _tesseractApi;
        CancellationTokenSource _lastCancelSource;
        
        public DashboardViewModel(IUpcCodeService upcCodeSerivce, ITesseractApi tesseractApi)
        {

            _upcCodeSerivce = upcCodeSerivce;
            _tesseractApi = tesseractApi;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Model = new ProductInfo { number = "7572000081" };
        }

    }
}
