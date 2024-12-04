using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IST4310FInalProject.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string SchoolName { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public string Major { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [ForeignKey("StudentInfo")]
        public int StudentInfoId { get; set; }

        // Navigation property
        [ValidateNever]
        public virtual StudentInfo StudentInfo { get; set; }
    }
}
