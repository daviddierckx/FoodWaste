using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AvansFysio.Models;
using AvansFysio.Helper;

namespace AvansFysio.Controllers
{
    public class BehandelplansController : Controller
    {
        private readonly PatientContext _context;
        VektislijstApi _api = new VektislijstApi();

        public BehandelplansController(PatientContext context)
        {
            _context = context;
        }

        // GET: Behandelplans
        public async Task<IActionResult> Index()
        {
            var patientContext = _context.Behandelplan.Include(b => b.Patient);
            return View(await patientContext.ToListAsync());
        }

        // GET: Behandelplans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var behandelplan = await _context.Behandelplan
                .Include(b => b.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (behandelplan == null)
            {
                return NotFound();
            }

            return View(behandelplan);
        }

        // GET: Behandelplans/Create
        public IActionResult Create()
        {
            int lastProductId = _context.Patients.Max(item => item.Id);
            ViewData["ID"] = lastProductId; 
            return View();
        }

        // POST: Behandelplans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BehandelPlanNaam,Duur,Hoeveel,PatientId")] Behandelplan behandelplan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(behandelplan);
                await _context.SaveChangesAsync();
                
                int lastProductId = _context.Patients.Max(item => item.Id);
                ViewData["ID"] = lastProductId;
                return RedirectToAction("Create", "Behandelings");
            }
           
            return View(behandelplan);
        }

        // GET: Behandelplans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var behandelplan = await _context.Behandelplan.FindAsync(id);
            if (behandelplan == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Behandeling", behandelplan.PatientId);
            return View(behandelplan);
        }

        // POST: Behandelplans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BehandelPlanNaam,Duur,Hoeveel,PatientId")] Behandelplan behandelplan)
        {
            if (id != behandelplan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(behandelplan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BehandelplanExists(behandelplan.Id))
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
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Behandeling", behandelplan.PatientId);
            return View(behandelplan);
        }

        // GET: Behandelplans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var behandelplan = await _context.Behandelplan
                .Include(b => b.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (behandelplan == null)
            {
                return NotFound();
            }

            return View(behandelplan);
        }

        // POST: Behandelplans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var behandelplan = await _context.Behandelplan.FindAsync(id);
            _context.Behandelplan.Remove(behandelplan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BehandelplanExists(int id)
        {
            return _context.Behandelplan.Any(e => e.Id == id);
        }
    }
}
