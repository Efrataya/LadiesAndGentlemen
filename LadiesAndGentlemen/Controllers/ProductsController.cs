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
    public class ProductsController : Controller
    {
        private readonly LadiesAndGentlemenContext _context;

        public ProductsController(LadiesAndGentlemenContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("cart") == null)
            {
                return View(await _context.Product.ToListAsync());
            }
            else
            {
                string productId = HttpContext.Session.GetString("cart");
                string[] ids = productId.Split(',');
                int[] myInts = ids.Select(int.Parse).ToArray();
                var purchased = from p in _context.Product
                                where myInts.Any(s => s == p.Id)
                                select p;
                var leftItems = _context.Product.Except(purchased);
                return View(await leftItems.ToListAsync());
            }

        }

        // GET: cart
        public async Task<IActionResult> Cart(int Id)
        {
            if (HttpContext.Session.GetString("cart") == null)
            {
                string myString = Id.ToString();
                HttpContext.Session.SetString("cart", myString);

                var purchased = from p in _context.Product
                                where Id == p.Id
                                select p;
               
           
                return View(await purchased.ToListAsync());
            }
            else
            {
                string productId = HttpContext.Session.GetString("cart");
                productId += ",";
                productId += Id;
                HttpContext.Session.SetString("cart", productId);
                string[] ids = productId.Split(',');
                int[] myInts = ids.Select(int.Parse).ToArray();
                var c = from p in _context.Product
                        where myInts.Contains(p.Id)
                        select p;
                
                return View(await c.ToListAsync());
            }

        }

        public async Task<IActionResult> Search()
        {

            return View(await _context.Product.ToListAsync());
        }

        [HttpPost]

        public async Task<IActionResult> Search(float price)
        {
            var p = from product in _context.Product
                    where product.price <= price
                    select product;
            return View(await p.ToListAsync());
        }
        public async Task<IActionResult> SearchName(string Description)
        {
            var p = from product in _context.Product
                    where product.Description.Contains(Description)
                    select product;
            return View(await p.ToListAsync());
        }
        public async Task<IActionResult> SearchWomen()
        {
            var p = from product in _context.Product
                    where product.Category.Name == "Women"
                    select product;
            return View(await p.ToListAsync());
        }
        public async Task<IActionResult> SearchMen()
        {
            var p = from product in _context.Product
                    where product.Category.Name == "Men"
                    select product;
            return View(await p.ToListAsync());
        }
        public async Task<IActionResult> SearchChild()
        {
            var p = from product in _context.Product
                    where product.Category.Name == "Children"
                    select product;
            return View(await p.ToListAsync());
        }
        public async Task<IActionResult> SearchWomenShirts()
        {
            var p = from product in _context.Product
                    where product.Category.Name == "Shirts"
                    select product;
            return View(await p.ToListAsync());
        }
        public async Task<IActionResult> SearchWomenShkirs()
        {
            var p = from product in _context.Product
                    where product.Category.Name == "Skirts" 
                    select product;
            return View(await p.ToListAsync());
        }
        public async Task<IActionResult> SearchWomenDresses()
        {
            var p = from product in _context.Product
                    where product.Category.Name == "Dresses"
                    select product;
            return View(await p.ToListAsync());
        }
        public async Task<IActionResult> SearchWomenShoes()
        {
            var p = from product in _context.Product
                    where product.Category.Name == "Shoes"
                    select product;
            return View(await p.ToListAsync());
        }





        public async Task<IActionResult> SearchMenSuits()
        {
            var p = from product in _context.Product
                    where product.Category.Name == "Suits" 
                    select product;
            return View(await p.ToListAsync());
        }
        public async Task<IActionResult> SearchMenShirts()
        {
            var p = from product in _context.Product
                    where product.Category.Name == "Shirts" 
                    select product;
            return View(await p.ToListAsync());
        }
        public async Task<IActionResult> SearchMenAccessories()
        {
            var p = from product in _context.Product
                    where product.Category.Name == "Accessories" 
                    select product;
            return View(await p.ToListAsync());
        }
        public async Task<IActionResult> SearchMenShoes()
        {
            var p = from product in _context.Product
                    where product.Category.Name == "Shoes" 
                    select product;
            return View(await p.ToListAsync());
        }









        public async Task<IActionResult> SearchChildrenSuits()
        {
            var p = from product in _context.Product
                    where product.Category.Name == "Shoes" 
                    select product;
            return View(await p.ToListAsync());
        }
        public async Task<IActionResult> SearchChildrenShirts()
        {
            var p = from product in _context.Product
                    where product.Category.Name == "Suits" 
                    select product;
            return View(await p.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,Description,price")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,Description,price")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
