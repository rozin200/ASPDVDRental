﻿using Microsoft.AspNetCore.Mvc;
using RopeyDVDRental.Models;
using Microsoft.EntityFrameworkCore;

namespace RopeyDVDRental.Controllers
{
    public class OldCopiesController : Controller
    {
        private readonly ApplicationDbContext  _context;

        public OldCopiesController (ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var loans = _context.Loan.Where(l=>l.DateOut.AddDays(31) <= DateTime.Now).ToList();

            var ret = from title in _context.Set<DVDTitle>()
                      join cpy in _context.Set<DVDCopy>()
                      on title.DVDNumber equals cpy.DVDNumber
                      where !(from l in loans select l.CopyNumber).Contains(cpy.CopyNumber)
                      select title;
            return View(ret.Distinct());
        }
    }
}
