
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

        public FilterService()
        {
            filters = new IFilter[]
            {
                new FirstName(),
                new LastName(),
                new AddDate(),
                new ModifiedDate()
            };
        }

        //MAKE THIS SOLID 
        public List<ContactItem> FilterValidation(List<ContactItem> contactItem, FilterModel filterModeldDate)
        {



           

            foreach (var filter in filters)
            {
               
               contactItem =  filter.FilterSearch(contactItem,filterModeldDate);
            }

            return contactItem;



        }

    }


}
