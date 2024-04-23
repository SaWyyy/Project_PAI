using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt_PAI.Model;
using System.Security.Claims;

namespace Projekt_PAI.Pages
{
    [Authorize]
    public class CartModel : PageModel
    {
        private readonly AuthDbContext _context;
        public CartModel(AuthDbContext context) 
        {
            _context = context;
        }

        public List<OrdersModel> Orders { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId != null)
            {
                Orders = await _context.Orders.Where(order => order.UserId == userId).ToListAsync();
                ViewData["CartItemCount"] = Orders.Count;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteFromOrderAsync()
        {
            try
            {
                int orderId = int.Parse(Request.Form["orderId"]);

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var order = await _context.Orders.Where(o => o.Id == orderId && o.UserId == userId).FirstOrDefaultAsync();

                if(order != null)
                {
                    _context.Orders.Remove(order);
                    await _context.SaveChangesAsync();
                }

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToPage();
            }
        }
    }
}
