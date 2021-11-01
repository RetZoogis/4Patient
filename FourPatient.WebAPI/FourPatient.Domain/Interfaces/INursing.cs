using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourPatient.Domain.Tables;

namespace FourPatient.Domain
{
    public interface INursing
    {
        IEnumerable<Nursing> GetAll();
        Nursing Get(int id);
        void Create(Nursing survey);
        void Update(Nursing survey);
        void Delete(int id);
    }
}
