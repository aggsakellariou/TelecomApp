using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelecomApp.Models;

namespace TelecomApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly TelecomDBContext _context;

        public LoginController(TelecomDBContext context)
        {
            _context = context;
        }

        // GET: Login
        public IActionResult Index()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Login model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == model.Username);

                if (user != null)
                {
                    string? userType;
                    string redirectAction;
                    string redirectController;

                    if (_context.Admins.Any(a => a.UserId == user.UserId))
                    {
                        userType = user.Property;
                        redirectController = "PhonePrograms";
                        redirectAction = "Index";
                    }
                    else if (_context.Sellers.Any(s => s.UserId == user.UserId))
                    {
                        userType = user.Property;
                        redirectController = "Clients";
                        redirectAction = "Index";
                    }
                    else if (_context.Clients.Any(c => c.UserId == user.UserId))
                    {
                        userType = user.Property;
                        redirectController = "Bills";
                        redirectAction = "Index";
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return View(model);
                    }

                    return RedirectToAction(redirectAction, redirectController, new { userType, userId = user.UserId });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View(model);
        }

        // GET: Logout
        public IActionResult Logout()
        {
            return RedirectToAction("Index", "Login", new { userType = (string?)null });
        }
    }
}