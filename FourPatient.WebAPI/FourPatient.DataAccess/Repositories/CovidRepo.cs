using FourPatient.DataAccess.Entities;
using FourPatient.Domain;
using System.Collections.Generic;
using System.Linq;
using Covid = FourPatient.Domain.Tables.Covid;
using Review = FourPatient.Domain.Tables.Review;

// This Class hold access methods for data layer

namespace FourPatient.DataAccess
{
    public class CovidRepo : ICovid
    {
        private readonly _4PatientContext _context;
        private readonly IReview _reviewrepo;

        public CovidRepo(_4PatientContext context, IReview reviewrepo)
        {
            context.Database.EnsureCreated();
            _context = context;
            _reviewrepo = reviewrepo;
        }
        public IEnumerable<Covid> GetAll()
        {
            ICollection<Entities.Covid> List = _context.Covids.ToList();
            ICollection<Covid> N = List.Select(n => (Covid)Map.Table(n)).ToList();

            foreach (var Covid in N)
            {
                Covid.Review = _reviewrepo.Get(Covid.Id);
                //Covid.Review = (Review)Map.Table(_context.Reviews.Find(Covid.Id));
            };

            return N;
        }

        public Covid Get(int id)
        {
            // The DbSet .Find() method searches DB based on primary key value
            var n = _context.Covids.Find(id); // This Enumerable method also works .First(n => n.Id == id);
            Covid N = (Covid)Map.Table(n);

            N.Review = _reviewrepo.Get(N.Id);
            //N.Review = (Review)Map.Table(_context.Reviews.Find(N.Id));

            return N;
        }

        public void Create(Covid N)
        {
            // Recalculate average score
            N.AverageC = Average(N);

            // map to EF entity
            var entity = (Entities.Covid)Map.Entity(N);

            _context.Covids.Add(entity);

            // write changes to DB
            _context.SaveChanges();

            _reviewrepo.Update(_reviewrepo.Get(N.Id));
        }
        public void Update(Covid N)
        {
            // Recalculate average score
            N.AverageC = Average(N);

            // map to EF entity
            _context.Covids.Update((Entities.Covid)Map.Entity(N));

            // write changes to DB
            _context.SaveChanges();

            _reviewrepo.Update(_reviewrepo.Get(N.Id));
        }
        public void Delete(int id)
        {
            Entities.Covid C = (Entities.Covid)Map.Entity(Get(id));

            _context.ChangeTracker.Clear();

            _context.Remove(C);

            // write changes to DB
            _context.SaveChanges();
        }
        public decimal? Average(Covid N)
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
