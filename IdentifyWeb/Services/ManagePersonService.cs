using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using IdentifyWeb.Models;
using System.Data.Entity;

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

                var person = dbContext.Persons.FirstOrDefault(x => x.Id == personId);
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

                if (personDto != null)
                {

                    var person = new Person();

                    if (personDto.Id != null)
                    {
                        if (!ManagePersonService.CheckIfPersonBelongsToUser(userId, personDto.Id))
                            return false;

                        person = dbContext.Persons.FirstOrDefault(x => x.Id == personDto.Id);

                        dbContext.Entry(person).State = EntityState.Modified;
                    }
                    else
                    {
                        person.Id = Guid.NewGuid().ToString();
                        dbContext.Persons.Add(person);
                    }
                    personDto.ApplicationUserId = userId;

                    TransferPersonInfoFromDtoToEntity(personDto, person); ;

                    PersonalSubAttribute personalSubAttribute = new PersonalSubAttribute();
                    if (person.PersonsAttribute.Any(x => x.PersonalSubAttribute.Any()))
                    {
                       personalSubAttribute.
                    }


                    //person.PersonsAttribute.Add(new PersonsAttribute());
                    dbContext.SaveChanges();
                }

            }
            return true;

        }


        private static void TransferPersonInfoFromDtoToEntity(PersonDto sourceDto, Person targetEntity)
        {
            targetEntity.Id = sourceDto.Id;
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