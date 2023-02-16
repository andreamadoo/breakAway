using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BreakAway.Entities;

namespace BreakAway.Models.Contact
{

    public class AddressModel
    {
        public int? Id { get; set; }

        public string Street1 { get; set; }

        public string Street2 { get; set; }
      
        public string City { get; set; }

        public string StateProvince { get; set; }

        public string CountryRegion { get; set; }

        public string AddressType { get; set; }

        public string PostalCode { get; set; }
    }

}