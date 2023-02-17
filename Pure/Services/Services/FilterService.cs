
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakAway.Services
{
    public class FilterService : IFilterService
    {

        //MAKE THIS SOLID 
        public List<ContactItem> FilterValidation(List<ContactItem> contactItem, string firstName, string lastName, string addDate, string modifiedDate)
        {

            if (!string.IsNullOrEmpty(firstName))
            {

                contactItem = contactItem.Where(i => i.FirstName.Contains(firstName)).ToList();

            }

            if (!string.IsNullOrEmpty(lastName))
            {

                contactItem = contactItem.Where(i => i.LastName.Contains(lastName)).ToList();

            }

            if (!string.IsNullOrEmpty(addDate))
            {

                contactItem = contactItem.Where(i => i.AddDate.ToString().Contains(addDate)).ToList();

            }

            if (!string.IsNullOrEmpty(modifiedDate))
            {

                contactItem = contactItem.Where(i => i.ModifiedDate.ToString().Contains(modifiedDate)).ToList();

            }

            return contactItem;



        }

    }
}
