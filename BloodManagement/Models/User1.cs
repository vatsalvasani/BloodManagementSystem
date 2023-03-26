using System.ComponentModel.DataAnnotations;

namespace BloodManagement.Models
{
    public class User1
    {

        public long User1Id { get; set; }
        public string User_Name { get; set; }
        public string Contact_no { get; set; }
        public string Address { get; set; }
        public string Blood_type { get; set; }
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

        public string Email { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public static implicit operator User1(List<User1> v)
        {
            throw new NotImplementedException();
        }
    }
}
