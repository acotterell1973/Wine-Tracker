using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Polly;
using WineTracker.Interface;
using WineTracker.Models;
using WineTracker.Services.Components.ExternalServices;
using Xamarin;

namespace WineTracker.Services.Components
{
    public class GeoLocationComponent : IGeoLocationComponent
    {
        private readonly IApiGooglePlacesDatabase _apiGooglePlacesDatabase;
        public GeoLocationComponent(IApiGooglePlacesDatabase apiGooglePlacesDatabase)
        {
            _apiGooglePlacesDatabase = apiGooglePlacesDatabase;
        }
        public async Task<Position> GetCurentLocation(CancellationToken cancellationToken)
        {
            var policy = Policy.Handle<Exception>().RetryAsync(3, (exception, attempt) =>
            {
                Insights.Report(exception);
            });
            try
            {
                return await policy.ExecuteAsync(async () =>
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;
                    return await locator.GetPositionAsync(timeoutMilliseconds: 10000, token: cancellationToken);

                });
            }
            catch (Exception exception)
            {
                Insights.Report(exception);
            }

            return null;
        }

        public async Task<GeoCode> GetAddressesByGeoCode(CancellationToken cancellationToken, string lat, string longt)
        {
            var policy = Policy.Handle<Exception>().RetryAsync(3, (exception, attempt) =>
            {
                Insights.Report(exception, new Dictionary<string, string>
                {
                    { "Number Of Attempts", attempt.ToString()},
                    { "Latitude", lat},
                     { "Longtitude", longt}
                });
            });
            try
            {
                return await policy.ExecuteAsync(async () =>
                {
                    using (Insights.TrackTime("GeoAddress", "GetAddressByGeoCodeTask", lat + "|" + longt))
                    {
                        return await _apiGooglePlacesDatabase.GetAddressByGeoCodeTask(lat, longt);
                    }
                });
            }
            catch (Exception exception)
            {
                Insights.Report(exception);
            }

            return null;

        }

        public async Task<GeoPlaces> GetNearByPlacesTask(CancellationToken cancellationToken, string lat, string longt)
        {
            var policy = Policy.Handle<Exception>().RetryAsync(3, (exception, attempt) =>
            {
                Insights.Report(exception, new Dictionary<string, string>
                {
                    { "Number Of Attempts", attempt.ToString()},
                    { "Latitude", lat},
                     { "Longtitude", longt}
                });
            });
            try
            {
                return await policy.ExecuteAsync(async () =>
                {
                    using (Insights.TrackTime("GeoAddress", "GetNearByPlacesTask", lat + "|" + longt))
                    {
                        return await _apiGooglePlacesDatabase.GetPlacesNearByTask(lat, longt);
                    }
                });
            }
            catch (Exception exception)
            {
                Insights.Report(exception);
            }

            return null;

        }
    }
}
