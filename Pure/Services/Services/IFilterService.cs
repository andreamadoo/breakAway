using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakAway.Services
{
    public interface IFilterService 
    {

        List<ContactItem> FilterValidation(List<ContactItem> contactItem, string firstName, string lastName, string addDate, string modifiedDate);


    }
}
