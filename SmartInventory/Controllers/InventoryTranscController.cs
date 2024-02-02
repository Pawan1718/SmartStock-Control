using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SmartInventory.Data;
using SmartInventory.Models;
using SmartInventory.ViewModels;

namespace SmartInventory.Controllers
{
    public class InventoryTranscController : Controller
    {
        private readonly imsDbContext _context;
        public InventoryTranscController(imsDbContext context)
        {
            _context = context;
        }
        public IActionResult Blank()
        {
            return View();
        }
      
        public IActionResult ManageTransactions()
        {
            var inventoryData = _context.Inventories.Include("Product").ToList();
            return View(inventoryData);
        }
      
        public IActionResult AddTransactions()
        {
            LoadProducts();
            return View();
        }
      
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddTransactions(InventoryItem model)
        {
            if (model.isRejected && string.IsNullOrEmpty(model.ReasonForRejection))
            {
                TempData["error"] = "Reason for rejection is required.";
            }
            else
            {
                ModelState.Remove("Product");
                if (ModelState.IsValid)
                {
                    var SoldStock = _context.Inventories.Where(x => x.TransactionType == "Stock Out" && x.ProductId == model.ProductId).Sum(x => x.Quantity);
                    var PurchaseStock = _context.Inventories.Where(x => x.TransactionType == "Stock In" && x.ProductId == model.ProductId).Sum(x => x.Quantity);
                    var stockLevel = PurchaseStock - SoldStock;

                    if (model.TransactionType == "Stock In")
                    {
                        _context.Add(model);
                        _context.SaveChanges();
                        TempData["success"] = "Stock-In successful. Inventory updated.";
                        return RedirectToAction("ManageTransactions");
                    }
                    else
                    {
                        if (stockLevel < 50)
                        {
                            TempData["warning"] = "Low inventory! Consider restocking soon.";
                        }
                        if (stockLevel <= 10)
                        {
                            TempData["error"] = "Insufficient stock for this product. Please review the quantity.";
                        }
                        else
                        {
                            _context.Add(model);
                            _context.SaveChanges();
                            TempData["success"] = "Stock-Out successful. Inventory updated.";
                            return RedirectToAction("ManageTransactions");
                             
                        }
                    }
                }
            }
            LoadProducts();
            return View(model);
        }

        

        [NonAction]
        private void LoadProducts()
            {
              var product = _context.Products.ToList();
              ViewBag.Product = new SelectList(product, "Id", "Name");
            }


        public IActionResult UpdateTransactions(int? id)
        {
            if (id == null || _context.Inventories == null)
            {
                return NotFound();
            }

            var inventoryItem =  _context.Inventories.Find(id);
            if (inventoryItem == null)
            {
                return NotFound();
            }
            LoadProducts();
            return View(inventoryItem);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateTransactions(int id, InventoryItem model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (model.isRejected && string.IsNullOrEmpty(model.ReasonForRejection))
            {
                TempData["error"] = "Reason for rejection is required.";
            }
            else
            {
                ModelState.Remove("Product");
                if (ModelState.IsValid)
                {
                    if (model.TransactionType == "Stock In")
                    {
                        _context.Update(model);
                        _context.SaveChanges();
                        TempData["success"] = "Stock-In details updated successfully.";
                        return RedirectToAction("ManageTransactions");
                    }
                    else
                    {
                        _context.Update(model);
                        _context.SaveChanges();
                        TempData["success"] = "Stock-Out details updated successfully.";
                        return RedirectToAction("ManageTransactions");
                    }
                }
            }
            LoadProducts();
            return View(model);
        }



        public IActionResult RemoveTransactions(int? id)
        {
           var transaction=_context.Inventories.Find(id);
            return View(transaction);
        }




        [HttpPost, ActionName("RemoveTransactions")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_context.Inventories == null)
            {
                return Problem("Entity set 'imsDbContext.Inventories'  is null.");
            }
            var inventoryItem = _context.Inventories.Find(id);
            if (inventoryItem != null)
            {
                _context.Inventories.Remove(inventoryItem);
            }
            TempData["success"] = "Inventory transaction deleted successfully.";
            _context.SaveChanges();
            return RedirectToAction("ManageTransactions");
        }
    
            
            
            
            private bool InventoryItemExists(int id)
             {
            return (_context.Inventories?.Any(e => e.Id == id)).GetValueOrDefault();
              }

    }
}
