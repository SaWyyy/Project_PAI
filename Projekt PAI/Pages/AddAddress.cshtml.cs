using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt_PAI.Model;
using System.Security.Claims;

namespace Projekt_PAI.Pages
{
    [Authorize]
    public class AddAddressModel : PageModel
    {
        private readonly AuthDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AddAddressModel(AuthDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public AddressModel Model { get; set; }
        public List<OrdersModel> Orders { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            Model = await _context.Address.SingleOrDefaultAsync(a => a.UserId == user.Id) ?? new AddressModel();
            if (user.Id != null)
            {
                Orders = _context.Orders.Where(order => order.UserId == user.Id).ToList();
                ViewData["CartItemCount"] = Orders.Count;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("Nie znaleziono zalogowanego u¿ytkowanika.");
            }

            string id = user.Id;
            Model.UserId = id;

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingAddress = await _context.Address.SingleOrDefaultAsync(a => a.UserId == user.Id);

            if(existingAddress == null)
            {
                _context.Address.Add(Model);
            }
            else
            {
                existingAddress.StreetAndNr = Model.StreetAndNr;
                existingAddress.City = Model.City;
                existingAddress.PostalCode = Model.PostalCode;
                _context.Address.Update(existingAddress);
            }

            var orders = await _context.Orders.Where(o => o.UserId == user.Id).ToListAsync();
            if (orders != null)
            {
                _context.Orders.RemoveRange(orders);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("/ThanksForOrder");
        }
    }
}
