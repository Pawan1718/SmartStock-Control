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
    public class CategoriesController : Controller
    {
        private readonly imsDbContext _context;

        public CategoriesController(imsDbContext context)
        {
            _context = context;
        }

      


        public async Task<IActionResult> ManageCategories()
        {
              return _context.Categories != null ? 
                          View(await _context.Categories.ToListAsync()) :
                          Problem("Entity set 'imsDbContext.Categories'  is null.");
        }



        public IActionResult CategoryList()
        {
            return _context.Categories != null ?
                        View( _context.Categories.ToList()) :
                        Problem("Entity set 'imsDbContext.Categories'  is null.");
        }




        public async Task<IActionResult> CategoryDetails(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        public IActionResult AddCategory()
        {
            return View();
        }


      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddCategory( Category model)
        {
            ModelState.Remove("Products");
            if (ModelState.IsValid)
            {
              bool isCategoryAvailable=_context.Categories.Any(x=>x.Name == model.Name);
                if (isCategoryAvailable)
                {
                    TempData["warning"] = "A category with this name already exists. Please choose a different name.";
                }
                else
                {
                    _context.Add(model);
                    _context.SaveChanges();
                    TempData["success"] = "Product category created successfuly!";
                    return RedirectToAction("CategoryList");
                }
            
            }
            return View(model);
        }



        public IActionResult UpdateCategory(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCategory(int id, Category model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Products");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["success"] = "Product category updated successfuly!";

                return RedirectToAction("ManageCategories");
            }
            return View(model);
        }



        public IActionResult RemoveCategory(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category =  _context.Categories
                .FirstOrDefault(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }



        [HttpPost, ActionName("RemoveCategory")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'imsDbContext.Categories'  is null.");
            }
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                bool isProductAvailable = _context.Products.Any(p => p.CategoryId ==id);
                if (isProductAvailable)
                {
                    TempData["error"] ="Cannot delete this category as it is associated with one or more products." +
                                         "Please remove the associated products first.";
                    return View(category);
                }

                else
                {
                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                    TempData["success"] = "Product category Removed From Category List!";
                    return RedirectToAction("ManageCategories");
                }
            }
            return View();

        }



        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
