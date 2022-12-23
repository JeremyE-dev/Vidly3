using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly3.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please enter customer's name.")]
        [StringLength(255)]
        public string Name { get; set; } 
        
        public bool IsSubscribedToNewsletter { get; set; }
        
        public MembershipType MembershipType { get; set; }
        
        [Display(Name = "Membership Type")]
        //note: this is implicitily required because its of type byte
        //if it wa snullable it woudl not be requred ()i.e. with a question Mark 
        public byte MembershipTypeId { get; set; }

        [Display(Name = "Date of Birth")]
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }
    }
}