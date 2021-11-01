using System;
using System.Collections.Generic;

#nullable disable

// Hold data layer objects, either from Business layer or SQL Server

namespace FourPatient.DataAccess.Entities
{
    public partial class Cleanliness
    {
        // Primitive properties
        // Data type? = Nullable
        public int Id { get; set; }
        public int? WaitingRoom { get; set; }
        public int? WardRoom { get; set; }
        public int? Equipment { get; set; }
        public int? Bathroom { get; set; }
        public decimal? AverageCl { get; set; }

        // Objects property
        public virtual Review Review { get; set; }
    }
}
