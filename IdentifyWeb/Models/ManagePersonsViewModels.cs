﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IdentifyWeb.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using static IdentifyWeb.ControllerHelper.Enumerations;

namespace IdentifyWeb.Models
{
    public class ManagePersonsIndexViewModel
    {

        public List<ItemGroupSection> PageRenderActions { get; set; }

        public ManagePersonsIndexViewModel()
        {

        }
    }

    public class PersonViewModel
    {
        private DateTime _dateOfBirth;
        public PersonViewModel() { }

        public PersonViewModel(ModifyActionRequired modifyActionRequired) {
            ModifyActionRequired = modifyActionRequired;
        }

        public PersonViewModel(string personId, string firstName, string lastName, string @alias, DateTime dateOfBirth, Gender? gender)
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
            Alias = alias;
            DateOfBirth = dateOfBirth;
            Gender = gender;
        }

        public PersonViewModel(PersonDto person)
        {
            PersonId = person.Id;
            FirstName = person.FirstName;
            LastName = person.LastName;
            Alias = person.Alias;
            Gender = person.Gender;
            DateOfBirth = person.DateOfBirth;

            ModifyActionRequired = ModifyActionRequired.Update;

            EmergencyContactViewModels = new EmergencyContactViewModels(person.PersonsAttribute);


            //EmergencyContactViewModels.Add(new EmergencyContactViewModel(1,"sdf-Sd-rt-g1-d2", "Michael","Strange","Snig", "0423170746"));
        }

        public string PersonId { get; set; }


        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Alias")]
        public string Alias { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender? Gender { get; set; }


        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }

        public ModifyActionRequired ModifyActionRequired { get; set; }


        public EmergencyContactViewModels EmergencyContactViewModels { get; set; }



        public PersonDto ToDto()
        {
            var personDto = new PersonDto();

            personDto.Id = PersonId;
            personDto.FirstName = FirstName;
            personDto.LastName = LastName;
            personDto.Alias = Alias;
            personDto.DateOfBirth = DateOfBirth;
            personDto.Gender = Gender.Value;

            personDto.ModifyActionRequired = ModifyActionRequired;


            personDto.PersonsAttribute = new List<PersonsAttributeDto>();

            if (EmergencyContactViewModels != null)
            {
                foreach (var evm in EmergencyContactViewModels)
                {

                    var personsAttributeDto = new PersonsAttributeDto();

                    personsAttributeDto.Id = evm.PersonsAttributeId;
                    personsAttributeDto.PersonId = personDto.Id;
                    personsAttributeDto.PersonsAttributeCategoryId = evm.PersonsAttributeCategoryId;
                    personsAttributeDto.ModifyActionRequired = evm.ModifyActionRequired; 

                    PersonalSubAttributeDto personalSubAttributeDto = new PersonalSubAttributeDto();
                    personalSubAttributeDto.Id = evm.PersonalSubAttributeId;
                    personalSubAttributeDto.FirstName = evm.FirstName;
                    personalSubAttributeDto.LastName = evm.LastName;
                    personalSubAttributeDto.Alias = evm.Alias;
                    personalSubAttributeDto.FirstName = evm.FirstName;

                    personalSubAttributeDto.ModifyActionRequired = evm.ModifyActionRequired;

                    personsAttributeDto.PersonalSubAttributeDtos.Add(personalSubAttributeDto);

                    var phoneNumberSubAttributeDto = new PhoneNumberSubAttributeDto();
                    phoneNumberSubAttributeDto.Id = evm.PhoneNumberSubAttributeId;
                    phoneNumberSubAttributeDto.Number = evm.PhoneNumber;
                    phoneNumberSubAttributeDto.ModifyActionRequired = evm.ModifyActionRequired;

                    personsAttributeDto.PhoneNumberSubAttributeDtos.Add(phoneNumberSubAttributeDto);

                    personDto.PersonsAttribute.Add(personsAttributeDto);

                }
            }

            return personDto;

        }


    }




    public class PersonSectionViewModel
    {

        public List<PersonLinkDto> PersonLinks { get; set; }

        public PersonSectionViewModel(List<Person> persons )
        {
            PersonLinks =  persons.Select(x => new PersonLinkDto(x)).ToList();
        }


        public class PersonLinkDto : LinkDto
        {
            public PersonLinkDto(Person person)
            {
                if (person != null)
                {
                    Id = person.Id;
                    FirstName = person.FirstName;
                    LastName = person.LastName;
                    Alias = person.Alias;
                }
            }

            public string Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Alias { get; set; }

            public override string EditUrl
            {
                get
                {
                    return string.Format("ManagePerson/ModifyPerson?id={0}", Id);
                }
            }

            public override string DeleteUrl
            {
                get
                {
                    return string.Format("ManagePerson/DeletePerson?id={0}", Id);
                }
            }

            public string FullName
            {
                get { return String.Format("{0} {1} ({2})", FirstName, LastName, Alias); }

            }
        }

        public abstract class LinkDto
        {
            public string Id { get; set; }
            public abstract string EditUrl { get; }
            public abstract string DeleteUrl { get; }
            public bool CanEdit { get; set; }
            public bool CanDelete { get; set; }

        }

    }
}