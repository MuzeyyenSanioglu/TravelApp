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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TravelAppContext dbContext) : base(dbContext)
        {
        }

        public Result CheckUserByExist(string username)
        {
            Result<User> result = new Result<User>();
            try
            {
                bool IsUserExists = _dbContext.Users.Any(s => s.UserName == username);
                result.AlreadyExist = IsUserExists;
                result.SetSuccess();
            }
            catch (Exception ex)
            {
                result.SetFailure(ex.Message);
            }
            return result;
        }

        public Result<User> GetUserByUsername(string username)
        {
            Result < User > result = new Result<User> ();
            try
            {
                User user = _dbContext.Users.FirstOrDefault(s => s.UserName ==username);
                if (user == null)
                    result.SetFailure("user not exists");
                else
                    result.SetData(user);
            }
            catch (Exception ex)
            {
                result.SetFailure(ex.Message);
            }
            return result;
        }
    }
}
