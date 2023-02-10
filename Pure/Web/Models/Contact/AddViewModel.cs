using System;

namespace BreakAway.Models.Contact
{
    public class AddViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime AddDate { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}