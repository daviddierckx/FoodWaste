using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AvansFysio.Models;
using AvansFysio.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Dynamic;

namespace AvansFysio.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientContext _context;
        private readonly IPatientRepository _patientRepository;
        public bool isSorted = true;

        public PatientController(PatientContext context,IPatientRepository patientRepository)
        {
            _context = context;
            _patientRepository = patientRepository;
        }

        [Authorize]
        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "vandaag" : "";
            var patients = _patientRepository.GetAllPatients();
            switch (sortOrder)
            {
                case "vandaag":
                    patients = _patientRepository.GetAllPatientsSorted();
                    break;
                default:
                    patients = _patientRepository.GetAllPatients();
                    break;
            }
            return View(patients.ToList());
        }
        // GET: Patient/Details/5
        [Authorize(Roles ="Therapeut")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            var behandelplan = _context.Behandelplan.Where(n => n.Patient.Id == id).Select(x => x).FirstOrDefault();
            var behandeling = _context.Behandeling.Where(n => n.Patient.Id == id).Select(x => x).FirstOrDefault();
            var opmerkingen = _context.Opmerkingen.Where(n => n.Patient.Id == id).Select(x => x).FirstOrDefault();

            dynamic mymodel = new ExpandoObject();
            mymodel.Behandelplan = behandelplan;
            mymodel.Behandeling = behandeling;
            mymodel.Opmerkingen = opmerkingen;

            if (patient == null)
            {
                return NotFound();
            }
            return View(mymodel);
        }
       
       
        // GET: Patient/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naam,Email,Leeftijd,Omschrijving,DiagnoseCode,MedewerkerOfStudent,IntakeGedaanDoor,onderSupervisieVan,HoofdBehandelaar,DatumAanmelding,DatumOntslag,Opmerkingen,Behandeling")] Patient patient)
        {
            var patients = patient.Id;

            if (ModelState.IsValid)
            {

                _context.Add(patient);
                await _context.SaveChangesAsync();
                int lastProductId = _context.Patients.Max(item => item.Id);
                ViewData["ID"] = lastProductId;
                return RedirectToAction("Create", "Behandelplans");
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }
        // GET: Patients/Edit/5
        public async Task<IActionResult> Behandelplan(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.FindAsync(id);
            var behandelplan =  _context.Behandelplan.Where(n => n.Patient.Id == id).Select(x => x).ToList() ;
            if (patient == null)
            {
                return NotFound();
            }
            return View(behandelplan);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naam,Email,Leeftijd,Omschrijving,DiagnoseCode,MedewerkerOfStudent,IntakeGedaanDoor,onderSupervisieVan,HoofdBehandelaar,DatumAanmelding,DatumOntslag,Opmerkingen,Behandeling")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("~/patient/details/"+patient.Id);
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }
    }
}
