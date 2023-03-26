namespace BloodManagement.Models
{
    public class HospitalRegisterDTO
    {
            public string Hospital_Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string contat_no { get; set; } = string.Empty;
            public string address { get; set; } = string.Empty;
            public string city { get; set; } = string.Empty;
            public string state { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;

            public bool A { get; set; } = false;
            public bool A_positve { get; set; } = false;
            public bool B { get; set; } = false;
            public bool B_positve { get; set; } = false;
           public bool C { get; set; } = false;
           public bool C_positve { get; set; } = false;
           public bool AB { get; set; } = false;
           public bool AB_positve { get; set; } = false;

    }
}
