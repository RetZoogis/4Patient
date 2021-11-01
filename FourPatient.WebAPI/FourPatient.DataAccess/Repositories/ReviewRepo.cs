using System;
using System.Collections.Generic;
using System.Linq;
using FourPatient.DataAccess.Entities;
using FourPatient.Domain;
using Accommodation = FourPatient.Domain.Tables.Accommodation;
using Cleanliness = FourPatient.Domain.Tables.Cleanliness;
using Covid = FourPatient.Domain.Tables.Covid;
using Hospital = FourPatient.Domain.Tables.Hospital;
using Nursing = FourPatient.Domain.Tables.Nursing;
using Patient = FourPatient.Domain.Tables.Patient;
using Review = FourPatient.Domain.Tables.Review;

// This Class hold access methods for data layer

namespace FourPatient.DataAccess
{
    public class ReviewRepo : IReview
    {
        private readonly _4PatientContext _context;
        private readonly IPatient _patientrepo;
        private readonly IHospital _hospitalrepo;

        public ReviewRepo(_4PatientContext context, IPatient patientrepo, IHospital hospitalrepo)
        {
            context.Database.EnsureCreated();
            _context = context;
            //_patientrepo = patientrepo;
            _hospitalrepo = hospitalrepo;
        }
        public IEnumerable<Review> GetAll()
        {
            ICollection<Entities.Review> List = _context.Reviews.ToList();
            ICollection<Review> N = List.Select(n => (Review)Map.Table(n)).ToList();

            foreach (var Review in N)
            {
                Review.Accommodation = (Accommodation?)Map.Table(_context.Accommodations.Find(Review.Id));
                Review.Cleanliness = (Cleanliness?)Map.Table(_context.Cleanlinesses.Find(Review.Id));
                Review.Covid = (Covid?)Map.Table(_context.Covids.Find(Review.Id));
                Review.Nursing = (Nursing?)Map.Table(_context.Nursings.Find(Review.Id));

                //Review.Hospital = _hospitalrepo.Get(Review.HospitalId);
                //Review.Patient = _patientrepo.Get(Review.PatientId);
                Review.Hospital = (Hospital)Map.Table(_context.Hospitals.Find(Review.HospitalId));
                Review.Patient = (Patient)Map.Table(_context.Patients.Find(Review.PatientId));
            };

            return N;
        }

        public Review Get(int id)
        {
            // The DbSet .Find() method searches DB based on primary key value
            var n = _context.Reviews.Find(id); // This Enumerable method also works .First(n => n.Id == id);
            Review Review = (Review)Map.Table(n);

            Review.Accommodation = (Accommodation?)Map.Table(_context.Accommodations.Find(Review.Id));
            Review.Cleanliness = (Cleanliness?)Map.Table(_context.Cleanlinesses.Find(Review.Id));
            Review.Covid = (Covid?)Map.Table(_context.Covids.Find(Review.Id));
            Review.Nursing = (Nursing?)Map.Table(_context.Nursings.Find(Review.Id));

            //Review.Hospital = _hospitalrepo.Get(Review.HospitalId);
            Review.Hospital = (Hospital)Map.Table(_context.Hospitals.Find(Review.HospitalId));
            //foreach (var review in Review.Hospital.Reviews)
            //{
            //    Review.Hospital.Reviews.Add((Review)Map.Table(review));
            //}

            //Review.Patient = _patientrepo.Get(Review.PatientId);
            Review.Patient = (Patient)Map.Table(_context.Patients.Find(Review.PatientId));
            //foreach (var review in Review.Patient.Reviews)
            //{
            //    Review.Patient.Reviews.Add((Review)Map.Table(review));
            //}

            // This closes the DBContext entity tracker so the same entity can be queried again in the same unit of work
            //_context.Entry(n).State = EntityState.Detached;

            return Review;
        }
        public IEnumerable<Review> Test()
        {
            ICollection<Entities.Review> List = _context.Reviews.ToList();
            ICollection<Review> N = List.Select(n => (Review)Map.Table(n)).ToList();

            return N;
        }

        public int Create(Review N)
        {
            
            // Recalculate average score
            N.Comfort = Average(N);

            // map to EF entity
            var entity = (Entities.Review)Map.Entity(N);

            _context.Reviews.Add(entity);

            // write changes to DB
            _context.SaveChanges();
            int id = entity.Id;
            return id;
        }
        public void Update(Review Review)
        {
            // Recalculate average score
            Review.Comfort = Average(Review);

            _context.ChangeTracker.Clear();

            // map to EF entity
            _context.Reviews.Update((Entities.Review)Map.Entity(Review));

            // write changes to DB
            _context.SaveChanges();

            _hospitalrepo.Update(_hospitalrepo.Get(Review.HospitalId));
        }
        public void Delete(int id)
        {
            Entities.Review R = (Entities.Review)Map.Entity(Get(id));

            _context.ChangeTracker.Clear();

            _context.Remove(R);

            // write changes to DB
            _context.SaveChanges();
        }
        public decimal Average(Review N)
        {
            decimal sum = 0;
            int i = 0;

            if (N.Accommodation == null)
                N.Accommodation = (Accommodation?)Map.Table(_context.Accommodations.Find(N.Id));
            if (N.Accommodation != null)
                sum += N.Accommodation.AverageA ?? 0;

            if (N.Cleanliness == null)
                N.Cleanliness = (Cleanliness?)Map.Table(_context.Cleanlinesses.Find(N.Id));
            if (N.Cleanliness != null)
                sum += N.Cleanliness.AverageCl ?? 0;

            if (N.Covid == null)
                N.Covid = (Covid?)Map.Table(_context.Covids.Find(N.Id));
            if (N.Covid != null)
                sum += N.Covid.AverageC ?? 0;

            if (N.Nursing == null)
                N.Nursing = (Nursing?)Map.Table(_context.Nursings.Find(N.Id));
            if (N.Nursing != null)
                sum += N.Nursing.AverageN ?? 0;


            if (N.Accommodation != null)
                i += N.Accommodation.AverageA != null ? 1 : 0;
            if (N.Cleanliness != null)
                i += N.Cleanliness.AverageCl != null ? 1 : 0;
            if (N.Covid != null)
                i += N.Covid.AverageC != null ? 1 : 0;
            if (N.Nursing != null)
                i += N.Nursing.AverageN != null ? 1 : 0;

            if (i == 0)
                return 0;
            else
                return (decimal)sum / i;
        }
    }
}
