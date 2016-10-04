using System.Collections.Generic;
using System.Threading.Tasks;
using WineTracker.Models;

namespace WineTracker.RepositoryServices.Components
{
    public interface IWineHunterComponent
    {
        Task<List<string>> GetCategoryCounts();
        Task<Bottle> GetLastBottle();
        Task<int> GetWineCount();
    }
}