using BreakAway.Models.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakAway.Services
{
    public interface IFilterService
    {

        void FilterValidation(IndexViewModel viewModel, string firstName, string lastName, string addDate, string modifiedDate);

        void KeepSearchFilters(IndexViewModel viewModel, string firstName, string lastName, string addDate, string modifiedDate);
    }
}
