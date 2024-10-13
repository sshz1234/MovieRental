﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using COMP2084Assign2Real.Data;
using COMP2084Assign2Real.Models;

namespace COMP2084Assign2Real.Controllers
{
    public class MovieRentalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieRentalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovieRentals
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rentals.ToListAsync());
        }

        // GET: MovieRentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieRental = await _context.Rentals
                .FirstOrDefaultAsync(m => m.MovieRentalId == id);
            if (movieRental == null)
            {
                return NotFound();
            }

            return View(movieRental);
        }

        // GET: MovieRentals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MovieRentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieRentalId,owingAmount,movieTitle,dueDate,rentalDate")] MovieRental movieRental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieRental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movieRental);
        }

        // GET: MovieRentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieRental = await _context.Rentals.FindAsync(id);
            if (movieRental == null)
            {
                return NotFound();
            }
            return View(movieRental);
        }

        // POST: MovieRentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieRentalId,owingAmount,movieTitle,dueDate,rentalDate")] MovieRental movieRental)
        {
            if (id != movieRental.MovieRentalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieRental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieRentalExists(movieRental.MovieRentalId))
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
            return View(movieRental);
        }

        // GET: MovieRentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieRental = await _context.Rentals
                .FirstOrDefaultAsync(m => m.MovieRentalId == id);
            if (movieRental == null)
            {
                return NotFound();
            }

            return View(movieRental);
        }

        // POST: MovieRentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieRental = await _context.Rentals.FindAsync(id);
            if (movieRental != null)
            {
                _context.Rentals.Remove(movieRental);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieRentalExists(int id)
        {
            return _context.Rentals.Any(e => e.MovieRentalId == id);
        }
    }
}
