using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly3.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //remove default logic
            // return base.IsValid(value, validationContext);

            // Implement custome logic

            //.ObjectInstance gives us access to the containing class(teh class thatuses teh attribute)
            //- in this case customer

            //because this is an aobject we need to cast it

            var customer = (Customer)validationContext.ObjectInstance;

            //now we can check the selected type
            // if 0 means that membership type was not selected so do not display error
            if (customer.MembershipTypeId == 0 ||customer.MembershipTypeId == 1) //this is Pay As You go- do not care about birthdate
            {
                return ValidationResult.Success; //Success is a static fields on the ValidatiopnResult class
            }
            
            if (customer.Birthdate == null)
            {
                return new ValidationResult("Birthdate is Required.");
            }

            //get the age
            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            //return the message

            return (age >= 18) ? ValidationResult.Success 
                : new ValidationResult("Customer should be at least 18 Years old to go on a membership.");

        }
    }
}