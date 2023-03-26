using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodManagement.Models
{
    public class Review1
    {
        public long Review1Id { get; set; }
        [ForeignKey("User")]
        public long User1Id { get; set; }
        public virtual User1 User1 { get; set; }

        [StringLength(100, ErrorMessage = "Review name cannot be longer than 100 characters.")]
        public string Description { get; set; }
    }
}
