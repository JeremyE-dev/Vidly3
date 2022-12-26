using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Vidly3.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        //deleted memebership type because including it will couple with domain class
        //to include this we create a new dto class called membership type
        //public MembershipType MembershipType { get; set; }
        //removed didplay attributes, because they were used in our forms
        // will  not be using because its an api
        
        public byte MembershipTypeId { get; set; }

        //[Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }
    }
}