using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using S.G.H.Models.Repositories;

namespace S.G.H.Models
{
    public class PatientRepository : IPatientRepository<Patient>
    {

        AppDbContext dbContext;


        public PatientRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public void Add(Patient patient)
        {
            dbContext.Patients.Add(patient);
            dbContext.SaveChanges();
        }


        public void Delete(int id)
        {
            var patient = Find(id);
            dbContext.Patients.Remove(patient);
            dbContext.SaveChanges();
        }


        public Patient Find(int id)
        {
            Patient patient = dbContext.Patients.Include(a => a.SonDocteur).SingleOrDefault(a => a.Matricule == id);
            return patient;
        }


        public List<Patient> GetPatientsList()
        {
            List<Patient> patients = dbContext.Patients.Include(a => a.SonDocteur).ToList();
            return patients;
        }


        public void Update(Patient newPatient,int id)
        {
            dbContext.Update(newPatient);
            dbContext.SaveChanges();
        }


        public List<Patient> Search(string prenom)
        {
            List<Patient> result;

            if (prenom == null)
            {
                result = GetPatientsList();
            }
            else
            {
                result = dbContext.Patients.Include(a => a.SonDocteur).Where(e => e.Prenom.Contains(prenom)).ToList();
            }
            return result;
        }
    }
}
