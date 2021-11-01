using System;
using System.Collections.Generic;

#nullable disable

namespace FourPatient.Domain.Tables
{
    public partial class Hospital
    {
        public Hospital()
        {
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public decimal Comfort { get; set; }
        public decimal Nursing { get; set; }
        public decimal Accomodations { get; set; }
        public decimal Cleanliness { get; set; }
        public decimal Covid { get; set; }
        public string Description { get; set; }
        public string Departments { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
