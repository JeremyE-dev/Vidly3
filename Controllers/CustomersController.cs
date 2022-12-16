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
                MembershipTypes = membershipTypes
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Customer customer) //this action is called when form is posted
        {
            //adding customer to a database
            //1// add to dbcontext - when you add this top the context its not written to the db
            //   its just in the memory our db context has a change tracking mechanism
            // anytime you add an obj to it or modify or remove one ofits existing objects
            // it will marke them as added modified or deleted
            //
            _context.Customers.Add(customer);
            //to save call context.save changes
            //at this poinht our dbcontext goes through all modified objects and 
            //based on teh kind of modification it will generate sql statements at runtime
            //then it will run themon eth db
            //all these changes are wrapped in a transactions
            //so either all chnages will be persisten together or nothing will
            _context.SaveChanges();
            //redirect to list of customers

          
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