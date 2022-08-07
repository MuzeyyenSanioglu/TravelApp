using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Domain.Entities;
using TravelApp.Domain.Model;
using TravelApp.Domain.Repositories;
using TravelApp.Infrastructure.Data;
using TravelApp.Infrastructure.Repositories.Base;

namespace TravelApp.Infrastructure.Repositories
{
    public class TravelRepository : Repository<Travel>, ITravelRepository
    {
        public TravelRepository(TravelAppContext dbContext) : base(dbContext)
        {
        }

        public Result<List<Travel>> GetTravelsFiltersByRoadFilter(string startCity, string endCity)
        {
            Result<List<Travel>> result = new Result<List<Travel>>();
            try
            {
              List<Travel> travels =  _dbContext.Travels.Where(s => s.StartingCity == startCity && s.EndCity == endCity && s.IsPublication).ToList();
              result.SetData(travels);
            }
            catch (Exception ex)
            {
                result.SetFailure(ex);
            }
            return result;
        }

       
    }
}
