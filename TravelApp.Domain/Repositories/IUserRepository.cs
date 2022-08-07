using TravelApp.Domain.Entities;
using TravelApp.Domain.Model;
using TravelApp.Domain.Repositories.Base;

namespace TravelApp.Domain.Repositories
{
    public interface IUserRepository :IRepository<User>
    {
        Result<User> GetUserByUsername(string username);
        Result CheckUserByExist(string username);
    }
}
