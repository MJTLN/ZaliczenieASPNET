using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderMenage.Models;

namespace OrderMenage.Controllers
{
    public class ProductController : Controller
    {
        private readonly OrderDbContext _context;

        public ProductController(OrderDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
              return _context.Product != null ? 
                          View(await _context.Product.ToListAsync()) :
                          Problem("Entity set 'OrderDbContext.Product'  is null.");
        }


        // GET: Product/AddEdit
        public IActionResult AddEdit(int id = 0)
        {
            if (id == 0)
                return View(new Product());
            else
                return View(_context.Product.Find(id));
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit([Bind("Name,Id,Type,Price")] Product product)
        {

            if (ModelState.IsValid)
            {
                if (product.Id == 0)
                {
                    _context.Add(product);
                }
                else
                {
                    _context.Update(product);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'OrderDbContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
