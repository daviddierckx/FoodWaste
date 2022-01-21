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
    public class OpmerkingensController : Controller
    {
        private readonly PatientContext _context;

        public OpmerkingensController(PatientContext context)
        {
            _context = context;
        }

        // GET: Opmerkingens
        public async Task<IActionResult> Index()
        {
            var patientContext = _context.Opmerkingen.Include(o => o.Patient);
            return View(await patientContext.ToListAsync());
        }

        // GET: Opmerkingens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opmerkingen = await _context.Opmerkingen
                .Include(o => o.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (opmerkingen == null)
            {
                return NotFound();
            }

            return View(opmerkingen);
        }

        // GET: Opmerkingens/Create
        public IActionResult Create()
        {
            int lastProductId = _context.Patients.Max(item => item.Id);
            ViewData["ID"] = lastProductId;
            return View();
        }

        // POST: Opmerkingens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Opmerking,Datum,OpmerkingenGemaaktDoor,ZichtbaarVoorPatiënt,PatientId")] Opmerkingen opmerkingen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(opmerkingen);
                await _context.SaveChangesAsync();
                int lastProductId = _context.Patients.Max(item => item.Id);
                ViewData["ID"] = lastProductId;
                return Redirect("~/patient");
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Behandeling", opmerkingen.PatientId);
            return View(opmerkingen);
        }

        // GET: Opmerkingens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opmerkingen = await _context.Opmerkingen.FindAsync(id);
            if (opmerkingen == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Behandeling", opmerkingen.PatientId);
            return View(opmerkingen);
        }

        // POST: Opmerkingens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Opmerking,Datum,OpmerkingenGemaaktDoor,ZichtbaarVoorPatiënt,PatientId")] Opmerkingen opmerkingen)
        {
            if (id != opmerkingen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opmerkingen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OpmerkingenExists(opmerkingen.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/patient/details/" + id);
            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Behandeling", opmerkingen.Patient);
            return View(opmerkingen);
        }

        // GET: Opmerkingens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var opmerkingen = await _context.Opmerkingen
                .Include(o => o.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (opmerkingen == null)
            {
                return NotFound();
            }

            return View(opmerkingen);
        }

        // POST: Opmerkingens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var opmerkingen = await _context.Opmerkingen.FindAsync(id);
            _context.Opmerkingen.Remove(opmerkingen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OpmerkingenExists(int id)
        {
            return _context.Opmerkingen.Any(e => e.Id == id);
        }
    }
}
