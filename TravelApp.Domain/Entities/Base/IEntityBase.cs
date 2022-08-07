using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Domain.Entities.Base
{
    public interface IEntityBase
    {
        int Id { get; }
        DateTime CreatedDate  { get; set; }
    }
}
