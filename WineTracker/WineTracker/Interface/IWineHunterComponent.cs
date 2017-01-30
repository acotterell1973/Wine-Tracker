using System.Collections.Generic;
using System.Threading.Tasks;
using WineTracker.Models;

namespace WineTracker.Interface
{
    public interface IWineHunterComponent
    {
        Task<List<string>> GetCategoryCounts();
        Task<Bottle> GetLastBottle();
        Task<int> GetWineCount();
    }
}