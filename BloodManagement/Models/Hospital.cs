using System.ComponentModel.DataAnnotations;

namespace BloodManagement.Models
{
    public class Hospital
    {
        public long HospitalId { get; set; }
        public string Hospital_Name { get; set; }
        public string Email { get; set; }
        public string contat_no { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

        public bool A { get; set; }
        public bool A_positve { get; set; }
        public bool B { get; set; } 
        public bool B_positve { get; set; }
        public bool C { get; set; }
        public bool C_positve { get;set; }  
        public bool AB { get; set; }     
        public bool AB_positve { get; set; }

    }
}
