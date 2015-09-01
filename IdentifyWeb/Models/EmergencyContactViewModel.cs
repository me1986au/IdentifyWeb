using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentifyWeb.Models
{
    public class EmergencyContactViewModel
    {
        public EmergencyContactViewModel()
        {
        }

        public EmergencyContactViewModel(int id, string name, string phoneNumber)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

    }

    public class EmergencyContactViewModels : List<EmergencyContactViewModel>
    {
        
    }

}