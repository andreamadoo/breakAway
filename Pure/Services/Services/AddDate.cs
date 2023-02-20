using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakAway.Services
{
    public class AddDate : IFilter
    {
        public List<ContactItem> FilterSearch(List<ContactItem> contactItem, FilterModel filterModeldDate)
        {
            if (!string.IsNullOrEmpty(filterModeldDate.AddDate))
            {

                contactItem = contactItem.Where(i => i.AddDate.ToString().Contains(filterModeldDate.AddDate)).ToList();

            }
            return contactItem;
        }
    }
}
