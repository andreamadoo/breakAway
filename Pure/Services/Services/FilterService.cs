
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakAway.Services
{
    public class FilterService : IFilterService
    {

        private readonly IFilter[] filters;

        public FilterService(IFilter[] filter)
        {

            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }

            filters = filter;


       
        }


      
        public List<ContactItem> FilterValidation(List<ContactItem> contactItem, FilterModel filterModel)
        {

            foreach (var filter in filters)
            {
                if (filter.FilterCheck(filterModel))
                {
                    contactItem = filter.FilterSearch(contactItem, filterModel);
                }

            }

            return contactItem;
        }

    }


}
