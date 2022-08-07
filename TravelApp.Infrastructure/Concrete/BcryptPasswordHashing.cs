using TravelApp.Infrastructure.Concrete.Interfaces;

namespace TravelApp.Infrastructure.Concrete
{
    public class BcryptPasswordHashing : IPasswordHashing
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
