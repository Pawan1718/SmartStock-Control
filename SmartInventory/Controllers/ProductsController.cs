using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartInventory.Data;
using SmartInventory.Models;

namespace SmartInventory.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly imsDbContext _context;

        public ProductsController(imsDbContext context)
        {
            _context = context;
        }


        public IActionResult Blank()
        {
            return View();
        }

        public IActionResult ManageProducts()
        {
            var product = _context.Products.Include(p => p.Category).ToList();
            return View(product);
        }
        


        public IActionResult ListProducts()
        {
            var product = _context.Products.Include(p => p.Category).ToList();
            return View(product);
        }



        public IActionResult ProductDetails(int? id)
        {
            var product=_context.Products.Find(id);
            LoadCategories();
            return View(product);
        }

    


        public IActionResult AddProducts()
        {
            LoadCategories();
            return View();
        }
    



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProducts( Product model)
        {
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                bool productsAvailable =_context.Products.Any(p => p.Name == model.Name);
                if (productsAvailable)
                {
                    TempData["warning"] ="A product with this name already exists. Please choose a different name.";
                }
                else
                {
                    _context.Add(model);
                    _context.SaveChangesAsync();
                    TempData["success"] = "Product added successfully to the inventory.";
                    return RedirectToAction("ManageProducts");
                }
            }
            LoadCategories();
            return View(model);
        }




        [NonAction]
        private void LoadCategories()
        {
            var categories= _context.Categories.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }



       
        public IActionResult UpdateProducts(int? id)
        {
            var product = _context.Products.Find(id);
            LoadCategories();
            return View(product);
        }
        


        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult  UpdateProducts(int id, Product model)
        {         
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                     _context.Update(model);
                     _context.SaveChanges();
                TempData["success"] = "Product details updated successfully.";
                return RedirectToAction("ManageProducts");
            }
            LoadCategories();
            return View(model);
        }

     


        public IActionResult RemoveProducts(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }




        [HttpPost, ActionName("RemoveProducts")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'imsDbContext.Products'  is null.");
            }
            var product = _context.Products.Find(id);
            if (product != null)
            {
                bool isInventoryAvailable = _context.Inventories.Any(p => p.ProductId ==id);
                if (isInventoryAvailable)
                {
                    TempData["warning"] ="Cannot delete this product as it is associated with one or more transactions. Please remove the associated transactions first.";
              
                }
                else
                {
                    _context.Products.Remove(product);
                    _context.SaveChanges();
                    TempData["success"] = "Product deleted successfully.";
                    return RedirectToAction("ManageProducts");
                }
            }
            return View(product);
        }





        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
