using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IST4310FInalProject.Models
{
    public class StudentInfo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string StudentId { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Major { get; set; }
        [Required]
        public string DegreeType { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        [ValidateNever]
        public virtual IdentityUser ApplicationUser { get; set; }

        // Navigation properties to related models (optional, for easier access)

        // Initialize collections
        public virtual ICollection<Education> Educations { get; set; } = new List<Education>();
        public virtual ICollection<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    }
}
