using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Polly;
using WineTracker.Models;
using WineTracker.RepositoryServices.Components.ExternalServices;
using Xamarin;

// ReSharper disable ImplicitlyCapturedClosure

namespace WineTracker.RepositoryServices.Components
{
    public class UpcCodeComponent : ComponentBase, IUpcCodeComponent
    {
        private readonly IApiUpcDatabase _apiUpcDatabase;
        private readonly IApiGooglePlacesDatabase _apiGooglePlacesDatabase;

        public UpcCodeComponent(IApiUpcDatabase apiUpcDatabase, IApiGooglePlacesDatabase apiGooglePlacesDatabase)
        {
            _apiUpcDatabase = apiUpcDatabase;
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
        public async Task<ProductInfo> GetProductByUpcCode(CancellationToken cancellationToken, string upccode)
        {
            var policy = Policy.Handle<Exception>().RetryAsync(3, (exception, attempt) =>
            {
                Insights.Report(exception, new Dictionary<string, string>
                {
                    { "Number Of Attempts", attempt.ToString()},
                    { "UPC COde", upccode}
                });
            });
            try
            {
                return await policy.ExecuteAsync(async () =>
                    {
                        using (Insights.TrackTime("ProductInfo", "GetProductByUpcCode", upccode))
                        {
                            return await _apiUpcDatabase.GetInfoByUpc(upccode);
                        }
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
    }
}
