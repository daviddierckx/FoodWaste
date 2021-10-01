using AvansFysio.Models;
using System.Collections.Generic;

namespace AvansFysio.Repository
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> Patients { get; }

        void AddPatient(Patient patient);
        Patient GetPatient(int id);
        int GetPatientCount();
    }
}