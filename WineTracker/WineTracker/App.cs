using System;
using FreshMvvm;
using WineTracker.Helpers;
using WineTracker.Services;
using WineTracker.Services.Components;
using WineTracker.ViewModels;
using Xamarin.Forms;
using Constants = WineTracker.Helpers.Constants;

namespace WineTracker
{
    public class App : Application
    {
        public App()
        {
            RegisterDependancies();
            RegisterRootNavigation();
        }
        private void RegisterRootNavigation()
        {
            var page = FreshPageModelResolver.ResolvePageModel<ScanProductViewModel>();
            var logginNavigation = new FreshNavigationContainer(page, Constants.LoginNavigationService);
        
            MainPage = logginNavigation;
        }
        private static void RegisterDependancies()
        {
            Akavache.BlobCache.ApplicationName = Constants.CacheName;
            FreshIOC.Container.Register<IUpcCodeComponent, UpcCodeComponent>();

            FreshIOC.Container.Register<IUpcCodeService, UpcCodeSerivce>();

            FreshIOC.Container.Register(HttpClientConnector.Instance);
        }

        public async void TriggerCameraScanner()
        {
            // or remove asyn, do task.run...
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan();

            BarcodeScanned(null, new BarcodeScannedEventArgs(result.Text));
        }
        private void BarcodeScanned(object sender, BarcodeScannedEventArgs e)
        {
            MessagingCenter.Send(this, Constants.Scanned, e);
        }

        // Event handler for barcode scan
        public static void OnBarcodeScan(Page page, Action<string> callback)
        {
            MessagingCenter.Subscribe<App, BarcodeScannedEventArgs>(page, Constants.Scanned, (sender, args) =>
            {
                var scannedCode = args.Data.TrimEnd();
                callback?.Invoke(scannedCode);
            });
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }

    public class BarcodeScannedEventArgs
    {
        public BarcodeScannedEventArgs(string text)
        {
            throw new NotImplementedException();
        }

        public string Data { get; set; }
    }
}
