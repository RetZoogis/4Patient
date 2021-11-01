using FourPatient.DataAccess.Entities;
using FourPatient.Domain;
using System.Collections.Generic;
using System.Linq;
using Cleanliness = FourPatient.Domain.Tables.Cleanliness;
using Review = FourPatient.Domain.Tables.Review;

// This Class hold access methods for data layer

namespace FourPatient.DataAccess
{
    public class CleanlinessRepo : ICleanliness
    {
        private readonly _4PatientContext _context;
        private readonly IReview _reviewrepo;

        public CleanlinessRepo(_4PatientContext context, IReview reviewrepo)
        {
            context.Database.EnsureCreated();
            _context = context;
            _reviewrepo = reviewrepo;
        }
        public IEnumerable<Cleanliness> GetAll()
        {
            ICollection<Entities.Cleanliness> List = _context.Cleanlinesses.ToList();
            ICollection<Cleanliness> N = List.Select(n => (Cleanliness)Map.Table(n)).ToList();

            foreach (var Cleanliness in N)
            {
                Cleanliness.Review = (Review)Map.Table(_context.Reviews.Find(Cleanliness.Id));
            };

            return N;
        }

        public Cleanliness Get(int id)
        {
            // The DbSet .Find() method searches DB based on primary key value
            var n = _context.Cleanlinesses.Find(id); // This Enumerable method also works .First(n => n.Id == id);
            Cleanliness N = (Cleanliness)Map.Table(n);

            N.Review = (Review)Map.Table(_context.Reviews.Find(N.Id));

            // This closes the DBContext entity tracker so the same entity can be queried again in the same unit of work
            //_context.Entry(n).State = EntityState.Detached;

            return N;
        }

        public void Create(Cleanliness N)
        {
            // Recalculate average score
            N.AverageCl = Average(N);

            // map to EF entity
            var entity = (Entities.Cleanliness)Map.Entity(N);

            _context.Cleanlinesses.Add(entity);

            // write changes to DB
            _context.SaveChanges();

            _reviewrepo.Update(_reviewrepo.Get(N.Id));
        }
        public void Update(Cleanliness N)
        {
            // Recalculate average score
            N.AverageCl = Average(N);

            // map to EF entity
            _context.Cleanlinesses.Update((Entities.Cleanliness)Map.Entity(N));

            // write changes to DB
            _context.SaveChanges();

            _reviewrepo.Update(_reviewrepo.Get(N.Id));
        }
        public void Delete(int id)
        {
            Entities.Cleanliness C = (Entities.Cleanliness)Map.Entity(Get(id));

            _context.ChangeTracker.Clear();

            _context.Remove(C);

            // write changes to DB
            _context.SaveChanges();
        }
        public decimal? Average(Cleanliness N)
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
