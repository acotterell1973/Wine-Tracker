using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using UrbanAirship;
using WineTracker;
using WineTracker.iOS;
using Xamarin;
using XLabs.Forms;
using XLabs.Platform.Device;

namespace WineTracker.iOS
{
   
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : XFormsApplicationDelegate
    {
        // class-level declarations
        NSObject observer;
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Settings.LoadDefaultValues();
            observer = NSNotificationCenter.DefaultCenter.AddObserver((NSString)"NSUserDefaultsDidChangeNotification", DefaultsChanged);
            DefaultsChanged(null);

            Insights.HasPendingCrashReport += Insights_HasPendingCrashReport;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            // Initialize Insights
            Insights.HasPendingCrashReport += (sender, isStartupCrash) =>
            {
                if (isStartupCrash)
                {
                    Insights.PurgePendingCrashReports().Wait();
                }
            };
            Insights.Initialize("3d83ba77a780617613caa96baf8226577b204f67");
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            UAirship.TakeOff();
            UAirship.Push.UserPushNotificationsEnabled = true;
            var chID = UAirship.Push.ChannelID;
            var devToken = UAirship.Push.DeviceToken;
            return base.FinishedLaunching(app, options);
        }

        /// <summary>
        /// This method is called when the application is about to terminate. Save data, if needed.
        /// </summary>
        /// <seealso cref="XFormsApplicationDelegate.DidEnterBackground"/>
        public override void WillTerminate(UIApplication application)
        {
            if (observer == null) return;
            NSNotificationCenter.DefaultCenter.RemoveObserver(observer);
            observer = null;
        }

        private void DefaultsChanged(NSNotification obj)
        {
            Settings.SetupByPreferences();
        }
        private void Insights_HasPendingCrashReport(object sender, bool isStartupCrash)
        {
          
        }

        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs unobservedTaskExceptionEventArgs)
        {
            var newExc = new Exception("TaskSchedulerOnUnobservedTaskException", unobservedTaskExceptionEventArgs.Exception);
            LogUnhandledException(newExc);
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            var newExc = new Exception("CurrentDomainOnUnhandledException", unhandledExceptionEventArgs.ExceptionObject as Exception);
            LogUnhandledException(newExc);
        }

        internal static void LogUnhandledException(Exception exception)
        {
            try
            {
                Insights.Report(exception);
                const string errorFileName = "Fatal.log";
                var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Resources);
                var errorFilePath = Path.Combine(libraryPath, errorFileName);
                var errorMessage = $"Time: {DateTime.Now}\r\nError: Unhandled Exception\r\n{exception.ToString()}";
                File.WriteAllText(errorFilePath, errorMessage);

              
            }
            catch
            {
                // just suppress any error logging exceptions
            }
        }


        [Conditional("DEBUG")]
        internal static void DisplayCrashReport()
        {
            const string errorFilename = "Fatal.log";
            var libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Resources);
            var errorFilePath = Path.Combine(libraryPath, errorFilename);

            if (!File.Exists(errorFilePath))
            {
                return;
            }

            var errorText = File.ReadAllText(errorFilePath);
            var alertView = new UIAlertView("Crash Report", errorText, null, "Close", "Clear") { UserInteractionEnabled = true };
            alertView.Clicked += (sender, args) =>
            {
                if (args.ButtonIndex != 0)
                {
                    File.Delete(errorFilePath);
                }
            };
            alertView.Show();
        }
    }
}
