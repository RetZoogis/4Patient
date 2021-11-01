using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FourPatient.WebAPI.Models
{
    public class Cleanliness
    {
        [Required]
        public int Id { get; set; }
        public int? WaitingRoom { get; set; }
        public int? WardRoom { get; set; }
        public int? Equipment { get; set; }
        public int? Bathroom { get; set; }
        public decimal? AverageCl { get; set; }
        public virtual Review Review { get; set; }
    }
}
