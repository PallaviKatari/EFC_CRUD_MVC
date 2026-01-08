using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp_DFA_EFC.Models;

namespace WebApp_DFA_EFC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MyDbContext _context;

        public ProductsController(MyDbContext context)
        {
            _context = context;
        }

        // =======================
        // GET: Products
        // =======================
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products1
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();

            return View(products); //strongly typed view - returns List<Product1> - model in Index.cshtml
        }

        // =======================
        // GET: Products/Details/5
        // =======================
        public async Task<IActionResult> Details(int? id) //1
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products1
                .FirstOrDefaultAsync(m => m.ProductId == id); //1

            if (product == null)
                return NotFound();
            TempData["info"] = "Product info!";
            return View(product);
        }

        // =======================
        // GET: Products/Create
        // =======================
        public IActionResult Create() // This method simply returns the Create view - opens the form
        {
            return View();
        }

        // =======================
        // POST: Products/Create
        // =======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product1 product) // perform the creation
        {
            if (ModelState.IsValid)
            {
                product.CreatedDate = DateTime.Now; // Current date and time

                _context.Add(product);
                await _context.SaveChangesAsync(); // Save to database
                TempData["success"] = "Product created successfully!";
                return RedirectToAction(nameof(Index)); // Redirect to Index action to show the list of products
            }

            return View(product);
        }

        // =======================
        // GET: Products/Edit/5
        // =======================
        public async Task<IActionResult> Edit(int? id) //Opens the form with existing data - Edit Form
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products1.FindAsync(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // =======================
        // POST: Products/Edit/5
        // =======================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product1 product) // Save the edited details
        {
            if (id != product.ProductId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    //product.CreatedDate= DateTime.Now;
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                        return NotFound();
                    else
                        throw;
                }
                TempData["warn"] = "Product updated successfully!";
                return RedirectToAction(nameof(Index)); // Return to Index view 
            }

            return View(product);
        }

        // =======================
        // GET: Products/Delete/5
        // =======================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Products1
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // =======================
        // POST: Products/Delete/5
        // =======================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products1.FindAsync(id);

            if (product != null)
            {
                _context.Products1.Remove(product);
                await _context.SaveChangesAsync();
            }
            TempData["error"] = "Product delete successfully!";
            return RedirectToAction(nameof(Index));
        }

        // =======================
        // Helper Method
        // =======================
        private bool ProductExists(int id)
        {
            return _context.Products1.Any(e => e.ProductId == id);
        }
    }
}
