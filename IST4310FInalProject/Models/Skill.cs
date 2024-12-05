using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IST4310FInalProject.Models
{
    public class Skill
    {
        [Key]
        public int Id { get; set; }
        public string Heading { get; set; }
        public string ToolSkills { get; set; }
        [ForeignKey("StudentInfo")]
        public int StudentInfoId { get; set; }

        // Navigation property
        [ValidateNever]
        public virtual StudentInfo StudentInfo { get; set; }
    }
}
