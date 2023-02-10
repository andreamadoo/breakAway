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
                                      Id = contact.Id,
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

        public ActionResult Edit(int id, string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.message = message;
            }

            var contact = _repository.Contacts.FirstOrDefault(p => p.Id == id);

            if (contact == null)
            {
                return RedirectToAction("Index", "Contact");
            }

            var viewModel = new EditViewModel
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                AddDate = contact.AddDate,
                ModifiedDate = contact.ModifiedDate
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", "Contact", model);
            }

            var contact = _repository.Customers.FirstOrDefault(p => p.Id == model.Id);

            if (contact == null)
            {
                return RedirectToAction("Index", "Contact", new { message = "Contact with id '" + model.Id + "' was not found" });
            }

            contact.FirstName = model.FirstName;
            contact.LastName = model.LastName;
            contact.AddDate = model.AddDate;
            contact.ModifiedDate = model.ModifiedDate;
            

            _repository.Save();

            return RedirectToAction("Edit", "Contact", new { id = contact.Id, message = "Changes saved successfully" });
        }

        public ActionResult Add(string message,AddViewModel model)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.message = message;
            }


            return View(model);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Add", "Contact", new { message = "Contact not created" });
            }

            var contact = new Contact
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                AddDate = model.AddDate,
                ModifiedDate = model.ModifiedDate,
            };

            _repository.Contacts.Add(contact);
            _repository.Save();

            return RedirectToAction("index", "Contact", new { message = "Contact added successfully" });
        }



    }

   
}