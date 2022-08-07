using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Domain.Entities
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
    }
}
