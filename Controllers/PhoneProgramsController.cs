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
    public class PhoneProgramsController : Controller
    {
        private readonly TelecomDBContext _context;

        public PhoneProgramsController(TelecomDBContext context)
        {
            _context = context;
        }

        // GET: PhonePrograms
        public async Task<IActionResult> Index(string userType)
        {
            ViewData["UserType"] = userType;

            return View(await _context.PhonePrograms.ToListAsync());
        }

        // GET: PhonePrograms/Details/5
        public async Task<IActionResult> Details(int? id, string userType)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneProgram = await _context.PhonePrograms
                .FirstOrDefaultAsync(m => m.ProgramId == id);
            if (phoneProgram == null)
            {
                return NotFound();
            }

            ViewData["UserType"] = userType;
            return View(phoneProgram);
        }

        // GET: PhonePrograms/Create
        public IActionResult Create(string userType)
        {
            ViewData["UserType"] = userType;
            return View();
        }

        // POST: PhonePrograms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProgramId,ProgramName,Benefits,Charge")] PhoneProgram phoneProgram, string userType)
        {
            Console.WriteLine(phoneProgram);
            if (ModelState.IsValid)
            {
                _context.Add(phoneProgram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { userType });
            }

            // Log ModelState errors
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"Error in {state.Key}: {error.ErrorMessage}");
                }
            }

            ViewData["UserType"] = userType;
            return View(phoneProgram);
        }

        // GET: PhonePrograms/Edit/5
        public async Task<IActionResult> Edit(int? id, string userType)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneProgram = await _context.PhonePrograms.FindAsync(id);
            if (phoneProgram == null)
            {
                return NotFound();
            }

            ViewData["UserType"] = userType;
            return View(phoneProgram);
        }

        // POST: PhonePrograms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProgramId,ProgramName,Benefits,Charge")] PhoneProgram phoneProgram, string userType)
        {
            if (id != phoneProgram.ProgramId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phoneProgram);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhoneProgramExists(phoneProgram.ProgramId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { userType });
            }

            ViewData["UserType"] = userType;

            return View(phoneProgram);
        }

        // GET: PhonePrograms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phoneProgram = await _context.PhonePrograms
                .FirstOrDefaultAsync(m => m.ProgramId == id);
            if (phoneProgram == null)
            {
                return NotFound();
            }

            return View(phoneProgram);
        }

        // POST: PhonePrograms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phoneProgram = await _context.PhonePrograms.FindAsync(id);
            if (phoneProgram != null)
            {
                _context.PhonePrograms.Remove(phoneProgram);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhoneProgramExists(int id)
        {
            return _context.PhonePrograms.Any(e => e.ProgramId == id);
        }
    }
}
