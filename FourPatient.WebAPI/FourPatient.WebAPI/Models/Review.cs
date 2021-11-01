using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FourPatient.WebAPI.Models
{
    public class Review
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public decimal Comfort { get; set; }
        public DateTime? DatePosted { get; set; }
        public string Message { get; set; }
        [Required]
        public int HospitalId { get; set; }
        [Required]
        public int PatientId { get; set; }

        public virtual Accommodation? Accommodation { get; set; }
        public virtual Cleanliness? Cleanliness { get; set; }
        public virtual Covid? Covid { get; set; }
        public virtual Nursing Nursing { get; set; }
        public virtual Hospital? Hospital { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
