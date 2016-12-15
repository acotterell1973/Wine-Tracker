using System;
using FreshMvvm;
using Tesseract;
using Tesseract.iOS;
using WineTracker.Helpers;
using WineTracker.RepositoryServices;
using WineTracker.RepositoryServices.Components;
using WineTracker.RepositoryServices.Components.ExternalServices;
using WineTracker.ViewModels;
using Xamarin.Forms;
using Constants = WineTracker.Helpers.Constants;
using WineTracker.Styles;
using WineTracker.NavigationService;

namespace WineTracker
{
    public class App : Application
    {


        public App()
        {

            DefaultStyle.InitStyles();
            Resources = DefaultStyle.StyleDictionary;

            RegisterDependancies();
            RegisterRootNavigation();
        }
        private void RegisterRootNavigation()
        {
             
            var loginPage = FreshPageModelResolver.ResolvePageModel<LoginViewModel>();
            var loginContainer = new FreshNavigationContainer(loginPage, NavigationContainerNames.AuthenticationContainer);
            var masterDetailContainer = new ThemedMasterDetailNavigationContainer(NavigationContainerNames.MainContainer);
            masterDetailContainer.Init("Menu", "Menu-30.png");
            masterDetailContainer.AddPageWithIcon<DashboardViewModel>("Home", "Plus-30.png");


            MainPage = loginContainer;
        }

        private static void RegisterDependancies()
        {

            Akavache.BlobCache.ApplicationName = Constants.CacheName;
            FreshIOC.Container.Register<IApiUpcDatabase, ApiUpcDatabase>();
            FreshIOC.Container.Register<IApiGooglePlacesDatabase, ApiGooglePlacesDatabase>();
            FreshIOC.Container.Register<ITesseractApi, TesseractApi>();
            FreshIOC.Container.Register<IWineHunterComponent, WineHunterComponent>();
            FreshIOC.Container.Register<IGeoLocationComponent, GeoLocationComponent>();

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
