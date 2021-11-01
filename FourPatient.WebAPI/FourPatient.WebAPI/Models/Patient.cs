using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FourPatient.WebAPI.Models
{
    public class Patient
    {
        public Patient()
        {
            Reviews = new HashSet<Review>();
        }
        [Required]
        public int Id { get; set; }
       
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
     
        public string Password { get; set; }

        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "State must be 2 characters")]
        public string State { get; set; }
       
        [DataType(DataType.Date, ErrorMessage = "Please enter date in correct form")]
        public DateTime? DoB { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int ZipCode { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
