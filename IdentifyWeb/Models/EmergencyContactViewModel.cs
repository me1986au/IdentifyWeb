﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using IdentifyWeb.ControllerHelper;

namespace IdentifyWeb.Models
{
    public class EmergencyContactViewModel
    {
        public EmergencyContactViewModel()
        {
        }

        public EmergencyContactViewModel(int id, string personsAttributeId, string firstName, string lastName, string @alias, string phoneNumber)
        {
            Id = id;
            PersonsAttributeId = personsAttributeId;
            FirstName = firstName;
            LastName = lastName;
            Alias = alias;
            PhoneNumber = phoneNumber;
        }


        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Alias { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        public string PersonsAttributeId { get; set; }
        public int PersonsAttributeCategoryId = (int)Enumerations.PersonsAttributeCategoryEnum.EmergencyContact;



    }




    public class EmergencyContactViewModels : List<EmergencyContactViewModel>
    {
        
    }

}