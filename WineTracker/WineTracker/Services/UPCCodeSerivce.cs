using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Akavache;
using WineTracker.Models;
using WineTracker.Services.Components;

namespace WineTracker.Services
{
    public class UpcCodeSerivce : IUpcCodeService
    {
        private readonly UpcCodeComponent _upcCodeComponent;
        public UpcCodeSerivce(UpcCodeComponent upcCodeComponent1)
        {
            _upcCodeComponent = upcCodeComponent1;
        }

        public async Task<WineItemInfo> GetProductByUpcCode(CancellationToken cancellationToken, string code)
        {
            if (_upcCodeComponent == null) return null;

            string key = $"GetProductByUpcCode::{code}";

            var productInfo = await BlobCache.LocalMachine.GetAndFetchLatest(
                key,
                async () =>
                {
                    var upcInfoTask =  _upcCodeComponent.GetProductByUpcCode(cancellationToken, code);
                //    var positionTask =  _upcCodeComponent.GetCurentLocation(cancellationToken);

                    await  Task.WhenAll(upcInfoTask);
                    var upcInfo = upcInfoTask.Result;
                  //  var position = positionTask.Result;

                 //   if (position == null) return upcInfo;

                  //  //Get Address Info from GeCode
                  //var addresses = await  _upcCodeComponent.GetAddressesByGeoCode(cancellationToken, position.Latitude.ToString(), position.Longitude.ToString());

                  //  if (upcInfo != null)
                  //  {

                  //      if (addresses != null)
                  //      {
                  //          upcInfo.Address = addresses.results.First().formatted_address;
                  //      }
                  //      upcInfo.ScannedText =
                  //      $"Time: {position.Timestamp} \nLat: {position.Latitude} \nLong: {position.Longitude} \n Altitude: {position.Altitude} \nAltitude Accuracy: {position.AltitudeAccuracy} \nAccuracy: {position.Accuracy} \n Heading: {position.Heading} \n Speed: {position.Speed}";

                  //  }

                    return upcInfo;

                }, null, null);

            return productInfo;
        }
    }
}
