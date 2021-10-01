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
        private readonly IPatientRepository _repository;

        public PatientsViewComponent(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var patients =  _repository.GetPatientCount();
            ViewData["PatientCount"] = patients;
            return View("patients");
        }
      
    }
}
