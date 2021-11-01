using FourPatient.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourPatient.Domain
{
    public interface ICleanliness
    {
        IEnumerable<Cleanliness> GetAll();
        Cleanliness Get(int id);
        void Create(Cleanliness survey);
        void Update(Cleanliness survey);
        void Delete(int id);
    }
}
