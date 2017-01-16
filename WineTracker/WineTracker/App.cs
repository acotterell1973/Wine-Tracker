using System;
using System.Collections.Generic;
using FreshMvvm;
using Tesseract;
using Tesseract.iOS;
using WineTracker.Helpers;
using WineTracker.Interface;
using WineTracker.Models.DirectLineClient;
using WineTracker.ViewModels;
using Xamarin.Forms;
using Constants = WineTracker.Helpers.Constants;
using WineTracker.Styles;
using WineTracker.NavigationService;
using WineTracker.Services;
using WineTracker.Services.Components;
using WineTracker.Services.Components.ExternalServices;

namespace WineTracker
{
    public class App : Application
    {
        public static string DirectLineKey = "Yw6Eq8yum9A.cwA.ksU.D03WNexhL_LBCPUtb1jF78EqeNttBTtyw4bVp_yeKrA";
        public static BotUser BotSender = new BotUser { Id = "2CC8343", DisplayName = "You" };
        public static BotUser BotFriend = new BotUser { Id = "BADB229", DisplayName = "WineHunter Bot" };
        public App()
        {

            DefaultStyle.InitStyles();

            Resources = DefaultStyle.StyleDictionary;

            Styles.Styles.InitStyles();
            foreach (KeyValuePair<string, object> keyValuePair in Styles.Styles.StyleDictionary)
            {
                if (!Resources.ContainsKey(keyValuePair.Key))
                    Resources.Add(keyValuePair.Key, keyValuePair.Value);
            }


            RegisterDependancies();
            RegisterRootNavigation();
        }
        private void RegisterRootNavigation()
        {

            var loginPage = FreshPageModelResolver.ResolvePageModel<ChatBotViewModel>();
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
            FreshIOC.Container.Register<IDirectLineApiClient, DirectLineApiClient>();
            FreshIOC.Container.Register<IChatServices, ChatServices>();
            FreshIOC.Container.Register<IUpcCodeComponent, UpcCodeComponent>();
            FreshIOC.Container.Register<IUpcCodeService, UpcCodeSerivce>();
            FreshIOC.Container.Register<ICognitiveService, CognitiveService>();
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
