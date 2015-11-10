using System;
using System.Collections.Generic;
using IdentifyWeb.ControllerHelper;
using IdentifyWeb.Models;

namespace IdentifyWeb.Services
{


    public class EmergencyContactAttributeDto : PersonsAttributeDto
    {

        public EmergencyContactAttributeDto()
        {
            PersonsAttributeCategoryId = (int)Enumerations.PersonsAttributeCategoryEnum.EmergencyContact;
        }

    }


    public class PersonDto
    {
        public string Id { get; set; }
        public string ApplicationUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }


        public ICollection<PersonsAttributeDto> PersonsAttribute { get; set; }

        public PersonDto()
        {
        }

        public PersonDto(Person person)
        {

            Id = person.Id;
            ApplicationUserId = person.ApplicationUserId;
            FirstName = person.FirstName;
            LastName = person.LastName;
            Alias = person.Alias;
            Gender = person.Gender;
            DateOfBirth = person.DateOfBirth;

            PersonsAttribute = new List<PersonsAttributeDto>();

            foreach (var pattr in person.PersonsAttribute)
            {
                var dto = new PersonsAttributeDto();

                dto.Id = pattr.Id;
                dto.PersonsAttributeCategoryId = pattr.PersonsAttributeCategoryId;

                if (pattr.PersonalSubAttribute != null)
                {
                    foreach (var personalAttribute in pattr.PersonalSubAttribute)
                    {

                        var dto1 = new PersonalSubAttributeDto();

                        dto1.PersonsAttributeId = pattr.Id;

                        dto1.Id = personalAttribute.Id;
                        dto1.FirstName = personalAttribute.FirstName;
                        dto1.LastName = personalAttribute.LastName;
                        dto1.Alias = personalAttribute.Alias;

                        dto.PersonalSubAttributeDtos.Add(dto1);
                    }
                }

                if (pattr.PhoneNumberSubAttribute != null)
                {
                    foreach (var phone in pattr.PhoneNumberSubAttribute)
                    {

                        var dto1 = new PhoneNumberSubAttributeDto();

                        dto1.PersonsAttributeId = pattr.Id;

                        dto1.Id = phone.Id;
                        dto1.Ext = phone.Ext;
                        dto1.Number = phone.Number;

                        dto.PhoneNumberSubAttributeDtos.Add(dto1);

                    }
                }

                this.PersonsAttribute.Add(dto);

            }
        }


    }


        public class PersonsAttributeDto
        {

            public PersonsAttributeDto()
            {
                AddressSubAttributeDtos = new List<AddressSubAttributeDto>();
                PhoneNumberSubAttributeDtos = new List<PhoneNumberSubAttributeDto>();
                PersonalSubAttributeDtos = new List<PersonalSubAttributeDto>();
                TimeFrameSubAttributeDtos = new List<TimeFrameSubAttributeDto>();
            }

            public string Id { get; set; }
            public int PersonsAttributeCategoryId { get; set; }


            public ICollection<AddressSubAttributeDto> AddressSubAttributeDtos { get; set; }
            public ICollection<PhoneNumberSubAttributeDto> PhoneNumberSubAttributeDtos { get; set; }
            public ICollection<PersonalSubAttributeDto> PersonalSubAttributeDtos { get; set; }
            public ICollection<TimeFrameSubAttributeDto> TimeFrameSubAttributeDtos { get; set; }

            public string PersonId { get; set; }

        }


        public class PersonalSubAttributeDto
        {
            public string Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Alias { get; set; }

            public string PersonsAttributeId { get; set; }
        }

        public class PhoneNumberSubAttributeDto
        {

            public string Id { get; set; }
            public string Ext { get; set; }
            public string Number { get; set; }

            public string PersonsAttributeId { get; set; }

        }

        public class AddressSubAttributeDto
        {
            public string Id { get; set; }
            public string StreetAddress { get; set; }
            public string StreetAddress1 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostCode { get; set; }
            public string CountryRegion { get; set; }

            public string PersonsAttributeId { get; set; }

        }

        public class TimeFrameSubAttributeDto
        {
            public string Id { get; set; }
            public DateTime From { get; set; }
            public DateTime To { get; set; }

            public string PersonsAttributeId { get; set; }

        }



    }

