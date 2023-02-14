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

            //var contacts = new List<ContactItem>();

            //foreach(var contact in _repository.Contacts)
            //{
            //    var c = new ContactItem
            //    {
            //        Id = contact.Id,
            //        FirstName = contact.FirstName,
            //        LastName = contact.LastName,
            //        AddDate = contact.AddDate,
            //        ModifiedDate = contact.ModifiedDate,

            //    };
            //    contacts.Add(c);
            //}

            //viewModel.Contacts = contacts.ToArray();


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

            var addresses = new List<AddressModel>();

            foreach (var address in contact.Addresses)
            {
                var addressModel = new AddressModel
                {
                    Id = address.Id,
                    PostalCode = address.PostalCode,
                    Street1 = address.Mail.Street1,
                    Street2 = address.Mail.Street2,
                    City = address.Mail.City,
                    StateProvince = address.Mail.StateProvince,
                    CountryRegion = address.CountryRegion,
                    AddressType = address.AddressType
                };
               
                addresses.Add(addressModel);
            }

            viewModel.Addresses = addresses;

           

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

            var contact = _repository.Contacts.FirstOrDefault(p => p.Id == model.Id);

            if (contact == null)
            {
                return RedirectToAction("Index", "Contact", new { message = "Contact with id '" + model.Id + "' was not found" });
            }

            contact.FirstName = model.FirstName;
            contact.LastName = model.LastName;
            contact.AddDate = model.AddDate;
            contact.ModifiedDate = DateTime.Now;



            foreach (var addressModel in model.Addresses)
            {

                var address = contact.Addresses.FirstOrDefault(p => p.Id == addressModel.Id);

                if (address == null)
                {
                    address = new Address
                    {
                        Mail = new Mail ()
                    };

                    contact.Addresses.Add(address);
                }

                address.Mail.Street1 = addressModel.Street1;
                address.Mail.Street2 = addressModel.Street2;
                address.Mail.City = addressModel.City;
                address.PostalCode = addressModel.PostalCode;
                address.CountryRegion = addressModel.CountryRegion;
                address.Mail.StateProvince = addressModel.StateProvince;
                address.AddressType = addressModel.AddressType;
                address.ModifiedDate = DateTime.Now;

                if (address.AddressType == null)
                {
                    return RedirectToAction("Edit", "Contact", new { message = " The Address Type is required!" });
                }

            }

            

            _repository.Save();

            return RedirectToAction("Edit", "Contact", new { id = contact.Id, message = "Changes saved successfully" });
        }

        public ActionResult Add(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.message = message;
            }

            var model = new AddViewModel();

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
                AddDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
            };


            
            _repository.Contacts.Add(contact);
            _repository.Save();

            return RedirectToAction("index", "Contact", new { message = "Contact added successfully" });
        }




    }

   
}