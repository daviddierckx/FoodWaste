using AvansFysio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AvansFysio.Repository;

namespace AvansFysio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPatientRepository _repository;

        public HomeController(ILogger<HomeController> logger,IPatientRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.GetAllPatients());
        }

        [HttpGet]
        public ViewResult VoegPatient()
        {
            return View();
        }
        [HttpPost]
        public ActionResult VoegPatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _repository.AddPatient(patient);
       
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
          
        }

        public ActionResult Details(int id)
        {
            Patient patient = (Patient)_repository.GetPatient(id);
            if (patient == null)
            {
                return View("NotFound");
            }
            Console.WriteLine(patient.Id + " "+patient.Naam);

            return View("Details",patient);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
       
    }
}
