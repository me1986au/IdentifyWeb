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

        public EmergencyContactViewModel(Enumerations.ModifyActionRequired modifyActionRequired)
        {
            ModifyActionRequired = modifyActionRequired;
        }

        public EmergencyContactViewModel(string personsAttributeId, string firstName, string lastName, string @alias, string phoneNumber)
        {
            PersonsAttributeId = personsAttributeId;
            FirstName = firstName;
            LastName = lastName;
            Alias = alias;
            PhoneNumber = phoneNumber;
        }

        public EmergencyContactViewModel (PersonsAttributeDto dto)
        {
            PersonsAttributeId = dto.Id;
            PersonsAttributeCategoryId = dto.PersonsAttributeCategoryId;

            if (dto.PersonalSubAttributeDtos != null && dto.PersonalSubAttributeDtos.Any())
            {
                PersonalSubAttributeId = dto.PersonalSubAttributeDtos.First().Id;
                FirstName = dto.PersonalSubAttributeDtos.First().FirstName;
                LastName = dto.PersonalSubAttributeDtos.First().LastName;
                Alias = dto.PersonalSubAttributeDtos.First().Alias;
            }

            if (dto.PhoneNumberSubAttributeDtos != null && dto.PhoneNumberSubAttributeDtos.Any())
            {
                PhoneNumberSubAttributeId = dto.PhoneNumberSubAttributeDtos.First().Id;
                PhoneNumber = dto.PhoneNumberSubAttributeDtos.First().Number;
            }

            PersonsAttributeId = dto.Id;
            ModifyActionRequired = Enumerations.ModifyActionRequired.Update;
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
        public string PhoneNumberSubAttributeId { get; set; }
        public string PersonalSubAttributeId { get; set; }
        public Enumerations.ModifyActionRequired ModifyActionRequired { get; set; }



        public int PersonsAttributeCategoryId = (int)Enumerations.PersonsAttributeCategoryEnum.EmergencyContact;


    }




    public class EmergencyContactViewModels : List<EmergencyContactViewModel>
    {
        public EmergencyContactViewModels()
        {

        }

        public EmergencyContactViewModels(ICollection<PersonsAttributeDto> dtos)
        {
           if (dtos != null)
            {
                foreach( var attr in dtos)
                {
                    var emergencyContactViewModel = new EmergencyContactViewModel(attr);
                   
                    this.Add(emergencyContactViewModel);
                }
            }

        }

    }

}