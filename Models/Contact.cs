using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Models 
{
    public class Contact : IdentityUser
    {
        

        [Required]
        [StringLength(50, ErrorMessage = "the {0} must be at least {2}) and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "the {0} must be at least {2}) and at max {1} characters long.", MinimumLength = 1)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "A valid e-mail is required.")]
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string PersonalEmail { get; set; }

       
        [EmailAddress]
        [Display(Name = "E-mail")]
        public string WorkEmail { get; set; }


        [Required(ErrorMessage = "A valid phone number is required.")]
        [Phone]
        [Display(Name = "Phone Number")]
        public string MobileNum { get; set; }
    
        [Phone]
        [Display(Name = "Home Number(optional)")]
        public string HomeNum { get; set; }
      
        [Phone]
        [Display(Name = "Work Number(optional)")]
        public string WorkNum { get; set; }


        [Required(ErrorMessage = "An address is required.")]
        [StringLength(100, ErrorMessage = "the {0} must be at least {2}) and at max {1} characters long.", MinimumLength = 10)]
        [Display(Name = "Address")]
        public string Address1 { get; set; }

        
        [StringLength(100, ErrorMessage = "the {0} must be at least {2}) and at max {1} characters long.", MinimumLength = 10)]
        [Display(Name = "Secondary Address(optional)")]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "A City is required.")]
        [StringLength(70, ErrorMessage = "the {0} must be at least {2}) and at max {1} characters long.", MinimumLength = 2)]
        public string City { get; set; }
       
        [Required(ErrorMessage = "A State is required.")]
        [StringLength(70, ErrorMessage = "the {0} must be at least {2}) and at max {1} characters long.", MinimumLength = 2)]
        public string State { get; set; }

        [Required(ErrorMessage = "A Zip Code is required.")]
        [StringLength(10, ErrorMessage = "the {0} must be at least {2}) and at max {1} characters long.", MinimumLength = 5)]
        [Display(Name = "Zip Code")]
        public string Zip { get; set; }

        [StringLength(80, ErrorMessage = "the {0} must be at least {2}) and at max {1} characters long.", MinimumLength = 3)]
        [Display(Name = "Zipcode")]
        public string Company{ get; set; }

        

    }
