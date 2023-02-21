using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakAway.Services
{
    public class AddDate : IFilter
    {
        public List<ContactItem> FilterSearch(List<ContactItem> contactItem, FilterModel filterModel)
        {
            

            contactItem = contactItem.Where(i => i.AddDate.ToString().Contains(filterModel.AddDate)).ToList();

            
            return contactItem;
        }

        public bool FilterCheck(FilterModel filterModel)
        {
            if (filterModel != null)
            {
                return (!string.IsNullOrEmpty(filterModel.AddDate));
            }
            return false;
        }
    }
}
