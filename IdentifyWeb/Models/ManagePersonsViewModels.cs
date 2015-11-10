using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using IdentifyWeb.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

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


            EmergencyContactViewModels = new EmergencyContactViewModels(person);

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


            personDto.PersonsAttribute = new List<PersonsAttributeDto>();

            if (EmergencyContactViewModels != null)
            {
                foreach (var evm in EmergencyContactViewModels)
                {

                    var personalAttributeDto = new PersonsAttributeDto();

                    personalAttributeDto.Id = evm.PersonsAttributeId;
                    personalAttributeDto.PersonId = personDto.Id;
                    personalAttributeDto.PersonsAttributeCategoryId = evm.PersonsAttributeCategoryId;

                    PersonalSubAttributeDto personalSubAttributeDto = new PersonalSubAttributeDto();
                    personalSubAttributeDto.FirstName = FirstName;
                    personalSubAttributeDto.LastName = LastName;
                    personalSubAttributeDto.Alias = Alias;
                    personalSubAttributeDto.FirstName = FirstName;

                    personalAttributeDto.PersonalSubAttributeDtos.Add(personalSubAttributeDto);

                    var phoneNumberSubAttributeDto = new PhoneNumberSubAttributeDto();
                    phoneNumberSubAttributeDto.Number = evm.PhoneNumber;
                    personalAttributeDto.PhoneNumberSubAttributeDtos.Add(phoneNumberSubAttributeDto);

                    personDto.PersonsAttribute.Add(personalAttributeDto);

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