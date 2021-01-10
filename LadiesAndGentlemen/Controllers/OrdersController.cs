using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LadiesAndGentlemen.Data;
using LadiesAndGentlemen.Models;
using Microsoft.AspNetCore.Http;

namespace LadiesAndGentlemen.Controllers
{
    public class OrdersController : Controller
    {
        private readonly LadiesAndGentlemenContext _context;

        public OrdersController(LadiesAndGentlemenContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var ladiesAndGentlemenContext = _context.Order.Include(o => o.Cart);
            return View(await ladiesAndGentlemenContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Cart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

       

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {
            Order order = new Order();
            _context.Add(order);
            await _context.SaveChangesAsync();
            //order's cart:
            order.Cart = new Cart(); 
            string cartId = HttpContext.Session.GetString("cartUnit");
            int x = Int32.Parse(cartId);
            //order.CartId = x;
            var myCart = from chosenCart in _context.Cart
                         where chosenCart.Id == x
                         select chosenCart;
            
            order.Cart = (Cart)myCart;
            //order's client:
            order.Client = new Client();
            string clientId = HttpContext.Session.GetString("clientId");
            int y = Int32.Parse(clientId);
            var myClient = from chosenClient in _context.Client
                         where chosenClient.Id ==y
                         select chosenClient;
            order.Client= (Client)myClient;
            //order's sum:
            string sum = HttpContext.Session.GetString("price");
            float z = Int32.Parse(sum);
            order.Sum = z;
            await _context.SaveChangesAsync();
            //if (ModelState.IsValid)
            //{
            //    _context.Add(order);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["CartId"] = new SelectList(_context.Cart, "Id", "Id", order.CartId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "Id", "Id", order.CartId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Sum,CartId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CartId"] = new SelectList(_context.Cart, "Id", "Id", order.CartId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Cart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
