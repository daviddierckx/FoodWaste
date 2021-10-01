using AvansFysio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AvansFysio.Repository
{
    public class PatientRepository : IPatientRepository   
    {
        public int num = 0;
        public List<Patient> patients = new List<Patient>();


        public IEnumerable<Patient> Patients => patients;

        public void AddPatient(Patient patient)
        {
            patient.Id = num;
            patients.Add(patient);
            num++;
        }
        public Patient GetPatient(int id)
        {
            return patients.Find(x => x.Id == id);
        }
        public int GetPatientCount()
        {
            return Patients.Count();
        }
    }
}
