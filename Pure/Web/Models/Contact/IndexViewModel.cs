using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BreakAway.Entities;
using BreakAway.Services;

namespace BreakAway.Models.Contact
{
    public class IndexViewModel
    {
        public ContactItem[] Contacts { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddDate { get; set; }
        public string ModifiedDate { get; set; }
    }

   
}