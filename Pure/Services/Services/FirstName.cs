using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakAway.Services
{
    public class FirstName : IFilter
    {

        public List<ContactItem> FilterSearch(List<ContactItem> ContactItem, FilterModel filterModel)
        {
            if (!string.IsNullOrEmpty(filterModel.FirstName))
            {

                ContactItem = ContactItem.Where(i => i.FirstName.Contains(filterModel.FirstName)).ToList();

            }
            return ContactItem;
        }

    }
}
