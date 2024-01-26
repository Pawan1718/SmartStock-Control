using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartInventory.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required,DisplayName("Product Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
