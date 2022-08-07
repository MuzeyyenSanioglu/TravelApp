using TravelApp.Domain.Entities;
using TravelApp.Domain.Model;
using TravelApp.Domain.Repositories.Base;

namespace TravelApp.Domain.Repositories
{
    public interface ITravelRepository : IRepository<Travel>
    {
       
        Result<List<Travel>> GetTravelsFiltersByRoadFilter(string startCity, string endCity);

    }
}
