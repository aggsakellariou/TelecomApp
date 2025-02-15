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
    public class BillsController : Controller
    {
        private readonly TelecomDBContext _context;

        public BillsController(TelecomDBContext context)
        {
            _context = context;
        }

        // GET: Bills
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
                    var bills = await _context.Bills
                        .Where(b => b.PhoneId == client.PhoneId)
                        .Include(b => b.Phone)
                        .ToListAsync();
                    return View(bills);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                var telecomDBContext = _context.Bills.Include(b => b.Phone);
                return View(await telecomDBContext.ToListAsync());
            }
        }

        // GET: Bills/Details/5
        public async Task<IActionResult> Details(int? id, string userType, int userId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.Phone)
                .FirstOrDefaultAsync(m => m.BillId == id);
            if (bill == null)
            {
                return NotFound();
            }

            ViewData["UserType"] = userType;
            ViewData["UserId"] = userId;
            return View(bill);
        }

        // GET: Bills/Create
        public IActionResult Create(int phoneId, string userType, string userId)
        {
            var phone = _context.Phones.FirstOrDefault(p => p.PhoneId == phoneId);
            if (phone == null)
            {
                return NotFound();
            }

            var program = _context.PhonePrograms.Find(phone.ProgramId);
            if (program == null)
            {
                return NotFound();
            }

            ViewData["PhoneNumber"] = phone.PhoneNumber;
            ViewData["Charge"] = program.Charge;
            ViewData["UserType"] = userType;
            ViewData["UserId"] = userId;
            return View(new BillCalls { Bill = new Bill { PhoneId = phoneId } });
        }

        // POST: Bills/Create
        [HttpPost]
        [Route("Bills/Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BillCalls model, string userType, string userId)
        {
            if (ModelState.IsValid)
            {
                _context.Bills.Add(model.Bill);
                await _context.SaveChangesAsync();

                foreach (var call in model.Calls)
                {
                    call.Bills.Add(model.Bill);
                    _context.Calls.Add(call);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Clients", new { userType, userId });
            }

            ViewData["UserType"] = userType;
            ViewData["UserId"] = userId;
            return View(model);
        }

        // GET: Bills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["PhoneId"] = new SelectList(_context.Phones, "PhoneId", "PhoneId", bill.PhoneId);
            return View(bill);
        }

        // POST: Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BillId,PhoneId,Costs")] Bill bill)
        {
            if (id != bill.BillId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.BillId))
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
            ViewData["PhoneId"] = new SelectList(_context.Phones, "PhoneId", "PhoneId", bill.PhoneId);
            return View(bill);
        }

        // GET: Bills/Delete/5
        public async Task<IActionResult> Delete(int? id, string userType, int userId)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bills
                .Include(b => b.Phone)
                .FirstOrDefaultAsync(m => m.BillId == id);
            if (bill == null)
            {
                return NotFound();
            }

            ViewData["UserType"] = userType;
            ViewData["UserId"] = userId;
            return View(bill);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, string userType, int userId)
        {
            var bill = await _context.Bills
                .Include(b => b.Calls)
                .FirstOrDefaultAsync(b => b.BillId == id);

            if (bill != null)
            {
                _context.Calls.RemoveRange(bill.Calls);
                _context.Bills.Remove(bill);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index), new { userType, userId });
        }

        private bool BillExists(int id)
        {
            return _context.Bills.Any(e => e.BillId == id);
        }
    }
}
