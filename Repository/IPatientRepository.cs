using AvansFysio.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvansFysio.Repository
{
    public interface IPatientRepository
    {
        Task AddPatient(Patient patient);
        IEnumerable<Patient> GetAllPatients();
        IEnumerable<Patient> GetPatient(int id);
        public IEnumerable<Patient> GetAllPatientsSorted();
    }
}