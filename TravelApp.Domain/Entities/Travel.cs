using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Domain.Entities
{
    public class Travel
    {
        public string StartingCity { get; set; } 
        public string EndCity { get; set; }
        public string Description { get; set; }
        public int NumberOfSeat { get; set; }
        public int NumberOfAvailebleSeat { get; set; }
        public DateTime TravelDate { get; set; }
        public bool  IsActive { get; set; }

    }
}
