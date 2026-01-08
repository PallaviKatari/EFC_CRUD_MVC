using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp_DFA_EFC.Models;

namespace WebApp_DFA_EFC.Controllers
{
    public class ProductsPaginationController : Controller
    {
        private readonly MyDbContext _context;

        public ProductsPaginationController(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(
    string? searchString,
    string sortOrder,
    int pageNumber = 1,
    int pageSize = 5)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;

            ViewData["NameSort"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSort"] = sortOrder == "price" ? "price_desc" : "price";
            ViewData["DateSort"] = sortOrder == "date" ? "date_desc" : "date";

            var products = _context.Products1.AsQueryable();

            // 🔍 Search
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }

            // 🔃 Sorting
            products = sortOrder switch
            {
                "name_desc" => products.OrderByDescending(p => p.Name),
                "price" => products.OrderBy(p => p.Price),
                "price_desc" => products.OrderByDescending(p => p.Price),
                "date" => products.OrderBy(p => p.CreatedDate),
                "date_desc" => products.OrderByDescending(p => p.CreatedDate),
                _ => products.OrderBy(p => p.Name)
            };

            // 📄 Pagination
            int totalRecords = await products.CountAsync();
            var items = await products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.PageNumber = pageNumber;
            ViewBag.TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            return View(items);
        }

    }
}
