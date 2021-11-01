using System.Collections.Generic;
using System.Linq;
using FourPatient.DataAccess.Entities;
using FourPatient.Domain;
using Patient = FourPatient.Domain.Tables.Patient;
using Review = FourPatient.Domain.Tables.Review;

// This Class hold access methods for data layer

namespace FourPatient.DataAccess
{
    public class PatientRepo : IPatient
    {
        private readonly _4PatientContext _context;

        public PatientRepo(_4PatientContext context)
        {
            context.Database.EnsureCreated();
            _context = context;
        }
        public IEnumerable<Patient> GetAll()
        {
            ICollection<Entities.Patient> List = _context.Patients.ToList();
            ICollection<Patient> N = List.Select(n => (Patient)Map.Table(n)).ToList();

            foreach (var Patient in N)
            {
                ICollection<Entities.Review> R = _context.Reviews.Where(x => x.PatientId == Patient.Id).ToList();
                Patient.Reviews = R.Select(x => (Review)Map.Table(x)).ToList();
            };

            return N;
        }

        public Patient Get(int id)
        {
            // The DbSet .Find() method searches DB based on primary key value
            var n = _context.Patients.Find(id); // This Enumerable method also works .First(n => n.Id == id);
            Patient Patient = (Patient)Map.Table(n);

            ICollection<Entities.Review> R = _context.Reviews.Where(x => x.PatientId == Patient.Id).ToList();
            Patient.Reviews = R.Select(x => (Review)Map.Table(x)).ToList();

            // This closes the DBContext entity tracker so the same entity can be queried again in the same unit of work
            //_context.Entry(n).State = EntityState.Detached;

            return Patient;
        }

        public void Create(Patient N)
        {
            // map to EF entity
            var entity = (Entities.Patient)Map.Entity(N);

            _context.Patients.Add(entity);

            // write changes to DB
            _context.SaveChanges();
        }
        public void Update(Patient P)
        {
            // map to EF entity
            _context.Patients.Update((Entities.Patient)Map.Entity(P));

            // write changes to DB
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            Entities.Patient patient = (Entities.Patient)Map.Entity(Get(id));

            _context.ChangeTracker.Clear();

            _context.Remove(patient);

            // write changes to DB
            _context.SaveChanges();
        }
    }
}
