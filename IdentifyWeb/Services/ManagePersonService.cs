using System;
using System.Collections.Generic;
using System.Linq;
using IdentifyWeb.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace IdentifyWeb.Services
{
    public static class ManagePersonService
    {
        public static List<Person> GetPersonsByUserId(string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {

                var persons = dbContext.Persons.Where(x => x.ApplicationUserId == userId).ToList();

                return persons;
            }
        }


        public static bool CheckIfPersonBelongsToUser(string userId, string personId)
        {
            using (var dbContext = new ApplicationDbContext())
            {

                var returnValue = dbContext.Persons.Any(x => x.Id == personId && x.ApplicationUserId == userId);

                return returnValue;
            }
        }

        public static PersonDto GetPerson(string personId)
        {
            using (var dbContext = new ApplicationDbContext())
            {

                var person = dbContext.Persons
                    .Include(p => p.PersonsAttribute)
                    .Include(p => p.PersonsAttribute.Select(c => c.PersonalSubAttribute))
                    .Include(p => p.PersonsAttribute.Select(c => c.PhoneNumberSubAttribute))
                    .FirstOrDefault(x => x.Id == personId);

                var personDto = new PersonDto(person);
                return personDto;
            }
        }

        private static Person SaveEmergencyContactAttribute(EmergencyContactAttributeDto emergencyContactAttributeDto)
        {
            using (var dbContext = new ApplicationDbContext())
            {



                var person = dbContext.Persons.FirstOrDefault();

                return person;
            }
        }


        public static bool SavePerson(string userId, PersonDto personDto)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {

                try
                {

                    if (personDto != null)
                    {

                        var person = new Person();

                        var isNew = personDto.Id == null;

                        if (!isNew)
                        {
                            if (!ManagePersonService.CheckIfPersonBelongsToUser(userId, personDto.Id))
                                return false;

                            person = dbContext.Persons.FirstOrDefault(x => x.Id == personDto.Id);

                            dbContext.Entry(person).State = EntityState.Modified;
                        }
                        else
                        {
                            dbContext.Persons.Add(person);
                        }
                        personDto.ApplicationUserId = userId;

                        TransferPersonInfoFromDtoToEntity(personDto, person, isNew); ;

                        var attributes = personDto.PersonsAttribute;

                        //add new personal attributes.
                        var newAttributes = attributes.Where(x => x.Id == null).ToList();

                        foreach (var attr in newAttributes)
                        {
                            var personAttributeEntity = new PersonsAttribute();
                            personAttributeEntity.PersonsAttributeCategoryId = attr.PersonsAttributeCategoryId;
                            personAttributeEntity.PersonId = personDto.Id;
                            personAttributeEntity.Id = Guid.NewGuid().ToString();
                            dbContext.Entry(personAttributeEntity).State = EntityState.Added;


                            personAttributeEntity.PhoneNumberSubAttribute = new List<PhoneNumberSubAttribute>();
                            foreach (var phoneAttribute in attr.PhoneNumberSubAttributeDtos)
                            {
                                var phoneNumberSubAttributeEntity = new PhoneNumberSubAttribute();
                                phoneNumberSubAttributeEntity.Id = Guid.NewGuid().ToString();
                                phoneNumberSubAttributeEntity.Ext = phoneAttribute.Ext;
                                phoneNumberSubAttributeEntity.Number = phoneAttribute.Number;

                                personAttributeEntity.PhoneNumberSubAttribute.Add(phoneNumberSubAttributeEntity);
                            }

                            // 
                            personAttributeEntity.PersonalSubAttribute = new List<PersonalSubAttribute>();
                            foreach (var personalSubAttribute in attr.PersonalSubAttributeDtos)
                            {
                                var personalSubAttributeEntity = new PersonalSubAttribute();
                                personalSubAttributeEntity.Id = Guid.NewGuid().ToString();
                                personalSubAttributeEntity.FirstName = personalSubAttribute.FirstName;
                                personalSubAttributeEntity.LastName = personalSubAttribute.LastName;
                                personalSubAttributeEntity.Alias = personalSubAttribute.Alias;
                                
                                personAttributeEntity.PersonalSubAttribute.Add(personalSubAttributeEntity);
                            }

                            if (person.PersonsAttribute == null)
                            {
                                person.PersonsAttribute = new List<PersonsAttribute>();
                            }

                            person.PersonsAttribute.Add(personAttributeEntity);

                        }



                        //person.PersonsAttribute.Add(new PersonsAttribute());
                        dbContext.SaveChanges();
                    }


                }
                catch (DbEntityValidationException e)
                {

                }




            }
            return true;

        }


        private static void TransferPersonInfoFromDtoToEntity(PersonDto sourceDto, Person targetEntity, bool generateNewPersonId = false)
        {
            targetEntity.Id = generateNewPersonId ? Guid.NewGuid().ToString() : sourceDto.Id;
            targetEntity.ApplicationUserId = sourceDto.ApplicationUserId;
            targetEntity.FirstName = sourceDto.FirstName;
            targetEntity.LastName = sourceDto.LastName;
            targetEntity.Alias = sourceDto.Alias;
            targetEntity.DateOfBirth = sourceDto.DateOfBirth;
            targetEntity.Gender = sourceDto.Gender;
        }


        private static void UpdatePersonAttributuesWithinEntity(ApplicationDbContext dbContext, PersonDto sourceDto, Person targetEntity)
        {
            var dbPersonAttributes = targetEntity.PersonsAttribute.ToList();
            var viewPersonAttributes = sourceDto.PersonsAttribute.ToList();

            // Remove update Old Attributes

            foreach (var dbAttr in dbPersonAttributes)
            {
                if (!viewPersonAttributes.Any(x => x.Id == dbAttr.Id))
                {
                    dbContext.Entry(dbAttr).State = EntityState.Deleted;

                }
                else
                {
                    dbContext.Entry(dbAttr).State = EntityState.Modified;
                }
            }

            // foreach(var newAttribute)






        }


    }
}