using FourPatient.Domain.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourPatient.Domain
{
    public interface IHospital
    {
        IEnumerable<Hospital> GetAll();
        Hospital Get(int id);
        void Create(Hospital hospital);
        void Update(Hospital hospital);
        void Delete(int id);
        IEnumerable<Hospital> SearchName(string str);
        IEnumerable<Hospital> SearchZip(int zip);
        IEnumerable<Hospital> SearchCity(string str);
        IEnumerable<Hospital> SearchAddress(string str);
        IEnumerable<Hospital> SearchDepartments(string str);
        IEnumerable<Hospital> SearchState(string str);
    }
}
