using FourPatient.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourPatient.Domain
{
    public interface ICovid
    {
        IEnumerable<Covid> GetAll();
        Covid Get(int id);
        void Create(Covid survey);
        void Update(Covid survey);
        void Delete(int id);
    }
}
