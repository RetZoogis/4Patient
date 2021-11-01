using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace FourPatient.Domain.Tables
{
    public partial class Patient
    {
        public Patient()
        {
            Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "State must be 2 characters")]
        public string State { get; set; }
        public DateTime? DoB { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int ZipCode { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
