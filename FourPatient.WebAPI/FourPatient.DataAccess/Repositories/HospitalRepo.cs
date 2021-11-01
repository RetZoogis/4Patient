using FourPatient.DataAccess.Entities;
using FourPatient.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Hospital = FourPatient.Domain.Tables.Hospital;
using Review = FourPatient.Domain.Tables.Review;

// This Class hold access methods for data layer

namespace FourPatient.DataAccess
{
    public class HospitalRepo : IHospital
    {
        private readonly _4PatientContext _context;

        public HospitalRepo(_4PatientContext context)
        {
            context.Database.EnsureCreated();
            _context = context;
        }
        public IEnumerable<Hospital> GetAll()
        {
            ICollection<Entities.Hospital> List = _context.Hospitals.ToList();
            ICollection<Hospital> N = List.Select(n => (Hospital)Map.Table(n)).ToList();

            foreach (var Hospital in N)
            {
                ICollection<Entities.Review> R = _context.Reviews.Where(x => x.HospitalId == Hospital.Id).ToList();
                Hospital.Reviews = R.Select(x => (Review)Map.Table(x)).ToList();
            };

            return N;
        }

        public IEnumerable<Hospital> SearchName(string str)
        {
            ICollection<Entities.Hospital> List = _context.Hospitals.ToList();
            ICollection<Entities.Hospital> query = List.Where(Hospital => Hospital.Name.ToLower().Contains(str.ToLower())).ToList();
            ICollection<Hospital> N = query.Select(n => (Hospital)Map.Table(n)).ToList();
            foreach (var Hospital in N)
            {
                ICollection<Entities.Review> R = _context.Reviews.Where(x => x.HospitalId == Hospital.Id).ToList();
                Hospital.Reviews = R.Select(x => (Review)Map.Table(x)).ToList();
            };

            return N;
        }
        public IEnumerable<Hospital> SearchZip(int zip)
        {
            ICollection<Entities.Hospital> List = _context.Hospitals.ToList();
            ICollection<Entities.Hospital> query = List.Where(Hospital => Hospital.ZipCode == zip).ToList();
            ICollection<Hospital> N = query.Select(n => (Hospital)Map.Table(n)).ToList();
            foreach (var Hospital in N)
            {
                ICollection<Entities.Review> R = _context.Reviews.Where(x => x.HospitalId == Hospital.Id).ToList();
                Hospital.Reviews = R.Select(x => (Review)Map.Table(x)).ToList();
            };

            return N;
        }
        public IEnumerable<Hospital> SearchCity(string str)
        {
            ICollection<Entities.Hospital> List = _context.Hospitals.ToList();
            ICollection<Entities.Hospital> query = List.Where(Hospital => Hospital.City.ToLower().Contains(str.ToLower())).ToList();
            ICollection<Hospital> N = query.Select(n => (Hospital)Map.Table(n)).ToList();
            foreach (var Hospital in N)
            {
                ICollection<Entities.Review> R = _context.Reviews.Where(x => x.HospitalId == Hospital.Id).ToList();
                Hospital.Reviews = R.Select(x => (Review)Map.Table(x)).ToList();
            };

            return N;
        }
        public IEnumerable<Hospital> SearchAddress(string str)
        {
            ICollection<Entities.Hospital> List = _context.Hospitals.ToList();
            ICollection<Entities.Hospital> query = List.Where(Hospital => Hospital.Address.ToLower().Contains(str.ToLower())).ToList();
            ICollection<Hospital> N = query.Select(n => (Hospital)Map.Table(n)).ToList();
            foreach (var Hospital in N)
            {
                ICollection<Entities.Review> R = _context.Reviews.Where(x => x.HospitalId == Hospital.Id).ToList();
                Hospital.Reviews = R.Select(x => (Review)Map.Table(x)).ToList();
            };

            return N;
        }
        public IEnumerable<Hospital> SearchDepartments(string str)
        {
            ICollection<Entities.Hospital> List = _context.Hospitals.ToList();
            ICollection<Entities.Hospital> query = List.Where(Hospital => Hospital.Departments.ToLower().Contains(str.ToLower())).ToList();
            ICollection<Hospital> N = query.Select(n => (Hospital)Map.Table(n)).ToList();
            foreach (var Hospital in N)
            {
                ICollection<Entities.Review> R = _context.Reviews.Where(x => x.HospitalId == Hospital.Id).ToList();
                Hospital.Reviews = R.Select(x => (Review)Map.Table(x)).ToList();
            };

            return N;
        }
        public IEnumerable<Hospital> SearchState(string str)
        {
            ICollection<Entities.Hospital> List = _context.Hospitals.ToList();
            ICollection<Entities.Hospital> query = List.Where(Hospital => Hospital.State.ToLower().Contains(str.ToLower())).ToList();
            ICollection<Hospital> N = query.Select(n => (Hospital)Map.Table(n)).ToList();
            foreach (var Hospital in N)
            {
                ICollection<Entities.Review> R = _context.Reviews.Where(x => x.HospitalId == Hospital.Id).ToList();
                Hospital.Reviews = R.Select(x => (Review)Map.Table(x)).ToList();
            };

            return N;
        }
        public Hospital Get(int id)
        {
            // The DbSet .Find() method searches DB based on primary key value
            var n = _context.Hospitals.Find(id); // This Enumerable method also works .First(n => n.Id == id);
            Hospital Hospital = (Hospital)Map.Table(n);

            ICollection<Entities.Review> R = _context.Reviews.Where(x => x.HospitalId == Hospital.Id).ToList();
            Hospital.Reviews = R.Select(x => (Review)Map.Table(x)).ToList();

            return Hospital;
        }

