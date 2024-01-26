using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SmartInventory.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required,DisplayName("Category Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Product>Products { get; set; }

    }
}
