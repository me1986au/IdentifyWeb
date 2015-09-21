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
                var personDto = new PersonDto();
                return person;
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


        private static bool SavePerson(string userId, PersonDto personDto)
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

                        person = ManagePersonService.GetPerson(personDto.Id);
                        dbContext.Entry(person).State = EntityState.Modified;
                    }
                    else
                    {
                        person.Id = Guid.NewGuid().ToString();
                        dbContext.Persons.Add(person);
                    }

                    person.ApplicationUserId = userId;
                    person.FirstName = personDto.FirstName;
                    person.LastName = personDto.LastName;
                    person.Alias = personDto.Alias;
                    person.DateOfBirth = personDto.DateOfBirth;
                    person.Gender = personDto.Gender;

                    PersonalSubAttribute personalSubAttribute = new PersonalSubAttribute();
                    if (person.PersonsAttribute.Any(x => x.PersonalSubAttribute.Any()))
                    {
                        personalSubAttribute = Pw
                    }



                    person.PersonsAttribute.Add(new PersonsAttribute());


                    dbContext.SaveChanges();
                }

            }
            return true;

        }


    }
}