        public void Create(Hospital H)
        {
            // map to EF entity
            var entity = (Entities.Hospital)Map.Entity(H);

            _context.Hospitals.Add(entity);

            // write changes to DB
            _context.SaveChanges();
        }
        public void Update(Hospital H)
        {
            H = Average(H);

            _context.ChangeTracker.Clear();

            // map to EF entity
            _context.Hospitals.Update((Entities.Hospital)Map.Entity(H));

            // write changes to DB
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            Entities.Hospital hospital = (Entities.Hospital)Map.Entity(Get(id));

            _context.ChangeTracker.Clear();

            _context.Remove(hospital);

            // write changes to DB
            _context.SaveChanges();
        }
        public Hospital Average(Hospital H)
        {
            decimal sum = 0;
            int i = 0;

            decimal sumA = 0;
            decimal sumCl = 0;
            decimal sumC = 0;
            decimal sumN = 0;
            int countA = 0;
            int countC = 0;
            int countCl = 0;
            int countN = 0;

            ICollection<Entities.Review> R = _context.Reviews.Where(x => x.HospitalId == H.Id).ToList();

            foreach (var review in R)
            {
                sum += review.Comfort;
                i += review.Comfort != 0 ? 1 : 0;

                Entities.Accommodation a = _context.Accommodations.Find(review.Id);
                if (a != null)
                {
                    sumA += a.AverageA ?? 0;
                    countA += a.AverageA != null ? 1 : 0;
                }

                Entities.Cleanliness cl = _context.Cleanlinesses.Find(review.Id);
                if (cl != null)
                {
                    sumCl += cl.AverageCl ?? 0;
                    countCl += cl.AverageCl != null ? 1 : 0;
                }

                Entities.Covid c = _context.Covids.Find(review.Id);
                if (c != null)
                {
                    sumC += c.AverageC ?? 0;
                    countC += c.AverageC != null ? 1 : 0;
                }

                Entities.Nursing n = _context.Nursings.Find(review.Id);
                if (n != null)
                {
                    sumN += n.AverageN ?? 0;
                    countN += n.AverageN != null ? 1 : 0;
                }
            }

            if (i != 0)
                H.Comfort = (decimal)sum / i;
            else
                H.Comfort = 0;

            if (countA != 0)
                H.Accomodations = (decimal)sumA / countA;
            else
                H.Accomodations = 0;

            if (countCl != 0)
                H.Cleanliness = (decimal)sumCl / countCl;
            else
                H.Cleanliness = 0;

            if (countC != 0)
                H.Covid = (decimal)sumC / countC;
            else
                H.Covid = 0;

            if (countN != 0)
                H.Nursing = (decimal)sumN / countN;
            else
                H.Nursing = 0;

            return H;
        }
    }
}
