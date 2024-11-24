using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using COMP2084Assign2Real.Data;
using COMP2084Assign2Real.Models;
using Microsoft.AspNetCore.Authorization;

namespace COMP2084Assign2Real.Controllers
{
    [Authorize]
    //remove from consumers view
    public class UserRentalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserRentalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserRentals
        public async Task<IActionResult> Index()
        {
            return View(await _context.userRental.ToListAsync());
        }

        // GET: UserRentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRental = await _context.userRental
                .FirstOrDefaultAsync(m => m.UserRentalId == id);
            if (userRental == null)
            {
                return NotFound();
            }

            return View(userRental);
        }

        // GET: UserRentals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserRentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserRentalId,name,email,phone")] UserRental userRental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userRental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userRental);
        }

        // GET: UserRentals/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRental = await _context.userRental.FindAsync(id);
            if (userRental == null)
            {
                return NotFound();
            }
            return View(userRental);
        }

        // POST: UserRentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("UserRentalId,name,email,phone")] UserRental userRental)
        {
            if (id != userRental.UserRentalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userRental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserRentalExists(userRental.UserRentalId))
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
            return View(userRental);
        }

        // GET: UserRentals/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userRental = await _context.userRental
                .FirstOrDefaultAsync(m => m.UserRentalId == id);
            if (userRental == null)
            {
                return NotFound();
            }

            return View(userRental);
        }

        // POST: UserRentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userRental = await _context.userRental.FindAsync(id);
            if (userRental != null)
            {
                _context.userRental.Remove(userRental);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserRentalExists(int id)
        {
            return _context.userRental.Any(e => e.UserRentalId == id);
        }
    }
}
