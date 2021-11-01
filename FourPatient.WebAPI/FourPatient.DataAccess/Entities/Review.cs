using System;
using System.Collections.Generic;

#nullable disable

// This Class holds data layer objects, either from Business layer or SQL Server

namespace FourPatient.DataAccess.Entities
{
    public partial class Review
    {
       // Primitive properties
       // Data type? = Nullable
        public int Id { get; set; }
        public int PatientId { get; set; }
        public decimal Comfort { get; set; }
        public DateTime? DatePosted { get; set; }
        public string Message { get; set; }
        public int HospitalId { get; set; }

        // Object properties
        public virtual Accommodation? Accommodation { get; set; }
        public virtual Cleanliness? Cleanliness { get; set; }
        public virtual Nursing? Nursing { get; set; }
        public virtual Hospital Hospital { get; set; }
        public virtual Covid? Covid { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
