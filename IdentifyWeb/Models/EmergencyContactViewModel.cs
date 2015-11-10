using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using IdentifyWeb.ControllerHelper;
using IdentifyWeb.Services;

namespace IdentifyWeb.Models
{
    public class EmergencyContactViewModel
    {
        public EmergencyContactViewModel()
        {
        }

        public EmergencyContactViewModel(string personsAttributeId, string firstName, string lastName, string @alias, string phoneNumber)
        {
            PersonsAttributeId = personsAttributeId;
            FirstName = firstName;
            LastName = lastName;
            Alias = alias;
            PhoneNumber = phoneNumber;
        }


        
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
        public EmergencyContactViewModels(PersonDto dto)
        {
           if (dto.PersonsAttribute != null)
            {
                foreach( var attr in dto.PersonsAttribute)
                {
                    var emergencyContactViewModel = new EmergencyContactViewModel();

                    emergencyContactViewModel.PersonsAttributeId = attr.Id;
                    emergencyContactViewModel.PersonsAttributeCategoryId = attr.PersonsAttributeCategoryId;

                    if (attr.PersonalSubAttributeDtos != null && attr.PersonalSubAttributeDtos.Any())
                    {
                        emergencyContactViewModel.FirstName = attr.PersonalSubAttributeDtos.First().FirstName;
                        emergencyContactViewModel.LastName = attr.PersonalSubAttributeDtos.First().LastName;
                        emergencyContactViewModel.Alias = attr.PersonalSubAttributeDtos.First().Alias;
                    }

                    if (attr.PhoneNumberSubAttributeDtos != null && attr.PhoneNumberSubAttributeDtos.Any())
                    {
                        emergencyContactViewModel.PhoneNumber = attr.PhoneNumberSubAttributeDtos.First().Number;
                    }

                    emergencyContactViewModel.PersonsAttributeId = attr.Id;
                    this.Add(emergencyContactViewModel);
                }
            }

        }

    }

}