using FourPatient.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourPatient.Domain
{
    public interface IPatient
    {
        IEnumerable<Patient> GetAll();
        Patient Get(int id);
        void Create(Patient patient);
        void Update(Patient patient);
        void Delete(int id);
    }
}
