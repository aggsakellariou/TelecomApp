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
    public class CallsController : Controller
    {
        private readonly TelecomDBContext _context;

        public CallsController(TelecomDBContext context)
        {
            _context = context;
        }

        // GET: Calls
        public async Task<IActionResult> Index(string userType, int userId)
        {
            ViewData["UserType"] = userType;
            ViewData["UserId"] = userId;

            if (userType == "Client")
            {
                var client = await _context.Clients
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (client != null)
                {
                    var calls = await _context.Calls
                        .Where(c => c.Bills.Any(b => b.PhoneId == client.PhoneId))
                        .Include(c => c.Bills)
                        .ToListAsync();
                    return View(calls);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return View(await _context.Calls.ToListAsync());
            }
        }

        public async Task<IActionResult> IndexSeller(int clinetId, string userType, int userId)
        {
            ViewData["UserType"] = userType;
            ViewData["UserId"] = userId;

            if (userType == "Seller")
            {
                var client = await _context.Clients
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.UserId == clinetId);

                if (client != null)
                {
                    var calls = await _context.Calls
                        .Where(c => c.Bills.Any(b => b.PhoneId == client.PhoneId))
                        .Include(c => c.Bills)
                        .ToListAsync();
                    return View(calls);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return View(await _context.Calls.ToListAsync());
            }
        }

        // GET: Calls/Details/5
        public async Task<IActionResult> Details(int? id, string userType, int userId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await _context.Calls
                .FirstOrDefaultAsync(m => m.CallId == id);
            if (call == null)
            {
                return NotFound();
            }

            ViewData["UserType"] = userType;
            ViewData["UserId"] = userId;
            return View(call);
        }

        // GET: Calls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Calls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CallId,Description")] Call call)
        {
            if (ModelState.IsValid)
            {
                _context.Add(call);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(call);
        }

        // GET: Calls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await _context.Calls.FindAsync(id);
            if (call == null)
            {
                return NotFound();
            }
            return View(call);
        }

        // POST: Calls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CallId,Description")] Call call)
        {
            if (id != call.CallId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(call);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CallExists(call.CallId))
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
            return View(call);
        }

        // GET: Calls/Delete/5
        public async Task<IActionResult> Delete(int? id, string userId, string userType)
        {
            if (id == null)
            {
                return NotFound();
            }

            var call = await _context.Calls
                .FirstOrDefaultAsync(m => m.CallId == id);
            if (call == null)
            {
                return NotFound();
            }

            ViewData["UserType"] = userType;
            ViewData["UserId"] = userId;
            return View(call);
        }

        // POST: Calls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string userId, string userType)
        {
            var call = await _context.Calls.FindAsync(id);
            if (call != null)
            {
                _context.Calls.Remove(call);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { userType, userId });
        }

        private bool CallExists(int id)
        {
            return _context.Calls.Any(e => e.CallId == id);
        }
    }
}
