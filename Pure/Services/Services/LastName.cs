using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakAway.Services
{
    public class LastName : IFilter
    {
        public List<ContactItem> FilterSearch(List<ContactItem> ContactItem, FilterModel filterModel)
        {
            if (!string.IsNullOrEmpty(filterModel.LastName))
            {

                ContactItem = ContactItem.Where(i => i.LastName.Contains(filterModel.LastName)).ToList();

            }
            return ContactItem;
        }
    }
}
