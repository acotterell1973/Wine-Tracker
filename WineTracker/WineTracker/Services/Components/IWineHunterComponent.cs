using System.Collections.Generic;
using System.Threading.Tasks;
using WineTracker.Models;

namespace WineTracker.Services.Components
{
    public interface IWineHunterComponent
    {
        Task<List<string>> GetCategoryCounts();
        Task<Bottle> GetLastBottle();
        Task<int> GetWineCount();
    }
}