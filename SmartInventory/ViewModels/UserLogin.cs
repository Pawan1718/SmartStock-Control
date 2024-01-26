using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SmartInventory.ViewModels
{
    public class UserLogin
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public string userName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }


    }
}
