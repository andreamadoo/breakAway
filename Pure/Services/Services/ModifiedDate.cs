using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakAway.Services
{
    public class ModifiedDate : IFilter
    {
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

            contactItem = contactItem.Where(i => i.ModifiedDate.ToString().Contains(filterModel.ModifiedDate)).ToList();

            
            return contactItem;
        }

        public bool FilterCheck(FilterModel filterModel)
        {
            if (filterModel != null)
            {
                return (!string.IsNullOrEmpty(filterModel.ModifiedDate));
            }
            return false;

        }
    }
}
