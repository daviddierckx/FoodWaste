using AvansFysio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AvansFysio.Repository
{
    public class PatientRepository : IPatientRepository
    {

        private readonly PatientContext _context;
        public PatientRepository(PatientContext context)
        {
            _context = context;
        }

        public IEnumerable<Patient> GetAllPatients()
        {
            return _context.Patients.ToList();
        }
        public IEnumerable<Patient> GetAllPatientsSorted()
        {
            var sortedRepository = _context.Patients.Where(x => x.DatumAanmelding.Date == DateTime.Now.Date).ToList();
            return sortedRepository;
        }
        public async Task AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }
        public IEnumerable<Patient> GetPatient(int id)
        {
            return _context.Patients.Where(g => g.Id == id);
        }

        //public int GetPatientCount()
        //{
        //    return Patients.Count();
        //}
    }
}
