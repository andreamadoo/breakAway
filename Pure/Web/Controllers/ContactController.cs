﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using BreakAway.Entities;
using BreakAway.Models.Contact;
using BreakAway.Services;



namespace BreakAway.Controllers
{

    public class ContactController : Controller
    {
        private readonly IRepository _repository;
        private readonly IFilterService _filterService;
        

        public ContactController(IRepository repository, IFilterService filterService)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }

            _filterService = filterService;

            _repository = repository;
        }


       
 
       


        [HttpGet]
        public ActionResult Index(string message, [Bind(Prefix = "Filter")]FilterModel filterModel)
        {
            

            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.message = message;
            }

            
      
            var viewModel = new IndexViewModel();

            viewModel.Contacts = (from contact in _repository.Contacts
                                  select new ContactItem
                                  {
                                      Id = contact.Id,
                                      FirstName = contact.FirstName,
                                      LastName = contact.LastName,
                                      AddDate = contact.AddDate,
                                      ModifiedDate = contact.ModifiedDate,

                                  }).ToArray();

            var filteredList = _filterService.FilterValidation(viewModel.Contacts.ToList(), filterModel);

            viewModel.Contacts = filteredList.ToArray();

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

            var contactAddresses = contact.Addresses.ToArray();
            foreach (var realAddress in contactAddresses)
            {
                var address = model.Addresses.FirstOrDefault(p => p.Id == realAddress.Id);

                Console.WriteLine(realAddress.Id);


                if (address == null)
                {
                    _repository.Addresses.Delete(realAddress);
                }
            }

            foreach (var addressModel in model.Addresses)
            {

                var address = contact.Addresses.FirstOrDefault(p => p.Id == addressModel.Id);

                if (address == null)
                {
                    address = new Address
                    {
                        Mail = new Mail()
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
            //ModelState.Clear();

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
            //ModelState.Clear();

            return RedirectToAction("index", "Contact", new { message = "Contact added successfully" });
        }




    }


}