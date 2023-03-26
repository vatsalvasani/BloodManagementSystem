using BloodManagement.Models;

namespace BloodManagement.Data
{
    public interface IAuthRepo
    {
        Task<long> Register(User1 user, string password);
        Task<string> Login(string username,string password);
        Task<bool> UserExists(string username);

    }
}
