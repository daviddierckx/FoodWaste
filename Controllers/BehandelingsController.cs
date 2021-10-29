using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AvansFysio.Models;
using System.Net.Http;
using Newtonsoft.Json;
using AvansFysio.Helper;

namespace AvansFysio.Controllers
{
    public class BehandelingsController : Controller
    {
        private readonly PatientContext _context;
        VektislijstApi _api = new VektislijstApi();

        public BehandelingsController(PatientContext context)
        {
            _context = context;
        }

        // GET: Behandelings
        public async Task<IActionResult> Index()
        {
            var patientContext = _context.Behandeling.Include(b => b.Patient);
            return View(await patientContext.ToListAsync());
        }

        // GET: Behandelings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var behandeling = await _context.Behandeling
                .Include(b => b.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (behandeling == null)
            {
                return NotFound();
            }

            return View(behandeling);
        }

        // GET: Behandelings/Create
        public IActionResult Create()
        {
            int lastProductId = _context.Patients.Max(item => item.Id);
            ViewData["ID"] = lastProductId;
            return View();
        }

        // POST: Behandelings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Omschrijving,OefenzaalOfBehandel,Bijzonderheden,BehandelingUitgevoerdDoor,BehandelingUitgevoerdDatum,PatientId")] Behandeling behandeling)
        {
            if (ModelState.IsValid)
            {
                _context.Add(behandeling);
                await _context.SaveChangesAsync();

                int lastProductId = _context.Patients.Max(item => item.Id);
                ViewData["ID"] = lastProductId;
                return RedirectToAction("Create", "Opmerkingens");

            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Behandeling", behandeling.PatientId);
            return View(behandeling);
        }

        // GET: Behandelings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            List<VektislijstVerrichtingen> vektislijst = new List<VektislijstVerrichtingen>();
            HttpClient client = _api.Initial();
            var res = await client.GetAsync("api/VektislijstVerrichtingen");
            if (res.IsSuccessStatusCode)
            {
                string responseBody = await res.Content.ReadAsStringAsync();
                vektislijst = JsonConvert.DeserializeObject<List<VektislijstVerrichtingen>>(responseBody);
            }
          
          
            if (id == null)
            {
                return NotFound();
            }

            var behandeling = await _context.Behandeling.FindAsync(id);
            if (behandeling == null)
            {
                return NotFound();
            }
            List<VektislijstVerrichtingen> vektislijsts = vektislijst;
            var codes = new List<string>();
            foreach (var item in vektislijsts)
            {
                codes.Add(item.Waarde.ToString());
            }
            ViewBag.Vektislijst = codes;
            return View(behandeling);
        }

        // POST: Behandelings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Omschrijving,OefenzaalOfBehandel,Bijzonderheden,BehandelingUitgevoerdDoor,BehandelingUitgevoerdDatum,PatientId")] Behandeling behandeling)
        {
            if (id != behandeling.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(behandeling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BehandelingExists(behandeling.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/patient/details/"+id);

            }
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Behandeling", behandeling.PatientId);
            return View(behandeling);
        }

        // GET: Behandelings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var behandeling = await _context.Behandeling
                .Include(b => b.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (behandeling == null)
            {
                return NotFound();
            }

            return View(behandeling);
        }

        // POST: Behandelings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var behandeling = await _context.Behandeling.FindAsync(id);
            _context.Behandeling.Remove(behandeling);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BehandelingExists(int id)
        {
            return _context.Behandeling.Any(e => e.Id == id);
        }
    }
}
