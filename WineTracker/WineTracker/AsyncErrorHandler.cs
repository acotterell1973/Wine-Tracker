using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using WineTracker.Extensions;
using WineTracker.Interface;
using Xamarin;
using Xamarin.Forms;

namespace WineTracker
{
    public static class AsyncErrorHandler
    {
        private static bool _skipAdditionCall;

        private static void AddUserInfo(Dictionary<string, string> additionalInformation)
        {
            additionalInformation.Add("USER INFO START", "++++++++++++++++++++++++++++++++++++++++++");

            additionalInformation.Add("USER INFO END", "++++++++++++++++++++++++++++++++++++++++++");
        }

        public static void HandleInsightException<T>(Exception exception, T objectData)
        {
            _skipAdditionCall = true;
            var additionalInformation = objectData.ToDictionary<T>() as Dictionary<string, string>;
            AddUserInfo(additionalInformation);

            Insights.Report(exception, additionalInformation);
            HandleException(exception);
        }

        public static void HandleInsightException<T>(Exception exception, Dictionary<string, string> additionalInformation, T objectData)
        {
            _skipAdditionCall = true;
            additionalInformation.Add("OBJECT DATA START", "++++++++++++++++++++++++++++++++++++++++++");
            additionalInformation.Add("OBJECT DATA", JsonConvert.SerializeObject(objectData));
            additionalInformation.Add("OBJECT DATA END", "++++++++++++++++++++++++++++++++++++++++++++");
            AddUserInfo(additionalInformation);

            Insights.Report(exception, additionalInformation);
            HandleException(exception);

        }
        public static void HandleInsightException(Exception exception, Dictionary<string, string> additionalInformation)
        {
            _skipAdditionCall = true;
            AddUserInfo(additionalInformation);

            Insights.Report(exception, additionalInformation);
            HandleException(exception);
        }
        public static void HandleException(Exception exception)
        {
            if (!_skipAdditionCall)
            {
                Insights.Report(exception);
                _skipAdditionCall = false;
            }


#if DEBUG
            DependencyService.Get<ITelemetry>().LogToDevice($"Error : {exception.Message}", exception);
            DependencyService.Get<ITelemetry>().ShowLog();
#endif
            Device.BeginInvokeOnMainThread(() =>
            {

            });
        }
    }
}
