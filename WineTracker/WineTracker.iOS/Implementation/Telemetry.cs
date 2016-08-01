using System;
using WineTracker.iOS.Implementation;
using WineTracker.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(Telemetry))]
namespace WineTracker.iOS.Implementation
{
	public class Telemetry :ITelemetry
	{
	

        public void LogToDevice(string loginfo)
        {
            var newExc = new Exception(loginfo);
            AppDelegate.LogUnhandledException(newExc);
        }

        public void LogToDevice(string loginfo, Exception innerException)
        {
            var newExc = new Exception(loginfo, innerException);
            AppDelegate.LogUnhandledException(newExc);
        }

	    public void ShowLog()
	    {
            Device.BeginInvokeOnMainThread(() =>
            {
                AppDelegate.DisplayCrashReport();
            });
	        
	    }
	}

}
