using System;
using System.Collections.Generic;

#nullable disable

namespace FourPatient.Domain.Tables
{
    public partial class Cleanliness
    {
        public int Id { get; set; }
        public int? WaitingRoom { get; set; }
        public int? WardRoom { get; set; }
        public int? Equipment { get; set; }
        public int? Bathroom { get; set; }
        public decimal? AverageCl { get; set; }

        public virtual Review Review { get; set; }
    }
}
