using System;
using Xamarin;

namespace WineTracker.Components
{
    public static class AsyncErrorHandler
    {
        public static void HandleException(Exception exception)
        {
            Insights.Report(exception);
        }
    }
}
