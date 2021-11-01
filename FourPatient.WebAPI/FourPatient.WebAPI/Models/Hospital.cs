using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace FourPatient.WebAPI.Models
{
    public class Hospital
    {
        public Hospital()
        {
            Reviews = new HashSet<Review>();
        }
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public int ZipCode { get; set; }
        [Required]
        public decimal Comfort { get; set; }
        [Required]
        public decimal Nursing { get; set; }
        [Required]
        public decimal Accomodations { get; set; }
        [Required]
        public decimal Cleanliness { get; set; }
        [Required]
        public decimal Covid { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Departments { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
