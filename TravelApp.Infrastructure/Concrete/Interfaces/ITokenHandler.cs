using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Domain.Entities;

namespace TravelApp.Infrastructure.Concrete.Interfaces
{
    public interface ITokenHandler
    {
        AccessToken CreateToken(TokenRequestModel user, List<OperationClaim> operationClaims = null);
    }
}
