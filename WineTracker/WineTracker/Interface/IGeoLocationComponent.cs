using System.Threading;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;
using WineTracker.Models;

namespace WineTracker.Interface
{
    public interface IGeoLocationComponent
    {
        Task<GeoCode> GetAddressesByGeoCode(CancellationToken cancellationToken, string lat, string longt);
        Task<Position> GetCurentLocation(CancellationToken cancellationToken);
        Task<GeoPlaces> GetNearByPlacesTask(CancellationToken cancellationToken, string lat, string longt);
    }
}