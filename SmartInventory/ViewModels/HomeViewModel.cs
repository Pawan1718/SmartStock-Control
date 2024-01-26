using SmartInventory.Models;
using System.Collections.Generic;

namespace SmartInventory.ViewModels
{
    public class HomeViewModel
    {
        public string TransactionType { get; set; }

        public List<Product> FeaturedProducts { get; set; }
        public List<InventoryItem> LatestTransactions { get; set; }
        public int StockAvailable { get; internal set; }
        public int RejectedStock { get; internal set; }
    }
}
