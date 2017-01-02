using System.Threading;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;
using WineTracker.Models;

namespace WineTracker.Services.Components
{
    public interface IUpcCodeComponent
    {
        Task<Position> GetCurentLocation(CancellationToken cancellationToken);
        Task<WineItemInfo> GetProductByUpcCode(CancellationToken cancellationToken, string upccode);

        Task<GeoCode> GetAddressesByGeoCode(CancellationToken cancellationToken, string lat, string longt);


    }
}