using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }




    public class EmergencyContactViewModels : List<EmergencyContactViewModel>
    {
        
    }

}