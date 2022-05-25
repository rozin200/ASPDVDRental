﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RopeyDVDRental.Models;

namespace RopeyDVDRental.Controllers
{
    public class DVDTitlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DVDTitlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DVDTitles
        public async Task<IActionResult> Index()
        {
            //return View(await _context.DVDTitle.ToListAsync());
            var ApplicationDbContext = _context.DVDTitle.Include(d => d.DVDCategory).Include(d => d.Producer).Include(d => d.Studio);
            return View(await ApplicationDbContext.ToListAsync());
        }

        // GET: DVDTitles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDTitle = await _context.DVDTitle
                .Include(d => d.DVDCategory)
                .Include(d => d.Producer)
                .Include(d => d.Studio)
                .FirstOrDefaultAsync(m => m.DVDNumber == id);
            if (dVDTitle == null)
            {
                return NotFound();
            }

            return View(dVDTitle);
        }

        // GET: DVDTitles/Create
        public IActionResult Create()
        {
            ViewData["CategoryNumber"] = new SelectList(_context.DVDCategory, "CategoryNumber", "CategoryNumber");
            ViewData["ProducerNumber"] = new SelectList(_context.Producer, "ProducerNumber", "ProducerNumber");
            ViewData["StudioNumber"] = new SelectList(_context.Studio, "StudioNumber", "StudioNumber");
            return View();
        }

        // POST: DVDTitles/Create

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DVDNumber,title,DateReleased,StandardCharge,PenaltyCharge,CategoryNumber,StudioNumber,ProducerNumber")] DVDTitle dVDTitle)
        {

             Console.WriteLine(dVDTitle);
            //DVDCategory dVDCategory = _context.DVDCategory.Where(l => l.CategoryNumber == dVDTitle.CategoryNumber).FirstOrDefault();
         //   Producer producer = _context.Producer.Where(l => l.ProducerNumber == dVDTitle.ProducerNumber).FirstOrDefault();
            //Studio studio = _context.Studio.Where(l => l.StudioNumber == dVDTitle.StudioNumber).FirstOrDefault();
           // dVDTitle.CategoryNumber = dVDCategory.CategoryNumber;
         //   dVDTitle.ProducerNumber = producer.ProducerNumber;
          //  dVDTitle.StudioNumber = studio.StudioNumber;
           // if (ModelState.IsValid)
         //   {
                _context.Add(dVDTitle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
         //   }
            ViewData["CategoryNumber"] = new SelectList(_context.DVDCategory, "CategoryNumber", "CategoryNumber", dVDTitle.CategoryNumber);
            ViewData["ProducerNumber"] = new SelectList(_context.Producer, "ProducerNumber", "ProducerNumber", dVDTitle.ProducerNumber);
            ViewData["StudioNumber"] = new SelectList(_context.Studio, "StudioNumber", "StudioNumber", dVDTitle.StudioNumber);
            return View(dVDTitle);
            

        }

        // GET: DVDTitles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDTitle = await _context.DVDTitle.FindAsync(id);
            if (dVDTitle == null)
            {
                return NotFound();
            }
            ViewData["CategoryNumber"] = new SelectList(_context.DVDCategory, "CategoryNumber", "CategoryNumber", dVDTitle.CategoryNumber);
            ViewData["ProducerNumber"] = new SelectList(_context.Producer, "ProducerNumber", "ProducerNumber", dVDTitle.ProducerNumber);
            ViewData["StudioNumber"] = new SelectList(_context.Studio, "StudioNumber", "StudioNumber", dVDTitle.StudioNumber);
            return View(dVDTitle);
        }

        // POST: DVDTitles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DVDNumber,title,DateReleased,StandardCharge,PenaltyCharge,CategoryNumber,StudioNumber,ProducerNumber")] DVDTitle dVDTitle)
        {
            if (id != dVDTitle.DVDNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dVDTitle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DVDTitleExists(dVDTitle.DVDNumber))
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
            ViewData["CategoryNumber"] = new SelectList(_context.DVDCategory, "CategoryNumber", "CategoryNumber", dVDTitle.CategoryNumber);
            ViewData["ProducerNumber"] = new SelectList(_context.Producer, "ProducerNumber", "ProducerNumber", dVDTitle.ProducerNumber);
            ViewData["StudioNumber"] = new SelectList(_context.Studio, "StudioNumber", "StudioNumber", dVDTitle.StudioNumber);
            return View(dVDTitle);
        }

        // GET: DVDTitles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDTitle = await _context.DVDTitle
                .Include(d => d.DVDCategory)
                .Include(d => d.Producer)
                .Include(d => d.Studio)
                .FirstOrDefaultAsync(m => m.DVDNumber == id);
            if (dVDTitle == null)
            {
                return NotFound();
            }

            return View(dVDTitle);
        }

        // POST: DVDTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dVDTitle = await _context.DVDTitle.FindAsync(id);
            _context.DVDTitle.Remove(dVDTitle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DVDTitleExists(int id)
        {
            return _context.DVDTitle.Any(e => e.DVDNumber == id);
        }
    }
}
