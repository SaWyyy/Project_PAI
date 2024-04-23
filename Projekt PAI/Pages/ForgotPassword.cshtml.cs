using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt_PAI.Model;
using System.ComponentModel.DataAnnotations;

namespace Projekt_PAI.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly AuthDbContext _context;
        public string message = "";
        public string errorMessage = "";

        [BindProperty]
        [Required(ErrorMessage = "To pole jest wymagane")]
        [EmailAddress(ErrorMessage = "To nie jest adres email")]
        public string Email { get; set; } = "";

        public bool ClearInput { get; set; }

        public ForgotPasswordModel(AuthDbContext context)
        {
            _context = context;
        }
        public async Task OnPost()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == Email);
            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    message = "Link do zmiany has³a zosta³ wys³any na podany adres email";
                    ClearInput = true;
                }
                else
                {
                    errorMessage = "Podany u¿ytkownik nie jest zarejestrowany";
                }
            }
        }

    }
}
