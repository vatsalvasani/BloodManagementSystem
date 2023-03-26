namespace BloodManagement.Models
{
    public class UserRegisterDTO
    {
        public string User_Name { get; set; } = string.Empty;
        public string Contact_no { get; set; } = string.Empty; 
        public string Address { get; set; } = string.Empty; 
        public string Blood_type { get; set; } = string.Empty; 
        public string Email { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
