using System;
using System.Collections.Generic;

#nullable disable

namespace FourPatient.Domain.Tables
{
    public partial class Covid
    {
        public int Id { get; set; }
        public int? WaitingRooms { get; set; }
        public int? Protocols { get; set; }
        public int? Separation { get; set; }
        public int? Safety { get; set; }
        public bool? Covid1 { get; set; }
        public int? Screening { get; set; }
        public int? Treatment { get; set; }
        public decimal? AverageC { get; set; }

        public virtual Review Review { get; set; }
    }
}
