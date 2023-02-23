using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakAway.Services
{
    public class FirstName : IFilter
    {
        //seperate methdos
        public List<ContactItem> FilterSearch(List<ContactItem> contactItem, FilterModel filterModel)
        {
            if (contactItem == null)
            {
                throw new ArgumentNullException();
            }

            if (filterModel == null)
            {
                return contactItem;
            }
            

            contactItem = contactItem.Where(i => i.FirstName.Contains(filterModel.FirstName)).ToList();

            
            return contactItem;
        }

        public bool FilterCheck(FilterModel filterModel)
        {
          
            if (filterModel != null)
            {
                return (!string.IsNullOrEmpty(filterModel.FirstName));
            }
            return false;

        }

    }
}
