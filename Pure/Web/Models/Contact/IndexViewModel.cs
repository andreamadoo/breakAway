using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BreakAway.Entities;

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

    public class ContactItem
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        
    }
}