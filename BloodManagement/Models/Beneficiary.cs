using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BloodManagement.Models
{
    public class Beneficiary
    {
        public long BeneficiaryId { get; set; }

        [ForeignKey("User")]
        public long User1Id { get; set; }
        public virtual User1 User1 { get; set; }

        [ForeignKey("Hospital")]
        public long HospitalId { get; set; }

        public virtual Hospital Hospital { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

    }
}
