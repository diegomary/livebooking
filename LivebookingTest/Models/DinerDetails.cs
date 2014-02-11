using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LivebookingTest.Models
{
    [Serializable]
    public class DinerDetails
    {
        public static  int[] Coversnum = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16};
        [Required(ErrorMessage = "Please insert the Customer's first name")]
        [StringLength(25, ErrorMessage = "Max 60 chars")]
        [Display(Name = "First name")]        
        public string FirstName{get;set;}

        [Required(ErrorMessage = "Please insert the Customer's last name")]
        [StringLength(40, ErrorMessage = "Max 60 chars")]
        [Display(Name = "Last name")]   
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please insert the dining date")]
        [Display(Name = "Date of booking")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Text)]
        public string DiningDate { get; set; }       

        [Required(ErrorMessage = "Please insert the number of covers")]
        [Range(0, 16, ErrorMessage = "Can only be between 0 .. 16")]
        [Display(Name = "Covers")]      
        public int Covers { get; set; }

        [Required(ErrorMessage = "Please insert the customer's Phone number")]      
        [Display(Name = "Phone number")]      
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddressAttribute(ErrorMessage = "Please enter a valid email address")]
        [Required(ErrorMessage = "Email")]
        [Display(Name = "Email")]       
        public string Email { get; set; }    
       
        [Display(Name = "Diner status")]
        public string DinerStatus { get; set; }
        public DinerDetails() { DinerStatus = ""; }
       
        public static class DinerStatusViewModel
        {
            public class Option
            {
                public string Value { get; set; }
            }
            public static IEnumerable<Option> DinerOptions = new List<Option>
            {
                new Option {Value=""},
                new Option {Value = "Not Arrived"},
                new Option {Value= "Seated"}                                                             
            };           

        }

    }
}