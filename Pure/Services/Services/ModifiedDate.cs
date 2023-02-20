using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakAway.Services
{
    public class ModifiedDate : IFilter
    {
        public List<ContactItem> FilterSearch(List<ContactItem> contactItem, FilterModel filterModeldDate)
        {
            if (!string.IsNullOrEmpty(filterModeldDate.ModifiedDate))
            {

                contactItem = contactItem.Where(i => i.ModifiedDate.ToString().Contains(filterModeldDate.ModifiedDate)).ToList();

            }
            return contactItem;
        }
    }
}
