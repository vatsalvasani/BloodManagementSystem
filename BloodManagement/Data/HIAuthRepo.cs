using BloodManagement.Models;

namespace BloodManagement.Data
{
    public interface HIAuthRepo
    {
        Task<long> Register(Hospital user, string password);
        Task<string> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
