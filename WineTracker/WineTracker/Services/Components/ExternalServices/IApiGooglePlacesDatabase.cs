using System.Threading.Tasks;
using WineTracker.Models;

namespace WineTracker.Services.Components.ExternalServices
{
    public interface IApiGooglePlacesDatabase
    {
        Task<GeoCode> GetAddressByGeoCodeTask(string lat, string longt);
    }
}