using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Hold UI objects, either from user or BL

namespace FourPatient.WebAPI.Models
{
    public class Accommodation
    {
        [Required]
        public int Id { get; set; }
        public int? CheckIn { get; set; }
        public int? Discharge { get; set; }
        public int? Equipment { get; set; }
        public int? Policy { get; set; }
        public int? Privacy { get; set; }
        public int? Room { get; set; }
        public int? FoodOptions { get; set; }
        public int? FoodQuality { get; set; }
        public int? DietOptions { get; set; }
        public int? Accessibility { get; set; }
        public int? Parking { get; set; }
        public decimal? AverageA { get; set; }
        public virtual Review Review { get; set; }
    }
}
