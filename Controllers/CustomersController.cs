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

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new NewCustomerViewModel
            {
                MembershipTypes = membershipTypes
            };
            return View(viewModel);
        }
        public ViewResult Index()
        {
            //var customers = GetCustomers();
            //calling to list immediately queries db, otherwise it will 
            //run query when iterating over it, like in the view
            //eager loading means importing model and types with them so they can be rendered in the view
            // need to include MemebershipType = .Include is an dextension method from System.Data.Entity
            //so need to add using System.data.Entity
            var customers = _context.Customers.Include(c => c.MembershipType).ToList(); ;

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            //var customer = GetCustomers().SingleOrDefault(c => c.Id == id);
            // query will run immediately because of call to extension method
            //to include associated objects use this technique
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if(customer == null)
            {
                return HttpNotFound();
            }
            
            return View(customer);
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