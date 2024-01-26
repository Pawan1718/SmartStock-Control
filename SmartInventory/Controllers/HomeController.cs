using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SmartInventory.Data;
using SmartInventory.Models;
using SmartInventory.ViewModels;

public class HomeController : Controller
{
    private readonly imsDbContext _context;

    public HomeController(imsDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var TotalStockIn = _context.Inventories
           .Where(x => x.TransactionType == "Stock In").Sum(x => x.Quantity);
  
        var TotalStockOut = _context.Inventories
            .Where(x => x.TransactionType == "Stock Out").Sum(x => x.Quantity);

        var TotalRejectedStock =_context.Inventories
            .Where(x=>x.isRejected).Sum(x => x.Quantity);

        var AvailableStock = TotalStockIn - TotalStockOut;



        var featuredProducts = _context.Products.Include("Category").Take(3).ToList();
        var latestTransactions = _context.Inventories
            .Include(i => i.Product)
            .OrderByDescending(i => i.TransactionDate)
            .Take(5)
            .ToList();

        var viewModel = new HomeViewModel
        {
            FeaturedProducts = featuredProducts,
            LatestTransactions = latestTransactions,
            StockAvailable =AvailableStock,
            RejectedStock = TotalRejectedStock
        };

        return View(viewModel);
    }

}
