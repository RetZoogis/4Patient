using System;
using System.Collections.Generic;

#nullable disable

// Hold data layer objects, either from Business layer or SQL Server

namespace FourPatient.DataAccess.Entities
{
    public partial class Hospital
    {
        public Hospital()
        {
            Reviews = new HashSet<Review>();
        }
        // Primitive properties
        // Data type? = Nullable
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string Description { get; set; }
        public string Departments { get; set; }
        public decimal Comfort { get; set; }
        public decimal Accomodations { get; set; }
        public decimal Cleanliness { get; set; }
        public decimal Covid { get; set; }
        public decimal Nursing { get; set; }

        // List property of associated objects
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
