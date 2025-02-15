using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TelecomApp.Models;

namespace TelecomApp.Controllers
{
    public class ClientsController : Controller
    {
        private readonly TelecomDBContext _context;

        public ClientsController(TelecomDBContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index(string userType, string userId)
        {
            ViewData["UserType"] = userType;
            ViewData["UserId"] = userId;

            var clients = await _context.Clients
                .Include(c => c.Phone)
                .ThenInclude(p => p.Program)
                .Include(c => c.User)
                .ToListAsync();

            var userClients = clients.Select(c => new UserClient
            {
                User = c.User,
                Client = c,
                Phone = c.Phone
            }).ToList();

            return View(userClients);
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id, string userType, string userId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Phone)
                .ThenInclude(p => p.Program)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            var userClient = new UserClient
            {
                User = client.User,
                Client = client,
                Phone = client.Phone
            };

            ViewData["UserType"] = userType;
            ViewData["UserId"] = userId;
            return View(userClient);
        }

        // GET: Clients/Create
        public IActionResult Create(string userType, string userId)
        {
            ViewData["UserType"] = userType;
            ViewData["UserId"] = userId;
            ViewData["ProgramId"] = new SelectList(_context.PhonePrograms, "ProgramId", "ProgramName");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserClient userClient, string userType, string userId)
        {
            if (ModelState.IsValid)
            {
                // Add User
                _context.Users.Add(userClient.User);
                await _context.SaveChangesAsync();

                // Add Phone
                _context.Phones.Add(userClient.Phone);
                await _context.SaveChangesAsync();

                // Get the inserted PhoneId
                userClient.Client.PhoneId = userClient.Phone.PhoneId;

                // Get the inserted UserId
                userClient.Client.UserId = userClient.User.UserId;

                // Add Client
                _context.Clients.Add(userClient.Client);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { userType, userId });
            }

            ViewData["UserType"] = userType;
            ViewData["UserId"] = userId;
            ViewData["ProgramId"] = new SelectList(_context.PhonePrograms, "ProgramId", "ProgramName", userClient.Phone.ProgramId);
            return View(userClient);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id, string userType, string userId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.User)
                .Include(c => c.Phone)
                .ThenInclude(p => p.Program)
                .FirstOrDefaultAsync(c => c.ClientId == id);

            if (client == null)
            {
                return NotFound();
            }

            var userClient = new UserClient
            {
                User = client.User,
                Client = client,
                Phone = client.Phone
            };

            ViewData["UserType"] = userType;
            ViewData["UserId"] = userId;
            ViewData["ProgramId"] = new SelectList(_context.PhonePrograms, "ProgramId", "ProgramName", client.Phone.ProgramId);
            return View(userClient);
        }

        // POST: Clients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserClient userClient, string userType, string userId)
        {
            if (id != userClient.Client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update only the Phone's ProgramId
                    var phone = await _context.Phones.FindAsync(userClient.Phone.PhoneId);
                    if (phone != null)
                    {
                        phone.ProgramId = userClient.Phone.ProgramId;
                        _context.Update(phone);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(userClient.Client.ClientId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { userType, userId });
            }

            ViewData["UserType"] = userType;
            ViewData["UserId"] = userId;
            ViewData["ProgramId"] = new SelectList(_context.PhonePrograms, "ProgramId", "ProgramName", userClient.Phone.ProgramId);
            return View(userClient);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .Include(c => c.Phone)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }
    }
}
