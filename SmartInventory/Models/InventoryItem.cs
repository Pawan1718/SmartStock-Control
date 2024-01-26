
using System.ComponentModel.DataAnnotations;

namespace SmartInventory.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }


        public int ProductId { get; set; }


        public Product Product { get; set; }


        [Required]
        public  int Quantity { get; set; }

        public decimal Price { get; set; }

        public DateTime TransactionDate { get; set; }


        public string TransactionType { get; set; }


        public bool isRejected { get; set; }

        public string? ReasonForRejection {  get; set; }

        public string StockStatus
        {
            get
            {
                return Quantity <= 10 ? "Low Stock" : "Normal Stock";
            }
        }
    }
}
