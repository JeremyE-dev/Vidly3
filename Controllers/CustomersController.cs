using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly3.Models;
using System.Data.Entity;
using Vidly3.ViewModels;

namespace Vidly3.Controllers
{
    public class CustomersController : Controller
    {

        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New() // this is to poulate the form
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(), //adding this field here initializes the cusomer object with defail values, its an int which is 1,
                                           //since it has a value no more error message in summaryyyyyyyyyyyyyyyyy 
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm",viewModel);
        }

        [HttpPost]
        //when asp.net mvc populates this customer object
        //using request data it checks to see if this object is valid
        //based on teh data annotations applied on various properties of this customer 
        //class 
        // 
        public ActionResult Save(Customer customer) //this action is called when form is posted
        {// at this point in the controller we can use model state  property to get access to
            // validation data

            //if model state is not valid return the save view
            // the view that contains the customer form
            //otherwise we either add or update the customer 

            if(!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes
                };
                return View("CustomerForm", viewModel);
            }

            //if this is a new customer then create it
            if (customer.Id == 0)
            {
                _context.Customers.Add(customer);
            } else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                //instead of using tryUpdateModel , which update's all properties 
                // manually setthem instead to avoid security vulnerability

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
          
            _context.SaveChanges();
            
            return RedirectToAction("Index", "Customers");
        }
        public ViewResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList(); ;

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if(customer == null)
            {
                return HttpNotFound();
            }
            
            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }



        //private IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer {Id = 1, Name = "John Smith" },
        //        new Customer {Id = 2, Name = "Mary Williams" }
        //    };
        //}
    }
}