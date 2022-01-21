using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AvansFysio.Models;

namespace AvansFysio.Controllers
{
    public class BeschikbaarsController : Controller
    {
        private readonly PatientContext _context;

        public BeschikbaarsController(PatientContext context)
        {
            _context = context;
        }

        // GET: Beschikbaars
        public async Task<IActionResult> Index()
        {
            var query = _context.Beschikbaar.Where(i => i.Id == 1);
            
            string[] daysOfTheWeek = { "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag" };
            if (_context.Beschikbaar.Count() < 1)
            {
                for (int i = 0; i < daysOfTheWeek.Length; i++)
                {
                    _context.Add(new Beschikbaar()
                    {
                        BeginTijd = new TimeSpan(0, 0, 0, 0),
                        EindTijd = new TimeSpan(0, 0, 0, 0),
                        BeschikbaarOpDieDag = false,
                        Dag = daysOfTheWeek[i]
                    }); 
                }
            }
            await _context.SaveChangesAsync();

            return View(await _context.Beschikbaar.ToListAsync());
        }

        // GET: Beschikbaars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beschikbaar = await _context.Beschikbaar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beschikbaar == null)
            {
                return NotFound();
            }

            return View(beschikbaar);
        }

        // GET: Beschikbaars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Beschikbaars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BeschikbaarOpDieDag,Dag,BeginTijd,EindTijd")] Beschikbaar beschikbaar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beschikbaar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(beschikbaar);
        }

        // GET: Beschikbaars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beschikbaar = await _context.Beschikbaar.FindAsync(id);
            if (beschikbaar == null)
            {
                return NotFound();
            }
            return View(beschikbaar);
        }

        // POST: Beschikbaars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BeschikbaarOpDieDag,Dag,BeginTijd,EindTijd")] Beschikbaar beschikbaar)
        {
            if (id != beschikbaar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beschikbaar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeschikbaarExists(beschikbaar.Id))
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
            return View(beschikbaar);
        }

        // GET: Beschikbaars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beschikbaar = await _context.Beschikbaar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (beschikbaar == null)
            {
                return NotFound();
            }

            return View(beschikbaar);
        }

        // POST: Beschikbaars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var beschikbaar = await _context.Beschikbaar.FindAsync(id);
            _context.Beschikbaar.Remove(beschikbaar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeschikbaarExists(int id)
        {
            return _context.Beschikbaar.Any(e => e.Id == id);
        }
    }
}
