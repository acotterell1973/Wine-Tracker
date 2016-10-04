using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WineTracker.Interface;
using WineTracker.Models;
using Xamarin.Forms;

namespace WineTracker.RepositoryServices.Components
{
    public class WineHunterComponent : IWineHunterComponent
    {
        private readonly Repository<Bottle> _bottleRepo;
        private Repository<BottleImage> _bottleImageRepo;
        private Repository<BottleOccasions> _bottleOccasionsRepo;
        private Repository<BottleLocation> _bottleLocationRepo;

        public WineHunterComponent()
        {
            var connection = DependencyService.Get<IDatabaseConnection>().DbAsyncConnection();

            _bottleRepo = new Repository<Bottle>(connection);
            _bottleImageRepo = new Repository<BottleImage>(connection);
            _bottleOccasionsRepo = new Repository<BottleOccasions>(connection);
            _bottleLocationRepo = new Repository<BottleLocation>(connection);
        }

        public async Task<List<string>>  GetCategoryCounts()
        {
            await Task.Delay(100);
            return null;
        }

        public async Task<bool> SaveWineInfo(BottleInfo bottleInfo)
        {
            await Task.Delay(100);
            return false;
        } 

        public async Task<int> GetWineCount()
        {
            var counts = await _bottleRepo.AsQueryable().CountAsync();
            return counts;
        }

        public async Task<Bottle> GetLastBottle()
        {
            var bottle = await _bottleRepo.Get(orderBy: x => x.CreatedDate);
            return bottle.First();
        }
    }
}
