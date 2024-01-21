using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrderMenage.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace OrderMenage.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly OrderDbContext _context;

        public OrderController(OrderDbContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
              return _context.Order != null ? 
                          View(await _context.Order.ToListAsync()) :
                          Problem("Entity set 'OrderDbContext.Order'  is null.");
        }

        // GET: Order/AddEdit
        public IActionResult AddEdit(int id=0)
        {
            ViewBag.ProductId = new SelectList(_context.Product, "Id", "Name");

            if (id==0)
                return View(new Order());
            else
                return View(_context.Order.Find(id));
        }

        // POST: Order/AddEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit([Bind("Id,ProductId,Size,Amount,CustomerName,CustomerPhone,CustomerMail,Address,OrderTime")] Order order)
        {

            ViewBag.ProductId = new SelectList(_context.Product, "Id", "Name", order.ProductId);

            if (ModelState.IsValid)
            {
                if(order.Id == 0)
                {
                    order.OrderTime = DateTime.Now;
                    _context.Add(order);
                }
                else
                {
                    _context.Update(order);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ProductId = new SelectList(_context.Product, "Id", "ProductName");
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'OrderDbContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Auth
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }
    }
}
