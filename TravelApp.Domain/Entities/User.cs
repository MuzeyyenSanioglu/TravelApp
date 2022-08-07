using TravelApp.Domain.Entities.Base;

namespace TravelApp.Domain.Entities
{
    public class User :Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
