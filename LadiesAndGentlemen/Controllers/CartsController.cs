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
using System.Collections.ObjectModel;

namespace LadiesAndGentlemen.Controllers
{
    public class CartsController : Controller
    {
        private readonly LadiesAndGentlemenContext _context;

        public CartsController(LadiesAndGentlemenContext context)
        {
            _context = context;
        }


        // GET: Carts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cart.ToListAsync());
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts/Create
        public IActionResult Create( )
        {
            
                    return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCart()
        {
            if (HttpContext.Session.GetString("cart") == null)
            {
                ViewData["CartError"] = "Your cart is empty!";

                //await _context.SaveChangesAsync();
                //string myString = Id.ToString();
                //HttpContext.Session.SetString("cart", myString);

                //var purchased = from p in _context.Product
                //                where Id == p.Id
                //                select p;
                //foreach (var product in purchased)
                //{
                //    product.CartId = cart.Id;
                //    product.Cart = cart;
                //    cart.Products.Add(product);
                //}
                //string cartUnit = cart.Id.ToString();
                //HttpContext.Session.SetString("cartUnit", cartUnit);
                //return View(cart);
                return View();
            }
            else
            {
                Cart cart;
                cart = new Cart();
                cart.Products = new List<Product>();
                _context.Add(cart);
                await _context.SaveChangesAsync();
               string myProduct = HttpContext.Session.GetString("cart");
                string[] ids = myProduct.Split(',');
                int[] myInts = ids.Select(int.Parse).ToArray();
                var c = from p in _context.Product
                        where myInts.Contains(p.Id)
                        select p;
                foreach (var product in c)
                {
                    product.CartId = cart.Id;
                    product.Cart = cart;
                    cart.Products.Add(product);
                }
                string cartUnit = cart.Id.ToString();
                HttpContext.Session.SetString("cartUnit", cartUnit);
                
            }
            
            return View();
        }


        


   





            // GET: Carts/Edit/5
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
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
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }
    }
}
