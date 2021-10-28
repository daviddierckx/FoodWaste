using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AvansFysio;
using AvansFysio.Models;
using AvansFysio.Repository;

namespace AvansFysio.Components
{
    public class PatientsViewComponent:ViewComponent
    {
        private readonly IPatientRepository _patientRepository;
        private readonly PatientContext _context;


        public PatientsViewComponent(IPatientRepository patientRepository, PatientContext patientContext)
        {
            _patientRepository = patientRepository;
            _context = patientContext;
        }

        public IViewComponentResult Invoke()
        {
            var patients = _patientRepository.GetAllPatientsSorted().Count();
            ViewData["PatientCount"] = patients;
            return View("patients");
        }
        
    }
}
