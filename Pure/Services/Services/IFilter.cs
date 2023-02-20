using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreakAway.Services
{
    public interface IFilter
    {

        List<ContactItem> FilterSearch(List<ContactItem> ContactItem, FilterModel filterModel);

    }
}
