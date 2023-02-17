using BreakAway.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakAway.Services
{ 

    public class FilterService : IFilterService
    {
        public void FilterValidation(IndexViewModel viewModel, string firstName, string lastName, string addDate, string modifiedDate)
        {

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



        }

        public void KeepSearchFilters(IndexViewModel viewModel, string firstName, string lastName, string addDate, string modifiedDate)
        {

            viewModel.FirstName = firstName;

            viewModel.LastName = lastName;

            viewModel.AddDate = addDate;

            viewModel.ModifiedDate = modifiedDate;

        }
    }
}
