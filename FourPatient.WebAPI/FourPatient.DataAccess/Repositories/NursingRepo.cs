using System.Collections.Generic;
using System.Linq;
using FourPatient.DataAccess;
using FourPatient.DataAccess.Entities;
using FourPatient.Domain;
using Nursing = FourPatient.Domain.Tables.Nursing;
using Review = FourPatient.Domain.Tables.Review;

// This Class hold access methods for data layer

namespace FourPatient.DataAccess
{
    public class NursingRepo : INursing
    {
        private readonly _4PatientContext _context;
        private readonly IReview _reviewrepo;

        public NursingRepo(_4PatientContext context, IReview reviewrepo)
        {
            context.Database.EnsureCreated();
            _context = context;
            _reviewrepo = reviewrepo;
        }
        public IEnumerable<Nursing> GetAll()
        {
            ICollection<Entities.Nursing> List = _context.Nursings.ToList();
            ICollection<Nursing> N = List.Select(n => (Nursing)Map.Table(n)).ToList();

            foreach (var Nursing in N)
            {
                //var r = _context.Nursings.Find(Nursing.Id);
                //Nursing.Review = (Review)Map.Table(r.Review);
                //Nursing.Review = _reviewrepo.Get(Nursing.Id);
                Nursing.Review = (Review)Map.Table(_context.Reviews.Find(Nursing.Id));
            };

            return N;
        }

        public Nursing Get(int id)
        {
            // The DbSet .Find() method searches DB based on primary key value
            var n = _context.Nursings.Find(id); // This Enumerable method also works .First(n => n.Id == id);
            Nursing N = (Nursing)Map.Table(n);

            //N.Review = _reviewrepo.Get(N.Id);
            N.Review = (Review)Map.Table(_context.Reviews.Find(N.Id));

            return N;
        }

        public void Create(Nursing N)
        {
            // Recalculate average score
            N.AverageN = Average(N);

            // map to EF entity
            var entity = (Entities.Nursing)Map.Entity(N);

            _context.Nursings.Add(entity);

            // write changes to DB
            _context.SaveChanges();

            _reviewrepo.Update(_reviewrepo.Get(N.Id));
        }
        public void Update(Nursing N)
        {
            // Recalculate average score
            N.AverageN = Average(N);

            // map to EF entity
            _context.Nursings.Update((Entities.Nursing)Map.Entity(N));

            // write changes to DB
            _context.SaveChanges();

            _reviewrepo.Update(_reviewrepo.Get(N.Id));
        }
        public void Delete(int id)
        {
            Entities.Nursing N = (Entities.Nursing)Map.Entity(Get(id));

            _context.ChangeTracker.Clear();

            _context.Remove(N);

            // write changes to DB
            _context.SaveChanges();
        }
        public decimal? Average(Nursing N)
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
