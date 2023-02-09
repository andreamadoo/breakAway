using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using BreakAway.Entities;
using BreakAway.Models.Contact;

namespace BreakAway.Controllers
{

    public class ContactController : Controller
    {
        private readonly IRepository _repository;

        public ContactController(IRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            _repository = repository;
        }

        [HttpGet]
        public ActionResult Index(string message, string firstName, string lastName, string addDate, string modifiedDate)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.message = message;
            }

            var viewModel = new IndexViewModel();

           
            viewModel.FirstName = firstName;

            viewModel.LastName = lastName;


            viewModel.AddDate = addDate;

            
            viewModel.ModifiedDate = modifiedDate;





            viewModel.Contacts = (from contact in _repository.Contacts
                                  select new ContactItem
                                  {
                                     
                                      FirstName = contact.FirstName,
                                      LastName = contact.LastName,
                                      AddDate = contact.AddDate,
                                      ModifiedDate = contact.ModifiedDate,
                                      
                                  }).ToArray();


            if (!string.IsNullOrEmpty(firstName))
            {

                viewModel.Contacts = viewModel.Contacts.Where(i => i.FirstName.Contains(firstName)).ToArray();
                
            }

            if (!string.IsNullOrEmpty(lastName))
            {

                viewModel.Contacts = viewModel.Contacts.Where(i => i.LastName.Contains(lastName)).ToArray();

            }

            if (!string.IsNullOrEmpty(addDate))
            {

                viewModel.Contacts = viewModel.Contacts.Where(i => i.AddDate.ToString().Contains(addDate)).ToArray();

            }

            if (!string.IsNullOrEmpty(modifiedDate))
            {

                viewModel.Contacts = viewModel.Contacts.Where(i => i.ModifiedDate.ToString().Contains(modifiedDate)).ToArray();

            }




            return View(viewModel);
        }

        
    }

   
}