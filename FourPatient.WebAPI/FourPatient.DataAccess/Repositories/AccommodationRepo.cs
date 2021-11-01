using System.Collections.Generic;
using System.Linq;
using FourPatient.DataAccess.Entities;
using FourPatient.Domain;
using Accommodation = FourPatient.Domain.Tables.Accommodation;
using Review = FourPatient.Domain.Tables.Review;

// This Class hold access methods for data layer

namespace FourPatient.DataAccess
{
    public class AccommodationRepo : IAccommodation
    {
        private readonly _4PatientContext _context;
        private readonly IReview _reviewrepo;

        public AccommodationRepo(_4PatientContext context, IReview reviewrepo)
        {
            context.Database.EnsureCreated();
            _context = context;
            _reviewrepo = reviewrepo;
        }
        public IEnumerable<Accommodation> GetAll()
        {
            ICollection<Entities.Accommodation> List = _context.Accommodations.ToList();
            ICollection<Accommodation> N = List.Select(n => (Accommodation)Map.Table(n)).ToList();

            foreach (var Accommodation in N)
            {
                Accommodation.Review = (Review)Map.Table(_context.Reviews.Find(Accommodation.Id));
            };

            return N;
        }

        public Accommodation Get(int id)
        {
            // The DbSet .Find() method searches DB based on primary key value
            var n = _context.Accommodations.Find(id); // This Enumerable method also works .First(n => n.Id == id);
            Accommodation N = (Accommodation)Map.Table(n);

            N.Review = (Review)Map.Table(_context.Reviews.Find(N.Id));

            // This closes the DBContext entity tracker so the same entity can be queried again in the same unit of work
            //_context.Entry(n).State = EntityState.Detached;

            return N;
        }

        public void Create(Accommodation N)
        {
            // Recalculate average score
            N.AverageA = Average(N);

            // map to EF entity
            var entity = (Entities.Accommodation)Map.Entity(N);

            _context.Accommodations.Add(entity);

            // write changes to DB
            _context.SaveChanges();

            _reviewrepo.Update(_reviewrepo.Get(N.Id));
        }
        public void Update(Accommodation N)
        {
            // Recalculate average score
            N.AverageA = Average(N);

            // map to EF entity
            _context.Accommodations.Update((Entities.Accommodation)Map.Entity(N));

            // write changes to DB
            _context.SaveChanges();

            _reviewrepo.Update(_reviewrepo.Get(N.Id));
        }
        public void Delete(int id)
        {
            Entities.Accommodation A = (Entities.Accommodation)Map.Entity(Get(id));

            _context.ChangeTracker.Clear();

            _context.Remove(A);

            // write changes to DB
            _context.SaveChanges();
        }
        public decimal? Average(Accommodation N)
        {
            int sum = 0;
            int i = 0;

            foreach (var prop in N.GetType().GetProperties())
            {
                //This selects all properties of type int? since all the survey questions are of type int?
                //Additional survey questions can be added without updating this code
                if (prop.PropertyType == typeof(int?))
                {
                    sum += (int?)prop.GetValue(N, null) ?? 0;
                    i += (int?)prop.GetValue(N, null) != null ? 1 : 0;
                }
            }

            if (i == 0)
                return null;
            else
                return (decimal)sum / i;
        }
    }
}
