using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly3.Dtos;
using Vidly3.Models;

namespace Vidly3.Controllers.Api
{
  
    
    public class CustomersController : ApiController
    {
       
        //1.) set up db context so we can get dat afrom it
        private ApplicationDbContext _context;

        public CustomersController()
        {
            //initialize in constructor
            _context = new ApplicationDbContext();
        }

        // by convention in asp.net web api thsi action will respond to 
        //GET /api/customers
        public IHttpActionResult GetCustomers()
        {
          
            var customerDtos = _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }
        // by convention in asp.net web api thsi action will respond to 
        //GET /api/customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                //this method takes an enumeration that specifies the type of error
                // this is Part of the restful cpnvention
                // if resource is not NotFound we return the not found error
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return NotFound();
            }

            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        //POST /api/customers
        // we post a customer to a customers collection
        //by convention when we create a resource we return teh newly created resource to teh client
        //because that esource wull generally have an id created by the server

        [HttpPost] //mark with http post so will only be called if we sent http post request
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            //validate object
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            //Map the DTO back to domain object
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            // teh call to add returns an ID, so we need to add that to
            // our customer object

            customerDto.Id = customer.Id;

            //return URI (Unified resource Identifier) of created customer
            //example /api/customer/10)

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        //PUT /api/Customers/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            //validate object
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            //check if customer id is valid

            if (customerInDb == null)
            {
                return NotFound();
            }

            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);
            //customerInDb.Name = customerDto.Name;
            //customerInDb.Birthdate = customerDto.Birthdate;
            //customerInDb.IsSubscribedToNewsletter = customerDto.IsSubscribedToNewsletter;
            //customerInDb.MembershipTypeId = customerDto.MembershipTypeId;

            _context.SaveChanges();
            return Ok();
        }

        //Delete /api/customers/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            //check if customer id is valid

            if (customerInDb == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok();
            
        }



     
    }
}
