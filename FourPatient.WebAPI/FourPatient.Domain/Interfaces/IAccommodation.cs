using FourPatient.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Expose DL repositories to UI

namespace FourPatient.Domain
{
    public interface IAccommodation
    {
        IEnumerable<Accommodation> GetAll();
        Accommodation Get(int id);
        void Create(Accommodation survey);
        void Update(Accommodation survey);
        void Delete(int id);
    }
}
