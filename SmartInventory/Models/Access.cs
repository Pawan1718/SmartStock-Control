using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartInventory.Models
{
    public class Access
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }

       
        [Required]
        [EmailAddress]
        public string EmailId { get; set; }

       
        [Required]
        [RegularExpression(@"^[0-11]+$", ErrorMessage = "Mobile must contain only digits.")]
        public int? Mobile { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }


    
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
