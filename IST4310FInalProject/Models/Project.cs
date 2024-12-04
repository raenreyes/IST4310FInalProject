using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IST4310FInalProject.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string TechnologiesUsed { get; set; }

        // Foreign key to StudentInfo
        [ForeignKey("StudentInfo")]
        public int StudentInfoId { get; set; }

        // Navigation property
        [ValidateNever]
        public virtual StudentInfo StudentInfo { get; set; }
    }
}
