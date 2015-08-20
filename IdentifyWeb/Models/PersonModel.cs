using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentifyWeb.Models
{
    public class Person
    {
        public string PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }
        public int Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

    }
}