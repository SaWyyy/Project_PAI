using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt_PAI.Model;
using System.Reflection;
using System.Security.Claims;

namespace Projekt_PAI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly AuthDbContext _context;

        public IndexModel(AuthDbContext context)
        {
            _context = context;
        }

        public List<BooksModel> Books { get; set; }
        public List<OrdersModel> Orders { get; set; }

        [BindProperty]
        public string searchBar { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            Books = string.IsNullOrEmpty(searchBar)
                ? await _context.Books.ToListAsync()
                : Books = await _context.Books.Where(book => EF.Functions.Like(book.Title, searchBar + "%")).ToListAsync();

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId != null)
            {
                Orders = await _context.Orders.Where(order => order.UserId == userId).ToListAsync();
                ViewData["CartItemCount"] = Orders.Count;
            }
        }

        public async Task<IActionResult> OnPostAddToOrderAsync()
        {
            try
            {
                int bookId = int.Parse(Request.Form["bookId"]);

                var book = await _context.Books.FindAsync(bookId);

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var order = new OrdersModel
                {
                    Author = book.Author,
                    Title = book.Title,
                    Genre = book.Genre,
                    UserId = userId
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToPage();
            }
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            if (!string.IsNullOrEmpty(searchBar))
            {
                Books = await _context.Books.Where(book => EF.Functions.Like(book.Title, searchBar + "%")).ToListAsync();
            }
            else
            {
                Books = await _context.Books.ToListAsync();
            }
            return Page();
        }

    }
}
