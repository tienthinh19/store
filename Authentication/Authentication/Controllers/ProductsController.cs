using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Authentication.Helper;

namespace Authentication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly AuthenticationContext _context;

        public ProductsController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(int? pageIndex,
            string? description)
        {
            // Start with IQueryable<Product>
            IQueryable<Product> products = _context.Products.Include(p => p.Category);

            // Apply filters
            if (!string.IsNullOrEmpty(description))
            {
                products = products.Where(p => p.Description.Contains(description));
            }

            // Pagination
            const int pageSize = 6;
            var paginatedList = await PaginatedList<Product>.CreateAsync(products.AsNoTracking(), pageIndex ?? 1, pageSize);

            // Save current filter for rendering the view
            ViewBag.CurrentFilter = description;

            return View(paginatedList);
        }



        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Price,Discount,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Price,Discount,CategoryId")] Product product)
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }


        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
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
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        private IQueryable<Product> Filter(IQueryable<Product> products, int? categoryId, string? description, double? fromPrice, double? toPrice, string? op)
        {
            FilterInfo filterInfo = new FilterInfo();
            switch (op)
            {
                case "search-do":
                    filterInfo = new FilterInfo
                    {
                        CategoryId = categoryId,
                        Description = description,
                        FromPrice = fromPrice,
                        ToPrice = toPrice,
                    };
                    break;
                case "search-clear":
                    break;
                default:
                    filterInfo = HttpContext.Session.Get<FilterInfo>("filterInfo") ?? filterInfo;
                    break;
            }
            HttpContext.Session.Set<FilterInfo>("filterInfo", filterInfo);
            ViewBag.CategoryList = new SelectList(_context.Categories, "Id", "Name", filterInfo.CategoryId);
            ViewBag.FilterInfo = filterInfo;
            if (filterInfo.CategoryId != null && filterInfo.CategoryId != -1)
            {
                products = products.Where(p => p.CategoryId == filterInfo.CategoryId);
            }
            if (!string.IsNullOrEmpty(filterInfo.Description))
            {
                products = products.Where(p => p.Description.Contains(filterInfo.Description));
            }
            if (filterInfo.FromPrice != null)
            {
                products = products.Where(p => p.Price >= filterInfo.FromPrice);
            }
            if (filterInfo.ToPrice != null)
            {
                products = products.Where(p => p.Price <= filterInfo.ToPrice);
            }
            return products;
        }

        private IQueryable<Product> Sort(IQueryable<Product> products, string? orderBy, string? orderType, string? op)
        {
            SortInfo sortInfo = new SortInfo { OrderBy = "Id", OrderType = "ASC" };
            switch (op)
            {
                case "sort-do":
                    sortInfo = new SortInfo { OrderBy = orderBy, OrderType = orderType };
                    break;
                case "sort-clear":
                    break;
                default:
                    sortInfo = HttpContext.Session.Get<SortInfo>("sortInfo") ?? sortInfo;
                    break;
            }
            HttpContext.Session.Set<SortInfo>("sortInfo", sortInfo);
            List<string> fieldList = new List<string> { "Id", "Description", "Discount", "Price", "Category" };
            ViewBag.FieldList = new SelectList(fieldList, sortInfo.OrderBy);
            ViewBag.SortInfo = sortInfo;

            if (!string.IsNullOrEmpty(sortInfo.OrderBy) && !string.IsNullOrEmpty(sortInfo.OrderType))
            {
                try
                {
                    switch (sortInfo.OrderBy.ToLower())
                    {
                        case "description":
                            products = sortInfo.OrderType.ToUpper() == "ASC" ? products.OrderBy(p => p.Description) : products.OrderByDescending(p => p.Description);
                            break;
                        case "discount":
                            products = sortInfo.OrderType.ToUpper() == "ASC" ? products.OrderBy(p => p.Discount) : products.OrderByDescending(p => p.Discount);
                            break;
                        case "price":
                            products = sortInfo.OrderType.ToUpper() == "ASC" ? products.OrderBy(p => p.Price) : products.OrderByDescending(p => p.Price);
                            break;
                        case "category":
                            products = sortInfo.OrderType.ToUpper() == "ASC" ? products.OrderBy(p => p.Category.Name) : products.OrderByDescending(p => p.Category.Name);
                            break;
                        default:
                            products = products.OrderBy(p => p.Id);
                            break;
                    }
                }
                catch
                {
                    products = products.OrderBy(p => p.Id);
                }
            }
            else
            {
                products = products.OrderBy(p => p.Id);
            }

            return products;
        }
    }
}
